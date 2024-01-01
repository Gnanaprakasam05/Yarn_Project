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
    public partial class FilterDispatchConfirmationPage : ContentPage
    {
        public SearchDispatchConfirmationFilter SearchDispatchConfirmationFilter { get; set; }
        public SearchDispatchConfirmationFilter SearchDispatchConfirmationFilterCheck { get; set; }
        public int _transaction_type;
        DraftConfirmationViewModel viewModel;

        public int Todays_Plan { get; set; }
        public FilterDispatchConfirmationPage(SearchDispatchConfirmationFilter searchDispatchConfirmationFilter, int TodaysPlan, SearchDispatchConfirmationFilter SearchDispatchConfirmationFilter_Ckeck = null)
        {
            InitializeComponent();

            Todays_Plan = TodaysPlan;

            if (TodaysPlan == 0)
            {
                TeamVisible.IsVisible = true;
                TeamEntryVisible.IsVisible = true;
                butTeamNameClear.IsVisible = true;

                TeamGroupVisible.IsVisible = true;
                TeamGroupEntryVisible.IsVisible = true;
                butTeamGroupNameClear.IsVisible = true;

            }

            SearchDispatchConfirmationFilterCheck = SearchDispatchConfirmationFilter_Ckeck;
            viewModel = new DraftConfirmationViewModel();
            Title = "Filter For Dispatch Confirmation";
            if (TodaysPlan == 6 || TodaysPlan == 7)
                grdDispatchedDate.IsVisible = true;
            if (TodaysPlan == 12)
                RdoForms.IsVisible = true;
            if (searchDispatchConfirmationFilter == null)
            {
                SearchDispatchConfirmationFilter.transaction_type = 1;
                SearchDispatchConfirmationFilter.confirmation_date_from = DateTime.Now.ToLocalTime();
                SearchDispatchConfirmationFilter.confirmation_date_to = DateTime.Now.ToLocalTime();
                SearchDispatchConfirmationFilter.approved_date_from = DateTime.Now.ToLocalTime();
                SearchDispatchConfirmationFilter.approved_date_to = DateTime.Now.ToLocalTime();
                SearchDispatchConfirmationFilter.dispatched_date_from = DateTime.Now.ToLocalTime();
                SearchDispatchConfirmationFilter.dispatched_date_to = DateTime.Now.ToLocalTime();
            }
            else
            {
                SearchDispatchConfirmationFilter = searchDispatchConfirmationFilter;
                //if (SearchDispatchConfirmationFilter.segment == 1) RdoDomestic.IsChecked = true; else if (SearchDispatchConfirmationFilter.segment == 2) RdoExport.IsChecked=true;
                if (SearchDispatchConfirmationFilter.confirmation_date == 1) { RdoConfirm.IsChecked = true; Confirmation_Clicked(null, null); }
                if (SearchDispatchConfirmationFilter.approved_date == 1) { RdoDispatch.IsChecked = true; Dispatch_Clicked(null, null); }
                if (SearchDispatchConfirmationFilter.dispatched_date == 1) { RdoDispatched.IsChecked = true; Dispatched_Clicked(null, null); }
                if (SearchDispatchConfirmationFilter.include_received_form == 1) { RdoForms.IsChecked = true; Forms_Clicked(null, null); }
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
            if (SearchDispatchConfirmationFilterCheck.DispatchDate_Change == true || SearchDispatchConfirmationFilterCheck.ConfirmationDate_Change == true || SearchDispatchConfirmationFilterCheck.Dispatched_Change == true || !string.IsNullOrEmpty(SearchDispatchConfirmationFilter.confirmation_no) || !string.IsNullOrEmpty(SearchDispatchConfirmationFilter.customer_name) || !string.IsNullOrEmpty(SearchDispatchConfirmationFilter.supplier_name) ||
               !string.IsNullOrEmpty(SearchDispatchConfirmationFilter.count_name) || !string.IsNullOrEmpty(SearchDispatchConfirmationFilter.user_name) || !string.IsNullOrEmpty(SearchDispatchConfirmationFilter.TeamName) || !string.IsNullOrEmpty(SearchDispatchConfirmationFilter.TeamGroupName))
            {
                SearchDispatchConfirmationFilterCheck.Search_Edit = true;

                if (Todays_Plan == 1 || Todays_Plan == 0 || Todays_Plan == 8 || Todays_Plan == 2 || Todays_Plan == 3 || Todays_Plan == 4 || Todays_Plan == 5 || Todays_Plan == 10 || Todays_Plan == 6 || Todays_Plan == 11 || Todays_Plan == 7 || Todays_Plan == 9 || Todays_Plan == 14)
                {
                    SearchDispatchConfirmationFilterCheck.filter_check = 1;
                }
            }
            else
            {
                SearchDispatchConfirmationFilterCheck.Search_Edit = false;


                if (Todays_Plan == 1 || Todays_Plan == 0 || Todays_Plan == 8 || Todays_Plan == 2 || Todays_Plan == 3 || Todays_Plan == 4 || Todays_Plan == 5 || Todays_Plan == 10 || Todays_Plan == 6 || Todays_Plan == 11 || Todays_Plan == 7 || Todays_Plan == 9 || Todays_Plan == 14)
                {
                    SearchDispatchConfirmationFilterCheck.filter_check = 0;
                }

            }


            SearchDispatchConfirmationFilter.search_flag = 1;
            SearchDispatchConfirmationFilter.filter_flag = 1;
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void TxtSupplier_Focused(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LedgersListPage(null, 1, null, null, null, null, null, SearchDispatchConfirmationFilter));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            txtCustomerName.Text = SearchDispatchConfirmationFilter.customer_name;
            txtSupplierName.Text = SearchDispatchConfirmationFilter.supplier_name;
            txtCountName.Text = SearchDispatchConfirmationFilter.count_name;
            txtUsername.Text = SearchDispatchConfirmationFilter.user_name;
            txtTeamName.Text = SearchDispatchConfirmationFilter.TeamName;
            txtTeamGroupName.Text = SearchDispatchConfirmationFilter.TeamGroupName;





            //if (SuggestionList.Age==1) RdoSmall.IsChecked = true; else if (SuggestionList.Age == 2) RdoLittleGrown.IsChecked = true; else if (SuggestionList.Age==3) RdoGrown.IsChecked = true;
            //if (SuggestionList.Severity == 1) RdoMinimum.IsChecked = true; else if (SuggestionList.Severity == 2) RdoMedium.IsChecked=true; else if (SuggestionList.Severity == 3) RdoMaximum.IsChecked=true;
            //if (viewModel.SuggestionListDetails.Count == 0)
            //viewModel.LoadSuggestionListDetailsCommand.Execute(SuggestionList.Id);
        }

        private void ButTeamNameClear_Clicked(object sender, EventArgs e)
        {
            txtTeamName.Text = null;
            SearchDispatchConfirmationFilter.TeamId = 0;
            SearchDispatchConfirmationFilter.TeamName = null;
        }

        private void ButTeamGroupNameClear_Clicked(object sender, EventArgs e)
        {
            txtTeamGroupName.Text = null;
            SearchDispatchConfirmationFilter.TeamGroupId = 0;
            SearchDispatchConfirmationFilter.TeamGroupName = null;
        }
        private async void TeamName_Focused(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LedgerTeamNamePage(0, null, SearchDispatchConfirmationFilter));
        }

        private async void TeamGroupName_Focused(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LedgerTeamNamePage(3, null, SearchDispatchConfirmationFilter));
        }
        private async void TxtCount_Focused(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CountListPage(null, null, null, null, null, SearchDispatchConfirmationFilter));
        }

        private async void TxtUsername_Focused(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new UserListPage(null, null, null, null, SearchDispatchConfirmationFilter)));
        }

        private async void TxtCustomer_Focused(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LedgersListPage(null, 2, null, null, null, null, null, SearchDispatchConfirmationFilter));
        }

        //private void segment_Clicked(object sender, EventArgs e)
        //{
        //    if (RdoDomestic.IsChecked == true && RdoExport.IsChecked == false)
        //        SearchDispatchConfirmationFilter.segment = 1;
        //    else if (RdoDomestic.IsChecked == false && RdoExport.IsChecked == true)
        //        SearchDispatchConfirmationFilter.segment = 2;
        //    else
        //        SearchDispatchConfirmationFilter.segment = 0;
        //}

        private void type_Clicked(object sender, EventArgs e)
        {
            //if (RdoSell.IsChecked == true && RdoBuy.IsChecked == false)
            //    SearchDispatchConfirmationFilter.transaction_type = 1;
            //else if (RdoSell.IsChecked == false && RdoBuy.IsChecked == true)
            //    SearchDispatchConfirmationFilter.transaction_type = 2;
            //else
            //    SearchDispatchConfirmationFilter.transaction_type = 0;
        }

        private void txtFDateTime_DateSelected(object sender, DateChangedEventArgs e)
        {
            //SearchDispatchConfirmationFilter.confirmation_date_from = txtFDateTime.Date;
        }
        private void txtTDateTime_DateSelected(object sender, DateChangedEventArgs e)
        {
            //SearchDispatchConfirmationFilter.confirmation_date_to = txtTDateTime.Date;
        }

        private void txtDFDateTime_DateSelected(object sender, DateChangedEventArgs e)
        {
            SearchDispatchConfirmationFilter.approved_date_from = txtDFDateTime.Date;
        }
        private void txtDTDateTime_DateSelected(object sender, DateChangedEventArgs e)
        {
            SearchDispatchConfirmationFilter.approved_date_to = txtDTDateTime.Date;
        }

        private void ButCustomerClear_Clicked(object sender, EventArgs e)
        {
            txtCustomerName.Text = "";
            SearchDispatchConfirmationFilter.customer_id = 0;
            SearchDispatchConfirmationFilter.customer_name = "";
        }

        private void ButSupplierClear_Clicked(object sender, EventArgs e)
        {
            txtSupplierName.Text = "";
            SearchDispatchConfirmationFilter.supplier_id = 0;
            SearchDispatchConfirmationFilter.supplier_name = "";
        }

        private void ButCountClear_Clicked(object sender, EventArgs e)
        {
            txtCountName.Text = "";
            SearchDispatchConfirmationFilter.count_id = 0;
            SearchDispatchConfirmationFilter.count_name = "";
        }
        private void ButUserClear_Clicked(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            SearchDispatchConfirmationFilter.user_id = 0;
            SearchDispatchConfirmationFilter.user_name = "";
        }

        private void Confirmation_Clicked(object sender, EventArgs e)
        {
            if (RdoConfirm.IsChecked == true)
            {
                SearchDispatchConfirmationFilter.confirmation_date = 1;
                txtFDateTime.IsEnabled = true;
                txtTDateTime.IsEnabled = true;

                SearchDispatchConfirmationFilterCheck.ConfirmationDate_Change = true;
            }
            else
            {
                SearchDispatchConfirmationFilter.confirmation_date = 0;
                txtFDateTime.IsEnabled = false;
                txtTDateTime.IsEnabled = false;

                SearchDispatchConfirmationFilterCheck.ConfirmationDate_Change = false;
            }
        }

        private void Dispatch_Clicked(object sender, EventArgs e)
        {
            if (RdoDispatch.IsChecked == true)
            {
                SearchDispatchConfirmationFilter.approved_date = 1;
                txtDFDateTime.IsEnabled = true;
                txtDTDateTime.IsEnabled = true;

                SearchDispatchConfirmationFilterCheck.DispatchDate_Change = true;
            }
            else
            {
                SearchDispatchConfirmationFilter.approved_date = 0;
                txtDFDateTime.IsEnabled = false;
                txtDTDateTime.IsEnabled = false;

                SearchDispatchConfirmationFilterCheck.DispatchDate_Change = false;
            }
        }

        private void Dispatched_Clicked(object sender, EventArgs e)
        {
            if (RdoDispatched.IsChecked == true)
            {
                SearchDispatchConfirmationFilter.dispatched_date = 1;
                txtAFDateTime.IsEnabled = true;
                txtATDateTime.IsEnabled = true;

                SearchDispatchConfirmationFilterCheck.Dispatched_Change = true;
            }
            else
            {
                SearchDispatchConfirmationFilter.dispatched_date = 0;
                txtAFDateTime.IsEnabled = false;
                txtATDateTime.IsEnabled = false;

                SearchDispatchConfirmationFilterCheck.Dispatched_Change = false;
            }
        }

        private void txtAFDateTime_DateSelected(object sender, DateChangedEventArgs e)
        {
            SearchDispatchConfirmationFilter.dispatched_date_from = txtAFDateTime.Date;
        }

        private void txtATDateTime_DateSelected(object sender, DateChangedEventArgs e)
        {
            SearchDispatchConfirmationFilter.dispatched_date_to = txtATDateTime.Date;
        }

        private void Forms_Clicked(object sender, EventArgs e)
        {
            if (RdoForms.IsChecked == true)
            {
                SearchDispatchConfirmationFilter.include_received_form = 1;
            }
            else
            {
                SearchDispatchConfirmationFilter.include_received_form = 0;
            }
        }
    }
}