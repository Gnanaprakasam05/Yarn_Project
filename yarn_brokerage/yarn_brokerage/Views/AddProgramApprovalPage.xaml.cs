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
    public partial class AddProgramApprovalPage : ContentPage
    {
        public DraftConfirmation DraftConfirmation { get; set; }
        public DraftConfirmation DraftConfirmationCheck { get; set; }
        public DraftConfirmationDetails DraftConfirmationDetails { get; set; }

        public DraftConfirmationDispatchDelivery DraftConfirmationDispatchDelivery { get; set; }

        DraftConfirmationDispatchDeliveryDetailViewModel viewModel;
        public DateTime date { get; set; }

        public AddProgramApprovalPage(DraftConfirmation draftConfirmation = null, DraftConfirmationDetails draftConfirmationDetails = null, DispatchConfirm dispatchConfirm = null, DraftConfirmation DraftConfirmation_Check = null)
        {
            InitializeComponent();

            if (Application.Current.Properties["transaction_program_approval_ApproveAllowed"].ToString() == "0")
                butRevoke.IsVisible = false;


            DraftConfirmationCheck = DraftConfirmation_Check;
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

            if (DraftConfirmationDispatchDelivery.allow_credit_billing == 1)
                lblAllowCreditBilling.IsVisible = true;
            else
                lblAllowCreditBilling.IsVisible = false;

            BindingContext = viewModel = new DraftConfirmationDispatchDeliveryDetailViewModel();

        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            var ApprovalId = await viewModel.ProgramApprovalCommand(DraftConfirmationDispatchDelivery, 0);
            if (ApprovalId != 0)
            {
                DraftConfirmationCheck.Add_Flag = true;
                DraftConfirmationCheck.id = ApprovalId;
            }

            DraftConfirmationCheck.Cancel_Click = 0;
            await Navigation.PopAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            DraftConfirmationCheck.Cancel_Click = 1;
            await Navigation.PopAsync();
        }
        async void Revoke_Clicked(object sender, EventArgs e)
        {
            var ApprovalId = await viewModel.ProgramApprovalCommand(DraftConfirmationDispatchDelivery, 1);
            if (ApprovalId != 0)
            {
                DraftConfirmationCheck.Add_Flag = true;
                DraftConfirmationCheck.id = ApprovalId;
            }
            DraftConfirmationCheck.Cancel_Click = 0;
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
    }
}