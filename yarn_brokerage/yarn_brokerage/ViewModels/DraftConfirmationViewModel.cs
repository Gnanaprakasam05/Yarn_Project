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
    public class DraftConfirmationViewModel : BaseViewModel
    {
        public InfiniteScrollCollection<DraftConfirmation> DraftConfirmations { get; set; }
        public ObservableCollection<MessageGroup> MessageGroup { get; set; }

        public DraftConfirmation DraftConfirmation { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command SearchItemsCommand { get; set; }
        public DateTime date { get; set; }
        private const int PageSize = 10;
        private string user_team_group_id;
        public SearchConfirmationFilter SearchFilter { get; set; }
        // public Command StoreDraftConfirmationCommand { get; set; }

        public DraftConfirmationViewModel(SearchConfirmationFilter searchFilter = null, string _Title = "Confirmation")
        {
            Title = _Title;
            date = DateTime.Now.ToLocalTime();
            MessageGroup = new ObservableCollection<MessageGroup>();
            DraftConfirmations = new InfiniteScrollCollection<DraftConfirmation>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = DraftConfirmations.Count / PageSize;

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
                                new KeyValuePair<string,string>("search_string",_SearchString.Search_string),
                                new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                new KeyValuePair<string,string>("CurrentPageNumber",page.ToString()),
                                new KeyValuePair<string,string>("PageSize",PageSize.ToString()),
                                new KeyValuePair<string, string>("team_group_id", user_team_group_id)
                            });
                        }
                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_list", formcontent);

                        var response = await request.Content.ReadAsStringAsync();

                        DraftConfirmation_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<DraftConfirmation_list>(response);
                        DraftConfirmation_list draftConfirmation_List = new DraftConfirmation_list();
                        draftConfirmation_List.totalRows = res.totalRows;
                        draftConfirmation_List.draftConfirmations = new List<DraftConfirmation>();
                        foreach (DraftConfirmation item in res.draftConfirmations)
                        {
                            if (Application.Current.Properties["user_type"].ToString() == "1")
                                item.admin_user = true;
                            if (item.price != Convert.ToInt32(item.price))
                                item.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.price)));
                            else
                                item.price = Convert.ToInt32(item.price);
                            item.price_per = item.price.ToString() + " " + item.per;
                            double diff2 = (DateTime.Today.Date.ToLocalTime() - item.transaction_date_time).TotalDays;
                            if (diff2 > 0)
                                item.transaction_detail = item.transaction_detail + " - " + (int)Math.Ceiling(diff2) + " Days Old";

                            item.TransactionDetails = item.transaction_date_time.ToString("dd-MM-yyyy") + " ( " + item.confirmation_no + " ) " + " - " + item.DelayDays + " Days Old";

                            draftConfirmation_List.draftConfirmations.Add(item);
                        }
                        IsBusy = false;
                        return draftConfirmation_List.draftConfirmations;
                    }
                    // return the items that need to be added

                },
                OnCanLoadMore = () =>
                {
                    return DraftConfirmations.Count < totalRows;
                }
            };

            if (searchFilter == null)
                LoadItemsCommand = new Command(async (object searchstring) => await ExecuteLoadItemsCommand(searchstring)); //object confirmation_date confirmation_date
            else if (searchFilter != null)
                LoadItemsCommand = new Command(async (object searchFilters) => await ExecuteStoreItemsCommand(searchFilter));
            SearchItemsCommand = new Command(async (object searchFilters) => await ExecuteStoreItemsCommand(searchFilters));
            // StoreDraftConfirmationCommand = new Command(async (object draftConfirmation) => await ExecuteStoreDraftConfirmationCommand(draftConfirmation));
            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as Item;
            //    DraftConfirmation.Add(newItem);
            //    await DataStore.AddItemAsync(newItem);
            //});
        }

        search_string _SearchString;
        private int totalRows = 0;
        private int Check_TeamGroup { get; set; }
        async Task ExecuteLoadItemsCommand(object searchstring) //object confirmation_date
        {
            if (IsBusy)
                return;
            //DateTime _confirmation_date;
            _SearchString = searchstring as search_string;
            //if (searchstring != null)
            //    _SearchString = (string)searchstring;


            if (Application.Current.Properties["mobile_dashboard_show_team_group_data"].ToString() == "1" && Application.Current.Properties["team_group_id"] != null)
            {
                user_team_group_id = Application.Current.Properties["team_group_id"].ToString();
            }
            else if (Application.Current.Properties["mobile_dashboard_show_team_group_data"].ToString() == "1" && Application.Current.Properties["team_group_id"] == null)
            {
                user_team_group_id = "";

                if (Application.Current.Properties["user_type"].ToString() == "2")
                {
                    Check_TeamGroup = 1;
                }

            }
            else if (Application.Current.Properties["mobile_dashboard_show_team_group_data"].ToString() == "0")
            {
                user_team_group_id = "";

            }

            IsBusy = true;

            try
            {
                DraftConfirmations.Clear();
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
                                new KeyValuePair<string,string>("search_string",_SearchString.Search_string),
                                new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                new KeyValuePair<string,string>("CurrentPageNumber","0"),
                                new KeyValuePair<string,string>("PageSize",PageSize.ToString()),
                                new KeyValuePair<string, string>("team_group_id", user_team_group_id)
                            });

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            DraftConfirmation_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<DraftConfirmation_list>(response);
                            foreach (DraftConfirmation item in res.draftConfirmations)
                            {
                                if (Application.Current.Properties["user_type"].ToString() == "1")
                                    item.admin_user = true;
                                if (item.price != Convert.ToInt32(item.price))
                                    item.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.price)));
                                else
                                    item.price = Convert.ToInt32(item.price);
                                item.price_per = item.price.ToString() + " " + item.per;
                                if (item.status == 1)
                                    item.status_image = "approved.png";
                                if (item.status == 5)
                                    item.status_image = "rejected.png";
                                double diff2 = (DateTime.Today.Date - item.transaction_date_time).TotalDays;
                                if (diff2 > 0)
                                    item.transaction_detail = item.transaction_detail + " - " + (int)Math.Ceiling(diff2) + " Days Old";

                                string delayDay = item.DelayDays;


                                item.TransactionDetails = item.transaction_date_time.ToString("dd-MM-yyyy") + " ( " + item.confirmation_no + " ) " + " - " + item.DelayDays + " Days Old";
                                //item.TransactionDetails = item.transaction_date_time.ToString(dd-MM-YYYY);
                                //if(delayDay != "")
                                //  item.TransactionDetails = item.TransactionDateTime1 + " ( " + item.confirmation_no + " ) " + " - " + item.DelayDays + " Days Old";
                                //else
                                //    item.TransactionDetails = item.TransactionDateTime1 + " ( " + item.confirmation_no + " ) " + " - " + "0 Days Old";

                                DraftConfirmations.Add(item);
                            }
                            //DraftConfirmations.AddRange(res.draftConfirmations);
                            totalRows = res.totalRows;
                            No_Of_Count_With_Bag_Weight = "";
                            if (res.totalRows > 0)
                                No_Of_Count_With_Bag_Weight = "" + res.totalRows + " Nos. ( " + res.confirm_value + "  )";
                            else
                                No_Of_Count_With_Bag_Weight = "No Records.";

                            if (Check_TeamGroup == 1)
                            {
                                DraftConfirmations.Clear();
                                No_Of_Count_With_Bag_Weight = "";
                                Check_TeamGroup = 0;
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
                DraftConfirmations.Clear();
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

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_list", formcontent);
                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            DraftConfirmation_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<DraftConfirmation_list>(response);
                            foreach (DraftConfirmation item in res.draftConfirmations)
                            {
                                if (Application.Current.Properties["user_type"].ToString() == "1")
                                    item.admin_user = true;
                                if (item.price != Convert.ToInt32(item.price))
                                    item.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.price)));
                                else
                                    item.price = Convert.ToInt32(item.price);
                                item.price_per = item.price.ToString() + " " + item.per;
                                double diff2 = (DateTime.Today.Date - item.transaction_date_time).TotalDays;
                                if (diff2 > 0)
                                    item.transaction_detail = item.transaction_detail + " - " + (int)Math.Ceiling(diff2) + " Days Old";

                                item.TransactionDetails = item.transaction_date_time.ToString("dd-MM-yyyy") + " ( " + item.confirmation_no + " ) " + " - " + item.DelayDays + " Days Old";

                                DraftConfirmations.Add(item);
                            }
                            totalRows = res.totalRows;

                            if (res.totalRows > 0)
                                No_Of_Count_With_Bag_Weight = "" + res.totalRows + " Nos. ( " + res.confirm_value + "  )";
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

        public async Task<DraftConfirmation> StoreDraftConfirmationCommand(DraftConfirmation draftConfirmation)
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
                            new KeyValuePair<string,string>("id",draftConfirmation.id.ToString()),
                            new KeyValuePair<string,string>("transaction_date_time",draftConfirmation.transaction_date_time.ToString("yyyy/MM/dd")),
                            new KeyValuePair<string,string>("segment",draftConfirmation.segment.ToString()),
                            new KeyValuePair<string,string>("customer_id",draftConfirmation.customer_id.ToString()),
                            new KeyValuePair<string,string>("supplier_id",draftConfirmation.supplier_id.ToString()),
                            new KeyValuePair<string,string>("count_id",draftConfirmation.count_id.ToString()),
                            new KeyValuePair<string,string>("bag_weight",draftConfirmation.bag_weight.ToString()),
                            new KeyValuePair<string,string>("qty",draftConfirmation.qty.ToString()),
                            new KeyValuePair<string,string>("unit",draftConfirmation.unit.ToString()),
                            new KeyValuePair<string,string>("price",draftConfirmation.price.ToString()),
                            new KeyValuePair<string,string>("per",draftConfirmation.per.ToString()),
                            new KeyValuePair<string,string>("confirmation_no",draftConfirmation.confirmation_no),
                            new KeyValuePair<string,string>("confirmed_remarks",draftConfirmation.confirmedRemarks.ToString()),
                            new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),


                            //new KeyValuePair<string,string>("dispatch_from_date",draftConfirmation.dispatch_from_date.ToString("yyyy/MM/dd")),
                            //new KeyValuePair<string,string>("dispatch_to_date",draftConfirmation.dispatch_to_date.ToString("yyyy/MM/dd")),
                            //new KeyValuePair<string,string>("payment_date",draftConfirmation.payment_date.ToString("yyyy/MM/dd")),
                            
                            //new KeyValuePair<string,string>("enquiry_ids",draftConfirmation.enquiry_ids.ToString()),                            
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_store", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        DraftConfirmation_Store_Result res = Newtonsoft.Json.JsonConvert.DeserializeObject<DraftConfirmation_Store_Result>(response);
                        res.draftConfirmations.SupplierWhatsappGroup_Ckeck = res.SupplierWhatsappGroup;
                        res.draftConfirmations.StoreMessage = res.message;
                        res.draftConfirmations.confirmedRemarks = draftConfirmation.confirmedRemarks;
                        res.draftConfirmations.Flag_Check = 1;
                        res.draftConfirmations.supplier_name = draftConfirmation.supplier_name;
                        res.draftConfirmations.customer_name = draftConfirmation.customer_name;
                        res.draftConfirmations.count_name = draftConfirmation.count_name;
                        res.draftConfirmations.price = draftConfirmation.price;
                        res.draftConfirmations.supplier_id = draftConfirmation.supplier_id;
                        res.draftConfirmations.customer_id = draftConfirmation.customer_id;
                        res.draftConfirmations.count_id = draftConfirmation.count_id;
                        res.draftConfirmations.bag_weight = draftConfirmation.bag_weight;
                        res.draftConfirmations.qty_unit = res.draftConfirmations.qty.ToString() + " " + res.draftConfirmations.unit;
                        res.draftConfirmations.price_per = res.draftConfirmations.price.ToString() + " " + res.draftConfirmations.per;
                        draftConfirmation = res.draftConfirmations;


                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return draftConfirmation;
        }
        public async Task<ObservableCollection<MessageGroup>> MessageGroupDraftConfirmationCommand()
        {
            try
            {
                MessageGroup.Clear();
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    using (var cl = new HttpClient())
                    {
                        HttpContent formcontent = null;

                        formcontent = new FormUrlEncodedContent(new[]
                        {
                            new KeyValuePair<string,string>("supplier_whatsapp_group",""),

                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/message_group_get", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        ObservableCollection<MessageGroup> res = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<MessageGroup>>(response);

                        foreach (MessageGroup item in res)
                        {
                            MessageGroup.Add(item);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return MessageGroup;
        }
        bool check;
        public async Task<bool> WhatsappConfirmationDraftConfirmationCommand(DraftConfirmation draftConfirmation, MessageGroup MessageGroup_List)
        {

            DraftConfirmation = draftConfirmation;

            string customer_id = Convert.ToString(DraftConfirmation.customer_id);
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
                            new KeyValuePair<string,string>("id",DraftConfirmation.id.ToString()),
                            new KeyValuePair<string, string>("supplier_name",DraftConfirmation.supplier_name),
                            new KeyValuePair<string, string>("customer_name",DraftConfirmation.customer_name),
                            new KeyValuePair<string, string>("customer_id",customer_id),
                            new KeyValuePair<string, string>("group_whatsapp_id",MessageGroup_List.TeamGroupId.ToString()),
                            new KeyValuePair<string, string>("qty_unit",DraftConfirmation.qty_unit.ToString()),
                            new KeyValuePair<string, string>("price_per",DraftConfirmation.price_per.ToString()),
                            new KeyValuePair<string, string>("count_name",DraftConfirmation.count_name.ToString()),
                            new KeyValuePair<string,string>("confirmed_remarks",DraftConfirmation.confirmedRemarks.ToString()),
                            new KeyValuePair<string,string>("send_status",Application.Current.Properties["send_status"].ToString()),
                            new KeyValuePair<string, string>("user_id",Application.Current.Properties["user_id"].ToString()),

                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/whatsapp_send_confirmation_from_app", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();
                        int res = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(response);

                        if (res == 1)
                        {
                            check = true;
                        }
                        else
                        {
                            check = false;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return check;
        }



        internal async Task<string> rejectDraftConfirmation(int DraftConfirmationId)
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

        internal async Task<int> DuplicateConfirmationNo(DraftConfirmation DraftConfirmation)
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
                            new KeyValuePair<string,string>("id",DraftConfirmation.id.ToString()),
                            new KeyValuePair<string,string>("confirmation_no",DraftConfirmation.confirmation_no.ToString()),
                            new KeyValuePair<string,string>("transaction_date_time",DraftConfirmation.transaction_date_time.ToString("yyyy/MM/dd")),
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

        public async Task<int> getAmendCount(int DraftConfirmationId)
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
                            new KeyValuePair<string,string>("id",DraftConfirmationId.ToString()),
                            new KeyValuePair<string,string>("getAmendmentCount","1"),
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_detail_list", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        DraftConfirmation_Detail_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<DraftConfirmation_Detail_list>(response);
                        count = res.draftConfirmationDetails.Count();
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
            return count;
        }

        public async void SendForApproval(DraftConfirmation DraftConfirmation)
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
                            new KeyValuePair<string,string>("id",DraftConfirmation.id.ToString()),
                            new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                        });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_send_for_approval", formcontent);

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
        public void AddStoreCommand(DraftConfirmation draftConfirmation)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {

                DraftConfirmations.Add(new DraftConfirmation
                {
                    id = draftConfirmation.id,
                    transaction_date_time = draftConfirmation.transaction_date_time,
                    segment = draftConfirmation.segment,
                    confirmation_no = draftConfirmation.confirmation_no,
                    customer_id = draftConfirmation.customer_id,
                    customer_name = draftConfirmation.customer_name,
                    supplier_id = draftConfirmation.supplier_id,
                    supplier_name = draftConfirmation.supplier_name,
                    count_id = draftConfirmation.count_id,
                    count_name = draftConfirmation.count_name,
                    bag_weight = draftConfirmation.bag_weight,
                    qty = draftConfirmation.qty,
                    unit = draftConfirmation.unit,
                    qty_unit = draftConfirmation.qty_unit,
                    price = draftConfirmation.price,
                    per = draftConfirmation.per,
                    price_per = draftConfirmation.price_per,
                    user_name = draftConfirmation.user_name,
                    description = draftConfirmation.description,
                    admin_user = draftConfirmation.admin_user,
                    enquiry_ids = draftConfirmation.enquiry_ids,
                    status = draftConfirmation.status,
                    send_for_approval = draftConfirmation.send_for_approval,
                    payment_status = draftConfirmation.payment_status,
                    dispatch_confirm_status = draftConfirmation.dispatch_confirm_status,
                    dispatch_status = draftConfirmation.dispatch_status,
                    delivery_status = draftConfirmation.delivery_status,
                    dispatch_payment_confirmation = draftConfirmation.dispatch_payment_confirmation,
                    status_image = draftConfirmation.status_image,
                    confirmed_user = draftConfirmation.confirmed_user,
                    approved_user = draftConfirmation.approved_user,
                    rejected_user = draftConfirmation.rejected_user,
                    payment_user = draftConfirmation.payment_user,
                    dispatch_confirm_user = draftConfirmation.dispatch_confirm_user,
                    dispatch_user = draftConfirmation.dispatch_user,
                    delivery_user = draftConfirmation.delivery_user,
                    approved_user_id = draftConfirmation.approved_user_id,
                    rejected_user_id = draftConfirmation.rejected_user_id,
                    payment_user_id = draftConfirmation.payment_user_id,
                    dispatch_confirm_user_id = draftConfirmation.dispatch_confirm_user_id,
                    dispatch_user_id = draftConfirmation.dispatch_user_id,
                    delivery_user_id = draftConfirmation.delivery_user_id,
                    transaction_detail = draftConfirmation.transaction_detail,
                    item_balance_value = draftConfirmation.item_balance_value,
                    TransactionDetails = draftConfirmation.TransactionDetails,
                    CustomerSmsNo = draftConfirmation.CustomerSmsNo,
                    CustomerEmail = draftConfirmation.CustomerEmail,
                    SupplierEmail = draftConfirmation.SupplierEmail,
                    SupplierSmsNo = draftConfirmation.SupplierSmsNo,
                    SupplierGst = draftConfirmation.SupplierGst,
                    SetActive = draftConfirmation.SetActive,
                    DelayDays = draftConfirmation.DelayDays,
                    TransactionDateTime1 = draftConfirmation.TransactionDateTime1,
                    confirmedRemarks = draftConfirmation.confirmedRemarks,
                    StoreMessage = draftConfirmation.StoreMessage,
                    TotalQty = draftConfirmation.TotalQty,
                    Add_Flag = draftConfirmation.Add_Flag,
                    Cancel_Click = draftConfirmation.Cancel_Click,
                    Edit_Flag = draftConfirmation.Edit_Flag,

                });


                No_Of_Count_With_Bag_Weight = "" + DraftConfirmations.Count() + " Nos.";

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

        public void EditStoreCommand(DraftConfirmation draftConfirmation)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var index_value = DraftConfirmations.IndexOf(DraftConfirmations.Where(x => x.id == draftConfirmation.id).FirstOrDefault());

                DraftConfirmations[index_value] = new DraftConfirmation()
                {
                    id = draftConfirmation.id,
                    transaction_date_time = draftConfirmation.transaction_date_time,
                    segment = draftConfirmation.segment,
                    confirmation_no = draftConfirmation.confirmation_no,
                    customer_id = draftConfirmation.customer_id,
                    customer_name = draftConfirmation.customer_name,
                    supplier_id = draftConfirmation.supplier_id,
                    supplier_name = draftConfirmation.supplier_name,
                    count_id = draftConfirmation.count_id,
                    count_name = draftConfirmation.count_name,
                    bag_weight = draftConfirmation.bag_weight,
                    qty = draftConfirmation.qty,
                    unit = draftConfirmation.unit,
                    qty_unit = draftConfirmation.qty_unit,
                    price = draftConfirmation.price,
                    per = draftConfirmation.per,
                    price_per = draftConfirmation.price_per,
                    user_name = draftConfirmation.user_name,
                    description = draftConfirmation.description,
                    admin_user = draftConfirmation.admin_user,
                    enquiry_ids = draftConfirmation.enquiry_ids,
                    status = draftConfirmation.status,
                    send_for_approval = draftConfirmation.send_for_approval,
                    payment_status = draftConfirmation.payment_status,
                    dispatch_confirm_status = draftConfirmation.dispatch_confirm_status,
                    dispatch_status = draftConfirmation.dispatch_status,
                    delivery_status = draftConfirmation.delivery_status,
                    dispatch_payment_confirmation = draftConfirmation.dispatch_payment_confirmation,
                    status_image = draftConfirmation.status_image,
                    confirmed_user = draftConfirmation.confirmed_user,
                    approved_user = draftConfirmation.approved_user,
                    rejected_user = draftConfirmation.rejected_user,
                    payment_user = draftConfirmation.payment_user,
                    dispatch_confirm_user = draftConfirmation.dispatch_confirm_user,
                    dispatch_user = draftConfirmation.dispatch_user,
                    delivery_user = draftConfirmation.delivery_user,
                    approved_user_id = draftConfirmation.approved_user_id,
                    rejected_user_id = draftConfirmation.rejected_user_id,
                    payment_user_id = draftConfirmation.payment_user_id,
                    dispatch_confirm_user_id = draftConfirmation.dispatch_confirm_user_id,
                    dispatch_user_id = draftConfirmation.dispatch_user_id,
                    delivery_user_id = draftConfirmation.delivery_user_id,
                    transaction_detail = draftConfirmation.transaction_detail,
                    item_balance_value = draftConfirmation.item_balance_value,
                    TransactionDetails = draftConfirmation.TransactionDetails,
                    CustomerSmsNo = draftConfirmation.CustomerSmsNo,
                    CustomerEmail = draftConfirmation.CustomerEmail,
                    SupplierEmail = draftConfirmation.SupplierEmail,
                    SupplierSmsNo = draftConfirmation.SupplierSmsNo,
                    SupplierGst = draftConfirmation.SupplierGst,
                    SetActive = draftConfirmation.SetActive,
                    DelayDays = draftConfirmation.DelayDays,
                    TransactionDateTime1 = draftConfirmation.TransactionDateTime1,
                    confirmedRemarks = draftConfirmation.confirmedRemarks,
                    StoreMessage = draftConfirmation.StoreMessage,
                    TotalQty = draftConfirmation.TotalQty,
                    Add_Flag = draftConfirmation.Add_Flag,
                    Cancel_Click = draftConfirmation.Cancel_Click,
                    Edit_Flag = draftConfirmation.Edit_Flag,

                };
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


    }

}