using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using yarn_brokerage.Models;
using yarn_brokerage.ViewModels;
using System.Collections.ObjectModel;

namespace yarn_brokerage.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AddCommissionInvoicePage : ContentPage
    {
        public CommissionInvoice CommissionInvoice { get; set; }
        public ObservableCollection<CommissionInvoiceDetail> _CommissionInvoiceDetails { get; set; }

        CommissionInvoiceViewModel viewModel;
        CommissionInvoiceDetailViewModel detailViewModel;
        public DateTime date { get; set; }
        public SearchFilter _searchFilter { get; set; }
        public Indexes _enquiry { get; set; }
        public Approval Approval { get; set; }
        ApprovalViewModel approvalViewModel;
        public AddCommissionInvoicePage(CommissionInvoice _CommissionInvoice, SearchFilter searchFilter = null, Indexes enquiry = null)
        {
            InitializeComponent();
            
            viewModel = new CommissionInvoiceViewModel();
            approvalViewModel = new ApprovalViewModel();
            _searchFilter = searchFilter;
            _enquiry = enquiry;            
            if (_CommissionInvoice == null)
            {
                CommissionInvoice = new CommissionInvoice();
                Title = "Add Commission Invoice";
                CommissionInvoice.invoice_date = DateTime.Now.ToLocalTime();
                CommissionInvoice.ledger_type = 1;
                CommissionInvoice.commission_type_string = "%";
                lblInvoiceNo.Text = "New";
                ////CommissionInvoice.dispatch_from_date = DateTime.Now.ToLocalTime();
                ////CommissionInvoice.dispatch_to_date = DateTime.Now.ToLocalTime();
                ////CommissionInvoice.payment_date = DateTime.Now.ToLocalTime();
                //CommissionInvoice.unit = "BAGS";
                //CommissionInvoice.per = "/ KG";                
                //if (searchFilter != null)
                //{
                //    if (searchFilter.transaction_type == 2)
                //    {
                //        CommissionInvoice.supplier_id = searchFilter.ledger_id;
                //        CommissionInvoice.supplier_name = searchFilter.ledger_name;
                //        if (enquiry != null)
                //        {
                //            CommissionInvoice.customer_id = enquiry.ledger_id;
                //            CommissionInvoice.customer_name = enquiry.ledger_name;
                //        }
                //    }
                //    else if (searchFilter.transaction_type == 1)
                //    {
                //        CommissionInvoice.customer_id = searchFilter.ledger_id;
                //        CommissionInvoice.customer_name = searchFilter.ledger_name;
                //        if (enquiry != null)
                //        {
                //            CommissionInvoice.supplier_id = enquiry.ledger_id;
                //            CommissionInvoice.supplier_name = enquiry.ledger_name;
                //        }
                //    }
                //    if (enquiry != null)
                //    {
                //        CommissionInvoice.segment = enquiry.segment;
                //        CommissionInvoice.count_id = enquiry.count_id;
                //        CommissionInvoice.count_name = enquiry.count_name;                        
                //        CommissionInvoice.qty = enquiry.qty;
                //        CommissionInvoice.unit = enquiry.unit;
                //        CommissionInvoice.qty_unit = enquiry.qty_unit;
                //        CommissionInvoice.price = enquiry.current_price;
                //        CommissionInvoice.per = enquiry.per;
                //        CommissionInvoice.price_per = enquiry.price_per;
                //        CommissionInvoice.user_name = enquiry.user_name;
                //        CommissionInvoice.admin_user = enquiry.admin_user;

                //    }
                //    CommissionInvoice.enquiry_ids = searchFilter.id.ToString() + ", " + enquiry.id.ToString();                  
                //}
            }
            else
            {
                CommissionInvoice = _CommissionInvoice;            
                if (CommissionInvoice.ledger_type == 1) RdoSupplier_Clicked(null, null); else if (CommissionInvoice.ledger_type == 2) RdoCustomer_Clicked(null, null);
                Title = "Edit Commission Invoice";                
            }

            txtDateTime.Date = CommissionInvoice.invoice_date;
            
            lblDateTime.Text = "Invoice Date";

            //txtCValue.ReturnCommand = new Command(() => txtPrice.Focus());
            //detailViewModel =
            //if (CommissionInvoice.send_for_approval == 1)
            //{
            //    butSave.IsVisible = false;
            //    lblAddInvoice.IsVisible = false;
            //}

            BindingContext = detailViewModel  = new CommissionInvoiceDetailViewModel();

            //detailViewModel.LoadItemsCommand.Execute(CommissionInvoice.id);
            //if (Convert.ToDouble(txtBagweight.Text) == 0)
            //    txtBagweight.Text = "";
            if (CommissionInvoice.id > 0)
            {
                butCType.Text = (CommissionInvoice.commission_type == 1) ? "%" : "Fixed";
                txtCValue.Text = (CommissionInvoice.commission_value > 0) ? CommissionInvoice.commission_value.ToString() : "";
                lblInvoiceNo.Text = CommissionInvoice.invoice_no.ToString();
                //txtPrice.Text = (CommissionInvoice.price > 0) ? CommissionInvoice.price.ToString() : "";               
                //txtConfirmationNo.Text = CommissionInvoice.confirmation_no;
            }
            if (Convert.ToDouble(txtCValue.Text) == 0)
                txtCValue.Text = "";
            
        }

        async void Save_Clicked(object sender, EventArgs e)
        {

            //CommissionInvoice.qty = (txtCValue.Text != "") ? Convert.ToDouble(txtCValue.Text) : 0;
            //CommissionInvoice.price = (txtPrice.Text != "") ? Convert.ToDecimal(txtPrice.Text) : 0;
            //CommissionInvoice.confirmation_no = (txtConfirmationNo.Text != "") ? txtConfirmationNo.Text : "";
            if (butSave.Text == "Save")
            {
                //    if (string.IsNullOrWhiteSpace(CommissionInvoice.confirmation_no))
                //    {
                //        await DisplayAlert("Alert", "Enter the Confirmation No...", "OK");
                //        return;
                //    }
                if (string.IsNullOrWhiteSpace(CommissionInvoice.company_name))
                {
                    await DisplayAlert("Alert", "Please connect a company for this contact...", "OK");
                    return;
                }
                else if (string.IsNullOrWhiteSpace(CommissionInvoice.ledger_name))
                {
                    await DisplayAlert("Alert", "Select Contact in list...", "OK");
                    return;
                }
                else if (CommissionInvoice.commission_value <= 0)
                {
                    await DisplayAlert("Alert", "Commission value should be grater than zero...", "OK");
                    txtCValue.Focus();
                    return;
                }
                int count = await detailViewModel.CommissionInvoiceCount();
                if (count <= 0)
                {
                    await DisplayAlert("Alert", "Invoice details are empty...", "OK");
                    return;
                }
                //    else if (string.IsNullOrWhiteSpace(CommissionInvoice.supplier_name))
                //    {
                //        await DisplayAlert("Alert", "Select Supplier in list..", "OK");
                //        return;
                //    }
                //    else if (string.IsNullOrWhiteSpace(CommissionInvoice.count_name))
                //    {
                //        await DisplayAlert("Alert", "Select Count in list...", "OK");
                //        return;
                //    }
                //else if (CommissionInvoice.bag_weight <= 0)
                //{
                //    await DisplayAlert("Alert", "Enther the Bag Weight...", "OK");
                //    txtBagweight.Focus();
                //    return;
                //}
                //else if (CommissionInvoice.qty <= 0)
                //{
                //    await DisplayAlert("Alert", "Enter the Quantity...", "OK");
                //    txtCValue.Focus();
                //    return;
                //}
                //else if (CommissionInvoice.price <= 0)
                //{
                //    await DisplayAlert("Alert", "Enter the Price...", "OK");
                //    txtPrice.Focus();
                //    return;
                //}
                //if(await viewModel.DuplicateConfirmationNo(CommissionInvoice) > 0)
                //{
                //    await DisplayAlert("Alert", "Confirmation No is already exist...", "OK");
                //    txtConfirmationNo.Focus();
                //    return;
                //}
                int i = await SaveConfirmation(sender, e);
            }
            else
            {
                //if (await detailViewModel.checkAmend() == false)
                //{
                //    if (Convert.ToInt32(txtCValue.Text) != TotalQty)
                //    {
                //        await DisplayAlert("Alert", "Confirmation Quantity and Dispatch Plan Quantity should be same...", "OK");
                //        txtPrice.Focus();
                //        return;
                //    }
                //}
                viewModel.SendForApproval(CommissionInvoice);
                await Navigation.PopAsync();
            }
        }

        async Task<int> SaveConfirmation(object sender, EventArgs e)
        {
            //if (RdoDomestic.IsChecked) CommissionInvoice.segment = 1; else if (RdoExport.IsChecked) CommissionInvoice.segment = 2;
            CommissionInvoice.total_commission = TotalAmount;
            CommissionInvoice = await viewModel.StoreCommissionInvoiceCommand(CommissionInvoice);
            //if (_searchFilter != null)
            //{
            //    CommissionInvoice_list dc_list = new CommissionInvoice_list();
            //    List<CommissionInvoice> dc = new List<CommissionInvoice>();
            //    dc.Add(CommissionInvoice);
            //    dc_list.CommissionInvoices = dc;
            //   // _enquiry.confirm_list = dc_list;
            //    _enquiry.confirmed = 1;
            //    //Application.Current.Properties["direct_confirmation"] = 1;
            //    //await Navigation.PopToRootAsync();
            //}
            //else
            if (CommissionInvoice.id > 0)
            {
                await detailViewModel.StoreCommissionInvoiceCommand(CommissionInvoice);
                //Title = "Edit Commission Invoice";
                //control_enable(false);
                //lblInvoiceNo.Text = CommissionInvoice.invoice_no.ToString();
                //CommissionInvoiceListView.IsVisible = true;
                //grdDispatch.IsVisible = true;
            }
            if (sender != null)
                await Navigation.PopAsync();
            return 1;
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        double TotalAmount = 0.00;
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            txtLedgerName.Text = CommissionInvoice.ledger_name;
            txtCompanyName.Text = CommissionInvoice.company_name;
            //txtCountName.Text = CommissionInvoice.count_name;
            txtCValue.Text = (CommissionInvoice.commission_value > 0) ? CommissionInvoice.commission_value.ToString():"";
            ////txtPrice.Text = (CommissionInvoice.price > 0) ? CommissionInvoice.price.ToString():"";
            butCType.Text = (CommissionInvoice.commission_type == 1) ? "%" : "Fixed";
            //butPrice.Text = CommissionInvoice.per.ToString();
            //txtConfirmationNo.Text = CommissionInvoice.confirmation_no;
            //if (CommissionInvoice.id > 0)
            //{
            //    ApprovalViewModel approvalViewModel = new ApprovalViewModel();
            //    approval = await approvalViewModel.GetApprovalAsync(CommissionInvoice.id);
            if (Approval != null)
            {
                //if (Approval.status == 0)
                //    grdStatus.IsVisible = false;
                //else
                //    grdStatus.IsVisible = true;
                //CommissionInvoice.status = Approval.status;
                //if (CommissionInvoice.status == 1)
                //{
                //    CommissionInvoice.status_image = "approved.png";                   
                //}
                //else if (CommissionInvoice.status == 5)
                //{
                //    CommissionInvoice.status_image = "rejected.png";                   
                //}
                ////CommissionInvoice.approved_at = Approval.approved_at;
                //CommissionInvoice.approved_user = Approval.approved_user;
                ////CommissionInvoice.rejected_at = Approval.rejected_at;
                //CommissionInvoice.rejected_user = Approval.rejected_user;
                //lblApproved.Source = CommissionInvoice.status_image;                
            }
            if (_CommissionInvoiceDetails!=null)
            {
                if (_CommissionInvoiceDetails.Count > 0)
                {
                //   if (await detailViewModel.checkduplicate(CommissionInvoiceDetail.draft_confirmation_detail_id) == true)
                //        await DisplayAlert("Commssion Invoice","This invoice already added in the invoice detail","Ok");
                //    else
                    await detailViewModel.ExecuteCommissionInvoiceDetailsCommand(_CommissionInvoiceDetails);                    
                }
            }
            else
            {
                await detailViewModel.ExecuteCommissionInvoiceCommand(CommissionInvoice);
            }
            int count = await detailViewModel.CommissionInvoiceCount();
            if (count > 0)
            {
                control_enable(false);
                //butSave.Text = "Send For Approval";
            }
            else
            {
                control_enable(true);
                //butSave.Text = "Save";
            }
         /*   TotalAmount = await detailViewModel.CommissionInvoiceTotalAmount();
            if (TotalAmount > 0)
                lblInvoiceDetails.Text = "Invoice Details ( Rs. " + TotalAmount.ToString("0.00") + " )";
            else*/
                if (CommissionInvoice.total_commission > 0)
                lblInvoiceDetails.Text = "Invoice Details( Rs. " + CommissionInvoice.total_commission.ToString("0.00") + " )";
            else
                lblInvoiceDetails.Text = "Invoice Details";
            //CommissionInvoiceListView.HeightRequest = count * 120;
        }

        private void control_enable(bool enable)
        {
            txtDateTime.IsEnabled = enable;
            RdoSupplier.IsEnabled = enable;
            RdoCustomer.IsEnabled = enable;
            txtLedgerName.IsEnabled = enable;
            //txtCValue.IsEnabled = enable;
            butCType.IsEnabled = enable;           
            butLedgerClear.IsEnabled = enable;
        }

        private void txtBagweight_Focused(object sender, FocusEventArgs e)
        {
            //if (txtBagweight.Text.Trim() != "")
            //{
            //    txtBagweight.Text = string.Format("{0:0}", Convert.ToDouble(txtBagweight.Text));
            //    if (Convert.ToDouble(txtBagweight.Text) == 0)
            //    {
            //        txtBagweight.Text = "";
            //        CommissionInvoice.bag_weight = 0;
            //    }
            //}
            //else
            //{
            //    CommissionInvoice.bag_weight = 0;
            //}
        }

        private void txtCValue_Focused(object sender, FocusEventArgs e)
        {
            if (txtCValue.Text.Trim() != "")
            {
                txtCValue.Text = string.Format("{0:0}", Convert.ToDouble(txtCValue.Text));
                if (Convert.ToDouble(txtCValue.Text) == 0)
                {
                    txtCValue.Text = "";
                    //CommissionInvoice.qty = 0;
                }
            }
            else
            {
                //CommissionInvoice.qty = 0;
            }
        }

        private async void LblApproved_Clicked(object sender, EventArgs e)
        {
            Approval = await approvalViewModel.GetApprovalAsync(CommissionInvoice.id);
            await Navigation.PushAsync(new AddApprovalPage(Approval));
        }

        private async void Log_Tapped(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new LogPage(CommissionInvoice));
            //await Navigation.PushAsync(new LogPage(approval));
        }


        private void txtDateTime_DateSelected(object sender, DateChangedEventArgs e)
        {
            CommissionInvoice.invoice_date = txtDateTime.Date;
        }

        private async void lblAddInvoice_Clicked(object sender, EventArgs e)
        {
            //CommissionInvoice.qty = (txtCValue.Text != "") ? Convert.ToDouble(txtCValue.Text) : 0;
            //CommissionInvoice.price = (txtPrice.Text != "") ? Convert.ToDecimal(txtPrice.Text) : 0;
            //CommissionInvoice.confirmation_no = (txtConfirmationNo.Text != "") ? txtConfirmationNo.Text : "";
            if (string.IsNullOrWhiteSpace(CommissionInvoice.company_name))
            {
                await DisplayAlert("Alert", "Please connect a company for this contact...", "OK");
                return;
            }
            else if (string.IsNullOrWhiteSpace(CommissionInvoice.ledger_name))
            {
                await DisplayAlert("Alert", "Select Contact in list...", "OK");
                return;
            }
            else if (CommissionInvoice.commission_value <= 0)
            {
                await DisplayAlert("Alert", "Commission value should be grater than zero...", "OK");
                txtCValue.Focus();
                return;
            }
            //else if (CommissionInvoice.price <= 0)
            //{
            //    await DisplayAlert("Alert", "Enter the Price...", "OK");
            //    txtPrice.Focus();
            //    return;
            //}
            //else if (await viewModel.DuplicateConfirmationNo(CommissionInvoice) > 0)
            //{
            //    await DisplayAlert("Alert", "Confirmation No is already exist...", "OK");
            //    txtConfirmationNo.Focus();
            //    return;
            //}
            //int i = await SaveConfirmation(null, null);
            _CommissionInvoiceDetails = new ObservableCollection<CommissionInvoiceDetail>();
            //_CommissionInvoiceDetails.qty = Convert.ToInt32(txtCValue.Text) - TotalQty;
            //_CommissionInvoiceDetails.balance_qty = Convert.ToInt32(txtCValue.Text) - TotalQty; 
            CommissionInvoice.exclude_commission_invoice_id = detailViewModel.ExcludeCommisionInvoiceId();
            await Navigation.PushAsync(new AddCommissionInvoiceDetailPage(CommissionInvoice, _CommissionInvoiceDetails));
            //await Navigation.PushModalAsync(new NavigationPage(new AddCommissionInvoiceDetailPage(CommissionInvoice, _CommissionInvoiceDetails)));
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            int Amend_flag = 0;
            var item = e.SelectedItem as CommissionInvoiceDetail;
            // Manually deselect item.
            CommissionInvoiceListView.SelectedItem = null;
            if (item == null)
                return;
            //item.balance_qty = Convert.ToInt32(item.qty) +  Convert.ToInt32(txtCValue.Text) - TotalQty;
            //if (CommissionInvoice.send_for_approval == 0 && CommissionInvoice.status == 0)
            //{
            //    if (item.bags_ready == 1 || item.payment_ready == 1 || item.payment_received == 1 || item.transporter_ready == 1 || item.dispatched == 1 || item.program_approval == 1)
            //        Amend_flag = 1;
            //}
            //await Navigation.PushAsync(new EditCommissionInvoiceDetailPage(CommissionInvoice, item, Amend_flag));
            //await Navigation.PushModalAsync(new NavigationPage(new EditCommissionInvoiceDetailPage(CommissionInvoice, item)));
        }

        private void RdoSupplier_Clicked(object sender, EventArgs e)
        {
            lblcontactname.Text = "Supplier";
            txtLedgerName.Placeholder = "Supplier Name";
            RdoSupplier.IsChecked = true;
            RdoCustomer.IsChecked = false;
            if (CommissionInvoice.ledger_type == 2)
            {
                ButLedgerClear_Clicked(null, null);
            }
            CommissionInvoice.ledger_type = 1;
        }

        private void RdoCustomer_Clicked(object sender, EventArgs e)
        {
            lblcontactname.Text = "Customer";
            txtLedgerName.Placeholder = "Customer Name";
            RdoCustomer.IsChecked = true;
            RdoSupplier.IsChecked = false;
            if(CommissionInvoice.ledger_type == 1)
            {
                ButLedgerClear_Clicked(null, null);
            }
            CommissionInvoice.ledger_type = 2;
        }

        private async void TxtLedgerName_Focused(object sender, EventArgs e)
        {
            if (txtLedgerName.IsEnabled == false)
                return;
            await Navigation.PushAsync(new LedgersListPage(null, CommissionInvoice.ledger_type, null, null, null, null, null, null, 0, CommissionInvoice));            
        }

        private void ButLedgerClear_Clicked(object sender, EventArgs e)
        {
            txtLedgerName.Text = "";
            txtCValue.Text = "";
            txtCompanyName.Text = "";
            CommissionInvoice.ledger_id = 0;
            CommissionInvoice.ledger_name = "";
            CommissionInvoice.commission_type = 1;
            CommissionInvoice.commission_value = 0;
            CommissionInvoice.company_id = null;
            CommissionInvoice.company_name = null;
        }

        private async void DeleteClicked(object sender, EventArgs e)
        {
            int draft_confirmation_detail_id = 0;
            var item = (Xamarin.Forms.Image)sender;
            if (item.GestureRecognizers.Count > 0)
            {
                var gesture = (TapGestureRecognizer)item.GestureRecognizers[0];
                draft_confirmation_detail_id = (int)gesture.CommandParameter;
            }
            var result = await this.DisplayAlert("Attention!", "Do you want to delete this item?", "Yes", "No");
            if (result)
            {
                try
                {
                    await detailViewModel.DeleteCommissionInvoiceDetailsCommand(CommissionInvoice, draft_confirmation_detail_id);
                    int count = await detailViewModel.CommissionInvoiceCount();
                    if (count > 0)
                    {
                        control_enable(false);
                        //butSave.Text = "Send For Approval";
                    }
                    else
                    {
                        control_enable(true);
                        //butSave.Text = "Save";
                    }
                    TotalAmount = await detailViewModel.CommissionInvoiceTotalAmount();
                    if (TotalAmount > 0)
                        lblInvoiceDetails.Text = "Invoice Details ( Rs. " + TotalAmount.ToString("0.00") + " )";
                    else
                        lblInvoiceDetails.Text = "Invoice Details";
                }
                catch (Exception ex)
                {
                    //OnAppearing();
                }
                finally
                {
                    //OnAppearing();
                }
            }
        }
    }
}