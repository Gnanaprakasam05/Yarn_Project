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
using Xamarin.Forms.Extended;
using System.Linq;

namespace yarn_brokerage.ViewModels
{
    public class CommissionReceiptViewModel : BaseViewModel
    {
        public InfiniteScrollCollection<CommissionReceipt> CommissionReceipts { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command SearchItemsCommand { get; set; }
        public DateTime date { get; set; }
        private const int PageSize = 20;
        public SearchConfirmationFilter SearchFilter { get; set; }
        
        // public Command StoreCommissionReceiptCommand { get; set; }

        public CommissionReceiptViewModel(SearchConfirmationFilter searchFilter = null, string _Title="Commission Receipt")
        {
            Title = _Title;
            date = DateTime.Now.ToLocalTime();
            //CommissionReceipts = new ObservableCollection<CommissionReceipt>();
            CommissionReceipts = new InfiniteScrollCollection<CommissionReceipt>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = CommissionReceipts.Count / PageSize;

                    //var items = await _dataService.GetItemsAsync(page, PageSize);

                    using (var cl = new HttpClient())
                    {
                        HttpContent formcontent = null;
                        if (SearchFilter != null)
                        {
                            formcontent = new FormUrlEncodedContent(new[]
                            {
                                new KeyValuePair<string, string>("transaction_type", SearchFilter.transaction_type.ToString()),
                                new KeyValuePair<string, string>("count_id", SearchFilter.count_id.ToString()),
                                new KeyValuePair<string, string>("segment", SearchFilter.segment.ToString()),
                                new KeyValuePair<string, string>("confirmation_no", SearchFilter.confirmation_no.ToString()),
                                new KeyValuePair<string, string>("ledgers_id", SearchFilter.ledger_id.ToString()),
                                new KeyValuePair<string, string>("exact_ledger_id", SearchFilter.exact_ledger_id.ToString()),
                                new KeyValuePair<string, string>("supplier_id", SearchFilter.supplier_id.ToString()),
                                new KeyValuePair<string, string>("customer_id", SearchFilter.customer_id.ToString()),
                                new KeyValuePair<string, string>("user_type", Application.Current.Properties["user_type"].ToString()),
                                new KeyValuePair<string, string>("user_id", Application.Current.Properties["user_id"].ToString()),
                                new KeyValuePair<string, string>("s_user_id", SearchFilter.user_id.ToString()),
                                new KeyValuePair<string, string>("confirmation_date_from", SearchFilter.confirmation_date_from.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string, string>("confirmation_date_to", SearchFilter.confirmation_date_to.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string, string>("dispatch_date_from", SearchFilter.dispatch_date_from.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string, string>("dispatch_date_to", SearchFilter.dispatch_date_to.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string, string>("payment_date_from", SearchFilter.payment_date_from.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string, string>("payment_date_to", SearchFilter.payment_date_to.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string, string>("confirmation_date", SearchFilter.confirmation_date.ToString()),
                                new KeyValuePair<string, string>("dispatch_date", SearchFilter.dispatch_date.ToString()),
                                new KeyValuePair<string, string>("payment_date", SearchFilter.payment_date.ToString()),
                                new KeyValuePair<string,string>("search_string",SearchFilter.search_string.ToString()),
                                new KeyValuePair<string, string>("CurrentPageNumber", page.ToString()),
                                new KeyValuePair<string, string>("PageSize", PageSize.ToString())
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
                                //new KeyValuePair<string,string>("transaction_date",_SearchString.current_date.ToString("yyyy-MM-dd"))
                            });
                        }
                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/commission_receipt_list", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        CommissionReceipt_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<CommissionReceipt_list>(response);
                        CommissionReceipt_list CommissionReceipt_List = new CommissionReceipt_list();
                        CommissionReceipt_List.totalRows = res.totalRows;
                        CommissionReceipt_List.CommissionReceipts = new List<CommissionReceipt>();
                        foreach (CommissionReceipt item in res.CommissionReceipts)
                        {
                            if (item.ledger_type == 2)
                                item.image = "buyer.png";
                            item.receipt_details = item.receipt_no + " ( " + item.receipt_date.ToString("dd-MM-yyyy") + " )";
                            //if (Application.Current.Properties["user_type"].ToString() == "1")
                            //    item.admin_user = true;
                            //if (item.price != Convert.ToInt32(item.price))
                            //    item.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.price)));
                            //else
                            //    item.price = Convert.ToInt32(item.price);
                            //item.price_per = item.price.ToString() + " " + item.per;
                            //double diff2 = (DateTime.Today.Date.ToLocalTime() - item.transaction_date_time).TotalDays;
                            //if (diff2 > 0)
                            //    item.transaction_detail = item.transaction_detail + " - " + (int)Math.Ceiling(diff2) + " Days Old";

                            CommissionReceipt_List.CommissionReceipts.Add(item);
                        }
                        IsBusy = false;
                        return CommissionReceipt_List.CommissionReceipts;
                    }
                    // return the items that need to be added

                },
                OnCanLoadMore = () =>
                {
                    return CommissionReceipts.Count < totalRows;
                }
            };
            
            if (searchFilter == null)
                LoadItemsCommand = new Command(async (object searchstring) => await ExecuteLoadItemsCommand(searchstring)); //object confirmation_date confirmation_date
            else if (searchFilter != null)
                LoadItemsCommand = new Command(async (object searchFilters) => await ExecuteStoreItemsCommand(searchFilter));
            SearchItemsCommand = new Command(async (object searchFilters) => await ExecuteStoreItemsCommand(searchFilters));
            // StoreCommissionReceiptCommand = new Command(async (object CommissionReceipt) => await ExecuteStoreCommissionReceiptCommand(CommissionReceipt));
            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as Item;
            //    CommissionReceipt.Add(newItem);
            //    await DataStore.AddItemAsync(newItem);
            //});
        }

        search_string _SearchString;
        private int totalRows = 0;

        async Task ExecuteLoadItemsCommand(object searchstring) //object confirmation_date
        {
            if (IsBusy)
                return;
            //DateTime _confirmation_date;
            _SearchString = searchstring as search_string;
            //if (searchstring != null)
            //    _SearchString = (string)searchstring;
            IsBusy = true;

            try
            {
                CommissionReceipts.Clear();
                //if (confirmation_date != null)
                //    _confirmation_date = (DateTime)confirmation_date;
                //else
                //{
                //    _confirmation_date = date;
                //}
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
                                //new KeyValuePair<string,string>("transaction_date",_SearchString.current_date.ToString("yyyy-MM-dd"))
                            });

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/commission_receipt_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            CommissionReceipt_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<CommissionReceipt_list>(response);
                            foreach (CommissionReceipt item in res.CommissionReceipts)
                            {
                                if (item.ledger_type == 2)
                                    item.image = "buyer.png";
                                item.receipt_details = item.receipt_no + " ( " + item.receipt_date.ToString("dd-MM-yyyy") + " )";
                                //if (Application.Current.Properties["user_type"].ToString() == "1")
                                //    item.admin_user = true;
                                //if (item.price != Convert.ToInt32(item.price))
                                //    item.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.price)));
                                //else
                                //    item.price = Convert.ToInt32(item.price);
                                //item.price_per = item.price.ToString() + " " + item.per;
                                //if (item.status == 1)
                                //    item.status_image = "approved.png";
                                //if (item.status == 5)
                                //    item.status_image = "rejected.png";
                                //double diff2 = (DateTime.Today.Date - item.transaction_date_time).TotalDays;
                                //if (diff2 > 0)
                                //    item.transaction_detail = item.transaction_detail + " - " + (int)Math.Ceiling(diff2) + " Days Old";
                                CommissionReceipts.Add(item);
                            }
                            //CommissionReceipts.AddRange(res.CommissionReceipts);
                            totalRows = res.totalRows;
                            No_Of_Count_With_Bag_Weight = "";
                            if (res.totalRows > 0)
                                No_Of_Count_With_Bag_Weight = "" + res.totalRows + " Nos. ( " + res.confirm_value + "  )";
                            else
                                No_Of_Count_With_Bag_Weight = "No Records.";
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

        async Task ExecuteStoreItemsCommand(object searchFilter)
        {
            if (IsBusy)
                return;
            if (searchFilter == null)
                return;

            SearchFilter = new SearchConfirmationFilter();

            SearchFilter = (SearchConfirmationFilter)searchFilter;

            IsBusy = true;

            try
            {
                CommissionReceipts.Clear();
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
                                new KeyValuePair<string,string>("transaction_type",SearchFilter.transaction_type.ToString()),
                                new KeyValuePair<string,string>("count_id",SearchFilter.count_id.ToString()),
                                new KeyValuePair<string,string>("segment",SearchFilter.segment.ToString()),
                                new KeyValuePair<string, string>("confirmation_no", SearchFilter.confirmation_no.ToString()),
                                new KeyValuePair<string,string>("ledgers_id",SearchFilter.ledger_id.ToString()),
                                new KeyValuePair<string,string>("exact_ledger_id",SearchFilter.exact_ledger_id.ToString()),
                                new KeyValuePair<string,string>("supplier_id",SearchFilter.supplier_id.ToString()),
                                new KeyValuePair<string,string>("customer_id",SearchFilter.customer_id.ToString()),
                                new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                new KeyValuePair<string,string>("s_user_id",SearchFilter.user_id.ToString()),
                                new KeyValuePair<string,string>("confirmation_date_from", SearchFilter.confirmation_date_from.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string,string>("confirmation_date_to", SearchFilter.confirmation_date_to.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string,string>("dispatch_date_from", SearchFilter.dispatch_date_from.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string,string>("dispatch_date_to", SearchFilter.dispatch_date_to.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string,string>("payment_date_from", SearchFilter.payment_date_from.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string,string>("payment_date_to", SearchFilter.payment_date_to.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string,string>("confirmation_date",SearchFilter.confirmation_date.ToString()),
                                new KeyValuePair<string,string>("dispatch_date",SearchFilter.dispatch_date.ToString()),
                                new KeyValuePair<string,string>("payment_date",SearchFilter.payment_date.ToString()),
                                new KeyValuePair<string,string>("search_string",SearchFilter.search_string.ToString()),
                                new KeyValuePair<string,string>("CurrentPageNumber","0"),
                                new KeyValuePair<string,string>("PageSize",PageSize.ToString())
                            });

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/commission_receipt_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            CommissionReceipt_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<CommissionReceipt_list>(response);
                            foreach (CommissionReceipt item in res.CommissionReceipts)
                            {
                                //if (Application.Current.Properties["user_type"].ToString() == "1")
                                //    item.admin_user = true;
                                //if (item.price != Convert.ToInt32(item.price))
                                //    item.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.price)));
                                //else
                                //    item.price = Convert.ToInt32(item.price);
                                //item.price_per = item.price.ToString() + " " + item.per;
                                //double diff2 = (DateTime.Today.Date - item.transaction_date_time).TotalDays;
                                //if (diff2 > 0)
                                //    item.transaction_detail = item.transaction_detail + " - " + (int)Math.Ceiling(diff2) + " Days Old";
                                CommissionReceipts.Add(item);
                            }
                            totalRows = res.totalRows;
                            
                            if (res.totalRows > 0)
                                No_Of_Count_With_Bag_Weight = "" + res.totalRows + " Nos. ( " + res.confirm_value + "  )";
                            else
                                No_Of_Count_With_Bag_Weight = "No Records.";
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

        public async Task<CommissionReceipt> StoreCommissionReceiptCommand(CommissionReceipt CommissionReceipt)
        {
            //Indexes CommissionReceipt = (Indexes)_CommissionReceipt;
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
                            new KeyValuePair<string,string>("id",CommissionReceipt.id.ToString()),
                            new KeyValuePair<string,string>("receipt_no",CommissionReceipt.receipt_no.ToString()),
                            new KeyValuePair<string,string>("receipt_date",CommissionReceipt.receipt_date.ToString("yyyy/MM/dd")),
                            new KeyValuePair<string,string>("ledger_type",CommissionReceipt.ledger_type.ToString()),
                            new KeyValuePair<string,string>("ledger_id",CommissionReceipt.ledger_id.ToString()),
                            new KeyValuePair<string,string>("company_id",CommissionReceipt.company_id.ToString()),
                            new KeyValuePair<string,string>("total_receipt_amount",CommissionReceipt.total_receipt_amount.ToString()),
                            new KeyValuePair<string,string>("total_adjusted_amount",CommissionReceipt.total_adjusted_amount.ToString()),                            
                            new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),                                                
                        });
                        
                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/commission_receipt_store", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        CommissionReceipt_Store_Result res = Newtonsoft.Json.JsonConvert.DeserializeObject<CommissionReceipt_Store_Result>(response);
                        //res.CommissionReceipts.supplier_name = CommissionReceipt.supplier_name;
                        //res.CommissionReceipts.customer_name = CommissionReceipt.customer_name;
                        //res.CommissionReceipts.count_name = CommissionReceipt.count_name;
                        //res.CommissionReceipts.count_name = CommissionReceipt.count_name;
                        //res.CommissionReceipts.qty_unit = res.CommissionReceipts.qty.ToString() + " " + res.CommissionReceipts.unit;
                        //res.CommissionReceipts.price_per = res.CommissionReceipts.price.ToString() + " " + res.CommissionReceipts.per; 
                        //foreach (CommissionReceipt item in res.CommissionReceipts)
                        //{
                        CommissionReceipt = res.CommissionReceipts;                            
                        //}

                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
             return CommissionReceipt;
        }

        internal async Task<string> rejectCommissionReceipt(int CommissionReceiptId)
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
                            new KeyValuePair<string,string>("id",CommissionReceiptId.ToString()),
                            new KeyValuePair<string,string>("transaction_date_time",DateTime.Now.ToLocalTime().ToString("yyyy/MM/ddTHH:mm")),                            
                            new KeyValuePair<string,string>("status","5"),
                            new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                            new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_approval", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        Approval_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<Approval_list>(response);
                        
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return "failure";
            }
            return "sucess";
        }

        internal async Task<int>DuplicateConfirmationNo(CommissionReceipt CommissionReceipt)
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
                            new KeyValuePair<string,string>("id",CommissionReceipt.id.ToString()),
                            //new KeyValuePair<string,string>("confirmation_no",CommissionReceipt.confirmation_no.ToString()),                           
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_duplicate_no", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        return Convert.ToInt32(response);

                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return 0;
            }
            return 1;
        }

        public async Task<int> getAmendCount(int CommissionReceiptId)
        {
            int count = 0;
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
                            new KeyValuePair<string,string>("id",CommissionReceiptId.ToString()),
                            new KeyValuePair<string,string>("getAmendmentCount","1"),
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_detail_list", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        CommissionReceipt_Detail_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<CommissionReceipt_Detail_list>(response);
                        count = res.CommissionReceiptDetails.Count();
                        //res.CommissionReceipts.price_per = res.CommissionReceipts.price.ToString() + " " + res.CommissionReceipts.per;
                        ////foreach (CommissionReceipt item in res.CommissionReceipts)
                        ////{
                        //CommissionReceipt = res.CommissionReceipts;
                        //}

                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return count;
        }

        public async void SendForApproval(CommissionReceipt CommissionReceipt)
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
                            new KeyValuePair<string,string>("id",CommissionReceipt.id.ToString()),                            
                            new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_send_for_approval", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        CommissionReceipt_Store_Result res = Newtonsoft.Json.JsonConvert.DeserializeObject<CommissionReceipt_Store_Result>(response);

                        //res.CommissionReceipts.price_per = res.CommissionReceipts.price.ToString() + " " + res.CommissionReceipts.per;
                        ////foreach (CommissionReceipt item in res.CommissionReceipts)
                        ////{
                        //CommissionReceipt = res.CommissionReceipts;
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