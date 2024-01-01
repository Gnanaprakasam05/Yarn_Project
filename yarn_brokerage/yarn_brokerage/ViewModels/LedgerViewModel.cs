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
    public class LedgersViewModel : BaseViewModel
    {
        public InfiniteScrollCollection<Ledger> Ledgers { get; set; }
        public Command LoadLedgersCommand { get; set; }
        public Command LoadSearchLedgersCommand { get; set; }

        public int _ledger_type;

        private const int PageSize = 20;

        public LedgersViewModel(int ledger_type)
        {
            _ledger_type = ledger_type;
            if (ledger_type == 1)
                Title = "Supplier List";
            else
                Title = "Customer List";
            Ledgers = new InfiniteScrollCollection<Ledger>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = Ledgers.Count / PageSize;

                    //var items = await _dataService.GetItemsAsync(page, PageSize);

                    using (var cl = new HttpClient())
                    {
                        HttpContent formcontent = null;
                        if (_ledger_type == 1)
                        {
                            formcontent = new FormUrlEncodedContent(new[]
                            {
                                    new KeyValuePair<string,string>("ledger_type","1"),
                                    new KeyValuePair<string,string>("search_string",_SearchString),
                                    new KeyValuePair<string,string>("CurrentPageNumber",page.ToString()),
                                    new KeyValuePair<string,string>("PageSize",PageSize.ToString())
                                });
                        }
                        else if (_ledger_type == 2)
                        {
                            formcontent = new FormUrlEncodedContent(new[]
                            {
                                    new KeyValuePair<string,string>("ledger_type","2"),
                                    new KeyValuePair<string,string>("search_string",_SearchString),
                                    new KeyValuePair<string,string>("CurrentPageNumber",page.ToString()),
                                    new KeyValuePair<string,string>("PageSize",PageSize.ToString())
                                });
                        }
                        else
                        {
                            formcontent = new FormUrlEncodedContent(new[]
                            {                                   
                                    new KeyValuePair<string,string>("search_string",_SearchString),
                                    new KeyValuePair<string,string>("CurrentPageNumber",page.ToString()),
                                    new KeyValuePair<string,string>("PageSize",PageSize.ToString())
                            });
                        }

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/ledger_list", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        Ledger_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<Ledger_list>(response);
                        //foreach (Ledger item in res.ledgers)
                        //{
                        //Ledgers.AddRange(res.ledgers);
                        //}
                        IsBusy = false;
                        return res.ledgers;
                    }
                    // return the items that need to be added
                    
                },
                OnCanLoadMore = () =>
                {
                    return Ledgers.Count < totalRows;
                }
            };
            
            LoadLedgersCommand = new Command(async (object CountId) => await ExecuteLoadLedgersCommand(CountId));
            //LoadSearchLedgersCommand = new Command(async (object SearchString) => await ExecuteSearchLoadLedgersCommand(SearchString));
            //MessagingCenter.Subscribe<NewLedgerPage, Ledger>(this, "AddLedger", async (obj, Ledger) =>
            //{
            //    var newItem = Ledger as Ledger;
            //    Ledgers.Add(newItem);
            //    await App.Database.SaveLedgerAsync(newItem);
            //});
        }
        private string _SearchString = "";
        private int totalRows = 0;
        async Task ExecuteLoadLedgersCommand(object searchstring)
        {            
            if (IsBusy)
                return;

            _SearchString = "";
            if (searchstring != null)
                _SearchString = (string)searchstring;

            IsBusy = true;

            try
            {
                Ledgers.Clear();
                try
                {
                    var current = Connectivity.NetworkAccess;

                    if (current == NetworkAccess.Internet)
                    {
                        using (var cl = new HttpClient())
                        {
                            HttpContent formcontent = null;
                            if (_ledger_type == 1)
                            {
                                formcontent = new FormUrlEncodedContent(new[]
                                {
                                    new KeyValuePair<string,string>("ledger_type","1"),
                                    new KeyValuePair<string,string>("search_string",_SearchString),
                                    new KeyValuePair<string,string>("CurrentPageNumber","0"),
                                    new KeyValuePair<string,string>("PageSize",PageSize.ToString())                                    
                                });
                            }
                            else if (_ledger_type == 2)
                            {
                                formcontent = new FormUrlEncodedContent(new[]
                                {
                                    new KeyValuePair<string,string>("ledger_type","2"),
                                    new KeyValuePair<string,string>("search_string",_SearchString),
                                    new KeyValuePair<string,string>("CurrentPageNumber","0"),
                                    new KeyValuePair<string,string>("PageSize",PageSize.ToString())
                                });
                            }
                            else
                            {
                                formcontent = new FormUrlEncodedContent(new[]
                                {
                                    new KeyValuePair<string,string>("search_string",_SearchString),
                                    new KeyValuePair<string,string>("CurrentPageNumber","0"),
                                    new KeyValuePair<string,string>("PageSize",PageSize.ToString())
                            });
                            }
                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/ledger_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            Ledger_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<Ledger_list>(response);
                            //foreach (Ledger item in res.ledgers)
                            //{
                                Ledgers.AddRange(res.ledgers);
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

        internal async void StoreLedgerCommand(Ledger ledger)
        {
            //Indexes enquiry = (Indexes)_enquiry;
            if (ledger.MobileNo == null)
                ledger.MobileNo = "";
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
                            new KeyValuePair<string,string>("id",ledger.Id.ToString()),
                            new KeyValuePair<string,string>("name",ledger.Name.ToString()),
                            new KeyValuePair<string,string>("ledger_type",ledger.LedgerType.ToString()),
                            new KeyValuePair<string,string>("mobile_no",ledger.MobileNo.ToString()),
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/ledger_store", formcontent);

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

        //async internal void ExecuteSearchLoadLedgersCommand(string SearchString="", int CountId=0)
        //{
        //    if (IsBusy)
        //        return;

        //    string _SearchString = "";
        //    if (SearchString != null)
        //        _SearchString = (string)SearchString;

        //    if (CountId == null)
        //        CountId = 0;

        //    IsBusy = true;

        //    try
        //    {
        //        Ledgers.Clear();
        //        var Ledgers = await App.Database.GetLedgersAsync((int)CountId, _SearchString);
        //        foreach (var Ledger in Ledgers)
        //        {
        //            Ledgers.Add(Ledger);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}

        //async Task ExecuteSearchLoadLedgersCommand(object SearchString)
        //{
        //    if (IsBusy)
        //        return;

        //    string _SearchString = "";
        //    if (SearchString != null)
        //        _SearchString = (string)SearchString;

        //    IsBusy = true;

        //    try
        //    {
        //        Ledgers.Clear();
        //        var Ledgers = await App.Database.GetLedgersAsync(0,_SearchString);
        //        foreach (var Ledger in Ledgers)
        //        {
        //            Ledgers.Add(Ledger);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}
    }
}