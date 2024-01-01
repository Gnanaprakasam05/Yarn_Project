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
    public class PaymentViewModel : BaseViewModel
    {
        public ObservableCollection<DraftConfirmationPayment> DraftConfirmationPayments { get; set; }
        public Command LoadItemsCommand { get; set; }
        //public Command SearchItemsCommand { get; set; }
        public DateTime date { get; set; }

        //public SearchFilter SearchFilter { get; set; }
        // public Command StorePaymentCommand { get; set; }

        public PaymentViewModel()
        {
            Title = "Payment Confirmation List";
            date = DateTime.Now.ToLocalTime();
            DraftConfirmationPayments = new ObservableCollection<DraftConfirmationPayment>();
            //if (searchFilter == null)
                LoadItemsCommand = new Command(async(object DCDispatchDelivery) => await ExecuteLoadItemsCommand(DCDispatchDelivery));
            //else if (searchFilter != null)
            //LoadItemsCommand = new Command(async (object searchFilters) => await ExecuteStoreItemsCommand(searchFilter));
            //SearchItemsCommand = new Command(async (object searchFilters) => await ExecuteStoreItemsCommand(searchFilters));
            // StorePaymentCommand = new Command(async (object Payment) => await ExecuteStorePaymentCommand(Payment));
            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as Item;
            //    Payment.Add(newItem);
            //    await DataStore.AddItemAsync(newItem);
            //});
        }

        async Task ExecuteLoadItemsCommand(object DCDispatchDelivery)
        {
            if (IsBusy)
                return;            
            IsBusy = true;
            DraftConfirmationDispatchDelivery draftConfirmationDispatchDelivery = DCDispatchDelivery as DraftConfirmationDispatchDelivery;
            try
            {
                DraftConfirmationPayments.Clear();
               
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
                                    new KeyValuePair<string,string>("detail_id",draftConfirmationDispatchDelivery.id.ToString()),
                                    new KeyValuePair<string,string>("customer_id",draftConfirmationDispatchDelivery.customer_id.ToString()),
                                    new KeyValuePair<string,string>("supplier_id",draftConfirmationDispatchDelivery.supplier_id.ToString()),
                                    new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                    new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),                                    
                                });
                            

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/payment_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            DraftConfirmationPayment_List res = Newtonsoft.Json.JsonConvert.DeserializeObject<DraftConfirmationPayment_List>(response);
                            foreach (DraftConfirmationPayment item in res.draftConfirmationPayments)
                            {
                                item.advance_amount = item.excess_amount - item.utilized_amount;
                                DraftConfirmationPayments.Add(item);
                            }
                            //if (res.totalRows > 0)
                            //    No_Of_Count_With_Bag_Weight = "" + res.totalRows + " Nos. ( " + res.payment_value + "  )";
                            //else
                            //    No_Of_Count_With_Bag_Weight = "No Records.";
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

        public async Task<Payment> GetPaymentAsync(int id)
        {
            Payment item = new Payment();

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
                                    new KeyValuePair<string,string>("id",id.ToString()),
                                    new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                    new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                    //new KeyValuePair<string,string>("confirmation_date",_confirmation_date.ToString("yyyy-MM-dd"))
                                });


                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/payment_list", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        Payment_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<Payment_list>(response);
                        item = res.Payments[0];
                        if (Application.Current.Properties["user_type"].ToString() == "1")
                            item.admin_user = true;
                        if (item.price != Convert.ToInt32(item.price))
                            item.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.price)));
                        else
                            item.price = Convert.ToInt32(item.price);
                        item.price_per = item.price.ToString() + " " + item.per;                        
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return item;
        }
        public async Task<DraftConfirmationPayment> StorePaymentCommand(DraftConfirmationPayment Payment)
        {
            //Indexes Payment = (Indexes)_Payment;
            DraftConfirmationPayment item = new DraftConfirmationPayment();
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
                            new KeyValuePair<string,string>("id",Payment.id.ToString()),
                            new KeyValuePair<string,string>("draft_confirmation_id",Payment.draft_confirmation_id.ToString()),
                            new KeyValuePair<string,string>("draft_confirmation_detail_id",Payment.draft_confirmation_detail_id.ToString()),
                            new KeyValuePair<string,string>("transaction_date_time",DateTime.Now.ToLocalTime().ToString("yyyy/MM/ddTHH:mm")),
                            new KeyValuePair<string,string>("payment_date",Payment.payment_date.ToString("yyyy/MM/dd")),
                            new KeyValuePair<string,string>("amount",Payment.amount.ToString()),
                            new KeyValuePair<string,string>("invoice_amount",Payment.invoice_amount.ToString()),
                            new KeyValuePair<string,string>("excess_amount",Payment.excess_amount.ToString()),
                            new KeyValuePair<string,string>("utilized_amount",Payment.utilized_amount.ToString()),
                            new KeyValuePair<string,string>("from_advance_id",Payment.from_advance_id.ToString()),
                            new KeyValuePair<string,string>("utr_number",Payment.utr_number.ToString()),
                            //new KeyValuePair<string,string>("status",Payment.status.ToString()),                                            
                            new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                            new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                        });
                        
                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/payment_store", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        DraftConfirmationPayment res = Newtonsoft.Json.JsonConvert.DeserializeObject<DraftConfirmationPayment>(response);
                        item = res;
                        //if (Application.Current.Properties["user_type"].ToString() == "1")
                        //    item.admin_user = true;
                        //if (item.price != Convert.ToInt32(item.price))
                        //    item.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.price)));
                        //else
                        //    item.price = Convert.ToInt32(item.price);
                        //item.price_per = item.price.ToString() + " " + item.per;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return item;
        }
        public async Task<string> DeleteDraftConfirmationPaymentCommand(object paymentId) //object confirmation_date
        {
            if (IsBusy)
                return "Record Not Deleted";
            IsBusy = true;
            try
            {
                DraftConfirmationPayments.Clear();
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
                                    new KeyValuePair<string,string>("id",paymentId.ToString()),
                                });


                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_delete_payment", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            //DraftConfirmation_Detail_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<DraftConfirmation_Detail_list>(response);
                            //foreach (DraftConfirmationDetails item in res.draftConfirmationDetails)
                            //{
                            //    draftConfirmationDetails.Add(item);
                            //}

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
            return "Record Deleted Sucessfully";
        }
    }
}