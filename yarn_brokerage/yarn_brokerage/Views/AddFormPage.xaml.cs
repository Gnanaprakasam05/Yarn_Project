using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using yarn_brokerage.Models;
using yarn_brokerage.ViewModels;
namespace yarn_brokerage.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AddFormPage : ContentPage
    {
        public DraftConfirmationForm DraftConfirmationForm { get; set; }
        
        DraftConfirmationFormsViewModel viewModel;
        public DateTime date { get; set; }
        public decimal amount;
        public SearchConfirmationFilter _searchFilter { get; set; }
        public DraftConfirmationDispatchDelivery _draftConfirmationDispatchDelivery { get; set; }
        //public Indexes _enquiry { get; set; }
        public AddFormPage(DraftConfirmationForm form, DraftConfirmationDispatchDelivery draftConfirmationDispatchDelivery)
        {
            InitializeComponent();            
            viewModel = new DraftConfirmationFormsViewModel();            
            DraftConfirmationForm = form;
            _draftConfirmationDispatchDelivery = draftConfirmationDispatchDelivery;
            Title = "Receive Form";
            if (DraftConfirmationForm.received == 1) chkReceived.IsChecked = true;ChkReceived_Clicked(null, null);
            if (DraftConfirmationForm.issued == 1) chkIssued.IsChecked = true; ChkIssued_Clicked(null, null);
            //if (DraftConfirmationForm.id <= 0)
            //    DraftConfirmationForm.received_date = DateTime.Now.ToLocalTime();
            //amount = DraftConfirmationForm.amount;
            //if (DraftConfirmationForm.send_for_approval == 1)
            //    butSave.IsVisible = false;
            BindingContext = this;
            //if (Convert.ToDouble(txtAmount.Text) == 0)
            //    txtAmount.Text = "";
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            //DraftConfirmationForm.form_status = 1;
            DraftConfirmationForm.remarks = (!string.IsNullOrWhiteSpace(txtRemarks.Text)) ? txtRemarks.Text : "";
            DraftConfirmationForm _Form = await viewModel.StoreForm(DraftConfirmationForm);
            await Task.Delay(200);
            await Navigation.PopAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {        
            await Navigation.PopAsync();
        }
        decimal AdvanceAmount;
        protected override void OnAppearing()
        {
            base.OnAppearing();            
        }

        private void ChkReceived_Clicked(object sender, EventArgs e)
        {
            if (chkReceived.IsChecked == true)
                DraftConfirmationForm.received = 1;
            else
                DraftConfirmationForm.received = 0;
        }

        private void ChkIssued_Clicked(object sender, EventArgs e)
        {
            if (chkIssued.IsChecked == true)
                DraftConfirmationForm.issued = 1;
            else
                DraftConfirmationForm.issued = 0;
        }
    }
}