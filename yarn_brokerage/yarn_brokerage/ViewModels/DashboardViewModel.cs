using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using yarn_brokerage.Models;
using yarn_brokerage.Views;


namespace yarn_brokerage.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        public ObservableCollection<Dashboard> Dashboards { get; set; }
        public ObservableCollection<AttendenceDashboard> AttendenceDashboard { get; set; }
        public Dashboard _Dashboard { get; set; }

        public SearchDispatchConfirmationFilter SearchFilter { get; set; }
        public AttendenceDashboard _AttendenceDashboard { get; set; }
        public ObservableCollection<TopDashboard> _TopDashboard { get; set; }

        public ObservableCollection<DispatchConfirm> DispatchConfirm { get; set; }
        public ObservableCollection<DispatchConfirm> DispatchConfirm1 { get; set; }
        public ObservableCollection<DispatchConfirm> DispatchConfirm2 { get; set; }
        public ObservableCollection<DispatchConfirm> DispatchConfirm3 { get; set; }
        public Command LoadDashboardsCommand { get; set; }

        private int User_Id;
        private int Check_TeamGroup;

        private string _SearchString = "";

        private int totalRows = 0;
        private const int PageSize = 20;
        private string Enquiry_Date;
        public DashboardViewModel()
        {
            int flag = 0;
            Title = "Dashboard List";
            Dashboards = new ObservableCollection<Dashboard>();
            DispatchConfirm = new ObservableCollection<DispatchConfirm>();
            DispatchConfirm1 = new ObservableCollection<DispatchConfirm>();
            DispatchConfirm2 = new ObservableCollection<DispatchConfirm>();
            DispatchConfirm3 = new ObservableCollection<DispatchConfirm>();
            AttendenceDashboard = new ObservableCollection<AttendenceDashboard>();
            _TopDashboard = new ObservableCollection<TopDashboard>();
        }


        public async Task<ObservableCollection<AttendenceDashboard>> ExecuteLoadAttendenceDashboardsCommand(DateTime firstDayOfMonth, DateTime lastDayOfMonth, int AttendanceTeamNameId) //, int overdue_flag
        {
            _SearchString = "";


            if (AttendanceTeamNameId != 0)
            {
                User_Id = AttendanceTeamNameId;
            }
            else
            {
                User_Id = (int)Application.Current.Properties["user_id"];
            }

            try
            {
                AttendenceDashboard.Clear();
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
                                    new KeyValuePair<string,string>("CurrentPageNumber","0"),
                                    new KeyValuePair<string,string>("PageSize","1"),
                                    new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                    new KeyValuePair<string, string>("filterParameters[date_from]", firstDayOfMonth.ToString("yyyy-MM-dd")),
                                    new KeyValuePair<string, string>("filterParameters[date_to]", lastDayOfMonth.ToString("yyyy-MM-dd")),
                            });

                        
                           
                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"].ToString() + "api/attendance_summary_list_mobile", formcontent);
                            var response = await request.Content.ReadAsStringAsync();

                            var res = Newtonsoft.Json.JsonConvert.DeserializeObject<DashboardAttendence>(response);


                            if(res.AttendenceDashboard.Count != 0)
                            {
                                foreach (AttendenceDashboard item in res.AttendenceDashboard)
                                {
                                    AttendenceDashboard.Add(item);
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
            return AttendenceDashboard;
        }

        public async Task<Dashboard> ExecuteLoadDashboardsCommand(DateTime enquiry_date, string time_flag) //, int overdue_flag
        {

            if (IsBusy)
                return _Dashboard;
            IsBusy = true;

            _SearchString = "";

            if (time_flag == "Daily")
            {
                Enquiry_Date = enquiry_date.ToString("yyyy-MM-dd");
            }
            else if (time_flag == "Monthly")
            {
                Enquiry_Date = enquiry_date.ToString("yyyy-MM");
            }
            else if (time_flag == "Yearly")
            {
                Enquiry_Date = enquiry_date.ToString("yyyy");
            }


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
                Dashboards.Clear();
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

                                    new KeyValuePair<string,string>("enquiry_date",Enquiry_Date),
                                    new KeyValuePair<string,string>("time_flag",time_flag),
                                    new KeyValuePair<string,string>("team_group_id",user_team_group_id),
                            });
                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/dashboard_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            Dashboard res = Newtonsoft.Json.JsonConvert.DeserializeObject<Dashboard>(response);
                            _Dashboard = res;


                            if (Check_TeamGroup == 1)
                            {
                                _Dashboard.OfferCount = 0;
                                _Dashboard.EnquiryCount = 0;
                                _Dashboard.ConfirmationCount = "0";
                                _Dashboard.OfferLabel = "0 OFFERS ";
                                _Dashboard.EnquiryLabel = "0 ENQUIRY ";
                                _Dashboard.ConfirmLabel = "0 CONFIRM ";

                                _Dashboard.OfferValue = "0 UNITS ";
                                _Dashboard.EnquiryValue = "0 UNITS ";
                                _Dashboard.ConfirmValue = "0 UNITS ";


                                _Dashboard.DispatchConfirmValue = "0 UNITS ";
                                _Dashboard.bags_not_ready_value = "0 UNITS ";
                                _Dashboard.payment_not_ready_value = "0 UNITS ";
                                _Dashboard.payment_not_received_value = "0 UNITS ";

                                _Dashboard.transporter_not_ready_value = "0 UNITS ";
                                _Dashboard.dispatched_value = "0 UNITS ";
                                _Dashboard.invoice_details_value = "0 UNITS ";
                                _Dashboard.delivered_value = "0 UNITS ";
                                _Dashboard.dispatched_value = "0 UNITS ";
                                _Dashboard.after_dispatch_value = "0 UNITS ";



                                _Dashboard.ApprovalCount = "0";
                                _Dashboard.DispatchConfirmCount = "0";
                                _Dashboard.bags_not_ready_count = "0";
                                _Dashboard.payment_not_ready_count = "0";
                                _Dashboard.payment_not_received_count = "0";

                                _Dashboard.transporter_not_ready_count = "0";
                                _Dashboard.dispatched_value = "0";
                                _Dashboard.invoice_details_count = "0";
                                _Dashboard.dispatched_count = "0";
                                _Dashboard.delivered_count = "0";
                                _Dashboard.ProgramApprovalCount = "0";
                                _Dashboard.draft_confirmation_count = "0";
                                _Dashboard.after_dispatch_count = "0";

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
            return _Dashboard;
        }
        public async Task<ObservableCollection<TopDashboard>> ExecuteLoadTopDashboardsCommand(DateTime enquiry_date, string time_flag)
        {
            if (IsBusy)
                return _TopDashboard;
        
            _SearchString = "";

            IsBusy = true;

            try
            {
                _TopDashboard.Clear();
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
                                    new KeyValuePair<string,string>("enquiry_date",(time_flag=="Daily") ? enquiry_date.ToString("yyyy-MM-dd"):enquiry_date.ToString("yyyy-MM")),
                                    new KeyValuePair<string,string>("time_flag",time_flag),

                            });
                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/dashboard_top_list", formcontent);


                            var response = await request.Content.ReadAsStringAsync();

                            ObservableCollection<TopDashboard> res = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<TopDashboard>>(response);
                            foreach (TopDashboard item in res)
                            {
                                _TopDashboard.Add(item);
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
            return _TopDashboard;
        }

        public async Task<ObservableCollection<AttendenceDashboard>> ExecuteLoadAttendanceCommand(DateTime enquiry_date, string time_flag)
        {
            if (IsBusy)
                return AttendenceDashboard;

            _SearchString = "";

            IsBusy = true;

            try
            {
                AttendenceDashboard.Clear();
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
                                    new KeyValuePair<string,string>("enquiry_date",(time_flag=="Daily") ? enquiry_date.ToString("yyyy-MM-dd"):enquiry_date.ToString("yyyy-MM")),
                                    new KeyValuePair<string,string>("time_flag",time_flag),
                                    new KeyValuePair<string,string>("team_group_id",time_flag),
                            });
                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/dashboard_top_list", formcontent);


                            var response = await request.Content.ReadAsStringAsync();

                            ObservableCollection<AttendenceDashboard> res = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<AttendenceDashboard>>(response);
                            foreach (AttendenceDashboard item in res)
                            {
                                AttendenceDashboard.Add(item);
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
            return AttendenceDashboard;
        }

        public int EnquiryCount()
        {
            return _TopDashboard.Count();
        }


        int _TodaysPlan = 0;
        public int BAGQTY { get; set; }
        public int FCLQTY { get; set; }
        public int BOXQTY { get; set; }
        public int PALQTY { get; set; }
        public int BALEQTY { get; set; }

        string QTY;

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
                
                    DispatchConfirm1.Clear();
         

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
                                DispatchConfirm1.Add(item);
                            }

                            BAGQTY = (int)DispatchConfirm1.Where(x => x.unit == "BAGS").Sum(x => x.qty);
                            FCLQTY = (int)DispatchConfirm1.Where(x => x.unit == "FCL").Sum(x => x.qty);
                            BOXQTY = (int)DispatchConfirm1.Where(x => x.unit == "BOX").Sum(x => x.qty);
                            PALQTY = (int)DispatchConfirm1.Where(x => x.unit == "PALLET").Sum(x => x.qty);
                            BALEQTY = (int)DispatchConfirm1.Where(x => x.unit == "BALE").Sum(x => x.qty);

                            QTY = (BAGQTY + FCLQTY + BOXQTY + PALQTY + BALEQTY).ToString() + " UNITS";

                            totalRows = res.totalRows;

                            if (res.totalRows > 0)
                            {
                                No_Of_Pending_Today_With_Bag_Weight = QTY;
                            }
                            else
                            {
                                No_Of_Pending_Today_With_Bag_Weight = "No Records.";
                            }


                            if (Check_TeamGroup == 1)
                            {

                                DispatchConfirm1.Clear();
                                No_Of_Pending_Today_With_Bag_Weight = "0 UNITS";
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
                    DispatchConfirm2.Clear();
          

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
                                DispatchConfirm2.Add(item);
                            }

                            BAGQTY = (int)DispatchConfirm2.Where(x => x.unit == "BAGS").Sum(x => x.qty);
                            FCLQTY = (int)DispatchConfirm2.Where(x => x.unit == "FCL").Sum(x => x.qty);
                            BOXQTY = (int)DispatchConfirm2.Where(x => x.unit == "BOX").Sum(x => x.qty);
                            PALQTY = (int)DispatchConfirm2.Where(x => x.unit == "PALLET").Sum(x => x.qty);
                            BALEQTY = (int)DispatchConfirm2.Where(x => x.unit == "BALE").Sum(x => x.qty);

                            QTY = (BAGQTY + FCLQTY + BOXQTY + PALQTY + BALEQTY).ToString() + " UNITS";

                            totalRows = res.totalRows;

                            if (res.totalRows > 0)
                            {
                                No_Of_Pending_Delay_With_Bag_Weight = QTY;
                            }
                            else
                            {
                                No_Of_Pending_Delay_With_Bag_Weight = "No Records.";
                            }

                            if (Check_TeamGroup == 1)
                            {

                                DispatchConfirm2.Clear();



                                No_Of_Pending_Delay_With_Bag_Weight = "0 UNITS";
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
              
                   DispatchConfirm3.Clear();
                 
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
                                DispatchConfirm3.Add(item);
                            }

                            BAGQTY = (int)DispatchConfirm3.Where(x => x.unit == "BAGS").Sum(x => x.qty);
                            FCLQTY = (int)DispatchConfirm3.Where(x => x.unit == "FCL").Sum(x => x.qty);
                            BOXQTY = (int)DispatchConfirm3.Where(x => x.unit == "BOX").Sum(x => x.qty);
                            PALQTY = (int)DispatchConfirm3.Where(x => x.unit == "PALLET").Sum(x => x.qty);
                            BALEQTY = (int)DispatchConfirm3.Where(x => x.unit == "BALE").Sum(x => x.qty);
                            QTY = (BAGQTY + FCLQTY + BOXQTY + PALQTY + BALEQTY).ToString() + " UNITS";
                           
                            
                            totalRows = res.totalRows;

                            if (res.totalRows > 0)
                            {
                                No_Of_Pending_Future_With_Bag_Weight = QTY;
                            }
                            else
                            {
                                No_Of_Pending_Future_With_Bag_Weight = "No Records.";
                            }

                            if (Check_TeamGroup == 1)
                            {
                                DispatchConfirm3.Clear();
                                No_Of_Pending_Future_With_Bag_Weight = "0 UNITS";
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

        private string user_team_group_id;
        public async Task<Performance> ExecuteLoadWhatsappDashboardsCommand()
        {
            Performance responce = new Performance();

            try
            {
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    using (var cl = new HttpClient())
                    {

                        HttpContent formcontent = null;

                      
                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"].ToString() + "api/whatsapp_data_from_app", formcontent);

                        var response = await request.Content.ReadAsStringAsync();

                        Performance res = Newtonsoft.Json.JsonConvert.DeserializeObject<Performance>(response);

                        responce = res;
                    }
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
            return responce;
        }

        public async Task<Performance> ExecuteTeamPermissionCommand()
        {
            Performance responce = new Performance();


            if (Application.Current.Properties["team_group_id"] == null)
            {
                user_team_group_id = "";
            }
            else
            {
                user_team_group_id = Application.Current.Properties["team_group_id"].ToString();
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
                                        new KeyValuePair<string,string>("team_group_id",user_team_group_id),
                            });


                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"].ToString() + "api/whatsapp_data_from_app", formcontent);

                        var response = await request.Content.ReadAsStringAsync();

                        Performance res = Newtonsoft.Json.JsonConvert.DeserializeObject<Performance>(response);

                        responce = res;
                    }
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
            return responce;
        }

    }
}