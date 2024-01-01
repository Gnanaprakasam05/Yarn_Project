using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using yarn_brokerage.Models;

namespace yarn_brokerage.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterCommissionInvoicePage : ContentPage
    {
        public SearchOutStandingFilter SearchFilter { get; set; }
        public FilterCommissionInvoicePage(SearchOutStandingFilter _searchFilter)
        {
            InitializeComponent();
            if (_searchFilter == null)
                SearchFilter = new SearchOutStandingFilter();
            else
            {
                SearchFilter = _searchFilter;
                if (SearchFilter.contact_type_id == 1) RdoSupplier_Clicked(null, null); else if (SearchFilter.contact_type_id == 2) RdoCustomer_Clicked(null, null);
                if (SearchFilter.commission_date_flg == 1) { RdoConfirm.IsChecked = true; Confirmation_Clicked(null, null); }
            }
            BindingContext = this;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            txtLedgerName.Text = SearchFilter.ledger_name;
            SearchFilter.contact_id = SearchFilter.contact_id;
        }
        private async void TxtLedgerName_Focused(object sender, EventArgs e)
        {
            if (txtLedgerName.IsEnabled == false)
                return;
            await Navigation.PushAsync(new LedgersListPage(null, SearchFilter.contact_type_id, null, null, null, null, null, null, 0, null, null, null, SearchFilter));
        }
        private void RdoSupplier_Clicked(object sender, EventArgs e)
        {
            lblcontactname.Text = "Supplier";
            txtLedgerName.Placeholder = "Supplier Name";
            SearchFilter.contact_type_id = 1;
            RdoSupplier.IsChecked = true;
            RdoCustomer.IsChecked = false;
        }

        private void RdoCustomer_Clicked(object sender, EventArgs e)
        {
            lblcontactname.Text = "Customer";
            txtLedgerName.Placeholder = "Customer Name";
            SearchFilter.contact_type_id = 2;
            RdoCustomer.IsChecked = true;
            RdoSupplier.IsChecked = false;
        }

        private void ButCustomerClear_Clicked(object sender, EventArgs e)
        {
            txtLedgerName.Text = "";
        }
        private async void butFilter_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void Confirmation_Clicked(object sender, EventArgs e)
        {
            if (RdoConfirm.IsChecked == true)
            {
                SearchFilter.commission_date_flg = 1;
                txtDFDateTime.IsEnabled = true;
                txtDTDateTime.IsEnabled = true;
            }
            else
            {
                SearchFilter.commission_date_flg = 0;
                txtDFDateTime.IsEnabled = false;
                txtDTDateTime.IsEnabled = false;
            }
        }
    }
}