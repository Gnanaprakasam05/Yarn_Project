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
    public partial class AddApprovalPage : ContentPage
    {
        public Approval Approval { get; set; }
        public Approval ApprovalCheck { get; set; }

        ApprovalViewModel viewModel;
        public DateTime date { get; set; }
        public SearchConfirmationFilter _searchFilter { get; set; }
        //public Indexes _enquiry { get; set; }
        public AddApprovalPage(Approval approval, Approval CheckApproval = null)
        {
            InitializeComponent();


            if (Application.Current.Properties["transaction_pending_approval_ApproveAllowed"].ToString() == "0")
                butRevoke.IsVisible = false;

            viewModel = new ApprovalViewModel();


            ApprovalCheck = CheckApproval;

            Approval = approval;
            Title = "Approval";
            if (approval.dispatch_details.Count > 0)
                DraftConfirmationListView.HeightRequest = approval.dispatch_details.Count * 57;

            double dispatch_qty = 0;
            foreach (DraftConfirmationDetails item in Approval.dispatch_details)
            {
                dispatch_qty += (double)item.qty;
            }
            if (dispatch_qty > 0)
                lblDispatchDetails.Text = "Dispatch Details (" + dispatch_qty + " " + Approval.unit + ")";
            if (Approval.status == 1 || Approval.status == 5)
                butRevoke.Text = "Revoke";
            //if (Approval.status == 1)
            //    butSave.Text = "Reject";
            //else 
            if (Approval.status == 5)
                butSave.Text = "Approve";
            if (Approval.supplier_md_mobile_no != null)
            {
                string[] arrPoints = new string[] { };
                arrPoints = Approval.supplier_md_mobile_no.Split(',');
                lblSupplierMobile.Children.Clear();
                foreach (string arrPoint in arrPoints)
                {
                    Label lbl1 = new Label
                    {
                        Margin = new Thickness(0, 0, 4, 0),
                        TextColor = Color.FromRgb(0, 0, 255),
                        FontSize = 15,
                        //TextDecorations = TextDecorations.Underline,
                        //HorizontalTextAlignment = TextAlignment.Center,
                        //HorizontalOptions = LayoutOptions.Center,
                    };
                    lbl1.Text = arrPoint;
                    var _tap = new TapGestureRecognizer();
                    _tap.Tapped += (s, e1) =>
                    {
                        SupplierMobile_Tapped(s, e1);
                    };
                    lbl1.GestureRecognizers.Add(_tap);
                    lblSupplierMobile.Children.Add(lbl1);
                }
            }
            if (Approval.customer_md_mobile_no != null)
            {
                string[] arrPoints1 = new string[] { };
                arrPoints1 = Approval.customer_md_mobile_no.Split(',');
                lblCustomerMobile.Children.Clear();
                foreach (string arrPoint in arrPoints1)
                {
                    Label lbl1 = new Label
                    {
                        Margin = new Thickness(0, 0, 4, 0),
                        TextColor = Color.FromRgb(0, 0, 255),
                        FontSize = 15,
                        //TextDecorations = TextDecorations.Underline,
                        //HorizontalTextAlignment = TextAlignment.Center,
                        //HorizontalOptions = LayoutOptions.Center,
                    };
                    lbl1.Text = arrPoint;
                    var _tap = new TapGestureRecognizer();
                    _tap.Tapped += (s, e1) =>
                    {
                        SupplierMobile_Tapped(s, e1);
                    };
                    lbl1.GestureRecognizers.Add(_tap);
                    lblCustomerMobile.Children.Add(lbl1);
                }
            }
            txtCustomerConfirmedBy.Text = Approval.CustomerTeamName;
            txtSupplierConfirmedBy.Text = Approval.SupplierTeamName;
            BindingContext = this;
        }
        private async void TxtSupplierConfirmedBy_Focused(object sender, EventArgs e)
        {
            if (txtCustomerName.IsEnabled == false)
                return;
            await Navigation.PushAsync(new LedgerTeamNamePage(1, Approval));
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {

        }
        private async void TxtCustomerConfirmedBy_Focused(object sender, EventArgs e)
        {
            if (txtCustomerName.IsEnabled == false)
                return;
            await Navigation.PushAsync(new LedgerTeamNamePage(2, Approval));
        }
        async void Revoke_Clicked(object sender, EventArgs e)
        {
            double dispatch_qty = 0;
            foreach (DraftConfirmationDetails item in Approval.dispatch_details)
            {
                dispatch_qty += (double)item.qty;
            }

            //if (Approval.qty != dispatch_qty)
            //{
            //    await DisplayAlert("Alert", "Mismatch in confirmation and dispatch quantity...", "OK");
            //    return;
            //}
            if (butRevoke.Text == "Revoke")
                Approval.status = 0;
            else
                Approval.status = 1;
            Approval _Approval = await viewModel.StoreApprovalCommand(Approval);
            Approval.rejected_user = _Approval.rejected_user;
            Approval.rejected_user_id = _Approval.rejected_user_id;
            Approval.approved_user = _Approval.approved_user;
            Approval.approved_user_id = _Approval.approved_user_id;
            await Task.Delay(200);
            string pay_date = "";
            string dis_date = "";
            foreach (DraftConfirmationDetails draftConfirmationDetails in _Approval.dispatch_details)
            {
                pay_date = draftConfirmationDetails.payment_date.ToString("dd-MM-yyyy");
                dis_date = draftConfirmationDetails.dispatch_date.ToString("dd-MM-yyyy");
            }
            string message = "*Confirmation*";
            message += "\n\n";
            message += "Mill Name : " + _Approval.supplier_name;
            message += "\n\n";
            message += "Buyer Name : " + _Approval.customer_name;
            message += "\n\n";
            message += "Count : " + _Approval.count_name;
            message += "\n\n";
            message += "Rate : " + _Approval.price_per;
            message += "\n\n";
            message += "Quantity : " + _Approval.qty_unit;
            message += "\n\n";
            //message += "Kg :" + _Approval.
            message += "Despatch and Payment : " + pay_date + " and " + dis_date;
            //Device.OpenUri(new Uri("whatsapp://send/?phone&text=" + message + "&app_absent=0")); //https://api.whatsapp.com/



            ApprovalCheck.Id = _Approval.Id;
            ApprovalCheck.Add_Flag = true;


            await Navigation.PopAsync();
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            double dispatch_qty = 0;
            foreach (DraftConfirmationDetails item in Approval.dispatch_details)
            {
                dispatch_qty += (double)item.qty;
            }

            //if (Approval.qty != dispatch_qty)
            //{
            //    await DisplayAlert("Alert", "Dispatch quantity and confirmed quantity should be same...", "OK");
            //    return;
            //}

            if (butSave.Text == "Approve")
                Approval.status = 1;
            else
                Approval.status = 5;
            Approval _Approval = await viewModel.StoreApprovalCommand(Approval);
            Approval.rejected_user = _Approval.rejected_user;
            Approval.rejected_user_id = _Approval.rejected_user_id;
            Approval.approved_user = _Approval.approved_user;
            Approval.approved_user_id = _Approval.approved_user_id;
            await Task.Delay(200);
            ApprovalCheck.Id = _Approval.Id;
            ApprovalCheck.Add_Flag = true;
            await Navigation.PopAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            ApprovalCheck.CancelClick = 1;
            await Navigation.PopAsync();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            txtCustomerConfirmedBy.Text = Approval.CustomerTeamName;
            txtSupplierConfirmedBy.Text = Approval.SupplierTeamName;
        }

        private async void TxtCustomer_Focused(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new LedgersListPage(null,2,null,Approval));
        }
        private async void TxtCount_Focused(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new CountListPage(null,Approval));
        }

        private void txtBagweight_Focused(object sender, FocusEventArgs e)
        {
            //if (txtBagweight.Text.Trim() != "")
            //{
            //    txtBagweight.Text = string.Format("{0:0}", Convert.ToDouble(txtBagweight.Text));
            //    if (Convert.ToDouble(txtBagweight.Text) == 0)
            //    {
            //        txtBagweight.Text = "";
            //        Approval.bag_weight = 0;
            //    }
            //}
            //else
            //{
            //    Approval.bag_weight = 0;
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
            //        Approval.qty = 0;
            //    }
            //}
            //else
            //{
            //    Approval.qty = 0;
            //}
        }

        private void TxtPrice_Focused(object sender, FocusEventArgs e)
        {
            //if (txtPrice.Text.Trim() != "")
            //{
            //    //txtPrice.Text = string.Format("{0:0.00}", Convert.ToDouble(txtPrice.Text));
            //    if (Convert.ToDouble(txtPrice.Text) == 0)
            //    {
            //        txtPrice.Text = "";
            //        Approval.price = 0;
            //    }
            //}
            //else
            //{
            //    Approval.price = 0;
            //}
        }

        private async void TxtSupplier_Focused(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new LedgersListPage(null,1,null,Approval));
        }

        private void RdoDomestic_Clicked(object sender, EventArgs e)
        {
            //RdoDomestic.IsChecked = true;
            //RdoExport.IsChecked = false;
        }

        private void RdoExport_Clicked(object sender, EventArgs e)
        {
            //RdoExport.IsChecked = true;
            //RdoDomestic.IsChecked = false;
        }

        private void ButQty_Clicked(object sender, EventArgs e)
        {
            //if (butQty.Text == "BAGS")
            //{
            //    butQty.Text = "FCL";
            //    Approval.unit = "FCL";
            //}
            //else
            //{
            //    butQty.Text = "BAGS";
            //    Approval.unit = "BAGS";
            //}
        }

        private void ButPrice_Clicked(object sender, EventArgs e)
        {
            //if (butPrice.Text == "/ KG")
            //{
            //    butPrice.Text = "/ 5 KG";
            //    Approval.per = "/ 5 KG";
            //}
            //else
            //{
            //    butPrice.Text = "/ KG";
            //    Approval.per = "/ KG";
            //}
        }

        private void SupplierMobile_Tapped(object sender, EventArgs e)
        {
            Label lblsupplierMobile = (Label)sender;
            Device.OpenUri(new Uri("tel:" + lblsupplierMobile.Text));
        }

        private void CustomerMobile_Tapped(object sender, EventArgs e)
        {
            Label lblCustomerMobile = (Label)sender;
            Device.OpenUri(new Uri("tel:" + lblCustomerMobile.Text));
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as DraftConfirmationDetails;
            // Manually deselect item.
            DraftConfirmationListView.SelectedItem = null;
            if (item == null)
                return;
            DraftConfirmation draftConfirmation = new DraftConfirmation();
            draftConfirmation.id = Approval.Id;
            draftConfirmation.supplier_id = Approval.supplier_id;
            draftConfirmation.count_id = Approval.count_id;
            draftConfirmation.price = Approval.price;
            draftConfirmation.unit = Approval.unit;
            draftConfirmation.per = Approval.per;

            await Navigation.PushAsync(new AddDraftConfirmationDetailPage(draftConfirmation, item, 1));
        }
    }
}