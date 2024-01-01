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
    public partial class EditPaymentPage : ContentPage
    {
        public DraftConfirmationPayment DraftConfirmationPayment { get; set; }
        
        PaymentViewModel viewModel;
        public DateTime date { get; set; }
        public decimal amount;
        public SearchConfirmationFilter _searchFilter { get; set; }        
        //public Indexes _enquiry { get; set; }
        public EditPaymentPage(DraftConfirmationPayment payment, DraftConfirmationDispatchDelivery draftConfirmationDispatchDelivery)
        {
            InitializeComponent();            
            viewModel = new PaymentViewModel();            
            DraftConfirmationPayment = payment;
            Title = "Payment";
            if (DraftConfirmationPayment.id <= 0)
                DraftConfirmationPayment.payment_date = DateTime.Now.ToLocalTime();
            amount = DraftConfirmationPayment.amount;
            if (DraftConfirmationPayment.send_for_approval == 1)
                butSave.IsVisible = false;
            BindingContext = this;
            if (Convert.ToDouble(txtAmount.Text) == 0)
                txtAmount.Text = "";
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DraftConfirmationPayment.utr_number))
            {
                await DisplayAlert("Alert", "Enter the UTR Number...", "OK");
                return;
            }
            //else if(DraftConfirmationPayment.amount > amount)
            //{
            //    await DisplayAlert("alert", "Amount should not exceed Rs." + amount + "...", "ok");
            //    txtAmount.Focus();
            //    return;
            //}
            else if (DraftConfirmationPayment.amount <= 0)
            {
                await DisplayAlert("alert", "Enter the amount...", "ok");
                txtAmount.Focus();
                return;
            }

            if (DraftConfirmationPayment.amount > DraftConfirmationPayment.invoice_amount)
                DraftConfirmationPayment.excess_amount = DraftConfirmationPayment.amount - DraftConfirmationPayment.invoice_amount;
            else
            {
                DraftConfirmationPayment.invoice_amount = DraftConfirmationPayment.amount;
                DraftConfirmationPayment.excess_amount = (DraftConfirmationPayment.amount - DraftConfirmationPayment.invoice_amount > 0) ? DraftConfirmationPayment.amount - DraftConfirmationPayment.invoice_amount : 0;
            }

            //DraftConfirmationPayment.payment_status = 1;
            DraftConfirmationPayment _Payment = await viewModel.StorePaymentCommand(DraftConfirmationPayment);
            await Task.Delay(200);
            await Navigation.PopAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {        
            await Navigation.PopAsync();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (DraftConfirmationPayment.from_advance_id > 0)
            {
                lblAUTRNo.Text = "Advance Received on " + DraftConfirmationPayment.utr_number;
                txtAmount.IsEnabled = false;
                lblAUTRNo.IsVisible = true;
                lblUTRNo.IsVisible = false;
                txtUTRNo.IsVisible = false;
            }
        }
        
        private void TxtAmount_Focused(object sender, FocusEventArgs e)
        {
            if (txtAmount.Text.Trim() != "")
            {                
                if (Convert.ToDouble(txtAmount.Text) == 0)
                {
                    txtAmount.Text = "";
                    DraftConfirmationPayment.amount = 0;
                }
            }
            else
            {
                DraftConfirmationPayment.amount = 0;
            }
        }
        private async void Delete_Clicked(object sender, EventArgs e)
        {
            if (DraftConfirmationPayment.send_for_approval == 1)
                return;
            var result = await this.DisplayAlert("Attention!", "Do you want to delete this payment?", "Yes", "No");
            if (result)
            {
                try
                {
                    string message = await viewModel.DeleteDraftConfirmationPaymentCommand(DraftConfirmationPayment.id);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    await Navigation.PopAsync();
                }
            }
        }
    }
}