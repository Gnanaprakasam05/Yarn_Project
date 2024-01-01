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
    public partial class DispatchConfirmPage : ContentPage
    {
        DispatchConfirmViewModel viewModel;

        public DraftConfirmation DraftConfirmation_Check { get; set; }
        public SearchDispatchConfirmationFilter _searchDispatchConfirmationFilter { get; set; }
        public SearchDispatchConfirmationFilter SearchDispatchConfirmationFilter { get; set; }
        public dispatch_search_string dispatch_Search_String { get; set; }
        public int _TodaysPlan;
        public int Chech_pending;
        public DispatchConfirmPage(SearchDispatchConfirmationFilter searchFilter = null, int TodaysPlan = 1, int chech_pending = 0) //, int overdue_flag=0
        {
            InitializeComponent();

            if (Application.Current.Properties["transaction_pending_confirmation_FindAllowed"].ToString() == "0")
            {
                butFilter.IsVisible = false;
                lblFilter.IsVisible = false;
            }


            if (Application.Current.Properties["transaction_program_approval_FindAllowed"].ToString() == "0")
            {
                butFilter.IsVisible = false;
                lblFilter.IsVisible = false;
            }

            if (Application.Current.Properties["transaction_current_plan_FindAllowed"].ToString() == "0")
            {
                butFilter.IsVisible = false;
                lblFilter.IsVisible = false;
            }

            if (Application.Current.Properties["transaction_dispatched_FindAllowed"].ToString() == "0")
            {
                butFilter.IsVisible = false;
                lblFilter.IsVisible = false;
            }

            Chech_pending = chech_pending;

            if (TodaysPlan == 0 || TodaysPlan == 8)
            {
                ProgramView_List.IsVisible = true;
                DispatchConfirmListView.IsVisible = false;
            }

            DraftConfirmation_Check = new DraftConfirmation();

            _searchDispatchConfirmationFilter = searchFilter;
            dispatch_Search_String = new dispatch_search_string();
            SearchDispatchConfirmationFilter = new SearchDispatchConfirmationFilter();
            BindingContext = viewModel = new DispatchConfirmViewModel(null, TodaysPlan);
            //RdoIncludeOverdue.IsChecked = (overdue_flag == 1) ? true : false;
            _TodaysPlan = TodaysPlan;
            //if (TodaysPlan == 7 || TodaysPlan == 0 || TodaysPlan == 8)
            //{
            Grid.SetRow(txtSearch, 0);
            Grid.SetColumnSpan(txtSearch, 4);
            txtSearch.Margin = new Thickness(8, 0, 8, 0);
            RdoIncludeOverdue.IsVisible = false;
            startDatePicker.IsVisible = false;
            imgNextDate.IsVisible = false;
            imgPreviousDate.IsVisible = false;
            if (TodaysPlan == 13)
            {
                startDatePicker.IsVisible = true;
                txtSearch.IsVisible = false;
                imgPreviousDate.IsVisible = true;
                butFilter.IsVisible = false;
                lblFilter.IsVisible = false;
            }
            if (TodaysPlan != 2 && TodaysPlan != 3 && TodaysPlan != 4 && TodaysPlan != 1 && TodaysPlan != 10)
            {
                txtRecordCount.Margin = new Thickness(10, -15, 0, 10);
            }
            if (TodaysPlan == 2)
                grdBagsNotReady.IsVisible = true;
            else if (TodaysPlan == 3 || TodaysPlan == 10)
                grdPaymentNotReady.IsVisible = true;
            else if (TodaysPlan == 4)
                grdTransporterNotReady.IsVisible = true;
            else if (TodaysPlan == 1)
                grdCurrentPlan.IsVisible = true;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as DispatchConfirm;
            // Manually deselect item.
            DispatchConfirmListView.SelectedItem = null;
            ProgramView_List.SelectedItem = null;
            if (item == null)
                return;
            if (_TodaysPlan == 0)
            {
                await Navigation.PushAsync(new SendToProgramApprovalPage(null, null, item, DraftConfirmation_Check));
                sendapproval_close = true;
            }
            else if (_TodaysPlan == 8)
            {
                await Navigation.PushAsync(new AddProgramApprovalPage(null, null, item, DraftConfirmation_Check));
                addapproval_close = true;
            }
            else if (_TodaysPlan == 12)
                await Navigation.PushAsync(new FormsListPage(null, null, item));
            else if (_TodaysPlan == 13)
                return;
            else
            {
                await Navigation.PushAsync(new AddDispatchDeliveryDetailPage(null, null, item, 0, 0, DraftConfirmation_Check));
                detail_close = true;
            }

        }

        bool Check_DeleteFlag;
        bool Check_CancelFlag;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (DraftConfirmation_Check.Add_Flag != false)
            {
                if (_TodaysPlan == 0)
                {
                    viewModel.DeleteCommand(DraftConfirmation_Check.id);
                    DraftConfirmation_Check.Add_Flag = false;

                }
                else if (_TodaysPlan == 8)
                {
                    viewModel.ProgramApprovalDeleteCommand(DraftConfirmation_Check.id);

                    DraftConfirmation_Check.Add_Flag = false;
                }
            }
            else if (SearchDispatchConfirmationFilter.Search_Edit != false)
            {
                if (_searchDispatchConfirmationFilter != null)
                {
                    if (_searchDispatchConfirmationFilter.search_flag == 1)
                    {

                        butClear.IsVisible = true;
                        lblClear.IsVisible = true;

                        viewModel.SearchItemsCommand.Execute(_searchDispatchConfirmationFilter);
                        _searchDispatchConfirmationFilter.search_flag = 0;
                    }
                    SearchDispatchConfirmationFilter.Search_Edit = false;
                    DraftConfirmation_Check.Cancel_Click = 0;
                }

            }
            else
            {
                chkStatusSearch_Clicked(null, null);
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
            if (_searchDispatchConfirmationFilter != null)
            {
                _searchDispatchConfirmationFilter.search_string = txtSearch.Text;
                if (grdBagsNotReady.IsVisible == true)
                {
                    _searchDispatchConfirmationFilter.payment_ready = (chkPaymentReady.IsChecked == true) ? 1 : 0;
                    _searchDispatchConfirmationFilter.payment_received = (chkPaymentReceived.IsChecked == true) ? 1 : 0;
                    _searchDispatchConfirmationFilter.transporter_ready = (chkTransporterReady.IsChecked == true) ? 1 : 0;
                }
                else if (grdPaymentNotReady.IsVisible == true)
                {
                    _searchDispatchConfirmationFilter.bags_ready = (chkBagsReady.IsChecked == true) ? 1 : 0;
                    _searchDispatchConfirmationFilter.transporter_ready = (chkTransporterReady1.IsChecked == true) ? 1 : 0;
                }
                else if (grdTransporterNotReady.IsVisible == true)
                {
                    _searchDispatchConfirmationFilter.bags_ready = (chkBagsReady1.IsChecked == true) ? 1 : 0;
                    _searchDispatchConfirmationFilter.payment_ready = (chkPaymentReady1.IsChecked == true) ? 1 : 0;
                    _searchDispatchConfirmationFilter.payment_received = (chkPaymentReceived1.IsChecked == true) ? 1 : 0;
                }
                else if (grdCurrentPlan.IsVisible == true)
                {
                    _searchDispatchConfirmationFilter.bags_ready = (chkBagsReady2.IsChecked == true) ? 1 : 0;
                    _searchDispatchConfirmationFilter.payment_ready = (chkPaymentReady2.IsChecked == true) ? 1 : 0;
                    _searchDispatchConfirmationFilter.payment_received = (chkPaymentReceived2.IsChecked == true) ? 1 : 0;
                    _searchDispatchConfirmationFilter.transporter_ready = (chkTransporterReady2.IsChecked == true) ? 1 : 0;
                }
                viewModel.SearchItemsCommand.Execute(_searchDispatchConfirmationFilter);
            }
            else
            {
                chkStatusSearch_Clicked(null, null);
            }
        }

        private void StartDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            if (Convert.ToDateTime(startDatePicker.Date.ToString("yyyy-MM-dd")) > Convert.ToDateTime(DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd")))
            {
                imgNextDate.IsVisible = false;
                startDatePicker.Date = e.OldDate;
                dispatch_Search_String.Search_string = txtSearch.Text;
                dispatch_Search_String.current_date = e.OldDate;
                dispatch_Search_String.overdue_flag = (RdoIncludeOverdue.IsChecked) ? 1 : 0;
                if (grdBagsNotReady.IsVisible == true)
                {
                    dispatch_Search_String.payment_ready = (chkPaymentReady.IsChecked == true) ? 1 : 0;
                    dispatch_Search_String.payment_received = (chkPaymentReceived.IsChecked == true) ? 1 : 0;
                    dispatch_Search_String.transporter_ready = (chkTransporterReady.IsChecked == true) ? 1 : 0;
                }
                else if (grdPaymentNotReady.IsVisible == true)
                {
                    dispatch_Search_String.bags_ready = (chkBagsReady.IsChecked == true) ? 1 : 0;
                    dispatch_Search_String.transporter_ready = (chkTransporterReady1.IsChecked == true) ? 1 : 0;
                }
                else if (grdTransporterNotReady.IsVisible == true)
                {
                    dispatch_Search_String.bags_ready = (chkBagsReady1.IsChecked == true) ? 1 : 0;
                    dispatch_Search_String.payment_ready = (chkPaymentReady1.IsChecked == true) ? 1 : 0;
                    dispatch_Search_String.payment_received = (chkPaymentReceived1.IsChecked == true) ? 1 : 0;
                }
                else if (grdCurrentPlan.IsVisible == true)
                {
                    dispatch_Search_String.bags_ready = (chkBagsReady2.IsChecked == true) ? 1 : 0;
                    dispatch_Search_String.payment_ready = (chkPaymentReady2.IsChecked == true) ? 1 : 0;
                    dispatch_Search_String.payment_received = (chkPaymentReceived2.IsChecked == true) ? 1 : 0;
                    dispatch_Search_String.transporter_ready = (chkTransporterReady2.IsChecked == true) ? 1 : 0;
                }
                viewModel.LoadItemsCommand.Execute(dispatch_Search_String);
            }
            else
            {
                if (Convert.ToDateTime(startDatePicker.Date.ToString("yyyy-MM-dd")) != Convert.ToDateTime(DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd")))
                    imgNextDate.IsVisible = true;
                if (Convert.ToDateTime(startDatePicker.Date.ToString("yyyy-MM-dd")) == Convert.ToDateTime(DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd")))
                    imgNextDate.IsVisible = false;
                chkStatusSearch_Clicked(null, null);
            }
        }

        private void PreviousDate_Clicked(object sender, EventArgs e)
        {
            startDatePicker.Date = startDatePicker.Date.AddDays(-1);
            if (Convert.ToDateTime(startDatePicker.Date.ToString("yyyy-MM-dd")) != Convert.ToDateTime(DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd")))
                imgNextDate.IsVisible = true;
            ButClear_Clicked(null, null);
        }

        private void NextDate_Clicked(object sender, EventArgs e)
        {
            startDatePicker.Date = startDatePicker.Date.AddDays(1);
            if (Convert.ToDateTime(startDatePicker.Date.ToString("yyyy-MM-dd")) == Convert.ToDateTime(DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd")))
                imgNextDate.IsVisible = false;
            ButClear_Clicked(null, null);
        }

        private async void lblFilter_Tapped(object sender, EventArgs e)
        {
            //txtSearch.Text = "";
            if (_searchDispatchConfirmationFilter == null)
            {
                _searchDispatchConfirmationFilter = new SearchDispatchConfirmationFilter();
                _searchDispatchConfirmationFilter.confirmation_date_from = DateTime.Now.ToLocalTime();
                _searchDispatchConfirmationFilter.confirmation_date_to = DateTime.Now.ToLocalTime();
                _searchDispatchConfirmationFilter.approved_date_from = DateTime.Now.ToLocalTime();
                _searchDispatchConfirmationFilter.approved_date_to = DateTime.Now.ToLocalTime();
                _searchDispatchConfirmationFilter.dispatched_date_from = DateTime.Now.ToLocalTime();
                _searchDispatchConfirmationFilter.dispatched_date_to = DateTime.Now.ToLocalTime();
            }

            if (SearchDispatchConfirmationFilter == null)
            {
                SearchDispatchConfirmationFilter = new SearchDispatchConfirmationFilter();
            }
            await Navigation.PushModalAsync(new NavigationPage(new FilterDispatchConfirmationPage(_searchDispatchConfirmationFilter, _TodaysPlan, SearchDispatchConfirmationFilter)));
            filter_close = true;
        }

        private void ButClear_Clicked(object sender, EventArgs e)
        {
            lblFilter.Text = "Filter";
            butClear.IsVisible = false;
            lblClear.IsVisible = false;
            //butFilter.IsVisible = true;
            txtSearch.Text = "";
            _searchDispatchConfirmationFilter = null;
            SearchDispatchConfirmationFilter = null;
            //startDatePicker.Date = DateTime.Now.ToLocalTime();
            chkStatusSearch_Clicked(null, null);
        }

        bool Search_string;
        bool filter_check;
        bool sendapproval_close;
        bool addapproval_close;
        bool detail_close;
        bool filter_close;
        private void chkStatusSearch_Clicked(object sender, EventArgs e)
        {
            dispatch_Search_String.Search_string = txtSearch.Text;
            dispatch_Search_String.current_date = startDatePicker.Date;
            dispatch_Search_String.overdue_flag = (RdoIncludeOverdue.IsChecked) ? 1 : 0;

            if (grdBagsNotReady.IsVisible == true)
            {
                dispatch_Search_String.payment_ready = (chkPaymentReady.IsChecked == true) ? 1 : 0;
                dispatch_Search_String.payment_received = (chkPaymentReceived.IsChecked == true) ? 1 : 0;
                dispatch_Search_String.transporter_ready = (chkTransporterReady.IsChecked == true) ? 1 : 0;
            }
            else if (grdPaymentNotReady.IsVisible == true)
            {
                dispatch_Search_String.bags_ready = (chkBagsReady.IsChecked == true) ? 1 : 0;
                dispatch_Search_String.transporter_ready = (chkTransporterReady1.IsChecked == true) ? 1 : 0;

            }
            else if (grdTransporterNotReady.IsVisible == true)
            {
                dispatch_Search_String.bags_ready = (chkBagsReady1.IsChecked == true) ? 1 : 0;
                dispatch_Search_String.payment_ready = (chkPaymentReady1.IsChecked == true) ? 1 : 0;
                dispatch_Search_String.payment_received = (chkPaymentReceived1.IsChecked == true) ? 1 : 0;
            }
            else if (grdCurrentPlan.IsVisible == true)
            {
                dispatch_Search_String.bags_ready = (chkBagsReady2.IsChecked == true) ? 1 : 0;
                dispatch_Search_String.payment_ready = (chkPaymentReady2.IsChecked == true) ? 1 : 0;
                dispatch_Search_String.payment_received = (chkPaymentReceived2.IsChecked == true) ? 1 : 0;
                dispatch_Search_String.transporter_ready = (chkTransporterReady2.IsChecked == true) ? 1 : 0;
            }

            if (_TodaysPlan == 1 || _TodaysPlan == 14 || _TodaysPlan == 0 || _TodaysPlan == 2 || _TodaysPlan == 3 || _TodaysPlan == 8 || _TodaysPlan == 6 || _TodaysPlan == 11 || _TodaysPlan == 7 || _TodaysPlan == 9 || _TodaysPlan == 5 || _TodaysPlan == 10)
            {
                if (dispatch_Search_String.Search_string == null || dispatch_Search_String.Search_string == "")
                {
                    Search_string = false;
                }
                else
                {
                    Search_string = true;
                }

                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    Search_string = true;
                }


                if (addapproval_close == true || sendapproval_close == true || detail_close == true || filter_close == true)
                {
                    DraftConfirmation_Check.Cancel_Click = 1;
                    addapproval_close = false;
                    sendapproval_close = false;
                    Search_string = false;
                    detail_close = false;
                    filter_close = false;
                }
            }

            if (DraftConfirmation_Check.Delete_Flag == true)
            {
                DraftConfirmation_Check.Cancel_Click = 0;
                DraftConfirmation_Check.Delete_Flag = false;
            }
            if (SearchDispatchConfirmationFilter != null)
            {
                if (SearchDispatchConfirmationFilter.filter_check == 1)
                {
                    filter_check = true;
                    SearchDispatchConfirmationFilter.filter_check = 0;
                }
            }

            if (SearchDispatchConfirmationFilter == null)
            {
                DraftConfirmation_Check.Cancel_Click = 0;

            }

            if (DraftConfirmation_Check.Save_Check == true)
            {
                DraftConfirmation_Check.Cancel_Click = 0;
                DraftConfirmation_Check.Save_Check = false;
            }

            if (DraftConfirmation_Check.Cancel_Click != 1 || Search_string == true || filter_check == true)
            {

                if (Chech_pending == 1)
                {
                    viewModel.ExecuteLoadPendingConfirmationTodayCommand();
                    Chech_pending = 1;


                }
                else if (Chech_pending == 2)
                {
                    viewModel.ExecuteLoadPendingConfirmationDelayCommand();
                    Chech_pending = 2;

                }
                else if (Chech_pending == 3)
                {
                    viewModel.ExecuteLoadPendingConfirmationFutureCommand();
                    Chech_pending = 3;

                }
                else
                {
                    viewModel.LoadItemsCommand.Execute(dispatch_Search_String);
                    DraftConfirmation_Check.Cancel_Click = 1;
                    Search_string = false;
                    filter_check = false;
                }

            }
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            bool b = await viewModel.GenerateInvoice(dispatch_Search_String);
            if (b == true)
                await DisplayAlert("Pending Commission", "Invoice Generated Successfully", "Ok");
            else
                await DisplayAlert("Pending Commission", "Error!. Can't Generate Invoice Successfully", "Ok");
            await Task.Delay(200);
            viewModel.LoadItemsCommand.Execute(dispatch_Search_String);
        }
    }
}