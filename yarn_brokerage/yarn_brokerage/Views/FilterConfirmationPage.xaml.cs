using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using yarn_brokerage.Models;
using yarn_brokerage.ViewModels;
namespace yarn_brokerage.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class FilterConfirmationPage : ContentPage
    {
        public SearchConfirmationFilter SearchConfirmationFilter { get; set; }
        public SearchConfirmationFilter SearchConfirmationFilterCheck { get; set; }
        public int _transaction_type;
        DraftConfirmationViewModel viewModel;
        int _flag_from_another_moodule;
        public FilterConfirmationPage(SearchConfirmationFilter searchConfirmationFilter, int flag_from_another_moodule=0, SearchConfirmationFilter SearchConfirmationFilter_Check = null)
        {
            InitializeComponent();


            SearchConfirmationFilterCheck = SearchConfirmationFilter_Check;
            //_transaction_type = transaction_type;
            viewModel = new DraftConfirmationViewModel();
            _flag_from_another_moodule = flag_from_another_moodule;
            if (_flag_from_another_moodule == 1)
            {
                Title = "Filter For Confirmation";
            }
            else if (_flag_from_another_moodule == 2)
            {
                Title = "Filter For Confirmations Report";
                lblUser.IsVisible = false;
                txtUsername.IsVisible = false;
                butUserClear.IsVisible = false;
            }
            else
                Title = "Filter For Enquiry";
            if (_flag_from_another_moodule == 2)
            {
                lblConfirmationStatus.IsVisible = true;
                grdConfirmationStatus.IsVisible = true;
                lblGroupBy.IsVisible = true;
                grdGroupBy.IsVisible = true;
            }
            if (searchConfirmationFilter == null)
            {
                SearchConfirmationFilter = new SearchConfirmationFilter();
                SearchConfirmationFilter.transaction_type = 1;
                SearchConfirmationFilter.confirmation_date_from = DateTime.Now.ToLocalTime();
                SearchConfirmationFilter.confirmation_date_to = DateTime.Now.ToLocalTime();
                SearchConfirmationFilter.dispatch_date_from = DateTime.Now.ToLocalTime();
                SearchConfirmationFilter.dispatch_date_to = DateTime.Now.ToLocalTime();
                SearchConfirmationFilter.payment_date_from = DateTime.Now.ToLocalTime();
                SearchConfirmationFilter.payment_date_to = DateTime.Now.ToLocalTime();
            }
            else
            {
                SearchConfirmationFilter = searchConfirmationFilter;
                if (SearchConfirmationFilter.segment == 1) RdoDomestic.IsChecked = true; else if (SearchConfirmationFilter.segment == 2) RdoExport.IsChecked=true;
                if (SearchConfirmationFilter.confirmation_date == 1) { RdoConfirm.IsChecked = true;  Confirmation_Clicked(null,null); }
                if (SearchConfirmationFilter.dispatch_date == 1) { RdoDispatch.IsChecked = true; Dispatch_Clicked(null, null); }
                if (SearchConfirmationFilter.payment_date == 1) { RdoPayment.IsChecked = true; Payment_Clicked(null, null); }
                if (SearchConfirmationFilter.approved == 1) chkApproved.IsChecked = true;
                if (SearchConfirmationFilter.bags_ready == 1) chkBagsReady.IsChecked = true;
                if (SearchConfirmationFilter.payment_ready == 1) chkPaymentReady.IsChecked = true;
                if (SearchConfirmationFilter.payment_received == 1) chkPaymentReceived.IsChecked = true;
                if (SearchConfirmationFilter.transporter_ready == 1) chkTransporterReady.IsChecked = true;
                if (SearchConfirmationFilter.dispatched == 1) chkDispatched.IsChecked = true;
                if (SearchConfirmationFilter.invoiced == 1) chkInvoiced.IsChecked = true;
                if (SearchConfirmationFilter.delivered == 1) chkDelivered.IsChecked = true;
                if (SearchConfirmationFilter.customer_confirmed == 1) chkCustomerConfirmed.IsChecked = true;

                if (SearchConfirmationFilter.not_approved == 1) chkNotApproved.IsChecked = true;
                if (SearchConfirmationFilter.not_bags_ready == 1) chkNotBagsReady.IsChecked = true;
                if (SearchConfirmationFilter.not_payment_ready == 1) chkNotPaymentReady.IsChecked = true;
                if (SearchConfirmationFilter.not_payment_received == 1) chkNotPaymentReceived.IsChecked = true;
                if (SearchConfirmationFilter.not_transporter_ready == 1) chkNotTransporterReady.IsChecked = true;
                if (SearchConfirmationFilter.not_dispatched == 1) chkNotDispatched.IsChecked = true;
                if (SearchConfirmationFilter.not_invoiced == 1) chkNotInvoiced.IsChecked = true;
                if (SearchConfirmationFilter.not_delivered == 1) chkNotDelivered.IsChecked = true;
                if (SearchConfirmationFilter.not_customer_confirmed == 1) chkNotCustomerConfirmed.IsChecked = true;

                if (SearchConfirmationFilter.group_by_customer == 1) chkCustomer.IsChecked = true;
                if (SearchConfirmationFilter.group_by_supplier == 1) chkSupplier.IsChecked = true;
                if (SearchConfirmationFilter.group_by_count == 1) chkCount.IsChecked = true;
                if (SearchConfirmationFilter.group_by_segment == 1) chkSegment.IsChecked = true;
                if (SearchConfirmationFilter.group_by_month == 1) chkMonth.IsChecked = true;
                if (SearchConfirmationFilter.group_by_year == 1) chkYear.IsChecked = true;
                // if (SearchConfirmationFilter.transaction_type == 1) RdoSell.IsChecked = true; else if (SearchConfirmationFilter.transaction_type == 2) RdoBuy.IsChecked = true;
            }
           
            //if (_transaction_type == 1)
            //{
            //    //lblDateTime.Text = "Offer Date Time";
            //    lblSupplier.Text = "Supplier";
            //}
            //else
            //{
            //    //lblDateTime.Text = "Enquiry Date Time";
            //    lblSupplier.Text = "Customer";
            //}
            //Item = new Item
            //{
            //    Text = "Item name",
            //    Description = "This is an item description."
            //};



            BindingContext = this;
          
        }

        //public bool CloseApp = true;
        //protected override bool OnBackButtonPressed()
        //{
        //    Device.BeginInvokeOnMainThread(async () =>
        //    {
        //        //CloseApp = false;
        //        await Navigation.PopToRootAsync();

        //    });
        //    return CloseApp;
        //}

        private async void butFilter_Clicked(object sender, EventArgs e)
        {
            if (_flag_from_another_moodule == 2)
                await Navigation.PopAsync();
            else
            {


                if (SearchConfirmationFilterCheck.Date_Change == true || SearchConfirmationFilter.segment > 0 || !string.IsNullOrEmpty(SearchConfirmationFilter.confirmation_no)
                    || !string.IsNullOrEmpty(SearchConfirmationFilter.customer_name) || !string.IsNullOrEmpty(SearchConfirmationFilter.supplier_name) ||
                    !string.IsNullOrEmpty(SearchConfirmationFilter.count_name) || !string.IsNullOrEmpty(SearchConfirmationFilter.user_name))
                {
                    SearchConfirmationFilterCheck.Search_Edit = true;
                }
                else
                {
                    SearchConfirmationFilterCheck.Search_Edit = false;

                }










                SearchConfirmationFilter.search_flag = 1;
                SearchConfirmationFilter.filter_flag = 1;
                await Navigation.PopModalAsync();
            }
        }
        
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void TxtSupplier_Focused(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LedgersListPage(null, 1,null,null,null,SearchConfirmationFilter));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();            
            txtCustomerName.Text = SearchConfirmationFilter.customer_name;
            txtSupplierName.Text = SearchConfirmationFilter.supplier_name;
            txtCountName.Text = SearchConfirmationFilter.count_name;
            txtUsername.Text = SearchConfirmationFilter.user_name;
            //if (SuggestionList.Age==1) RdoSmall.IsChecked = true; else if (SuggestionList.Age == 2) RdoLittleGrown.IsChecked = true; else if (SuggestionList.Age==3) RdoGrown.IsChecked = true;
            //if (SuggestionList.Severity == 1) RdoMinimum.IsChecked = true; else if (SuggestionList.Severity == 2) RdoMedium.IsChecked=true; else if (SuggestionList.Severity == 3) RdoMaximum.IsChecked=true;
            //if (viewModel.SuggestionListDetails.Count == 0)
            //viewModel.LoadSuggestionListDetailsCommand.Execute(SuggestionList.Id);
        }

        private async void TxtCount_Focused(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CountListPage(null,null,null,SearchConfirmationFilter));
        }

        private async void TxtUsername_Focused(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new UserListPage(null,null ,SearchConfirmationFilter)));
        }

        private async void TxtCustomer_Focused(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LedgersListPage(null, 2, null, null, null,SearchConfirmationFilter));
        }

        private void segment_Clicked(object sender, EventArgs e)
        {
            if (RdoDomestic.IsChecked == true && RdoExport.IsChecked == false)
                SearchConfirmationFilter.segment = 1;
            else if (RdoDomestic.IsChecked == false && RdoExport.IsChecked == true)
                SearchConfirmationFilter.segment = 2;
            else
                SearchConfirmationFilter.segment = 0;
        }

        private void type_Clicked(object sender, EventArgs e)
        {
            //if (RdoSell.IsChecked == true && RdoBuy.IsChecked == false)
            //    SearchConfirmationFilter.transaction_type = 1;
            //else if (RdoSell.IsChecked == false && RdoBuy.IsChecked == true)
            //    SearchConfirmationFilter.transaction_type = 2;
            //else
            //    SearchConfirmationFilter.transaction_type = 0;
        }

        private void txtFDateTime_DateSelected(object sender, DateChangedEventArgs e)
        {
            SearchConfirmationFilter.confirmation_date_from = txtFDateTime.Date;
        }
        private void txtTDateTime_DateSelected(object sender, DateChangedEventArgs e)
        {
            SearchConfirmationFilter.confirmation_date_to = txtTDateTime.Date;
        }
        
        private void txtDFDateTime_DateSelected(object sender, DateChangedEventArgs e)
        {
            SearchConfirmationFilter.dispatch_date_from = txtDFDateTime.Date;
        }
        private void txtDTDateTime_DateSelected(object sender, DateChangedEventArgs e)
        {
            SearchConfirmationFilter.dispatch_date_to = txtDTDateTime.Date;
        }

        private void txtPFDateTime_DateSelected(object sender, DateChangedEventArgs e)
        {
            SearchConfirmationFilter.payment_date_from = txtPFDateTime.Date;
        }
        private void txtPTDateTime_DateSelected(object sender, DateChangedEventArgs e)
        {
            SearchConfirmationFilter.payment_date_to = txtPTDateTime.Date;
        }
        private void ButCustomerClear_Clicked(object sender, EventArgs e)
        {
            txtCustomerName.Text = "";
            SearchConfirmationFilter.customer_id = 0;
            SearchConfirmationFilter.customer_name = "";
        }

        private void ButSupplierClear_Clicked(object sender, EventArgs e)
        {
            txtSupplierName.Text = "";
            SearchConfirmationFilter.supplier_id = 0;
            SearchConfirmationFilter.supplier_name = "";
        }

        private void ButCountClear_Clicked(object sender, EventArgs e)
        {
            txtCountName.Text = "";
            SearchConfirmationFilter.count_id = 0;
            SearchConfirmationFilter.count_name = "";
        }
        private void ButUserClear_Clicked(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            SearchConfirmationFilter.user_id = 0;
            SearchConfirmationFilter.user_name = "";
        }

        private void Confirmation_Clicked(object sender, EventArgs e)
        {
            if (RdoConfirm.IsChecked == true)
            {
                SearchConfirmationFilter.confirmation_date = 1;
                txtFDateTime.IsEnabled = true;
                txtTDateTime.IsEnabled = true;

                SearchConfirmationFilterCheck.Date_Change = true;
            }
            else
            {
                SearchConfirmationFilter.confirmation_date = 0;
                txtFDateTime.IsEnabled = false;
                txtTDateTime.IsEnabled = false;

                SearchConfirmationFilterCheck.Date_Change = false;
            }
        }

        private void Dispatch_Clicked(object sender, EventArgs e)
        {
            if (RdoDispatch.IsChecked == true)
            {
                SearchConfirmationFilter.dispatch_date = 1;
                txtDFDateTime.IsEnabled = true;
                txtDTDateTime.IsEnabled = true;
            }
            else
            {
                SearchConfirmationFilter.dispatch_date = 0;
                txtDFDateTime.IsEnabled = false;
                txtDTDateTime.IsEnabled = false;
            }
        }

        private void Payment_Clicked(object sender, EventArgs e)
        {
            if (RdoPayment.IsChecked == true)
            {
                SearchConfirmationFilter.payment_date = 1;
                txtPFDateTime.IsEnabled = true;
                txtPTDateTime.IsEnabled = true;
            }        
            else
            {
                SearchConfirmationFilter.payment_date = 0;
                txtPFDateTime.IsEnabled = false;
                txtPTDateTime.IsEnabled = false;
            }
        }

        private void chkStatusSearch_Clicked(object sender, EventArgs e)
        {
            if (grdConfirmationStatus.IsVisible == true)
            {
                SearchConfirmationFilter.approved = (chkApproved.IsChecked == true) ? 1 : 0;
                SearchConfirmationFilter.bags_ready = (chkBagsReady.IsChecked == true) ? 1 : 0;
                SearchConfirmationFilter.payment_ready = (chkPaymentReady.IsChecked == true) ? 1 : 0;
                SearchConfirmationFilter.payment_received = (chkPaymentReceived.IsChecked == true) ? 1 : 0;
                SearchConfirmationFilter.transporter_ready = (chkTransporterReady.IsChecked == true) ? 1 : 0;

                SearchConfirmationFilter.dispatched = (chkDispatched.IsChecked == true) ? 1 : 0;
                SearchConfirmationFilter.invoiced = (chkInvoiced.IsChecked == true) ? 1 : 0;
                SearchConfirmationFilter.delivered = (chkDelivered.IsChecked == true) ? 1 : 0;
                SearchConfirmationFilter.customer_confirmed = (chkCustomerConfirmed.IsChecked == true) ? 1 : 0;


                SearchConfirmationFilter.not_approved = (chkNotApproved.IsChecked == true) ? 1 : 0;
                SearchConfirmationFilter.not_bags_ready = (chkNotBagsReady.IsChecked == true) ? 1 : 0;
                SearchConfirmationFilter.not_payment_ready = (chkNotPaymentReady.IsChecked == true) ? 1 : 0;
                SearchConfirmationFilter.not_payment_received = (chkNotPaymentReceived.IsChecked == true) ? 1 : 0;
                SearchConfirmationFilter.not_transporter_ready = (chkNotTransporterReady.IsChecked == true) ? 1 : 0;

                SearchConfirmationFilter.not_dispatched = (chkNotDispatched.IsChecked == true) ? 1 : 0;
                SearchConfirmationFilter.not_invoiced = (chkNotInvoiced.IsChecked == true) ? 1 : 0;
                SearchConfirmationFilter.not_delivered = (chkNotDelivered.IsChecked == true) ? 1 : 0;
                SearchConfirmationFilter.not_customer_confirmed = (chkNotCustomerConfirmed.IsChecked == true) ? 1 : 0;
            }
            if(grdGroupBy.IsVisible == true)
            {
                Plugin.InputKit.Shared.Controls.CheckBox checkBox = sender as Plugin.InputKit.Shared.Controls.CheckBox;
                if (checkBox.Text == "Customer")
                {
                    chkSupplier.IsChecked = false;
                    chkCount.IsChecked = false;
                    chkSegment.IsChecked = false;
                    chkMonth.IsChecked = false;
                    chkYear.IsChecked = false;
                }
                else if (checkBox.Text == "Supplier")
                {
                    chkCustomer.IsChecked = false;
                    chkCount.IsChecked = false;
                    chkSegment.IsChecked = false;
                    chkMonth.IsChecked = false;
                    chkYear.IsChecked = false;
                }
                else if (checkBox.Text == "Count")
                {
                    chkSupplier.IsChecked = false;
                    chkCustomer.IsChecked = false;
                    chkSegment.IsChecked = false;
                    chkMonth.IsChecked = false;
                    chkYear.IsChecked = false;
                }
                else if (checkBox.Text == "Segment")
                {
                    chkSupplier.IsChecked = false;
                    chkCount.IsChecked = false;
                    chkCustomer.IsChecked = false;
                    chkMonth.IsChecked = false;
                    chkYear.IsChecked = false;
                }
                else if (checkBox.Text == "Month")
                {
                    chkSupplier.IsChecked = false;
                    chkCount.IsChecked = false;
                    chkCustomer.IsChecked = false;
                    chkSegment.IsChecked = false;
                    chkYear.IsChecked = false;
                }
                else if (checkBox.Text == "Year")
                {
                    chkSupplier.IsChecked = false;
                    chkCount.IsChecked = false;
                    chkCustomer.IsChecked = false;
                    chkMonth.IsChecked = false;
                    chkSegment.IsChecked = false;
                }
                SearchConfirmationFilter.group_by_customer = (chkCustomer.IsChecked == true) ? 1 : 0;
                SearchConfirmationFilter.group_by_supplier = (chkSupplier.IsChecked == true) ? 1 : 0;
                SearchConfirmationFilter.group_by_count = (chkCount.IsChecked == true) ? 1 : 0;
                SearchConfirmationFilter.group_by_segment = (chkSegment.IsChecked == true) ? 1 : 0;
                SearchConfirmationFilter.group_by_month = (chkMonth.IsChecked == true) ? 1 : 0;
                SearchConfirmationFilter.group_by_year = (chkYear.IsChecked == true) ? 1 : 0;
            }
        }
    }
}