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
using System.Linq;

namespace yarn_brokerage.ViewModels
{
    public class CountViewModel : BaseViewModel
    {
        public InfiniteScrollCollection<Count> Counts { get; set; }
        public Count _Count { get; set; }
        public Command LoadCountsCommand { get; set; }
        private const int PageSize = 20;
        public CountViewModel()
        {
            Title = "Count List";
            Counts = new InfiniteScrollCollection<Count>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = Counts.Count / PageSize;

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
                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/count_list", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        Count_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<Count_list>(response);
                        //foreach (Ledger item in res.ledgers)
                        //{
                        //Ledgers.AddRange(res.ledgers);
                        //}
                        IsBusy = false;
                        return res.counts;
                    }
                    // return the items that need to be added

                },
                OnCanLoadMore = () =>
                {
                    return Counts.Count < totalRows;
                }
            };
            LoadCountsCommand = new Command(async (object searchstring) => await ExecuteLoadCountsCommand(searchstring));
        }

        private string _SearchString = "";
        private int totalRows = 0;
        
        async Task ExecuteLoadCountsCommand(object searchstring)
        {
            if (IsBusy)
                return;
            
            _SearchString = "";
            if (searchstring != null)
                _SearchString = (string)searchstring;

            IsBusy = true;

            try
            {
                Counts.Clear();
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
                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/count_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            Count_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<Count_list>(response);
                            //foreach (Count item in res.counts)
                            //{
                                Counts.AddRange(res.counts);
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

        internal async void StoreCountCommand(Count count)
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
                            new KeyValuePair<string,string>("id",count.Id.ToString()),                            
                            new KeyValuePair<string,string>("name",count.Name.ToString()),
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/count_store", formcontent);

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




        public async Task SupplierCountList(DraftConfirmation DraftConfirmation)
        {
            if (IsBusy)
                return;


            //if (DraftConfirmation == null)
            //    return;

            IsBusy = true;

            try
            {
                Counts.Clear();
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
                                    new KeyValuePair<string,string>("id",DraftConfirmation.supplier_id.ToString()),
                              });

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/ledger_supplier_count_from_app", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            IEnumerable<Count> res = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Count>>(response);
                            //foreach (Count item in res.counts)
                            //{
                            Counts.AddRange(res);
                            
                            //}

                        }
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            catch(Exception EX)
            {
                Debug.WriteLine(EX);
            }
            finally
            {
                IsBusy = false;
            }
         
        }

        public async Task<int> CountsCount()
        {
            return Counts.Count();
        }
    }
}