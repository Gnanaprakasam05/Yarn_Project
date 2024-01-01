using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
    public partial class EditDraftConfirmationDetailPage : ContentPage
    {

        public DraftConfirmation DraftConfirmation { get; set; }
        public DraftConfirmationDetails DraftConfirmationDetails { get; set; }

        public CheckDraftConfirmationDetails DraftConfirmationDetailsCheck { get; set; }

        DraftConfirmationDetailViewModel viewModel;
        int _Amendflag;
        public DateTime date { get; set; }
        public decimal Qty;
        public DateTime dispatch_date { get; set; }
        public DateTime payment_date { get; set; }
        public decimal qty { get; set; }
        public decimal bag_weight { get; set; }
        public decimal balance_qty { get; set; }
        public decimal gross_amount { get; set; }
        public decimal tax_prec { get; set; }
        public decimal insurance { get; set; }
        public decimal other_charges { get; set; }
        public decimal invoice_value { get; set; }
        public string remarks { get; set; }
        public decimal tax_amount { get; set; }
        public decimal frieght { get; set; }
        public decimal no_of_boxes { get; set; }
        public string unit { get; set; }
        public int flag { get; set; }
        public EditDraftConfirmationDetailPage(DraftConfirmation draftConfirmation, DraftConfirmationDetails draftConfirmationDetails, int Amend_flag = 2)
        {
            InitializeComponent();
            
            viewModel = new DraftConfirmationDetailViewModel();
                        
            if (draftConfirmation == null)
                DraftConfirmation = new DraftConfirmation();                
            else
                DraftConfirmation = draftConfirmation;


            if (draftConfirmationDetails == null)            
                DraftConfirmationDetails = new DraftConfirmationDetails();
            else
                DraftConfirmationDetails = draftConfirmationDetails;


            
                dispatch_date = draftConfirmationDetails.dispatch_date;
                payment_date = draftConfirmationDetails.payment_date;
                qty = draftConfirmationDetails.qty;
                bag_weight = draftConfirmationDetails.bag_weight;
                no_of_boxes = draftConfirmationDetails.no_of_boxes;
                gross_amount = draftConfirmationDetails.gross_amount;
                tax_prec = draftConfirmationDetails.tax_prec;
                tax_amount = draftConfirmationDetails.tax_amount;
                insurance = draftConfirmationDetails.insurance;
                other_charges = draftConfirmationDetails.other_charges;
                invoice_value = draftConfirmationDetails.invoice_value;
                remarks = draftConfirmationDetails.remarks;
               frieght = draftConfirmationDetails.frieght;















                _Amendflag = Amend_flag;
            Qty = DraftConfirmationDetails.balance_qty;
            grdBags.IsVisible = DraftConfirmation.unit == "BAGS" || DraftConfirmation.unit == "BALE" || DraftConfirmation.unit == "BOX";
            grdFCL.IsVisible = DraftConfirmation.unit == "FCL" || DraftConfirmation.unit == "PALLET";
            if (DraftConfirmation.unit == "PALLET")
            {
                lblNoOfBoxes.Text = "No of Cones";
                lblWeightPerBox.Text = "Weight Per Cone";
            }

            if (DraftConfirmation.unit == "BALE" || DraftConfirmation.unit == "BOX")
            {
                txtBagWeight.IsEnabled = true;
                lblBagWeight.Text = (DraftConfirmation.unit == "BALE") ? "Bale Weight" : "Box Weight";
            }

            if (DraftConfirmationDetails.id <= 0)
            {
                DraftConfirmationDetails.dispatch_date = DateTime.Now.ToLocalTime();
                DraftConfirmationDetails.payment_date = DateTime.Now.ToLocalTime();
            }

            txtDispatchDateTime.Date = DraftConfirmationDetails.dispatch_date;
            txtPaymentDateTime.Date = DraftConfirmationDetails.payment_date;
            if (DraftConfirmationDetails.rate_type == 0) RdoExMill_Clicked(null, null); else if (DraftConfirmationDetails.rate_type == 1) RdoNetRate_Clicked(null, null);
            if (DraftConfirmationDetails.cancel_dispatch == 1) { chkDispatch.IsChecked = true; txtRemarks.IsVisible = true; lblRemarks.IsVisible = true; };
            if (DraftConfirmation.send_for_approval == 1 || _Amendflag == 1)
                butSave.IsVisible = false;

            txtRemarks.Text = DraftConfirmationDetails.remarks;

            BindingContext = this;
            if (Convert.ToDouble(txtQty.Text) == 0)
                txtQty.Text = "";
            if (Convert.ToDouble(txtFrieght.Text) == 0)
                txtFrieght.Text = "";
            if (Convert.ToDouble(txtInsurance.Text) == 0)
                txtInsurance.Text = "";
            if (Convert.ToDouble(txtOtherCharges.Text) == 0)
                txtOtherCharges.Text = "";
            if (Convert.ToDouble(txtNoOfBoxes.Text) == 0)
                txtNoOfBoxes.Text = "";
            if (Convert.ToDouble(txtWeightPerBox.Text) == 0)
                txtWeightPerBox.Text = "";
            if (DraftConfirmationDetails.rate_type == 1)
                txtTaxPrec.Text = "";
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (_Amendflag == 2)
            {
                if (DraftConfirmationDetails.qty > Qty)
                {
                    await DisplayAlert("alert", "Quantity should not exceed " + Qty + "...", "ok");
                    txtQty.Focus();
                    return;
                }
            }
            if (DraftConfirmationDetails.qty <= 0)
            {
                await DisplayAlert("alert", "Enter the quantity...", "ok");
                txtQty.Focus();
                return;
            }
            else if (DraftConfirmationDetails.bag_weight <= 0 && grdBags.IsVisible == true)
            {
                await DisplayAlert("alert", "Enter the weight...", "ok");
                txtBagWeight.Focus();
                return;
            }
            else if (DraftConfirmationDetails.tax_id <= 0)
            {
                await DisplayAlert("Alert", "Please set tax for this count...", "OK");
                return;
            }
            else if (DraftConfirmationDetails.invoice_value <= 0)
            {
                await DisplayAlert("Alert", "Invalid Invoice Value...", "OK");
                txtQty.Focus();
                return;
            }
            DraftConfirmationDetails.draft_confirmation_id = DraftConfirmation.id;
            DraftConfirmationDetails.remarks = (!string.IsNullOrWhiteSpace(txtRemarks.Text)) ? txtRemarks.Text : ""; 
            string message = await viewModel.StoreDraftConfirmationCommand(DraftConfirmationDetails);


            DraftConfirmation.Flag_Check = 1;
           
            if (dispatch_date != DraftConfirmationDetails.dispatch_date ||
                payment_date != DraftConfirmationDetails.payment_date   || qty != DraftConfirmationDetails.qty  
                 || bag_weight != DraftConfirmationDetails.bag_weight  || no_of_boxes != DraftConfirmationDetails.no_of_boxes   || gross_amount != DraftConfirmationDetails.gross_amount 
                 || tax_prec != DraftConfirmationDetails.tax_prec || tax_amount != DraftConfirmationDetails.tax_amount 
                 || frieght != DraftConfirmationDetails.frieght || insurance != DraftConfirmationDetails.insurance || other_charges != DraftConfirmationDetails.other_charges || invoice_value != DraftConfirmationDetails.invoice_value )
            {
                var num1 = 2;
                DraftConfirmation.Flag_Check = num1;
                DraftConfirmation.Final_Edit_Flag = true;
            }
            else
            {
                var num2 = 0;
                DraftConfirmation.Flag_Check = num2;
                DraftConfirmation.Final_Edit_Flag = false;
            }
           
            await Navigation.PopAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            DraftConfirmation.Flag_Check = 0;
            await Navigation.PopAsync();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (DraftConfirmationDetails.id <= 0)
            {
                InvoiceDetails invoiceDetails = await viewModel.CalculateInvoice(DraftConfirmation);
                if (DraftConfirmation.unit == "BAGS")
                {
                    DraftConfirmationDetails.bag_weight = invoiceDetails.bag_weight;
                    txtBagWeight.Text = DraftConfirmationDetails.bag_weight.ToString();
                }
                DraftConfirmationDetails.rate_type = invoiceDetails.rate_type;
                if (DraftConfirmationDetails.rate_type == 1)
                    RdoNetRate_Clicked(null, null);
                else
                    RdoExMill_Clicked(null, null);
                if (DraftConfirmation.segment == 1)
                {
                    DraftConfirmationDetails.tax_id = invoiceDetails.domestic_tax_id;
                    DraftConfirmationDetails.tax_prec = invoiceDetails.domestic_tax_perc;
                }
                else
                {
                    DraftConfirmationDetails.tax_id = invoiceDetails.export_tax_id;
                    DraftConfirmationDetails.tax_prec = invoiceDetails.export_tax_perc;
                }
                if (DraftConfirmationDetails.rate_type != 1)
                    txtTaxPrec.Text = string.Format("{0} %", DraftConfirmationDetails.tax_prec);
            }
            


            CalculateNetAmount(null,null);
        }
        
        private void CalculateNetAmount(object sender, TextChangedEventArgs e)
        {
            if (txtQty.Text == "")
                DraftConfirmationDetails.qty = 0;
            if (txtBagWeight.Text == "")
                DraftConfirmationDetails.bag_weight = 0;
            if (txtFrieght.Text == "")
                DraftConfirmationDetails.frieght = 0;
            if (txtInsurance.Text == "")
                DraftConfirmationDetails.insurance = 0;
            if (txtOtherCharges.Text == "")
                DraftConfirmationDetails.other_charges = 0;
            if (txtNoOfBoxes.Text == "" && (DraftConfirmation.unit == "FCL" || DraftConfirmation.unit == "PALLET"))
                DraftConfirmationDetails.no_of_boxes = 0;
            if (txtWeightPerBox.Text == "" && (DraftConfirmation.unit == "FCL" || DraftConfirmation.unit == "PALLET"))
                DraftConfirmationDetails.bag_weight = 0;
            DraftConfirmationDetails.qty = Convert.ToInt32(DraftConfirmationDetails.qty);
            if(DraftConfirmationDetails.qty >0)txtQty.Text = DraftConfirmationDetails.qty.ToString();
            DraftConfirmationDetails.gross_weight = ((DraftConfirmation.unit == "FCL" || DraftConfirmation.unit == "PALLET") ? DraftConfirmationDetails.bag_weight * DraftConfirmationDetails.no_of_boxes : DraftConfirmationDetails.bag_weight) * DraftConfirmationDetails.qty;
            txtGrossWeight.Text = string.Format("{0:0.000}", DraftConfirmationDetails.gross_weight);
            DraftConfirmationDetails.gross_amount = ((DraftConfirmation.unit == "FCL" || DraftConfirmation.unit == "PALLET") ? DraftConfirmationDetails.bag_weight * DraftConfirmationDetails.no_of_boxes : DraftConfirmationDetails.bag_weight) * DraftConfirmationDetails.qty * ((DraftConfirmation.per == "/ 5 KG") ? DraftConfirmation.price / 5 : DraftConfirmation.price);
            txtGrossAmount.Text = string.Format("{0:0.00}", DraftConfirmationDetails.gross_amount);
            if (DraftConfirmationDetails.rate_type != 1)
                DraftConfirmationDetails.tax_amount = DraftConfirmationDetails.gross_amount * (DraftConfirmationDetails.tax_prec / 100);            
            txtTaxAmount.Text = string.Format("{0:0.00}", DraftConfirmationDetails.tax_amount);
            DraftConfirmationDetails.invoice_value = DraftConfirmationDetails.gross_amount + DraftConfirmationDetails.tax_amount + DraftConfirmationDetails.frieght + DraftConfirmationDetails.insurance + DraftConfirmationDetails.other_charges;
            txtNetAmount.Text = string.Format("{0:0.00}", DraftConfirmationDetails.invoice_value);
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
            //await Navigation.PushAsync(new LogPage(approval));
        }
        
        private void txtDispatchDateTime_DateSelected(object sender, DateChangedEventArgs e)
        {
            DraftConfirmationDetails.dispatch_date = txtDispatchDateTime.Date;
        }

        private void txtPaymentDateTime_DateSelected(object sender, DateChangedEventArgs e)
        {
            DraftConfirmationDetails.payment_date = txtPaymentDateTime.Date;
        }

        private void RdoExMill_Clicked(object sender, EventArgs e)
        {
            RdoExMill.IsChecked = true;
            RdoNetRate.IsChecked = false;
        }

        private void RdoNetRate_Clicked(object sender, EventArgs e)
        {
            RdoNetRate.IsChecked = true;
            RdoExMill.IsChecked = false;
        }

        //private async void Delete_Clicked(object sender, EventArgs e)
        //{
        //    if (DraftConfirmation.send_for_approval == 1 || _Amendflag==1)
        //        return;
        //    var result = await this.DisplayAlert("Attention!", "Do you want to delete this dispatch?", "Yes", "No");
        //    if (result)
        //    {
        //        try
        //        {
        //          string message = await viewModel.DeleteDraftConfirmationDetailsCommand(DraftConfirmationDetails.id);
        //        }
        //        catch (Exception ex)
        //        {
                    
        //        }
        //        finally
        //        {
        //            await Navigation.PopAsync();
        //        }
        //    }
        //}

        private void ChkCancelDispatch_Clicked(object sender, EventArgs e)
        {
            if (chkDispatch.IsChecked == true)
            {
                DraftConfirmationDetails.cancel_dispatch = 1;
                lblRemarks.IsVisible = true;
                txtRemarks.IsVisible = true;
            }
            else
            {
                DraftConfirmationDetails.cancel_dispatch = 0;
                lblRemarks.IsVisible = false;
                txtRemarks.IsVisible = false;
            }
        }

        private async void lblDispatchDeliveryDetails_Clicked(object sender, EventArgs e)
        {
            if (DraftConfirmationDetails.cancel_dispatch != 1)
                await Navigation.PushAsync(new AddDispatchDeliveryDetailPage(DraftConfirmation, DraftConfirmationDetails,null,1));
            else
                await DisplayAlert("Alert", "Dispatch is Cancelled. So can't enter the dispatch and delivery details...", "OK");
        }
    }
}