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
    public partial class DraftConfirmationPage : ContentPage
    {
        DraftConfirmationViewModel viewModel;
        public SearchConfirmationFilter _searchFilter { get; set; }
        public SearchConfirmationFilter SearchConfirmationFilter { get; set; }
        public search_string _search_string { get; set; }

        public DraftConfirmation DraftConfirmation { get; set; }
        public DraftConfirmationPage(SearchConfirmationFilter searchFilter = null)
        {
            InitializeComponent();

            if (Application.Current.Properties["transaction_draft_confirmation_InsertAllowed"].ToString() == "0")
            {
                addEnable.IsEnabled = false;
            }
            if (Application.Current.Properties["transaction_draft_confirmation_FindAllowed"].ToString() == "0")
            {
                butFilter.IsVisible = false;
                lblFilter.IsVisible = false;
            }

            _searchFilter = searchFilter;
            _search_string = new search_string();

            DraftConfirmation = new DraftConfirmation();

            SearchConfirmationFilter = new SearchConfirmationFilter();

            BindingContext = viewModel = new DraftConfirmationViewModel();


        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as DraftConfirmation;

            bool EditFlag = true;
            // Manually deselect item.
            DraftConfirmationListView.SelectedItem = null;
            if (item == null)
                return;
            int count = await viewModel.getAmendCount(item.id);
            if (count <= 0 || item.send_for_approval == 0 || item.status == 0)
            {
                await Navigation.PushAsync(new AddDraftConfirmationPage(item, null, null, EditFlag, DraftConfirmation));


                //await this.DisplayAlert("Alert", "All the dispatch plans are processed", "Ok");
            }
            else
            {
                await Navigation.PushAsync(new EditDraftConfirmationPage(item));
            }
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddDraftConfirmationPage(null, null, null, false, DraftConfirmation));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


            if (SearchConfirmationFilter == null)
            {
                SearchConfirmationFilter = new SearchConfirmationFilter();
            }
            if (DraftConfirmation.Edit_Flag != false)
            {
                viewModel.EditStoreCommand(DraftConfirmation);
                DraftConfirmation.Edit_Flag = false;
            }
            else if (SearchConfirmationFilter.Search_Edit != false)
            {
                if (_searchFilter != null)
                {

                    butClear.IsVisible = true;
                    lblClear.IsVisible = true;
                    viewModel.SearchItemsCommand.Execute(_searchFilter);

                }

                SearchConfirmationFilter.Search_Edit = false;
                //DraftConfirmation.Cancel_Click = 0;
            }
            else
            {
                _search_string.Search_string = txtSearch.Text;
                _search_string.current_date = startDatePicker.Date;



                if (DraftConfirmation.Cancel_Click != 1)
                {
                    viewModel.LoadItemsCommand.Execute(_search_string);

                    DraftConfirmation.Cancel_Click = 1;
                }

            }



            //if (DraftConfirmation.Add_Flag != false)
            //{
            //    viewModel.AddStoreCommand(DraftConfirmation);
            //    DraftConfirmation.Add_Flag = false;


            //} else if (DraftConfirmation.Edit_Flag != false)
            //{



            //    viewModel.EditStoreCommand(DraftConfirmation);
            //    DraftConfirmation.Edit_Flag = false;




            //}
            //else if(SearchConfirmationFilter.Search_Edit != false)
            //{
            //    if (_searchFilter != null)
            //    {

            //        butClear.IsVisible = true;
            //        lblClear.IsVisible = true;                
            //        viewModel.SearchItemsCommand.Execute(_searchFilter);

            //    }
            //    SearchConfirmationFilter.Search_Edit = false;
            //    DraftConfirmation.Cancel_Click = 0;
            //} 
            //else
            //{
            //        _search_string.Search_string = txtSearch.Text;
            //        _search_string.current_date = startDatePicker.Date;
            //        if (DraftConfirmation.Cancel_Click != 1)
            //        {
            //            viewModel.LoadItemsCommand.Execute(_search_string);//startDatePicker.Date

            //            DraftConfirmation.Cancel_Click = 1;
            //        }

            //}
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
            if (SearchConfirmationFilter == null)
            {
                SearchConfirmationFilter = new SearchConfirmationFilter();
            }
            await Navigation.PushModalAsync(new NavigationPage(new FilterConfirmationPage(_searchFilter, 1, SearchConfirmationFilter)));
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
            SearchConfirmationFilter = null;
            txtSearch.Text = "";
            //startDatePicker.Date = DateTime.Now.ToLocalTime();
            _search_string.Search_string = txtSearch.Text;
            _search_string.current_date = startDatePicker.Date;
            viewModel.LoadItemsCommand.Execute(_search_string);
        }
    }
}