using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using yarn_brokerage.Models;

namespace yarn_brokerage.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopupAttendance : Rg.Plugins.Popup.Pages.PopupPage
	{
      public ObservableCollection<DataList> DataList { get; set; }

        public AttendanceList AttendanceList { get; set; }
        public PopupAttendance (AttendanceList attendanceList)
		{
			InitializeComponent ();

            AttendanceList = attendanceList;
            DataList = new ObservableCollection<DataList>();

            ExecuteLoadItemsCommand(AttendanceList);

            BindingContext = this;
        }



        async Task ExecuteLoadItemsCommand(AttendanceList attendence_Dashboard)
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
                            {      new KeyValuePair<string,string>("log_date",attendence_Dashboard.TransactionDate),
                                   
                                    new KeyValuePair<string,string>("employee_id",attendence_Dashboard.EmployeeId.ToString()),
                            });

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"].ToString() + "api/attendance_timesheet_mobile", formcontent);

                            var response = await request.Content.ReadAsStringAsync();

                            var res = Newtonsoft.Json.JsonConvert.DeserializeObject<AttendanceTimeSheet>(response);

                            if (res.DataList.Count != 0)
                            {
                            int sno = 1;
                                foreach (DataList item in res.DataList)
                                {
                                   if(item.Id != 0)
                                {
                                    item.Sno = sno++;
                                }
                                DataList.Add(item);
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
         
          
        











        //private async void OnClose(object sender, EventArgs e)
        //{
        //    await PopupNavigation.PopAsync(true);
        //}
        //protected override Task OnAppearingAnimationEndAsync()
        //{
        //    return Content.FadeTo(0.5);
        //}
        //protected override Task OnDisappearingAnimationBeginAsync()
        //{
        //    return Content.FadeTo(1);
        //}
    }
}