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
    public class CommissionInvoiceDetailViewModel : BaseViewModel
    {
        public ObservableCollection<CommissionInvoiceDetail> CommissionInvoiceDetails { get; set; }
       // public CommissionInvoice CommissionInvoice;
        public Command LoadCommissionInvoiceDetailsCommand { get; set; }
        public Command LoadCommissionInvoiceCommand { get; set; }
        //public Command LoadItemsCommand { get; set; }
        public DateTime date { get; set; }

        public SearchConfirmationFilter SearchFilter { get; set; }
        // public Command StoreCommissionInvoiceCommand { get; set; }

        public CommissionInvoiceDetailViewModel()
        {
            Title = "Dispatch Details";
            date = DateTime.Now.ToLocalTime();
            CommissionInvoiceDetails = new ObservableCollection<CommissionInvoiceDetail>();
           // CommissionInvoice = new CommissionInvoice();
            LoadCommissionInvoiceCommand = new Command(async (object CommissionInvoice) => await ExecuteCommissionInvoiceCommand(CommissionInvoice));
            LoadCommissionInvoiceDetailsCommand = new Command(async (object CommissionInvoiceDetail) => await ExecuteCommissionInvoiceDetailsCommand(CommissionInvoiceDetail));
            //LoadItemsCommand = new Command(async (object CommissionInvoiceId) => await ExecuteLoadItemsCommand(CommissionInvoiceId));
        }

        public async Task ExecuteCommissionInvoiceCommand(object commissionInvoice) //object confirmation_date
        {
            if (IsBusy)
                return;
            IsBusy = true;
            CommissionInvoice _CommissionInvoice = commissionInvoice as CommissionInvoice;
            try
            {
                CommissionInvoiceDetails.Clear();
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
                                    new KeyValuePair<string,string>("id",_CommissionInvoice.id.ToString()),
                                });


                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/commission_invoice_detail_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            CommissionInvoice_Detail_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<CommissionInvoice_Detail_list>(response);
                            foreach (CommissionInvoiceDetail item in res.CommissionInvoiceDetails)
                            {
                                if (_CommissionInvoice.ledger_type == 1)
                                    item.image = "buyer.png";
                                item.invoice_details = item.invoice_no + " ( " + item.invoice_date.ToString("dd-MM-yyyy") + " )";
                                //if(item.cancel_dispatch == 1)
                                //{
                                //    item.TextColor = "Red";
                                //    item.dispatch_status_visible = 1;
                                //    item.dispatch_status = "CANCELLED";
                                //}
                                //else if(item.program_approval==1 && item.program_approved == 0 && item.dispatched == 0)
                                //{
                                //    item.TextColor = "Green";
                                //    item.dispatch_status_visible = 1;
                                //    item.dispatch_status = "APPROVAL";
                                //}
                                //else if (item.program_approval == 1 && item.program_approved == 1 && item.dispatched == 0)
                                //{
                                //    item.TextColor = "Green";
                                //    item.dispatch_status_visible = 1;
                                //    item.dispatch_status = "CURRENT";
                                //}
                                //else if (item.program_approval == 1 && item.program_approved == 1 && item.dispatched == 1)
                                //{
                                //    item.TextColor = "Green";
                                //    item.dispatch_status_visible = 1;
                                //    item.dispatch_status = "DISPATCHED";
                                //}
                                CommissionInvoiceDetails.Add(item);
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

        public async Task ExecuteCommissionInvoiceDetailsCommand(object commissionInvoiceDetail) //object confirmation_date
        {
            if (IsBusy)
                return;
            IsBusy = true;
            ObservableCollection<CommissionInvoiceDetail> _CommissionInvoiceDetail = commissionInvoiceDetail as ObservableCollection<CommissionInvoiceDetail>;
            try
            {                
                try
                {
                    foreach (CommissionInvoiceDetail item in _CommissionInvoiceDetail)
                    {
                        if(await checkduplicate(item.draft_confirmation_detail_id) == false)
                            CommissionInvoiceDetails.Add(item);
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
        public async Task<InvoiceDetails> CalculateInvoice(CommissionInvoice CommissionInvoice) //object confirmation_date
        {
            InvoiceDetails invoiceDetails = new InvoiceDetails();
            try
            {
                CommissionInvoiceDetails.Clear();
                try
                {
                    var current = Connectivity.NetworkAccess;

                    if (current == NetworkAccess.Internet)
                    {
                        using (var cl = new HttpClient())
                        {
                            HttpContent formcontent = null;

                            //formcontent = new FormUrlEncodedContent(new[]
                            //{
                            //        new KeyValuePair<string,string>("supplier_id",CommissionInvoice.supplier_id.ToString()),
                            //        new KeyValuePair<string,string>("count_id",CommissionInvoice.count_id.ToString()),
                            //    });


                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_invoice_details", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            InvoiceDetails res = Newtonsoft.Json.JsonConvert.DeserializeObject<InvoiceDetails>(response);
                            invoiceDetails = res;
                            //foreach (CommissionInvoiceDetails item in res.CommissionInvoiceDetails)
                            //{
                            //    CommissionInvoiceDetails.Add(item);
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

        public async Task<Boolean> checkduplicate(int draft_confirm_detail_id)
        {
            int i = CommissionInvoiceDetails.Where(x => x.draft_confirmation_detail_id == draft_confirm_detail_id).Count();
            if (i > 0)
                return true;
            else
                return false;
        }

        public async Task<int> CommissionInvoiceCount()
        {
            return CommissionInvoiceDetails.Count();
        }

        public async Task<double> CommissionInvoiceTotalAmount()
        {
            return CommissionInvoiceDetails.Sum(x => Convert.ToDouble(x.commission_amount));
        }

        public async Task<string> StoreCommissionInvoiceCommand(CommissionInvoice CommissionInvoice)
        {
            //Indexes enquiry = (Indexes)_enquiry;
            try
            {
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    foreach (CommissionInvoiceDetail item in CommissionInvoiceDetails)
                    {
                        using (var cl = new HttpClient())
                        {
                            HttpContent formcontent = null;

                            formcontent = new FormUrlEncodedContent(new[]
                            {
                            new KeyValuePair<string,string>("id",item.id.ToString()),
                            new KeyValuePair<string,string>("commission_invoice_id",CommissionInvoice.id.ToString()),
                            new KeyValuePair<string,string>("draft_confirmation_id",item.draft_confirmation_id.ToString()),
                            new KeyValuePair<string,string>("draft_confirmation_detail_id",item.draft_confirmation_detail_id.ToString()),
                            new KeyValuePair<string,string>("ledger_id",item.ledger_id.ToString()),
                            new KeyValuePair<string,string>("invoice_no",item.invoice_no.ToString()),
                            new KeyValuePair<string,string>("invoice_date",item.invoice_date.ToString("yyyy/MM/dd")),
                            new KeyValuePair<string,string>("qty",item.qty.ToString()),
                            new KeyValuePair<string,string>("unit",item.unit.ToString()),
                            new KeyValuePair<string,string>("exmill_amount",item.exmill_amount.ToString()),
                            new KeyValuePair<string,string>("commission_amount",item.commission_amount.ToString()),
                            new KeyValuePair<string,string>("ledger_type",CommissionInvoice.ledger_type.ToString()),
                            new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                        });

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/commission_invoice_store_detail", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            //CommissionInvoice_Store_Result res = Newtonsoft.Json.JsonConvert.DeserializeObject<CommissionInvoice_Store_Result>(response);

                            //res.CommissionInvoices.price_per = res.CommissionInvoices.price.ToString() + " " + res.CommissionInvoices.per;
                            ////foreach (CommissionInvoice item in res.CommissionInvoices)
                            ////{
                            //CommissionInvoice = res.CommissionInvoices;
                            //}

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return "failure";
            }
             return "Sucess";
        }

        public async Task<string> DeleteCommissionInvoiceDetailsCommand(CommissionInvoice CommissionInvoice, int draft_confirmation_detail_id) //object confirmation_date
        {
            CommissionInvoiceDetail CIDetails = CommissionInvoiceDetails.Where(x => x.draft_confirmation_detail_id == draft_confirmation_detail_id).FirstOrDefault();


            if (CIDetails != null)
            {
                if (CIDetails.id > 0)
                {
                    if (IsBusy)
                        return "Record Not Deleted";
                    IsBusy = true;
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
                                    new KeyValuePair<string,string>("id",CIDetails.id.ToString()),
                                    new KeyValuePair<string,string>("draft_confirmation_detail_id",CIDetails.draft_confirmation_detail_id.ToString()),
                                    new KeyValuePair<string,string>("ledger_type",CommissionInvoice.ledger_type.ToString()),
                                });


                                    var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/commission_invoice_delete_detail", formcontent);

                                    //request.EnsureSuccessStatusCode(); 

                                    var response = await request.Content.ReadAsStringAsync();

                                    //CommissionInvoice_Detail_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<CommissionInvoice_Detail_list>(response);
                                    //foreach (CommissionInvoiceDetails item in res.CommissionInvoiceDetails)
                                    //{
                                    //    CommissionInvoiceDetails.Add(item);
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
                }
                CommissionInvoiceDetails.Remove(CIDetails);
            }

            return "Record Deleted Sucessfully";
        }

        internal string ExcludeCommisionInvoiceId()
        {
            return string.Join(",", CommissionInvoiceDetails.Where(p => p.id == 0)
                                 .Select(p => p.draft_confirmation_detail_id.ToString()));
        }
    }    
}
