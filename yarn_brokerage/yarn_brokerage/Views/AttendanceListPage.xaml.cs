using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using yarn_brokerage.Models;
using yarn_brokerage.ViewModels;

namespace yarn_brokerage.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AttendanceListPage : ContentPage
	{
        AttendanceViewModel viewModel;

        public DateTime AttendenceDashboard { get; set; }
        AttendenceDashboard Attendence_Dashboard { get; set; }
        public AttendanceListPage (AttendenceDashboard attendance_data)
		{
			InitializeComponent ();

            Attendence_Dashboard = attendance_data;
           
            BindingContext = viewModel = new AttendanceViewModel(Attendence_Dashboard);

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

                viewModel.LoadItemsCommand.Execute(Attendence_Dashboard);
        }




        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as AttendanceList;

            attendancelist.SelectedItem = null;

            if (item == null)
                return;

            await PopupNavigation.Instance.PushAsync(new PopupAttendance(item));
           
        }

    }
}