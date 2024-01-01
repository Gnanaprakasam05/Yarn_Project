using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using yarn_brokerage.Models;
using yarn_brokerage.ViewModels;

namespace yarn_brokerage.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommissionInvoiceReportPage : ContentPage
    {
        public CommissionInvoiceViewModel ViewModel { get; set; }
        public SearchOutStandingFilter _searchFilter { get; set; }
        public CommissionInvoiceReportPage(SearchOutStandingFilter searchFilter = null)
        {
            InitializeComponent();
            _searchFilter = searchFilter;
            BindingContext = ViewModel = new CommissionInvoiceViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (_searchFilter != null)
            {
                ViewModel.SearchOutstandingCommand.Execute(_searchFilter);
            }
            else
            {
                Filter_Clicked(null, null);
            }
        }
        private async void Filter_Clicked(object sender, EventArgs e)
        {
            if (_searchFilter == null)
            {
                _searchFilter = new SearchOutStandingFilter();
                _searchFilter.invoice_date_from = DateTime.Now.ToLocalTime();
                _searchFilter.invoice_date_to = DateTime.Now.ToLocalTime();
            }
            await Navigation.PushAsync(new FilterCommissionInvoicePage(_searchFilter));
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (_searchFilter == null)
            {
                _searchFilter = new SearchOutStandingFilter();

            }
            await Navigation.PushAsync(new FilterCommissionInvoicePage(_searchFilter));
        }

        private async void CommissionInvoiceListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as CommissionInvoice;
            await Navigation.PushAsync(new CommissionInvoiceListPage(item, _searchFilter));
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            _searchFilter.search_string = txtSearch.Text;
            ViewModel.SearchOutstandingCommand.Execute(_searchFilter);
        }
    }
}