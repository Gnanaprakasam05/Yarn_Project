using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using yarn_brokerage.Models;
using yarn_brokerage.ViewModels;
using System.Collections.ObjectModel;
using Rg.Plugins.Popup.Services;
using System.Timers;

namespace yarn_brokerage.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AddAttendancePage : ContentPage
    {
        public Attendance Attendance { get; set; }
        public ObservableCollection<AttendanceDetail> _AttendanceDetails { get; set; }

        AttendanceViewModel viewModel;
        AttendanceDetailViewModel detailViewModel;
        public DateTime date { get; set; }
        public SearchFilter _searchFilter { get; set; }
        public Indexes _enquiry { get; set; }
        public Approval Approval { get; set; }
        ApprovalViewModel approvalViewModel;
        public AddAttendancePage(Attendance _Attendance, SearchFilter searchFilter = null, Indexes enquiry = null)
        {
            InitializeComponent();
            
            //viewModel = new AttendanceViewModel(searchFilter);
            approvalViewModel = new ApprovalViewModel();
            _searchFilter = searchFilter;
            _enquiry = enquiry;    
            Attendance = _Attendance;            
                
            Title = "Time Sheet";     
            txtDateTime.Date = Attendance.transaction_date;
            
            lblDateTime.Text = "Log Date";


            BindingContext = detailViewModel  = new AttendanceDetailViewModel();

            
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
                
                int count = await detailViewModel.AttendanceCount();
                if (count <= 0)
                {
                    await DisplayAlert("Alert", "Receipt details are empty...", "OK");
                    return;
                }
                
                int i = await SaveConfirmation(sender, e);
            
        }

        async Task<int> SaveConfirmation(object sender, EventArgs e)
        {
            Attendance.remarks = String.IsNullOrEmpty(txtRemarks.Text) ? "" : txtRemarks.Text;
            Attendance = await viewModel.StoreAttendanceCommand(Attendance);
            
            if (sender != null)
                await Navigation.PopAsync();
            return 1;
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        double TotalAmount = 0.00;
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            txtRemarks.Text = Attendance.remarks;
            txtDateTime.Date = Attendance.transaction_date;
            if (Approval != null)
            {
                //if (Approval.status == 0)
                //    grdStatus.IsVisible = false;
                //else
                //    grdStatus.IsVisible = true;
                //Attendance.status = Approval.status;
                //if (Attendance.status == 1)
                //{
                //    Attendance.status_image = "approved.png";                   
                //}
                //else if (Attendance.status == 5)
                //{
                //    Attendance.status_image = "rejected.png";                   
                //}
                ////Attendance.approved_at = Approval.approved_at;
                //Attendance.approved_user = Approval.approved_user;
                ////Attendance.rejected_at = Approval.rejected_at;
                //Attendance.rejected_user = Approval.rejected_user;
                //lblApproved.Source = Attendance.status_image;                
            }
            
                await detailViewModel.ExecuteAttendanceCommand(Attendance);
          
                lblReceiptDetails.Text = "Time Sheet";
            //detailViewModel.TotalBalanceAmount(Attendance);
            //AttendanceListView.HeightRequest = count * 120;
        }

        private void control_enable(bool enable)
        {
        }

        private void txtBagweight_Focused(object sender, FocusEventArgs e)
        {
            //if (txtBagweight.Text.Trim() != "")
            //{
            //    txtBagweight.Text = string.Format("{0:0}", Convert.ToDouble(txtBagweight.Text));
            //    if (Convert.ToDouble(txtBagweight.Text) == 0)
            //    {
            //        txtBagweight.Text = "";
            //        Attendance.bag_weight = 0;
            //    }
            //}
            //else
            //{
            //    Attendance.bag_weight = 0;
            //}
        }


        private async void LblApproved_Clicked(object sender, EventArgs e)
        {
            Approval = await approvalViewModel.GetApprovalAsync(Attendance.id);
            await Navigation.PushAsync(new AddApprovalPage(Approval));
        }

        private async void Log_Tapped(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new LogPage(Attendance));
            //await Navigation.PushAsync(new LogPage(approval));
        }


        
        private async void lblAddReceipt_Clicked(object sender, EventArgs e)
        {
            
        }
        Timer timer;
        public AttendanceDetail _AttendanceDetail { get; set; }
        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (PopupNavigation.Instance.PopupStack.Count <= 0)
                {
                    timer.Stop();
                    detailViewModel.UpdateAttendanceDetail(Attendance,_AttendanceDetail);
                    //string Since = PatientDiseaseDetails.SincePeriod;
                    //ButSince.Text = Since;
                    //ButTablet.Text = PatientDiseaseDetails.TabletPeriod;
                    //ButInsulin.Text = PatientDiseaseDetails.InsulinPeriod;
                    //BindingContext = this;
                }
            });
        }


        private async void DeleteClicked(object sender, EventArgs e)
        {
        }

    }
}