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
    public class DraftConfirmationDetailViewModel : BaseViewModel
    {
        public ObservableCollection<DraftConfirmationDetails> draftConfirmationDetails { get; set; }
        public ObservableCollection<DraftConfirmationDetails> DraftConfirmationDetails { get; set; }
       // public DraftConfirmation DraftConfirmation;
        public Command LoadDraftConfirmationDetailsCommand { get; set; }
        //public Command LoadItemsCommand { get; set; }
        public DateTime date { get; set; }

        public SearchConfirmationFilter SearchFilter { get; set; }
        // public Command StoreDraftConfirmationCommand { get; set; }

        public DraftConfirmationDetailViewModel()
        {
            Title = "Dispatch Details";
            date = DateTime.Now.ToLocalTime();
            draftConfirmationDetails = new ObservableCollection<DraftConfirmationDetails>();
           // DraftConfirmation = new DraftConfirmation();
            LoadDraftConfirmationDetailsCommand = new Command(async (object draftConfirmationId) => await ExecuteDraftConfirmationDetailsCommand(draftConfirmationId));
            //LoadItemsCommand = new Command(async (object draftConfirmationId) => await ExecuteLoadItemsCommand(draftConfirmationId));
        }
               
        public async Task ExecuteDraftConfirmationDetailsCommand(object draftConfirmationId) //object confirmation_date
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                draftConfirmationDetails.Clear();
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


                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_detail_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            DraftConfirmation_Detail_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<DraftConfirmation_Detail_list>(response);
                            foreach (DraftConfirmationDetails item in res.draftConfirmationDetails)
                            {
                                if(item.cancel_dispatch == 1)
                                {
                                    item.TextColor = "Red";
                                    item.dispatch_status_visible = 1;
                                    item.dispatch_status = "CANCELLED";
                                }
                                else if(item.program_approval==1 && item.program_approved == 0 && item.dispatched == 0)
                                {
                                    item.TextColor = "Green";
                                    item.dispatch_status_visible = 1;
                                    item.dispatch_status = "APPROVAL";
                                }
                                else if (item.program_approval == 1 && item.program_approved == 1 && item.dispatched == 0)
                                {
                                    item.TextColor = "Green";
                                    item.dispatch_status_visible = 1;
                                    item.dispatch_status = "CURRENT";
                                }
                                else if (item.program_approval == 1 && item.program_approved == 1 && item.dispatched == 1 && item.delivered == 1 && item.invoice_details == 1 && item.customer_acknowledgement == 1)
                                {
                                    item.TextColor = "Green";
                                    item.dispatch_status_visible = 1;
                                    item.dispatch_status = "CLOSED";
                                }
                                else if (item.program_approval == 1 && item.program_approved == 1 && item.dispatched == 1)
                                {
                                    item.TextColor = "Green";
                                    item.dispatch_status_visible = 1;
                                    item.dispatch_status = "DISPATCHED";
                                }
                                else if (item.send_for_approval == 1 && item.status == 0)
                                {
                                    item.TextColor = "Green";
                                    item.dispatch_status_visible = 1;
                                    item.dispatch_status = "APPROVE";
                                }
                                else if (item.send_for_approval == 1 && item.status == 1)
                                {
                                    item.TextColor = "Green";
                                    item.dispatch_status_visible = 1;
                                    item.dispatch_status = "P. CONF";
                                }
                                draftConfirmationDetails.Add(item);
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



        //public async Task<List<DraftConfirmationDetails>> ExecuteDraftConfirmationDetails(object draftConfirmationId) //object confirmation_date
        //{
        //   List<DraftConfirmationDetails> ListDraft = new List<DraftConfirmationDetails>();
        //    IsBusy = true;
        //    try
        //    {
               
        //        try
        //        {
        //            var current = Connectivity.NetworkAccess;

        //            if (current == NetworkAccess.Internet)
        //            {
        //                using (var cl = new HttpClient())
        //                {
        //                    HttpContent formcontent = null;

        //                    formcontent = new FormUrlEncodedContent(new[]
        //                    {
        //                            new KeyValuePair<string,string>("id",draftConfirmationId.ToString()),
        //                        });


        //                    var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_detail_list", formcontent);

        //                    //request.EnsureSuccessStatusCode(); 

        //                    var response = await request.Content.ReadAsStringAsync();

        //                    DraftConfirmation_Detail_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<DraftConfirmation_Detail_list>(response);
        //                    foreach (DraftConfirmationDetails item in res.draftConfirmationDetails)
        //                    {
        //                        ListDraft.Add(
        //                            new DraftConfirmationDetails
        //                            {
        //                                bag_weight = item.bag_weight,
        //                                balance_qty = item.balance_qty,
        //                                no_of_boxes = item.no_of_boxes
        //                            }
        //                        ); ;

        //                        //DraftConfirmationDetails.Add(item);
        //                    }
        //                    //DraftConfirmationDetails.Add(res.draftConfirmationDetails);
        //                }
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.WriteLine(ex);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }

        //    return ListDraft;
        //}














        public async Task<InvoiceDetails> CalculateInvoice(DraftConfirmation draftConfirmation) //object confirmation_date
        {
            InvoiceDetails invoiceDetails = new InvoiceDetails();
            try
            {
                draftConfirmationDetails.Clear();
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
                
            }
            return invoiceDetails;
        }

        public async Task<Boolean> checkAmend()
        {
            int i = draftConfirmationDetails.Where(x => x.program_approval == 1 || x.bags_ready == 1 || x.payment_ready == 1 || x.payment_received == 1 || x.transporter_ready == 1 || x.dispatched == 1 || x.amend == 0).Count();
            if (i > 0)
                return true;
            else
                return false;
        }

        public async Task<int> DraftConfirmationCount()
        {
            return draftConfirmationDetails.Count();
        }

        public async Task<int> DraftConfirmationTotalQty()
        {
            return draftConfirmationDetails.Where(x => x.cancel_dispatch == 0).Sum(x => Convert.ToInt32(x.qty));
        }

        public async Task<int> DraftConfirmationQty()
        {
            return draftConfirmationDetails.Where(x => x.cancel_dispatch == 0).Sum(x => Convert.ToInt32(x.balance_qty));
        }


        public async Task<int> DraftConfirmationTotalCancelQty()
        {
            return draftConfirmationDetails.Where(x => x.cancel_dispatch == 1).Sum(x => Convert.ToInt32(x.qty));
        }

        public async Task<string> StoreDraftConfirmationCommand(DraftConfirmationDetails DraftConfirmationDetails)
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
                            new KeyValuePair<string,string>("id",DraftConfirmationDetails.id.ToString()),
                            new KeyValuePair<string,string>("draft_confirmation_id",DraftConfirmationDetails.draft_confirmation_id.ToString()),
                            new KeyValuePair<string,string>("dispatch_date",DraftConfirmationDetails.dispatch_date.ToString("yyyy/MM/dd")),
                            new KeyValuePair<string,string>("payment_date",DraftConfirmationDetails.payment_date.ToString("yyyy/MM/dd")),
                            new KeyValuePair<string,string>("qty",DraftConfirmationDetails.qty.ToString()),
                            new KeyValuePair<string,string>("unit",DraftConfirmationDetails.unit.ToString()),
                            new KeyValuePair<string,string>("bag_weight",DraftConfirmationDetails.bag_weight.ToString()),
                            new KeyValuePair<string,string>("no_of_boxes",DraftConfirmationDetails.no_of_boxes.ToString()),
                            new KeyValuePair<string,string>("rate_type",DraftConfirmationDetails.rate_type.ToString()),
                            new KeyValuePair<string,string>("gross_weight",DraftConfirmationDetails.gross_weight.ToString()),
                            new KeyValuePair<string,string>("gross_amount",DraftConfirmationDetails.gross_amount.ToString()),
                            new KeyValuePair<string,string>("tax_id",DraftConfirmationDetails.tax_id.ToString()),
                            new KeyValuePair<string,string>("tax_prec",DraftConfirmationDetails.tax_prec.ToString()),
                            new KeyValuePair<string,string>("tax_amount",DraftConfirmationDetails.tax_amount.ToString()),
                            new KeyValuePair<string,string>("frieght",DraftConfirmationDetails.frieght.ToString()),
                            new KeyValuePair<string,string>("insurance",DraftConfirmationDetails.insurance.ToString()),
                            new KeyValuePair<string,string>("other_charges",DraftConfirmationDetails.other_charges.ToString()),
                            new KeyValuePair<string,string>("invoice_value",DraftConfirmationDetails.invoice_value.ToString()),
                            new KeyValuePair<string,string>("cancel_dispatch",DraftConfirmationDetails.cancel_dispatch.ToString()),
                            new KeyValuePair<string,string>("remarks",DraftConfirmationDetails.remarks.ToString()),
                            new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_store_detail", formcontent);

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
                return "failure";
            }
             return "Success";
        }

        public async Task<string> DeleteDraftConfirmationDetailsCommand(object draftConfirmationId) //object confirmation_date
        {
            if (IsBusy)
                return "Record Not Deleted";
            IsBusy = true;
            try
            {
                draftConfirmationDetails.Clear();
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
