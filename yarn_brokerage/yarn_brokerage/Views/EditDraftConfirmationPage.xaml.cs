using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using yarn_brokerage.Models;
using yarn_brokerage.ViewModels;
namespace yarn_brokerage.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class EditDraftConfirmationPage : ContentPage
    {
        public DraftConfirmation DraftConfirmation { get; set; }
        public DraftConfirmationDetails DraftConfirmationDetails { get; set; }

        DraftConfirmationViewModel viewModel;
        DraftConfirmationDetailViewModel detailViewModel;
        public DateTime date { get; set; }
        public SearchFilter _searchFilter { get; set; }
        public Indexes _enquiry { get; set; }
        public Approval Approval { get; set; }
        ApprovalViewModel approvalViewModel;
        int AmendFlag;
        public EditDraftConfirmationPage(DraftConfirmation draftConfirmation, SearchFilter searchFilter = null, Indexes enquiry = null, int amend_flag=0)
        {
            InitializeComponent();            
            viewModel = new DraftConfirmationViewModel();
            approvalViewModel = new ApprovalViewModel();
            _searchFilter = searchFilter;
            _enquiry = enquiry;
            AmendFlag = amend_flag;
            if (draftConfirmation == null)
            {
                DraftConfirmation = new DraftConfirmation();

                Title = "Add Confirmation";
                DraftConfirmation.transaction_date_time = DateTime.Now.ToLocalTime();
                //DraftConfirmation.dispatch_from_date = DateTime.Now.ToLocalTime();
                //DraftConfirmation.dispatch_to_date = DateTime.Now.ToLocalTime();
                //DraftConfirmation.payment_date = DateTime.Now.ToLocalTime();
                DraftConfirmation.unit = "BAGS";
                DraftConfirmation.per = "/ KG";
                if (searchFilter != null)
                {
                    if (searchFilter.transaction_type == 2)
                    {
                        DraftConfirmation.supplier_id = searchFilter.ledger_id;
                        DraftConfirmation.supplier_name = searchFilter.ledger_name;
                        if (enquiry != null)
                        {
                            DraftConfirmation.customer_id = enquiry.ledger_id;
                            DraftConfirmation.customer_name = enquiry.ledger_name;
                        }
                    }
                    else if (searchFilter.transaction_type == 1)
                    {
                        DraftConfirmation.customer_id = searchFilter.ledger_id;
                        DraftConfirmation.customer_name = searchFilter.ledger_name;
                        if (enquiry != null)
                        {
                            DraftConfirmation.supplier_id = enquiry.ledger_id;
                            DraftConfirmation.supplier_name = enquiry.ledger_name;
                        }
                    }
                    if (enquiry != null)
                    {
                        DraftConfirmation.segment = enquiry.segment;
                        DraftConfirmation.count_id = enquiry.count_id;
                        DraftConfirmation.count_name = enquiry.count_name;
                        DraftConfirmation.qty = enquiry.qty;
                        DraftConfirmation.unit = enquiry.unit;
                        DraftConfirmation.qty_unit = enquiry.qty_unit;
                        DraftConfirmation.price = enquiry.current_price;
                        DraftConfirmation.per = enquiry.per;
                        DraftConfirmation.price_per = enquiry.price_per;
                        DraftConfirmation.user_name = enquiry.user_name;
                        DraftConfirmation.admin_user = enquiry.admin_user;

                    }
                    DraftConfirmation.enquiry_ids = searchFilter.id.ToString() + ", " + enquiry.id.ToString();
                }
            }
            else
            {
                DraftConfirmation = draftConfirmation;
                if (DraftConfirmation.segment == 1) RdoDomestic_Clicked(null, null); else if (DraftConfirmation.segment == 2) RdoExport_Clicked(null, null);
                Title = "Edit Confirmation";
            }

            txtDateTime.Date = DraftConfirmation.transaction_date_time;

            lblDateTime.Text = "Date";

            txtQty.ReturnCommand = new Command(() => txtPrice.Focus());
            //detailViewModel =
            if (DraftConfirmation.send_for_approval == 1)
            {
                butSave.IsVisible = false;
                lblAddDispatch.IsVisible = false;
            }
            if (AmendFlag == 1)
                LblAmend_Clicked(null,null);
            BindingContext = detailViewModel = new DraftConfirmationDetailViewModel();

            //detailViewModel.LoadItemsCommand.Execute(DraftConfirmation.id);
            //if (Convert.ToDouble(txtBagweight.Text) == 0)
            //    txtBagweight.Text = "";
            if (DraftConfirmation.id > 0)
            {
                txtQty.Text = (DraftConfirmation.qty > 0) ? DraftConfirmation.qty.ToString() : "";
                txtPrice.Text = (DraftConfirmation.price > 0) ? DraftConfirmation.price.ToString() : "";
                txtConfirmationNo.Text = DraftConfirmation.confirmation_no;
            }
            if (Convert.ToDouble(txtQty.Text) == 0)
                txtQty.Text = "";
            if (Convert.ToDouble(txtPrice.Text) == 0)
                txtPrice.Text = "";
        }

        async void Save_Clicked(object sender, EventArgs e)
        {

            DraftConfirmation.qty = (txtQty.Text != "") ? Convert.ToDouble(txtQty.Text) : 0;
            DraftConfirmation.price = (txtPrice.Text != "") ? Convert.ToDecimal(txtPrice.Text) : 0;
            DraftConfirmation.confirmation_no = (txtConfirmationNo.Text != "") ? txtConfirmationNo.Text : "";
            if (butSave.Text == "Save")
            {
                if (string.IsNullOrWhiteSpace(DraftConfirmation.confirmation_no))
                {
                    await DisplayAlert("Alert", "Enter the Confirmation No...", "OK");
                    return;
                }
                else if (string.IsNullOrWhiteSpace(DraftConfirmation.customer_name))
                {
                    await DisplayAlert("Alert", "Select Customer in list...", "OK");
                    return;
                }
                else if (string.IsNullOrWhiteSpace(DraftConfirmation.supplier_name))
                {
                    await DisplayAlert("Alert", "Select Supplier in list..", "OK");
                    return;
                }
                else if (string.IsNullOrWhiteSpace(DraftConfirmation.count_name))
                {
                    await DisplayAlert("Alert", "Select Count in list...", "OK");
                    return;
                }
                //else if (DraftConfirmation.bag_weight <= 0)
                //{
                //    await DisplayAlert("Alert", "Enther the Bag Weight...", "OK");
                //    txtBagweight.Focus();
                //    return;
                //}
                else if (DraftConfirmation.qty <= 0)
                {
                    await DisplayAlert("Alert", "Enter the Quantity...", "OK");
                    txtQty.Focus();
                    return;
                }
                else if (DraftConfirmation.price <= 0)
                {
                    await DisplayAlert("Alert", "Enter the Price...", "OK");
                    txtPrice.Focus();
                    return;
                }
                else if (await viewModel.DuplicateConfirmationNo(DraftConfirmation) > 0)
                {
                    await DisplayAlert("Alert", "Confirmation No is already exist...", "OK");
                    txtConfirmationNo.Focus();
                    return;
                }
                int i = await SaveConfirmation(sender, e);
            }
            else
            {              
                //if (Convert.ToInt32(txtQty.Text) != TotalQty)
                //{
                //    await DisplayAlert("Alert", "Confirmation Quantity and Dispatch Plan Quantity should be same...", "OK");
                //    txtPrice.Focus();
                //    return;
                //}
                viewModel.SendForApproval(DraftConfirmation);
                if (AmendFlag == 1)
                    await Navigation.PopToRootAsync();
                else
                    await Navigation.PopAsync();
            }
        }

        async Task<int> SaveConfirmation(object sender, EventArgs e)
        {
            if (RdoDomestic.IsChecked) DraftConfirmation.segment = 1; else if (RdoExport.IsChecked) DraftConfirmation.segment = 2;
            DraftConfirmation = await viewModel.StoreDraftConfirmationCommand(DraftConfirmation);
            if (_searchFilter != null)
            {
                DraftConfirmation_list dc_list = new DraftConfirmation_list();
                List<DraftConfirmation> dc = new List<DraftConfirmation>();
                dc.Add(DraftConfirmation);
                dc_list.draftConfirmations = dc;
                _enquiry.confirm_list = dc_list;
                _enquiry.confirmed = 1;
                //Application.Current.Properties["direct_confirmation"] = 1;
                //await Navigation.PopToRootAsync();
            }
            //else
            if (DraftConfirmation.id > 0)
            {
                Title = "Edit Confirmation";
                //DraftConfirmationListView.IsVisible = true;
                //grdDispatch.IsVisible = true;
            }
            if (sender != null)
                await Navigation.PopAsync();
            return 1;
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            if (AmendFlag == 1)
                await Navigation.PopToRootAsync();
            else
                await Navigation.PopAsync();
        }
        int TotalQty = 0;
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            txtSupplierName.Text = DraftConfirmation.supplier_name;
            txtCustomerName.Text = DraftConfirmation.customer_name;
            txtCountName.Text = DraftConfirmation.count_name;
            //txtQty.Text = (DraftConfirmation.qty > 0) ? DraftConfirmation.qty.ToString():"";
            //txtPrice.Text = (DraftConfirmation.price > 0) ? DraftConfirmation.price.ToString():"";
            butQty.Text = DraftConfirmation.unit.ToString();
            butPrice.Text = DraftConfirmation.per.ToString();
            //txtConfirmationNo.Text = DraftConfirmation.confirmation_no;
            //if (DraftConfirmation.id > 0)
            //{
            //    ApprovalViewModel approvalViewModel = new ApprovalViewModel();
            //    approval = await approvalViewModel.GetApprovalAsync(DraftConfirmation.id);
            if (Approval != null)
            {
                if (Approval.status == 0)
                    grdStatus.IsVisible = false;
                else
                    grdStatus.IsVisible = true;
                DraftConfirmation.status = Approval.status;
                if (DraftConfirmation.status == 1)
                {
                    DraftConfirmation.status_image = "approved.png";
                }
                else if (DraftConfirmation.status == 5)
                {
                    DraftConfirmation.status_image = "rejected.png";
                }
                //DraftConfirmation.approved_at = Approval.approved_at;
                DraftConfirmation.approved_user = Approval.approved_user;
                //DraftConfirmation.rejected_at = Approval.rejected_at;
                DraftConfirmation.rejected_user = Approval.rejected_user;
                lblApproved.Source = DraftConfirmation.status_image;
            }
            await detailViewModel.ExecuteDraftConfirmationDetailsCommand(DraftConfirmation.id);

            int count = await detailViewModel.DraftConfirmationCount();
            if (count > 0)
            {
                control_enable(false);
                butSave.Text = "Send For Approval";
            }
            else
            {
                control_enable(true);
                butSave.Text = "Save";
            }
            TotalQty = await detailViewModel.DraftConfirmationTotalQty();
            if (TotalQty > 0)
                lblDispatchDetails.Text = "Dispatch Details (" + TotalQty + " " + DraftConfirmation.unit + ")";
            else
                lblDispatchDetails.Text = "Dispatch Details";
            DraftConfirmationListView.HeightRequest = count * 57;
        }

        private void control_enable(bool enable)
        {
            txtDateTime.IsEnabled = enable;
            RdoDomestic.IsEnabled = enable;
            RdoExport.IsEnabled = enable;
            txtConfirmationNo.IsEnabled = enable;
            txtCustomerName.IsEnabled = enable;
            txtSupplierName.IsEnabled = enable;
            txtCountName.IsEnabled = enable;
            txtQty.IsEnabled = enable;
            butQty.IsEnabled = enable;
            txtPrice.IsEnabled = enable;
            butPrice.IsEnabled = enable;
            butSupplierClear.IsEnabled = enable;
            butCustomerClear.IsEnabled = enable;
        }

        private async void TxtCustomer_Focused(object sender, EventArgs e)
        {
            if (txtCustomerName.IsEnabled == false)
                return;
            await Navigation.PushAsync(new LedgersListPage(null, 2, null, DraftConfirmation));
        }

        private async void TxtCount_Focused(object sender, EventArgs e)
        {
            if (txtCountName.IsEnabled == false)
                return;
            await Navigation.PushAsync(new CountListPage(null, DraftConfirmation));
        }

        private void txtBagweight_Focused(object sender, FocusEventArgs e)
        {
            //if (txtBagweight.Text.Trim() != "")
            //{
            //    txtBagweight.Text = string.Format("{0:0}", Convert.ToDouble(txtBagweight.Text));
            //    if (Convert.ToDouble(txtBagweight.Text) == 0)
            //    {
            //        txtBagweight.Text = "";
            //        DraftConfirmation.bag_weight = 0;
            //    }
            //}
            //else
            //{
            //    DraftConfirmation.bag_weight = 0;
            //}
        }

        private void TxtQty_Focused(object sender, FocusEventArgs e)
        {
            if (txtQty.Text.Trim() != "")
            {
                txtQty.Text = string.Format("{0:0}", Convert.ToDouble(txtQty.Text));
                if (Convert.ToDouble(txtQty.Text) == 0)
                {
                    txtQty.Text = "";
                    DraftConfirmation.qty = 0;
                }
            }
            else
            {
                DraftConfirmation.qty = 0;
            }
        }

        private void TxtPrice_Focused(object sender, FocusEventArgs e)
        {
            if (txtPrice.Text.Trim() != "")
            {
                //txtPrice.Text = string.Format("{0:0.00}", Convert.ToDouble(txtPrice.Text));
                if (Convert.ToDouble(txtPrice.Text) == 0)
                {
                    txtPrice.Text = "";
                    DraftConfirmation.price = 0;
                }
            }
            else
            {
                DraftConfirmation.price = 0;
            }
        }

        private async void TxtSupplier_Focused(object sender, EventArgs e)
        {
            if (txtSupplierName.IsEnabled == false)
                return;
            await Navigation.PushAsync(new LedgersListPage(null, 1, null, DraftConfirmation));
        }

        private void RdoDomestic_Clicked(object sender, EventArgs e)
        {
            RdoDomestic.IsChecked = true;
            RdoExport.IsChecked = false;
            DraftConfirmation.segment = 1;
        }

        private void RdoExport_Clicked(object sender, EventArgs e)
        {
            RdoExport.IsChecked = true;
            RdoDomestic.IsChecked = false;
            DraftConfirmation.segment = 2;
        }

        private void ButQty_Clicked(object sender, EventArgs e)
        {
            if (butQty.Text == "BAGS")
            {
                butQty.Text = "FCL";
                DraftConfirmation.unit = "FCL";
            }
            else if (butQty.Text == "FCL")
            {
                butQty.Text = "BOX";
                DraftConfirmation.unit = "BOX";
            }
            else if (butQty.Text == "BOX")
            {
                butQty.Text = "PALLET";
                DraftConfirmation.unit = "PALLET";
            }
            else if (butQty.Text == "PALLET")
            {
                butQty.Text = "BALE";
                DraftConfirmation.unit = "BALE";
            }
            else
            {
                butQty.Text = "BAGS";
                DraftConfirmation.unit = "BAGS";
            }
        }

        private void ButPrice_Clicked(object sender, EventArgs e)
        {
            if (butPrice.Text == "/ KG")
            {
                butPrice.Text = "/ 5 KG";
                DraftConfirmation.per = "/ 5 KG";
            }
            else
            {
                butPrice.Text = "/ KG";
                DraftConfirmation.per = "/ KG";
            }
        }

        private async void LblApproved_Clicked(object sender, EventArgs e)
        {
            Approval = await approvalViewModel.GetApprovalAsync(DraftConfirmation.id);
            await Navigation.PushAsync(new AddApprovalPage(Approval));
        }

        private async void Log_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LogPage(DraftConfirmation));
            //await Navigation.PushAsync(new LogPage(approval));
        }

        private void ButCustomerClear_Clicked(object sender, EventArgs e)
        {
            txtCustomerName.Text = "";
            DraftConfirmation.customer_id = 0;
            DraftConfirmation.customer_name = "";
        }

        private void ButSupplierClear_Clicked(object sender, EventArgs e)
        {
            txtSupplierName.Text = "";
            DraftConfirmation.supplier_id = 0;
            DraftConfirmation.supplier_name = "";
        }

        private void txtDateTime_DateSelected(object sender, DateChangedEventArgs e)
        {
            DraftConfirmation.transaction_date_time = txtDateTime.Date;
        }

        private async void lblAddDispatch_Clicked(object sender, EventArgs e)
        {
            DraftConfirmation.qty = (txtQty.Text != "") ? Convert.ToDouble(txtQty.Text) : 0;
            DraftConfirmation.price = (txtPrice.Text != "") ? Convert.ToDecimal(txtPrice.Text) : 0;
            DraftConfirmation.confirmation_no = (txtConfirmationNo.Text != "") ? txtConfirmationNo.Text : "";
            if (string.IsNullOrWhiteSpace(DraftConfirmation.confirmation_no))
            {
                await DisplayAlert("Alert", "Enter the Confirmation No...", "OK");
                return;
            }
            else if (string.IsNullOrWhiteSpace(DraftConfirmation.customer_name))
            {
                await DisplayAlert("Alert", "Select Customer in list...", "OK");
                return;
            }
            else if (string.IsNullOrWhiteSpace(DraftConfirmation.supplier_name))
            {
                await DisplayAlert("Alert", "Select Supplier in list..", "OK");
                return;
            }
            else if (string.IsNullOrWhiteSpace(DraftConfirmation.count_name))
            {
                await DisplayAlert("Alert", "Select Count in list...", "OK");
                return;
            }
            else if (DraftConfirmation.qty <= 0)
            {
                await DisplayAlert("Alert", "Enter the Quantity...", "OK");
                txtQty.Focus();
                return;
            }
            else if (DraftConfirmation.price <= 0)
            {
                await DisplayAlert("Alert", "Enter the Price...", "OK");
                txtPrice.Focus();
                return;
            }
            else if (await viewModel.DuplicateConfirmationNo(DraftConfirmation) > 0)
            {
                await DisplayAlert("Alert", "Confirmation No is already exist...", "OK");
                txtConfirmationNo.Focus();
                return;
            }
            int i = await SaveConfirmation(null, null);
            DraftConfirmationDetails = new DraftConfirmationDetails();
            DraftConfirmationDetails.qty = Convert.ToInt32(txtQty.Text) - TotalQty;
            DraftConfirmationDetails.balance_qty = Convert.ToInt32(txtQty.Text) - TotalQty;
            await Navigation.PushAsync(new AddDraftConfirmationDetailPage(DraftConfirmation, DraftConfirmationDetails));
            //await Navigation.PushModalAsync(new NavigationPage(new AddDraftConfirmationDetailPage(DraftConfirmation, DraftConfirmationDetails)));
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            int Amend_flag = 0;
            var item = e.SelectedItem as DraftConfirmationDetails;
            // Manually deselect item.
            DraftConfirmationListView.SelectedItem = null;
            if (item == null)
                return;
            item.balance_qty = Convert.ToInt32(item.qty) + Convert.ToInt32(txtQty.Text) - TotalQty;
            if (DraftConfirmation.send_for_approval == 0 && DraftConfirmation.status == 0)
            {
                if (item.bags_ready == 1 || item.payment_ready == 1 || item.payment_received == 1 || item.transporter_ready == 1 || item.dispatched == 1 || item.program_approval==1 || item.amend == 0)
                    Amend_flag = 1;                
            }
            await Navigation.PushAsync(new EditDraftConfirmationDetailPage(DraftConfirmation, item, Amend_flag));
            
            //await Navigation.PushModalAsync(new NavigationPage(new EditDraftConfirmationDetailPage(DraftConfirmation, item)));
        }

        private async void LblAmend_Clicked(object sender, EventArgs e)
        {
            string message = await viewModel.rejectDraftConfirmation(DraftConfirmation.id);
            if(message == "sucess")
            {
                lblAddDispatch.IsVisible = true;
                butSave.IsVisible = true;
                DraftConfirmation.send_for_approval = 0;
                DraftConfirmation.status = 0;
            }
        }
    }
}