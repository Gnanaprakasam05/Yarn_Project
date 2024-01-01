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
namespace yarn_brokerage.ViewModels
{
    public class ConfirmationReoprtViewModel : BaseViewModel
    {
        public InfiniteScrollCollection<DispatchConfirm> DispatchConfirms { get; set; }
        public InfiniteScrollCollection<GroupBy> groupBies { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command SearchItemsCommand { get; set; }
        public DateTime date { get; set; }
        private const int PageSize = 20;
        public SearchConfirmationFilter SearchFilter { get; set; }
        // public Command StoreDispatchConfirmCommand { get; set; }

        public ConfirmationReoprtViewModel(SearchConfirmationFilter searchFilter = null, string _Title="Confirmation")
        {
            Title = _Title;
            date = DateTime.Now.ToLocalTime();
            //DispatchConfirms = new ObservableCollection<DispatchConfirm>();

            groupBies = new InfiniteScrollCollection<GroupBy>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = groupBies.Count / PageSize;

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

                                new KeyValuePair<string,string>("approved_status",SearchFilter.approved.ToString()),
                                new KeyValuePair<string,string>("bags_ready",SearchFilter.bags_ready.ToString()),
                                new KeyValuePair<string,string>("payment_ready",SearchFilter.payment_ready.ToString()),
                                new KeyValuePair<string,string>("payment_received",SearchFilter.payment_received.ToString()),
                                new KeyValuePair<string,string>("transporter_ready",SearchFilter.transporter_ready.ToString()),
                                new KeyValuePair<string,string>("dispatched",SearchFilter.dispatched.ToString()),
                                new KeyValuePair<string,string>("invoiced",SearchFilter.invoiced.ToString()),
                                new KeyValuePair<string,string>("delivered",SearchFilter.delivered.ToString()),
                                new KeyValuePair<string,string>("customer_confirmed",SearchFilter.customer_confirmed.ToString()),

                                new KeyValuePair<string,string>("not_approved_status",SearchFilter.not_approved.ToString()),
                                new KeyValuePair<string,string>("not_bags_ready",SearchFilter.not_bags_ready.ToString()),
                                new KeyValuePair<string,string>("not_payment_ready",SearchFilter.not_payment_ready.ToString()),
                                new KeyValuePair<string,string>("not_payment_received",SearchFilter.not_payment_received.ToString()),
                                new KeyValuePair<string,string>("not_transporter_ready",SearchFilter.not_transporter_ready.ToString()),
                                new KeyValuePair<string,string>("not_dispatched",SearchFilter.not_dispatched.ToString()),
                                new KeyValuePair<string,string>("not_invoiced",SearchFilter.not_invoiced.ToString()),
                                new KeyValuePair<string,string>("not_delivered",SearchFilter.not_delivered.ToString()),
                                new KeyValuePair<string,string>("not_customer_confirmed",SearchFilter.not_customer_confirmed.ToString()),

                                new KeyValuePair<string,string>("group_by_customer",SearchFilter.group_by_customer.ToString()),
                                new KeyValuePair<string,string>("group_by_supplier",SearchFilter.group_by_supplier.ToString()),
                                new KeyValuePair<string,string>("group_by_count",SearchFilter.group_by_count.ToString()),
                                new KeyValuePair<string,string>("group_by_segment",SearchFilter.group_by_segment.ToString()),
                                new KeyValuePair<string,string>("group_by_month",SearchFilter.group_by_month.ToString()),
                                new KeyValuePair<string,string>("group_by_year",SearchFilter.group_by_year.ToString()),

                                new KeyValuePair<string, string>("approved","1"),
                                new KeyValuePair<string,string>("search_string",SearchFilter.search_string.ToString()),
                                new KeyValuePair<string, string>("CurrentPageNumber", page.ToString()),
                                new KeyValuePair<string, string>("PageSize", PageSize.ToString())
                            });
                        }
                        else
                        {
                            formcontent = new FormUrlEncodedContent(new[]
                            {
                                new KeyValuePair<string,string>("search_string",_SearchString),
                                new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                new KeyValuePair<string,string>("CurrentPageNumber",page.ToString()),
                                new KeyValuePair<string,string>("PageSize",PageSize.ToString()),
                                new KeyValuePair<string, string>("approved","1"),
                            });
                        }
                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/dispatch_confirmation_list", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        GroupBy_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<GroupBy_list>(response);

                        GroupBy_list GroupBy_list = new GroupBy_list();
                        GroupBy_list.totalRows = res.totalRows;
                        GroupBy_list.GroupBy = new List<GroupBy>();
                        foreach (GroupBy item in res.GroupBy)
                        {
                            if (SearchFilter.group_by_segment == 1)
                            {
                                item.group_by_name = (item.group_by_name == "1") ? "Domestic" : "Export";
                            }
                            item.bag_weight = (Convert.ToInt32(item.bag_weight) > 0) ? item.bag_weight + " BAGS" : "";
                            item.fcl_weight = (Convert.ToInt32(item.fcl_weight) > 0) ? item.fcl_weight + " FCL" : "";
                            item.weight = ((item.bag_weight != "") ? item.bag_weight : "") + ((item.fcl_weight != "" && item.bag_weight != "") ? " / " : "") + ((item.fcl_weight != "") ? item.fcl_weight : "");
                            GroupBy_list.GroupBy.Add(item);
                        }
                        IsBusy = false;
                        return GroupBy_list.GroupBy;                        
                    }
                    // return the items that need to be added

                },
                OnCanLoadMore = () =>
                {
                    return groupBies.Count < totalRows;
                }
            };


            DispatchConfirms = new InfiniteScrollCollection<DispatchConfirm>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = DispatchConfirms.Count / PageSize;

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

                                new KeyValuePair<string,string>("approved_status",SearchFilter.approved.ToString()),
                                new KeyValuePair<string,string>("bags_ready",SearchFilter.bags_ready.ToString()),
                                new KeyValuePair<string,string>("payment_ready",SearchFilter.payment_ready.ToString()),
                                new KeyValuePair<string,string>("payment_received",SearchFilter.payment_received.ToString()),
                                new KeyValuePair<string,string>("transporter_ready",SearchFilter.transporter_ready.ToString()),
                                new KeyValuePair<string,string>("dispatched",SearchFilter.dispatched.ToString()),
                                new KeyValuePair<string,string>("invoiced",SearchFilter.invoiced.ToString()),
                                new KeyValuePair<string,string>("delivered",SearchFilter.delivered.ToString()),
                                new KeyValuePair<string,string>("customer_confirmed",SearchFilter.customer_confirmed.ToString()),

                                new KeyValuePair<string,string>("not_approved_status",SearchFilter.not_approved.ToString()),
                                new KeyValuePair<string,string>("not_bags_ready",SearchFilter.not_bags_ready.ToString()),
                                new KeyValuePair<string,string>("not_payment_ready",SearchFilter.not_payment_ready.ToString()),
                                new KeyValuePair<string,string>("not_payment_received",SearchFilter.not_payment_received.ToString()),
                                new KeyValuePair<string,string>("not_transporter_ready",SearchFilter.not_transporter_ready.ToString()),
                                new KeyValuePair<string,string>("not_dispatched",SearchFilter.not_dispatched.ToString()),
                                new KeyValuePair<string,string>("not_invoiced",SearchFilter.not_invoiced.ToString()),
                                new KeyValuePair<string,string>("not_delivered",SearchFilter.not_delivered.ToString()),
                                new KeyValuePair<string,string>("not_customer_confirmed",SearchFilter.not_customer_confirmed.ToString()),

                                new KeyValuePair<string,string>("group_by_customer",SearchFilter.group_by_customer.ToString()),
                                new KeyValuePair<string,string>("group_by_supplier",SearchFilter.group_by_supplier.ToString()),
                                new KeyValuePair<string,string>("group_by_count",SearchFilter.group_by_count.ToString()),
                                new KeyValuePair<string,string>("group_by_segment",SearchFilter.group_by_segment.ToString()),
                                new KeyValuePair<string,string>("group_by_month",SearchFilter.group_by_month.ToString()),
                                new KeyValuePair<string,string>("group_by_year",SearchFilter.group_by_year.ToString()),

                                new KeyValuePair<string, string>("approved","1"),
                                new KeyValuePair<string,string>("search_string",SearchFilter.search_string.ToString()),
                                new KeyValuePair<string, string>("CurrentPageNumber", page.ToString()),
                                new KeyValuePair<string, string>("PageSize", PageSize.ToString())
                            });
                        }
                        else
                        {
                            formcontent = new FormUrlEncodedContent(new[]
                            {
                                new KeyValuePair<string,string>("search_string",_SearchString),
                                new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                new KeyValuePair<string,string>("CurrentPageNumber",page.ToString()),
                                new KeyValuePair<string,string>("PageSize",PageSize.ToString()),
                                new KeyValuePair<string, string>("approved","1"),
                            });
                        }
                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/dispatch_confirmation_list", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        DispatchConfirm_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<DispatchConfirm_list>(response);
                        DispatchConfirm_list DispatchConfirm_List = new DispatchConfirm_list();
                        DispatchConfirm_List.totalRows = res.totalRows;
                        DispatchConfirm_List.DispatchConfirms = new List<DispatchConfirm>();
                        foreach (DispatchConfirm item in res.DispatchConfirms)
                        {
                            if (Application.Current.Properties["user_type"].ToString() == "1")
                                item.admin_user = true;
                            if (item.price != Convert.ToInt32(item.price))
                                item.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.price)));
                            else
                                item.price = Convert.ToInt32(item.price);
                            item.price_per = item.price.ToString() + " " + item.per;
                            double diff2 = (DateTime.Today.Date.ToLocalTime() - item.transaction_date_time).TotalDays;
                            if (diff2 > 0)
                                item.transaction_detail = item.transaction_detail + " - " + (int)Math.Ceiling(diff2) + " Days Old";
                            DispatchConfirm_List.DispatchConfirms.Add(item);
                        }
                        IsBusy = false;
                        return DispatchConfirm_List.DispatchConfirms;
                    }
                    // return the items that need to be added

                },
                OnCanLoadMore = () =>
                {
                    return DispatchConfirms.Count < totalRows;
                }
            };
            
            if (searchFilter == null)
                LoadItemsCommand = new Command(async (object searchstring) => await ExecuteLoadItemsCommand(searchstring)); //object confirmation_date confirmation_date
            else if (searchFilter != null)
                LoadItemsCommand = new Command(async (object searchFilters) => await ExecuteStoreItemsCommand(searchFilter));
            SearchItemsCommand = new Command(async (object searchFilters) => await ExecuteStoreItemsCommand(searchFilters));
            // StoreDispatchConfirmCommand = new Command(async (object DispatchConfirm) => await ExecuteStoreDispatchConfirmCommand(DispatchConfirm));
            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as Item;
            //    DispatchConfirm.Add(newItem);
            //    await DataStore.AddItemAsync(newItem);
            //});
        }

        string _SearchString;
        private int totalRows = 0;

        async Task ExecuteLoadItemsCommand(object searchstring) //object confirmation_date
        {
            if (IsBusy)
                return;
            //DateTime _confirmation_date;
            _SearchString = "";
            if (searchstring != null)
                _SearchString = (string)searchstring;
            IsBusy = true;

            try
            {
                DispatchConfirms.Clear();
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
                                new KeyValuePair<string,string>("search_string",_SearchString),
                                new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                new KeyValuePair<string,string>("CurrentPageNumber","0"),
                                new KeyValuePair<string,string>("PageSize",PageSize.ToString()),
                                new KeyValuePair<string, string>("approved","1"),
                            });

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/dispatch_confirmation_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            DispatchConfirm_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<DispatchConfirm_list>(response);
                            foreach (DispatchConfirm item in res.DispatchConfirms)
                            {
                                if (Application.Current.Properties["user_type"].ToString() == "1")
                                    item.admin_user = true;
                                if (item.price != Convert.ToInt32(item.price))
                                    item.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.price)));
                                else
                                    item.price = Convert.ToInt32(item.price);
                                item.price_per = item.price.ToString() + " " + item.per;
                                //if (item.status == 1)
                                //    item.status_image = "approved.png";
                                //if (item.status == 5)
                                //    item.status_image = "rejected.png";
                                double diff2 = (DateTime.Today.Date - item.transaction_date_time).TotalDays;
                                if (diff2 > 0)
                                    item.transaction_detail = item.transaction_detail + " - " + (int)Math.Ceiling(diff2) + " Days Old";
                                DispatchConfirms.Add(item);
                            }
                            //DispatchConfirms.AddRange(res.DispatchConfirms);
                            totalRows = res.totalRows;

                            No_Of_Count_With_Bag_Weight = "";
                            Balance_Value = "";
                            if (res.totalRows > 0)
                            {
                                No_Of_Count_With_Bag_Weight = "" + res.totalRows + " Nos. ( " + res.dispatch_confirm_value + "  )";
                                //Balance_Value = res.balance_value;
                            }
                            else
                            {
                                No_Of_Count_With_Bag_Weight = "No Records.";
                                Balance_Value = "0 BAGS / 0 FCL";
                            }
                            

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
                DispatchConfirms.Clear();
                groupBies.Clear();
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

                                new KeyValuePair<string,string>("approved_status",SearchFilter.approved.ToString()),
                                new KeyValuePair<string,string>("bags_ready",SearchFilter.bags_ready.ToString()),
                                new KeyValuePair<string,string>("payment_ready",SearchFilter.payment_ready.ToString()),
                                new KeyValuePair<string,string>("payment_received",SearchFilter.payment_received.ToString()),
                                new KeyValuePair<string,string>("transporter_ready",SearchFilter.transporter_ready.ToString()),
                                new KeyValuePair<string,string>("dispatched",SearchFilter.dispatched.ToString()),
                                new KeyValuePair<string,string>("invoiced",SearchFilter.invoiced.ToString()),
                                new KeyValuePair<string,string>("delivered",SearchFilter.delivered.ToString()),
                                new KeyValuePair<string,string>("customer_confirmed",SearchFilter.customer_confirmed.ToString()),

                                new KeyValuePair<string,string>("not_approved_status",SearchFilter.not_approved.ToString()),
                                new KeyValuePair<string,string>("not_bags_ready",SearchFilter.not_bags_ready.ToString()),
                                new KeyValuePair<string,string>("not_payment_ready",SearchFilter.not_payment_ready.ToString()),
                                new KeyValuePair<string,string>("not_payment_received",SearchFilter.not_payment_received.ToString()),
                                new KeyValuePair<string,string>("not_transporter_ready",SearchFilter.not_transporter_ready.ToString()),
                                new KeyValuePair<string,string>("not_dispatched",SearchFilter.not_dispatched.ToString()),
                                new KeyValuePair<string,string>("not_invoiced",SearchFilter.not_invoiced.ToString()),
                                new KeyValuePair<string,string>("not_delivered",SearchFilter.not_delivered.ToString()),
                                new KeyValuePair<string,string>("not_customer_confirmed",SearchFilter.not_customer_confirmed.ToString()),
                                                              
                                new KeyValuePair<string,string>("group_by_customer",SearchFilter.group_by_customer.ToString()),
                                new KeyValuePair<string,string>("group_by_supplier",SearchFilter.group_by_supplier.ToString()),
                                new KeyValuePair<string,string>("group_by_count",SearchFilter.group_by_count.ToString()),
                                new KeyValuePair<string,string>("group_by_segment",SearchFilter.group_by_segment.ToString()),
                                new KeyValuePair<string,string>("group_by_month",SearchFilter.group_by_month.ToString()),
                                new KeyValuePair<string,string>("group_by_year",SearchFilter.group_by_year.ToString()),

                                new KeyValuePair<string, string>("approved","1"),
                                new KeyValuePair<string,string>("CurrentPageNumber","0"),
                                new KeyValuePair<string,string>("PageSize",PageSize.ToString())
                            });

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/dispatch_confirmation_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();
                            

                            if (SearchFilter.group_by_customer == 1 || SearchFilter.group_by_supplier == 1 || SearchFilter.group_by_count == 1 || SearchFilter.group_by_segment == 1 || SearchFilter.group_by_month == 1|| SearchFilter.group_by_year == 1)
                            {
                                GroupBy_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<GroupBy_list>(response);
                                foreach (GroupBy item in res.GroupBy)
                                {
                                    if(SearchFilter.group_by_segment == 1)
                                    {
                                        item.group_by_name = (item.group_by_name == "1") ? "Domestic" : "Export";
                                    }
                                    item.bag_weight = (Convert.ToInt32(item.bag_weight) > 0) ? item.bag_weight + " BAGS" : "";
                                    item.fcl_weight = (Convert.ToInt32(item.fcl_weight) > 0) ? item.fcl_weight + " FCL" : "";
                                    item.weight = ((item.bag_weight != "") ? item.bag_weight : "" ) + ((item.fcl_weight != "" && item.bag_weight != "") ? " / " : "") + ((item.fcl_weight != "") ? item.fcl_weight : "");
                                    groupBies.Add(item);
                                }
                                totalRows = res.totalRows;

                                if (res.totalRows > 0)
                                {
                                    No_Of_Count_With_Bag_Weight = "" + res.totalRows + " Nos. ( " + res.dispatch_confirm_value + "  )";
                                    //Balance_Value = res.balance_value;
                                }
                                else
                                {
                                    No_Of_Count_With_Bag_Weight = "No Records.";
                                    Balance_Value = "0 BAGS / 0 FCL";
                                }
                            }
                            else
                            {
                                DispatchConfirm_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<DispatchConfirm_list>(response);
                                foreach (DispatchConfirm item in res.DispatchConfirms)
                                {
                                    if (Application.Current.Properties["user_type"].ToString() == "1")
                                        item.admin_user = true;
                                    if (item.price != Convert.ToInt32(item.price))
                                        item.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.price)));
                                    else
                                        item.price = Convert.ToInt32(item.price);
                                    item.price_per = item.price.ToString() + " " + item.per;
                                    double diff2 = (DateTime.Today.Date - item.transaction_date_time).TotalDays;
                                    if (diff2 > 0)
                                        item.transaction_detail = item.transaction_detail + " - " + (int)Math.Ceiling(diff2) + " Days Old";
                                    DispatchConfirms.Add(item);
                                }
                                totalRows = res.totalRows;

                                if (res.totalRows > 0)
                                {
                                    No_Of_Count_With_Bag_Weight = "" + res.totalRows + " Nos. ( " + res.dispatch_confirm_value + "  )";
                                    //Balance_Value = res.balance_value;
                                }
                                else
                                {
                                    No_Of_Count_With_Bag_Weight = "No Records.";
                                    Balance_Value = "0 BAGS / 0 FCL";
                                }
                            }                            
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

        //public async Task<DispatchConfirm> StoreDispatchConfirmCommand(DispatchConfirm DispatchConfirm)
        //{
        //    //Indexes DispatchConfirm = (Indexes)_DispatchConfirm;
        //    try
        //    {
        //        var current = Connectivity.NetworkAccess;

        //        if (current == NetworkAccess.Internet)
        //        {
        //            using (var cl = new HttpClient())
        //            {
        //                HttpContent formcontent = null;

        //                formcontent = new FormUrlEncodedContent(new[]
        //                {
        //                    new KeyValuePair<string,string>("id",DispatchConfirm.id.ToString()),
        //                    new KeyValuePair<string,string>("transaction_date_time",DispatchConfirm.transaction_date_time.ToString("yyyy/MM/dd")),
        //                    new KeyValuePair<string,string>("segment",DispatchConfirm.segment.ToString()),
        //                    new KeyValuePair<string,string>("customer_id",DispatchConfirm.customer_id.ToString()),
        //                    new KeyValuePair<string,string>("supplier_id",DispatchConfirm.supplier_id.ToString()),
        //                    new KeyValuePair<string,string>("count_id",DispatchConfirm.count_id.ToString()),
        //                    new KeyValuePair<string,string>("bag_weight",DispatchConfirm.bag_weight.ToString()),
        //                    new KeyValuePair<string,string>("qty",DispatchConfirm.qty.ToString()),
        //                    new KeyValuePair<string,string>("unit",DispatchConfirm.unit.ToString()),
        //                    new KeyValuePair<string,string>("price",DispatchConfirm.price.ToString()),
        //                    new KeyValuePair<string,string>("per",DispatchConfirm.per.ToString()),
        //                    new KeyValuePair<string,string>("confirmation_no",DispatchConfirm.confirmation_no.ToString()),
        //                    //new KeyValuePair<string,string>("dispatch_from_date",DispatchConfirm.dispatch_from_date.ToString("yyyy/MM/dd")),
        //                    //new KeyValuePair<string,string>("dispatch_to_date",DispatchConfirm.dispatch_to_date.ToString("yyyy/MM/dd")),
        //                    //new KeyValuePair<string,string>("payment_date",DispatchConfirm.payment_date.ToString("yyyy/MM/dd")),
        //                    new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
        //                    //new KeyValuePair<string,string>("enquiry_ids",DispatchConfirm.enquiry_ids.ToString()),                            
        //                });
                        
        //                var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_store", formcontent);

        //                //request.EnsureSuccessStatusCode(); 

        //                var response = await request.Content.ReadAsStringAsync();

        //                DispatchConfirm_Store_Result res = Newtonsoft.Json.JsonConvert.DeserializeObject<DispatchConfirm_Store_Result>(response);
        //                res.DispatchConfirms.supplier_name = DispatchConfirm.supplier_name;
        //                res.DispatchConfirms.customer_name = DispatchConfirm.customer_name;
        //                res.DispatchConfirms.count_name = DispatchConfirm.count_name;
        //                res.DispatchConfirms.count_name = DispatchConfirm.count_name;
        //                res.DispatchConfirms.qty_unit = res.DispatchConfirms.qty.ToString() + " " + res.DispatchConfirms.unit;
        //                res.DispatchConfirms.price_per = res.DispatchConfirms.price.ToString() + " " + res.DispatchConfirms.per; 
        //                //foreach (DispatchConfirm item in res.DispatchConfirms)
        //                //{
        //                DispatchConfirm = res.DispatchConfirms;                            
        //                //}

        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }
        //     return DispatchConfirm;
        //}
    }
}