using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.CSharp;
using yarn_brokerage.Models;
using yarn_brokerage.ViewModels;
using System.Runtime.CompilerServices;

namespace yarn_brokerage.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AddEnquiryPage : ContentPage
    {
        public Indexes Enquiry { get; set; }
        public Indexes Indexes { get; set; }

        public DraftConfirmation DraftConfirmation { get; set; }
        //public  EnquiriesViewModel viewStoreModel { get; set; }

        public int _transaction_type;
        EnquiriesViewModel viewModel;
        SearchFilter _searchFilter;
        public DateTime date { get; set; }

        public bool EditFlag { get; set; }
        public AddEnquiryPage(Indexes enquiry, DateTime dateTime, SearchFilter searchFilter = null, Indexes indexes = null, bool Edit_Flag = false)
        {
            InitializeComponent();

            if (Application.Current.Properties["transaction_offers_enquiry_UpdateAllowed"].ToString() == "0")
                butSave.IsVisible = false;

            //_transaction_type = transaction_type;
            viewModel = new EnquiriesViewModel();

            DraftConfirmation = new DraftConfirmation();
            //EnquiriesViewModel viewStoreModel = new EnquiriesViewModel();

            //viewStoreModel = vm;
            _searchFilter = searchFilter;
            if (enquiry == null)
            {
                Indexes = indexes;
                Enquiry = new Indexes();
                //if (_transaction_type == 1)
                //    Title = "Add Offer";
                //else
                if (_searchFilter != null)
                {
                    if (_searchFilter.transaction_type == 2)
                    {
                        Title = "Add Enquiry";
                        RdoBuy_Clicked(null, null);
                    }
                    else
                    {
                        Title = "Add Offer";
                        RdoSell_Clicked(null, null);
                    }
                    if (_searchFilter.segment == 1) RdoDomestic_Clicked(null, null); else if (_searchFilter.segment == 2) RdoExport_Clicked(null, null);
                    RdoSell.Color = Color.Gray;
                    RdoBuy.Color = Color.Gray;
                    RdoSell.TextColor = Color.Gray;
                    RdoBuy.TextColor = Color.Gray;

                    RdoDomestic.Color = Color.Gray;
                    RdoExport.Color = Color.Gray;
                    RdoDomestic.TextColor = Color.Gray;
                    RdoExport.TextColor = Color.Gray;

                    RdoBuy.IsEnabled = false;
                    RdoSell.IsEnabled = false;
                    RdoDomestic.IsEnabled = false;
                    RdoExport.IsEnabled = false;

                    Enquiry.count_id = _searchFilter.count_id;
                    Enquiry.count_name = _searchFilter.count_name;
                    txtCountName.IsEnabled = false;

                    if (_searchFilter.exact_ledger_id > 0 && _searchFilter.ledger_id > 0)
                    {
                        Enquiry.ledger_id = (int)_searchFilter.exact_ledger_id;
                        Enquiry.ledger_name = _searchFilter.exact_ledger_name;
                        Enquiry.exact_ledger_id = _searchFilter.ledger_id;
                        Enquiry.exact_ledger_name = _searchFilter.ledger_name;
                        txtLedgerName.IsEnabled = false;
                    }
                    txtExactLedgerName.IsEnabled = false;
                }
                else
                {
                    Title = "Add Offer / Enquiry";
                    Enquiry.transaction_type = 1;
                }
                Enquiry.unit = "BAGS";
                Enquiry.per = "/ KG";
                Enquiry.transaction_date_time = dateTime;
                date = Enquiry.transaction_date_time;
                control_visible(false);
            }
            else
            {
                Enquiry = enquiry;


                EditFlag = Edit_Flag;

                Indexes = indexes;

                //if (_transaction_type == 1)
                //    Title = "Edit Offer";
                //else
                Title = "Edit Offer / Enquiry";
                date = Enquiry.transaction_date_time;





                if (Enquiry.segment == 1) RdoDomestic_Clicked(null, null); else if (Enquiry.segment == 2) RdoExport_Clicked(null, null);
                if (Enquiry.transaction_type == 1) RdoSell_Clicked(null, null); else if (Enquiry.transaction_type == 2) RdoBuy_Clicked(null, null);
                if (_searchFilter != null)
                {
                    if (_searchFilter.transaction_type == 2)
                        Title = "Edit Enquiry";
                    else
                        Title = "Edit Offer";

                    control_visible(false);
                    enquiry_control_visible(false);


                    lblCurrentPrice.IsVisible = true;
                    txtCurrentPrice.IsVisible = true;
                    if (Enquiry.confirmed == 1)
                        lblConfirm.IsVisible = true;
                    else if (Enquiry.hide_confirmed == 0)
                        butConfirm.IsVisible = true;
                }
                else
                {
                    //CheckCound();
                    control_visible(true);
                }

            }
            //txtBagweight.ReturnCommand = new Command(() => txtQty.Focus());
            txtQty.ReturnCommand = new Command(() => txtPrice.Focus());
            BindingContext = this;

            if (Convert.ToDouble(txtQty.Text) == 0)
                txtQty.Text = "";
            if (Convert.ToDouble(txtPrice.Text) == 0)
                txtPrice.Text = "";
            if (Convert.ToDouble(txtCurrentPrice.Text) == 0)
                txtCurrentPrice.Text = "";
        }

        async void Save_Clicked(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(Enquiry.ledger_name))
            {
                if (Enquiry.transaction_type == 1)
                {
                    await DisplayAlert("Alert", "Select Supplier in list..", "OK");
                }
                if (Enquiry.transaction_type == 2)
                {
                    await DisplayAlert("Alert", "Select Customer in list...", "OK");
                }
                return;
            }
            else if (string.IsNullOrWhiteSpace(Enquiry.count_name))
            {
                await DisplayAlert("Alert", "Select Count in list...", "OK");
                return;
            }
            //else if(Enquiry.bag_weight <= 0){
            //    await DisplayAlert("Alert", "Enter the Bag Weight...", "OK");
            //    txtBagweight.Focus();
            //    return;
            //}
            else if (Enquiry.qty <= 0)
            {
                await DisplayAlert("Alert", "Enter the Quantity...", "OK");
                txtQty.Focus();
                return;
            }
            //else if (Enquiry.price <= 0)
            //{
            //    await DisplayAlert("Alert", "Enter the Price...", "OK");
            //    txtPrice.Focus();
            //    return;
            //}
            //else if (txtCurrentPrice.IsVisible == true)
            //{
            //    if (Enquiry.current_price <= 0)
            //    {
            //        await DisplayAlert("Alert", "Enter the Current Price...", "OK");
            //        txtCurrentPrice.Focus();
            //        return;
            //    }
            //}
            //MessagingCenter.Send(this, "AddItem", Item);
            if (RdoDomestic.IsChecked) Enquiry.segment = 1; else if (RdoExport.IsChecked) Enquiry.segment = 2;
            //viewModel.StoreEnquiryCommand(Enquiry);
            storeData();

            Indexes.CancelClick = 0;

            if (EditFlag != false)
            {
                Indexes.Edit_Flag = true;
                Indexes.Add_Flag = false;
            }
            else
            {
                Indexes.Add_Flag = true;
                Indexes.Edit_Flag = false;
            }


        }
        private async void storeData(int flag = 0)
        {
            //Indexes enquiry = (Indexes)_enquiry;
            try
            {
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    using (var cl = new HttpClient())
                    {
                        HttpContent formcontent = null;

                        formcontent = new FormUrlEncodedContent(new[]
                        {
                            new KeyValuePair<string,string>("id",Enquiry.id.ToString()),
                            new KeyValuePair<string,string>("transaction_date_time",Enquiry.transaction_date_time.ToString("yyyy/MM/dd")), //THH:mm
                            new KeyValuePair<string,string>("transaction_type",Enquiry.transaction_type.ToString()),
                            new KeyValuePair<string,string>("segment",Enquiry.segment.ToString()),
                            new KeyValuePair<string,string>("ledger_id",Enquiry.ledger_id.ToString()),
                            new KeyValuePair<string,string>("exact_ledger_id",Enquiry.exact_ledger_id.ToString()),
                            new KeyValuePair<string,string>("count_id",Enquiry.count_id.ToString()),
                            new KeyValuePair<string,string>("bag_weight",Enquiry.bag_weight.ToString()),
                            new KeyValuePair<string,string>("qty",Enquiry.qty.ToString()),
                            new KeyValuePair<string,string>("unit",Enquiry.unit.ToString()),
                            new KeyValuePair<string,string>("price",Enquiry.price.ToString()),
                            new KeyValuePair<string,string>("per",Enquiry.per.ToString()),
                            new KeyValuePair<string,string>("current_price",Enquiry.current_price.ToString()),
                            new KeyValuePair<string,string>("enquiry_date",Enquiry.transaction_date_time.ToString("yyyy-MM-dd")),
                            new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                            new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/enquiry_store", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();


                        Enquiry_List res = Newtonsoft.Json.JsonConvert.DeserializeObject<Enquiry_List>(response);
                        if (res.message == "Record added/updated successfully.")
                        {
                            Enquiry.id = res.data.id;
                            Enquiry.offer_counts = (Enquiry.transaction_type == 2) ? (Convert.ToInt32(res.count) > 1) ? res.count + " Offers" : res.count + " Offer" : (Convert.ToInt32(res.count) > 1) ? res.count + " Enquiries" : res.count + " Enquiry";
                            Enquiry.current_price = res.data.current_price;
                            Title = "Edit Enquiry";
                            Enquiry.price_per = Enquiry.current_price.ToString() + " " + Enquiry.per;
                            Enquiry.qty_unit = Enquiry.qty.ToString() + " " + Enquiry.unit;


                            if (Application.Current.Properties["user_type"].ToString() == "1")
                                Indexes.admin_user = true;
                            if (res.data.transaction_type == 2)
                            {
                                Indexes.description_color = "Green";
                                Indexes.image = "buyer.png";
                                Indexes.reverse_image = "customer.png";
                            }
                            else
                            {
                                Indexes.description_color = res.data.description_color;
                                Indexes.image = res.data.image;
                                Indexes.reverse_image = res.data.reverse_image;
                            }
                            if (res.data.price != Convert.ToInt32(res.data.price))
                                Indexes.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(res.data.price)));
                            else
                                Indexes.price = Convert.ToInt32(res.data.price);
                            if (res.data.current_price != Convert.ToInt32(res.data.current_price))
                                Indexes.current_price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(res.data.current_price)));
                            else
                                Indexes.current_price = Convert.ToInt32(res.data.current_price);
                            Indexes.price_per = res.data.current_price.ToString() + " " + res.data.per;

                            Indexes.bag_weight = Enquiry.bag_weight;
                            Indexes.transaction_date_time = Enquiry.transaction_date_time;
                            Indexes.transaction_type = Enquiry.transaction_type;
                            Indexes.segment = Enquiry.segment;

                            Indexes.qty = Enquiry.qty;
                            Indexes.unit = Enquiry.unit;

                            Indexes.per = Enquiry.per;
                            Indexes.confirmed = res.data.confirmed;
                            Indexes.id = res.data.id;
                            Indexes.ledger_id = res.data.ledger_id;
                            Indexes.count_id = res.data.count_id;

                            Indexes.ledger_name = Enquiry.ledger_name;
                            Indexes.count_name = Enquiry.count_name;
                            Indexes.qty_unit = Enquiry.qty_unit;
                            Indexes.offer_counts = Enquiry.offer_counts;













                            lblShowOffers.Text = Enquiry.offer_counts;
                            txtCurrentPrice.Text = (Enquiry.current_price > 0) ? string.Format("{0}", Enquiry.current_price) : "";

                            if (res.best_list != null)
                            {
                                Enquiry.best_supplier = res.best_list.ledger_name;
                                Enquiry.best_qty = res.best_list.qty;
                                Enquiry.best_price = res.best_list.current_price;
                                //lblBestSupplier.Text = Enquiry.best_supplier;
                                //lblBestQty.Text = string.Format("{0:0}", Enquiry.best_qty)+ " Bags"; 
                                //lblBestPrice.Text = "Rs." + string.Format("{0:0.00}", Enquiry.best_price); 
                            }
                            else
                            {
                                //lblBestSupplier.IsVisible = false;
                                //grdBest.IsVisible = false;
                            }

                            if (flag == 0)
                            {
                                //if (_searchFilter != null || grdOffer.IsVisible == true)
                                await Navigation.PopAsync();
                                //else
                                //    control_visible(true);
                            }
                        }
                        else
                        {
                            await DisplayAlert("Yarn Brokerage", res.message, "Cancel");
                        }
                    }
                }
                else
                {
                    await DisplayAlert("Yarn Brokerage", "No Internet Connection", "Cancel");
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Yarn Brokerage", ex.Message.ToString(), "Cancel");
            }
        }

        private void control_visible(bool flag)
        {
            lblCurrentPrice.IsVisible = flag;
            txtCurrentPrice.IsVisible = flag;
            grdOffer.IsVisible = flag;
            //lblBestSupplier.IsVisible = flag;
            //grdBest.IsVisible = flag;           
            lblLine1.IsVisible = flag;
            lblLine2.IsVisible = flag;
            //if (string.IsNullOrWhiteSpace(Enquiry.best_supplier))
            //{
            //    lblBestSupplier.IsVisible = false;
            //    grdBest.IsVisible = false;
            //}
        }
        private void enquiry_control_visible(bool flag)
        {

            txtDateTime.IsEnabled = flag;

            txtLedgerName.IsEnabled = flag;
            txtExactLedgerName.IsEnabled = flag;
            txtCountName.IsEnabled = flag;
            txtQty.IsEnabled = flag;
            butQty.IsEnabled = flag;
            butSupplierClear.IsVisible = flag;
            butCustomerClear.IsVisible = flag;
            txtPrice.IsEnabled = flag;
            butPrice.IsEnabled = flag;
            txtCurrentPrice.IsEnabled = flag;




            RdoSell.IsEnabled = flag;
            RdoBuy.IsEnabled = flag;
            RdoDomestic.IsEnabled = flag;
            RdoExport.IsEnabled = flag;


            butSave.IsVisible = flag;
            butCancel.IsVisible = flag;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            Indexes.CancelClick = 1;
            await Navigation.PopAsync();
        }

        private async void TxtSupplier_Focused(object sender, EventArgs e)
        {
            if (txtLedgerName.IsEnabled == false)
                return;
            await Navigation.PushAsync(new LedgersListPage(Enquiry, Enquiry.transaction_type, null, DraftConfirmation));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //if (txtLedgerName.Text != Enquiry.ledger_name)
            //{
            //    Enquiry.count_name = "";
            //    Enquiry.count_id = 0;
            //}

            txtLedgerName.Text = Enquiry.ledger_name;
            txtExactLedgerName.Text = Enquiry.exact_ledger_name;
            txtCountName.Text = Enquiry.count_name;
            if (_searchFilter != null)
            {
                lblShowOffers.Text = _searchFilter.offer_counts;
                Enquiry.offer_counts = _searchFilter.offer_counts;
                _searchFilter.search_flag = 1;
                if (Enquiry.confirmed == 1 && grdOffer.IsVisible == false)
                {
                    lblConfirm.IsVisible = true;
                    butConfirm.IsVisible = false;
                }
            }

            //if (Enquiry.bag_weight > 0)
            //    txtBagweight.Text = string.Format("{0:0}", Convert.ToDouble(Enquiry.bag_weight)); 
            //else
            //    txtBagweight.Text = "";

            //if (txtBagweight.Text.Trim() != "")
            //{
            //    if (Convert.ToDouble(txtBagweight.Text) > 0)
            //        txtBagweight.IsEnabled = false;
            //    else
            //        txtBagweight.IsEnabled = true;
            //}
            //else
            //    txtBagweight.IsEnabled = true;
            //if (SuggestionList.Age==1) RdoSmall.IsChecked = true; else if (SuggestionList.Age == 2) RdoLittleGrown.IsChecked = true; else if (SuggestionList.Age==3) RdoGrown.IsChecked = true;
            //if (SuggestionList.Severity == 1) RdoMinimum.IsChecked = true; else if (SuggestionList.Severity == 2) RdoMedium.IsChecked=true; else if (SuggestionList.Severity == 3) RdoMaximum.IsChecked=true;
            //if (viewModel.SuggestionListDetails.Count == 0)
            //viewModel.LoadSuggestionListDetailsCommand.Execute(SuggestionList.Id);
        }

        async void CheckCound()
        {
            _searchFilter = new SearchFilter();
            _searchFilter.id = Enquiry.id;
            _searchFilter.count_id = Enquiry.count_id;
            _searchFilter.count_name = Enquiry.count_name;
            _searchFilter.ledger_id = Enquiry.ledger_id;
            _searchFilter.ledger_name = Enquiry.ledger_name;
            _searchFilter.exact_ledger_id = Enquiry.exact_ledger_id;
            _searchFilter.exact_ledger_name = Enquiry.exact_ledger_name;
            _searchFilter.current_price = Enquiry.current_price;
            _searchFilter.qty = Enquiry.qty;
            _searchFilter.qty_unit = Enquiry.qty_unit;
            _searchFilter.image = Enquiry.image;
            _searchFilter.transaction_type = (Enquiry.transaction_type == 1) ? 2 : 1;
            _searchFilter.segment = Enquiry.segment;
            _searchFilter.transaction_date_time = Enquiry.transaction_date_time;
            _searchFilter.price_per = Enquiry.price_per;
            _searchFilter.search_flag = 1;
            //await viewModel.ExecuteTotalCountListCommand(_searchFilter);
            var Count = viewModel.EnquiryCount();
            var Type = (Enquiry.transaction_type == 1) ? 2 : 1;
            var List = (Type == 2) ? (Count > 1) ? Count + " Enquiries" : Count + " Enquiry" : (Count > 1) ? Count + " Offers" : Count + " Offer";
            lblShowOffers.Text = List;
        }
        private async void TxtCount_Focused(object sender, EventArgs e)
        {
            if (txtCountName.IsEnabled == false)
                return;
            await Navigation.PushAsync(new CountListPage(Enquiry));
        }

        private void txtBagweight_Focused(object sender, FocusEventArgs e)
        {
            //if (txtBagweight.Text.Trim() != "")
            //{
            //    txtBagweight.Text = string.Format("{0:0}", Convert.ToDouble(txtBagweight.Text));
            //    if (Convert.ToDouble(txtBagweight.Text) == 0)
            //    {
            //        txtBagweight.Text = "";
            //        Enquiry.bag_weight = 0;
            //    }
            //}
            //else
            //{
            //    Enquiry.bag_weight = 0;
            //}
        }

        private void TxtQty_Focused(object sender, FocusEventArgs e)
        {
            butClear.IsVisible = true;
        }

        private void TxtPrice_Focused(object sender, FocusEventArgs e)
        {
            butClearPrice.IsVisible = true;
        }

        private void RdoDomestic_Clicked(object sender, EventArgs e)
        {
            RdoDomestic.IsChecked = true;
            RdoExport.IsChecked = false;
        }

        private void RdoExport_Clicked(object sender, EventArgs e)
        {
            RdoExport.IsChecked = true;
            RdoDomestic.IsChecked = false;
        }

        private void RdoSell_Clicked(object sender, EventArgs e)
        {
            RdoSell.IsChecked = true;
            RdoBuy.IsChecked = false;
            lblSupplier.Text = "Supplier";
            lblCustomer.Text = "Customer";
            lblPrice.Text = "Offer Price";
            lblCurrentPrice.Text = "Current Offer";
            //DraftConfirmation.supplier_id = 0;
            if (Enquiry.transaction_type != 1)
            {
                Enquiry.ledger_id = 0;
                Enquiry.ledger_name = "";
                Enquiry.exact_ledger_id = 0;
                Enquiry.exact_ledger_name = "";
                txtLedgerName.Text = "";
                txtExactLedgerName.Text = "";
            }
            Enquiry.transaction_type = 1;
        }

        private void RdoBuy_Clicked(object sender, EventArgs e)
        {
            RdoBuy.IsChecked = true;
            RdoSell.IsChecked = false;
            lblSupplier.Text = "Customer";
            lblCustomer.Text = "Supplier";
            lblPrice.Text = "Enquiry Price";
            lblCurrentPrice.Text = "Current Price";
            //DraftConfirmation.supplier_id = 0;
            if (Enquiry.transaction_type != 2)
            {
                Enquiry.ledger_id = 0;
                Enquiry.ledger_name = "";
                Enquiry.exact_ledger_id = 0;
                Enquiry.exact_ledger_name = "";
                txtLedgerName.Text = "";
                txtExactLedgerName.Text = "";

            }
            Enquiry.transaction_type = 2;
        }

        private async void Show_Offers_Clicked(object sender, EventArgs e)
        {
            _searchFilter = new SearchFilter();
            _searchFilter.id = Enquiry.id;
            _searchFilter.count_id = Enquiry.count_id;
            _searchFilter.count_name = Enquiry.count_name;
            _searchFilter.ledger_id = Enquiry.ledger_id;
            _searchFilter.ledger_name = Enquiry.ledger_name;
            _searchFilter.exact_ledger_id = Enquiry.exact_ledger_id;
            _searchFilter.exact_ledger_name = Enquiry.exact_ledger_name;
            _searchFilter.current_price = Enquiry.current_price;
            _searchFilter.qty = Enquiry.qty;
            _searchFilter.qty_unit = Enquiry.qty_unit;
            _searchFilter.image = Enquiry.image;
            _searchFilter.transaction_type = (Enquiry.transaction_type == 1) ? 2 : 1;
            _searchFilter.segment = Enquiry.segment;
            _searchFilter.transaction_date_time = Enquiry.transaction_date_time;
            _searchFilter.price_per = Enquiry.price_per;
            _searchFilter.search_flag = 1;
            await Navigation.PushAsync(new EnquiriesPage(_searchFilter));
        }

        private void ButQty_Clicked(object sender, EventArgs e)
        {
            if (butQty.Text == "BAGS")
            {
                butQty.Text = "FCL";
                Enquiry.unit = "FCL";
            }
            else if (butQty.Text == "FCL")
            {
                butQty.Text = "BOX";
                Enquiry.unit = "BOX";
            }
            else if (butQty.Text == "BOX")
            {
                butQty.Text = "PALLET";
                Enquiry.unit = "PALLET";
            }
            else if (butQty.Text == "PALLET")
            {
                butQty.Text = "BALE";
                Enquiry.unit = "BALE";
            }
            else
            {
                butQty.Text = "BAGS";
                Enquiry.unit = "BAGS";
            }
        }

        private void TxtQty_UnFocused(object sender, FocusEventArgs e)
        {
            if (txtQty.Text.Trim() != "")
            {
                txtQty.Text = string.Format("{0:0}", Convert.ToDouble(txtQty.Text));
                if (Convert.ToDouble(txtQty.Text) == 0)
                {
                    txtQty.Text = "";
                    Enquiry.qty = 0;
                }
            }
            else
            {
                Enquiry.qty = 0;
            }
            butClear.IsVisible = false;
        }

        private void ButClear_Clicked(object sender, EventArgs e)
        {
            txtQty.Text = "";
            Enquiry.qty = 0;
        }

        private void ButClearPrice_Clicked(object sender, EventArgs e)
        {
            txtPrice.Text = "";
            Enquiry.price = 0;
        }

        private void ButClearCurrentPrice_Clicked(object sender, EventArgs e)
        {
            txtCurrentPrice.Text = "";
            Enquiry.current_price = 0;
        }

        private void TxtPrice_UnFocused(object sender, FocusEventArgs e)
        {
            if (txtPrice.Text.Trim() != "")
            {
                //txtPrice.Text = string.Format("{0:0.00}", Convert.ToDouble(txtPrice.Text));
                if (Convert.ToDouble(txtPrice.Text) == 0)
                {
                    txtPrice.Text = "";
                    Enquiry.price = 0;
                }
            }
            else
            {
                Enquiry.price = 0;
            }
            butClearPrice.IsVisible = false;
        }

        private void TxtCurrentPrice_Focused(object sender, FocusEventArgs e)
        {
            butClearCurrentPrice.IsVisible = true;
        }

        private void TxtCurrentPrice_UnFocused(object sender, FocusEventArgs e)
        {
            if (txtCurrentPrice.Text.Trim() != "")
            {
                //txtPrice.Text = string.Format("{0:0.00}", Convert.ToDouble(txtPrice.Text));
                if (Convert.ToDouble(txtCurrentPrice.Text) == 0)
                {
                    txtCurrentPrice.Text = "";
                    Enquiry.current_price = 0;
                }
            }
            else
            {
                Enquiry.current_price = 0;
            }
            butClearCurrentPrice.IsVisible = false;
        }

        private async void ButConfirm_Clicked(object sender, EventArgs e)
        {
            //Application.Current.Properties["direct_confirmation"] = 1;
            storeData(1);

            await Navigation.PushAsync(new AddDraftConfirmationPage(null, _searchFilter, Enquiry));

            //await Navigation.PopToRootAsync();
        }

        private async void LblConfirm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddDraftConfirmationPage(Enquiry.confirm_list.draftConfirmations[0]));
        }

        private void ButPrice_Clicked(object sender, EventArgs e)
        {
            if (butPrice.Text == "/ KG")
            {
                butPrice.Text = "/ 5 KG";
                Enquiry.per = "/ 5 KG";
            }
            else
            {
                butPrice.Text = "/ KG";
                Enquiry.per = "/ KG";
            }
        }

        private async void TxtCustomer_Focused(object sender, EventArgs e)
        {
            if (txtExactLedgerName.IsEnabled == false)
                return;
            await Navigation.PushAsync(new LedgersListPage(Enquiry, Enquiry.transaction_type, null, DraftConfirmation, null, null, null, null, 1));
        }

        private void ButSupplierClear_Clicked(object sender, EventArgs e)
        {
            txtLedgerName.Text = "";
            Enquiry.ledger_id = 0;
            Enquiry.ledger_name = "";
            //DraftConfirmation.supplier_id = 0;
        }

        private void ButCustomerClear_Clicked(object sender, EventArgs e)
        {
            txtExactLedgerName.Text = "";
            Enquiry.exact_ledger_id = 0;
            Enquiry.exact_ledger_name = "";
        }

        private void txtDateTime_DateSelected(object sender, DateChangedEventArgs e)
        {
            Enquiry.transaction_date_time = txtDateTime.Date;
        }

        //private void ToolbarItem_Clicked(object sender, EventArgs e)
        //{
        //    viewModel.EditStoreItemsCommand();
        //}
    }
}