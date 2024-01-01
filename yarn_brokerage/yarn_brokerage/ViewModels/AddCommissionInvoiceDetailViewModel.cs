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
    public class AddCommissionInvoiceDetailViewModel : BaseViewModel
    {
        public InfiniteScrollCollection<AddCommissionInvoiceDetail> AddCommissionInvoiceDetails { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command SearchItemsCommand { get; set; }
        public DateTime date { get; set; }
        private const int PageSize = 20;
        public SearchConfirmationFilter SearchFilter { get; set; }
        // public Command StoreAddCommissionInvoiceDetailCommand { get; set; }
        
        double totalInvoice = 0.00;
        public double TotalInvoice
        {
            get { return totalInvoice; }
            set { SetProperty(ref totalInvoice, value); }
        }

        public AddCommissionInvoiceDetailViewModel(SearchConfirmationFilter searchFilter = null, string _Title="Invoice Details")
        {
            Title = _Title;
            date = DateTime.Now.ToLocalTime();
            //AddCommissionInvoiceDetails = new ObservableCollection<AddCommissionInvoiceDetail>();
            AddCommissionInvoiceDetails = new InfiniteScrollCollection<AddCommissionInvoiceDetail>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = AddCommissionInvoiceDetails.Count / PageSize;

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
                                new KeyValuePair<string,string>("ledger_type",_CommissionInvoice.ledger_type.ToString()),
                                new KeyValuePair<string,string>("ledger_id",_CommissionInvoice.ledger_id.ToString()),
                                new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                new KeyValuePair<string,string>("CurrentPageNumber",page.ToString()),
                                new KeyValuePair<string,string>("PageSize",PageSize.ToString()),
                                //new KeyValuePair<string,string>("transaction_date",_SearchString.current_date.ToString("yyyy-MM-dd"))
                            });
                        }
                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/commission_invoice_invoice_list", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        AddCommissionInvoiceDetail_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<AddCommissionInvoiceDetail_list>(response);
                        AddCommissionInvoiceDetail_list AddCommissionInvoiceDetail_List = new AddCommissionInvoiceDetail_list();
                        AddCommissionInvoiceDetail_List.totalRows = res.totalRows;
                        AddCommissionInvoiceDetail_List.AddCommissionInvoiceDetails = new List<AddCommissionInvoiceDetail>();
                        foreach (AddCommissionInvoiceDetail item in res.AddCommissionInvoiceDetails)
                        {
                            if (_CommissionInvoice.ledger_type == 1)
                            {
                                item.image = "buyer.png";
                                item.ledger_id = item.customer_id;
                                item.ledger_name = item.customer_name;
                            }
                            else if (_CommissionInvoice.ledger_type == 2)
                            {
                                item.image = "customer.png";
                                item.ledger_id = item.supplier_id;
                                item.ledger_name = item.supplier_name;
                            }
                            item.invoice_details = item.invoice_no + " ( " + item.invoice_date.ToString("dd-MM-yyyy") + " )";
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
                            AddCommissionInvoiceDetail_List.AddCommissionInvoiceDetails.Add(item);
                        }
                        IsBusy = false;
                        return AddCommissionInvoiceDetail_List.AddCommissionInvoiceDetails;
                    }
                    // return the items that need to be added

                },
                OnCanLoadMore = () =>
                {
                    return AddCommissionInvoiceDetails.Count < totalRows;
                }
            };
            
            if (searchFilter == null)
                LoadItemsCommand = new Command(async (object CommissionInvoice) => await ExecuteLoadItemsCommand(CommissionInvoice)); //object confirmation_date confirmation_date
            else if (searchFilter != null)
                LoadItemsCommand = new Command(async (object searchFilters) => await ExecuteStoreItemsCommand(searchFilter));
            SearchItemsCommand = new Command(async (object searchFilters) => await ExecuteStoreItemsCommand(searchFilters));
            // StoreAddCommissionInvoiceDetailCommand = new Command(async (object AddCommissionInvoiceDetail) => await ExecuteStoreAddCommissionInvoiceDetailCommand(AddCommissionInvoiceDetail));
            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as Item;
            //    AddCommissionInvoiceDetail.Add(newItem);
            //    await DataStore.AddItemAsync(newItem);
            //});
        }

        internal void AddInvoiceDetails(CommissionInvoice commissionInvoice, ObservableCollection<CommissionInvoiceDetail> _CommissionInvoiceDetails)
        {
            //ObservableCollection<CommissionInvoiceDetail> commissionInvoiceDetails = new ObservableCollection<CommissionInvoiceDetail>();
            ObservableCollection<AddCommissionInvoiceDetail> ACIDetails = new ObservableCollection<AddCommissionInvoiceDetail>(AddCommissionInvoiceDetails.Where(x => x.setActive == true).ToList());
            if (ACIDetails.Count > 0) {
                foreach(AddCommissionInvoiceDetail item in ACIDetails)
                {
                    CommissionInvoiceDetail commissionInvoiceDetail = new CommissionInvoiceDetail();
                    commissionInvoiceDetail.draft_confirmation_id = item.draft_confirmation_id;
                    commissionInvoiceDetail.draft_confirmation_detail_id = item.id;
                    commissionInvoiceDetail.invoice_no = item.invoice_no;
                    commissionInvoiceDetail.invoice_date = item.invoice_date;
                    commissionInvoiceDetail.qty = item.qty;
                    commissionInvoiceDetail.unit = item.unit;
                    commissionInvoiceDetail.qty_unit = item.qty_unit;
                    commissionInvoiceDetail.ledger_id = item.ledger_id;
                    commissionInvoiceDetail.ledger_name = item.ledger_name;
                    commissionInvoiceDetail.exmill_amount = item.invoice_value;
                    commissionInvoiceDetail.image = item.image;
                    commissionInvoiceDetail.invoice_details = item.invoice_details;
                    commissionInvoiceDetail.commission_amount = (commissionInvoice.commission_type == 1) ? (commissionInvoiceDetail.exmill_amount * (commissionInvoice.commission_value / 100)) : (commissionInvoiceDetail.qty * commissionInvoice.commission_value);
                    _CommissionInvoiceDetails.Add(commissionInvoiceDetail);
                }
            }
            //return commissionInvoiceDetails;
        }

        //private Boolean checkduplicate(int draft_confirm_detail_id)
        //{
        //    int i = _CommissionInvoiceDetails.Where(x => x.draft_confirmation_detail_id == draft_confirm_detail_id).Count();
        //    if (i > 0)
        //        return true;
        //    else
        //        return false;
        //}

        internal void UpdateInvoiceDetails(int id)
        {
            //AddCommissionInvoiceDetails.Clear();
            //return;
            AddCommissionInvoiceDetail ACIDetails = AddCommissionInvoiceDetails.Where(x => x.id == id).FirstOrDefault();
            var index = AddCommissionInvoiceDetails.IndexOf(AddCommissionInvoiceDetails.Where(x => x.id == id).FirstOrDefault());
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
                index = AddCommissionInvoiceDetails.IndexOf(ACIDetails);
            AddCommissionInvoiceDetails.Remove(ACIDetails);
            AddCommissionInvoiceDetails.Insert(index, ACIDetails);
            TotalInvoiceAmount();
        }

        internal void TotalInvoiceAmount()
        {
            TotalInvoice = AddCommissionInvoiceDetails.Where(x => x.setActive == true).Sum(x => x.invoice_value);
        }

        search_string _SearchString;
        CommissionInvoice _CommissionInvoice;
        private int totalRows = 0;

        async Task ExecuteLoadItemsCommand(object CommissionInvoice) //object confirmation_date 
        {
            if (IsBusy)
                return;
            //DateTime _confirmation_date;
            _CommissionInvoice = CommissionInvoice as CommissionInvoice;
            //if (searchstring != null)
            //    _SearchString = (string)searchstring;
            IsBusy = true;

            try
            {
                AddCommissionInvoiceDetails.Clear();
                TotalInvoice = 0;
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
                                new KeyValuePair<string,string>("ledger_type",_CommissionInvoice.ledger_type.ToString()),
                                new KeyValuePair<string,string>("ledger_id",_CommissionInvoice.ledger_id.ToString()),
                                new KeyValuePair<string,string>("exclude_commission_invoice_id",_CommissionInvoice.exclude_commission_invoice_id.ToString()),
                                new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                new KeyValuePair<string,string>("CurrentPageNumber","0"),
                                new KeyValuePair<string,string>("PageSize",PageSize.ToString()),
                                //new KeyValuePair<string,string>("transaction_date",_SearchString.current_date.ToString("yyyy-MM-dd"))
                            });

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/commission_invoice_invoice_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            AddCommissionInvoiceDetail_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<AddCommissionInvoiceDetail_list>(response);
                            foreach (AddCommissionInvoiceDetail item in res.AddCommissionInvoiceDetails)
                            {
                                if (_CommissionInvoice.ledger_type == 1)
                                {
                                    item.image = "buyer.png";
                                    item.ledger_id = item.customer_id;
                                    item.ledger_name = item.customer_name;
                                }
                                else if (_CommissionInvoice.ledger_type == 2)
                                {
                                    item.image = "customer.png";
                                    item.ledger_id = item.supplier_id;
                                    item.ledger_name = item.supplier_name;
                                }
                                item.invoice_details = item.invoice_no + " ( " + item.invoice_date.ToString("dd-MM-yyyy") + " )";
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
                                AddCommissionInvoiceDetails.Add(item);
                            }
                            //AddCommissionInvoiceDetails.AddRange(res.AddCommissionInvoiceDetails);
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
                AddCommissionInvoiceDetails.Clear();
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

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/commission_invoice_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            AddCommissionInvoiceDetail_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<AddCommissionInvoiceDetail_list>(response);
                            foreach (AddCommissionInvoiceDetail item in res.AddCommissionInvoiceDetails)
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
                                AddCommissionInvoiceDetails.Add(item);
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

        public async Task<AddCommissionInvoiceDetail> StoreAddCommissionInvoiceDetailCommand(AddCommissionInvoiceDetail AddCommissionInvoiceDetail)
        {
            //Indexes AddCommissionInvoiceDetail = (Indexes)_AddCommissionInvoiceDetail;
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
            //                new KeyValuePair<string,string>("id",AddCommissionInvoiceDetail.id.ToString()),
            //                new KeyValuePair<string,string>("invoice_no",AddCommissionInvoiceDetail.invoice_no.ToString()),
            //                new KeyValuePair<string,string>("invoice_date",AddCommissionInvoiceDetail.invoice_date.ToString("yyyy/MM/dd")),
            //                new KeyValuePair<string,string>("ledger_type",AddCommissionInvoiceDetail.ledger_type.ToString()),
            //                new KeyValuePair<string,string>("ledger_id",AddCommissionInvoiceDetail.ledger_id.ToString()),
            //                new KeyValuePair<string,string>("company_id",AddCommissionInvoiceDetail.company_id.ToString()),
            //                new KeyValuePair<string,string>("commission_type",AddCommissionInvoiceDetail.commission_type.ToString()),
            //                new KeyValuePair<string,string>("commission_value",AddCommissionInvoiceDetail.commission_value.ToString()),                            
            //                new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),                                                
            //            });
                        
            //            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/_store", formcontent);

            //            //request.EnsureSuccessStatusCode(); 

            //            var response = await request.Content.ReadAsStringAsync();

            //            AddCommissionInvoiceDetail_Store_Result res = Newtonsoft.Json.JsonConvert.DeserializeObject<AddCommissionInvoiceDetail_Store_Result>(response);
            //            //res.AddCommissionInvoiceDetails.supplier_name = AddCommissionInvoiceDetail.supplier_name;
            //            //res.AddCommissionInvoiceDetails.customer_name = AddCommissionInvoiceDetail.customer_name;
            //            //res.AddCommissionInvoiceDetails.count_name = AddCommissionInvoiceDetail.count_name;
            //            //res.AddCommissionInvoiceDetails.count_name = AddCommissionInvoiceDetail.count_name;
            //            //res.AddCommissionInvoiceDetails.qty_unit = res.AddCommissionInvoiceDetails.qty.ToString() + " " + res.AddCommissionInvoiceDetails.unit;
            //            //res.AddCommissionInvoiceDetails.price_per = res.AddCommissionInvoiceDetails.price.ToString() + " " + res.AddCommissionInvoiceDetails.per; 
            //            //foreach (AddCommissionInvoiceDetail item in res.AddCommissionInvoiceDetails)
            //            //{
            //            AddCommissionInvoiceDetail = res.AddCommissionInvoiceDetails;                            
            //            //}

            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex);
            //}
             return AddCommissionInvoiceDetail;
        }

        internal async Task<string> rejectAddCommissionInvoiceDetail(int AddCommissionInvoiceDetailId)
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
                            new KeyValuePair<string,string>("id",AddCommissionInvoiceDetailId.ToString()),
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

        internal async Task<int>DuplicateConfirmationNo(AddCommissionInvoiceDetail AddCommissionInvoiceDetail)
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
                            new KeyValuePair<string,string>("id",AddCommissionInvoiceDetail.id.ToString()),
                            //new KeyValuePair<string,string>("confirmation_no",AddCommissionInvoiceDetail.confirmation_no.ToString()),                           
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

        public async Task<int> getAmendCount(int AddCommissionInvoiceDetailId)
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
                            new KeyValuePair<string,string>("id",AddCommissionInvoiceDetailId.ToString()),
                            new KeyValuePair<string,string>("getAmendmentCount","1"),
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_detail_list", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        AddCommissionInvoiceDetail_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<AddCommissionInvoiceDetail_list>(response);
                        count = res.AddCommissionInvoiceDetails.Count();
                        //res.AddCommissionInvoiceDetails.price_per = res.AddCommissionInvoiceDetails.price.ToString() + " " + res.AddCommissionInvoiceDetails.per;
                        ////foreach (AddCommissionInvoiceDetail item in res.AddCommissionInvoiceDetails)
                        ////{
                        //AddCommissionInvoiceDetail = res.AddCommissionInvoiceDetails;
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

        public async void SendForApproval(AddCommissionInvoiceDetail AddCommissionInvoiceDetail)
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
                            new KeyValuePair<string,string>("id",AddCommissionInvoiceDetail.id.ToString()),                            
                            new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_send_for_approval", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        //AddCommissionInvoiceDetail_Store_Result res = Newtonsoft.Json.JsonConvert.DeserializeObject<AddCommissionInvoiceDetail_Store_Result>(response);

                        //res.AddCommissionInvoiceDetails.price_per = res.AddCommissionInvoiceDetails.price.ToString() + " " + res.AddCommissionInvoiceDetails.per;
                        ////foreach (AddCommissionInvoiceDetail item in res.AddCommissionInvoiceDetails)
                        ////{
                        //AddCommissionInvoiceDetail = res.AddCommissionInvoiceDetails;
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