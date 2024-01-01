using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using yarn_brokerage.Models;
using yarn_brokerage.ViewModels;

namespace yarn_brokerage.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttendanceSummary : ContentPage
    {
        AttendanceViewModel viewModel;
        AttendenceDashboard Attendence_Dashboard { get; set; }

        bool BackButtonCheck;
        public AttendanceSummary(AttendenceDashboard attendance_data = null, bool backButtonCheck = false)
        {
            InitializeComponent();

            BackButtonCheck = backButtonCheck;

            if (attendance_data == null)
            {
                Attendence_Dashboard = new AttendenceDashboard();
                DateTime currentDate = startAttendenceSummaryDatePicker.Date;
                DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                Attendence_Dashboard.FromDays = firstDayOfMonth;
                Attendence_Dashboard.ToDays = lastDayOfMonth;
            }
            else
            {
                Attendence_Dashboard = attendance_data;
            }



            BindingContext = viewModel = new AttendanceViewModel(Attendence_Dashboard);  // 1 => summary
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LoadSummaryCommand.Execute(Attendence_Dashboard);
        }

        private void butAttendenceSummaryPreviousDate_Clicked(object sender, EventArgs e)
        {
            startAttendenceSummaryDatePicker.IsVisible = false;
            startAttendenceSummaryDatePicker.Date = startAttendenceSummaryDatePicker.Date.AddMonths(-1);
            startAttendenceSummaryDatePicker.IsVisible = true;
        }
        private void butAttendenceNextSummaryDate_Clicked(object sender, EventArgs e)
        {
            startAttendenceSummaryDatePicker.IsVisible = false;
            startAttendenceSummaryDatePicker.Date = startAttendenceSummaryDatePicker.Date.AddMonths(1);
            startAttendenceSummaryDatePicker.IsVisible = true;
        }
        private void startAttendenceSummaryDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            DateTime currentDate = startAttendenceSummaryDatePicker.Date;
            DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            Attendence_Dashboard.FromDays = firstDayOfMonth;
            Attendence_Dashboard.ToDays = lastDayOfMonth;
            viewModel.LoadSummaryCommand.Execute(Attendence_Dashboard);
        }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as AttendenceDashboard;

            AttandanceSummaryListView.SelectedItem = null;

            if (item == null)
                return;

            Attendence_Dashboard.User_Id = item.YarnUserId;

            await Navigation.PushAsync(new AttendanceListPage(Attendence_Dashboard));

        }
        public bool CloseApp = true;
        protected override bool OnBackButtonPressed()
        {
            if (Navigation.NavigationStack.Count <= 1)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    bool result = await this.DisplayAlert("Attention!", "Do you want to exit?", "Yes", "No");
                    if (result)
                    {
                        CloseApp = false;
                        Process.GetCurrentProcess().CloseMainWindow();
                        Process.GetCurrentProcess().Close();
                    }

                });
            }
            else
            {
                CloseApp = false;
            }
            return CloseApp;
        }
    }
}