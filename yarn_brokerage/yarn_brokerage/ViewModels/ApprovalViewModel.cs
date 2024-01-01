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
    public class ApprovalViewModel : BaseViewModel
    {
        public ObservableCollection<Approval> Approvals { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command SearchItemsCommand { get; set; }
        public DateTime date { get; set; }
        private const int PageSize = 10;
        public SearchApprovalFilter SearchFilter { get; set; }
        //public SearchFilter SearchFilter { get; set; }
        // public Command StoreApprovalCommand { get; set; }

        public ApprovalViewModel(SearchApprovalFilter searchFilter = null)
        {
            Title = "Approval List";
            date = DateTime.Now.ToLocalTime();
            Approvals = new ObservableCollection<Approval>();
            if (searchFilter == null)
                LoadItemsCommand = new Command(async(object searchstring) => await ExecuteLoadItemsCommand(searchstring));
            else if (searchFilter != null)
                LoadItemsCommand = new Command(async (object searchFilters) => await ExecuteStoreItemsCommand(searchFilter));
                SearchItemsCommand = new Command(async (object searchFilters) => await ExecuteStoreItemsCommand(searchFilters));
            // StoreApprovalCommand = new Command(async (object Approval) => await ExecuteStoreApprovalCommand(Approval));
            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as Item;
            //    Approval.Add(newItem);
            //    await DataStore.AddItemAsync(newItem);
            //});
        }
        private string _SearchString = "";
        private int totalRows = 0;

        async Task ExecuteLoadItemsCommand(object searchstring)
        {
            if (IsBusy)
                return;            
            IsBusy = true;

            string _SearchString = "";
            if (searchstring != null)
                _SearchString = (string)searchstring;

            try
            {
                Approvals.Clear();
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
                                    new KeyValuePair<string,string>("search_string",_SearchString),
                                    new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                    new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                    //new KeyValuePair<string,string>("confirmation_date",_confirmation_date.ToString("yyyy-MM-dd"))
                                });
                            

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/approval_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            Approval_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<Approval_list>(response);
                            foreach (Approval item in res.Approvals)
                            {
                                foreach (DraftConfirmationDetails draftConfirmationDetails in item.dispatch_details)
                                {
                                    if (draftConfirmationDetails.cancel_dispatch == 1)
                                    {
                                        draftConfirmationDetails.TextColor = "Red";
                                        draftConfirmationDetails.dispatch_status_visible = 1;
                                        draftConfirmationDetails.dispatch_status = "CANCELLED";
                                    }
                                    else if (draftConfirmationDetails.program_approval == 1 && draftConfirmationDetails.program_approved == 0 && draftConfirmationDetails.dispatched == 0)
                                    {
                                        draftConfirmationDetails.TextColor = "Green";
                                        draftConfirmationDetails.dispatch_status_visible = 1;
                                        draftConfirmationDetails.dispatch_status = "APPROVAL";
                                    }
                                    else if (draftConfirmationDetails.program_approval == 1 && draftConfirmationDetails.program_approved == 1 && draftConfirmationDetails.dispatched == 0)
                                    {
                                        draftConfirmationDetails.TextColor = "Green";
                                        draftConfirmationDetails.dispatch_status_visible = 1;
                                        draftConfirmationDetails.dispatch_status = "CURRENT";
                                    }
                                    else if (draftConfirmationDetails.program_approval == 1 && draftConfirmationDetails.program_approved == 1 && draftConfirmationDetails.dispatched == 1 && draftConfirmationDetails.delivered == 1 && draftConfirmationDetails.invoice_details == 1 && draftConfirmationDetails.customer_acknowledgement == 1)
                                    {
                                        draftConfirmationDetails.TextColor = "Green";
                                        draftConfirmationDetails.dispatch_status_visible = 1;
                                        draftConfirmationDetails.dispatch_status = "CLOSED";
                                    }
                                    else if (draftConfirmationDetails.program_approval == 1 && draftConfirmationDetails.program_approved == 1 && draftConfirmationDetails.dispatched == 1)
                                    {
                                        draftConfirmationDetails.TextColor = "Green";
                                        draftConfirmationDetails.dispatch_status_visible = 1;
                                        draftConfirmationDetails.dispatch_status = "DISPATCHED";
                                    }
                                }
                                if (Application.Current.Properties["user_type"].ToString() == "1")
                                    item.admin_user = true;
                                if (item.price != Convert.ToInt32(item.price))
                                    item.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.price)));
                                else
                                    item.price = Convert.ToInt32(item.price);
                                item.price_per = item.price.ToString() + " " + item.per;
                                item.confirm_customer = item.confirm_customer_bags + " BAGS / " + item.confirm_customer_fcl + " FCL";
                                item.confirm_supplier = item.confirm_supplier_bags + " BAGS / " + item.confirm_supplier_fcl + " FCL";
                                item.pending_customer = item.pending_customer_bags + " BAGS / " + item.pending_customer_fcl + " FCL";
                                item.pending_supplier = item.pending_supplier_bags + " BAGS / " + item.pending_supplier_fcl + " FCL";
                                double diff2 = (DateTime.Today.Date - item.transaction_date_time).TotalDays;
                                if (diff2 > 0)
                                    item.transaction_detail = item.transaction_detail + " - " + (int)Math.Ceiling(diff2) + " Days Old";


                                item.TransactionDetails = item.transaction_date_time.ToString("dd-MM-yyyy") + " ( " + item.ConfirmationNo + " ) " + " - " + (int)Math.Ceiling(diff2) + " Days Old";



                                Approvals.Add(item);
                            }
                            totalRows = res.totalRows;
                            if (res.totalRows > 0)
                                No_Of_Count_With_Bag_Weight = "" + res.totalRows + " Nos. ( " + res.approval_value + "  )";
                            else
                                No_Of_Count_With_Bag_Weight = "No Records.";
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

            SearchFilter = new SearchApprovalFilter();

            SearchFilter = (SearchApprovalFilter)searchFilter;

            IsBusy = true;

            try
            {
                Approvals.Clear();
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
                                new KeyValuePair<string,string>("ledgers_id",SearchFilter.ledger_id.ToString()),
                                new KeyValuePair<string,string>("exact_ledger_id",SearchFilter.exact_ledger_id.ToString()),
                                new KeyValuePair<string,string>("supplier_id",SearchFilter.supplier_id.ToString()),
                                new KeyValuePair<string,string>("customer_id",SearchFilter.customer_id.ToString()),
                                new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                new KeyValuePair<string,string>("s_user_id",SearchFilter.user_id.ToString()),
                                new KeyValuePair<string,string>("confirmation_date_from", SearchFilter.confirmation_date_from.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string,string>("confirmation_date_to", SearchFilter.confirmation_date_to.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string,string>("approved_date_from", SearchFilter.approved_date_from.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string,string>("approved_date_to", SearchFilter.approved_date_to.ToString("yyyy-MM-dd")),                                
                                new KeyValuePair<string,string>("confirmation_date",SearchFilter.confirmation_date.ToString()),
                                new KeyValuePair<string,string>("approved_date",SearchFilter.approved_date.ToString()),
                                new KeyValuePair<string,string>("search_string",SearchFilter.search_string.ToString()),
                                new KeyValuePair<string,string>("CurrentPageNumber","0"),
                                new KeyValuePair<string,string>("PageSize",PageSize.ToString())
                            });

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/approval_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            Approval_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<Approval_list>(response);
                            foreach (Approval item in res.Approvals)
                            {
                                foreach( DraftConfirmationDetails draftConfirmationDetails in item.dispatch_details)
                                {
                                    if (draftConfirmationDetails.cancel_dispatch == 1)
                                    {
                                        draftConfirmationDetails.TextColor = "Red";
                                        draftConfirmationDetails.dispatch_status_visible = 1;
                                        draftConfirmationDetails.dispatch_status = "CANCELLED";
                                    }
                                    else if (draftConfirmationDetails.program_approval == 1 && draftConfirmationDetails.program_approved == 0 && draftConfirmationDetails.dispatched == 0)
                                    {
                                        draftConfirmationDetails.TextColor = "Green";
                                        draftConfirmationDetails.dispatch_status_visible = 1;
                                        draftConfirmationDetails.dispatch_status = "APPROVAL";
                                    }
                                    else if (draftConfirmationDetails.program_approval == 1 && draftConfirmationDetails.program_approved == 1 && draftConfirmationDetails.dispatched == 0)
                                    {
                                        draftConfirmationDetails.TextColor = "Green";
                                        draftConfirmationDetails.dispatch_status_visible = 1;
                                        draftConfirmationDetails.dispatch_status = "CURRENT";
                                    }
                                    else if (draftConfirmationDetails.program_approval == 1 && draftConfirmationDetails.program_approved == 1 && draftConfirmationDetails.dispatched == 1 && draftConfirmationDetails.delivered == 1 && draftConfirmationDetails.invoice_details == 1 && draftConfirmationDetails.customer_acknowledgement == 1)
                                    {
                                        draftConfirmationDetails.TextColor = "Green";
                                        draftConfirmationDetails.dispatch_status_visible = 1;
                                        draftConfirmationDetails.dispatch_status = "CLOSED";
                                    }
                                    else if (draftConfirmationDetails.program_approval == 1 && draftConfirmationDetails.program_approved == 1 && draftConfirmationDetails.dispatched == 1)
                                    {
                                        draftConfirmationDetails.TextColor = "Green";
                                        draftConfirmationDetails.dispatch_status_visible = 1;
                                        draftConfirmationDetails.dispatch_status = "DISPATCHED";
                                    }
                                }
                                if (Application.Current.Properties["user_type"].ToString() == "1")
                                    item.admin_user = true;
                                if (item.price != Convert.ToInt32(item.price))
                                    item.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.price)));
                                else
                                    item.price = Convert.ToInt32(item.price);
                                item.price_per = item.price.ToString() + " " + item.per;
                                item.confirm_customer = item.confirm_customer_bags + " BAGS / " + item.confirm_customer_fcl + " FCL";
                                item.confirm_supplier = item.confirm_supplier_bags + " BAGS / " + item.confirm_supplier_fcl + " FCL";
                                item.pending_customer = item.pending_customer_bags + " BAGS / " + item.pending_customer_fcl + " FCL";
                                item.pending_supplier = item.pending_supplier_bags + " BAGS / " + item.pending_supplier_fcl + " FCL";
                                double diff2 = (DateTime.Today.Date - item.transaction_date_time).TotalDays;
                                if (diff2 > 0)
                                    //item.transaction_detail = item.transaction_detail + " - " + (int)Math.Ceiling(diff2) + " Days Old";
                                item.TransactionDetails = item.transaction_date_time.ToString("dd-MM-yyyy") + " ( " + item.ConfirmationNo + " ) " + " - " + (int)Math.Ceiling(diff2) + " Days Old";
                                Approvals.Add(item);
                            }
                            totalRows = res.totalRows;
                            if (res.totalRows > 0)
                                No_Of_Count_With_Bag_Weight = "" + res.totalRows + " Nos. ( " + res.approval_value + "  )";
                            else
                                No_Of_Count_With_Bag_Weight = "No Records.";
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

        public async Task<Approval> GetApprovalAsync(int id)
        {
            Approval item = new Approval();

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


                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/approval_list", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        Approval_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<Approval_list>(response);
                        item = res.Approvals[0];
                        if (Application.Current.Properties["user_type"].ToString() == "1")
                            item.admin_user = true;
                        if (item.price != Convert.ToInt32(item.price))
                            item.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.price)));
                        else
                            item.price = Convert.ToInt32(item.price);
                        item.price_per = item.price.ToString() + " " + item.per;
                        item.confirm_customer = item.confirm_customer_bags + " BAGS / " + item.confirm_customer_fcl + " FCL";
                        item.confirm_supplier = item.confirm_supplier_bags + " BAGS / " + item.confirm_supplier_fcl + " FCL";
                        item.pending_customer = item.pending_customer_bags + " BAGS / " + item.pending_customer_fcl + " FCL";
                        item.pending_supplier = item.pending_supplier_bags + " BAGS / " + item.pending_supplier_fcl + " FCL";

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return item;
        }
        public async Task<Approval> StoreApprovalCommand(Approval Approval)
        {
            //Indexes Approval = (Indexes)_Approval;
            Approval item = new Approval();
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
                            new KeyValuePair<string,string>("id",Approval.Id.ToString()),
                            new KeyValuePair<string,string>("transaction_date_time",DateTime.Now.ToLocalTime().ToString("yyyy/MM/ddTHH:mm")),
                            new KeyValuePair<string,string>("remarks",(Approval.approved_remarks != null) ? Approval.approved_remarks.ToString():""),
                            new KeyValuePair<string,string>("status",Approval.status.ToString()),                                            
                            new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                            new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                        });
                        
                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_approval", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        Approval_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<Approval_list>(response);
                        item = res.Approvals[0];
                        if (Application.Current.Properties["user_type"].ToString() == "1")
                            item.admin_user = true;
                        if (item.price != Convert.ToInt32(item.price))
                            item.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.price)));
                        else
                            item.price = Convert.ToInt32(item.price);
                        item.price_per = item.price.ToString() + " " + item.per;
                        item.confirm_customer = item.confirm_customer_bags + " BAGS / " + item.confirm_customer_fcl + " FCL";
                        item.confirm_supplier = item.confirm_supplier_bags + " BAGS / " + item.confirm_supplier_fcl + " FCL";
                        item.pending_customer = item.pending_customer_bags + " BAGS / " + item.pending_customer_fcl + " FCL";
                        item.pending_supplier = item.pending_supplier_bags + " BAGS / " + item.pending_supplier_fcl + " FCL";

                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
             return item;
        }

        public string BAGQTY { get; set; }
        public string FCLQTY { get; set; }

        string QTY;
        public void RemoveApprovalCommand(Approval Approval)
        {

            var index_value = Approvals.IndexOf(Approvals.Where(x => x.Id == Approval.Id).FirstOrDefault());

            Approvals.RemoveAt(index_value);

            for (int i = 0; i < Approvals.Count(); i++)
            {
                if (Approvals[i].unit == "BAGS")
                {
                    for (int bags = 0; bags < Approvals.Count(); bags++)
                    {
                        BAGQTY = Approvals.Where(x => x.unit == "BAGS").Sum(x => x.qty).ToString() + " BAGS ";
                    }
                }
                if (Approvals[i].unit == "FCL")
                {

                    for (int fcl = 0; fcl < Approvals.Count(); fcl++)
                    {
                        FCLQTY = Approvals.Where(x => x.unit == "FCL").Sum(x => x.qty).ToString() + " FCL ";
                    }
                }


            }
            QTY = BAGQTY + FCLQTY;



            if (Approvals.Count() > 0)
                No_Of_Count_With_Bag_Weight = Approvals.Count() + " No's. (" + QTY + ")";
            else
                No_Of_Count_With_Bag_Weight = "No Records.";



        }
    }
}