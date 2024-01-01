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
using Rg.Plugins.Popup.Services;
using System.Timers;

namespace yarn_brokerage.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AddCommissionReceiptPage : ContentPage
    {
        public CommissionReceipt CommissionReceipt { get; set; }
        public ObservableCollection<CommissionReceiptDetail> _CommissionReceiptDetails { get; set; }

        CommissionReceiptViewModel viewModel;
        CommissionReceiptDetailViewModel detailViewModel;
        public DateTime date { get; set; }
        public SearchFilter _searchFilter { get; set; }
        public Indexes _enquiry { get; set; }
        public Approval Approval { get; set; }
        ApprovalViewModel approvalViewModel;
        public AddCommissionReceiptPage(CommissionReceipt _CommissionReceipt, SearchFilter searchFilter = null, Indexes enquiry = null)
        {
            InitializeComponent();
            
            viewModel = new CommissionReceiptViewModel();
            approvalViewModel = new ApprovalViewModel();
            _searchFilter = searchFilter;
            _enquiry = enquiry;            
            if (_CommissionReceipt == null)
            {
                CommissionReceipt = new CommissionReceipt();
                Title = "Add Commission Receipt";
                CommissionReceipt.receipt_date = DateTime.Now.ToLocalTime();
                CommissionReceipt.ledger_type = 1;
                CommissionReceipt.commission_type_string = "%";
                lblReceiptNo.Text = "New";
                ////CommissionReceipt.dispatch_from_date = DateTime.Now.ToLocalTime();
                ////CommissionReceipt.dispatch_to_date = DateTime.Now.ToLocalTime();
                ////CommissionReceipt.payment_date = DateTime.Now.ToLocalTime();
                //CommissionReceipt.unit = "BAGS";
                //CommissionReceipt.per = "/ KG";                
                //if (searchFilter != null)
                //{
                //    if (searchFilter.transaction_type == 2)
                //    {
                //        CommissionReceipt.supplier_id = searchFilter.ledger_id;
                //        CommissionReceipt.supplier_name = searchFilter.ledger_name;
                //        if (enquiry != null)
                //        {
                //            CommissionReceipt.customer_id = enquiry.ledger_id;
                //            CommissionReceipt.customer_name = enquiry.ledger_name;
                //        }
                //    }
                //    else if (searchFilter.transaction_type == 1)
                //    {
                //        CommissionReceipt.customer_id = searchFilter.ledger_id;
                //        CommissionReceipt.customer_name = searchFilter.ledger_name;
                //        if (enquiry != null)
                //        {
                //            CommissionReceipt.supplier_id = enquiry.ledger_id;
                //            CommissionReceipt.supplier_name = enquiry.ledger_name;
                //        }
                //    }
                //    if (enquiry != null)
                //    {
                //        CommissionReceipt.segment = enquiry.segment;
                //        CommissionReceipt.count_id = enquiry.count_id;
                //        CommissionReceipt.count_name = enquiry.count_name;                        
                //        CommissionReceipt.qty = enquiry.qty;
                //        CommissionReceipt.unit = enquiry.unit;
                //        CommissionReceipt.qty_unit = enquiry.qty_unit;
                //        CommissionReceipt.price = enquiry.current_price;
                //        CommissionReceipt.per = enquiry.per;
                //        CommissionReceipt.price_per = enquiry.price_per;
                //        CommissionReceipt.user_name = enquiry.user_name;
                //        CommissionReceipt.admin_user = enquiry.admin_user;

                //    }
                //    CommissionReceipt.enquiry_ids = searchFilter.id.ToString() + ", " + enquiry.id.ToString();                  
                //}
            }
            else
            {
                CommissionReceipt = _CommissionReceipt;            
                if (CommissionReceipt.ledger_type == 1) RdoSupplier_Clicked(null, null); else if (CommissionReceipt.ledger_type == 2) RdoCustomer_Clicked(null, null);
                Title = "Edit Commission Receipt";                
            }

            txtDateTime.Date = CommissionReceipt.receipt_date;
            
            lblDateTime.Text = "Receipt Date";

            //txtReceiptAmount.ReturnCommand = new Command(() => txtPrice.Focus());
            //detailViewModel =
            //if (CommissionReceipt.send_for_approval == 1)
            //{
            //    butSave.IsVisible = false;
            //    lblAddReceipt.IsVisible = false;
            //}

            BindingContext = detailViewModel  = new CommissionReceiptDetailViewModel();

            //detailViewModel.LoadItemsCommand.Execute(CommissionReceipt.id);
            //if (Convert.ToDouble(txtBagweight.Text) == 0)
            //    txtBagweight.Text = "";
            if (CommissionReceipt.id > 0)
            {
                //butApply.Text = (CommissionReceipt.commission_type == 1) ? "%" : "Fixed";
                txtReceiptAmount.Text = (CommissionReceipt.total_receipt_amount > 0) ? CommissionReceipt.total_receipt_amount.ToString() : "";
                lblReceiptNo.Text = CommissionReceipt.receipt_no.ToString();
                //txtPrice.Text = (CommissionReceipt.price > 0) ? CommissionReceipt.price.ToString() : "";               
                //txtConfirmationNo.Text = CommissionReceipt.confirmation_no;
            }
            if (Convert.ToDouble(txtReceiptAmount.Text) == 0)
                txtReceiptAmount.Text = "";
            
        }

        async void Save_Clicked(object sender, EventArgs e)
        {

            //CommissionReceipt.qty = (txtReceiptAmount.Text != "") ? Convert.ToDouble(txtReceiptAmount.Text) : 0;
            //CommissionReceipt.price = (txtPrice.Text != "") ? Convert.ToDecimal(txtPrice.Text) : 0;
            //CommissionReceipt.confirmation_no = (txtConfirmationNo.Text != "") ? txtConfirmationNo.Text : "";
            if (butSave.Text == "Save")
            {
                //    if (string.IsNullOrWhiteSpace(CommissionReceipt.confirmation_no))
                //    {
                //        await DisplayAlert("Alert", "Enter the Confirmation No...", "OK");
                //        return;
                //    }
                if (string.IsNullOrWhiteSpace(CommissionReceipt.ledger_name))
                {
                    await DisplayAlert("Alert", "Select Contact in list...", "OK");
                    return;
                }
                else if (string.IsNullOrWhiteSpace(CommissionReceipt.company_name))
                {
                    await DisplayAlert("Alert", "Please connect a company for this contact...", "OK");
                    return;
                }                
                else if (CommissionReceipt.total_receipt_amount <= 0)
                {
                    await DisplayAlert("Alert", "Receipt Amount should be grater than zero...", "OK");
                    txtReceiptAmount.Focus();
                    return;
                }
                int count = await detailViewModel.CommissionReceiptCount();
                if (count <= 0)
                {
                    await DisplayAlert("Alert", "Receipt details are empty...", "OK");
                    return;
                }
                //    else if (string.IsNullOrWhiteSpace(CommissionReceipt.supplier_name))
                //    {
                //        await DisplayAlert("Alert", "Select Supplier in list..", "OK");
                //        return;
                //    }
                //    else if (string.IsNullOrWhiteSpace(CommissionReceipt.count_name))
                //    {
                //        await DisplayAlert("Alert", "Select Count in list...", "OK");
                //        return;
                //    }
                //else if (CommissionReceipt.bag_weight <= 0)
                //{
                //    await DisplayAlert("Alert", "Enther the Bag Weight...", "OK");
                //    txtBagweight.Focus();
                //    return;
                //}
                //else if (CommissionReceipt.qty <= 0)
                //{
                //    await DisplayAlert("Alert", "Enter the Quantity...", "OK");
                //    txtReceiptAmount.Focus();
                //    return;
                //}
                //else if (CommissionReceipt.price <= 0)
                //{
                //    await DisplayAlert("Alert", "Enter the Price...", "OK");
                //    txtPrice.Focus();
                //    return;
                //}
                //if(await viewModel.DuplicateConfirmationNo(CommissionReceipt) > 0)
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
                //    if (Convert.ToInt32(txtReceiptAmount.Text) != TotalQty)
                //    {
                //        await DisplayAlert("Alert", "Confirmation Quantity and Dispatch Plan Quantity should be same...", "OK");
                //        txtPrice.Focus();
                //        return;
                //    }
                //}
                viewModel.SendForApproval(CommissionReceipt);
                await Navigation.PopAsync();
            }
        }

        async Task<int> SaveConfirmation(object sender, EventArgs e)
        {
            //if (RdoDomestic.IsChecked) CommissionReceipt.segment = 1; else if (RdoExport.IsChecked) CommissionReceipt.segment = 2;
            CommissionReceipt.total_adjusted_amount = TotalAmount;
            CommissionReceipt = await viewModel.StoreCommissionReceiptCommand(CommissionReceipt);
            //if (_searchFilter != null)
            //{
            //    CommissionReceipt_list dc_list = new CommissionReceipt_list();
            //    List<CommissionReceipt> dc = new List<CommissionReceipt>();
            //    dc.Add(CommissionReceipt);
            //    dc_list.CommissionReceipts = dc;
            //   // _enquiry.confirm_list = dc_list;
            //    _enquiry.confirmed = 1;
            //    //Application.Current.Properties["direct_confirmation"] = 1;
            //    //await Navigation.PopToRootAsync();
            //}
            //else
            if (CommissionReceipt.id > 0)
            {
                await detailViewModel.StoreCommissionReceiptCommand(CommissionReceipt);
                //Title = "Edit Commission Receipt";
                //control_enable(false);
                //lblReceiptNo.Text = CommissionReceipt.Receipt_no.ToString();
                //CommissionReceiptListView.IsVisible = true;
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
            txtLedgerName.Text = CommissionReceipt.ledger_name;
            txtCompanyName.Text = CommissionReceipt.company_name;
            //txtCountName.Text = CommissionReceipt.count_name;
            txtReceiptAmount.Text = (CommissionReceipt.total_receipt_amount > 0) ? CommissionReceipt.total_receipt_amount.ToString():"";
            ////txtPrice.Text = (CommissionReceipt.price > 0) ? CommissionReceipt.price.ToString():"";
            //butApply.Text = (CommissionReceipt.commission_type == 1) ? "%" : "Fixed";
            //butPrice.Text = CommissionReceipt.per.ToString();
            //txtConfirmationNo.Text = CommissionReceipt.confirmation_no;
            //if (CommissionReceipt.id > 0)
            //{
            //    ApprovalViewModel approvalViewModel = new ApprovalViewModel();
            //    approval = await approvalViewModel.GetApprovalAsync(CommissionReceipt.id);
            if (Approval != null)
            {
                //if (Approval.status == 0)
                //    grdStatus.IsVisible = false;
                //else
                //    grdStatus.IsVisible = true;
                //CommissionReceipt.status = Approval.status;
                //if (CommissionReceipt.status == 1)
                //{
                //    CommissionReceipt.status_image = "approved.png";                   
                //}
                //else if (CommissionReceipt.status == 5)
                //{
                //    CommissionReceipt.status_image = "rejected.png";                   
                //}
                ////CommissionReceipt.approved_at = Approval.approved_at;
                //CommissionReceipt.approved_user = Approval.approved_user;
                ////CommissionReceipt.rejected_at = Approval.rejected_at;
                //CommissionReceipt.rejected_user = Approval.rejected_user;
                //lblApproved.Source = CommissionReceipt.status_image;                
            }
            if (_CommissionReceiptDetails!=null)
            {
                if (_CommissionReceiptDetails.Count > 0)
                {
                //   if (await detailViewModel.checkduplicate(CommissionReceiptDetail.draft_confirmation_detail_id) == true)
                //        await DisplayAlert("Commssion Receipt","This Receipt already added in the Receipt detail","Ok");
                //    else
                    await detailViewModel.ExecuteCommissionReceiptDetailsCommand(CommissionReceipt, _CommissionReceiptDetails);                    
                }
            }
            else
            {
                await detailViewModel.ExecuteCommissionReceiptCommand(CommissionReceipt);
            }
            int count = await detailViewModel.CommissionReceiptCount();
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
            TotalAmount = await detailViewModel.CommissionReceiptTotalAmount();
            if (TotalAmount > 0)
                lblReceiptDetails.Text = "Receipt Details ( Rs. " + TotalAmount.ToString("0.00") + " )";
            else
                lblReceiptDetails.Text = "Receipt Details";
            detailViewModel.TotalBalanceAmount(CommissionReceipt);
            //CommissionReceiptListView.HeightRequest = count * 120;
        }

        private void control_enable(bool enable)
        {
           // txtDateTime.IsEnabled = enable;
            RdoSupplier.IsEnabled = enable;
            RdoCustomer.IsEnabled = enable;
            txtLedgerName.IsEnabled = enable;
            //txtReceiptAmount.IsEnabled = enable;
            //butApply.IsEnabled = enable;           
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
            //        CommissionReceipt.bag_weight = 0;
            //    }
            //}
            //else
            //{
            //    CommissionReceipt.bag_weight = 0;
            //}
        }

        private void txtReceiptAmount_Focused(object sender, FocusEventArgs e)
        {
            if (txtReceiptAmount.Text.Trim() != "")
            {
                txtReceiptAmount.Text = string.Format("{0:0.00}", Convert.ToDouble(txtReceiptAmount.Text));
                if (Convert.ToDouble(txtReceiptAmount.Text) == 0)
                {
                    txtReceiptAmount.Text = "";
                    //CommissionReceipt.qty = 0;
                }
                if (CommissionReceipt.total_receipt_amount != Convert.ToDouble(txtReceiptAmount.Text))
                {
                    CommissionReceipt.total_receipt_amount = (txtReceiptAmount.Text != "") ? Convert.ToDouble(txtReceiptAmount.Text) : 0;
                    detailViewModel.TotalBalanceAmount(CommissionReceipt);
                }
            }
            else
            {
                //CommissionReceipt.qty = 0;
            }
        }

        private async void LblApproved_Clicked(object sender, EventArgs e)
        {
            Approval = await approvalViewModel.GetApprovalAsync(CommissionReceipt.id);
            await Navigation.PushAsync(new AddApprovalPage(Approval));
        }

        private async void Log_Tapped(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new LogPage(CommissionReceipt));
            //await Navigation.PushAsync(new LogPage(approval));
        }


        private void txtDateTime_DateSelected(object sender, DateChangedEventArgs e)
        {
            CommissionReceipt.receipt_date = txtDateTime.Date;
        }

        private async void lblAddReceipt_Clicked(object sender, EventArgs e)
        {
            //CommissionReceipt.qty = (txtReceiptAmount.Text != "") ? Convert.ToDouble(txtReceiptAmount.Text) : 0;
            //CommissionReceipt.price = (txtPrice.Text != "") ? Convert.ToDecimal(txtPrice.Text) : 0;
            //CommissionReceipt.confirmation_no = (txtConfirmationNo.Text != "") ? txtConfirmationNo.Text : "";
            if (string.IsNullOrWhiteSpace(CommissionReceipt.ledger_name))
            {
                await DisplayAlert("Alert", "Select Contact in list...", "OK");
                return;
            }            
            else if (string.IsNullOrWhiteSpace(CommissionReceipt.company_name))
            {
                await DisplayAlert("Alert", "Please connect a company for this contact...", "OK");
                return;
            }
            //else if (CommissionReceipt.total_receipt_amount <= 0)
            //{
            //    await DisplayAlert("Alert", "Receipt amount should be grater than zero...", "OK");
            //    txtReceiptAmount.Focus();
            //    return;
            //}
            //else if (CommissionReceipt.price <= 0)
            //{
            //    await DisplayAlert("Alert", "Enter the Price...", "OK");
            //    txtPrice.Focus();
            //    return;
            //}
            //else if (await viewModel.DuplicateConfirmationNo(CommissionReceipt) > 0)
            //{
            //    await DisplayAlert("Alert", "Confirmation No is already exist...", "OK");
            //    txtConfirmationNo.Focus();
            //    return;
            //}
            //int i = await SaveConfirmation(null, null);
            _CommissionReceiptDetails = new ObservableCollection<CommissionReceiptDetail>();
            //_CommissionReceiptDetails.qty = Convert.ToInt32(txtReceiptAmount.Text) - TotalQty;
            //_CommissionReceiptDetails.balance_qty = Convert.ToInt32(txtReceiptAmount.Text) - TotalQty; 
            CommissionReceipt.exclude_commission_receipt_id = detailViewModel.ExcludeCommisionReceiptId();
            CommissionReceipt.total_receipt_amount = (txtReceiptAmount.Text != "") ? Convert.ToDouble(txtReceiptAmount.Text) : 0;
            await Navigation.PushAsync(new AddCommissionReceiptDetailPage(CommissionReceipt, _CommissionReceiptDetails));
            //await Navigation.PushModalAsync(new NavigationPage(new AddCommissionReceiptDetailPage(CommissionReceipt, _CommissionReceiptDetails)));
        }
        Timer timer;
        public CommissionReceiptDetail _CommissionReceiptDetail { get; set; }
        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            int Amend_flag = 0;
            var item = e.SelectedItem as CommissionReceiptDetail;
            // Manually deselect item.
            CommissionReceiptListView.SelectedItem = null;
            if (item == null)
                return;
            _CommissionReceiptDetail = item;
            await PopupNavigation.Instance.PushAsync(new ReceiptAmountPopupView(item));
            timer = new Timer();
            timer.Interval = 1; // 1 milliseconds  
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            //item.balance_qty = Convert.ToInt32(item.qty) +  Convert.ToInt32(txtReceiptAmount.Text) - TotalQty;
            //if (CommissionReceipt.send_for_approval == 0 && CommissionReceipt.status == 0)
            //{
            //    if (item.bags_ready == 1 || item.payment_ready == 1 || item.payment_received == 1 || item.transporter_ready == 1 || item.dispatched == 1 || item.program_approval == 1)
            //        Amend_flag = 1;
            //}
            //await Navigation.PushAsync(new EditCommissionReceiptDetailPage(CommissionReceipt, item, Amend_flag));
            //await Navigation.PushModalAsync(new NavigationPage(new EditCommissionReceiptDetailPage(CommissionReceipt, item)));
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (PopupNavigation.Instance.PopupStack.Count <= 0)
                {
                    timer.Stop();
                    detailViewModel.UpdateCommissionReceiptDetail(CommissionReceipt,_CommissionReceiptDetail);
                    //string Since = PatientDiseaseDetails.SincePeriod;
                    //ButSince.Text = Since;
                    //ButTablet.Text = PatientDiseaseDetails.TabletPeriod;
                    //ButInsulin.Text = PatientDiseaseDetails.InsulinPeriod;
                    //BindingContext = this;
                }
            });
        }

        private void RdoSupplier_Clicked(object sender, EventArgs e)
        {
            lblcontactname.Text = "Supplier";
            txtLedgerName.Placeholder = "Supplier Name";
            RdoSupplier.IsChecked = true;
            RdoCustomer.IsChecked = false;
            if (CommissionReceipt.ledger_type == 2)
            {
                ButLedgerClear_Clicked(null, null);
            }
            CommissionReceipt.ledger_type = 1;
        }

        private void RdoCustomer_Clicked(object sender, EventArgs e)
        {
            lblcontactname.Text = "Customer";
            txtLedgerName.Placeholder = "Customer Name";
            RdoCustomer.IsChecked = true;
            RdoSupplier.IsChecked = false;
            if(CommissionReceipt.ledger_type == 1)
            {
                ButLedgerClear_Clicked(null, null);
            }
            CommissionReceipt.ledger_type = 2;
        }

        private async void TxtLedgerName_Focused(object sender, EventArgs e)
        {
            if (txtLedgerName.IsEnabled == false)
                return;
            await Navigation.PushAsync(new LedgersListPage(null, CommissionReceipt.ledger_type, null, null, null, null, null, null, 0, null, CommissionReceipt));            
        }

        private void ButLedgerClear_Clicked(object sender, EventArgs e)
        {
            txtLedgerName.Text = "";
            txtReceiptAmount.Text = "";
            txtCompanyName.Text = "";
            CommissionReceipt.ledger_id = 0;
            CommissionReceipt.ledger_name = "";            
            CommissionReceipt.total_receipt_amount = 0;
            CommissionReceipt.company_id = null;
            CommissionReceipt.company_name = null;
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
                    await detailViewModel.DeleteCommissionReceiptDetailsCommand(CommissionReceipt, draft_confirmation_detail_id);
                    int count = await detailViewModel.CommissionReceiptCount();
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
                    TotalAmount = await detailViewModel.CommissionReceiptTotalAmount();
                    if (TotalAmount > 0)
                        lblReceiptDetails.Text = "Receipt Details ( Rs. " + TotalAmount.ToString("0.00") + " )";
                    else
                        lblReceiptDetails.Text = "Receipt Details";
                    detailViewModel.TotalBalanceAmount(CommissionReceipt);
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

        private async void butApply_Clicked(object sender, EventArgs e)
        {
            CommissionReceipt.total_receipt_amount = (txtReceiptAmount.Text != "") ? Convert.ToDouble(txtReceiptAmount.Text) : 0;
            if (CommissionReceipt.total_receipt_amount <= 0)
            {
                await DisplayAlert("Alert", "Receipt amount should be grater than zero...", "OK");
                txtReceiptAmount.Focus();
                return;
            }
            detailViewModel.splitReceiptAmount(CommissionReceipt);
        }

        private void txtReceiptAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtReceiptAmount.Text.Trim() != "")
            {
                if (CommissionReceipt.total_receipt_amount != Convert.ToDouble(txtReceiptAmount.Text))
                {
                    CommissionReceipt.total_receipt_amount = (txtReceiptAmount.Text != "") ? Convert.ToDouble(txtReceiptAmount.Text) : 0;
                    detailViewModel.TotalBalanceAmount(CommissionReceipt);
                }
            }
        }
    }
}