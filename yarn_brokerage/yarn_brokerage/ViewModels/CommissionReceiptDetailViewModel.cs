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
    public class CommissionReceiptDetailViewModel : BaseViewModel
    {
        public ObservableCollection<CommissionReceiptDetail> CommissionReceiptDetails { get; set; }
       // public CommissionReceipt CommissionReceipt;
        public Command LoadCommissionReceiptDetailsCommand { get; set; }
        public Command LoadCommissionReceiptCommand { get; set; }
        //public Command LoadItemsCommand { get; set; }
        public DateTime date { get; set; }

        public SearchConfirmationFilter SearchFilter { get; set; }
        // public Command StoreCommissionReceiptCommand { get; set; }
        
        double totalBalance = 0.00;
        public double TotalBalance
        {
            get { return totalBalance; }
            set { SetProperty(ref totalBalance, value); }
        }

        public CommissionReceiptDetailViewModel()
        {
            Title = "Dispatch Details";
            date = DateTime.Now.ToLocalTime();
            CommissionReceiptDetails = new ObservableCollection<CommissionReceiptDetail>();
           // CommissionReceipt = new CommissionReceipt();
            LoadCommissionReceiptCommand = new Command(async (object CommissionReceipt) => await ExecuteCommissionReceiptCommand(CommissionReceipt));
            //LoadCommissionReceiptDetailsCommand = new Command(async (object CommissionReceiptDetail) => await ExecuteCommissionReceiptDetailsCommand(CommissionReceiptDetail));
            //LoadItemsCommand = new Command(async (object CommissionReceiptId) => await ExecuteLoadItemsCommand(CommissionReceiptId));
        }

        public async Task ExecuteCommissionReceiptCommand(object commissionReceipt) //object confirmation_date
        {
            if (IsBusy)
                return;
            IsBusy = true;
            CommissionReceipt _CommissionReceipt = commissionReceipt as CommissionReceipt;
            try
            {
                CommissionReceiptDetails.Clear();
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
                                    new KeyValuePair<string,string>("id",_CommissionReceipt.id.ToString()),
                                });


                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/commission_receipt_detail_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            CommissionReceipt_Detail_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<CommissionReceipt_Detail_list>(response);
                            foreach (CommissionReceiptDetail item in res.CommissionReceiptDetails)
                            {
                                if (_CommissionReceipt.ledger_type == 1)
                                    item.image = "buyer.png";
                                item.receipt_details = item.invoice_no + " ( " + item.invoice_date.ToString("dd-MM-yyyy") + " )";
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
                                CommissionReceiptDetails.Add(item);
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

        public async Task ExecuteCommissionReceiptDetailsCommand(CommissionReceipt CommissionReceipt, object commissionReceiptDetail) //object confirmation_date
        {
            if (IsBusy)
                return;
            IsBusy = true;
            ObservableCollection<CommissionReceiptDetail> _CommissionReceiptDetail = commissionReceiptDetail as ObservableCollection<CommissionReceiptDetail>;
            try
            {                
                try
                {
                    foreach (CommissionReceiptDetail item in _CommissionReceiptDetail)
                    {
                        if(await checkduplicate(item.commission_invoice_id) == false)
                            CommissionReceiptDetails.Add(item);
                    }
                    if (CommissionReceipt.exclude_commission_receipt_id == "")
                        splitReceiptAmount(CommissionReceipt);
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
       
        public async Task<Boolean> checkduplicate(int draft_confirm_detail_id)
        {
            int i = CommissionReceiptDetails.Where(x => x.commission_invoice_id == draft_confirm_detail_id).Count();
            if (i > 0)
                return true;
            else
                return false;
        }

        public async Task<int> CommissionReceiptCount()
        {
            return CommissionReceiptDetails.Count();
        }

        public async Task<double> CommissionReceiptTotalAmount()
        {
            return CommissionReceiptDetails.Sum(x => Convert.ToDouble(x.commission_receipt_amount));
        }

        public async Task<string> StoreCommissionReceiptCommand(CommissionReceipt CommissionReceipt)
        {
            //Indexes enquiry = (Indexes)_enquiry;
            try
            {
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    foreach (CommissionReceiptDetail item in CommissionReceiptDetails)
                    {
                        using (var cl = new HttpClient())
                        {
                            HttpContent formcontent = null;

                            formcontent = new FormUrlEncodedContent(new[]
                            {
                            new KeyValuePair<string,string>("id",item.id.ToString()),
                            new KeyValuePair<string,string>("commission_receipt_id",CommissionReceipt.id.ToString()),
                            new KeyValuePair<string,string>("commission_invoice_id",item.commission_invoice_id.ToString()),
                            new KeyValuePair<string,string>("total_commission",item.total_commission.ToString()),
                            new KeyValuePair<string,string>("commission_receipt_amount",item.commission_receipt_amount.ToString()),
                            new KeyValuePair<string,string>("ledger_type",CommissionReceipt.ledger_type.ToString()),
                            new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                        });

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/commission_receipt_store_detail", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            //CommissionReceipt_Store_Result res = Newtonsoft.Json.JsonConvert.DeserializeObject<CommissionReceipt_Store_Result>(response);

                            //res.CommissionReceipts.price_per = res.CommissionReceipts.price.ToString() + " " + res.CommissionReceipts.per;
                            ////foreach (CommissionReceipt item in res.CommissionReceipts)
                            ////{
                            //CommissionReceipt = res.CommissionReceipts;
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

        public async Task<string> DeleteCommissionReceiptDetailsCommand(CommissionReceipt CommissionReceipt, int draft_confirmation_detail_id) //object confirmation_date
        {
            CommissionReceiptDetail CIDetails = CommissionReceiptDetails.Where(x => x.commission_invoice_id == draft_confirmation_detail_id).FirstOrDefault();


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
                                    new KeyValuePair<string,string>("draft_confirmation_detail_id",CIDetails.commission_invoice_id.ToString()),
                                    new KeyValuePair<string,string>("ledger_type",CommissionReceipt.ledger_type.ToString()),
                                });


                                    var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/commission_receipt_delete_detail", formcontent);

                                    //request.EnsureSuccessStatusCode(); 

                                    var response = await request.Content.ReadAsStringAsync();

                                    //CommissionReceipt_Detail_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<CommissionReceipt_Detail_list>(response);
                                    //foreach (CommissionReceiptDetails item in res.CommissionReceiptDetails)
                                    //{
                                    //    CommissionReceiptDetails.Add(item);
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
                CommissionReceiptDetails.Remove(CIDetails);
            }

            return "Record Deleted Sucessfully";
        }

        internal string ExcludeCommisionReceiptId()
        {
            return string.Join(",", CommissionReceiptDetails.Where(p => p.id == 0)
                                 .Select(p => p.commission_invoice_id.ToString()));
        }

        internal void splitReceiptAmount(CommissionReceipt commissionReceipt)
        {
            double balance_amount;
            balance_amount = commissionReceipt.total_receipt_amount;
            ObservableCollection<CommissionReceiptDetail> crd = new ObservableCollection<CommissionReceiptDetail>(CommissionReceiptDetails);
            CommissionReceiptDetails.Clear();
            foreach (CommissionReceiptDetail item in crd)
            {
                item.commission_receipt_amount = (balance_amount > item.total_commission) ? item.total_commission : balance_amount;
                balance_amount = balance_amount - item.commission_receipt_amount;                
                CommissionReceiptDetails.Add(item);
            }
            TotalBalanceAmount(commissionReceipt);
        }
        internal void TotalBalanceAmount(CommissionReceipt commissionReceipt)
        {
            TotalBalance = commissionReceipt.total_receipt_amount - CommissionReceiptDetails.Sum(x => x.commission_receipt_amount);
        }

        internal void UpdateCommissionReceiptDetail(CommissionReceipt commissionReceipt, CommissionReceiptDetail item)
        {
            var index = CommissionReceiptDetails.IndexOf(item);
            CommissionReceiptDetails.Remove(item);
            CommissionReceiptDetails.Insert(index, item);
            TotalBalanceAmount(commissionReceipt);
        }
    }    
}
