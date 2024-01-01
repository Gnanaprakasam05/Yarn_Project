using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
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
    public partial class AddNegotiationPage : ContentPage
    {
        public Negotiation Negotiation { get; set; }
        
        NegotiationViewModel viewModel;
        public AddNegotiationPage(Negotiation negotiation)
        {
            InitializeComponent();
            
            viewModel = new NegotiationViewModel();
            if (negotiation == null)
            {
                Negotiation = new Negotiation();

                Title = "Add Negotiation";
                Negotiation.transaction_date_time = DateTime.Now.ToLocalTime();
                
            }
            else
            {
                Negotiation = negotiation;

                Title = "Edit Negotiation";
                
            }

                lblDateTime.Text = "Date Time";
            
            txtCurrentOffer.ReturnCommand = new Command(() => txtCurrentBid.Focus());                    
            BindingContext = this;
            if (Convert.ToDouble(txtCurrentOffer.Text) == 0)
                txtCurrentOffer.Text = "";
            if (Convert.ToDouble(txtCurrentBid.Text) == 0)
                txtCurrentBid.Text = "";
            if (Convert.ToDouble(txtBags.Text) == 0)
                txtBags.Text = "";
            if (Convert.ToDouble(txtAvailableBags.Text) == 0)
                txtAvailableBags.Text = "";
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Negotiation.customer_name))
            {
                await DisplayAlert("Alert", "Select Customer in list...", "OK");             
                return;
            }
            else if (string.IsNullOrWhiteSpace(Negotiation.supplier_name))
            {
                await DisplayAlert("Alert", "Select Supplier in list..", "OK");
                return;
            }
            else if (string.IsNullOrWhiteSpace(Negotiation.count_name))
            {
                await DisplayAlert("Alert", "Select Count in list...", "OK");
                return;
            }
            else if (Negotiation.bags <= 0)
            {
                await DisplayAlert("Alert", "Enther the Required Bags...", "OK");
                txtBags.Focus();
                return;
            }
            else if (Negotiation.available_bags <= 0)
            {
                await DisplayAlert("Alert", "Enther the Available Bags...", "OK");
                txtAvailableBags.Focus();
                return;
            }
            else if (Negotiation.current_offer_price <= 0 && Negotiation.current_bid_price <= 0)
            {
                await DisplayAlert("Alert", "Enther the Current Offer / Bid Price...", "OK");
                txtCurrentOffer.Focus();
                return;
            }          
            viewModel.StoreNegotiationCommand(Negotiation);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            txtSupplierName.Text = Negotiation.supplier_name;
            txtCustomerName.Text = Negotiation.customer_name;
            if (Negotiation.customer_id > 0 && Negotiation.supplier_id > 0)
                negotiation_count_list();
            //if (SuggestionList.Age==1) RdoSmall.IsChecked = true; else if (SuggestionList.Age == 2) RdoLittleGrown.IsChecked = true; else if (SuggestionList.Age==3) RdoGrown.IsChecked = true;
            //if (SuggestionList.Severity == 1) RdoMinimum.IsChecked = true; else if (SuggestionList.Severity == 2) RdoMedium.IsChecked=true; else if (SuggestionList.Severity == 3) RdoMaximum.IsChecked=true;
            //if (viewModel.SuggestionListDetails.Count == 0)
            //viewModel.LoadSuggestionListDetailsCommand.Execute(SuggestionList.Id);
        }
        private async void negotiation_count_list()
        {
            try
            {
                //Negotiations.Clear();
                //if (negotiation_date != null)
                //    _negotiation_date = (DateTime)negotiation_date;
                //else
                //{
                //    _negotiation_date = date;
                //}
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
                                    new KeyValuePair<string,string>("customer_id",Negotiation.customer_id.ToString()),
                                    new KeyValuePair<string,string>("supplier_id",Negotiation.supplier_id.ToString()),
                                    new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                    new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                    new KeyValuePair<string,string>("negotiation_date",Negotiation.transaction_date_time.ToString("yyyy-MM-dd"))
                                });


                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/negotiation_count_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            Enquiry_negotiation res = Newtonsoft.Json.JsonConvert.DeserializeObject<Enquiry_negotiation>(response);
                            foreach (Indexes item in res.customer_list)
                            {
                                Negotiation.customer_enquiry_id = item.id;
                                Negotiation.count_id = item.count_id;
                                Negotiation.count_name = item.count_name;
                                txtCountName.Text = item.count_name;
                                Negotiation.bags = item.qty;
                                
                                txtBags.Text = string.Format("{0:0}", Convert.ToDouble(item.qty));
                                Negotiation.initial_offer_price = item.price;
                                //txtInitialBid.Text = string.Format("{0:0.00}", Convert.ToDouble(item.price));
                                if (!(Negotiation.id > 0))
                                {
                                    if (item.current_price > 0) Negotiation.last_bid_price = item.current_price; else Negotiation.last_bid_price = item.price;
                                }
                                txtLastBid.Text = string.Format("{0:0.00}", Convert.ToDouble(Negotiation.last_bid_price));
                                
                            }
                            if (res.supplier_list.Count > 0)
                            {
                                foreach (Indexes item in res.supplier_list)
                                {
                                    //Negotiation.count_id = item.count_id;
                                    //Negotiation.count_name = item.count_name;
                                    //Negotiation.qty = item.qty;
                                    Negotiation.supplier_enquiry_id = item.id;
                                    Negotiation.initial_bid_price = item.price;
                                    txtAvailableBags.Text = string.Format("{0:0}", Convert.ToDouble(item.qty));
                                    Negotiation.available_bags = item.qty;
                                    //txtinitialOffer.Text = string.Format("{0:0.00}", Convert.ToDouble(item.price));
                                    if (!(Negotiation.id > 0))
                                    {
                                        if (item.current_price > 0) Negotiation.last_offer_price = item.current_price; else Negotiation.last_offer_price = item.price;
                                    }
                                    txtLastOffer.Text = string.Format("{0:0.00}", Convert.ToDouble(Negotiation.last_offer_price));
                                }
                            }
                            else {
                                txtSupplierName.Text = "";
                                Negotiation.supplier_id = 0;
                                Negotiation.supplier_name = "";
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    //Debug.WriteLine(ex);
                }

            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
            }
            finally
            {
                //IsBusy = false;
            }
        }
        private async void TxtCustomer_Focused(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LedgersListPage(null,2,Negotiation));
        }
        private async void TxtCount_Focused(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new CountListPage(null));
        }

        private void txtBagweight_Focused(object sender, FocusEventArgs e)
        {
            if (txtAvailableBags.Text.Trim() != "")
            {
                txtAvailableBags.Text = string.Format("{0:0}", Convert.ToDouble(txtAvailableBags.Text));
                if (Convert.ToDouble(txtAvailableBags.Text) == 0)
                {
                    txtAvailableBags.Text = "";
                    Negotiation.available_bags = 0;
                }
            }
            else
            {
                Negotiation.available_bags = 0;
            }
        }

        private void TxtQty_Focused(object sender, FocusEventArgs e)
        {
            if (txtCurrentOffer.Text.Trim() != "")
            {
                txtCurrentOffer.Text = string.Format("{0:0.00}", Convert.ToDouble(txtCurrentOffer.Text));
                if (Convert.ToDouble(txtCurrentOffer.Text) == 0)
                {
                    txtCurrentOffer.Text = "";
                    Negotiation.current_offer_price = 0;
                }
            }
            else
            {
                Negotiation.current_offer_price = 0;
            }
        }

        private void TxtPrice_Focused(object sender, FocusEventArgs e)
        {
            if (txtCurrentBid.Text.Trim() != "")
            {
                txtCurrentBid.Text = string.Format("{0:0.00}", Convert.ToDouble(txtCurrentBid.Text));
                if (Convert.ToDouble(txtCurrentBid.Text) == 0)
                {
                    txtCurrentBid.Text = "";
                    Negotiation.current_bid_price = 0;
                }
            }
            else
            {
                Negotiation.current_bid_price = 0;
            }
        }

        private async void TxtSupplier_Focused(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LedgersListPage(null,1, Negotiation));
        }

        private void TxtBags_Unfocused(object sender, FocusEventArgs e)
        {
            if (txtBags.Text.Trim() != "")
            {
                txtBags.Text = string.Format("{0:0}", Convert.ToDouble(txtBags.Text));
                if (Convert.ToDouble(txtBags.Text) == 0)
                {
                    txtBags.Text = "";
                    Negotiation.bags = 0;
                }
            }
            else
            {
                Negotiation.bags = 0;
            }
        }

        //private void RdoDomestic_Clicked(object sender, EventArgs e)
        //{
        //    RdoDomestic.IsChecked = true;
        //    RdoExport.IsChecked = false;
        //}

        //private void RdoExport_Clicked(object sender, EventArgs e)
        //{
        //    RdoExport.IsChecked = true;
        //    RdoDomestic.IsChecked = false;
        //}
    }
}