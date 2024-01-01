using System;
using System.Collections.Generic;
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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LedgersListPage : ContentPage
    {
        LedgersViewModel viewModel;
        public Indexes Enquiry { get; set; }
        public Negotiation Negotiation { get; set; }
        public DraftConfirmation DraftConfirmation { get; set; }
        public CommissionInvoice CommissionInvoice { get; set; }
        public SearchFilter SearchFilter { get; set; }
        public SearchConfirmationFilter SearchConfirmationFilter { get; set; }
        public SearchOutStandingFilter SearchOutStandingFilter { get; set; }
        public SearchApprovalFilter SearchApprovalFilter { get; set; }
        public SearchDispatchConfirmationFilter SearchDispatchConfirmationFilter { get; set; }
        // public List<SuggestionProblemListDetails> suggestionProblemListDetails { get; set; }

        int _exact_ledger_flag = 0;
        int _transaction_type = 0;
        int beforeProblemCount = 0;
        public CommissionReceipt CommissionReceipt { get; set; }
        public CallLogModel CallLogModel { get; set; }

        public LedgersListPage (Indexes enquiry=null, int transaction_type=1, Negotiation negotiation=null, DraftConfirmation draftConfirmation = null, SearchFilter searchFilter = null, SearchConfirmationFilter searchConfirmationFilter = null, SearchApprovalFilter searchApprovalFilter = null, SearchDispatchConfirmationFilter searchDispatchConfirmationFilter = null, int exact_ledger_flag=0, CommissionInvoice commissionInvoice = null, CommissionReceipt commissionReceipt = null, CallLogModel callLogModel = null,SearchOutStandingFilter searchOutStandingFilter=null)
		{
			InitializeComponent ();
           
            Enquiry = enquiry;
            Negotiation = negotiation;
            DraftConfirmation = draftConfirmation;
            CommissionInvoice = commissionInvoice;
            CommissionReceipt = commissionReceipt;
            CallLogModel = callLogModel;
            SearchFilter = searchFilter;
            SearchConfirmationFilter = searchConfirmationFilter;
            SearchApprovalFilter = searchApprovalFilter;
            SearchDispatchConfirmationFilter = searchDispatchConfirmationFilter;
            SearchOutStandingFilter = searchOutStandingFilter;
            _transaction_type = transaction_type;
            _exact_ledger_flag = exact_ledger_flag;
            if (_exact_ledger_flag == 1)
                _transaction_type = (_transaction_type==1)? 2:1;
            txtSearch.Focus();
            BindingContext = viewModel = new LedgersViewModel(_transaction_type);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Ledger;
            LedgersListView.SelectedItem = null;
            if (item == null)
                return;
            if (Enquiry != null)
            {
                if (_exact_ledger_flag == 1)
                {
                    DraftConfirmation.supplier_id = item.Id;
                    Enquiry.exact_ledger_id = item.Id;
                    Enquiry.exact_ledger_name = item.Name;
                }
                else
                {
                    Enquiry.ledger_id = item.Id;
                    Enquiry.ledger_name = item.Name;
                    Enquiry.bag_weight = item.bag_weight;
                    Enquiry.counts = item.counts;
                }
            }
            if (Negotiation != null)
            {
                if (_transaction_type == 1)
                {
                    Negotiation.supplier_id = item.Id;
                    Negotiation.supplier_name = item.Name;
                }
                else if (_transaction_type == 2)
                {
                    Negotiation.customer_id = item.Id;
                    Negotiation.customer_name = item.Name;
                }
            }
            if (DraftConfirmation != null)
            {
                if (_transaction_type == 1)
                {
                    DraftConfirmation.supplier_id = item.Id;
                    DraftConfirmation.supplier_name = item.Name;
                }
                else if (_transaction_type == 2)
                {
                    DraftConfirmation.customer_id = item.Id;
                    DraftConfirmation.customer_name = item.Name;
                    DraftConfirmation.bag_weight = item.bag_weight;

                }
            }
            if (CommissionInvoice != null)
            {
                CommissionInvoice.ledger_id = item.Id;
                CommissionInvoice.ledger_name = item.Name;
                CommissionInvoice.commission_type = item.commission_type;
                CommissionInvoice.commission_value = item.commission_value;
                CommissionInvoice.company_id = item.company_id;
                CommissionInvoice.company_name= item.company_name;
            }
            if (CommissionReceipt != null)
            {
                CommissionReceipt.ledger_id = item.Id;
                CommissionReceipt.ledger_name = item.Name;
                //CommissionReceipt.commission_type = item.commission_type;
                //CommissionReceipt.commission_value = item.commission_value;
                CommissionReceipt.company_id = item.company_id;
                CommissionReceipt.company_name = item.company_name;
            }
            if (CallLogModel != null)
            {
                CallLogModel.ledger_id = item.Id;
                CallLogModel.LedgerName = item.Name;               
            }
            if (SearchFilter != null)
            {
                if (_transaction_type == 1)
                {
                    SearchFilter.supplier_id = item.Id;
                    SearchFilter.supplier_name = item.Name;
                }
                else if (_transaction_type == 2)
                {

                    SearchFilter.customer_id = item.Id;
                    SearchFilter.customer_name = item.Name;
                }
            }
            if (SearchConfirmationFilter != null)
            {
                if (_transaction_type == 1)
                {
                    SearchConfirmationFilter.supplier_id = item.Id;
                    SearchConfirmationFilter.supplier_name = item.Name;
                }
                else if (_transaction_type == 2)
                {
                    SearchConfirmationFilter.customer_id = item.Id;
                    SearchConfirmationFilter.customer_name = item.Name;
                }
            }
            if (SearchApprovalFilter != null)
            {
                if (_transaction_type == 1)
                {
                    SearchApprovalFilter.supplier_id = item.Id;
                    SearchApprovalFilter.supplier_name = item.Name;
                }
                else if (_transaction_type == 2)
                {
                    SearchApprovalFilter.customer_id = item.Id;
                    SearchApprovalFilter.customer_name = item.Name;
                }
            }
            if (SearchDispatchConfirmationFilter != null)
            {
                if (_transaction_type == 1)
                {
                    SearchDispatchConfirmationFilter.supplier_id = item.Id;
                    SearchDispatchConfirmationFilter.supplier_name = item.Name;
                }
                else if (_transaction_type == 2)
                {
                    SearchDispatchConfirmationFilter.customer_id = item.Id;
                    SearchDispatchConfirmationFilter.customer_name = item.Name;
                }
            }
            if (SearchOutStandingFilter != null)
            {
                SearchOutStandingFilter.ledger_name = item.Name;
                SearchOutStandingFilter.contact_id = item.Id;
            }
            
           await Navigation.PopAsync();
        }

        async void AddLedger_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddLedgerPage(null,_transaction_type)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            txtSearch.Focus();
            // int CountId = 0;
            //if (_fromSuggestionFlag == 1)
            //{
            //    CountId = suggestionList.CountId;
            //    if (suggestionProblemListDetails.Count > beforeProblemCount)
            //        Navigation.PopAsync();
            //}
            //if (viewModel.Ledgers.Count == 0)
            viewModel.LoadLedgersCommand.Execute("");
            //viewModel.ExecuteSearchLoadLedgersCommand(txtSearch.Text, CountId);
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            //int CountId = 0;
            //if (_fromSuggestionFlag == 1)
            //{
            //    CountId = suggestionList.CountId;                
            //}
            viewModel.LoadLedgersCommand.Execute(txtSearch.Text);
        }
    }
}