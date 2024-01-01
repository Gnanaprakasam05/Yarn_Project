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
    public partial class FilterEnquiryPage : ContentPage
    {
        public SearchFilter SearchFilter { get; set; }
        public int _transaction_type;
        EnquiriesViewModel viewModel;
        public FilterEnquiryPage(SearchFilter searchFilter, int flag_from_another_moodule=0)
        {
            InitializeComponent();
            //_transaction_type = transaction_type;
            viewModel = new EnquiriesViewModel();
            if (flag_from_another_moodule == 1)
            {
                Title = "Filter For Confirmation";
                lblEnquiryType.IsVisible = false;
                grdEnquiryType.IsVisible = false;
            }
            else
                Title = "Filter For Enquiry";
            if (searchFilter == null)
            {                
                SearchFilter.transaction_date_time = DateTime.Now.ToLocalTime();
                SearchFilter.transaction_type = 1;
            }
            else
            {
                SearchFilter = searchFilter;
                if (SearchFilter.segment == 1) RdoDomestic.IsChecked = true; else if (SearchFilter.segment == 2) RdoExport.IsChecked=true;
                if (SearchFilter.transaction_type == 1) RdoSell.IsChecked = true; else if (SearchFilter.transaction_type == 2) RdoBuy.IsChecked = true;
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
            SearchFilter.search_flag = 1;
            SearchFilter.filter_flag = 1;
            await Navigation.PopModalAsync();
        }
        
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void TxtSupplier_Focused(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LedgersListPage(null, 1,null,null,SearchFilter));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();            
            txtCustomerName.Text = SearchFilter.customer_name;
            txtSupplierName.Text = SearchFilter.supplier_name;
            txtCountName.Text = SearchFilter.count_name;
            txtUsername.Text = SearchFilter.user_name;
            //if (SuggestionList.Age==1) RdoSmall.IsChecked = true; else if (SuggestionList.Age == 2) RdoLittleGrown.IsChecked = true; else if (SuggestionList.Age==3) RdoGrown.IsChecked = true;
            //if (SuggestionList.Severity == 1) RdoMinimum.IsChecked = true; else if (SuggestionList.Severity == 2) RdoMedium.IsChecked=true; else if (SuggestionList.Severity == 3) RdoMaximum.IsChecked=true;
            //if (viewModel.SuggestionListDetails.Count == 0)
            //viewModel.LoadSuggestionListDetailsCommand.Execute(SuggestionList.Id);
        }

        private async void TxtCount_Focused(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CountListPage(null,null,SearchFilter));
        }

        private async void TxtUsername_Focused(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new UserListPage(null, SearchFilter)));
        }

        private async void TxtCustomer_Focused(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LedgersListPage(null, 2, null, null, SearchFilter));
        }

        private void segment_Clicked(object sender, EventArgs e)
        {
            if (RdoDomestic.IsChecked == true && RdoExport.IsChecked == false)
                SearchFilter.segment = 1;
            else if (RdoDomestic.IsChecked == false && RdoExport.IsChecked == true)
                SearchFilter.segment = 2;
            else
                SearchFilter.segment = 0;
        }

        private void type_Clicked(object sender, EventArgs e)
        {
            if (RdoSell.IsChecked == true && RdoBuy.IsChecked == false)
                SearchFilter.transaction_type = 1;
            else if (RdoSell.IsChecked == false && RdoBuy.IsChecked == true)
                SearchFilter.transaction_type = 2;
            else
                SearchFilter.transaction_type = 0;
        }

        private void txtDateTime_DateSelected(object sender, DateChangedEventArgs e)
        {
            SearchFilter.transaction_date_time = txtDateTime.Date;
        }

        private void ButCustomerClear_Clicked(object sender, EventArgs e)
        {
            txtCustomerName.Text = "";
            SearchFilter.customer_id = 0;
            SearchFilter.customer_name = "";
        }

        private void ButSupplierClear_Clicked(object sender, EventArgs e)
        {
            txtSupplierName.Text = "";
            SearchFilter.supplier_id = 0;
            SearchFilter.supplier_name = "";
        }

        private void ButCountClear_Clicked(object sender, EventArgs e)
        {
            txtCountName.Text = "";
            SearchFilter.count_id = 0;
            SearchFilter.count_name = "";
        }
        private void ButUserClear_Clicked(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            SearchFilter.user_id = 0;
            SearchFilter.user_name = "";
        }
    }
}