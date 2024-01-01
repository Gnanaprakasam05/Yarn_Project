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
	public partial class CountListPage : ContentPage
	{
        CountViewModel viewModel;
        public Indexes Enquiry { get; set; }
        public DraftConfirmation DraftConfirmation { get; set; }
        public SearchConfirmationFilter SearchConfirmationFilter { get; set; }
        public SearchFilter SearchFilter { get; set; }
        public SearchApprovalFilter SearchApprovalFilter { get; set; }
        public SearchDispatchConfirmationFilter SearchDispatchConfirmationFilter { get; set; }
        public CountListPage (Indexes enquiry=null, DraftConfirmation draftConfirmation=null, SearchFilter searchFilter = null, SearchConfirmationFilter searchConfirmationFilter=null, SearchApprovalFilter searchApprovalFilter = null, SearchDispatchConfirmationFilter searchDispatchConfirmationFilter=null)
		{
			InitializeComponent ();
            if (enquiry != null)
            {
                Enquiry = enquiry;
            }
            else
            {
                Enquiry = new Indexes();
            }
            if(draftConfirmation != null)
            {
                DraftConfirmation = draftConfirmation;
            }
            else
            {
                DraftConfirmation = new DraftConfirmation();
            }

            if (searchFilter != null)
            {
                SearchFilter = searchFilter;
            }
            else
            {
                SearchFilter = new SearchFilter();
            }

            if (searchConfirmationFilter != null)
            {
                SearchConfirmationFilter = searchConfirmationFilter;
            }
            else
            {
                SearchConfirmationFilter = new SearchConfirmationFilter();
            }
            if (searchApprovalFilter != null)
            {
                SearchApprovalFilter = searchApprovalFilter;
            }
            else
            {
                SearchApprovalFilter = new SearchApprovalFilter();
            } 
            
            if (searchDispatchConfirmationFilter != null)
            {
                SearchDispatchConfirmationFilter = searchDispatchConfirmationFilter;
            }
            else
            {
                SearchDispatchConfirmationFilter = new SearchDispatchConfirmationFilter();
            }
            
           
            txtSearch.Focus();
            BindingContext = viewModel = new CountViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Count;
            CountListView.SelectedItem = null;
            if (item == null)
                return;
            if (Enquiry != null)
            {
                Enquiry.count_id = item.Id;
                Enquiry.count_name = item.Name;
            }
            if (DraftConfirmation != null)
            {
                DraftConfirmation.count_id = item.Id;
                DraftConfirmation.count_name = item.Name;
            }
            if (SearchFilter != null)
            {
                SearchFilter.count_id = item.Id;
                SearchFilter.count_name = item.Name;
            }
            if (SearchConfirmationFilter != null)
            {
                SearchConfirmationFilter.count_id = item.Id;
                SearchConfirmationFilter.count_name = item.Name;
            }
            if (SearchApprovalFilter != null)
            {
                SearchApprovalFilter.count_id = item.Id;
                SearchApprovalFilter.count_name = item.Name;
            }
            if (SearchDispatchConfirmationFilter != null)
            {
                SearchDispatchConfirmationFilter.count_id = item.Id;
                SearchDispatchConfirmationFilter.count_name = item.Name;
            }
            await Navigation.PopAsync();            
        }
        async void AddCount_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddCountPage(null,1)));
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            txtSearch.Focus();
            //if (viewModel.Count.Count == 0)
            //int i;
            if (DraftConfirmation.supplier_id > 0 || Enquiry.ledger_id > 0 || Enquiry.exact_ledger_id > 0 || SearchConfirmationFilter.supplier_id > 0 || SearchFilter.supplier_id > 0 || SearchApprovalFilter.supplier_id > 0 || SearchDispatchConfirmationFilter.supplier_id > 0)
            {
                if(Enquiry.transaction_type == 1)
                {
                   
                        if (Enquiry.ledger_id > 0)
                        {
                            DraftConfirmation.supplier_id = Enquiry.ledger_id;
                        }
                    
                }
                else if (Enquiry.transaction_type == 2)
                {
                        if (Enquiry.exact_ledger_id > 0)
                        {
                            DraftConfirmation.supplier_id = (int)Enquiry.exact_ledger_id;
                        }
                        else
                        {
                            viewModel.LoadCountsCommand.Execute(null);
                        }

                    viewModel.SupplierCountList(DraftConfirmation);
                }
                
                if(SearchConfirmationFilter.supplier_id > 0)
                {
                    DraftConfirmation.supplier_id = SearchConfirmationFilter.supplier_id;
                } 
                if(SearchFilter.supplier_id > 0)
                {
                    DraftConfirmation.supplier_id = SearchFilter.supplier_id;
                }  
                if(SearchApprovalFilter.supplier_id > 0)
                {
                    DraftConfirmation.supplier_id = SearchApprovalFilter.supplier_id;
                } 
                if(SearchDispatchConfirmationFilter.supplier_id > 0)
                {
                    DraftConfirmation.supplier_id = SearchDispatchConfirmationFilter.supplier_id;
                }


                    viewModel.SupplierCountList(DraftConfirmation);

            }
            else
            {
                viewModel.LoadCountsCommand.Execute(null);
            }

            viewModel.LoadCountsCommand.Execute(null);

        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.LoadCountsCommand.Execute(txtSearch.Text);
        }
    }
}