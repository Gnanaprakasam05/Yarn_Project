using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using yarn_brokerage.Models;
using yarn_brokerage.Views;
using Xamarin.Essentials;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Extended;

namespace yarn_brokerage.ViewModels
{
    public class CallLogViewModel : BaseViewModel
    {
        public InfiniteScrollCollection<CallLogModel> CallLogs { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command SearchItemsCommand { get; set; }
        public DateTime date { get; set; }
        public SearchCallLogFilter SearchFilter { get; set; }
        // public Command StorecallLogCommand { get; set; }
        private const int PageSize = 20;

        public CallLogViewModel(SearchCallLogFilter searchFilter =null)
        {
            //if (_transaction_type == 1)
            //    Title = "Offers";
            //else
            SearchFilter = searchFilter;
            Title = "Call History";
            date = DateTime.Now.ToLocalTime();
            CallLogs = new InfiniteScrollCollection<CallLogModel>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = CallLogs.Count / PageSize;
                    using (var cl = new HttpClient())
                    {
                        HttpContent formcontent = null;
                        if (SearchFilter != null)
                        {
                            formcontent = new FormUrlEncodedContent(new[]
                           {
                                new KeyValuePair<string,string>("call_date_from", SearchFilter.call_date_from.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string,string>("call_date_to", SearchFilter.call_date_to.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                new KeyValuePair<string,string>("s_user_id",SearchFilter.user_id.ToString()),
                                new KeyValuePair<string,string>("call_date", SearchFilter.call_date.ToString()),
                                new KeyValuePair<string,string>("CurrentPageNumber",page.ToString()),
                                new KeyValuePair<string,string>("PageSize",PageSize.ToString())
                            });
                        }
                        else
                        {
                            formcontent = new FormUrlEncodedContent(new[]
                            {
                                new KeyValuePair<string,string>("search_string",_SearchString.Search_string),
                                new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                new KeyValuePair<string,string>("CurrentPageNumber",page.ToString()),
                                new KeyValuePair<string,string>("PageSize",PageSize.ToString()),
                            });
                        }

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/call_log_list", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        CallLog_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<CallLog_list>(response);
                        CallLog_list CallLog_List = new CallLog_list();
                        CallLog_List.totalRows = res.totalRows;
                        CallLog_List.CallLogs = new List<CallLogModel>();
                        foreach (CallLogModel item in res.CallLogs)
                        {
                            if (item.LedgerName == null)
                                item.LedgerName = "Unknown";
                            if (Application.Current.Properties["user_type"].ToString() == "1")
                                item.admin_user = true;                           
                            CallLog_List.CallLogs.Add(item);
                        }
                        IsBusy = false;
                        return CallLog_List.CallLogs;
                    }
                },
                OnCanLoadMore = () =>
                {
                    return CallLogs.Count < totalRows;
                }
            };

            if (searchFilter == null)
                LoadItemsCommand = new Command(async (object searchstring) => await ExecuteLoadItemsCommand(searchstring));
            else if (searchFilter != null)
                LoadItemsCommand = new Command(async (object searchFilters) => await ExecuteStoreItemsCommand(searchFilter));
            SearchItemsCommand = new Command(async (object searchFilters) => await ExecuteStoreItemsCommand(searchFilters));            
        }
        private int totalRows = 0;
        search_string _SearchString;
        async Task ExecuteLoadItemsCommand(object searchstring)
        {
            if (IsBusy)
                return;
            _SearchString = searchstring as search_string;
            IsBusy = true;

            try
            {
                CallLogs.Clear();
                
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
                                new KeyValuePair<string,string>("search_string",_SearchString.Search_string),
                                new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                new KeyValuePair<string,string>("CurrentPageNumber","0"),
                                new KeyValuePair<string,string>("PageSize",PageSize.ToString()),
                            });
                            
                            
                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/call_log_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            //CallLog_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<CallLog_list>(response);
                            //foreach (CallLogModel item in res.CallLogs)
                            //{
                            //    if (item.LedgerName == null)
                            //        item.LedgerName = "Unknown";
                            //    if (Application.Current.Properties["user_type"].ToString() == "1")
                            //        item.admin_user = true;                                
                            //    CallLogs.Add(item);
                            //}
                            totalRows = 0; // res.totalRows;
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

        public async void StoreCallLog(CallLogModel callLog)
        {
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
                            new KeyValuePair<string,string>("id",callLog.Id.ToString()),
                            new KeyValuePair<string,string>("call_name",callLog.CallName), //THH:mm
                            new KeyValuePair<string,string>("call_number",callLog.CallNumber.ToString()),
                            new KeyValuePair<string,string>("call_duration",callLog.CallDuration.ToString()),
                            new KeyValuePair<string,string>("call_duration_format",callLog.CallDurationFormat.ToString()),
                            new KeyValuePair<string,string>("call_date_tick",callLog.CallDateTick.ToString()),
                            new KeyValuePair<string,string>("call_date",callLog.CallDate.ToString("yyyy/MM/dd HH:mm:ss")),
                            new KeyValuePair<string,string>("call_type",callLog.CallType.ToString()),
                            new KeyValuePair<string,string>("call_title",callLog.CallTitle.ToString()),
                            new KeyValuePair<string,string>("call_description",callLog.CallDescription.ToString()),
                            new KeyValuePair<string,string>("remarks",(callLog.remarks!=null)?callLog.remarks.ToString():""),
                            new KeyValuePair<string,string>("ledger_id",callLog.ledger_id.ToString()),
                            new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                            new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/call_log_store", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response =  await request.Content.ReadAsStringAsync();


                        //callLog_List res = Newtonsoft.Json.JsonConvert.DeserializeObject<callLog_List>(response);
                        //if (res.message == "Record added/updated successfully.")
                        {
                            //callLog.id = res.data.id;
                            //callLog.offer_counts = (callLog.transaction_type == 2) ? (Convert.ToInt32(res.count) > 1) ? res.count + " Offers" : res.count + " Offer" : (Convert.ToInt32(res.count) > 1) ? res.count + " Enquiries" : res.count + " callLog";
                            //callLog.current_price = res.data.current_price;
                            //Title = "Edit callLog";
                            //callLog.price_per = callLog.current_price.ToString() + " " + callLog.per;
                            //callLog.qty_unit = callLog.qty.ToString() + " " + callLog.unit;

                            //lblShowOffers.Text = callLog.offer_counts;
                            //txtCurrentPrice.Text = (callLog.current_price > 0) ? string.Format("{0}", callLog.current_price) : "";

                            //if (res.best_list != null)
                            //{
                            //    callLog.best_supplier = res.best_list.ledger_name;
                            //    callLog.best_qty = res.best_list.qty;
                            //    callLog.best_price = res.best_list.current_price;
                            //    //lblBestSupplier.Text = callLog.best_supplier;
                            //    //lblBestQty.Text = string.Format("{0:0}", callLog.best_qty)+ " Bags"; 
                            //    //lblBestPrice.Text = "Rs." + string.Format("{0:0.00}", callLog.best_price); 
                            //}
                            //else
                            //{
                            //    //lblBestSupplier.IsVisible = false;
                            //    //grdBest.IsVisible = false;
                            //}

                            //if (flag == 0)
                            //{
                            //    //if (_searchFilter != null || grdOffer.IsVisible == true)
                            //    await Navigation.PopAsync();
                            //    //else
                            //    //    control_visible(true);
                            //}
                        }
                        //else
                        {
                            //await DisplayAlert("Yarn Brokerage", res.message, "Cancel");
                        }
                    }
                }
                else
                {
                    //await DisplayAlert("Yarn Brokerage", "No Internet Connection", "Cancel");
                }

            }
            catch (Exception ex)
            {
                //await DisplayAlert("Yarn Brokerage", ex.Message.ToString(), "Cancel");
            }
        }

        async Task ExecuteStoreItemsCommand(object searchFilter)
        {
            if (IsBusy)
                return;            
            if (searchFilter == null)
                return;

            SearchFilter = new SearchCallLogFilter();

            SearchFilter = (SearchCallLogFilter)searchFilter;

            IsBusy = true;

            try
            {
                CallLogs.Clear();               
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
                                new KeyValuePair<string,string>("call_date_from", SearchFilter.call_date_from.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string,string>("call_date_to", SearchFilter.call_date_to.ToString("yyyy-MM-dd")),                                
                                new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                new KeyValuePair<string,string>("s_user_id",SearchFilter.user_id.ToString()),
                                new KeyValuePair<string,string>("call_date", SearchFilter.call_date.ToString()),
                                new KeyValuePair<string,string>("CurrentPageNumber","0"),
                                new KeyValuePair<string,string>("PageSize",PageSize.ToString())
                            });

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/call_log_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            CallLog_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<CallLog_list>(response);
                            foreach (CallLogModel item in res.CallLogs)
                            {
                                if (item.LedgerName == null)
                                    item.LedgerName = "Unknown";
                                if (Application.Current.Properties["user_type"].ToString() == "1")
                                    item.admin_user = true;
                                CallLogs.Add(item);
                            }
                            totalRows = res.totalRows;
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
        
        public int callLogCount()
        {
            return CallLogs.Count();
        }
    }
}