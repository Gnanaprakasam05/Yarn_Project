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
    public class GodownUnloadingViewModel : BaseViewModel
    {
        public InfiniteScrollCollection<GodownUnloading> GodownUnloadings { get; set; }
        public GodownUnloading _GodownUnloading { get; set; }
        public Command LoadGodownUnloadingsCommand { get; set; }
        private const int PageSize = 20;
        public GodownUnloadingViewModel()
        {
            Title = "GodownUnloading List";
            GodownUnloadings = new InfiniteScrollCollection<GodownUnloading>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = GodownUnloadings.Count / PageSize;

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
                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/godown_unloading_list", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        GodownUnloading_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<GodownUnloading_list>(response);
                        //foreach (Ledger item in res.ledgers)
                        //{
                        //Ledgers.AddRange(res.ledgers);
                        //}
                        IsBusy = false;
                        return res.GodownUnloadings;
                    }
                    // return the items that need to be added

                },
                OnCanLoadMore = () =>
                {
                    return GodownUnloadings.Count < totalRows;
                }
            };
            LoadGodownUnloadingsCommand = new Command(async (object searchstring) => await ExecuteLoadGodownUnloadingsCommand(searchstring));
        }

        private string _SearchString = "";
        private int totalRows = 0;
        
        async Task ExecuteLoadGodownUnloadingsCommand(object searchstring)
        {
            if (IsBusy)
                return;
            
            _SearchString = "";
            if (searchstring != null)
                _SearchString = (string)searchstring;

            IsBusy = true;

            try
            {
                GodownUnloadings.Clear();
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
                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/godown_unloading_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            GodownUnloading_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<GodownUnloading_list>(response);
                            //foreach (GodownUnloading item in res.GodownUnloadings)
                            //{
                                GodownUnloadings.AddRange(res.GodownUnloadings);
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

        internal async void StoreGodownUnloadingCommand(GodownUnloading GodownUnloading)
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
                            new KeyValuePair<string,string>("id",GodownUnloading.Id.ToString()),                            
                            new KeyValuePair<string,string>("name",GodownUnloading.Name.ToString()),
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/godown_unloading_store", formcontent);

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