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
    public class CommissionInvoiceViewModel : BaseViewModel
    {
        public InfiniteScrollCollection<CommissionInvoice> CommissionInvoices { get; set; }
        public InfiniteScrollCollection<CommissionInvoice> GetCommissionOutstanding { get; set; }
        public InfiniteScrollCollection<CommissionInvoice> CommissionInvoicesList { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command SearchItemsCommand { get; set; }
        public Command SearchOutstandingCommand { get; set; }
        public Command GetCommissionInvoiceListCommand { get; set; }
        public DateTime date { get; set; }
        private const int PageSize = 20;
        public SearchConfirmationFilter SearchFilter { get; set; }

        // public Command StoreCommissionInvoiceCommand { get; set; }

        public CommissionInvoiceViewModel(SearchConfirmationFilter searchFilter = null, string _Title = "Commission Invoice")
        {
            Title = _Title;
            date = DateTime.Now.ToLocalTime();
            //CommissionInvoices = new ObservableCollection<CommissionInvoice>();
            CommissionInvoices = new InfiniteScrollCollection<CommissionInvoice>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = CommissionInvoices.Count / PageSize;

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
                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/commission_invoice_list", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        CommissionInvoice_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<CommissionInvoice_list>(response);
                        CommissionInvoice_list CommissionInvoice_List = new CommissionInvoice_list();
                        CommissionInvoice_List.totalRows = res.totalRows;
                        CommissionInvoice_List.CommissionInvoices = new List<CommissionInvoice>();
                        foreach (CommissionInvoice item in res.CommissionInvoices)
                        {
                            if (item.ledger_type == 2)
                                item.image = "buyer.png";
                            item.invoice_details = item.invoice_no + " ( " + item.invoice_date.ToString("dd-MM-yyyy") + " )";
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

                            CommissionInvoice_List.CommissionInvoices.Add(item);
                        }
                        IsBusy = false;
                        return CommissionInvoice_List.CommissionInvoices;
                    }
                    // return the items that need to be added

                },
                OnCanLoadMore = () =>
                {
                    return CommissionInvoices.Count < totalRows;
                }
            };

            GetCommissionOutstanding = new InfiniteScrollCollection<CommissionInvoice>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = GetCommissionOutstanding.Count / PageSize;

                    //var items = await _dataService.GetItemsAsync(page, PageSize);

                    using (var cl = new HttpClient())
                    {
                        HttpContent formcontent = null;
                        if (_SearchOutStandingFilter != null)
                        {
                            formcontent = new FormUrlEncodedContent(new[]
                          {
                                new KeyValuePair<string,string>("contact_type_id",_SearchOutStandingFilter.contact_type_id.ToString()),
                                new KeyValuePair<string,string>("contact_id",_SearchOutStandingFilter.contact_id.ToString()),
                                new KeyValuePair<string,string>("invoice_date_from",_SearchOutStandingFilter.invoice_date_from.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string,string>("invoice_date_to",_SearchOutStandingFilter.invoice_date_to.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string,string>("commission_date_flg",_SearchOutStandingFilter.commission_date_flg.ToString()),
                                new KeyValuePair<string,string>("search_string",_SearchOutStandingFilter.search_string.ToString()),
                                new KeyValuePair<string,string>("CurrentPageNumber",page.ToString()),
                                new KeyValuePair<string,string>("PageSize",PageSize.ToString()),
                            });

                        }

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/commission_invoice_report_summary", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        CommissionInvoice_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<CommissionInvoice_list>(response);
                        CommissionInvoice_list CommissionInvoice_List = new CommissionInvoice_list();
                        CommissionInvoice_List.totalRows = res.totalRows;
                        CommissionInvoice_List.CommissionInvoices = new List<CommissionInvoice>();
                        foreach (CommissionInvoice item in res.CommissionInvoices)
                        {
                            if (item.ledger_type == 1)
                                item.image = "customer.png";
                            else
                                item.image = "buyer.png";
                            double diff2 = (DateTime.Today.Date - item.invoice_date).TotalDays;
                            if (diff2 > 0)
                                item.commission_type_string = item.commission_type_string + (int)Math.Ceiling(diff2) + " Days Old";
                            //item.ledger_name = item.ledger_name + " - " + item.commission_type_string + "";
                            CommissionInvoice_List.CommissionInvoices.Add(item);
                        }
                        IsBusy = false;
                        return CommissionInvoice_List.CommissionInvoices;
                    }
                    // return the items that need to be added

                },
                OnCanLoadMore = () =>
                {
                    return GetCommissionOutstanding.Count < totalRows;
                }
            };


            CommissionInvoicesList = new InfiniteScrollCollection<CommissionInvoice>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = CommissionInvoicesList.Count / PageSize;

                    //var items = await _dataService.GetItemsAsync(page, PageSize);

                    using (var cl = new HttpClient())
                    {
                        HttpContent formcontent = null;
                        if (Commission_List.ledger_id != null)
                        {
                            formcontent = new FormUrlEncodedContent(new[]
                          {
                                new KeyValuePair<string,string>("contact_id",Commission_List.ledger_id.ToString()),
                                new KeyValuePair<string,string>("commission_date_flg",Commission_List.commission_date_flg.ToString()),
                                new KeyValuePair<string,string>("invoice_date",Commission_List.invoice_date.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string,string>("ledger_type",Commission_List.ledger_type.ToString()),
                                new KeyValuePair<string,string>("invoice_date_from",Commission_List.invoice_date_from.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string,string>("invoice_date_to",Commission_List.invoice_date_to.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string,string>("search_string",Commission_List.search_string.ToString()),
                                new KeyValuePair<string,string>("CurrentPageNumber",page.ToString()),
                                new KeyValuePair<string,string>("PageSize",PageSize.ToString()),
                            });

                        }

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/commission_invoice_report_detail", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        CommissionInvoice_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<CommissionInvoice_list>(response);
                        CommissionInvoice_list CommissionInvoice_List = new CommissionInvoice_list();
                        CommissionInvoice_List.totalRows = res.totalRows;
                        CommissionInvoice_List.CommissionInvoices = new List<CommissionInvoice>();
                        foreach (CommissionInvoice item in res.CommissionInvoices)
                        {
                            double diff2 = (DateTime.Today.Date - item.invoice_date).TotalDays;
                            if (diff2 > 0)
                                item.commission_type_string = item.commission_type_string + (int)Math.Ceiling(diff2) + " Days Old";
                            item.invoice_details = item.invoice_no + "(" + item.invoice_date.ToString("dd-MM-yyyy") + ")";
                            CommissionInvoice_List.CommissionInvoices.Add(item);
                        }
                        IsBusy = false;
                        return CommissionInvoice_List.CommissionInvoices;
                    }
                    // return the items that need to be added

                },
                OnCanLoadMore = () =>
                {
                    return CommissionInvoicesList.Count < totalRows;
                }
            };

            if (searchFilter == null)
                LoadItemsCommand = new Command(async (object searchstring) => await ExecuteLoadItemsCommand(searchstring)); //object confirmation_date confirmation_date
            else if (searchFilter != null)
                LoadItemsCommand = new Command(async (object searchFilters) => await ExecuteStoreItemsCommand(searchFilter));
            SearchItemsCommand = new Command(async (object searchFilters) => await ExecuteStoreItemsCommand(searchFilters));
            // StoreCommissionInvoiceCommand = new Command(async (object CommissionInvoice) => await ExecuteStoreCommissionInvoiceCommand(CommissionInvoice));
            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as Item;
            //    CommissionInvoice.Add(newItem);
            //    await DataStore.AddItemAsync(newItem);
            //});
            SearchOutstandingCommand = new Command(async (object SearchFilter) => await ExecuteLoadOutstandingCommand(SearchFilter));
            GetCommissionInvoiceListCommand = new Command(async (object commission_list) => await ExecuteCommissionListCommand(commission_list));
        }

        CommissionInvoice Commission_List;
        async Task ExecuteCommissionListCommand(object commission_list)
        {
            if (IsBusy)
                return;

            Commission_List = (CommissionInvoice)commission_list;

            IsBusy = true;
            try
            {
                CommissionInvoicesList.Clear();
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
                                new KeyValuePair<string,string>("contact_id",Commission_List.ledger_id.ToString()),
                                new KeyValuePair<string,string>("commission_date_flg",Commission_List.commission_date_flg.ToString()),
                                new KeyValuePair<string,string>("invoice_date",Commission_List.invoice_date.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string,string>("ledger_type",Commission_List.ledger_type.ToString()),
                                new KeyValuePair<string,string>("invoice_date_from",Commission_List.invoice_date_from.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string,string>("invoice_date_to",Commission_List.invoice_date_to.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string,string>("search_string",Commission_List.search_string.ToString()),
                                new KeyValuePair<string,string>("CurrentPageNumber","0"),
                                new KeyValuePair<string,string>("PageSize",PageSize.ToString()),
                            });

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/commission_invoice_report_detail", formcontent);
                            var response = await request.Content.ReadAsStringAsync();
                            CommissionInvoice_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<CommissionInvoice_list>(response);
                            foreach (CommissionInvoice item in res.CommissionInvoices)
                            {
                                double diff2 = (DateTime.Today.Date - item.invoice_date).TotalDays;
                                if (diff2 > 0)
                                    item.commission_type_string = item.commission_type_string + (int)Math.Ceiling(diff2) + " Days Old";
                                //item.invoice_details = item.invoice_date.ToString("dd-MM-yyyy") + " ( " + item.commission_type_string + " )";
                                item.invoice_details = item.invoice_no + " ( " + item.invoice_date.ToString("dd-MM-yyyy") + " )";
                                CommissionInvoicesList.Add(item);
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

        SearchOutStandingFilter _SearchOutStandingFilter;
        async Task ExecuteLoadOutstandingCommand(object searchFilter)
        {
            if (IsBusy)
                return;

            _SearchOutStandingFilter = searchFilter as SearchOutStandingFilter;

            IsBusy = true;
            try
            {
                GetCommissionOutstanding.Clear();
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
                                new KeyValuePair<string,string>("contact_type_id",_SearchOutStandingFilter.contact_type_id.ToString()),
                                new KeyValuePair<string,string>("contact_id",_SearchOutStandingFilter.contact_id.ToString()),
                                new KeyValuePair<string,string>("invoice_date_from",_SearchOutStandingFilter.invoice_date_from.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string,string>("invoice_date_to",_SearchOutStandingFilter.invoice_date_to.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string,string>("commission_date_flg",_SearchOutStandingFilter.commission_date_flg.ToString()),
                                new KeyValuePair<string,string>("search_string",_SearchOutStandingFilter.search_string.ToString()),          
                                new KeyValuePair<string,string>("CurrentPageNumber","0"),
                                new KeyValuePair<string,string>("PageSize",PageSize.ToString()),
                            });

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/commission_invoice_report_summary", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();
                            CommissionInvoice_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<CommissionInvoice_list>(response);
                            foreach (CommissionInvoice item in res.CommissionInvoices)
                            {
                                if (item.ledger_type == 1)
                                    item.image = "customer.png";
                                else
                                    item.image = "buyer.png";

                                double diff2 = (DateTime.Today.Date - item.invoice_date).TotalDays;
                                if (diff2 > 0)
                                    item.commission_type_string = item.commission_type_string + (int)Math.Ceiling(diff2) + " Days Old";
                                //item.ledger_name = item.ledger_name + " - " + item.commission_type_string + "";
                                GetCommissionOutstanding.Add(item);
                            }
                            totalRows = res.totalRows;
                            Balance_Value = "";
                            Balance_Value = "Total: " + res.total_commission.total_commission_amount.ToString();
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
                CommissionInvoices.Clear();
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

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/commission_invoice_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            CommissionInvoice_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<CommissionInvoice_list>(response);
                            foreach (CommissionInvoice item in res.CommissionInvoices)
                            {
                                if (item.ledger_type == 2)
                                    item.image = "buyer.png";
                                item.invoice_details = item.invoice_no + " ( " + item.invoice_date.ToString("dd-MM-yyyy") + " )";
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
                                CommissionInvoices.Add(item);
                            }
                            //CommissionInvoices.AddRange(res.CommissionInvoices);
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
                CommissionInvoices.Clear();
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

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/commission_invoice_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            CommissionInvoice_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<CommissionInvoice_list>(response);
                            foreach (CommissionInvoice item in res.CommissionInvoices)
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
                                CommissionInvoices.Add(item);
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

        public async Task<CommissionInvoice> StoreCommissionInvoiceCommand(CommissionInvoice CommissionInvoice)
        {
            //Indexes CommissionInvoice = (Indexes)_CommissionInvoice;
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
                            new KeyValuePair<string,string>("id",CommissionInvoice.id.ToString()),
                            new KeyValuePair<string,string>("invoice_no",CommissionInvoice.invoice_no.ToString()),
                            new KeyValuePair<string,string>("invoice_date",CommissionInvoice.invoice_date.ToString("yyyy/MM/dd")),
                            new KeyValuePair<string,string>("ledger_type",CommissionInvoice.ledger_type.ToString()),
                            new KeyValuePair<string,string>("ledger_id",CommissionInvoice.ledger_id.ToString()),
                            new KeyValuePair<string,string>("company_id",CommissionInvoice.company_id.ToString()),
                            new KeyValuePair<string,string>("commission_type",CommissionInvoice.commission_type.ToString()),
                            new KeyValuePair<string,string>("commission_value",CommissionInvoice.commission_value.ToString()),
                            new KeyValuePair<string,string>("commission_value",CommissionInvoice.commission_value.ToString()),
                            new KeyValuePair<string,string>("total_commission",CommissionInvoice.total_commission.ToString()),
                            new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/commission_invoice_store", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        CommissionInvoice_Store_Result res = Newtonsoft.Json.JsonConvert.DeserializeObject<CommissionInvoice_Store_Result>(response);
                        //res.CommissionInvoices.supplier_name = CommissionInvoice.supplier_name;
                        //res.CommissionInvoices.customer_name = CommissionInvoice.customer_name;
                        //res.CommissionInvoices.count_name = CommissionInvoice.count_name;
                        //res.CommissionInvoices.count_name = CommissionInvoice.count_name;
                        //res.CommissionInvoices.qty_unit = res.CommissionInvoices.qty.ToString() + " " + res.CommissionInvoices.unit;
                        //res.CommissionInvoices.price_per = res.CommissionInvoices.price.ToString() + " " + res.CommissionInvoices.per; 
                        //foreach (CommissionInvoice item in res.CommissionInvoices)
                        //{
                        CommissionInvoice = res.CommissionInvoices;
                        //}

                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return CommissionInvoice;
        }

        internal async Task<string> rejectCommissionInvoice(int CommissionInvoiceId)
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
                            new KeyValuePair<string,string>("id",CommissionInvoiceId.ToString()),
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

        internal async Task<int> DuplicateConfirmationNo(CommissionInvoice CommissionInvoice)
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
                            new KeyValuePair<string,string>("id",CommissionInvoice.id.ToString()),
                            //new KeyValuePair<string,string>("confirmation_no",CommissionInvoice.confirmation_no.ToString()),                           
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

        public async Task<int> getAmendCount(int CommissionInvoiceId)
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
                            new KeyValuePair<string,string>("id",CommissionInvoiceId.ToString()),
                            new KeyValuePair<string,string>("getAmendmentCount","1"),
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_detail_list", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        CommissionInvoice_Detail_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<CommissionInvoice_Detail_list>(response);
                        count = res.CommissionInvoiceDetails.Count();
                        //res.CommissionInvoices.price_per = res.CommissionInvoices.price.ToString() + " " + res.CommissionInvoices.per;
                        ////foreach (CommissionInvoice item in res.CommissionInvoices)
                        ////{
                        //CommissionInvoice = res.CommissionInvoices;
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

        public async void SendForApproval(CommissionInvoice CommissionInvoice)
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
                            new KeyValuePair<string,string>("id",CommissionInvoice.id.ToString()),
                            new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_send_for_approval", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        CommissionInvoice_Store_Result res = Newtonsoft.Json.JsonConvert.DeserializeObject<CommissionInvoice_Store_Result>(response);

                        //res.CommissionInvoices.price_per = res.CommissionInvoices.price.ToString() + " " + res.CommissionInvoices.per;
                        ////foreach (CommissionInvoice item in res.CommissionInvoices)
                        ////{
                        //CommissionInvoice = res.CommissionInvoices;
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