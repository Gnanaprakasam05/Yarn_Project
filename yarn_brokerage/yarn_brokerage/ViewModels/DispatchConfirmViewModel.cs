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
using Microcharts;
using Xamarin.Forms.Internals;

namespace yarn_brokerage.ViewModels
{
    public class DispatchConfirmViewModel : BaseViewModel
    {
        private InfiniteScrollCollection<DispatchConfirm> _infiniteItems;

        public InfiniteScrollCollection<DispatchConfirm> Dispatch
        {
            get { return _infiniteItems; }

            set
            {
                _infiniteItems = value;
                OnPropertyChanged(nameof(Dispatch));
            }
        }
        public bool Edit_Check { get; set; }
        public bool value_check { get; set; } = true;
        public InfiniteScrollCollection<DispatchConfirm> DispatchConfirms { get; set; }
        public ObservableCollection<DispatchConfirm> DispatchConfirm { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command SearchItemsCommand { get; set; }
        public DateTime date { get; set; }
        private const int PageSize = 10;
        private int page { get; set; } = 0;
        private int AddCount { get; set; } = 0;
        private int Check_TeamGroup { get; set; }

        private string user_team_group_id { get; set; }
        public SearchDispatchConfirmationFilter SearchFilter { get; set; }
        int _TodaysPlan;
        //public SearchFilter SearchFilter { get; set; }
        // public Command StoreDispatchConfirmCommand { get; set; }
        bool hideGenerateInvoice = false;
        public bool HideGenerateInvoice
        {
            get { return hideGenerateInvoice; }
            set { SetProperty(ref hideGenerateInvoice, value); }
        }

        public DispatchConfirmViewModel(SearchDispatchConfirmationFilter searchFilter = null, int TodaysPlan = 1)
        {
            if (TodaysPlan == 0)
                Title = "Pending Confirmation";
            if (TodaysPlan == 1)
                Title = "Current Plan";
            if (TodaysPlan == 2)
                Title = "Bags Not Ready";
            if (TodaysPlan == 3)
                Title = "Payment Not Ready";
            if (TodaysPlan == 4)
                Title = "Transporter Not Ready";
            if (TodaysPlan == 5)
                Title = "Processing";
            if (TodaysPlan == 6)
                Title = "Pending Invoice";
            if (TodaysPlan == 7)
                Title = "Transit";
            if (TodaysPlan == 8)
                Title = "Program Approval";
            if (TodaysPlan == 9)
                Title = "Customer Confirmation";
            if (TodaysPlan == 10)
                Title = "Payment Not Received";
            if (TodaysPlan == 11)
                Title = "Pending Payment";
            if (TodaysPlan == 12)
                Title = "Pending Forms";
            if (TodaysPlan == 13)
                Title = "Pending Commission";
            if (TodaysPlan == 14)
                Title = "Dispatched";

            date = DateTime.Now.ToLocalTime();
            _TodaysPlan = TodaysPlan;

            if (TodaysPlan == 0 || TodaysPlan == 8)
            {
                DispatchConfirm = new ObservableCollection<DispatchConfirm>();
            }
            else
            {
                DispatchConfirms = new InfiniteScrollCollection<DispatchConfirm>
                {
                    OnLoadMore = async () =>
                    {
                        IsBusy = true;

                        var page = DispatchConfirms.Count / PageSize;

                        using (var cl = new HttpClient())
                        {
                            HttpContent formcontent = null;
                            if (SearchFilter != null)
                            {
                                formcontent = new FormUrlEncodedContent(new[]
                                {
                                    new KeyValuePair<string,string>("transaction_type",SearchFilter.transaction_type.ToString()),
                                    new KeyValuePair<string,string>("count_id",SearchFilter.count_id.ToString()),
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
                                    new KeyValuePair<string,string>("dispatch_date_from", SearchFilter.approved_date_from.ToString("yyyy-MM-dd")),
                                    new KeyValuePair<string,string>("dispatch_date_to", SearchFilter.approved_date_to.ToString("yyyy-MM-dd")),
                                    new KeyValuePair<string,string>("dispatched_date_from", SearchFilter.dispatched_date_from.ToString("yyyy-MM-dd")),
                                    new KeyValuePair<string,string>("dispatched_date_to", SearchFilter.dispatched_date_to.ToString("yyyy-MM-dd")),
                                    new KeyValuePair<string,string>("confirmation_date",SearchFilter.confirmation_date.ToString()),
                                    new KeyValuePair<string,string>("dispatch_date",SearchFilter.approved_date.ToString()),
                                    new KeyValuePair<string,string>("dispatched_date",SearchFilter.dispatched_date.ToString()),
                                    new KeyValuePair<string,string>("bags_ready",SearchFilter.bags_ready.ToString()),
                                    new KeyValuePair<string,string>("payment_ready",SearchFilter.payment_ready.ToString()),
                                    new KeyValuePair<string,string>("payment_received",SearchFilter.payment_received.ToString()),
                                    new KeyValuePair<string,string>("transporter_ready",SearchFilter.transporter_ready.ToString()),
                                    new KeyValuePair<string,string>("search_string",SearchFilter.search_string.ToString()),
                                    new KeyValuePair<string,string>("include_received_form",SearchFilter.include_received_form.ToString()),
                                    new KeyValuePair<string,string>("datewise",datewise),
                                    new KeyValuePair<string,string>("todays_plan",_TodaysPlan.ToString()),
                                    new KeyValuePair<string,string>("outersearch",SearchFilter.outersearch.ToString()),
                                    new KeyValuePair<string,string>("CurrentPageNumber",page.ToString()),
                                    new KeyValuePair<string,string>("PageSize",PageSize.ToString())
                                });
                            }
                            else
                            {
                                formcontent = new FormUrlEncodedContent(new[]
                                {
                                    new KeyValuePair<string,string>("search_string",_SearchString.Search_string),
                                    new KeyValuePair<string,string>("bags_ready",_SearchString.bags_ready.ToString()),
                                    new KeyValuePair<string,string>("payment_ready",_SearchString.payment_ready.ToString()),
                                    new KeyValuePair<string,string>("payment_received",_SearchString.payment_received.ToString()),
                                    new KeyValuePair<string,string>("transporter_ready",_SearchString.transporter_ready.ToString()),
                                    new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                    new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                    new KeyValuePair<string,string>("dispatch_confirmation_date",_dispatch_confirmation_date.ToString("yyyy-MM-dd")),
                                    new KeyValuePair<string,string>("datewise",datewise),
                                    new KeyValuePair<string,string>("todays_plan",_TodaysPlan.ToString()),
                                    new KeyValuePair<string,string>("overdue_flag",_SearchString.overdue_flag.ToString()),
                                    new KeyValuePair<string,string>("CurrentPageNumber",page.ToString()),
                                    new KeyValuePair<string,string>("PageSize",PageSize.ToString()),
                                    new KeyValuePair<string, string>("team_group_id", user_team_group_id)
                                });
                            }

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/dispatch_confirmation_list", formcontent);

                            var response = await request.Content.ReadAsStringAsync();

                            DispatchConfirm_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<DispatchConfirm_list>(response);
                            DispatchConfirm_list dispatchConfirm_List = new DispatchConfirm_list();
                            dispatchConfirm_List.totalRows = res.totalRows;
                            dispatchConfirm_List.DispatchConfirms = new List<DispatchConfirm>();
                            //foreach (DispatchConfirm item in res.DispatchConfirms)
                            //{
                            //    if (Application.Current.Properties["user_type"].ToString() == "1")
                            //        item.admin_user = true;
                            //    if (item.price != Convert.ToInt32(item.price))
                            //        item.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.price)));
                            //    else
                            //        item.price = Convert.ToInt32(item.price);
                            //    item.price_per = item.price.ToString() + " " + item.per;
                            //    double diff2;
                            //    if (_TodaysPlan == 6 || _TodaysPlan == 7)
                            //        diff2 = (_dispatch_confirmation_date.Date - item.dispatched_date).TotalDays; //DateTime.Today.Date
                            //    else if (_TodaysPlan == 13)
                            //        diff2 = (_dispatch_confirmation_date.Date - item.invoice_date).TotalDays; //DateTime.Today.Date
                            //    else
                            //        diff2 = (_dispatch_confirmation_date.Date - item.dispatch_date).TotalDays; //DateTime.Today.Date
                            //    if (_TodaysPlan == 13)
                            //        item.transaction_detail = item.invoice_date.ToString("dd-MM-yyyy") + " ( " + item.confirmation_no + " )";


                            //    if (_TodaysPlan == 8)
                            //    {

                            //        diff2 = (DateTime.Today.Date - item.transaction_date_time).TotalDays;
                            //    }
                            //    if (_TodaysPlan == 0)
                            //    {

                            //        diff2 = (DateTime.Today.Date - item.transaction_date_time).TotalDays;
                            //    }

                            //    if (diff2 > 0)
                            //        item.transaction_detail = item.transaction_date_time + "-" + (int)Math.Ceiling(diff2) + " Days Old";



                            //    dispatchConfirm_List.DispatchConfirms.Add(item);
                            //}

                            foreach (DispatchConfirm item in res.DispatchConfirms)
                            {
                                if (Application.Current.Properties["user_type"].ToString() == "1")
                                    item.admin_user = true;
                                if (item.price != Convert.ToInt32(item.price))
                                    item.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.price)));
                                else
                                    item.price = Convert.ToInt32(item.price);
                                item.price_per = item.price.ToString() + " " + item.per;
                                double diff2;

                                diff2 = (DateTime.Today.Date - item.transaction_date_time).TotalDays;

                                if (_TodaysPlan == 6 || _TodaysPlan == 7)
                                    diff2 = (_dispatch_confirmation_date.Date - item.dispatched_date).TotalDays; //DateTime.Today.Date
                                else if (_TodaysPlan == 13)
                                    diff2 = (_dispatch_confirmation_date.Date - item.invoice_date).TotalDays; //DateTime.Today.Date
                                else
                                    diff2 = (_dispatch_confirmation_date.Date - item.dispatch_date).TotalDays; //DateTime.Today.Date
                                if (_TodaysPlan == 13)
                                    item.transaction_detail = item.invoice_date.ToString("dd-MM-yyyy") + " ( " + item.confirmation_no + " )";
                                if (_TodaysPlan == 8)
                                {

                                    diff2 = (DateTime.Today.Date - item.transaction_date_time).TotalDays;


                                    if (diff2 > 0)
                                    {
                                        item.DelayDays = (int)Math.Ceiling(diff2) + " Days Old";
                                    }
                                    else
                                    {
                                        item.DelayDays = "";
                                    }

                                    item.invoice_visible_on = false;
                                    item.invoice_visible_off = true;
                                }

                                if (diff2 > 0)
                                    item.transaction_detail = item.transaction_detail + " - " + (int)Math.Ceiling(diff2) + " Days Old";

                                if (_TodaysPlan == 0)
                                {

                                    diff2 = (DateTime.Today.Date - item.transaction_date_time).TotalDays;

                                    if (diff2 > 0)
                                    {
                                        item.DelayDays = (int)Math.Ceiling(diff2) + " Days Old";
                                    }
                                    else
                                    {
                                        item.DelayDays = "";
                                    }


                                    item.invoice_visible_on = true;
                                    item.invoice_visible_off = false;

                                }

                                if (_TodaysPlan == 0 || _TodaysPlan == 8)
                                {
                                    DispatchConfirm.Add(item);
                                }
                                else
                                {
                                    DispatchConfirms.Add(item);
                                }




                            }

                            IsBusy = false;
                            return dispatchConfirm_List.DispatchConfirms;
                        }

                    },
                    OnCanLoadMore = () =>
                    {
                        return DispatchConfirms.Count < totalRows;
                    }
                };

            }



            if (searchFilter == null)
                LoadItemsCommand = new Command(async (object searchstring) => await ExecuteLoadItemsCommand(searchstring));
            else if (searchFilter != null)
                LoadItemsCommand = new Command(async (object searchFilters) => await ExecuteStoreItemsCommand(searchFilter));
            SearchItemsCommand = new Command(async (object searchFilters) => await ExecuteStoreItemsCommand(searchFilters));

        }

        dispatch_search_string _SearchString;
        private int totalRows = 0;
        DateTime _dispatch_confirmation_date;
        string datewise = "";
        async Task ExecuteLoadItemsCommand(object searchstring)
        {
            if (IsBusy)
                return;

            if (Application.Current.Properties["mobile_dashboard_show_team_group_data"].ToString() == "1" && Application.Current.Properties["team_group_id"] != null)
            {
                user_team_group_id = Application.Current.Properties["team_group_id"].ToString();
            }
            else if (Application.Current.Properties["mobile_dashboard_show_team_group_data"].ToString() == "1" && Application.Current.Properties["team_group_id"] == null)
            {
                user_team_group_id = "";

                if (Application.Current.Properties["user_type"].ToString() == "2")
                {
                    Check_TeamGroup = 1;
                }

            }
            else if (Application.Current.Properties["mobile_dashboard_show_team_group_data"].ToString() == "0")
            {
                user_team_group_id = "";

            }



            IsBusy = true;

            _SearchString = searchstring as dispatch_search_string;

            try
            {

                if (_TodaysPlan == 0 || _TodaysPlan == 8)
                {
                    DispatchConfirm.Clear();
                }
                else
                {
                    DispatchConfirms.Clear();
                }

                if (_SearchString.current_date != null)
                    _dispatch_confirmation_date = (DateTime)_SearchString.current_date;
                else
                    _dispatch_confirmation_date = date;

                if (_dispatch_confirmation_date.ToString("yyyy-MM-dd") == DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd"))
                    datewise = "today";
                else if (Convert.ToDateTime(_dispatch_confirmation_date.ToString("yyyy-MM-dd")) < Convert.ToDateTime(DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd")))
                    datewise = "overdue";
                else if (Convert.ToDateTime(_dispatch_confirmation_date.ToString("yyyy-MM-dd")) > Convert.ToDateTime(DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd")))
                    datewise = "tomorrow";

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
                                    new KeyValuePair<string,string>("bags_ready",_SearchString.bags_ready.ToString()),
                                    new KeyValuePair<string,string>("payment_ready",_SearchString.payment_ready.ToString()),
                                    new KeyValuePair<string,string>("payment_received",_SearchString.payment_received.ToString()),
                                    new KeyValuePair<string,string>("transporter_ready",_SearchString.transporter_ready.ToString()),
                                    new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                    new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                    new KeyValuePair<string,string>("dispatch_confirmation_date",_dispatch_confirmation_date.ToString("yyyy-MM-dd")),
                                    new KeyValuePair<string,string>("datewise",datewise),
                                    new KeyValuePair<string,string>("todays_plan",_TodaysPlan.ToString()),
                                    new KeyValuePair<string,string>("overdue_flag",_SearchString.overdue_flag.ToString()),
                                    new KeyValuePair<string,string>("CurrentPageNumber","0"),
                                    new KeyValuePair<string,string>("PageSize",PageSize.ToString()),
                                    new KeyValuePair<string, string>("team_group_id",user_team_group_id)
                                });

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/dispatch_confirmation_list", formcontent);

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
                                double diff2;

                                diff2 = (DateTime.Today.Date - item.transaction_date_time).TotalDays;

                                if (_TodaysPlan == 6 || _TodaysPlan == 7)
                                    diff2 = (_dispatch_confirmation_date.Date - item.dispatched_date).TotalDays; //DateTime.Today.Date
                                else if (_TodaysPlan == 13)
                                    diff2 = (_dispatch_confirmation_date.Date - item.invoice_date).TotalDays; //DateTime.Today.Date
                                else
                                    diff2 = (_dispatch_confirmation_date.Date - item.dispatch_date).TotalDays; //DateTime.Today.Date
                                if (_TodaysPlan == 13)
                                    item.transaction_detail = item.invoice_date.ToString("dd-MM-yyyy") + " ( " + item.confirmation_no + " )";
                                if (_TodaysPlan == 8)
                                {

                                    diff2 = (DateTime.Today.Date - item.transaction_date_time).TotalDays;


                                    if (diff2 > 0)
                                    {
                                        item.DelayDays = (int)Math.Ceiling(diff2) + " Days Old";
                                    }
                                    else
                                    {
                                        item.DelayDays = "";
                                    }

                                    item.invoice_visible_on = false;
                                    item.invoice_visible_off = true;
                                }

                                if (diff2 > 0)
                                    item.transaction_detail = item.transaction_detail + " - " + (int)Math.Ceiling(diff2) + " Days Old";

                                if (_TodaysPlan == 0)
                                {

                                    diff2 = (DateTime.Today.Date - item.transaction_date_time).TotalDays;

                                    if (diff2 > 0)
                                    {
                                        item.DelayDays = (int)Math.Ceiling(diff2) + " Days Old";
                                    }
                                    else
                                    {
                                        item.DelayDays = "";
                                    }


                                    item.invoice_visible_on = true;
                                    item.invoice_visible_off = false;

                                }

                                if (_TodaysPlan == 0 || _TodaysPlan == 8)
                                {
                                    DispatchConfirm.Add(item);
                                }
                                else
                                {
                                    DispatchConfirms.Add(item);
                                }




                            }
                            totalRows = res.totalRows;
                            if (res.totalRows > 0)
                            {
                                No_Of_Count_With_Bag_Weight = "" + res.totalRows + " Nos. ( " + res.dispatch_confirm_value + "  )";
                                if (_TodaysPlan == 13) HideGenerateInvoice = true;
                            }
                            else
                            {
                                No_Of_Count_With_Bag_Weight = "No Records.";
                                if (_TodaysPlan == 13) HideGenerateInvoice = false;
                            }




                            if (Check_TeamGroup == 1)
                            {
                                if (_TodaysPlan == 0 || _TodaysPlan == 8)
                                {
                                    DispatchConfirm.Clear();
                                }
                                else
                                {
                                    DispatchConfirms.Clear();
                                }

                                No_Of_Count_With_Bag_Weight = "";
                                Check_TeamGroup = 0;
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

        internal async Task<bool> GenerateInvoice(dispatch_search_string search_String)
        {
            if (IsBusy)
                return false;
            IsBusy = true;

            _SearchString = search_String as dispatch_search_string;

            try
            {
                if (_SearchString.current_date != null)
                    _dispatch_confirmation_date = (DateTime)_SearchString.current_date;
                else
                    _dispatch_confirmation_date = date;

                if (_dispatch_confirmation_date.ToString("yyyy-MM-dd") == DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd"))
                    datewise = "today";
                else if (Convert.ToDateTime(_dispatch_confirmation_date.ToString("yyyy-MM-dd")) < Convert.ToDateTime(DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd")))
                    datewise = "overdue";
                else if (Convert.ToDateTime(_dispatch_confirmation_date.ToString("yyyy-MM-dd")) > Convert.ToDateTime(DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd")))
                    datewise = "tomorrow";

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
                                    new KeyValuePair<string,string>("bags_ready",_SearchString.bags_ready.ToString()),
                                    new KeyValuePair<string,string>("payment_ready",_SearchString.payment_ready.ToString()),
                                    new KeyValuePair<string,string>("payment_received",_SearchString.payment_received.ToString()),
                                    new KeyValuePair<string,string>("transporter_ready",_SearchString.transporter_ready.ToString()),
                                    new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                    new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                    new KeyValuePair<string,string>("dispatch_confirmation_date",_dispatch_confirmation_date.ToString("yyyy-MM-dd")),
                                    new KeyValuePair<string,string>("invoice_date",DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd")),
                                    new KeyValuePair<string,string>("datewise",datewise),
                                    new KeyValuePair<string,string>("todays_plan",_TodaysPlan.ToString()),
                                    new KeyValuePair<string,string>("overdue_flag",_SearchString.overdue_flag.ToString()),
                                    new KeyValuePair<string,string>("CurrentPageNumber","0"),
                                    new KeyValuePair<string,string>("PageSize",PageSize.ToString()),
                                    new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                    new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                });

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/commission_invoice_auto_generate", formcontent);

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
                                double diff2;
                                if (_TodaysPlan == 6 || _TodaysPlan == 7)
                                    diff2 = (_dispatch_confirmation_date.Date - item.dispatched_date).TotalDays; //DateTime.Today.Date
                                else if (_TodaysPlan == 13)
                                    diff2 = (_dispatch_confirmation_date.Date - item.invoice_date).TotalDays; //DateTime.Today.Date
                                else
                                    diff2 = (_dispatch_confirmation_date.Date - item.dispatch_date).TotalDays; //DateTime.Today.Date
                                if (_TodaysPlan == 13)
                                    item.transaction_detail = item.invoice_date.ToString("dd-MM-yyyy") + " ( " + item.confirmation_no + " )";
                                if (diff2 > 0)
                                    item.transaction_detail = item.transaction_detail + " - " + (int)Math.Ceiling(diff2) + " Days Old";
                                DispatchConfirms.Add(item);
                            }
                            totalRows = res.totalRows;
                            if (res.totalRows > 0)
                            {
                                No_Of_Count_With_Bag_Weight = "" + res.totalRows + " Nos. ( " + res.dispatch_confirm_value + "  )";
                                if (_TodaysPlan == 13) HideGenerateInvoice = true;
                            }
                            else
                            {
                                No_Of_Count_With_Bag_Weight = "No Records.";
                                if (_TodaysPlan == 13) HideGenerateInvoice = false;
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
            return true;
        }

        async Task ExecuteStoreItemsCommand(object searchFilter)
        {

            if (IsBusy)
                return;
            if (searchFilter == null)
                return;

            SearchFilter = new SearchDispatchConfirmationFilter();

            SearchFilter = (SearchDispatchConfirmationFilter)searchFilter;

            IsBusy = true;

            try
            {
                if (_TodaysPlan == 0 || _TodaysPlan == 8)
                {
                    DispatchConfirm.Clear();
                }
                else
                {
                    DispatchConfirms.Clear();
                }


                try
                {
                    var current = Connectivity.NetworkAccess;

                    if (current == NetworkAccess.Internet)
                    {
                        using (var cl = new HttpClient())
                        {
                            HttpContent formcontent = null;

                            if (SearchFilter.TeamGroupId > 0)
                            {
                                formcontent = new FormUrlEncodedContent(new[]
                                {
                                    new KeyValuePair<string,string>("transaction_type",SearchFilter.transaction_type.ToString()),
                                    new KeyValuePair<string,string>("count_id",SearchFilter.count_id.ToString()),
                                    new KeyValuePair<string, string>("confirmation_no", SearchFilter.confirmation_no.ToString()),
                                    new KeyValuePair<string,string>("ledgers_id",SearchFilter.ledger_id.ToString()),
                                    new KeyValuePair<string,string>("exact_ledger_id",SearchFilter.exact_ledger_id.ToString()),
                                    new KeyValuePair<string,string>("supplier_id",SearchFilter.supplier_id.ToString()),
                                    new KeyValuePair<string,string>("customer_id",SearchFilter.customer_id.ToString()),
                                    new KeyValuePair<string,string>("team_group_id",SearchFilter.TeamGroupId.ToString()),
                                    new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                    new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                    new KeyValuePair<string,string>("s_user_id",SearchFilter.user_id.ToString()),
                                    new KeyValuePair<string,string>("confirmation_date_from", SearchFilter.confirmation_date_from.ToString("yyyy-MM-dd")),
                                    new KeyValuePair<string,string>("confirmation_date_to", SearchFilter.confirmation_date_to.ToString("yyyy-MM-dd")),
                                    new KeyValuePair<string,string>("dispatch_date_from", SearchFilter.approved_date_from.ToString("yyyy-MM-dd")),
                                    new KeyValuePair<string,string>("dispatch_date_to", SearchFilter.approved_date_to.ToString("yyyy-MM-dd")),
                                    new KeyValuePair<string,string>("dispatched_date_from", SearchFilter.dispatched_date_from.ToString("yyyy-MM-dd")),
                                    new KeyValuePair<string,string>("dispatched_date_to", SearchFilter.dispatched_date_to.ToString("yyyy-MM-dd")),
                                    new KeyValuePair<string,string>("confirmation_date",SearchFilter.confirmation_date.ToString()),
                                    new KeyValuePair<string,string>("dispatch_date",SearchFilter.approved_date.ToString()),
                                    new KeyValuePair<string,string>("dispatched_date",SearchFilter.dispatched_date.ToString()),
                                    new KeyValuePair<string,string>("bags_ready",SearchFilter.bags_ready.ToString()),
                                    new KeyValuePair<string,string>("payment_ready",SearchFilter.payment_ready.ToString()),
                                    new KeyValuePair<string,string>("payment_received",SearchFilter.payment_received.ToString()),
                                    new KeyValuePair<string,string>("transporter_ready",SearchFilter.transporter_ready.ToString()),
                                    new KeyValuePair<string,string>("search_string",SearchFilter.search_string.ToString()),
                                    new KeyValuePair<string,string>("include_received_form",SearchFilter.include_received_form.ToString()),
                                    new KeyValuePair<string,string>("datewise",datewise),
                                    new KeyValuePair<string,string>("todays_plan",_TodaysPlan.ToString()),
                                    new KeyValuePair<string,string>("outersearch",SearchFilter.outersearch.ToString()),
                                    new KeyValuePair<string,string>("CurrentPageNumber","0"),
                                    new KeyValuePair<string,string>("PageSize",PageSize.ToString())
                                });
                            }
                            else if (SearchFilter.TeamId > 0)
                            {
                                formcontent = new FormUrlEncodedContent(new[]
                                {
                                        new KeyValuePair<string,string>("transaction_type",SearchFilter.transaction_type.ToString()),
                                        new KeyValuePair<string,string>("count_id",SearchFilter.count_id.ToString()),
                                        new KeyValuePair<string, string>("confirmation_no", SearchFilter.confirmation_no.ToString()),
                                        new KeyValuePair<string,string>("ledgers_id",SearchFilter.ledger_id.ToString()),
                                        new KeyValuePair<string,string>("exact_ledger_id",SearchFilter.exact_ledger_id.ToString()),
                                        new KeyValuePair<string,string>("supplier_id",SearchFilter.supplier_id.ToString()),
                                        new KeyValuePair<string,string>("customer_id",SearchFilter.customer_id.ToString()),

                                        new KeyValuePair<string,string>("team[id]",SearchFilter.TeamId.ToString()),

                                        new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                        new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                        new KeyValuePair<string,string>("s_user_id",SearchFilter.user_id.ToString()),
                                        new KeyValuePair<string,string>("confirmation_date_from", SearchFilter.confirmation_date_from.ToString("yyyy-MM-dd")),
                                        new KeyValuePair<string,string>("confirmation_date_to", SearchFilter.confirmation_date_to.ToString("yyyy-MM-dd")),
                                        new KeyValuePair<string,string>("dispatch_date_from", SearchFilter.approved_date_from.ToString("yyyy-MM-dd")),
                                        new KeyValuePair<string,string>("dispatch_date_to", SearchFilter.approved_date_to.ToString("yyyy-MM-dd")),
                                        new KeyValuePair<string,string>("dispatched_date_from", SearchFilter.dispatched_date_from.ToString("yyyy-MM-dd")),
                                        new KeyValuePair<string,string>("dispatched_date_to", SearchFilter.dispatched_date_to.ToString("yyyy-MM-dd")),
                                        new KeyValuePair<string,string>("confirmation_date",SearchFilter.confirmation_date.ToString()),
                                        new KeyValuePair<string,string>("dispatch_date",SearchFilter.approved_date.ToString()),
                                        new KeyValuePair<string,string>("dispatched_date",SearchFilter.dispatched_date.ToString()),
                                        new KeyValuePair<string,string>("bags_ready",SearchFilter.bags_ready.ToString()),
                                        new KeyValuePair<string,string>("payment_ready",SearchFilter.payment_ready.ToString()),
                                        new KeyValuePair<string,string>("payment_received",SearchFilter.payment_received.ToString()),
                                        new KeyValuePair<string,string>("transporter_ready",SearchFilter.transporter_ready.ToString()),
                                        new KeyValuePair<string,string>("search_string",SearchFilter.search_string.ToString()),
                                        new KeyValuePair<string,string>("include_received_form",SearchFilter.include_received_form.ToString()),
                                        new KeyValuePair<string,string>("datewise",datewise),
                                        new KeyValuePair<string,string>("todays_plan",_TodaysPlan.ToString()),
                                        new KeyValuePair<string,string>("outersearch",SearchFilter.outersearch.ToString()),
                                        new KeyValuePair<string,string>("CurrentPageNumber","0"),
                                        new KeyValuePair<string,string>("PageSize",PageSize.ToString())
                                });
                            }
                            else
                            {
                                formcontent = new FormUrlEncodedContent(new[]
                                {
                                        new KeyValuePair<string,string>("transaction_type",SearchFilter.transaction_type.ToString()),
                                        new KeyValuePair<string,string>("count_id",SearchFilter.count_id.ToString()),
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
                                        new KeyValuePair<string,string>("dispatch_date_from", SearchFilter.approved_date_from.ToString("yyyy-MM-dd")),
                                        new KeyValuePair<string,string>("dispatch_date_to", SearchFilter.approved_date_to.ToString("yyyy-MM-dd")),
                                        new KeyValuePair<string,string>("dispatched_date_from", SearchFilter.dispatched_date_from.ToString("yyyy-MM-dd")),
                                        new KeyValuePair<string,string>("dispatched_date_to", SearchFilter.dispatched_date_to.ToString("yyyy-MM-dd")),
                                        new KeyValuePair<string,string>("confirmation_date",SearchFilter.confirmation_date.ToString()),
                                        new KeyValuePair<string,string>("dispatch_date",SearchFilter.approved_date.ToString()),
                                        new KeyValuePair<string,string>("dispatched_date",SearchFilter.dispatched_date.ToString()),
                                        new KeyValuePair<string,string>("bags_ready",SearchFilter.bags_ready.ToString()),
                                        new KeyValuePair<string,string>("payment_ready",SearchFilter.payment_ready.ToString()),
                                        new KeyValuePair<string,string>("payment_received",SearchFilter.payment_received.ToString()),
                                        new KeyValuePair<string,string>("transporter_ready",SearchFilter.transporter_ready.ToString()),
                                        new KeyValuePair<string,string>("search_string",SearchFilter.search_string.ToString()),
                                        new KeyValuePair<string,string>("include_received_form",SearchFilter.include_received_form.ToString()),
                                        new KeyValuePair<string,string>("datewise",datewise),
                                        new KeyValuePair<string,string>("todays_plan",_TodaysPlan.ToString()),
                                        new KeyValuePair<string,string>("outersearch",SearchFilter.outersearch.ToString()),
                                        new KeyValuePair<string,string>("CurrentPageNumber","0"),
                                        new KeyValuePair<string,string>("PageSize",PageSize.ToString())
                                });
                            }



                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/dispatch_confirmation_list", formcontent);

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
                                double diff2;
                                if (_TodaysPlan == 6 || _TodaysPlan == 7)
                                    diff2 = (DateTime.Today.Date - item.dispatched_date).TotalDays; //DateTime.Today.Date
                                else if (_TodaysPlan == 13)
                                    diff2 = (_dispatch_confirmation_date.Date - item.invoice_date).TotalDays; //DateTime.Today.Date
                                else
                                    diff2 = (_dispatch_confirmation_date.Date - item.dispatch_date).TotalDays; //DateTime.Today.Date
                                if (_TodaysPlan == 13)
                                    item.transaction_detail = item.invoice_date.ToString("dd-MM-yyyy") + " ( " + item.confirmation_no + " )";
                                if (diff2 > 0)
                                    item.transaction_detail = item.transaction_detail + " - " + (int)Math.Ceiling(diff2) + " Days Old";

                                if (_TodaysPlan == 0 || _TodaysPlan == 8)
                                {
                                    DispatchConfirm.Add(item);
                                }
                                else
                                {
                                    DispatchConfirms.Add(item);
                                }
                            }
                            totalRows = res.totalRows;
                            if (res.totalRows > 0)
                            {
                                No_Of_Count_With_Bag_Weight = "" + res.totalRows + " Nos. ( " + res.dispatch_confirm_value + "  )";
                                if (_TodaysPlan == 13) HideGenerateInvoice = true;
                            }
                            else
                            {
                                No_Of_Count_With_Bag_Weight = "No Records.";
                                if (_TodaysPlan == 13) HideGenerateInvoice = false;
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
        public async Task<DispatchConfirm> GetDispatchConfirmAsync(int id)
        {
            DispatchConfirm item = new DispatchConfirm();

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
                                    new KeyValuePair<string,string>("id",id.ToString()),
                                    new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                    new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                    //new KeyValuePair<string,string>("confirmation_date",_confirmation_date.ToString("yyyy-MM-dd"))
                                });


                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/dispatch_confirmation_list", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        DispatchConfirm_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<DispatchConfirm_list>(response);
                        item = res.DispatchConfirms[0];
                        if (Application.Current.Properties["user_type"].ToString() == "1")
                            item.admin_user = true;
                        if (item.price != Convert.ToInt32(item.price))
                            item.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.price)));
                        else
                            item.price = Convert.ToInt32(item.price);
                        item.price_per = item.price.ToString() + " " + item.per;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return item;
        }
        public async Task<DispatchConfirm> StoreDispatchConfirmCommand(DispatchConfirm DispatchConfirm)
        {
            //Indexes DispatchConfirm = (Indexes)_DispatchConfirm;
            DispatchConfirm item = new DispatchConfirm();
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
                            new KeyValuePair<string,string>("id",DispatchConfirm.Id.ToString()),
                            new KeyValuePair<string,string>("transaction_date_time",DateTime.Now.ToLocalTime().ToString("yyyy/MM/ddTHH:mm")),
                            new KeyValuePair<string,string>("dispatch_confirm_date",DispatchConfirm.dispatch_confirm_date.ToString("yyyy/MM/dd")),
                            new KeyValuePair<string,string>("status",DispatchConfirm.status.ToString()),
                            new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                            new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/dispatch_confirmation_store", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        DispatchConfirm_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<DispatchConfirm_list>(response);
                        item = res.DispatchConfirms[0];
                        if (Application.Current.Properties["user_type"].ToString() == "1")
                            item.admin_user = true;
                        if (item.price != Convert.ToInt32(item.price))
                            item.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.price)));
                        else
                            item.price = Convert.ToInt32(item.price);
                        item.price_per = item.price.ToString() + " " + item.per;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return item;
        }
        public string BAGQTY { get; set; }
        public string FCLQTY { get; set; }
        public string BOXQTY { get; set; }
        public string PALQTY { get; set; }
        public string BALEQTY { get; set; }

        string QTY;
        public void DeleteCommand(int ID)
        {
            var index_value = DispatchConfirm.IndexOf(DispatchConfirm.Where(x => x.Id == ID).FirstOrDefault());
            DispatchConfirm.RemoveAt(index_value);
            //Edit_Check = true;
            for (int i = 0; i < DispatchConfirm.Count(); i++)
            {
                if (DispatchConfirm[i].unit == "BAGS")
                {
                    for (int bags = 0; bags < DispatchConfirm.Count(); bags++)
                    {
                        BAGQTY = DispatchConfirm.Where(x => x.unit == "BAGS").Sum(x => x.qty).ToString() + " BAGS";
                    }
                }
                if (DispatchConfirm[i].unit == "FCL")
                {

                    for (int fcl = 0; fcl < DispatchConfirm.Count(); fcl++)
                    {
                        FCLQTY = "/" + DispatchConfirm.Where(x => x.unit == "FCL").Sum(x => x.qty).ToString() + " FCL";
                    }
                }
                if (DispatchConfirm[i].unit == "BOX")
                {

                    for (int fcl = 0; fcl < DispatchConfirm.Count(); fcl++)
                    {
                        BOXQTY = "/" + DispatchConfirm.Where(x => x.unit == "BOX").Sum(x => x.qty).ToString() + " BOX";
                    }
                }
                if (DispatchConfirm[i].unit == "PALLET")
                {

                    for (int fcl = 0; fcl < DispatchConfirm.Count(); fcl++)
                    {
                        PALQTY = "/" + DispatchConfirm.Where(x => x.unit == "PALLET").Sum(x => x.qty).ToString() + " PALLET";
                    }
                }
                if (DispatchConfirm[i].unit == "BALE")
                {

                    for (int fcl = 0; fcl < DispatchConfirm.Count(); fcl++)
                    {
                        BALEQTY = "/" + DispatchConfirm.Where(x => x.unit == "BALE").Sum(x => x.qty).ToString() + " BALE";
                    }
                }

            }
            QTY = BAGQTY + FCLQTY + BOXQTY + PALQTY + BALEQTY;


            if (DispatchConfirm.Count() > 0)
                No_Of_Count_With_Bag_Weight = DispatchConfirm.Count() + " No's. (" + QTY + ")";
            else
                No_Of_Count_With_Bag_Weight = "No Records.";




        }
        public async Task ExecuteLoadPendingConfirmationTodayCommand()
        {



            IsBusy = true;


            if (Application.Current.Properties["mobile_dashboard_show_team_group_data"].ToString() == "1" && Application.Current.Properties["team_group_id"] != null)
            {
                user_team_group_id = Application.Current.Properties["team_group_id"].ToString();
            }
            else if (Application.Current.Properties["mobile_dashboard_show_team_group_data"].ToString() == "1" && Application.Current.Properties["team_group_id"] == null)
            {
                user_team_group_id = "";

                if (Application.Current.Properties["user_type"].ToString() == "2")
                {
                    Check_TeamGroup = 1;
                }

            }
            else if (Application.Current.Properties["mobile_dashboard_show_team_group_data"].ToString() == "0")
            {
                user_team_group_id = "";

            }


            DateTime today = DateTime.Now;

            SearchFilter = new SearchDispatchConfirmationFilter();

            SearchFilter.approved_date_from = today;
            SearchFilter.approved_date_to = today;
            SearchFilter.confirmation_date_from = today;
            SearchFilter.confirmation_date_to = today;
            SearchFilter.dispatched_date_from = today;
            SearchFilter.dispatched_date_to = today;
            SearchFilter.approved_date = 1;

            try
            {
                if (_TodaysPlan == 0 || _TodaysPlan == 8)
                {
                    DispatchConfirm.Clear();
                }
                else
                {
                    DispatchConfirms.Clear();
                }

                try
                {

                    if (_TodaysPlan == 0 || _TodaysPlan == 8)
                    {
                        DispatchConfirm.Clear();
                    }
                    else
                    {
                        DispatchConfirms.Clear();
                    }
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
                                        new KeyValuePair<string,string>("dispatch_date_from", SearchFilter.approved_date_from.ToString("yyyy-MM-dd")),
                                        new KeyValuePair<string,string>("dispatch_date_to", SearchFilter.approved_date_to.ToString("yyyy-MM-dd")),
                                        new KeyValuePair<string,string>("dispatched_date_from", SearchFilter.dispatched_date_from.ToString("yyyy-MM-dd")),
                                        new KeyValuePair<string,string>("dispatched_date_to", SearchFilter.dispatched_date_to.ToString("yyyy-MM-dd")),
                                        new KeyValuePair<string,string>("confirmation_date",SearchFilter.confirmation_date.ToString()),
                                        new KeyValuePair<string,string>("dispatch_date",SearchFilter.approved_date.ToString()),
                                        new KeyValuePair<string,string>("dispatched_date",SearchFilter.dispatched_date.ToString()),
                                        new KeyValuePair<string,string>("bags_ready",SearchFilter.bags_ready.ToString()),
                                        new KeyValuePair<string,string>("payment_ready",SearchFilter.payment_ready.ToString()),
                                        new KeyValuePair<string,string>("payment_received",SearchFilter.payment_received.ToString()),
                                        new KeyValuePair<string,string>("transporter_ready",SearchFilter.transporter_ready.ToString()),
                                        new KeyValuePair<string,string>("search_string",SearchFilter.search_string.ToString()),
                                        new KeyValuePair<string,string>("include_received_form",SearchFilter.include_received_form.ToString()),
                                        new KeyValuePair<string,string>("datewise","today"), //datewise
                                        new KeyValuePair<string,string>("todays_plan",_TodaysPlan.ToString()),
                                        new KeyValuePair<string,string>("outersearch",SearchFilter.outersearch.ToString()),
                                        new KeyValuePair<string,string>("CurrentPageNumber","0"),
                                         new KeyValuePair<string,string>("team_group_id",user_team_group_id),
                                        new KeyValuePair<string,string>("PageSize",PageSize.ToString())
                                });

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/dispatch_confirmation_list", formcontent);

                            var response = await request.Content.ReadAsStringAsync();

                            DispatchConfirm_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<DispatchConfirm_list>(response);

                            foreach (DispatchConfirm item in res.DispatchConfirms)
                            {

                                double diff2;

                                diff2 = (DateTime.Today.Date - item.transaction_date_time).TotalDays;

                                if (_TodaysPlan == 8)
                                {

                                    diff2 = (DateTime.Today.Date - item.transaction_date_time).TotalDays;

                                    if (diff2 > 0)
                                    {
                                        item.DelayDays = (int)Math.Ceiling(diff2) + " Days Old";
                                    }
                                    else
                                    {
                                        item.DelayDays = "";
                                    }
                                    item.invoice_visible_on = false;
                                    item.invoice_visible_off = true;
                                }
                                if (_TodaysPlan == 0)
                                {

                                    diff2 = (DateTime.Today.Date - item.transaction_date_time).TotalDays;

                                    if (diff2 > 0)
                                    {
                                        item.DelayDays = (int)Math.Ceiling(diff2) + " Days Old";
                                    }
                                    else
                                    {
                                        item.DelayDays = "";
                                    }
                                    item.invoice_visible_on = true;
                                    item.invoice_visible_off = false;
                                }

                                if (diff2 > 0)
                                    item.transaction_detail = item.transaction_detail + " - " + (int)Math.Ceiling(diff2) + " Days Old";

                                if (_TodaysPlan == 0 || _TodaysPlan == 8)
                                {
                                    DispatchConfirm.Add(item);
                                }
                                else
                                {
                                    DispatchConfirms.Add(item);
                                }


                            }
                            totalRows = res.totalRows;
                            if (res.totalRows > 0)
                            {
                                No_Of_Count_With_Bag_Weight = "" + res.totalRows + " Nos. ( " + res.dispatch_confirm_value + "  )";
                                if (_TodaysPlan == 13) HideGenerateInvoice = true;
                            }
                            else
                            {
                                No_Of_Count_With_Bag_Weight = "No Records.";
                                if (_TodaysPlan == 13) HideGenerateInvoice = false;
                            }

                            if (Check_TeamGroup == 1)
                            {
                                if (_TodaysPlan == 0 || _TodaysPlan == 8)
                                {
                                    DispatchConfirm.Clear();
                                }
                                else
                                {
                                    DispatchConfirms.Clear();
                                }

                                No_Of_Count_With_Bag_Weight = "";
                                Check_TeamGroup = 0;
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


        public async Task ExecuteLoadPendingConfirmationDelayCommand()
        {


            if (Application.Current.Properties["mobile_dashboard_show_team_group_data"].ToString() == "1" && Application.Current.Properties["team_group_id"] != null)
            {
                user_team_group_id = Application.Current.Properties["team_group_id"].ToString();
            }
            else if (Application.Current.Properties["mobile_dashboard_show_team_group_data"].ToString() == "1" && Application.Current.Properties["team_group_id"] == null)
            {
                user_team_group_id = "";

                if (Application.Current.Properties["user_type"].ToString() == "2")
                {
                    Check_TeamGroup = 1;
                }

            }
            else if (Application.Current.Properties["mobile_dashboard_show_team_group_data"].ToString() == "0")
            {
                user_team_group_id = "";

            }


            IsBusy = true;

            DateTime currentDate = DateTime.Now;
            DateTime delayFrom = currentDate.AddYears(-5);
            DateTime delayTo = currentDate.AddDays(-1);

            SearchFilter = new SearchDispatchConfirmationFilter();

            SearchFilter.approved_date_from = delayFrom;
            SearchFilter.approved_date_to = delayTo;
            SearchFilter.confirmation_date_from = delayFrom;
            SearchFilter.confirmation_date_to = delayTo;
            SearchFilter.dispatched_date_from = delayFrom;
            SearchFilter.dispatched_date_to = delayTo;
            SearchFilter.approved_date = 1;



            try
            {
                if (_TodaysPlan == 0 || _TodaysPlan == 8)
                {
                    DispatchConfirm.Clear();
                }
                else
                {
                    DispatchConfirms.Clear();
                }

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
                                        new KeyValuePair<string,string>("dispatch_date_from", SearchFilter.approved_date_from.ToString("yyyy-MM-dd")),
                                        new KeyValuePair<string,string>("dispatch_date_to", SearchFilter.approved_date_to.ToString("yyyy-MM-dd")),
                                        new KeyValuePair<string,string>("dispatched_date_from", SearchFilter.dispatched_date_from.ToString("yyyy-MM-dd")),
                                        new KeyValuePair<string,string>("dispatched_date_to", SearchFilter.dispatched_date_to.ToString("yyyy-MM-dd")),
                                        new KeyValuePair<string,string>("confirmation_date",SearchFilter.confirmation_date.ToString()),
                                        new KeyValuePair<string,string>("dispatch_date",SearchFilter.approved_date.ToString()),
                                        new KeyValuePair<string,string>("dispatched_date",SearchFilter.dispatched_date.ToString()),
                                        new KeyValuePair<string,string>("bags_ready",SearchFilter.bags_ready.ToString()),
                                        new KeyValuePair<string,string>("payment_ready",SearchFilter.payment_ready.ToString()),
                                        new KeyValuePair<string,string>("payment_received",SearchFilter.payment_received.ToString()),
                                        new KeyValuePair<string,string>("transporter_ready",SearchFilter.transporter_ready.ToString()),
                                        new KeyValuePair<string,string>("search_string",SearchFilter.search_string.ToString()),
                                        new KeyValuePair<string,string>("include_received_form",SearchFilter.include_received_form.ToString()),
                                        new KeyValuePair<string,string>("datewise","today"),
                                        new KeyValuePair<string,string>("todays_plan",_TodaysPlan.ToString()),
                                        new KeyValuePair<string,string>("outersearch",SearchFilter.outersearch.ToString()),
                                        new KeyValuePair<string,string>("CurrentPageNumber","0"),
                                         new KeyValuePair<string,string>("team_group_id",user_team_group_id),
                                        new KeyValuePair<string,string>("PageSize",PageSize.ToString())
                                });




                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/dispatch_confirmation_list", formcontent);



                            var response = await request.Content.ReadAsStringAsync();

                            DispatchConfirm_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<DispatchConfirm_list>(response);

                            foreach (DispatchConfirm item in res.DispatchConfirms)
                            {

                                double diff2;

                                diff2 = (DateTime.Today.Date - item.transaction_date_time).TotalDays;

                                if (_TodaysPlan == 0)
                                {

                                    diff2 = (DateTime.Today.Date - item.transaction_date_time).TotalDays;
                                    if (diff2 > 0)
                                    {
                                        item.DelayDays = (int)Math.Ceiling(diff2) + " Days Old";
                                    }
                                    else
                                    {
                                        item.DelayDays = "";
                                    }
                                    item.invoice_visible_on = true;
                                    item.invoice_visible_off = false;
                                }


                                if (diff2 > 0)
                                    item.transaction_detail = item.transaction_detail + " - " + (int)Math.Ceiling(diff2) + " Days Old";

                                if (_TodaysPlan == 0 || _TodaysPlan == 8)
                                {
                                    DispatchConfirm.Add(item);
                                }
                                else
                                {
                                    DispatchConfirms.Add(item);
                                }


                            }
                            totalRows = res.totalRows;
                            if (res.totalRows > 0)
                            {
                                No_Of_Count_With_Bag_Weight = "" + res.totalRows + " Nos. ( " + res.dispatch_confirm_value + "  )";
                                if (_TodaysPlan == 13) HideGenerateInvoice = true;
                            }
                            else
                            {
                                No_Of_Count_With_Bag_Weight = "No Records.";
                                if (_TodaysPlan == 13) HideGenerateInvoice = false;
                            }

                            if (Check_TeamGroup == 1)
                            {
                                if (_TodaysPlan == 0 || _TodaysPlan == 8)
                                {
                                    DispatchConfirm.Clear();
                                }
                                else
                                {
                                    DispatchConfirms.Clear();
                                }

                                No_Of_Count_With_Bag_Weight = "";
                                Check_TeamGroup = 0;
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
        public async Task ExecuteLoadPendingConfirmationFutureCommand()
        {



            IsBusy = true;

            DateTime currentDate = DateTime.Now;
            DateTime futureTo = currentDate.AddYears(5);
            DateTime futureFrom = currentDate.AddDays(1);

            SearchFilter = new SearchDispatchConfirmationFilter();

            SearchFilter.approved_date_from = futureFrom;
            SearchFilter.approved_date_to = futureTo;
            SearchFilter.confirmation_date_from = futureFrom;
            SearchFilter.confirmation_date_to = futureTo;
            SearchFilter.dispatched_date_from = futureFrom;
            SearchFilter.dispatched_date_to = futureTo;
            SearchFilter.approved_date = 1;

            if (Application.Current.Properties["mobile_dashboard_show_team_group_data"].ToString() == "1" && Application.Current.Properties["team_group_id"] != null)
            {
                user_team_group_id = Application.Current.Properties["team_group_id"].ToString();
            }
            else if (Application.Current.Properties["mobile_dashboard_show_team_group_data"].ToString() == "1" && Application.Current.Properties["team_group_id"] == null)
            {
                user_team_group_id = "";

                if (Application.Current.Properties["user_type"].ToString() == "2")
                {
                    Check_TeamGroup = 1;
                }

            }
            else if (Application.Current.Properties["mobile_dashboard_show_team_group_data"].ToString() == "0")
            {
                user_team_group_id = "";

            }


            try
            {
                if (_TodaysPlan == 0 || _TodaysPlan == 8)
                {
                    DispatchConfirm.Clear();
                }
                else
                {
                    DispatchConfirms.Clear();
                }

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
                                        new KeyValuePair<string,string>("dispatch_date_from", SearchFilter.approved_date_from.ToString("yyyy-MM-dd")),
                                        new KeyValuePair<string,string>("dispatch_date_to", SearchFilter.approved_date_to.ToString("yyyy-MM-dd")),
                                        new KeyValuePair<string,string>("dispatched_date_from", SearchFilter.dispatched_date_from.ToString("yyyy-MM-dd")),
                                        new KeyValuePair<string,string>("dispatched_date_to", SearchFilter.dispatched_date_to.ToString("yyyy-MM-dd")),
                                        new KeyValuePair<string,string>("confirmation_date",SearchFilter.confirmation_date.ToString()),
                                        new KeyValuePair<string,string>("dispatch_date",SearchFilter.approved_date.ToString()),
                                        new KeyValuePair<string,string>("dispatched_date",SearchFilter.dispatched_date.ToString()),
                                        new KeyValuePair<string,string>("bags_ready",SearchFilter.bags_ready.ToString()),
                                        new KeyValuePair<string,string>("payment_ready",SearchFilter.payment_ready.ToString()),
                                        new KeyValuePair<string,string>("payment_received",SearchFilter.payment_received.ToString()),
                                        new KeyValuePair<string,string>("transporter_ready",SearchFilter.transporter_ready.ToString()),
                                        new KeyValuePair<string,string>("search_string",SearchFilter.search_string.ToString()),
                                        new KeyValuePair<string,string>("include_received_form",SearchFilter.include_received_form.ToString()),
                                        new KeyValuePair<string,string>("datewise","today"), //datewise
                                        new KeyValuePair<string,string>("todays_plan",_TodaysPlan.ToString()),
                                        new KeyValuePair<string,string>("outersearch",SearchFilter.outersearch.ToString()),
                                        new KeyValuePair<string,string>("CurrentPageNumber","0"),
                                        new KeyValuePair<string,string>("team_group_id",user_team_group_id),
                                        new KeyValuePair<string,string>("PageSize",PageSize.ToString())
                                });

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/dispatch_confirmation_list", formcontent);

                            var response = await request.Content.ReadAsStringAsync();

                            DispatchConfirm_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<DispatchConfirm_list>(response);

                            foreach (DispatchConfirm item in res.DispatchConfirms)
                            {

                                double diff2;

                                diff2 = (DateTime.Today.Date - item.transaction_date_time).TotalDays;


                                if (_TodaysPlan == 0)
                                {

                                    diff2 = (DateTime.Today.Date - item.transaction_date_time).TotalDays;
                                    if (diff2 > 0)
                                    {
                                        item.DelayDays = (int)Math.Ceiling(diff2) + " Days Old";
                                    }
                                    else
                                    {
                                        item.DelayDays = "";
                                    }
                                    item.invoice_visible_on = true;
                                    item.invoice_visible_off = false;
                                }


                                if (diff2 > 0)
                                    item.transaction_detail = item.transaction_detail + " - " + (int)Math.Ceiling(diff2) + " Days Old";


                                if (_TodaysPlan == 0 || _TodaysPlan == 8)
                                {
                                    DispatchConfirm.Add(item);
                                }
                                else
                                {
                                    DispatchConfirms.Add(item);
                                }


                            }
                            totalRows = res.totalRows;
                            if (res.totalRows > 0)
                            {
                                No_Of_Count_With_Bag_Weight = "" + res.totalRows + " Nos. ( " + res.dispatch_confirm_value + "  )";
                                if (_TodaysPlan == 13) HideGenerateInvoice = true;
                            }
                            else
                            {
                                No_Of_Count_With_Bag_Weight = "No Records.";
                                if (_TodaysPlan == 13) HideGenerateInvoice = false;
                            }

                            if (Check_TeamGroup == 1)
                            {
                                if (_TodaysPlan == 0 || _TodaysPlan == 8)
                                {
                                    DispatchConfirm.Clear();
                                }
                                else
                                {
                                    DispatchConfirms.Clear();
                                }

                                No_Of_Count_With_Bag_Weight = "";
                                Check_TeamGroup = 0;
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
























        public void ProgramApprovalDeleteCommand(int ID)
        {
            try
            {
                //Edit_Check = false;
                var index_value = DispatchConfirm.IndexOf(DispatchConfirm.Where(x => x.Id == ID).FirstOrDefault());
                DispatchConfirm.RemoveAt(index_value);


                for (int i = 0; i < DispatchConfirm.Count(); i++)
                {
                    if (DispatchConfirm[i].unit == "BAGS")
                    {
                        for (int bags = 0; bags < DispatchConfirm.Count(); bags++)
                        {
                            BAGQTY = DispatchConfirm.Where(x => x.unit == "BAGS").Sum(x => x.qty).ToString() + " BAGS /";
                        }
                    }
                    if (DispatchConfirm[i].unit == "FCL")
                    {

                        for (int fcl = 0; fcl < DispatchConfirm.Count(); fcl++)
                        {
                            FCLQTY = DispatchConfirm.Where(x => x.unit == "FCL").Sum(x => x.qty).ToString() + " FCL";
                        }
                    }
                    if (DispatchConfirm[i].unit == "BOX")
                    {

                        for (int fcl = 0; fcl < DispatchConfirm.Count(); fcl++)
                        {
                            BOXQTY = "/" + DispatchConfirm.Where(x => x.unit == "BOX").Sum(x => x.qty).ToString() + " BOX";
                        }
                    }
                    if (DispatchConfirm[i].unit == "PALLET")
                    {

                        for (int fcl = 0; fcl < DispatchConfirm.Count(); fcl++)
                        {
                            PALQTY = "/" + DispatchConfirm.Where(x => x.unit == "PALLET").Sum(x => x.qty).ToString() + " PALLET";
                        }
                    }
                    if (DispatchConfirm[i].unit == "BALE")
                    {

                        for (int fcl = 0; fcl < DispatchConfirm.Count(); fcl++)
                        {
                            BALEQTY = "/" + DispatchConfirm.Where(x => x.unit == "BALE").Sum(x => x.qty).ToString() + " BALE";
                        }
                    }

                }
                QTY = BAGQTY + FCLQTY + BOXQTY + PALQTY + BALEQTY;


                if (DispatchConfirm.Count() > 0)
                    No_Of_Count_With_Bag_Weight = DispatchConfirm.Count() + " No's. (" + QTY + ")";
                else
                    No_Of_Count_With_Bag_Weight = "No Records.";


                //AddCount = 1;
                //Edit_Check = true;    
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

    }
}