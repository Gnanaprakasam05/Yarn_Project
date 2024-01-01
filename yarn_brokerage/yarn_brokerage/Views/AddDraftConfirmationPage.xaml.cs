using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class AddDraftConfirmationPage : ContentPage
    {
        public ObservableCollection<MessageGroup> MessageGroup_List { get; set; }
        public MessageGroup MessageGroup { get; set; }
        public DraftConfirmation DraftConfirmation { get; set; }
        public DraftConfirmationDetails DraftConfirmationDetails { get; set; }

        DraftConfirmationViewModel viewModel;
        DraftConfirmationDetailViewModel detailViewModel;
        public DateTime date { get; set; }
        public SearchFilter _searchFilter { get; set; }
        public Indexes _enquiry { get; set; }
        public Approval Approval { get; set; }
        ApprovalViewModel approvalViewModel;

        decimal DraftConfirmationDetails_QTY;
        decimal DraftConfirmationDetails_BALQTY;
        decimal QTY;
        public int Flag;    //1

        public int Add_Flag;
        public bool Edit_Flag;
        public int editCheck;

        public DraftConfirmation draftConfirmation_List { get; set; }
        public int Flag_Message { get; set; }
        public AddDraftConfirmationPage(DraftConfirmation draftConfirmation, SearchFilter searchFilter = null, Indexes enquiry = null, bool flag = false, DraftConfirmation DraftConfirmationData = null)
        {
            InitializeComponent();

            if (Application.Current.Properties["transaction_draft_confirmation_UpdateAllowed"].ToString() == "0")
                butSave.IsVisible = false;

            MessageGroup_List = new ObservableCollection<MessageGroup>();
            MessageGroup = new MessageGroup();
            viewModel = new DraftConfirmationViewModel();
            approvalViewModel = new ApprovalViewModel();
            _searchFilter = searchFilter;
            _enquiry = enquiry;

            if (draftConfirmation == null)
            {
                DraftConfirmation = new DraftConfirmation();
                Title = "Add Confirmation";

                draftConfirmation_List = DraftConfirmationData;

                Add_Flag = 1;


                txtConfirmationNo.IsEnabled = false;
                DraftConfirmation.confirmation_no = txtConfirmationNo.Text = " ";
                DraftConfirmation.confirmedRemarks = txtRemarks.Text = "";
                DraftConfirmation.transaction_date_time = DateTime.Now.ToLocalTime();
                //DraftConfirmation.dispatch_from_date = DateTime.Now.ToLocalTime();
                //DraftConfirmation.dispatch_to_date = DateTime.Now.ToLocalTime();
                //DraftConfirmation.payment_date = DateTime.Now.ToLocalTime();
                DraftConfirmation.unit = "BAGS";
                DraftConfirmation.per = "/ KG";


                txtConfirmationLabel.IsVisible = false;
                txtConfirmationNo.IsVisible = false;


                if (DraftConfirmationDetails == null)
                {
                    DraftConfirmationDetails = new DraftConfirmationDetails();
                }

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

                draftConfirmation_List = DraftConfirmationData;

                if (flag == true)
                {
                    Edit_Flag = flag;
                }
                if (DraftConfirmation.confirmedRemarks == null)
                {
                    DraftConfirmation.confirmedRemarks = "";
                }


                if (DraftConfirmation.confirmation_no == "")
                {
                    txtConfirmationLabel.IsVisible = false;
                    txtConfirmationNo.IsVisible = false;


                }
                else
                {
                    txtConfirmationLabel.IsVisible = true;
                    txtConfirmationNo.IsVisible = true;

                }


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

            BindingContext = detailViewModel = new DraftConfirmationDetailViewModel();

            //detailViewModel.LoadItemsCommand.Execute(DraftConfirmation.id);
            //if (Convert.ToDouble(txtBagweight.Text) == 0)
            //    txtBagweight.Text = "";
            if (DraftConfirmation.id > 0)
            {
                txtQty.Text = (DraftConfirmation.qty > 0) ? DraftConfirmation.qty.ToString() : "";
                txtPrice.Text = (DraftConfirmation.price > 0) ? DraftConfirmation.price.ToString() : "";
                txtConfirmationNo.Text = DraftConfirmation.confirmation_no; //DraftConfirmation.confirmation_no.Substring(DraftConfirmation.confirmation_no.Length - 5).ToString().PadRight(5, '0');
            }
            if (Convert.ToDouble(txtQty.Text) == 0)
                txtQty.Text = "";
            if (Convert.ToDouble(txtPrice.Text) == 0)
                txtPrice.Text = "";
        }
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
            txtRemarks.Text = DraftConfirmation.confirmedRemarks;


            if (DraftConfirmation.Final_Edit_Flag == true)
            {
                editCheck = 1;
            }





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


            if (DraftConfirmationDetails == null)
            {
                DraftConfirmationDetails = new DraftConfirmationDetails();
            }
            await detailViewModel.ExecuteDraftConfirmationDetailsCommand(DraftConfirmation.id);

            //DraftConfirmationDetails = await detailViewModel.ExecuteDraftConfirmationDetails(DraftConfirmation.id);

            DraftConfirmationDetails.draft_confirmation_id = 0;

            int count = await detailViewModel.DraftConfirmationCount();
            if (count > 0)
            {
                control_enable(false);
                txtConfirmationNo.IsEnabled = false;
                DraftConfirmationDetails.draft_confirmation_id = DraftConfirmation.id;
                butSave.IsVisible = false;
                butCancel.IsVisible = false;
                butOkey.IsVisible = true;
            }
            else
            {
                control_enable(true);
                txtConfirmationNo.IsEnabled = false;
                butSave.Text = "Save";
            }

            TotalQty = await detailViewModel.DraftConfirmationTotalQty();
            DraftConfirmation.TotalQty = TotalQty;
            TotalCancelQty = await detailViewModel.DraftConfirmationTotalCancelQty();
            if (TotalQty > 0)
                lblDispatchDetails.Text = "Dispatch Details (" + TotalQty + " " + DraftConfirmation.unit + ")";
            else
                lblDispatchDetails.Text = "Dispatch Details";
            DraftConfirmationListView.HeightRequest = count * 57;
        }
        async void Save_Clicked(object sender, EventArgs e)
        {

            DraftConfirmation.qty = (txtQty.Text != "") ? Convert.ToDouble(txtQty.Text) : 0;
            DraftConfirmation.price = (txtPrice.Text != "") ? Convert.ToDecimal(txtPrice.Text) : 0;


            if (Title == "Add Confirmation")
            {
                DraftConfirmationDetails_QTY = DraftConfirmationDetails.qty;
                DraftConfirmationDetails_BALQTY = DraftConfirmationDetails.balance_qty;
                QTY = DraftConfirmationDetails_BALQTY - DraftConfirmationDetails_QTY;
            }



            DraftConfirmation.confirmation_no = txtConfirmationNo.Text = " ";
            if (butSave.Text == "Save")
            {

                if (DraftConfirmation.id < 0)
                {
                    await DisplayAlert("Alert", "Confirmation No not in the correct format...", "OK");
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
                else if (DraftConfirmationDetails.draft_confirmation_id <= 0)
                {
                    await DisplayAlert("Alert", "Please enter draft confirmation details and try again.", "OK");
                    return;
                }

                string s = await SaveConfirmation(sender, e);

            }
            else
            {
                //if (await detailViewModel.checkAmend() == false)
                {
                    if (Convert.ToInt32(txtQty.Text) != (TotalQty + TotalCancelQty))
                    {
                        if (Convert.ToInt32(txtQty.Text) != (TotalQty))
                        {
                            await DisplayAlert("Alert", "Confirmation Quantity and Dispatch Plan Quantity should be same...", "OK");
                            txtPrice.Focus();
                            return;
                        }
                    }
                }
                viewModel.SendForApproval(DraftConfirmation);
                await Navigation.PopAsync();
            }
        }

        async Task<string> SaveConfirmation(object sender, EventArgs e)
        {
            if (Edit_Flag != false)
            {
                draftConfirmation_List.Edit_Flag = true;
                draftConfirmation_List.Add_Flag = false;

            }
            else
            {
                draftConfirmation_List.Add_Flag = true;
                draftConfirmation_List.Edit_Flag = false;


            }

            draftConfirmation_List.Cancel_Click = 0;

            if (Application.Current.Properties["user_type"].ToString() == "1")
                draftConfirmation_List.admin_user = true;
            if (DraftConfirmation.price != Convert.ToInt32(DraftConfirmation.price))
                draftConfirmation_List.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(DraftConfirmation.price)));
            else
                DraftConfirmation.price = Convert.ToInt32(DraftConfirmation.price);
            draftConfirmation_List.price_per = DraftConfirmation.price.ToString() + " " + DraftConfirmation.per;
            if (DraftConfirmation.status == 1)
                draftConfirmation_List.status_image = "approved.png";
            if (DraftConfirmation.status == 5)
                draftConfirmation_List.status_image = "rejected.png";
            double diff2 = (DateTime.Today.Date - DraftConfirmation.transaction_date_time).TotalDays;
            if (diff2 < 0)
                diff2 = 0;

            string delayDay = DraftConfirmation.DelayDays;

            if (delayDay == null || delayDay == "0")
            {
                DraftConfirmation.DelayDays = "0";
            }
            draftConfirmation_List.TransactionDetails = DraftConfirmation.transaction_date_time.ToString("dd-MM-yyyy") + " ( " + DraftConfirmation.confirmation_no + " ) " + " - " + diff2 + " Days Old";


            //draftConfirmation_List.customer_name = DraftConfirmation.customer_name;
            //draftConfirmation_List.supplier_name = DraftConfirmation.supplier_name;
            //draftConfirmation_List.confirmation_no = DraftConfirmation.confirmation_no;
            //draftConfirmation_List.count_name = DraftConfirmation.count_name;
            //draftConfirmation_List.unit = DraftConfirmation.unit;
            //draftConfirmation_List.price = DraftConfirmation.price;
            //draftConfirmation_List.confirmedRemarks = DraftConfirmation.confirmedRemarks;
            //draftConfirmation_List.segment = DraftConfirmation.segment;
            //draftConfirmation_List.qty = DraftConfirmation.qty;
            //draftConfirmation_List.per = DraftConfirmation.per;
            //draftConfirmation_List.transaction_date_time = DraftConfirmation.transaction_date_time;


            if (RdoDomestic.IsChecked) DraftConfirmation.segment = 1; else if (RdoExport.IsChecked) DraftConfirmation.segment = 2;
            DraftConfirmation = await viewModel.StoreDraftConfirmationCommand(DraftConfirmation);

            draftConfirmation_List.supplier_id = DraftConfirmation.supplier_id;
            draftConfirmation_List.customer_id = DraftConfirmation.customer_id;
            draftConfirmation_List.count_id = DraftConfirmation.count_id;
            draftConfirmation_List.bag_weight = DraftConfirmation.bag_weight;
            draftConfirmation_List.id = DraftConfirmation.id;
            draftConfirmation_List.qty_unit = DraftConfirmation.qty_unit;

            draftConfirmation_List.TotalQty = DraftConfirmation.TotalQty;
            draftConfirmation_List.customer_name = DraftConfirmation.customer_name;
            draftConfirmation_List.supplier_name = DraftConfirmation.supplier_name;
            draftConfirmation_List.confirmation_no = DraftConfirmation.confirmation_no;
            draftConfirmation_List.count_name = DraftConfirmation.count_name;
            draftConfirmation_List.unit = DraftConfirmation.unit;
            draftConfirmation_List.price = DraftConfirmation.price;
            draftConfirmation_List.confirmedRemarks = DraftConfirmation.confirmedRemarks;
            draftConfirmation_List.segment = DraftConfirmation.segment;
            draftConfirmation_List.qty = DraftConfirmation.qty;
            draftConfirmation_List.per = DraftConfirmation.per;
            draftConfirmation_List.transaction_date_time = DraftConfirmation.transaction_date_time;


            if (_searchFilter != null)
            {
                DraftConfirmation_list dc_list = new DraftConfirmation_list();
                List<DraftConfirmation> dc = new List<DraftConfirmation>();
                dc.Add(DraftConfirmation);
                dc_list.draftConfirmations = dc;
                _enquiry.confirm_list = dc_list;
                _enquiry.confirmed = 1;

            }

            if (DraftConfirmation.id > 0)
            {
                Title = "Edit Confirmation";
                txtConfirmationNo.Text = DraftConfirmation.confirmation_no;



            }





            return DraftConfirmation.StoreMessage;
        }
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            if (draftConfirmation_List.Add_Flag || draftConfirmation_List.Edit_Flag)
            {
                draftConfirmation_List.Cancel_Click = 0;

            }
            else
            {
                draftConfirmation_List.Cancel_Click = 1;
            }

            await Navigation.PopAsync();
        }

        //async void Ok_Clicked(object sender, EventArgs e)
        //{
        //    MessageGroup_List = await viewModel.MessageGroupDraftConfirmationCommand();
        //    for (int i = 0; i < MessageGroup_List.Count; i++)
        //    {
        //        if (2 == 2)  // if (DraftConfirmation.SupplierWhatsappGroup_Ckeck == MessageGroup_List[i].Id)
        //        {

        //            var index_value = MessageGroup_List.IndexOf(MessageGroup_List.Where(x => x.Id == MessageGroup_List[i].Id).FirstOrDefault());
        //            await PopupNavigation.Instance.PushAsync(new PopupGroup(DraftConfirmation, 2));
        //            break;
        //        }
        //    }
        //    if (draftConfirmation_List.Add_Flag || draftConfirmation_List.Edit_Flag)
        //    {
        //        draftConfirmation_List.Cancel_Click = 0;

        //    }
        //    else
        //    {
        //        draftConfirmation_List.Cancel_Click = 1;
        //    }
        //}
        async void Ok_Clicked(object sender, EventArgs e)
        {
            MessageGroup_List = await viewModel.MessageGroupDraftConfirmationCommand();

            if (Title == "Edit Confirmation")
            {


                Flag = DraftConfirmation.Flag_Check;

                if (Edit_Flag == true)
                {
                    if (DraftConfirmation.Flag_Check == 1)
                    {
                        Flag = 2;
                    }
                }
                else
                {
                    Flag = 1;
                }

                if (editCheck == 1)
                {
                    Flag = 2;
                    DraftConfirmation.Flag_Check = 2;
                }

                Application.Current.Properties["send_status"] = Flag;

                if (DraftConfirmation.qty == TotalQty)
                {
                    QTY = 0;
                }
                else if (DraftConfirmation.qty < TotalQty)
                {
                    QTY = 2;
                }
                else
                {
                    QTY = 1;
                }


                if (QTY == 1)
                {
                    bool result = await this.DisplayAlert("Attention!", "Confirmation Qty not equal to Dispatch Qty. Do you want to Continue?", "Yes", "No");
                    if (result)
                    {
                        if (DraftConfirmation.Flag_Check == 1 || DraftConfirmation.Flag_Check == 2)
                        {
                            if (QTY == 0)
                            {
                                var TeamGroupId = Application.Current.Properties["team_group_id"];


                                await viewModel.WhatsappConfirmationDraftConfirmationCommand(DraftConfirmation, MessageGroup);

                                if (sender != null)
                                {
                                    await Navigation.PopAsync();
                                }
                            }
                            else if (Flag == 2)    // 2 Edited   1  Added
                            {
                                var TeamGroupId1 = Application.Current.Properties["team_group_id"];


                                await viewModel.WhatsappConfirmationDraftConfirmationCommand(DraftConfirmation, MessageGroup);

                                if (sender != null)
                                {
                                    await Navigation.PopAsync();
                                }
                            }
                            else
                            {
                                if (sender != null)
                                {
                                    await Navigation.PopAsync();
                                }
                            }
                        }
                        else
                        {
                            await Navigation.PopAsync();
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else if (QTY == 2)
                {
                    await DisplayAlert("Alert", "Dispatch Qty higher then Confirmation Qty, Please Check it.", "OK");
                    return;
                }
                else
                {
                    if (DraftConfirmation.Flag_Check == 1 || DraftConfirmation.Flag_Check == 2)
                    {
                        if (QTY == 0)
                        {

                            if (Flag == 1)
                            {
                                for (int i = 0; i < MessageGroup_List.Count; i++)
                                {
                                    if (DraftConfirmation.SupplierWhatsappGroup_Ckeck == MessageGroup_List[i].Id)
                                    {


                                        await PopupNavigation.Instance.PushAsync(new PopupGroup(DraftConfirmation));
                                        break;
                                    }
                                }
                                if (draftConfirmation_List.Add_Flag || draftConfirmation_List.Edit_Flag)
                                {
                                    draftConfirmation_List.Cancel_Click = 0;

                                }
                                else
                                {
                                    draftConfirmation_List.Cancel_Click = 1;
                                }
                            }
                            else
                            {
                                await viewModel.WhatsappConfirmationDraftConfirmationCommand(DraftConfirmation, MessageGroup);
                            }

                            if (sender != null)
                            {
                                await Navigation.PopAsync();
                            }

                        }
                        else if (Flag == 2 || Flag == 1)       //Edited
                        {



                            if (Flag == 1)
                            {
                                for (int i = 0; i < MessageGroup_List.Count; i++)
                                {
                                    if (DraftConfirmation.SupplierWhatsappGroup_Ckeck == MessageGroup_List[i].Id)
                                    {

                                        await PopupNavigation.Instance.PushAsync(new PopupGroup(DraftConfirmation));
                                        break;
                                    }
                                }
                                if (draftConfirmation_List.Add_Flag || draftConfirmation_List.Edit_Flag)
                                {
                                    draftConfirmation_List.Cancel_Click = 0;

                                }
                                else
                                {
                                    draftConfirmation_List.Cancel_Click = 1;
                                }
                            }
                            else
                            {
                                await viewModel.WhatsappConfirmationDraftConfirmationCommand(DraftConfirmation, MessageGroup);
                            }

                            if (sender != null)
                            {

                                await Navigation.PopAsync();
                            }
                        }
                    }
                    draftConfirmation_List.Cancel_Click = 1;
                    await Navigation.PopAsync();
                }
            }
        }
        int TotalQty = 0;
        int TotalCancelQty = 0;

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
            txtRemarks.IsEnabled = enable;
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

            DraftConfirmation.confirmation_no = txtConfirmationNo.Text = " ";

            if (DraftConfirmation.confirmedRemarks == null)
            {
                DraftConfirmation.confirmedRemarks = "";
            }
            //DraftConfirmation.confirmation_no = (txtConfirmationNo.Text != "") ? txtConfirmationNo.Text : "";
            //if (string.IsNullOrWhiteSpace(DraftConfirmation.confirmation_no))
            //{
            //    await DisplayAlert("Alert", "Enter the Confirmation No...", "OK");
            //    return;
            //}
            if (string.IsNullOrWhiteSpace(DraftConfirmation.customer_name))
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
            //else if (await viewModel.DuplicateConfirmationNo(DraftConfirmation) > 0)
            //{
            //    await DisplayAlert("Alert", "Confirmation No is already exist...", "OK");
            //    txtConfirmationNo.Focus();
            //    return;
            //}
            string i = await SaveConfirmation(null, null);







            //DraftConfirmationDetails = new DraftConfirmationDetails();
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

            //DraftConfirmationDetails_QTY = item.balance_qty;
            //DraftConfirmationDetails_BALQTY = item.qty;

            item.balance_qty = Convert.ToInt32(item.qty) + Convert.ToInt32(txtQty.Text) - TotalQty;
            if (DraftConfirmation.send_for_approval == 0 && DraftConfirmation.status == 0)
            {
                if (item.bags_ready == 1 || item.payment_ready == 1 || item.payment_received == 1 || item.transporter_ready == 1 || item.dispatched == 1 || item.program_approval == 1 || item.amend == 0)
                    Amend_flag = 1;
            }
            await Navigation.PushAsync(new EditDraftConfirmationDetailPage(DraftConfirmation, item, Amend_flag));


            //await Navigation.PushModalAsync(new NavigationPage(new EditDraftConfirmationDetailPage(DraftConfirmation, item)));
        }

        private void txtConfirmationNo_Unfocused(object sender, FocusEventArgs e)
        {
            if (txtConfirmationNo.Text != null && txtConfirmationNo.Text.Length < 3)
                txtConfirmationNo.Text = txtConfirmationNo.Text.ToString().PadLeft(3, '0');
        }

        private async void TxtCustomerConfirmedBy_Focused(object sender, EventArgs e)
        {
            if (txtCustomerName.IsEnabled == false)
                return;
            await Navigation.PushAsync(new LedgerTeamNamePage(2));
        }

        private void txtRemark_Unfocused(object sender, FocusEventArgs e)
        {
            if (txtRemarks.Text == "")
            {
                DraftConfirmation.confirmedRemarks = " ";
            }
            else
            {
                DraftConfirmation.confirmedRemarks = txtRemarks.Text;
            }
        }




        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {

        }

        private void txtRemarks_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtRemarks.Text != "")
            {
                DraftConfirmation.confirmedRemarks = txtRemarks.Text;
            }
            else
            {
                DraftConfirmation.confirmedRemarks = "";
            }
        }
    }
}