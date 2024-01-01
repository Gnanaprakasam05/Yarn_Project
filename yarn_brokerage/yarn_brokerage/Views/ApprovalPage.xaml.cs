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
    public partial class ApprovalPage : ContentPage
    {
        ApprovalViewModel viewModel;

        public Approval Approval { get; set; }
        public SearchApprovalFilter _searchApprovalFilter { get; set; }
        public SearchApprovalFilter SearchApprovalFilter { get; set; }

        public ApprovalPage(SearchApprovalFilter searchFilter = null)
        {
            InitializeComponent();
            _searchApprovalFilter = searchFilter;

            Approval = new Approval();
            SearchApprovalFilter = new SearchApprovalFilter();

            BindingContext = viewModel = new ApprovalViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Approval;
            // Manually deselect item.
            ApprovalListView.SelectedItem = null;
            if (item == null)
                return;

            await Navigation.PushAsync(new AddApprovalPage(item, Approval));

            
        }
                

        protected override void OnAppearing()
        {
            base.OnAppearing();


            if (Approval.Add_Flag != false)
            {


                viewModel.RemoveApprovalCommand(Approval);

                Approval.Edit_Flag = false;
            }
            else if (SearchApprovalFilter.Search_Edit != false)
            {
                if (_searchApprovalFilter != null)
                {
                    if (_searchApprovalFilter.search_flag == 1)
                    {
                        //lblFilter.Text = "Result";
                        butClear.IsVisible = true;
                        lblClear.IsVisible = true;
                        //startDatePicker.Date = _searchApprovalFilter.transaction_date_time;
                        viewModel.SearchItemsCommand.Execute(_searchApprovalFilter);
                        _searchApprovalFilter.search_flag = 0;
                    }
                }
                SearchApprovalFilter.Search_Edit = false;
                Approval.CancelClick = 0;
            }
            else
            {
                if (Approval.CancelClick != 1)
                {
                    viewModel.LoadItemsCommand.Execute(txtSearch.Text);
                    Approval.CancelClick = 1;
                }

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
            if (_searchApprovalFilter != null)
            {
                _searchApprovalFilter.search_string = txtSearch.Text;
                viewModel.SearchItemsCommand.Execute(_searchApprovalFilter);
            }
            else
                viewModel.LoadItemsCommand.Execute(txtSearch.Text);
        }

        private void ButClear_Clicked(object sender, EventArgs e)
        {
            lblFilter.Text = "filter";
            butClear.IsVisible = false;
            lblClear.IsVisible = false;
            butFilter.IsVisible = true;
            _searchApprovalFilter = null;
            txtSearch.Text = "";
            //startdatepicker.date = datetime.now.tolocaltime();
            viewModel.LoadItemsCommand.Execute(null);
        }

        private async void lblFilter_Tapped(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            if (_searchApprovalFilter == null)
            {
                _searchApprovalFilter = new SearchApprovalFilter();
                _searchApprovalFilter.confirmation_date_from = DateTime.Now.ToLocalTime();
                _searchApprovalFilter.confirmation_date_to = DateTime.Now.ToLocalTime();
                _searchApprovalFilter.approved_date_from = DateTime.Now.ToLocalTime();
                _searchApprovalFilter.approved_date_to = DateTime.Now.ToLocalTime();                
            }
            await Navigation.PushModalAsync(new NavigationPage(new FilterApprovalPage(_searchApprovalFilter, SearchApprovalFilter)));
        }

        //private void StartDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        //{
        //    viewModel.LoadItemsCommand.Execute(startDatePicker.Date);
        //}

        //private void PreviousDate_Clicked(object sender, EventArgs e)
        //{
        //    startDatePicker.Date = startDatePicker.Date.AddDays(-1);
        //}

        //private void NextDate_Clicked(object sender, EventArgs e)
        //{
        //    startDatePicker.Date = startDatePicker.Date.AddDays(1);
        //}

        //private async void lblFilter_Tapped(object sender, EventArgs e)
        //{
        //    if (_searchApprovalFilter == null)
        //    {
        //        _searchApprovalFilter = new SearchFilter();
        //        _searchApprovalFilter.transaction_date_time = startDatePicker.Date;
        //    }
        //    await Navigation.PushModalAsync(new NavigationPage(new FilterEnquiryPage(_searchApprovalFilter,1)));
        //}

        //private void ButClear_Clicked(object sender, EventArgs e)
        //{
        //    lblFilter.Text = "Filter";
        //    butClear.IsVisible = false;
        //    lblClear.IsVisible = false;
        //    butFilter.IsVisible = true;
        //    _searchApprovalFilter = null;
        //    //startDatePicker.Date = DateTime.Now.ToLocalTime();
        //    viewModel.LoadItemsCommand.Execute(startDatePicker.Date);
        //}
    }
}