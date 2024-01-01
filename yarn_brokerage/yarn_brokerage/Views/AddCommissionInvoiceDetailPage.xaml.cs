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
using System.Collections.ObjectModel;

namespace yarn_brokerage.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AddCommissionInvoiceDetailPage : ContentPage
    {
        AddCommissionInvoiceDetailViewModel viewModel;
        //public SearchConfirmationFilter _searchFilter { get; set; }
        public ObservableCollection<CommissionInvoiceDetail> CommissionInvoiceDetails { get; set; }
        public CommissionInvoice CommissionInvoice;
        public search_string _search_string { get; set; }
        public AddCommissionInvoiceDetailPage(CommissionInvoice commissionInvoice, ObservableCollection<CommissionInvoiceDetail> commissionInvoiceDetails)
        {
            InitializeComponent();
            //_searchFilter = searchFilter;
            CommissionInvoice = commissionInvoice;
            CommissionInvoiceDetails = commissionInvoiceDetails;
            _search_string = new search_string();
            BindingContext = viewModel = new AddCommissionInvoiceDetailViewModel();
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as AddCommissionInvoiceDetail;
            // Manually deselect item.
            AddCommissionInvoiceDetailListView.SelectedItem = null;
            if (item == null)
                return;
            viewModel.UpdateInvoiceDetails(item.id);
            //CommissionInvoiceDetail.draft_confirmation_id = item.draft_confirmation_id;
            //CommissionInvoiceDetail.draft_confirmation_detail_id = item.id;
            //CommissionInvoiceDetail.invoice_no = item.invoice_no;
            //CommissionInvoiceDetail.invoice_date = item.invoice_date;
            //CommissionInvoiceDetail.qty = item.qty;
            //CommissionInvoiceDetail.unit = item.unit;
            //CommissionInvoiceDetail.qty_unit = item.qty_unit;
            //CommissionInvoiceDetail.ledger_id = item.ledger_id;
            //CommissionInvoiceDetail.ledger_name = item.ledger_name;
            //CommissionInvoiceDetail.exmill_amount = item.invoice_value;
            //CommissionInvoiceDetail.image = item.image;
            //CommissionInvoiceDetail.invoice_details = item.invoice_details;
            //CommissionInvoiceDetail.commission_amount = (CommissionInvoice.commission_type==1) ? (CommissionInvoiceDetail.exmill_amount * (CommissionInvoice.commission_value/100)) : (CommissionInvoiceDetail.qty* CommissionInvoice.commission_value);
            //await Navigation.PopAsync();
            //int count = await viewModel.getAmendCount(item.id);
            //if (count <= 0 || item.send_for_approval==0 || item.status==0)
            //{
            //    await Navigation.PushAsync(new AddAddCommissionInvoiceDetailPage(item));
            //    //await this.DisplayAlert("Alert", "All the dispatch plans are processed", "Ok");
            //}
            //else
            //{
            //    //await Navigation.PushAsync(new EditAddCommissionInvoiceDetail(item));
            //}            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (_searchFilter != null)
            //{
            //    //if (_searchFilter.search_flag == 1)
            //    //{
            //        //lblFilter.Text = "Result";
            //        butClear.IsVisible = true;
            //        lblClear.IsVisible = true;
            //        //imgPreviousDate.IsVisible = false;
            //        //startDatePicker.IsVisible = false;
            //        //imgNextDate.IsVisible = false;
            //        //startDatePicker.Date = _searchFilter.transaction_date_time; // single page confirmation
            //        viewModel.SearchItemsCommand.Execute(_searchFilter);
            //        //_searchFilter.search_flag = 0;
            //    //}
            //}
            //else
            //{
                _search_string.Search_string = txtSearch.Text;
                _search_string.current_date = startDatePicker.Date;
                viewModel.LoadItemsCommand.Execute(CommissionInvoice); //startDatePicker.Date
            //}
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (_searchFilter != null)
            //{
            //    _searchFilter.search_string = txtSearch.Text;
            //    viewModel.SearchItemsCommand.Execute(_searchFilter);
            //}
            //else
            //{
            //    _search_string.Search_string = txtSearch.Text;
            //    _search_string.current_date = startDatePicker.Date;
            //    viewModel.LoadItemsCommand.Execute(_search_string);
            //}
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
            //if (_searchFilter == null)
            //{
            //    _searchFilter = new SearchConfirmationFilter();
            //    _searchFilter.confirmation_date_from = DateTime.Now.ToLocalTime();
            //    _searchFilter.confirmation_date_to = DateTime.Now.ToLocalTime();
            //    _searchFilter.dispatch_date_from = DateTime.Now.ToLocalTime();
            //    _searchFilter.dispatch_date_to = DateTime.Now.ToLocalTime();
            //    _searchFilter.payment_date_from = DateTime.Now.ToLocalTime();
            //    _searchFilter.payment_date_to = DateTime.Now.ToLocalTime(); // single page confirmation
            //}
            //await Navigation.PushModalAsync(new NavigationPage(new FilterConfirmationPage(_searchFilter,1)));
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
            //_searchFilter = null;
            txtSearch.Text = "";
            //startDatePicker.Date = DateTime.Now.ToLocalTime();
            _search_string.Search_string = txtSearch.Text;
            _search_string.current_date = startDatePicker.Date;
            viewModel.LoadItemsCommand.Execute(_search_string);
        }

        private void CheckBox_CheckChanged(object sender, EventArgs e)
        {
            viewModel.TotalInvoiceAmount();
        }

        private async void butSave_Clicked(object sender, EventArgs e)
        {
            viewModel.AddInvoiceDetails(CommissionInvoice, CommissionInvoiceDetails);
            await  Navigation.PopAsync();
        }
    }
}