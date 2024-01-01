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
	public partial class CommissionInvoiceListPage : ContentPage
	{
		public CommissionInvoiceViewModel ViewModel { get; set; }
		public CommissionInvoice GetCommission { get; set; }

		public CommissionInvoiceListPage (CommissionInvoice item, SearchOutStandingFilter _searchFilter)
		{
			InitializeComponent ();
			GetCommission = item;
			GetCommission.commission_date_flg = _searchFilter.commission_date_flg;
			GetCommission.contact_type_id = _searchFilter.contact_type_id;
			GetCommission.contact_id = _searchFilter.contact_id;
			GetCommission.invoice_date_from = _searchFilter.invoice_date_from;
			GetCommission.invoice_date_to = _searchFilter.invoice_date_to;
			GetCommission.search_string = _searchFilter.search_string;
			BindingContext = ViewModel = new CommissionInvoiceViewModel();
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
			ViewModel.GetCommissionInvoiceListCommand.Execute(GetCommission);
        }
    }
}