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
    public class AttendanceViewModel : BaseViewModel
    {
        public InfiniteScrollCollection<AttendanceList> Attendance { get; set; }
        public ObservableCollection<AttendenceDashboard> AttendenceSummary { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command LoadSummaryCommand { get; set; }

        private const int PageSize = 20;

        public AttendenceDashboard AttendenceDashboard { get; set; }

        public AttendanceViewModel(AttendenceDashboard attendence_Dashboard)
        {
            AttendenceDashboard = attendence_Dashboard;

            AttendenceSummary = new ObservableCollection<AttendenceDashboard>();
            
            Attendance = new InfiniteScrollCollection<AttendanceList>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = Attendance.Count / PageSize;

                    //var items = await _dataService.GetItemsAsync(page, PageSize);


                    User_Id = attendence_Dashboard.User_Id;

                    if (User_Id != 0)
                    {
                        User_Id = attendence_Dashboard.User_Id;
                    }
                    else
                    {
                        User_Id = (int)Application.Current.Properties["user_id"];
                    }


                    using (var cl = new HttpClient())
                    {
                        HttpContent formcontent = null;

                        formcontent = new FormUrlEncodedContent(new[]
                        {
                                   new KeyValuePair<string, string>("CurrentPageNumber", page.ToString()),
                                    new KeyValuePair<string, string>("PageSize", PageSize.ToString()),
                                    new KeyValuePair<string, string>("filterParameters[date_from]", attendence_Dashboard.FromDays.ToString("yyyy-MM-dd")),
                                    new KeyValuePair<string, string>("filterParameters[date_to]", attendence_Dashboard.ToDays.ToString("yyyy-MM-dd")),
                                     new KeyValuePair<string,string>("user_id",User_Id.ToString()),

                                });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/attendance_summary_detail_list_mobile", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        var res = Newtonsoft.Json.JsonConvert.DeserializeObject<AttendanceListData>(response);
                        AttendanceListData enquiry_List = new AttendanceListData();
                        enquiry_List.TotalRows = res.TotalRows;
                        enquiry_List.AttendanceList = new List<AttendanceList>();

                        if (res.AttendanceList.Count != 0)
                        {
                            foreach (AttendanceList item in res.AttendanceList)
                            {
                                if (item.DayPresent == "Full Day")
                                {
                                    item.Attendance_List = "Full Day";
                                }
                                else if (item.DayPresent == "Half Day")
                                {
                                    item.Attendance_List = "Half Day";
                                }
                                else if (item.DayPresent == "Absent")
                                {
                                    item.Attendance_List = "Absent";
                                    item.Text_Color = System.Drawing.Color.Red;
                                }
                                item.PermissionDay = (item.PermissionDayMorning + item.PermissionDayEvening).ToString();
                                item.LateDays = (item.LateDayMorning + item.LateDayEvening + item.LateDayLunch).ToString();


                                item.PermissionDay = (item.PermissionDay == "0") ? "" : (item.PermissionDay).ToString();
                                item.LateDays = (item.LateDays == "0") ? "" : (item.LateDays).ToString();


                                Attendance.Add(item);
                            }
                        }
                        IsBusy = false;
                        return enquiry_List.AttendanceList;
                    }
                    // return the items that need to be added

                },
                OnCanLoadMore = () =>
                {
                    return Attendance.Count < totalRows;
                }
            };


            LoadItemsCommand = new Command(async (object enquiry_date) => await ExecuteLoadItemsCommand(attendence_Dashboard));
            LoadSummaryCommand = new Command(async (object enquiry_date) => await ExecuteLoadSummaryCommand(attendence_Dashboard));

        }

        DateTime _enquiry_date;
        private int totalRows = 0;
        private DateTime date;
        private int User_Id;
        async Task ExecuteLoadItemsCommand(AttendenceDashboard attendence_Dashboard)
        {
            if (IsBusy)
                return;

            //int userID = attendence_Dashboard.Id;

            User_Id = attendence_Dashboard.User_Id;

            if (User_Id != 0)
            {
                User_Id = attendence_Dashboard.User_Id;
            }
            else
            {
                User_Id = (int)Application.Current.Properties["user_id"];
            }

            IsBusy = true;

            try
            {
                Attendance.Clear();

                try
                {
                    var current = Connectivity.NetworkAccess;

                    if (current == NetworkAccess.Internet)
                    {
                        using (var cl = new HttpClient())
                        {
                            HttpContent formcontent = null;

                            formcontent = new FormUrlEncodedContent(new[]
                            {      new KeyValuePair<string,string>("CurrentPageNumber","0"),
                                    new KeyValuePair<string,string>("PageSize",PageSize.ToString()),
                                    new KeyValuePair<string, string>("filterParameters[date_from]", attendence_Dashboard.FromDays.ToString("yyyy-MM-dd")),
                                    new KeyValuePair<string, string>("filterParameters[date_to]", attendence_Dashboard.ToDays.ToString("yyyy-MM-dd")),
                                    new KeyValuePair<string,string>("user_id",User_Id.ToString()),
                            });

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"].ToString() + "api/attendance_summary_detail_list_mobile", formcontent);

                            var response = await request.Content.ReadAsStringAsync();

                            var res = Newtonsoft.Json.JsonConvert.DeserializeObject<AttendanceListData>(response);

                            if (res.AttendanceList.Count != 0)
                            {
                                foreach (AttendanceList item in res.AttendanceList)
                                {
                                    if (item.DayPresent == "Full Day")
                                    {
                                        item.Attendance_List = "Full Day";
                                    }
                                    else if (item.DayPresent == "Half Day")
                                    {
                                        item.Attendance_List = "Half Day";
                                    }
                                    else if (item.DayPresent == "Absent")
                                    {
                                        item.Attendance_List = "Absent";
                                        item.Text_Color = System.Drawing.Color.Red;
                                    }



                                    item.PermissionDay = (item.PermissionDayMorning + item.PermissionDayEvening).ToString();
                                    item.LateDays = (item.LateDayMorning + item.LateDayEvening + item.LateDayLunch).ToString();


                                    item.PermissionDay = (item.PermissionDay == "0") ? "" : (item.PermissionDay).ToString();
                                    item.LateDays = (item.LateDays == "0") ? "" : (item.LateDays).ToString();

                                    Attendance.Add(item);
                                }
                            }
                            totalRows = res.TotalRows;
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

        async Task ExecuteLoadSummaryCommand(AttendenceDashboard attendence_Dashboard)
        {
            if (IsBusy)
                return;


            IsBusy = true;

            try
            {
                AttendenceSummary.Clear();

                try
                {
                    var current = Connectivity.NetworkAccess;

                    if (current == NetworkAccess.Internet)
                    {
                        using (var cl = new HttpClient())
                        {
                            HttpContent formcontent = null;

                            formcontent = new FormUrlEncodedContent(new[]
                            {      new KeyValuePair<string,string>("CurrentPageNumber","0"),
                                    new KeyValuePair<string,string>("PageSize",PageSize.ToString()),
                                    new KeyValuePair<string, string>("filterParameters[date_from]", attendence_Dashboard.FromDays.ToString("yyyy-MM-dd")),
                                    new KeyValuePair<string, string>("filterParameters[date_to]", attendence_Dashboard.ToDays.ToString("yyyy-MM-dd")),
                                        new KeyValuePair<string,string>("user_id",""),
                            });

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"].ToString() + "api/attendance_summary_list_mobile", formcontent);

                            var response = await request.Content.ReadAsStringAsync();

                            var res = Newtonsoft.Json.JsonConvert.DeserializeObject<DashboardAttendence>(response);

                            if (res.AttendenceDashboard.Count != 0)
                            {
                                foreach (AttendenceDashboard item in res.AttendenceDashboard.OrderBy(d => d.EmployeeName))
                                {

                                    item.FullDay = (item.FullDay == "0") ? "" : (item.FullDay).ToString();
                                    item.HalfDay = (item.HalfDay == "0") ? "" : (item.HalfDay).ToString();
                                    item.Absent = (item.Absent == "0") ? "" : (item.Absent).ToString();
                                    item.LateTotal = (item.LateTotal == "0") ? "" : (item.LateTotal).ToString();
                                    item.PermissionTotal = (item.PermissionTotal == "0") ? "" : (item.PermissionTotal).ToString();
                                    AttendenceSummary.Add(item);
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
        public async Task<Attendance> StoreAttendanceCommand(Attendance _attendance)
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
                            new KeyValuePair<string,string>("id",_attendance.id.ToString()),
                            new KeyValuePair<string,string>("remarks",_attendance.remarks.ToString()),
                            new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["AttendenceURL"].ToString() + "api/attendance_store", formcontent);



                        var response = await request.Content.ReadAsStringAsync();

                        Attendance_Store_Result res = Newtonsoft.Json.JsonConvert.DeserializeObject<Attendance_Store_Result>(response);

                        _attendance = res.Attendances;

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return _attendance;
        }




        public int AttendancesCount()
        {
            return Attendance.Count();
        }
    }
}