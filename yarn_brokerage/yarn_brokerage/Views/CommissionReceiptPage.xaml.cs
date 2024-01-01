using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using yarn_brokerage.Models;
using yarn_brokerage.Views;
using yarn_brokerage.ViewModels;
using System.Diagnostics;

namespace yarn_brokerage.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class CommissionReceiptPage : ContentPage
    {
        CommissionReceiptViewModel viewModel;
        public SearchConfirmationFilter _searchFilter { get; set; }
        public search_string _search_string { get; set; }
        public CommissionReceiptPage(SearchConfirmationFilter searchFilter = null)
        {
            InitializeComponent();
            _searchFilter = searchFilter;
            _search_string = new search_string();
            BindingContext = viewModel = new CommissionReceiptViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as CommissionReceipt;
            // Manually deselect item.
            CommissionReceiptListView.SelectedItem = null;
            if (item == null)
                return;
            //int count = await viewModel.getAmendCount(item.id);
            //if (count <= 0 || item.send_for_approval==0 || item.status==0)
            //{
                await Navigation.PushAsync(new AddCommissionReceiptPage(item));
            //    //await this.DisplayAlert("Alert", "All the dispatch plans are processed", "Ok");
            //}
            //else
            //{
            //    //await Navigation.PushAsync(new EditCommissionReceipt(item));
            //}            
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddCommissionReceiptPage(null));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_searchFilter != null)
            {
                //if (_searchFilter.search_flag == 1)
                //{
                    //lblFilter.Text = "Result";
                    butClear.IsVisible = true;
                    lblClear.IsVisible = true;
                    //imgPreviousDate.IsVisible = false;
                    //startDatePicker.IsVisible = false;
                    //imgNextDate.IsVisible = false;
                    //startDatePicker.Date = _searchFilter.transaction_date_time; // single page confirmation
                    viewModel.SearchItemsCommand.Execute(_searchFilter);
                    //_searchFilter.search_flag = 0;
                //}
            }
            else
            {
                _search_string.Search_string = txtSearch.Text;
                _search_string.current_date = startDatePicker.Date;
                viewModel.LoadItemsCommand.Execute(_search_string); //startDatePicker.Date
            }
        }

        public bool CloseApp = true;
        protected override bool OnBackButtonPressed()
        {
            if (Navigation.NavigationStack.Count <= 1)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    bool result = await this.DisplayAlert("Attention!", "Do you want to exit?", "Yes", "No");
                    if (result)
                    {
                        CloseApp = false;
                        Process.GetCurrentProcess().CloseMainWindow();
                        Process.GetCurrentProcess().Close();
                    }

                });
            }
            else
            {
                CloseApp = false;
            }
            return CloseApp;
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_searchFilter != null)
            {
                _searchFilter.search_string = txtSearch.Text;
                viewModel.SearchItemsCommand.Execute(_searchFilter);
            }
            else
            {
                _search_string.Search_string = txtSearch.Text;
                _search_string.current_date = startDatePicker.Date;
                viewModel.LoadItemsCommand.Execute(_search_string);
            }
        }

        private void StartDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            _search_string.Search_string = txtSearch.Text;
            _search_string.current_date = startDatePicker.Date;
            viewModel.LoadItemsCommand.Execute(_search_string);
        }
        
        private void PreviousDate_Clicked(object sender, EventArgs e)
        {
            startDatePicker.Date = startDatePicker.Date.AddDays(-1); // single page confirmation
            ButClear_Clicked(null, null);
        }

        private void NextDate_Clicked(object sender, EventArgs e)
        {
            startDatePicker.Date = startDatePicker.Date.AddDays(1); // single page confirmation
            ButClear_Clicked(null, null);
        }

        private async void lblFilter_Tapped(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            if (_searchFilter == null)
            {
                _searchFilter = new SearchConfirmationFilter();
                _searchFilter.confirmation_date_from = DateTime.Now.ToLocalTime();
                _searchFilter.confirmation_date_to = DateTime.Now.ToLocalTime();
                _searchFilter.dispatch_date_from = DateTime.Now.ToLocalTime();
                _searchFilter.dispatch_date_to = DateTime.Now.ToLocalTime();
                _searchFilter.payment_date_from = DateTime.Now.ToLocalTime();
                _searchFilter.payment_date_to = DateTime.Now.ToLocalTime(); // single page confirmation
            }
            await Navigation.PushModalAsync(new NavigationPage(new FilterConfirmationPage(_searchFilter,1)));
        }

        private void ButClear_Clicked(object sender, EventArgs e)
        {
            lblFilter.Text = "Filter";
            butClear.IsVisible = false;
            lblClear.IsVisible = false;
            butFilter.IsVisible = true;
            //imgPreviousDate.IsVisible = true;
            //startDatePicker.IsVisible = true;
            //imgNextDate.IsVisible = true;
            _searchFilter = null;
            txtSearch.Text = "";
            //startDatePicker.Date = DateTime.Now.ToLocalTime();
            _search_string.Search_string = txtSearch.Text;
            _search_string.current_date = startDatePicker.Date;
            viewModel.LoadItemsCommand.Execute(_search_string);
        }
    }
}