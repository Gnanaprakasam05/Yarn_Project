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

namespace yarn_brokerage.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ConfirmationPage : ContentPage
    {
        ConfirmationViewModel viewModel;
        public SearchConfirmationFilter _searchFilter { get; set; }

        public ConfirmationPage(SearchConfirmationFilter searchFilter = null)
        {
            InitializeComponent();            
            _searchFilter = searchFilter;
            BindingContext = viewModel = new ConfirmationViewModel(null,"Draft Confirmation");
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as DraftConfirmation;
            // Manually deselect item.
            DraftConfirmationListView.SelectedItem = null;
            if (item == null)
                return;

            await Navigation.PushAsync(new AddDraftConfirmationPage(item));

            
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddDraftConfirmationPage(null));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_searchFilter != null)
            {
                if (_searchFilter.search_flag == 1)
                {
                    //lblFilter.Text = "Result";
                    butClear.IsVisible = true;
                    lblClear.IsVisible = true;
                    //startDatePicker.Date = _searchFilter.transaction_date_time; // single page confirmation
                    viewModel.SearchItemsCommand.Execute(_searchFilter);
                    _searchFilter.search_flag = 0;
                }
            }
            else
                // if (viewModel.DraftConfirmation.Count == 0)
                viewModel.LoadItemsCommand.Execute(txtSearch.Text); //startDatePicker.Date
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_searchFilter != null)
            {
                _searchFilter.search_string = txtSearch.Text;
                viewModel.SearchItemsCommand.Execute(_searchFilter);
            }
            else
                viewModel.LoadItemsCommand.Execute(txtSearch.Text);
        }

        private void StartDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            //viewModel.LoadItemsCommand.Execute(startDatePicker.Date);
        }
        
        private void PreviousDate_Clicked(object sender, EventArgs e)
        {
            //startDatePicker.Date = startDatePicker.Date.AddDays(-1); // single page confirmation
        }

        private void NextDate_Clicked(object sender, EventArgs e)
        {
            //startDatePicker.Date = startDatePicker.Date.AddDays(1); // single page confirmation
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
            _searchFilter = null;
            txtSearch.Text = "";
            //startDatePicker.Date = DateTime.Now.ToLocalTime();
            viewModel.LoadItemsCommand.Execute(null);
        }
    }
}