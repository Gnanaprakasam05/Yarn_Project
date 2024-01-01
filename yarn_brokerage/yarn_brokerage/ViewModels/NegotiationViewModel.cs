using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using yarn_brokerage.Models;
using yarn_brokerage.Views;
using Xamarin.Essentials;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace yarn_brokerage.ViewModels
{
    public class NegotiationViewModel : BaseViewModel
    {
        public ObservableCollection<Negotiation> Negotiations { get; set; }
        public Command LoadItemsCommand { get; set; }
        public DateTime date { get; set; }
        // public Command StoreNegotiationCommand { get; set; }

        public NegotiationViewModel()
        {
            Title = "Negotiation";
            date = DateTime.Now.ToLocalTime();
            Negotiations = new ObservableCollection<Negotiation>();
            LoadItemsCommand = new Command(async (object negotiation_date) => await ExecuteLoadItemsCommand(negotiation_date));
            // StoreNegotiationCommand = new Command(async (object negotiation) => await ExecuteStoreNegotiationCommand(negotiation));
            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as Item;
            //    Negotiation.Add(newItem);
            //    await DataStore.AddItemAsync(newItem);
            //});
        }

        async Task ExecuteLoadItemsCommand(object negotiation_date)
        {
            if (IsBusy)
                return;
            DateTime _negotiation_date;
            IsBusy = true;

            try
            {
                Negotiations.Clear();
                if (negotiation_date != null)
                    _negotiation_date = (DateTime)negotiation_date;
                else
                {
                    _negotiation_date = date;
                }
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
                                    new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                    new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                    new KeyValuePair<string,string>("negotiation_date",_negotiation_date.ToString("yyyy-MM-dd"))
                                });
                            

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/negotiation_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            Negotiation_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<Negotiation_list>(response);
                            foreach (Negotiation item in res.negotiations)
                            {
                                if (Application.Current.Properties["user_type"].ToString() == "1")
                                    item.admin_user = true;
                                Negotiations.Add(item);
                            }

                        }
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        internal async void StoreNegotiationCommand(Negotiation negotiation)
        {
            //Indexes negotiation = (Indexes)_negotiation;
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
                            new KeyValuePair<string,string>("id",negotiation.id.ToString()),
                            new KeyValuePair<string,string>("transaction_date_time",negotiation.transaction_date_time.ToString("yyyy/MM/ddThh:mm:ss")),                            
                            new KeyValuePair<string,string>("customer_id",negotiation.customer_id.ToString()),
                            new KeyValuePair<string,string>("supplier_id",negotiation.supplier_id.ToString()),
                            new KeyValuePair<string,string>("customer_enquiry_id",negotiation.customer_enquiry_id.ToString()),
                            new KeyValuePair<string,string>("supplier_enquiry_id",negotiation.supplier_enquiry_id.ToString()),
                            new KeyValuePair<string,string>("bags",negotiation.bags.ToString()),
                            new KeyValuePair<string,string>("available_bags",negotiation.available_bags.ToString()),
                            new KeyValuePair<string,string>("last_offer_price",negotiation.last_offer_price.ToString()),
                            new KeyValuePair<string,string>("last_bid_price",negotiation.last_bid_price.ToString()),
                            new KeyValuePair<string,string>("current_offer_price",negotiation.current_offer_price.ToString()),
                            new KeyValuePair<string,string>("current_bid_price",negotiation.current_bid_price.ToString()),
                            new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/negotiation_store", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        //Negotiation res = Newtonsoft.Json.JsonConvert.DeserializeObject<Negotiation>(response);
                        //foreach (Indexes item in res.Index)
                        //{
                        //    Negotiation.Add(item);
                        //}

                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            // return "Sucess";
        }
    }
}