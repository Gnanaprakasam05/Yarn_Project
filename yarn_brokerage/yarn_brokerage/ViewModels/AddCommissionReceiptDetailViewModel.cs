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
using Xamarin.Forms.Extended;
using System.Linq;

namespace yarn_brokerage.ViewModels
{
    public class AddCommissionReceiptDetailViewModel : BaseViewModel
    {
        public InfiniteScrollCollection<AddCommissionReceiptDetail> AddCommissionReceiptDetails { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command SearchItemsCommand { get; set; }
        public DateTime date { get; set; }
        private const int PageSize = 20;
        public SearchConfirmationFilter SearchFilter { get; set; }
        // public Command StoreAddCommissionReceiptDetailCommand { get; set; }
        
        double totalReceipt = 0.00;
        public double TotalReceipt
        {
            get { return totalReceipt; }
            set { SetProperty(ref totalReceipt, value); }
        }

        public AddCommissionReceiptDetailViewModel(SearchConfirmationFilter searchFilter = null, string _Title="Receipt Details")
        {
            Title = _Title;
            date = DateTime.Now.ToLocalTime();
            //AddCommissionReceiptDetails = new ObservableCollection<AddCommissionReceiptDetail>();
            AddCommissionReceiptDetails = new InfiniteScrollCollection<AddCommissionReceiptDetail>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = AddCommissionReceiptDetails.Count / PageSize;

                    //var items = await _dataService.GetItemsAsync(page, PageSize);

                    using (var cl = new HttpClient())
                    {
                        HttpContent formcontent = null;
                        if (SearchFilter != null)
                        {
                            formcontent = new FormUrlEncodedContent(new[]
                            {
                                new KeyValuePair<string, string>("transaction_type", SearchFilter.transaction_type.ToString()),
                                new KeyValuePair<string, string>("count_id", SearchFilter.count_id.ToString()),
                                new KeyValuePair<string, string>("segment", SearchFilter.segment.ToString()),
                                new KeyValuePair<string, string>("confirmation_no", SearchFilter.confirmation_no.ToString()),
                                new KeyValuePair<string, string>("ledgers_id", SearchFilter.ledger_id.ToString()),
                                new KeyValuePair<string, string>("exact_ledger_id", SearchFilter.exact_ledger_id.ToString()),
                                new KeyValuePair<string, string>("supplier_id", SearchFilter.supplier_id.ToString()),
                                new KeyValuePair<string, string>("customer_id", SearchFilter.customer_id.ToString()),
                                new KeyValuePair<string, string>("user_type", Application.Current.Properties["user_type"].ToString()),
                                new KeyValuePair<string, string>("user_id", Application.Current.Properties["user_id"].ToString()),
                                new KeyValuePair<string, string>("s_user_id", SearchFilter.user_id.ToString()),
                                new KeyValuePair<string, string>("confirmation_date_from", SearchFilter.confirmation_date_from.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string, string>("confirmation_date_to", SearchFilter.confirmation_date_to.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string, string>("dispatch_date_from", SearchFilter.dispatch_date_from.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string, string>("dispatch_date_to", SearchFilter.dispatch_date_to.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string, string>("payment_date_from", SearchFilter.payment_date_from.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string, string>("payment_date_to", SearchFilter.payment_date_to.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string, string>("confirmation_date", SearchFilter.confirmation_date.ToString()),
                                new KeyValuePair<string, string>("dispatch_date", SearchFilter.dispatch_date.ToString()),
                                new KeyValuePair<string, string>("payment_date", SearchFilter.payment_date.ToString()),
                                new KeyValuePair<string,string>("search_string",SearchFilter.search_string.ToString()),
                                new KeyValuePair<string, string>("CurrentPageNumber", page.ToString()),
                                new KeyValuePair<string, string>("PageSize", PageSize.ToString())
                            });
                        }
                        else
                        {
                            formcontent = new FormUrlEncodedContent(new[]
                            {
                                new KeyValuePair<string,string>("ledger_type",_CommissionReceipt.ledger_type.ToString()),
                                new KeyValuePair<string,string>("ledger_id",_CommissionReceipt.ledger_id.ToString()),
                                new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                new KeyValuePair<string,string>("CurrentPageNumber",page.ToString()),
                                new KeyValuePair<string,string>("PageSize",PageSize.ToString()),
                                //new KeyValuePair<string,string>("transaction_date",_SearchString.current_date.ToString("yyyy-MM-dd"))
                            });
                        }
                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/commission_receipt_invoice_list", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        AddCommissionReceiptDetail_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<AddCommissionReceiptDetail_list>(response);
                        AddCommissionReceiptDetail_list AddCommissionReceiptDetail_List = new AddCommissionReceiptDetail_list();
                        AddCommissionReceiptDetail_List.totalRows = res.totalRows;
                        AddCommissionReceiptDetail_List.AddCommissionReceiptDetails = new List<AddCommissionReceiptDetail>();
                        foreach (AddCommissionReceiptDetail item in res.AddCommissionReceiptDetails)
                        {
                            if (_CommissionReceipt.ledger_type == 1)
                            {
                                item.image = "buyer.png";
                                //item.ledger_id = item.customer_id;
                                //item.ledger_name = item.customer_name;
                            }
                            else if (_CommissionReceipt.ledger_type == 2)
                            {
                                item.image = "customer.png";
                                //item.ledger_id = item.supplier_id;
                                //item.ledger_name = item.supplier_name;
                            }
                            item.receipt_details = item.invoice_no + " ( " + item.invoice_date.ToString("dd-MM-yyyy") + " )";
                            //if (Application.Current.Properties["user_type"].ToString() == "1")
                            //    item.admin_user = true;
                            //if (item.price != Convert.ToInt32(item.price))
                            //    item.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.price)));
                            //else
                            //    item.price = Convert.ToInt32(item.price);
                            //item.price_per = item.price.ToString() + " " + item.per;
                            //double diff2 = (DateTime.Today.Date.ToLocalTime() - item.transaction_date_time).TotalDays;
                            //if (diff2 > 0)
                            //    item.transaction_detail = item.transaction_detail + " - " + (int)Math.Ceiling(diff2) + " Days Old";
                            AddCommissionReceiptDetail_List.AddCommissionReceiptDetails.Add(item);
                        }
                        IsBusy = false;
                        return AddCommissionReceiptDetail_List.AddCommissionReceiptDetails;
                    }
                    // return the items that need to be added

                },
                OnCanLoadMore = () =>
                {
                    return AddCommissionReceiptDetails.Count < totalRows;
                }
            };
            
            if (searchFilter == null)
                LoadItemsCommand = new Command(async (object CommissionReceipt) => await ExecuteLoadItemsCommand(CommissionReceipt)); //object confirmation_date confirmation_date
            else if (searchFilter != null)
                LoadItemsCommand = new Command(async (object searchFilters) => await ExecuteStoreItemsCommand(searchFilter));
            SearchItemsCommand = new Command(async (object searchFilters) => await ExecuteStoreItemsCommand(searchFilters));
            // StoreAddCommissionReceiptDetailCommand = new Command(async (object AddCommissionReceiptDetail) => await ExecuteStoreAddCommissionReceiptDetailCommand(AddCommissionReceiptDetail));
            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as Item;
            //    AddCommissionReceiptDetail.Add(newItem);
            //    await DataStore.AddItemAsync(newItem);
            //});
        }

        internal void AddReceiptDetails(CommissionReceipt commissionReceipt, ObservableCollection<CommissionReceiptDetail> _CommissionReceiptDetails)
        {
            //ObservableCollection<CommissionReceiptDetail> commissionReceiptDetails = new ObservableCollection<CommissionReceiptDetail>();
            ObservableCollection<AddCommissionReceiptDetail> ACIDetails = new ObservableCollection<AddCommissionReceiptDetail>(AddCommissionReceiptDetails.Where(x => x.setActive == true).ToList());
            if (ACIDetails.Count > 0) {
                foreach(AddCommissionReceiptDetail item in ACIDetails)
                {
                    CommissionReceiptDetail commissionReceiptDetail = new CommissionReceiptDetail();
                    commissionReceiptDetail.commission_invoice_id = item.id;                    
                    commissionReceiptDetail.invoice_no = item.invoice_no;
                    commissionReceiptDetail.invoice_date = item.invoice_date;                   
                    commissionReceiptDetail.ledger_id = item.ledger_id;
                    commissionReceiptDetail.ledger_name = item.ledger_name;                    
                    commissionReceiptDetail.image = item.image;
                    commissionReceiptDetail.receipt_details = item.receipt_details;
                    commissionReceiptDetail.total_commission = (item.balance_commission == 0) ? item.total_commission : item.balance_commission;
                    commissionReceiptDetail.balance_commission = item.balance_commission;
                    commissionReceiptDetail.commission_receipt_amount = 0;
                    _CommissionReceiptDetails.Add(commissionReceiptDetail);
                }
            }
            //return commissionReceiptDetails;
        }

        //private Boolean checkduplicate(int draft_confirm_detail_id)
        //{
        //    int i = _CommissionReceiptDetails.Where(x => x.draft_confirmation_detail_id == draft_confirm_detail_id).Count();
        //    if (i > 0)
        //        return true;
        //    else
        //        return false;
        //}

        internal void UpdateReceiptDetails(int id)
        {
            //AddCommissionReceiptDetails.Clear();
            //return;
            AddCommissionReceiptDetail ACIDetails = AddCommissionReceiptDetails.Where(x => x.id == id).FirstOrDefault();
            var index = AddCommissionReceiptDetails.IndexOf(AddCommissionReceiptDetails.Where(x => x.id == id).FirstOrDefault());
            if (ACIDetails != null)
            {
                if (ACIDetails.setActive == true)
                    ACIDetails.setActive = false;
                else
                {                    
                    ACIDetails.setActive = true;
                }
            }
            if (index == -1)
                index = AddCommissionReceiptDetails.IndexOf(ACIDetails);
            AddCommissionReceiptDetails.Remove(ACIDetails);
            AddCommissionReceiptDetails.Insert(index, ACIDetails);
            TotalReceiptAmount();
        }

        internal void TotalReceiptAmount()
        {
            TotalReceipt = AddCommissionReceiptDetails.Where(x => x.setActive == true).Sum(x => x.balance_commission);
        }

        search_string _SearchString;
        CommissionReceipt _CommissionReceipt;
        private int totalRows = 0;

        async Task ExecuteLoadItemsCommand(object CommissionReceipt) //object confirmation_date 
        {
            if (IsBusy)
                return;
            //DateTime _confirmation_date;
            _CommissionReceipt = CommissionReceipt as CommissionReceipt;
            //if (searchstring != null)
            //    _SearchString = (string)searchstring;
            IsBusy = true;

            try
            {
                AddCommissionReceiptDetails.Clear();
                TotalReceipt = 0;
                //if (confirmation_date != null)
                //    _confirmation_date = (DateTime)confirmation_date;
                //else
                //{
                //    _confirmation_date = date;
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
                                new KeyValuePair<string,string>("ledger_type",_CommissionReceipt.ledger_type.ToString()),
                                new KeyValuePair<string,string>("ledger_id",_CommissionReceipt.ledger_id.ToString()),
                                new KeyValuePair<string,string>("exclude_commission_receipt_id",_CommissionReceipt.exclude_commission_receipt_id.ToString()),
                                new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                new KeyValuePair<string,string>("CurrentPageNumber","0"),
                                new KeyValuePair<string,string>("PageSize",PageSize.ToString()),
                                //new KeyValuePair<string,string>("transaction_date",_SearchString.current_date.ToString("yyyy-MM-dd"))
                            });

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/commission_receipt_invoice_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            AddCommissionReceiptDetail_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<AddCommissionReceiptDetail_list>(response);
                            foreach (AddCommissionReceiptDetail item in res.AddCommissionReceiptDetails)
                            {
                                if (_CommissionReceipt.ledger_type == 1)
                                {
                                    item.image = "buyer.png";
                                    //item.ledger_id = item.customer_id;
                                    //item.ledger_name = item.customer_name;
                                }
                                else if (_CommissionReceipt.ledger_type == 2)
                                {
                                    item.image = "customer.png";
                                    //item.ledger_id = item.supplier_id;
                                    //item.ledger_name = item.supplier_name;
                                }
                                item.receipt_details = item.invoice_no + " ( " + item.invoice_date.ToString("dd-MM-yyyy") + " )";
                                //if (Application.Current.Properties["user_type"].ToString() == "1")
                                //    item.admin_user = true;
                                //if (item.price != Convert.ToInt32(item.price))
                                //    item.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.price)));
                                //else
                                //    item.price = Convert.ToInt32(item.price);
                                //item.price_per = item.price.ToString() + " " + item.per;
                                //if (item.status == 1)
                                //    item.status_image = "approved.png";
                                //if (item.status == 5)
                                //    item.status_image = "rejected.png";
                                //double diff2 = (DateTime.Today.Date - item.transaction_date_time).TotalDays;
                                //if (diff2 > 0)
                                //    item.transaction_detail = item.transaction_detail + " - " + (int)Math.Ceiling(diff2) + " Days Old";
                                AddCommissionReceiptDetails.Add(item);
                            }
                            //AddCommissionReceiptDetails.AddRange(res.AddCommissionReceiptDetails);
                            totalRows = res.totalRows;
                            No_Of_Count_With_Bag_Weight = "";
                            //if (res.totalRows > 0)
                            //    No_Of_Count_With_Bag_Weight = "" + res.totalRows + " Nos. ( " + res.confirm_value + "  )";
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

        async Task ExecuteStoreItemsCommand(object searchFilter)
        {
            if (IsBusy)
                return;
            if (searchFilter == null)
                return;

            SearchFilter = new SearchConfirmationFilter();

            SearchFilter = (SearchConfirmationFilter)searchFilter;

            IsBusy = true;

            try
            {
                AddCommissionReceiptDetails.Clear();
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
                                new KeyValuePair<string,string>("transaction_type",SearchFilter.transaction_type.ToString()),
                                new KeyValuePair<string,string>("count_id",SearchFilter.count_id.ToString()),
                                new KeyValuePair<string,string>("segment",SearchFilter.segment.ToString()),
                                new KeyValuePair<string, string>("confirmation_no", SearchFilter.confirmation_no.ToString()),
                                new KeyValuePair<string,string>("ledgers_id",SearchFilter.ledger_id.ToString()),
                                new KeyValuePair<string,string>("exact_ledger_id",SearchFilter.exact_ledger_id.ToString()),
                                new KeyValuePair<string,string>("supplier_id",SearchFilter.supplier_id.ToString()),
                                new KeyValuePair<string,string>("customer_id",SearchFilter.customer_id.ToString()),
                                new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                new KeyValuePair<string,string>("s_user_id",SearchFilter.user_id.ToString()),
                                new KeyValuePair<string,string>("confirmation_date_from", SearchFilter.confirmation_date_from.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string,string>("confirmation_date_to", SearchFilter.confirmation_date_to.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string,string>("dispatch_date_from", SearchFilter.dispatch_date_from.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string,string>("dispatch_date_to", SearchFilter.dispatch_date_to.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string,string>("payment_date_from", SearchFilter.payment_date_from.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string,string>("payment_date_to", SearchFilter.payment_date_to.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string,string>("confirmation_date",SearchFilter.confirmation_date.ToString()),
                                new KeyValuePair<string,string>("dispatch_date",SearchFilter.dispatch_date.ToString()),
                                new KeyValuePair<string,string>("payment_date",SearchFilter.payment_date.ToString()),
                                new KeyValuePair<string,string>("search_string",SearchFilter.search_string.ToString()),
                                new KeyValuePair<string,string>("CurrentPageNumber","0"),
                                new KeyValuePair<string,string>("PageSize",PageSize.ToString())
                            });

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/commission_receipt_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            AddCommissionReceiptDetail_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<AddCommissionReceiptDetail_list>(response);
                            foreach (AddCommissionReceiptDetail item in res.AddCommissionReceiptDetails)
                            {
                                //if (Application.Current.Properties["user_type"].ToString() == "1")
                                //    item.admin_user = true;
                                //if (item.price != Convert.ToInt32(item.price))
                                //    item.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.price)));
                                //else
                                //    item.price = Convert.ToInt32(item.price);
                                //item.price_per = item.price.ToString() + " " + item.per;
                                //double diff2 = (DateTime.Today.Date - item.transaction_date_time).TotalDays;
                                //if (diff2 > 0)
                                //    item.transaction_detail = item.transaction_detail + " - " + (int)Math.Ceiling(diff2) + " Days Old";
                                AddCommissionReceiptDetails.Add(item);
                            }
                            totalRows = res.totalRows;
                            
                            //if (res.totalRows > 0)
                            //    No_Of_Count_With_Bag_Weight = "" + res.totalRows + " Nos. ( " + res.confirm_value + "  )";
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

        public async Task<AddCommissionReceiptDetail> StoreAddCommissionReceiptDetailCommand(AddCommissionReceiptDetail AddCommissionReceiptDetail)
        {
            //Indexes AddCommissionReceiptDetail = (Indexes)_AddCommissionReceiptDetail;
            //try
            //{
            //    var current = Connectivity.NetworkAccess;

            //    if (current == NetworkAccess.Internet)
            //    {
            //        using (var cl = new HttpClient())
            //        {
            //            HttpContent formcontent = null;

            //            formcontent = new FormUrlEncodedContent(new[]
            //            {
            //                new KeyValuePair<string,string>("id",AddCommissionReceiptDetail.id.ToString()),
            //                new KeyValuePair<string,string>("Receipt_no",AddCommissionReceiptDetail.Receipt_no.ToString()),
            //                new KeyValuePair<string,string>("Receipt_date",AddCommissionReceiptDetail.Receipt_date.ToString("yyyy/MM/dd")),
            //                new KeyValuePair<string,string>("ledger_type",AddCommissionReceiptDetail.ledger_type.ToString()),
            //                new KeyValuePair<string,string>("ledger_id",AddCommissionReceiptDetail.ledger_id.ToString()),
            //                new KeyValuePair<string,string>("company_id",AddCommissionReceiptDetail.company_id.ToString()),
            //                new KeyValuePair<string,string>("commission_type",AddCommissionReceiptDetail.commission_type.ToString()),
            //                new KeyValuePair<string,string>("commission_value",AddCommissionReceiptDetail.commission_value.ToString()),                            
            //                new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),                                                
            //            });
                        
            //            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/_store", formcontent);

            //            //request.EnsureSuccessStatusCode(); 

            //            var response = await request.Content.ReadAsStringAsync();

            //            AddCommissionReceiptDetail_Store_Result res = Newtonsoft.Json.JsonConvert.DeserializeObject<AddCommissionReceiptDetail_Store_Result>(response);
            //            //res.AddCommissionReceiptDetails.supplier_name = AddCommissionReceiptDetail.supplier_name;
            //            //res.AddCommissionReceiptDetails.customer_name = AddCommissionReceiptDetail.customer_name;
            //            //res.AddCommissionReceiptDetails.count_name = AddCommissionReceiptDetail.count_name;
            //            //res.AddCommissionReceiptDetails.count_name = AddCommissionReceiptDetail.count_name;
            //            //res.AddCommissionReceiptDetails.qty_unit = res.AddCommissionReceiptDetails.qty.ToString() + " " + res.AddCommissionReceiptDetails.unit;
            //            //res.AddCommissionReceiptDetails.price_per = res.AddCommissionReceiptDetails.price.ToString() + " " + res.AddCommissionReceiptDetails.per; 
            //            //foreach (AddCommissionReceiptDetail item in res.AddCommissionReceiptDetails)
            //            //{
            //            AddCommissionReceiptDetail = res.AddCommissionReceiptDetails;                            
            //            //}

            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex);
            //}
             return AddCommissionReceiptDetail;
        }

        internal async Task<string> rejectAddCommissionReceiptDetail(int AddCommissionReceiptDetailId)
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
                            new KeyValuePair<string,string>("id",AddCommissionReceiptDetailId.ToString()),
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

        internal async Task<int>DuplicateConfirmationNo(AddCommissionReceiptDetail AddCommissionReceiptDetail)
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
                            new KeyValuePair<string,string>("id",AddCommissionReceiptDetail.id.ToString()),
                            //new KeyValuePair<string,string>("confirmation_no",AddCommissionReceiptDetail.confirmation_no.ToString()),                           
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_duplicate_no", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        return Convert.ToInt32(response);

                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return 0;
            }
            return 1;
        }

        public async Task<int> getAmendCount(int AddCommissionReceiptDetailId)
        {
            int count = 0;
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
                            new KeyValuePair<string,string>("id",AddCommissionReceiptDetailId.ToString()),
                            new KeyValuePair<string,string>("getAmendmentCount","1"),
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_detail_list", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        AddCommissionReceiptDetail_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<AddCommissionReceiptDetail_list>(response);
                        count = res.AddCommissionReceiptDetails.Count();
                        //res.AddCommissionReceiptDetails.price_per = res.AddCommissionReceiptDetails.price.ToString() + " " + res.AddCommissionReceiptDetails.per;
                        ////foreach (AddCommissionReceiptDetail item in res.AddCommissionReceiptDetails)
                        ////{
                        //AddCommissionReceiptDetail = res.AddCommissionReceiptDetails;
                        //}

                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return count;
        }

        public async void SendForApproval(AddCommissionReceiptDetail AddCommissionReceiptDetail)
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
                            new KeyValuePair<string,string>("id",AddCommissionReceiptDetail.id.ToString()),                            
                            new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_send_for_approval", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        //AddCommissionReceiptDetail_Store_Result res = Newtonsoft.Json.JsonConvert.DeserializeObject<AddCommissionReceiptDetail_Store_Result>(response);

                        //res.AddCommissionReceiptDetails.price_per = res.AddCommissionReceiptDetails.price.ToString() + " " + res.AddCommissionReceiptDetails.per;
                        ////foreach (AddCommissionReceiptDetail item in res.AddCommissionReceiptDetails)
                        ////{
                        //AddCommissionReceiptDetail = res.AddCommissionReceiptDetails;
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