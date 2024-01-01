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

namespace yarn_brokerage.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AddCallHistoryPage : ContentPage
    {
        public CallLogModel CallLogModel { get; set; }
        
        CallLogViewModel viewModel;
        
        public DateTime date { get; set; }
        public SearchFilter _searchFilter { get; set; }
        public Indexes _enquiry { get; set; }
        public Approval Approval { get; set; }
        ApprovalViewModel approvalViewModel;
        public AddCallHistoryPage(CallLogModel _CallLogModel, SearchFilter searchFilter = null, Indexes enquiry = null)
        {
            InitializeComponent();
            
            viewModel = new CallLogViewModel();
            approvalViewModel = new ApprovalViewModel();
            _searchFilter = searchFilter;
            _enquiry = enquiry;            
            if (_CallLogModel == null)
            {
                CallLogModel = new CallLogModel();                            
            }
            else
            {
                CallLogModel = _CallLogModel;     
            }
            Title = "Call Log Remarks";
            BindingContext = this;          
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            viewModel.StoreCallLog(CallLogModel);
            await Navigation.PopAsync();
        }
        

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        double TotalAmount = 0.00;
        protected override void OnAppearing()
        {
            base.OnAppearing();
            txtLedgerName.Text = CallLogModel.LedgerName;
        }
        
      
        private async void TxtLedgerName_Focused(object sender, EventArgs e)
        {
            if (txtLedgerName.IsEnabled == false)
                return;
            await Navigation.PushAsync(new LedgersListPage(null, 0, null, null, null, null, null, null, 0, null,null, CallLogModel));            
        }

        private void ButLedgerClear_Clicked(object sender, EventArgs e)
        {
            txtLedgerName.Text = "";
            //txtCValue.Text = "";
            txtCompanyName.Text = "";         
        }        
    }
}