using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using yarn_brokerage.Models;
using yarn_brokerage.ViewModels;
namespace yarn_brokerage.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AddDraftConfirmationDetailPage : ContentPage
    {
        public DraftConfirmation DraftConfirmation { get; set; }
        public DraftConfirmationDetails DraftConfirmationDetails { get; set; }

        DraftConfirmationDetailViewModel viewModel;
        public DateTime date { get; set; }
        public decimal Qty;
        public AddDraftConfirmationDetailPage(DraftConfirmation draftConfirmation, DraftConfirmationDetails draftConfirmationDetails, int viewFlag = 0, int Amend_flag = 0)
        {
            InitializeComponent();

            if (Application.Current.Properties["transaction_draft_confirmation_UpdateAllowed"].ToString() == "0")
                butSave.IsVisible = false;

            viewModel = new DraftConfirmationDetailViewModel();

            if (draftConfirmation == null)
                DraftConfirmation = new DraftConfirmation();
            else
                DraftConfirmation = draftConfirmation;

            if (draftConfirmationDetails == null)
                DraftConfirmationDetails = new DraftConfirmationDetails();
            else
                DraftConfirmationDetails = draftConfirmationDetails;

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
            if (viewFlag == 1)
            {
                butSave.IsVisible = false;
                butCancel.Text = "Close";
            }
            if (DraftConfirmation.send_for_approval == 1)
                butSave.IsVisible = false;
            if (Amend_flag == 1)
                butSave.IsVisible = true;
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
            if (DraftConfirmationDetails.qty > Qty)
            {
                await DisplayAlert("alert", "Quantity should not exceed " + Qty + "...", "ok");
                txtQty.Focus();
                DraftConfirmation.Flag_Check = 0;
                return;
            }
            else if (DraftConfirmationDetails.qty <= 0)
            {
                await DisplayAlert("alert", "Enter the quantity...", "ok");
                txtQty.Focus();
                DraftConfirmation.Flag_Check = 0;
                return;
            }
            else if (DraftConfirmationDetails.bag_weight <= 0 && grdBags.IsVisible == true)
            {
                await DisplayAlert("alert", "Enter the weight...", "ok");
                txtBagWeight.Focus();
                DraftConfirmation.Flag_Check = 0;
                return;
            }
            else if (DraftConfirmationDetails.tax_id <= 0)
            {
                await DisplayAlert("Alert", "Please set tax for this count...", "OK");
                DraftConfirmation.Flag_Check = 0;
                return;
            }
            else if (DraftConfirmationDetails.invoice_value <= 0)
            {
                await DisplayAlert("Alert", "Invalid Invoice Value...", "OK");
                txtQty.Focus();
                DraftConfirmation.Flag_Check = 0;
                return;
            }
            DraftConfirmationDetails.draft_confirmation_id = DraftConfirmation.id;
            DraftConfirmationDetails.unit = DraftConfirmation.unit;
            DraftConfirmationDetails.remarks = "";
            string message = await viewModel.StoreDraftConfirmationCommand(DraftConfirmationDetails);
            if (message == "Success")
            {
                DraftConfirmation.Flag_Check = 1;
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
            CalculateNetAmount(null, null);

            DraftConfirmation.Flag_Check = 0;

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

            if (DraftConfirmationDetails.qty > 0) txtQty.Text = DraftConfirmationDetails.qty.ToString();

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
            await Navigation.PushAsync(new LedgersListPage(null, 2, null, DraftConfirmation));
        }
        private async void TxtCount_Focused(object sender, EventArgs e)
        {
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
            await Navigation.PushAsync(new LedgersListPage(null, 1, null, DraftConfirmation));
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
    }
}