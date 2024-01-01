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
    public partial class FilterApprovalPage : ContentPage
    {
        public SearchApprovalFilter SearchApprovalFilter { get; set; }
        public SearchApprovalFilter SearchApprovalFilterCheck { get; set; }
        public int _transaction_type;
        DraftConfirmationViewModel viewModel;
        public FilterApprovalPage(SearchApprovalFilter searchApprovalFilter, SearchApprovalFilter SearchApprovalFilter_Check = null)
        {
            InitializeComponent();
            //_transaction_type = transaction_type;
            viewModel = new DraftConfirmationViewModel();
            Title = "Filter For Approval";

            SearchApprovalFilterCheck = SearchApprovalFilter_Check;
            if (searchApprovalFilter == null)
            {   
                SearchApprovalFilter.transaction_type = 1;
                SearchApprovalFilter.confirmation_date_from = DateTime.Now.ToLocalTime();
                SearchApprovalFilter.confirmation_date_to = DateTime.Now.ToLocalTime();
                SearchApprovalFilter.approved_date_from = DateTime.Now.ToLocalTime();
                SearchApprovalFilter.approved_date_to = DateTime.Now.ToLocalTime();                
            }
            else
            {
                SearchApprovalFilter = searchApprovalFilter;
                //if (SearchApprovalFilter.segment == 1) RdoDomestic.IsChecked = true; else if (SearchApprovalFilter.segment == 2) RdoExport.IsChecked=true;
                if (SearchApprovalFilter.confirmation_date == 1) { RdoConfirm.IsChecked = true; Confirmation_Clicked(null, null); }
                //if (SearchApprovalFilter.approved_date == 1) { RdoDispatch.IsChecked = true; Dispatch_Clicked(null, null); }
                    // if (SearchApprovalFilter.transaction_type == 1) RdoSell.IsChecked = true; else if (SearchApprovalFilter.transaction_type == 2) RdoBuy.IsChecked = true;
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

        private async void butFilter_Clicked(object sender, EventArgs e)
        {

            if (SearchApprovalFilter.Date_Change == true || !string.IsNullOrEmpty(SearchApprovalFilter.customer_name) || !string.IsNullOrEmpty(SearchApprovalFilter.supplier_name) ||
                !string.IsNullOrEmpty(SearchApprovalFilter.count_name) || !string.IsNullOrEmpty(SearchApprovalFilter.user_name))
            {
                SearchApprovalFilterCheck.Search_Edit = true;
            }
            else
            {
                SearchApprovalFilterCheck.Search_Edit = false;

            }

            SearchApprovalFilter.search_flag = 1;
            SearchApprovalFilter.filter_flag = 1;
            await Navigation.PopModalAsync();
        }
        
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void TxtSupplier_Focused(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LedgersListPage(null, 1,null,null,null,null,SearchApprovalFilter));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();            
            txtCustomerName.Text = SearchApprovalFilter.customer_name;
            txtSupplierName.Text = SearchApprovalFilter.supplier_name;
            txtCountName.Text = SearchApprovalFilter.count_name;
            txtUsername.Text = SearchApprovalFilter.user_name;
            //if (SuggestionList.Age==1) RdoSmall.IsChecked = true; else if (SuggestionList.Age == 2) RdoLittleGrown.IsChecked = true; else if (SuggestionList.Age==3) RdoGrown.IsChecked = true;
            //if (SuggestionList.Severity == 1) RdoMinimum.IsChecked = true; else if (SuggestionList.Severity == 2) RdoMedium.IsChecked=true; else if (SuggestionList.Severity == 3) RdoMaximum.IsChecked=true;
            //if (viewModel.SuggestionListDetails.Count == 0)
            //viewModel.LoadSuggestionListDetailsCommand.Execute(SuggestionList.Id);
        }

        private async void TxtCount_Focused(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CountListPage(null,null,null,null,SearchApprovalFilter));
        }

        private async void TxtUsername_Focused(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new UserListPage(null,null, null,SearchApprovalFilter)));
        }

        private async void TxtCustomer_Focused(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LedgersListPage(null, 2, null, null, null, null,SearchApprovalFilter));
        }

        //private void segment_Clicked(object sender, EventArgs e)
        //{
        //    if (RdoDomestic.IsChecked == true && RdoExport.IsChecked == false)
        //        SearchApprovalFilter.segment = 1;
        //    else if (RdoDomestic.IsChecked == false && RdoExport.IsChecked == true)
        //        SearchApprovalFilter.segment = 2;
        //    else
        //        SearchApprovalFilter.segment = 0;
        //}

        private void type_Clicked(object sender, EventArgs e)
        {
            //if (RdoSell.IsChecked == true && RdoBuy.IsChecked == false)
            //    SearchApprovalFilter.transaction_type = 1;
            //else if (RdoSell.IsChecked == false && RdoBuy.IsChecked == true)
            //    SearchApprovalFilter.transaction_type = 2;
            //else
            //    SearchApprovalFilter.transaction_type = 0;
        }

        private void txtFDateTime_DateSelected(object sender, DateChangedEventArgs e)
        {
            SearchApprovalFilter.confirmation_date_from = txtFDateTime.Date;
        }
        private void txtTDateTime_DateSelected(object sender, DateChangedEventArgs e)
        {
            SearchApprovalFilter.confirmation_date_to = txtTDateTime.Date;
        }
        
        private void txtDFDateTime_DateSelected(object sender, DateChangedEventArgs e)
        {
            //SearchApprovalFilter.approved_date_from = txtDFDateTime.Date;
        }
        private void txtDTDateTime_DateSelected(object sender, DateChangedEventArgs e)
        {
            //SearchApprovalFilter.approved_date_to = txtDTDateTime.Date;
        }
              
        private void ButCustomerClear_Clicked(object sender, EventArgs e)
        {
            txtCustomerName.Text = "";
            SearchApprovalFilter.customer_id = 0;
            SearchApprovalFilter.customer_name = "";
        }

        private void ButSupplierClear_Clicked(object sender, EventArgs e)
        {
            txtSupplierName.Text = "";
            SearchApprovalFilter.supplier_id = 0;
            SearchApprovalFilter.supplier_name = "";
        }

        private void ButCountClear_Clicked(object sender, EventArgs e)
        {
            txtCountName.Text = "";
            SearchApprovalFilter.count_id = 0;
            SearchApprovalFilter.count_name = "";
        }
        private void ButUserClear_Clicked(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            SearchApprovalFilter.user_id = 0;
            SearchApprovalFilter.user_name = "";
        }

        private void Confirmation_Clicked(object sender, EventArgs e)
        {
            if (RdoConfirm.IsChecked == true)
            {
                SearchApprovalFilter.confirmation_date = 1;
                txtFDateTime.IsEnabled = true;
                txtTDateTime.IsEnabled = true;

                SearchApprovalFilter.Date_Change = true;
            }
            else
            {
                SearchApprovalFilter.confirmation_date = 0;
                txtFDateTime.IsEnabled = false;
                txtTDateTime.IsEnabled = false;

                SearchApprovalFilter.Date_Change = false;
            }
        }

        private void Dispatch_Clicked(object sender, EventArgs e)
        {
            //if (RdoDispatch.IsChecked == true)
            //{
            //    SearchApprovalFilter.approved_date = 1;
            //    txtDFDateTime.IsEnabled = true;
            //    txtDTDateTime.IsEnabled = true;
            //}
            //else
            //{
            //    SearchApprovalFilter.approved_date = 0;
            //    txtDFDateTime.IsEnabled = false;
            //    txtDTDateTime.IsEnabled = false;
            //}        
        }                
    }
}