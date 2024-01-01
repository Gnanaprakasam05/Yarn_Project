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
    public partial class SendToProgramApprovalPage : ContentPage
    {
        public DraftConfirmation DraftConfirmation { get; set; }
        public DraftConfirmation DraftConfirmation_Checking { get; set; }
        public DraftConfirmationDetails DraftConfirmationDetails { get; set; }

        public DraftConfirmationDispatchDelivery DraftConfirmationDispatchDelivery { get; set; }

        DraftConfirmationDispatchDeliveryDetailViewModel viewModel;
        public DateTime date { get; set; }

        public SendToProgramApprovalPage(DraftConfirmation draftConfirmation = null, DraftConfirmationDetails draftConfirmationDetails = null, DispatchConfirm dispatchConfirm = null, DraftConfirmation DraftConfirmation_Check = null)
        {
            InitializeComponent();


            if (draftConfirmation == null)
            {
                DraftConfirmation = new DraftConfirmation();
            }

            DraftConfirmation_Checking = DraftConfirmation_Check;

            DraftConfirmationDispatchDelivery = new DraftConfirmationDispatchDelivery();

            if (draftConfirmation != null)
            {
                lblTransactionDetail.Text = draftConfirmation.transaction_detail;
                lblCustomerName.Text = draftConfirmation.customer_name;
                lblSupplierName.Text = draftConfirmation.supplier_name;
                lblCountName.Text = draftConfirmation.count_name;
                lblPricePer.Text = "Rs." + draftConfirmation.price_per;
                lblUserName.Text = draftConfirmation.user_name;
                if (draftConfirmation.admin_user == false)
                {
                    lblUserName.IsVisible = false;
                    imgUser.IsVisible = false;
                }
                DraftConfirmationDispatchDelivery.draft_confirmation_id = draftConfirmation.id;
            }
            DraftConfirmationDetails = draftConfirmationDetails;
            if (draftConfirmationDetails != null)
            {
                DraftConfirmationDispatchDelivery.id = draftConfirmationDetails.id;
                DraftConfirmationDispatchDelivery.allow_credit_billing = draftConfirmationDetails.allow_credit_billing;
                lblQtyUnit.Text = draftConfirmationDetails.qty_unit;


            }

            if (dispatchConfirm != null)
            {
                DraftConfirmation.confirmation_no = dispatchConfirm.confirmation_no;
                DraftConfirmation.id = dispatchConfirm.Id;
                DraftConfirmation.count_name = dispatchConfirm.count_name;
                DraftConfirmation.supplier_name = dispatchConfirm.supplier_name;
                DraftConfirmation.customer_name = dispatchConfirm.customer_name;
                DraftConfirmation.qty_unit = dispatchConfirm.qty_unit;
                DraftConfirmation.price = dispatchConfirm.price;
                DraftConfirmation.invoice_value = Convert.ToDecimal(dispatchConfirm.invoice_value);
                DraftConfirmation.CustomerSMS = dispatchConfirm.CustomerWhatsAppGroup;

                lblTransactionDetail.Text = dispatchConfirm.transaction_detail;

                DraftConfirmationDispatchDelivery.id = dispatchConfirm.Id;
                DraftConfirmationDispatchDelivery.draft_confirmation_id = dispatchConfirm.draft_confirmation_id;
                DraftConfirmationDispatchDelivery.allow_credit_billing = dispatchConfirm.allow_credit_billing;
                lblCustomerName.Text = dispatchConfirm.customer_name;
                lblSupplierName.Text = dispatchConfirm.supplier_name;
                lblCountName.Text = dispatchConfirm.count_name;
                lblPricePer.Text = "Rs." + dispatchConfirm.price_per;
                lblUserName.Text = dispatchConfirm.user_name;
                if (dispatchConfirm.admin_user == false)
                {
                    lblUserName.IsVisible = false;
                    imgUser.IsVisible = false;
                }

                lblQtyUnit.Text = dispatchConfirm.qty_unit;
            }
            if (DraftConfirmationDispatchDelivery.allow_credit_billing == 1) { chkAllowCreditBilling.IsChecked = true; ChkAllowCreditBilling_Clicked(null, null); }
            BindingContext = viewModel = new DraftConfirmationDispatchDeliveryDetailViewModel();

        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            var ApprovalId = await viewModel.SendToProgramApprovalCommand(DraftConfirmationDispatchDelivery);

            if (ApprovalId != 0)
            {
                DraftConfirmation_Checking.Add_Flag = true;
                DraftConfirmation_Checking.id = ApprovalId;
            }
            DraftConfirmation_Checking.Cancel_Click = 0;
            await Navigation.PopAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            DraftConfirmation_Checking.Cancel_Click = 1;
            await Navigation.PopAsync();
        }
        decimal TotalAmount;
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }



        private async void Delete_Clicked(object sender, EventArgs e)
        {
            var result = await this.DisplayAlert("Attention!", "Do you want to delete this dispatch?", "Yes", "No");
            if (result)
            {
                try
                {
                    string message = await viewModel.DeleteDraftConfirmationDetailsCommand(DraftConfirmationDetails.id);
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

        private async void Amend_Clicked(object sender, EventArgs e)
        {
            DraftConfirmation draftConfirmation = await viewModel.getDraftConfirmation(DraftConfirmationDispatchDelivery.draft_confirmation_id);
            //DraftConfirmationDetails draftConfirmationDetails = await viewModel.GetDraftConfirmationDetails(DraftConfirmationDispatchDelivery.id);
            //await Navigation.PushAsync(new EditDraftConfirmationPage(draftConfirmation, null, null,1));
            string message = await viewModel.rejectDraftConfirmation(draftConfirmation.id, DraftConfirmationDispatchDelivery.id);
            if (message == "sucess")
            {
                await this.DisplayAlert("Attention!", "Confirmation Moved to Draft", "Ok");
                await Navigation.PopAsync();
            }
        }
        async void SendMessage_Clicked(object sender, EventArgs e)
        {


            if (DraftConfirmation.CustomerSMS != null)
            {
                await viewModel.SendMessageCommand(DraftConfirmation);
                DraftConfirmation_Checking.Cancel_Click = 1;
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Alert!", "Customer WhatsApp No Group is Not Available.", "Ok");
            }
        }
        private void ChkAllowCreditBilling_Clicked(object sender, EventArgs e)
        {
            if (chkAllowCreditBilling.IsEnabled == false)
                return;
            if (chkAllowCreditBilling.IsChecked == true)
                DraftConfirmationDispatchDelivery.allow_credit_billing = 1;
            else
                DraftConfirmationDispatchDelivery.allow_credit_billing = 0;
        }
    }
}