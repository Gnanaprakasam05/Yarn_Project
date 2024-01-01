using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using yarn_brokerage.Models;
using yarn_brokerage.Views;
using Xamarin.Forms.Extended;

namespace yarn_brokerage.ViewModels
{
    public class TransporterViewModel : BaseViewModel
    {
        public InfiniteScrollCollection<Transporter> Transporters { get; set; }
        public Transporter _Transporter { get; set; }
        public Command LoadTransportersCommand { get; set; }
        private const int PageSize = 20;
        public TransporterViewModel()
        {
            Title = "Transporter List";
            Transporters = new InfiniteScrollCollection<Transporter>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = Transporters.Count / PageSize;

                    //var items = await _dataService.GetItemsAsync(page, PageSize);

                    using (var cl = new HttpClient())
                    {
                        HttpContent formcontent = null;
                        formcontent = new FormUrlEncodedContent(new[]
                           {
                                    new KeyValuePair<string,string>("search_string",_SearchString),
                                    new KeyValuePair<string,string>("CurrentPageNumber",page.ToString()),
                                    new KeyValuePair<string,string>("PageSize",PageSize.ToString())

                            });
                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/transporter_list", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        Transporter_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<Transporter_list>(response);
                        //foreach (Ledger item in res.ledgers)
                        //{
                        //Ledgers.AddRange(res.ledgers);
                        //}
                        IsBusy = false;
                        return res.Transporters;
                    }
                    // return the items that need to be added

                },
                OnCanLoadMore = () =>
                {
                    return Transporters.Count < totalRows;
                }
            };
            LoadTransportersCommand = new Command(async (object searchstring) => await ExecuteLoadTransportersCommand(searchstring));
        }

        private string _SearchString = "";
        private int totalRows = 0;
        
        async Task ExecuteLoadTransportersCommand(object searchstring)
        {
            if (IsBusy)
                return;
            
            _SearchString = "";
            if (searchstring != null)
                _SearchString = (string)searchstring;

            IsBusy = true;

            try
            {
                Transporters.Clear();
                try
                {
                    var current = Connectivity.NetworkAccess;

                    if (current == NetworkAccess.Internet)
                    {
                        using (var cl = new HttpClient())
                        {
                            HttpContent formcontent = null;
                            formcontent = new FormUrlEncodedContent(new[]
                            {
                                    new KeyValuePair<string,string>("search_string",_SearchString),
                                    new KeyValuePair<string,string>("CurrentPageNumber","0"),
                                    new KeyValuePair<string,string>("PageSize",PageSize.ToString())

                            });                            
                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/transporter_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            Transporter_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<Transporter_list>(response);
                            //foreach (Transporter item in res.Transporters)
                            //{
                                Transporters.AddRange(res.Transporters);
                                totalRows = res.totalRows;
                            //}

                        }
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        internal async void StoreTransporterCommand(Transporter Transporter)
        {
            //Indexes enquiry = (Indexes)_enquiry;
            try
            {
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    using (var cl = new HttpClient())
                    {
                        HttpContent formcontent = null;

                        formcontent = new FormUrlEncodedContent(new[]
                        {
                            new KeyValuePair<string,string>("id",Transporter.Id.ToString()),                            
                            new KeyValuePair<string,string>("name",Transporter.Name.ToString()),
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/transporter_store", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        //Enquiry res = Newtonsoft.Json.JsonConvert.DeserializeObject<Enquiry>(response);
                        //foreach (Indexes item in res.Index)
                        //{
                        //    Enquiries.Add(item);
                        //}

                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            // return "Sucess";
        }
    }
}