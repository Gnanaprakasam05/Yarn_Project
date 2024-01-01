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
    public partial class AddDispatchDeliveryDetailPage : ContentPage
    {

        public DispatchConfirm dispatchConfirmList { get; set; }
        public DraftConfirmation DraftConfirmation { get; set; }
        public DraftConfirmationDetails DraftConfirmationDetails { get; set; }
        public DraftConfirmation DraftConfirmationCheck { get; set; }
        public DraftConfirmationDispatchDelivery DraftConfirmationDispatchDelivery { get; set; }

        DraftConfirmationDispatchDeliveryDetailViewModel viewModel;
        public DateTime date { get; set; }
        int _ReportFlag = 0;
        int Todays_Plan = 0;
        public AddDispatchDeliveryDetailPage(DraftConfirmation draftConfirmation=null, DraftConfirmationDetails draftConfirmationDetails=null, DispatchConfirm dispatchConfirm= null, int AmendFlg =0, int ReportFlag =0, DraftConfirmation DraftConfirmation_Check = null )
        {
            InitializeComponent();

            DraftConfirmationCheck = DraftConfirmation_Check;
         

            _ReportFlag = ReportFlag;
            if(_ReportFlag == 1 && dispatchConfirm.program_approved == 1)
            {
                butSave.IsVisible = true;
                imgPaymentAdd.IsVisible = true;
            }
            else if (_ReportFlag == 1 && dispatchConfirm.program_approved == 0)
            {
                butSave.IsVisible = false;
                imgPaymentAdd.IsVisible = false;
            }
            DraftConfirmationDispatchDelivery = new DraftConfirmationDispatchDelivery();
            DraftConfirmation = draftConfirmation;

            if (draftConfirmation != null)
            {
                lblTransactionDetail.Text = draftConfirmation.transaction_detail;
                lblCustomerName.Text = draftConfirmation.customer_name;                
                lblSupplierName.Text = draftConfirmation.supplier_name;
                lblCountName.Text = draftConfirmation.count_name;                
                lblPricePer.Text = "Rs." + draftConfirmation.price_per;
                lblUserName.Text = draftConfirmation.user_name;
                if (draftConfirmation.admin_user == false)
                {
                    lblUserName.IsVisible = false;
                    imgUser.IsVisible = false;
                }
                if (draftConfirmation.send_for_approval == 1 || AmendFlg ==1)
                {
                    butSave.IsVisible = false;
                    imgPaymentAdd.IsVisible = false;
                }
                DraftConfirmationDispatchDelivery.draft_confirmation_id = draftConfirmation.id;
                DraftConfirmationDispatchDelivery.customer_id = draftConfirmation.customer_id;
                DraftConfirmationDispatchDelivery.supplier_id = draftConfirmation.supplier_id;
            }
            DraftConfirmationDetails = draftConfirmationDetails;
            if (draftConfirmationDetails != null)
            {
                DraftConfirmationDispatchDelivery.id = draftConfirmationDetails.id;
                txtNetAmount.Text = draftConfirmationDetails.invoice_value.ToString();
                DraftConfirmationDispatchDelivery.invoice_value = draftConfirmationDetails.invoice_value;
                lblQtyUnit.Text = draftConfirmationDetails.qty_unit;

                if (draftConfirmationDetails.bags_ready == 1) { chkBagsReady.IsChecked = true; DraftConfirmationDispatchDelivery.bags_ready = 1; ChkBagsReady_Clicked(null, null); }
                if (draftConfirmationDetails.payment_ready == 1) { chkPaymentReady.IsChecked = true; DraftConfirmationDispatchDelivery.payment_ready = 1; ChkPaymentReady_Clicked(null, null); }
                if (draftConfirmationDetails.payment_received == 1) { chkPaymentReceived.IsChecked = true; DraftConfirmationDispatchDelivery.payment_received = 1; ChkPaymentReceived_Clicked(null, null); }
                if (draftConfirmationDetails.transporter_ready == 1) { chkTransporterReady.IsChecked = true; DraftConfirmationDispatchDelivery.transporter_ready = 1; ChkTransporterReady_Clicked(null, null); }
                if (draftConfirmationDetails.dispatched == 1) { chkDispatched.IsChecked = true; DraftConfirmationDispatchDelivery.dispatched = 1; ChkDispatched_Clicked(null, null); }
                if (draftConfirmationDetails.invoice_details == 1) { chkInvoiceDetails.IsChecked = true; DraftConfirmationDispatchDelivery.invoice_details = 1; ChkInvoiceDetails_Clicked(null, null); }
                if (draftConfirmationDetails.delivered == 1) { chkDelivered.IsChecked = true; DraftConfirmationDispatchDelivery.delivered = 1; ChkDelivered_Clicked(null, null); }
                if (draftConfirmationDetails.customer_acknowledgement == 1) { chkCustomerAcknowledgement.IsChecked = true; DraftConfirmationDispatchDelivery.customer_acknowledgement = 1; ChkCustomerAcknowledgement_Clicked(null, null); }
                DraftConfirmationDispatchDelivery.transporter_id = Convert.ToInt32(draftConfirmationDetails.transporter_id);
                DraftConfirmationDispatchDelivery.transporter_name = draftConfirmationDetails.transporter_name;
                txtTransporterName.Text = draftConfirmationDetails.transporter_name;
                DraftConfirmationDispatchDelivery.godown_unloading_id = Convert.ToInt32(draftConfirmationDetails.godown_unloading_id);
                DraftConfirmationDispatchDelivery.godown_unloading_name = draftConfirmationDetails.godown_unloading_name;
                txtGodownUnloadingName.Text = draftConfirmationDetails.godown_unloading_name;
                txtLRNo.Text = draftConfirmationDetails.lr_number;

                if (draftConfirmationDetails.dispatched == 1)
                {
                    DraftConfirmationDispatchDelivery.dispatched_date = draftConfirmationDetails.dispatched_date;
                    DispatchDatePicker.Date = DraftConfirmationDispatchDelivery.dispatched_date;
                }
                else
                    DispatchDatePicker.Date = DateTime.Now.ToLocalTime();      


                txtDriverName.Text = draftConfirmationDetails.driver_name;
                txtLorryNo.Text = draftConfirmationDetails.truck_no;
                txtInvoiceNo.Text = draftConfirmationDetails.invoice_number;
                if (draftConfirmationDetails.invoice_details == 1)
                {
                    DraftConfirmationDispatchDelivery.invoice_date = draftConfirmationDetails.invoice_date;
                    InvoiceDatePicker.Date = DraftConfirmationDispatchDelivery.invoice_date;
                }
                else
                    InvoiceDatePicker.Date = DateTime.Now.ToLocalTime();



                txtAmount.Text = draftConfirmationDetails.invoice_amount.ToString();

                if (draftConfirmationDetails.delivered == 1)
                {
                    DraftConfirmationDispatchDelivery.delivery_date = draftConfirmationDetails.delivery_date;
                    DeliveryDatePicker.Date = DraftConfirmationDispatchDelivery.delivery_date;
                }
                else
                    DeliveryDatePicker.Date = DateTime.Now.ToLocalTime();

                txtRemarks.Text = draftConfirmationDetails.delivery_remarks;

                if (draftConfirmationDetails.customer_acknowledgement == 1)
                {
                    DraftConfirmationDispatchDelivery.acknowledgement_date = draftConfirmationDetails.acknowledgement_date;
                    CustomerAcknowledgementDatePicker.Date = DraftConfirmationDispatchDelivery.acknowledgement_date;
                }
                else
                    CustomerAcknowledgementDatePicker.Date = DateTime.Now.ToLocalTime();

                txtCustomerAcknowledgementRemarks.Text = draftConfirmationDetails.acknowledgement_remarks;
                DraftConfirmationDispatchDelivery.allow_credit_billing = draftConfirmationDetails.allow_credit_billing;
            }

            if(dispatchConfirm != null)
            {

                dispatchConfirmList = dispatchConfirm;
                lblTransactionDetail.Text = dispatchConfirm.transaction_detail;
                
                DraftConfirmationDispatchDelivery.id = dispatchConfirm.Id;
                DraftConfirmationDispatchDelivery.draft_confirmation_id = dispatchConfirm.draft_confirmation_id;
                DraftConfirmationDispatchDelivery.customer_id = dispatchConfirm.customer_id;
                DraftConfirmationDispatchDelivery.supplier_id = dispatchConfirm.supplier_id;

                lblCustomerName.Text = dispatchConfirm.customer_name;
                lblSupplierName.Text = dispatchConfirm.supplier_name;
                lblCountName.Text = dispatchConfirm.count_name;
                lblPricePer.Text = "Rs." + dispatchConfirm.price_per;
                lblUserName.Text = dispatchConfirm.user_name;
                if (dispatchConfirm.admin_user == false)
                {
                    lblUserName.IsVisible = false;
                    imgUser.IsVisible = false;
                }
                txtNetAmount.Text = dispatchConfirm.invoice_value.ToString();
                DraftConfirmationDispatchDelivery.invoice_value = Convert.ToDecimal(dispatchConfirm.invoice_value);
                lblQtyUnit.Text = dispatchConfirm.qty_unit;

                if (dispatchConfirm.bags_ready == 1) { chkBagsReady.IsChecked = true; chkBagsReady.IsEnabled = true; DraftConfirmationDispatchDelivery.bags_ready = 1; ChkBagsReady_Clicked(null, null); } else { chkBagsReady.IsEnabled = false; }
                if (dispatchConfirm.payment_ready == 1) { chkPaymentReady.IsChecked = true; chkPaymentReady.IsEnabled = true; DraftConfirmationDispatchDelivery.payment_ready = 1; ChkPaymentReady_Clicked(null, null); } else { chkPaymentReady.IsEnabled = false; }
                if (dispatchConfirm.payment_received == 1) { chkPaymentReceived.IsChecked = true; chkPaymentReceived.IsEnabled = true; DraftConfirmationDispatchDelivery.payment_received = 1; ChkPaymentReceived_Clicked(null, null); } else { chkPaymentReceived.IsEnabled = false; }
                if (dispatchConfirm.transporter_ready == 1) { chkTransporterReady.IsChecked = true; chkTransporterReady.IsEnabled = true;  DraftConfirmationDispatchDelivery.transporter_ready = 1; ChkTransporterReady_Clicked(null, null); } else { chkTransporterReady.IsEnabled = false; }
                if (dispatchConfirm.dispatched == 1) { chkDispatched.IsChecked = true; chkDispatched.IsEnabled = true; DraftConfirmationDispatchDelivery.dispatched = 1; ChkDispatched_Clicked(null, null); } else { chkDispatched.IsEnabled = false; }
                if (dispatchConfirm.invoice_details == 1) { chkInvoiceDetails.IsChecked = true; chkInvoiceDetails.IsEnabled = true; DraftConfirmationDispatchDelivery.invoice_details = 1; ChkInvoiceDetails_Clicked(null, null); } else { chkInvoiceDetails.IsEnabled = false; }
                if (dispatchConfirm.delivered == 1) { chkDelivered.IsChecked = true; chkDelivered.IsEnabled = true; DraftConfirmationDispatchDelivery.delivered = 1; ChkDelivered_Clicked(null, null); } else { chkDelivered.IsEnabled = false; }
                if (dispatchConfirm.customer_acknowledgement == 1) { chkCustomerAcknowledgement.IsChecked = true; chkCustomerAcknowledgement.IsEnabled = true; DraftConfirmationDispatchDelivery.customer_acknowledgement = 1; ChkCustomerAcknowledgement_Clicked(null, null); } else { chkCustomerAcknowledgement.IsEnabled = false; }

                DraftConfirmationDispatchDelivery.transporter_id = Convert.ToInt32(dispatchConfirm.transporter_id);
                DraftConfirmationDispatchDelivery.transporter_name = dispatchConfirm.transporter_name;
                txtTransporterName.Text = dispatchConfirm.transporter_name;

                DraftConfirmationDispatchDelivery.godown_unloading_id = Convert.ToInt32(dispatchConfirm.godown_unloading_id);
                DraftConfirmationDispatchDelivery.godown_unloading_name = dispatchConfirm.godown_unloading_name;
                txtGodownUnloadingName.Text = dispatchConfirm.godown_unloading_name;

                txtLRNo.Text = dispatchConfirm.lr_number;
                
                if (dispatchConfirm.dispatched == 1)
                {
                    DraftConfirmationDispatchDelivery.dispatched_date = dispatchConfirm.dispatched_date;
                    DispatchDatePicker.Date = DraftConfirmationDispatchDelivery.dispatched_date;
                }
                else
                    DraftConfirmationDispatchDelivery.dispatched_date = DispatchDatePicker.Date;

                txtDriverName.Text = dispatchConfirm.driver_name;
                txtLorryNo.Text = dispatchConfirm.truck_no;
                txtInvoiceNo.Text = dispatchConfirm.invoice_number;
                if (dispatchConfirm.invoice_details == 1)
                {
                    DraftConfirmationDispatchDelivery.invoice_date = dispatchConfirm.invoice_date;
                    InvoiceDatePicker.Date = DraftConfirmationDispatchDelivery.invoice_date;
                }
                else
                    DraftConfirmationDispatchDelivery.invoice_date = InvoiceDatePicker.Date;

                txtAmount.Text = dispatchConfirm.invoice_amount.ToString();

                if (dispatchConfirm.delivered == 1)
                {
                    DraftConfirmationDispatchDelivery.delivery_date = dispatchConfirm.delivery_date;
                    DeliveryDatePicker.Date = DraftConfirmationDispatchDelivery.delivery_date;
                }
                else
                    DraftConfirmationDispatchDelivery.delivery_date = DeliveryDatePicker.Date;

                txtRemarks.Text = dispatchConfirm.delivery_remarks;

                if (dispatchConfirm.customer_acknowledgement == 1)
                {
                    DraftConfirmationDispatchDelivery.acknowledgement_date = dispatchConfirm.acknowledgement_date;
                    CustomerAcknowledgementDatePicker.Date = DraftConfirmationDispatchDelivery.acknowledgement_date;
                }
                else
                    DraftConfirmationDispatchDelivery.acknowledgement_date = CustomerAcknowledgementDatePicker.Date;

                txtCustomerAcknowledgementRemarks.Text = dispatchConfirm.acknowledgement_remarks;
                DraftConfirmationDispatchDelivery.allow_credit_billing = dispatchConfirm.allow_credit_billing;
            }

            
            //if (DraftConfirmationDetails.id <= 0)
            //{
            //    DraftConfirmationDetails.dispatch_date = DateTime.Now.ToLocalTime();
            //    DraftConfirmationDetails.payment_date = DateTime.Now.ToLocalTime();
            //}



            //if (DraftConfirmationDetails.draft_confirmation_payment_detail.Count > 0)
            //    DraftConfirmationListView.HeightRequest = DraftConfirmationDetails.draft_confirmation_payment_detail.Count * 57;

            // txtDispatchDateTime.Date = DraftConfirmationDetails.dispatch_date;
            //txtPaymentDateTime.Date = DraftConfirmationDetails.payment_date;
            //if (DraftConfirmationDetails.rate_type == 0) RdoExMill_Clicked(null, null); else if (DraftConfirmationDetails.rate_type == 1) RdoNetRate_Clicked(null, null);
            //if (DraftConfirmationDetails.cancel_dispatch == 1) chkDispatch.IsChecked = true;

            BindingContext = viewModel = new DraftConfirmationDispatchDeliveryDetailViewModel(); 
            if (Convert.ToDouble(txtAmount.Text) == 0)
                txtAmount.Text = "";
            //if (Convert.ToDouble(txtFrieght.Text) == 0)
            //    txtFrieght.Text = "";
            //if (Convert.ToDouble(txtInsurance.Text) == 0)
            //    txtInsurance.Text = "";
            //if (Convert.ToDouble(txtOtherCharges.Text) == 0)
            //    txtOtherCharges.Text = "";
            //if (Convert.ToDouble(txtNoOfBoxes.Text) == 0)
            //    txtNoOfBoxes.Text = "";
            //if (Convert.ToDouble(txtWeightPerBox.Text) == 0)
            //    txtWeightPerBox.Text = "";
            //if (DraftConfirmationDetails.rate_type == 1)
            //    txtTaxPrec.Text = "";
        }

        async void Save_Clicked(object sender, EventArgs e)
        {

            DraftConfirmationDispatchDelivery.lr_number = (!string.IsNullOrWhiteSpace(txtLRNo.Text)) ? txtLRNo.Text : "";
            DraftConfirmationDispatchDelivery.driver_name = (!string.IsNullOrWhiteSpace(txtDriverName.Text)) ? txtDriverName.Text : "";
            DraftConfirmationDispatchDelivery.truck_no = (!string.IsNullOrWhiteSpace(txtLorryNo.Text)) ? txtLorryNo.Text : "";

            DraftConfirmationDispatchDelivery.invoice_number = (!string.IsNullOrWhiteSpace(txtInvoiceNo.Text)) ? txtInvoiceNo.Text : "";
            DraftConfirmationDispatchDelivery.invoice_amount = (!string.IsNullOrWhiteSpace(txtAmount.Text)) ? Convert.ToDecimal(txtAmount.Text) : 0;
            DraftConfirmationDispatchDelivery.delivery_remarks = (!string.IsNullOrWhiteSpace(txtRemarks.Text)) ? txtRemarks.Text : "";
            DraftConfirmationDispatchDelivery.acknowledgement_remarks = (!string.IsNullOrWhiteSpace(txtCustomerAcknowledgementRemarks.Text)) ? txtCustomerAcknowledgementRemarks.Text : "";

            if (DraftConfirmationDispatchDelivery.transporter_ready == 1)
            {
                if (DraftConfirmationDispatchDelivery.transporter_id <= 0)
                {
                    await DisplayAlert("Alert", "Please Select Any Transporter...", "OK");
                    //  txtQty.Focus();
                    return;
                }
            }

            if (DraftConfirmationDispatchDelivery.dispatched == 1)
            {
                if (string.IsNullOrWhiteSpace(DraftConfirmationDispatchDelivery.lr_number))
                {
                    await DisplayAlert("Alert", "Enter the LR Number...", "OK");
                    txtLRNo.Focus();
                    return;
                }
            }

            if (DraftConfirmationDispatchDelivery.invoice_details == 1)
            {
                if (string.IsNullOrWhiteSpace(DraftConfirmationDispatchDelivery.invoice_number))
                {
                    await DisplayAlert("Alert", "Enter the Invoice Number...", "OK");
                    txtInvoiceNo.Focus();
                    return;
                }
                if (DraftConfirmationDispatchDelivery.invoice_amount <= 0)
                {
                    await DisplayAlert("Alert", "Enter the Invoice Amount...", "OK");
                    txtLRNo.Focus();
                    return;
                }
            }
            
            if (DraftConfirmationDetails != null)
            {
                DraftConfirmationDetails.bags_ready = (chkBagsReady.IsChecked == true) ? 1 : 0;
                DraftConfirmationDetails.payment_ready = (chkPaymentReady.IsChecked == true) ? 1 : 0;
                DraftConfirmationDetails.payment_received = (chkPaymentReceived.IsChecked == true) ? 1 : 0;
                DraftConfirmationDetails.transporter_ready = (chkTransporterReady.IsChecked == true) ? 1 : 0;
                DraftConfirmationDetails.dispatched = (chkDispatched.IsChecked == true) ? 1 : 0;
                DraftConfirmationDetails.invoice_details = (chkInvoiceDetails.IsChecked == true) ? 1 : 0;
                DraftConfirmationDetails.delivered = (chkDelivered.IsChecked == true) ? 1 : 0;

                DraftConfirmationDetails.transporter_id = DraftConfirmationDispatchDelivery.transporter_id;
                DraftConfirmationDetails.transporter_name = (!string.IsNullOrWhiteSpace(txtTransporterName.Text)) ? txtTransporterName.Text : ""; 

                DraftConfirmationDetails.godown_unloading_id = DraftConfirmationDispatchDelivery.godown_unloading_id;
                DraftConfirmationDetails.godown_unloading_name = (!string.IsNullOrWhiteSpace(txtGodownUnloadingName.Text)) ? txtGodownUnloadingName.Text : ""; 

                DraftConfirmationDetails.lr_number = DraftConfirmationDispatchDelivery.lr_number;
                DraftConfirmationDetails.driver_name = DraftConfirmationDispatchDelivery.driver_name;
                DraftConfirmationDetails.truck_no = DraftConfirmationDispatchDelivery.truck_no;
                DraftConfirmationDetails.invoice_number = DraftConfirmationDispatchDelivery.invoice_number;
                
                if (DraftConfirmationDispatchDelivery.invoice_details == 1)
                {
                    DraftConfirmationDetails.invoice_date = DraftConfirmationDispatchDelivery.invoice_date;                    
                }
                DraftConfirmationDetails.invoice_amount = DraftConfirmationDispatchDelivery.invoice_amount;

                if (DraftConfirmationDispatchDelivery.delivered == 1)
                {
                    DraftConfirmationDetails.delivery_date = DraftConfirmationDispatchDelivery.delivery_date;                    
                }
                DraftConfirmationDetails.delivery_remarks = DraftConfirmationDispatchDelivery.delivery_remarks;
            }

            viewModel.StoreDraftConfirmationCommand(DraftConfirmationDispatchDelivery);

            if (dispatchConfirmList.lr_number == null)
            {
                dispatchConfirmList.lr_number = "";
            } 
            if (dispatchConfirmList.acknowledgement_remarks == null)
            {
                dispatchConfirmList.acknowledgement_remarks = "";
            }
            if (dispatchConfirmList.delivery_remarks == null)
            {
                dispatchConfirmList.delivery_remarks = "";
            }
            if (dispatchConfirmList.invoice_number == null)
            {
                dispatchConfirmList.invoice_number = "";
            }
            if (dispatchConfirmList.driver_name == null)
            {
                dispatchConfirmList.driver_name = "";
            } 
            if (dispatchConfirmList.truck_no == null)
            {
                dispatchConfirmList.truck_no = "";
            }



            if (    dispatchConfirmList.bags_ready != DraftConfirmationDispatchDelivery.bags_ready ||
                    dispatchConfirmList.payment_ready != DraftConfirmationDispatchDelivery.payment_ready ||
                    dispatchConfirmList.payment_received != DraftConfirmationDispatchDelivery.payment_received ||
                    dispatchConfirmList.transporter_ready != DraftConfirmationDispatchDelivery.transporter_ready ||
                    dispatchConfirmList.dispatched != DraftConfirmationDispatchDelivery.dispatched || 
                    dispatchConfirmList.invoice_details != DraftConfirmationDispatchDelivery.invoice_details ||
                    dispatchConfirmList.delivered != DraftConfirmationDispatchDelivery.delivered || 
                    dispatchConfirmList.acknowledgement_remarks != DraftConfirmationDispatchDelivery.acknowledgement_remarks ||
                    dispatchConfirmList.delivery_remarks != DraftConfirmationDispatchDelivery.delivery_remarks ||
                    Date_Check == true || acknowledgement_date == true || dispatched_date == true || invoice_date == true ||
                    dispatchConfirmList.invoice_amount != DraftConfirmationDispatchDelivery.invoice_amount ||
                    dispatchConfirmList.invoice_number != DraftConfirmationDispatchDelivery.invoice_number ||
                    dispatchConfirmList.godown_unloading_name != DraftConfirmationDispatchDelivery.godown_unloading_name ||
                    dispatchConfirmList.truck_no != DraftConfirmationDispatchDelivery.truck_no ||
                    dispatchConfirmList.driver_name != DraftConfirmationDispatchDelivery.driver_name ||
                    dispatchConfirmList.lr_number != DraftConfirmationDispatchDelivery.lr_number ||
                    dispatchConfirmList.transporter_name != DraftConfirmationDispatchDelivery.transporter_name)
            {
                DraftConfirmationCheck.Cancel_Click = 0;

                DraftConfirmationCheck.Save_Check = true;
            }
            else
            {
                DraftConfirmationCheck.Cancel_Click = 1;
                DraftConfirmationCheck.Save_Check = false;
               
            }







            await Navigation.PopAsync();
            //if (flg == false)
            //    await Navigation.PopAsync();
            //else
            //    flg = false;
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            DraftConfirmationCheck.Cancel_Click = 1;
            DraftConfirmationCheck.Cancel_Flag = true;

            await Navigation.PopAsync();
        }
        decimal TotalAmount;
        //decimal TotalUTRAmount;
        decimal TotalBalance;
        int count;
        bool flg = false;
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            txtTransporterName.Text = DraftConfirmationDispatchDelivery.transporter_name;            
            txtGodownUnloadingName.Text = DraftConfirmationDispatchDelivery.godown_unloading_name ;

            await viewModel.ExecuteDraftConfirmationDetailsCommand(DraftConfirmationDispatchDelivery);

            count = await viewModel.DraftConfirmationCount();
            //if (count > 0)
            //    control_enable(false);
            //else
            //    control_enable(true);
            TotalAmount = await viewModel.DraftConfirmationTotalAmount();
            //TotalUTRAmount = await viewModel.DraftConfirmationTotalUTRAmount();
            if (TotalAmount > 0)
                lblPaymentDetails.Text = "Payment Details (Rs." + TotalAmount + ")";
            else
                lblPaymentDetails.Text = "Payment Details";
            DraftConfirmationListView.HeightRequest = count * 57;
            if (Convert.ToDecimal(txtNetAmount.Text) == TotalAmount && count > 0) {
                chkPaymentReceived.IsEnabled = false;                
                chkPaymentReceived.IsChecked = true;
                DraftConfirmationDispatchDelivery.payment_received = 1;
                ChkPaymentReceived_Clicked(null, null);
                chkPaymentReady.IsEnabled = false;
                chkPaymentReady.IsChecked = true;
                DraftConfirmationDispatchDelivery.payment_ready = 1;
                ChkPaymentReady_Clicked(null, null);
                flg = true;
                //if (butSave.IsVisible == true)
                //    Save_Clicked(null, null);                
            }
            else
            {
                chkPaymentReady.IsEnabled = true;
                //DraftConfirmationDispatchDelivery.payment_received = 0;
                //chkPaymentReceived.IsChecked = false;
                flg = true;
                //if (butSave.IsVisible == true)
                //    Save_Clicked(null, null);
            }
            TotalBalance = viewModel.DraftConfirmationTotalBalance() - Convert.ToDecimal(txtNetAmount.Text);
            if (TotalBalance != 0)
            {
                lblExcessAmount.IsVisible = true;
                txtExcessAmount.IsVisible = true;
                lblExcessAmount.Text = (TotalBalance > 0) ? "Advance Balance" : "Pending Balance";
                txtExcessAmount.Text = (TotalBalance > 0) ? TotalBalance.ToString() : Math.Abs(TotalBalance).ToString();
            }
            else
            {
                lblExcessAmount.IsVisible = false;
                txtExcessAmount.IsVisible = false;
            }
            checkboxenable();

            DraftConfirmationCheck.Cancel_Click = 1;

            //if (DraftConfirmationDetails.id <= 0)
            //{
            //    InvoiceDetails invoiceDetails = await viewModel.CalculateInvoice(DraftConfirmation);
            //    if (DraftConfirmation.unit == "BAGS")
            //    {
            //        DraftConfirmationDetails.bag_weight = invoiceDetails.bag_weight;
            //        txtBagWeight.Text = DraftConfirmationDetails.bag_weight.ToString();
            //    }
            //    DraftConfirmationDetails.rate_type = invoiceDetails.rate_type;
            //    if (DraftConfirmationDetails.rate_type == 1)
            //        RdoNetRate_Clicked(null, null);
            //    else
            //        RdoExMill_Clicked(null, null);
            //    if (DraftConfirmation.segment == 1)
            //    {
            //        DraftConfirmationDetails.tax_id = invoiceDetails.domestic_tax_id;
            //        DraftConfirmationDetails.tax_prec = invoiceDetails.domestic_tax_perc;
            //    }
            //    else
            //    {
            //        DraftConfirmationDetails.tax_id = invoiceDetails.export_tax_id;
            //        DraftConfirmationDetails.tax_prec = invoiceDetails.export_tax_perc;
            //    }
            //    if (DraftConfirmationDetails.rate_type != 1)
            //        txtTaxPrec.Text = string.Format("{0} %", DraftConfirmationDetails.tax_prec);
            //}
            //CalculateNetAmount(null,null);
        }
        
        private void checkboxenable()
        {
            
            if (chkDispatched.IsChecked == true)
            {
                chkBagsReady.IsEnabled = false;
                chkTransporterReady.IsEnabled = false;
                chkDelivered.IsEnabled = true;
            }
            else if (chkDispatched.IsChecked == false)
            {
                if (chkInvoiceDetails.IsChecked == false)
                {
                    chkBagsReady.IsEnabled = true;
                    chkTransporterReady.IsEnabled = true;
                }
                chkDelivered.IsEnabled = false;
            }
                     
            if(chkDelivered.IsChecked == true)
            {
                chkDispatched.IsEnabled = false;
                chkCustomerAcknowledgement.IsEnabled = true;
            }
            else if(chkDelivered.IsChecked == false)
            {
                chkDispatched.IsEnabled = true;
                chkCustomerAcknowledgement.IsEnabled = false;
            }
            
            if (chkCustomerAcknowledgement.IsChecked == true)
            {
                chkDelivered.IsEnabled = false;                
            }
            else if (chkCustomerAcknowledgement.IsChecked == false)
            {
                if (chkDispatched.IsChecked != false) 
                    chkDelivered.IsEnabled = true; 
                
            }

            if (chkBagsReady.IsChecked == true && chkPaymentReady.IsChecked == true && chkPaymentReceived.IsChecked == true && chkTransporterReady.IsChecked == true)
            {
                if (chkDelivered.IsChecked == false) chkDispatched.IsEnabled = true;
                chkInvoiceDetails.IsEnabled = true;
            }
            if (chkBagsReady.IsChecked == true && chkTransporterReady.IsChecked == true && DraftConfirmationDispatchDelivery.allow_credit_billing==1)
            {
                if (chkDelivered.IsChecked == false) chkDispatched.IsEnabled = true;
                chkInvoiceDetails.IsEnabled = true;
            }
            else if (chkBagsReady.IsChecked == false || chkPaymentReady.IsChecked == false || chkPaymentReceived.IsChecked == false || chkTransporterReady.IsChecked == false)
            {
                chkDispatched.IsEnabled = false;
                chkInvoiceDetails.IsEnabled = false;
            }
            else if (chkBagsReady.IsChecked == false && chkPaymentReady.IsChecked == false && chkPaymentReceived.IsChecked == false && chkTransporterReady.IsChecked == false)
            {
                chkDispatched.IsEnabled = false;
                chkInvoiceDetails.IsEnabled = false;
            }
        }

        private void CalculateNetAmount(object sender, TextChangedEventArgs e)
        {
            //if (txtQty.Text == "")
            //    DraftConfirmationDetails.qty = 0;
            //if (txtFrieght.Text == "")
            //    DraftConfirmationDetails.frieght = 0;
            //if (txtInsurance.Text == "")
            //    DraftConfirmationDetails.insurance = 0;
            //if (txtOtherCharges.Text == "")
            //    DraftConfirmationDetails.other_charges = 0;
            //if (txtNoOfBoxes.Text == "" && DraftConfirmation.unit == "FCL")
            //    DraftConfirmationDetails.no_of_boxes = 0;
            //if (txtWeightPerBox.Text == "" && DraftConfirmation.unit == "FCL")
            //    DraftConfirmationDetails.bag_weight = 0;
            //DraftConfirmationDetails.qty = Convert.ToInt32(DraftConfirmationDetails.qty);
            //if(DraftConfirmationDetails.qty >0)txtQty.Text = DraftConfirmationDetails.qty.ToString();
            //DraftConfirmationDetails.gross_weight = ((DraftConfirmation.unit == "BAGS") ? DraftConfirmationDetails.bag_weight : DraftConfirmationDetails.bag_weight * DraftConfirmationDetails.no_of_boxes) * DraftConfirmationDetails.qty;
            //txtGrossWeight.Text = string.Format("{0:0.000}", DraftConfirmationDetails.gross_weight);
            //DraftConfirmationDetails.gross_amount = ((DraftConfirmation.unit == "BAGS") ? DraftConfirmationDetails.bag_weight : DraftConfirmationDetails.bag_weight * DraftConfirmationDetails.no_of_boxes) * DraftConfirmationDetails.qty * ((DraftConfirmation.per == "/ 5 KG") ? DraftConfirmation.price / 5 : DraftConfirmation.price);
            //txtGrossAmount.Text = string.Format("{0:0.00}", DraftConfirmationDetails.gross_amount);
            //if (DraftConfirmationDetails.rate_type != 1)
            //    DraftConfirmationDetails.tax_amount = DraftConfirmationDetails.gross_amount * (DraftConfirmationDetails.tax_prec / 100);            
            //txtTaxAmount.Text = string.Format("{0:0.00}", DraftConfirmationDetails.tax_amount);
            //DraftConfirmationDetails.invoice_value = DraftConfirmationDetails.gross_amount + DraftConfirmationDetails.tax_amount + DraftConfirmationDetails.frieght + DraftConfirmationDetails.insurance + DraftConfirmationDetails.other_charges;
            //txtNetAmount.Text = string.Format("{0:0.00}", DraftConfirmationDetails.invoice_value);
        }
        private async void TxtCustomer_Focused(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LedgersListPage(null,2,null,DraftConfirmation));
        }
        private async void TxtCount_Focused(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CountListPage(null,DraftConfirmation));
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
            //if (txtQty.Text.Trim() != "")
            //{
            //    txtQty.Text = string.Format("{0:0}", Convert.ToDouble(txtQty.Text));
            //    if (Convert.ToDouble(txtQty.Text) == 0)
            //    {
            //        txtQty.Text = "";
            //        DraftConfirmation.qty = 0;
            //    }
            //}
            //else
            //{
            //    DraftConfirmation.qty = 0;
            //}
        }
                

        private async void TxtSupplier_Focused(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LedgersListPage(null,1,null,DraftConfirmation));
        }

        private void RdoDomestic_Clicked(object sender, EventArgs e)
        {

        }

        private void RdoExport_Clicked(object sender, EventArgs e)
        {
           
        }

        private void ButQty_Clicked(object sender, EventArgs e)
        {
            //if (butQty.Text == "BAGS")
            //{
            //    butQty.Text = "FCL";
            //    DraftConfirmation.unit = "FCL";
            //}
            //else
            //{
            //    butQty.Text = "BAGS";
            //    DraftConfirmation.unit = "BAGS";
            //}
        }

        private void ButPrice_Clicked(object sender, EventArgs e)
        {
            //if (butPrice.Text == "/ KG")
            //{
            //    butPrice.Text = "/ 5 KG";
            //    DraftConfirmation.per = "/ 5 KG";
            //}
            //else
            //{
            //    butPrice.Text = "/ KG";
            //    DraftConfirmation.per = "/ KG";
            //}
        }

        private async void Log_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LogPage(DraftConfirmation));            
        }
        
        private void InvoiceDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            DraftConfirmationDispatchDelivery.invoice_date = InvoiceDatePicker.Date;

            DateTime checkDate = e.NewDate;

            if (dispatchConfirmList.invoice_date == DraftConfirmationDispatchDelivery.invoice_date)
            {
                invoice_date = false;
            }
            else
            {
                invoice_date = true;
            }

            //if (Todays_Plan != 14)
            //{
            //    if (checkDate.Equals(DraftConfirmationDispatchDelivery.invoice_date))
            //    {
            //        invoice_date = true;
            //    }
            //    else
            //    {
            //        invoice_date = false;
            //    }
            //}
            //else
            //{
            //    if (dispatchConfirmList.invoice_date == checkDate)
            //    {
            //        invoice_date = false;
            //    }
            //    else
            //    {
            //        invoice_date = true;
            //    }

            //}
        }

        private void DeliveryDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            DraftConfirmationDispatchDelivery.delivery_date = DeliveryDatePicker.Date;

            DateTime checkDate = e.NewDate;

            if (dispatchConfirmList.delivery_date == DraftConfirmationDispatchDelivery.delivery_date)
            {
                Date_Check = false;
            }
            else
            {
                Date_Check = true;
            }
            //if (Todays_Plan != 14)
            //{
            //    if (checkDate.Equals(DraftConfirmationDispatchDelivery.delivery_date))
            //    {
            //        Date_Check = true;
            //    }
            //    else
            //    {
            //        Date_Check = false;
            //    }
            //}
            //else
            //{
            //    if (dispatchConfirmList.delivery_date == checkDate)
            //    {
            //        Date_Check = false;
            //    }
            //    else
            //    {
            //        Date_Check = true;
            //    }

            //}

        }

      
        private async void Delete_Clicked(object sender, EventArgs e)
        {
            var result = await this.DisplayAlert("Attention!", "Do you want to delete this dispatch?", "Yes", "No");
            if (result)
            {
                try
                {
                  string message = await viewModel.DeleteDraftConfirmationDetailsCommand(DraftConfirmationDetails.id);
                }
                catch (Exception ex)
                {
                    
                }
                finally
                {
                    await Navigation.PopAsync();
                }
            }
        }
        private async void lblPaymentDetails_Clicked(object sender, EventArgs e)
        {            
            if(chkDispatched.IsChecked == true && DraftConfirmationDispatchDelivery.allow_credit_billing == 0)
            {
                await DisplayAlert("Alert", "Dispatch started. So, can't add payment", "Ok");
                return;
            }
            DraftConfirmationPayment draftConfirmationPayment = new DraftConfirmationPayment();
            draftConfirmationPayment.draft_confirmation_id = DraftConfirmationDispatchDelivery.draft_confirmation_id;
            draftConfirmationPayment.draft_confirmation_detail_id = DraftConfirmationDispatchDelivery.id;
            draftConfirmationPayment.amount = Convert.ToDecimal(txtNetAmount.Text) - TotalAmount;
            draftConfirmationPayment.invoice_amount = Convert.ToDecimal(txtNetAmount.Text) - TotalAmount;
            if (DraftConfirmation != null)
                draftConfirmationPayment.send_for_approval = DraftConfirmation.send_for_approval;
            await Navigation.PushAsync(new AddPaymentPage(draftConfirmationPayment, DraftConfirmationDispatchDelivery));
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (chkDispatched.IsChecked == true && DraftConfirmationDispatchDelivery.allow_credit_billing == 0)
            {
                await DisplayAlert("Alert", "Dispatch started. So, can't edit payment", "Ok");
                return;
            }

            var item = e.SelectedItem as DraftConfirmationPayment;
            // Manually deselect item.
            DraftConfirmationListView.SelectedItem = null;
            if (item == null)
                return;
            if (e.SelectedItemIndex + 1 != count)
            {
                await DisplayAlert("Alert", "Edit / Delete payment from last payment", "Ok");
                return;
            }
            if(item.utilized_amount > 0)
            {
                await DisplayAlert("Alert", "Advance Amount of this payment is used in another payment. So, can't edit this payment", "Ok");
                return;
            }
            if (DraftConfirmation != null)
                item.send_for_approval = DraftConfirmation.send_for_approval;
            item.invoice_amount = Convert.ToDecimal(txtNetAmount.Text) + item.invoice_amount - TotalAmount;
            await Navigation.PushAsync(new EditPaymentPage(item, DraftConfirmationDispatchDelivery));
        }
        
        private void ChkBagsReady_Clicked(object sender, EventArgs e)
        {
            if (chkBagsReady.IsEnabled == false)
                return;
            if (chkBagsReady.IsChecked == true)
                DraftConfirmationDispatchDelivery.bags_ready = 1;
            else
                DraftConfirmationDispatchDelivery.bags_ready = 0;
            checkboxenable();
        }

        private void ChkPaymentReady_Clicked(object sender, EventArgs e)
        {
            if (chkPaymentReady.IsEnabled == false)
                return;
            if (chkPaymentReady.IsChecked == true)
                DraftConfirmationDispatchDelivery.payment_ready = 1;
            else
                DraftConfirmationDispatchDelivery.payment_ready = 0;
            checkboxenable();
        }


        private void ChkPaymentReceived_Clicked(object sender, EventArgs e)
        {
            if (chkPaymentReceived.IsEnabled == false)
                return;
            if (chkPaymentReceived.IsChecked == true)
                DraftConfirmationDispatchDelivery.payment_received = 1;
            else
                DraftConfirmationDispatchDelivery.payment_received = 0;
            checkboxenable();
        }

        private void ChkTransporterReady_Clicked(object sender, EventArgs e)
        {
            if (chkTransporterReady.IsEnabled == false)
                return;
            if (chkTransporterReady.IsChecked == true)
                DraftConfirmationDispatchDelivery.transporter_ready = 1;
            else
                DraftConfirmationDispatchDelivery.transporter_ready = 0;
            checkboxenable();
        }

        private void ChkDispatched_Clicked(object sender, EventArgs e)
        {
            if (chkDispatched.IsEnabled == false)
                return;
            if (chkDispatched.IsChecked == true)
                DraftConfirmationDispatchDelivery.dispatched = 1;
            else
                DraftConfirmationDispatchDelivery.dispatched = 0;
            checkboxenable();
        }

        private void ChkInvoiceDetails_Clicked(object sender, EventArgs e)
        {
            if (chkInvoiceDetails.IsEnabled == false)
                return;
            if (chkInvoiceDetails.IsChecked == true)
                DraftConfirmationDispatchDelivery.invoice_details = 1;
            else
                DraftConfirmationDispatchDelivery.invoice_details = 0;
            checkboxenable();
        }

        private void ChkDelivered_Clicked(object sender, EventArgs e)
        {
            if (chkDelivered.IsEnabled == false)
                return;
            if (chkDelivered.IsChecked == true)
                DraftConfirmationDispatchDelivery.delivered = 1;
            else
                DraftConfirmationDispatchDelivery.delivered = 0;
            checkboxenable();
        }

        private async void TxtTransporter_Focused(object sender, EventArgs e)
        {
            if (txtTransporterName.IsEnabled == false)
                return;
            await Navigation.PushAsync(new TransporterListPage(DraftConfirmationDispatchDelivery));
        }

        private void ButTransporterClear_Clicked(object sender, EventArgs e)
        {
            txtTransporterName.Text = "";
            DraftConfirmationDispatchDelivery.transporter_id = 0;
            DraftConfirmationDispatchDelivery.transporter_name = "";
        }

        private async void TxtGodownUnloading_Focused(object sender, EventArgs e)
        {
            if (txtGodownUnloadingName.IsEnabled == false)
                return;
            await Navigation.PushAsync(new GodownUnloadingListPage(DraftConfirmationDispatchDelivery));
        }

        private void ButGodownUnloadingClear_Clicked(object sender, EventArgs e)
        {
            txtGodownUnloadingName.Text = "";
            DraftConfirmationDispatchDelivery.godown_unloading_id = 0;
            DraftConfirmationDispatchDelivery.godown_unloading_name = "";
        }


        private async void lblRemoveFromCurrentPlan_Clicked(object sender, EventArgs e)
        {
            if (_ReportFlag == 1)
                return;
            if (butSave.IsVisible == false)
                return;
            if (DraftConfirmation != null)
            {
                if (DraftConfirmation.send_for_approval == 1)
                    return;
            }
            if (chkBagsReady.IsChecked == true || chkPaymentReady.IsChecked == true || chkPaymentReceived.IsChecked == true || chkTransporterReady.IsChecked == true || chkDispatched.IsChecked == true || chkInvoiceDetails.IsChecked == true || chkDelivered.IsChecked == true)
            {
                await this.DisplayAlert("Info!", "Can't remove this plan because of Bags / Payment / Transproter / Dispatch process started..", "Ok");
                return;
            }
            var result = await this.DisplayAlert("Attention!", "Do you want to remove this plan from current plan?", "Yes", "No");
            if (result)
            {
                try
                {
                    //Save_Clicked(sender, e);
                    viewModel.RemoveFromCurrentPlan(DraftConfirmationDispatchDelivery);
                    DraftConfirmationCheck.Delete_Flag = true;
                    DraftConfirmationCheck.Cancel_Click = 0;
                    //if(message == "Sucess")
                    //{
                    //    DraftConfirmationCheck.Cancel_Click = 0;
                    //    DraftConfirmationCheck.Delete_Flag = true;
                    //}
                    //else
                    //{
                    //    DraftConfirmationCheck.Cancel_Click = 1;
                    //    DraftConfirmationCheck.Add_Flag = false;
                    //}
                }
                catch
                {

                }
                finally
                {
                    await Navigation.PopAsync();
                }
            }
        }
        bool Date_Check ;
        bool dispatched_date;
        private void DispatchDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            DraftConfirmationDispatchDelivery.dispatched_date = DispatchDatePicker.Date;

            DateTime checkDate = e.NewDate;

            if (dispatchConfirmList.dispatched_date == DraftConfirmationDispatchDelivery.dispatched_date)
            {
                dispatched_date = false;
            }
            else
            {
                dispatched_date = true;
            }
            //if (Todays_Plan != 14)
            //{
            //    if (checkDate.Equals(DraftConfirmationDispatchDelivery.dispatched_date))
            //    {
            //        dispatched_date = true;
            //    }
            //    else
            //    {
            //        dispatched_date = false;
            //    }
            //}
            //else
            //{
            //    if (dispatchConfirmList.acknowledgement_date == checkDate)
            //    {
            //        dispatched_date = false;
            //    }
            //    else
            //    {
            //        dispatched_date = true;
            //    }

            //}

        }

        private void ChkCustomerAcknowledgement_Clicked(object sender, EventArgs e)
        {
            if (chkCustomerAcknowledgement.IsEnabled == false)
                return;
            if (chkCustomerAcknowledgement.IsChecked == true)
                DraftConfirmationDispatchDelivery.customer_acknowledgement = 1;
            else
                DraftConfirmationDispatchDelivery.customer_acknowledgement = 0;
            checkboxenable();
        }
        bool acknowledgement_date;
        bool invoice_date;
        private void CustomerAcknowledgementDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            DraftConfirmationDispatchDelivery.acknowledgement_date = CustomerAcknowledgementDatePicker.Date;

            DateTime checkDate = e.NewDate;

            if (dispatchConfirmList.acknowledgement_date == DraftConfirmationDispatchDelivery.acknowledgement_date)
            {
                acknowledgement_date = false;
            }
            else
            {
                acknowledgement_date = true;
            }

            //if (Todays_Plan != 14)
            //{
            //    if (checkDate.Equals(DraftConfirmationDispatchDelivery.acknowledgement_date))
            //    {
            //        acknowledgement_date = true;
            //    }
            //    else
            //    {
            //        acknowledgement_date = false;
            //    }
            //}
            //else
            //{
            //    if (dispatchConfirmList.acknowledgement_date == checkDate)
            //    {
            //        acknowledgement_date = false;
            //    }
            //    else
            //    {
            //        acknowledgement_date = true;
            //    }

            //}

        }

    }
}