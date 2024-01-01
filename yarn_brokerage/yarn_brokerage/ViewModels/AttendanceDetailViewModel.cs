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

namespace yarn_brokerage.ViewModels
{
    public class AttendanceDetailViewModel : BaseViewModel
    {
        public ObservableCollection<AttendanceDetail> AttendanceDetails { get; set; }
       // public Attendance Attendance;
        public Command LoadAttendanceDetailsCommand { get; set; }
        public Command LoadAttendanceCommand { get; set; }
        //public Command LoadItemsCommand { get; set; }
        public DateTime date { get; set; }

        public SearchConfirmationFilter SearchFilter { get; set; }
        // public Command StoreAttendanceCommand { get; set; }
        
        double totalBalance = 0.00;
        public double TotalBalance
        {
            get { return totalBalance; }
            set { SetProperty(ref totalBalance, value); }
        }

        public AttendanceDetailViewModel()
        {
            Title = "Time Sheet";
            date = DateTime.Now.ToLocalTime();
            AttendanceDetails = new ObservableCollection<AttendanceDetail>();
           // Attendance = new Attendance();
            LoadAttendanceCommand = new Command(async (object Attendance) => await ExecuteAttendanceCommand(Attendance));
            //LoadAttendanceDetailsCommand = new Command(async (object AttendanceDetail) => await ExecuteAttendanceDetailsCommand(AttendanceDetail));
            //LoadItemsCommand = new Command(async (object AttendanceId) => await ExecuteLoadItemsCommand(AttendanceId));
        }

        public async Task ExecuteAttendanceCommand(object attendance) //object confirmation_date
        {
            if (IsBusy)
                return;
            IsBusy = true;
            Attendance _Attendance = attendance as Attendance;
            try
            {
                AttendanceDetails.Clear();
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
                                    new KeyValuePair<string,string>("employee_id",_Attendance.employee_id.ToString()),
                                    new KeyValuePair<string,string>("log_date",_Attendance.transaction_date.ToString("yyyy-MM-dd")),
                                    new KeyValuePair<string,string>("db_name","ePay_4".ToString()),
                                });


                            var request = await cl.PostAsync(Application.Current.Properties["AttendenceURL"].ToString() + "api/attendance_timesheet", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            Attendance_Detail_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<Attendance_Detail_list>(response);
                            int i = 0;
                            foreach (AttendanceDetail item in res.AttendanceDetails)
                            {
                                i = i + 1;
                                if (i % 2 == 0)
                                    item.log_status = "Log Out";
                                else
                                    item.log_status = "Log In";
                                //if (_Attendance.ledger_type == 1)
                                //    item.image = "buyer.png";
                                //item.receipt_details = item.invoice_no + " ( " + item.invoice_date.ToString("dd-MM-yyyy") + " )";
                                //if(item.cancel_dispatch == 1)
                                //{
                                //    item.TextColor = "Red";
                                //    item.dispatch_status_visible = 1;
                                //    item.dispatch_status = "CANCELLED";
                                //}
                                //else if(item.program_approval==1 && item.program_approved == 0 && item.dispatched == 0)
                                //{
                                //    item.TextColor = "Green";
                                //    item.dispatch_status_visible = 1;
                                //    item.dispatch_status = "APPROVAL";
                                //}
                                //else if (item.program_approval == 1 && item.program_approved == 1 && item.dispatched == 0)
                                //{
                                //    item.TextColor = "Green";
                                //    item.dispatch_status_visible = 1;
                                //    item.dispatch_status = "CURRENT";
                                //}
                                //else if (item.program_approval == 1 && item.program_approved == 1 && item.dispatched == 1)
                                //{
                                //    item.TextColor = "Green";
                                //    item.dispatch_status_visible = 1;
                                //    item.dispatch_status = "DISPATCHED";
                                //}
                                AttendanceDetails.Add(item);
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

        public async Task<int> AttendanceCount()
        {
            return AttendanceDetails.Count();
        }

        

        public async Task<string> StoreAttendanceCommand(Attendance Attendance)
        {
            //Indexes enquiry = (Indexes)_enquiry;
            try
            {
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    foreach (AttendanceDetail item in AttendanceDetails)
                    {
                        using (var cl = new HttpClient())
                        {
                            HttpContent formcontent = null;

                            formcontent = new FormUrlEncodedContent(new[]
                            {
                            new KeyValuePair<string,string>("id",item.id.ToString()),
                            new KeyValuePair<string,string>("commission_receipt_id",Attendance.id.ToString()),
                            //new KeyValuePair<string,string>("commission_invoice_id",item.commission_invoice_id.ToString()),
                            //new KeyValuePair<string,string>("total_commission",item.total_commission.ToString()),
                            //new KeyValuePair<string,string>("commission_receipt_amount",item.commission_receipt_amount.ToString()),
                            //new KeyValuePair<string,string>("ledger_type",Attendance.ledger_type.ToString()),
                            new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                        });

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/commission_receipt_store_detail", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            //Attendance_Store_Result res = Newtonsoft.Json.JsonConvert.DeserializeObject<Attendance_Store_Result>(response);

                            //res.Attendances.price_per = res.Attendances.price.ToString() + " " + res.Attendances.per;
                            ////foreach (Attendance item in res.Attendances)
                            ////{
                            //Attendance = res.Attendances;
                            //}

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return "failure";
            }
             return "Sucess";
        }

        
        internal void UpdateAttendanceDetail(Attendance attendance, AttendanceDetail item)
        {
            var index = AttendanceDetails.IndexOf(item);
            AttendanceDetails.Remove(item);
            AttendanceDetails.Insert(index, item);
            
        }
    }    
}
