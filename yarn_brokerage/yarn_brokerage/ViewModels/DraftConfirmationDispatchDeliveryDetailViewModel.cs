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
using System.Linq;

namespace yarn_brokerage.ViewModels
{
    public class DraftConfirmationDispatchDeliveryDetailViewModel : BaseViewModel
    {
        public ObservableCollection<DraftConfirmationPayment> draftConfirmationPayments { get; set; }
        // public DraftConfirmation DraftConfirmation;
        public Command LoadPaymentDetailsCommand { get; set; }
        //public Command LoadItemsCommand { get; set; }
        public DateTime date { get; set; }

        public DraftConfirmation draftConfirmation1 { get; set; }
        public SearchConfirmationFilter SearchFilter { get; set; }
        // public Command StoreDraftConfirmationCommand { get; set; }

        public DraftConfirmationDispatchDeliveryDetailViewModel()
        {
            //Title = "Dispatch Details";
            date = DateTime.Now.ToLocalTime();
            draftConfirmationPayments = new ObservableCollection<DraftConfirmationPayment>();
            // DraftConfirmation = new DraftConfirmation();
            LoadPaymentDetailsCommand = new Command(async (object draftConfirmationDispatchDelivery) => await ExecuteDraftConfirmationDetailsCommand(draftConfirmationDispatchDelivery));
            //LoadItemsCommand = new Command(async (object draftConfirmationId) => await ExecuteLoadItemsCommand(draftConfirmationId));
        }

        public async Task ExecuteDraftConfirmationDetailsCommand(object dCDispatchDelivery) //object confirmation_date
        {
            if (IsBusy)
                return;
            IsBusy = true;
            DraftConfirmationDispatchDelivery draftConfirmationDispatchDelivery = dCDispatchDelivery as DraftConfirmationDispatchDelivery;
            try
            {
                draftConfirmationPayments.Clear();
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
                                    new KeyValuePair<string,string>("id",draftConfirmationDispatchDelivery.draft_confirmation_id.ToString()),
                                    new KeyValuePair<string,string>("detail_id",draftConfirmationDispatchDelivery.id.ToString()),
                                });


                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_dispatch_delivery_detail_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            DraftConfirmationPayment_List res = Newtonsoft.Json.JsonConvert.DeserializeObject<DraftConfirmationPayment_List>(response);
                            foreach (DraftConfirmationPayment item in res.draftConfirmationPayments)
                            {
                                //item.invoice_amount = draftConfirmationDispatchDelivery.invoice_value - res.draftConfirmationPayments.Sum(x => Convert.ToDecimal(x.invoice_amount));
                                //if (item.amount > item.invoice_amount)
                                //    item.excess_amount = item.amount - item.invoice_amount;
                                //else
                                //{
                                //    item.invoice_amount = item.amount;
                                //    item.excess_amount = (item.amount - item.invoice_amount > 0) ? item.amount - item.invoice_amount : 0;
                                //}
                                //await RewritePayment(item);
                                item.utr_number_string = ((item.from_advance_id > 0) ? "Advance on " : "UTR : ") + item.utr_number;
                                draftConfirmationPayments.Add(item);
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
        public async Task<InvoiceDetails> CalculateInvoice(DraftConfirmation draftConfirmation) //object confirmation_date
        {
            InvoiceDetails invoiceDetails = new InvoiceDetails();
            try
            {
                draftConfirmationPayments.Clear();
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
                                    new KeyValuePair<string,string>("supplier_id",draftConfirmation.supplier_id.ToString()),
                                    new KeyValuePair<string,string>("count_id",draftConfirmation.count_id.ToString()),
                                });


                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_invoice_details", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            InvoiceDetails res = Newtonsoft.Json.JsonConvert.DeserializeObject<InvoiceDetails>(response);
                            invoiceDetails = res;
                            //foreach (DraftConfirmationDetails item in res.draftConfirmationPayments)
                            //{
                            //    draftConfirmationPayments.Add(item);
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

            }
            return invoiceDetails;
        }

        public async Task<int> DraftConfirmationCount()
        {
            return draftConfirmationPayments.Count();
        }

        public async Task<decimal> DraftConfirmationTotalAmount()
        {
            return draftConfirmationPayments.Sum(x => Convert.ToDecimal(x.invoice_amount));
        }
        public async Task<decimal> DraftConfirmationTotalUTRAmount()
        {
            return draftConfirmationPayments.Sum(x => Convert.ToDecimal(x.amount));
        }

        public async Task<DraftConfirmationPayment> RewritePayment(DraftConfirmationPayment Payment)
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

        public async void StoreDraftConfirmationCommand(DraftConfirmationDispatchDelivery DraftConfirmationDispatchDelivery)
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
                            new KeyValuePair<string,string>("id",DraftConfirmationDispatchDelivery.id.ToString()),
                            new KeyValuePair<string,string>("draft_confirmation_id",DraftConfirmationDispatchDelivery.draft_confirmation_id.ToString()),
                            new KeyValuePair<string,string>("bags_ready",DraftConfirmationDispatchDelivery.bags_ready.ToString()),
                            new KeyValuePair<string,string>("payment_ready",DraftConfirmationDispatchDelivery.payment_ready.ToString()),
                            new KeyValuePair<string,string>("payment_received",DraftConfirmationDispatchDelivery.payment_received.ToString()),
                            new KeyValuePair<string,string>("transporter_ready",DraftConfirmationDispatchDelivery.transporter_ready.ToString()),
                            new KeyValuePair<string,string>("transporter_id",DraftConfirmationDispatchDelivery.transporter_id.ToString()),
                            new KeyValuePair<string,string>("dispatched",DraftConfirmationDispatchDelivery.dispatched.ToString()),
                            new KeyValuePair<string,string>("lr_number",DraftConfirmationDispatchDelivery.lr_number.ToString()),
                            new KeyValuePair<string,string>("dispatched_date",DraftConfirmationDispatchDelivery.dispatched_date.ToString("yyyy/MM/dd")),
                            new KeyValuePair<string,string>("driver_name",DraftConfirmationDispatchDelivery.driver_name.ToString()),
                            new KeyValuePair<string,string>("truck_no",DraftConfirmationDispatchDelivery.truck_no.ToString()),
                            new KeyValuePair<string,string>("godown_unloading_id",DraftConfirmationDispatchDelivery.godown_unloading_id.ToString()),
                            new KeyValuePair<string,string>("invoice_details",DraftConfirmationDispatchDelivery.invoice_details.ToString()),
                            new KeyValuePair<string,string>("invoice_date",DraftConfirmationDispatchDelivery.invoice_date.ToString("yyyy/MM/dd")),
                            new KeyValuePair<string,string>("invoice_number",DraftConfirmationDispatchDelivery.invoice_number.ToString()),
                            new KeyValuePair<string,string>("invoice_amount",DraftConfirmationDispatchDelivery.invoice_amount.ToString()),
                            new KeyValuePair<string,string>("delivered",DraftConfirmationDispatchDelivery.delivered.ToString()),
                            new KeyValuePair<string,string>("delivery_date",DraftConfirmationDispatchDelivery.delivery_date.ToString("yyyy/MM/dd")),
                            new KeyValuePair<string,string>("delivery_remarks",DraftConfirmationDispatchDelivery.delivery_remarks.ToString()),
                            new KeyValuePair<string,string>("customer_acknowledgement",DraftConfirmationDispatchDelivery.customer_acknowledgement.ToString()),
                            new KeyValuePair<string,string>("acknowledgement_date",DraftConfirmationDispatchDelivery.acknowledgement_date.ToString("yyyy/MM/dd")),
                            new KeyValuePair<string,string>("acknowledgement_remarks",DraftConfirmationDispatchDelivery.acknowledgement_remarks.ToString()),
                            new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_dispatch_delivery_detail_store", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        DraftConfirmation_Store_Result res = Newtonsoft.Json.JsonConvert.DeserializeObject<DraftConfirmation_Store_Result>(response);

                        //res.draftConfirmations.price_per = res.draftConfirmations.price.ToString() + " " + res.draftConfirmations.per;
                        ////foreach (DraftConfirmation item in res.draftConfirmations)
                        ////{
                        //draftConfirmation = res.draftConfirmations;
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

        public async Task SendMessageCommand(DraftConfirmation DraftConfirmation)
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
                            new KeyValuePair<string,string>("confirmation_no",DraftConfirmation.confirmation_no.ToString()),
                            new KeyValuePair<string,string>("draft_confirmation_id",DraftConfirmation.id.ToString()),
                            new KeyValuePair<string,string>("count_name",DraftConfirmation.count_name.ToString()),
                            new KeyValuePair<string,string>("supplier_name",DraftConfirmation.supplier_name.ToString()),
                            new KeyValuePair<string,string>("customer_name",DraftConfirmation.customer_name.ToString()),
                            new KeyValuePair<string,string>("qty_unit",DraftConfirmation.qty_unit),
                            new KeyValuePair<string,string>("price",DraftConfirmation.price.ToString()),
                            new KeyValuePair<string,string>("customer_whatsapp_group",(DraftConfirmation.CustomerSMS == null) ? "" : DraftConfirmation.CustomerSMS ),
                            new KeyValuePair<string,string>("invoice_value",DraftConfirmation.invoice_value.ToString()),
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/whatsapp_send_pending_confirmation_from_app", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        //DraftConfirmation_Store_Result res = Newtonsoft.Json.JsonConvert.DeserializeObject<DraftConfirmation_Store_Result>(response);


                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }
        public async Task<int> SendToProgramApprovalCommand(DraftConfirmationDispatchDelivery DraftConfirmationDispatchDelivery)
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
                            new KeyValuePair<string,string>("id",DraftConfirmationDispatchDelivery.id.ToString()),
                            new KeyValuePair<string,string>("draft_confirmation_id",DraftConfirmationDispatchDelivery.draft_confirmation_id.ToString()),
                            new KeyValuePair<string,string>("allow_credit_billing",DraftConfirmationDispatchDelivery.allow_credit_billing.ToString()),
                            new KeyValuePair<string,string>("status","1"),
                            new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_send_to_program_approval_store", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        DraftConfirmation_Store_Result res = Newtonsoft.Json.JsonConvert.DeserializeObject<DraftConfirmation_Store_Result>(response);

                        draftConfirmation1 = res.draftConfirmations;

                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return draftConfirmation1.id;
        }

        public async Task<int> ProgramApprovalCommand(DraftConfirmationDispatchDelivery DraftConfirmationDispatchDelivery, int approval_flag)
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
                            new KeyValuePair<string,string>("id",DraftConfirmationDispatchDelivery.id.ToString()),
                            new KeyValuePair<string,string>("draft_confirmation_id",DraftConfirmationDispatchDelivery.draft_confirmation_id.ToString()),
                            new KeyValuePair<string,string>("program_approved",approval_flag.ToString()),
                            new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_program_approval_store", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        DraftConfirmation_Store_Result res = Newtonsoft.Json.JsonConvert.DeserializeObject<DraftConfirmation_Store_Result>(response);

                        draftConfirmation1 = res.draftConfirmations;
                        //res.draftConfirmations.price_per = res.draftConfirmations.price.ToString() + " " + res.draftConfirmations.per;
                        ////foreach (DraftConfirmation item in res.draftConfirmations)
                        ////{
                        //draftConfirmation = res.draftConfirmations;
                        //}

                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return draftConfirmation1.id;
        }

        public async void RemoveFromCurrentPlan(DraftConfirmationDispatchDelivery DraftConfirmationDispatchDelivery)
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
                            new KeyValuePair<string,string>("id",DraftConfirmationDispatchDelivery.id.ToString()),
                            new KeyValuePair<string,string>("draft_confirmation_id",DraftConfirmationDispatchDelivery.draft_confirmation_id.ToString()),
                            new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_remove_from_current_plan", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        DraftConfirmation_Store_Result res = Newtonsoft.Json.JsonConvert.DeserializeObject<DraftConfirmation_Store_Result>(response);

                        //res.draftConfirmations.price_per = res.draftConfirmations.price.ToString() + " " + res.draftConfirmations.per;
                        ////foreach (DraftConfirmation item in res.draftConfirmations)
                        ////{
                        //draftConfirmation = res.draftConfirmations;
                        //}

                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                //return "Failure";
            }
            //return "Sucess";
        }

        public async Task<string> DeleteDraftConfirmationDetailsCommand(object draftConfirmationId) //object confirmation_date
        {
            if (IsBusy)
                return "Record Not Deleted";
            IsBusy = true;
            try
            {
                draftConfirmationPayments.Clear();
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
                                    new KeyValuePair<string,string>("id",draftConfirmationId.ToString()),
                                });


                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_delete_detail", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            //DraftConfirmation_Detail_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<DraftConfirmation_Detail_list>(response);
                            //foreach (DraftConfirmationDetails item in res.draftConfirmationPayments)
                            //{
                            //    draftConfirmationPayments.Add(item);
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

        public async Task<DraftConfirmation> getDraftConfirmation(object draftConfirmationId) //object confirmation_date
        {
            DraftConfirmation draftConfirmation = new DraftConfirmation();
            try
            {
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
                                    new KeyValuePair<string,string>("id",draftConfirmationId.ToString()),
                                });


                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_data", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            DraftConfirmation_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<DraftConfirmation_list>(response);
                            draftConfirmation = res.draftConfirmations[0];
                            //DraftConfirmation_Detail_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<DraftConfirmation_Detail_list>(response);
                            //foreach (DraftConfirmationDetails item in res.draftConfirmationPayments)
                            //{
                            //    draftConfirmationPayments.Add(item);
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
            return draftConfirmation;
        }


        internal async Task<string> rejectDraftConfirmation(int DraftConfirmationId, int DraftConfirmationDispatchId = 0)
        {
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
                            new KeyValuePair<string,string>("id",DraftConfirmationId.ToString()),
                            new KeyValuePair<string,string>("draft_confirmation_dispatch_id",DraftConfirmationDispatchId.ToString()),
                            new KeyValuePair<string,string>("transaction_date_time",DateTime.Now.ToLocalTime().ToString("yyyy/MM/ddTHH:mm")),
                            new KeyValuePair<string,string>("status","5"),
                            new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                            new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_approval", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        Approval_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<Approval_list>(response);

                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return "failure";
            }
            return "sucess";
        }

        internal decimal DraftConfirmationTotalBalance()
        {
            return draftConfirmationPayments.Sum(x => Convert.ToDecimal(x.amount) - Convert.ToDecimal(x.utilized_amount));
        }

        public async Task<DraftConfirmationDetails> GetDraftConfirmationDetails(object draftConfirmationDetailsId) //object confirmation_date
        {
            DraftConfirmationDetails draftConfirmationDetails = new DraftConfirmationDetails();
            try
            {
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
                                    new KeyValuePair<string,string>("id",draftConfirmationDetailsId.ToString()),
                                    new KeyValuePair<string,string>("getFirst","1"),
                                });


                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_detail_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            DraftConfirmation_Detail_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<DraftConfirmation_Detail_list>(response);
                            draftConfirmationDetails = res.draftConfirmationDetails[0];
                            //DraftConfirmation_Detail_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<DraftConfirmation_Detail_list>(response);
                            //foreach (DraftConfirmationDetails item in res.draftConfirmationPayments)
                            //{
                            //    draftConfirmationPayments.Add(item);
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
            return draftConfirmationDetails;
        }
    }
}
