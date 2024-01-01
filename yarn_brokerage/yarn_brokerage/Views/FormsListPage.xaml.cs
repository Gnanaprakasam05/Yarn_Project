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
    public partial class FormsListPage : ContentPage
    {
        public DraftConfirmation DraftConfirmation { get; set; }
        public DraftConfirmationDetails DraftConfirmationDetails { get; set; }

        public DraftConfirmationDispatchDelivery DraftConfirmationDispatchDelivery { get; set; }

        DraftConfirmationFormsViewModel viewModel;

        public DateTime date { get; set; }
        
        public FormsListPage(DraftConfirmation draftConfirmation=null, DraftConfirmationDetails draftConfirmationDetails=null, DispatchConfirm dispatchConfirm= null)
        {
            InitializeComponent();

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
                txtInvoiceNo.Text = draftConfirmationDetails.invoice_number;
                InvoiceDatePicker.Date = draftConfirmationDetails.invoice_date;
                txtAmount.Text = draftConfirmationDetails.invoice_amount.ToString(); 

            }

            if(dispatchConfirm != null)
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

                txtInvoiceNo.Text = dispatchConfirm.invoice_number;
                InvoiceDatePicker.Date = dispatchConfirm.invoice_date;
                txtAmount.Text = dispatchConfirm.invoice_amount.ToString();
            }

            if (DraftConfirmationDispatchDelivery.allow_credit_billing == 1)
                lblAllowCreditBilling.IsVisible = true;
            else
                lblAllowCreditBilling.IsVisible = false;

            BindingContext = viewModel = new DraftConfirmationFormsViewModel(); 
            
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            viewModel.ProgramApprovalCommand(DraftConfirmationDispatchDelivery,0);
            await Navigation.PopAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        async void Revoke_Clicked(object sender, EventArgs e)
        {
            viewModel.ProgramApprovalCommand(DraftConfirmationDispatchDelivery,1);
            await Navigation.PopAsync();
        }
            decimal TotalAmount;
        protected async override void OnAppearing()
        {            
            base.OnAppearing();
            await viewModel.ExecuteDraftConfirmationDetailsCommand(DraftConfirmationDispatchDelivery);
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

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as DraftConfirmationForm;
            // Manually deselect item.
            DraftConfirmationFormListView.SelectedItem = null;
            if (item == null)
                return;
            await Navigation.PushAsync(new AddFormPage(item, DraftConfirmationDispatchDelivery));
        }
    }
}