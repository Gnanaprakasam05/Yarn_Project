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
namespace yarn_brokerage.ViewModels
{
    public class ConfirmationViewModel : BaseViewModel
    {
        public InfiniteScrollCollection<DraftConfirmation> DraftConfirmations { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command SearchItemsCommand { get; set; }
        public DateTime date { get; set; }
        private const int PageSize = 20;
        public SearchConfirmationFilter SearchFilter { get; set; }
        // public Command StoreDraftConfirmationCommand { get; set; }

        public ConfirmationViewModel(SearchConfirmationFilter searchFilter = null, string _Title="Confirmation")
        {
            Title = _Title;
            date = DateTime.Now.ToLocalTime();
            //DraftConfirmations = new ObservableCollection<DraftConfirmation>();
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
                                new KeyValuePair<string, string>("approved","1"),
                                new KeyValuePair<string,string>("search_string",SearchFilter.search_string.ToString()),
                                new KeyValuePair<string, string>("CurrentPageNumber", page.ToString()),
                                new KeyValuePair<string, string>("PageSize", PageSize.ToString())
                            });
                        }
                        else
                        {
                            formcontent = new FormUrlEncodedContent(new[]
                            {
                                new KeyValuePair<string,string>("search_string",_SearchString),
                                new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                new KeyValuePair<string,string>("CurrentPageNumber",page.ToString()),
                                new KeyValuePair<string,string>("PageSize",PageSize.ToString()),
                                new KeyValuePair<string, string>("approved","1"),
                            });
                        }
                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_list", formcontent);

                        //request.EnsureSuccessStatusCode(); 

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

        string _SearchString;
        private int totalRows = 0;

        async Task ExecuteLoadItemsCommand(object searchstring) //object confirmation_date
        {
            if (IsBusy)
                return;
            //DateTime _confirmation_date;
            _SearchString = "";
            if (searchstring != null)
                _SearchString = (string)searchstring;
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
                                new KeyValuePair<string,string>("search_string",_SearchString),
                                new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                new KeyValuePair<string,string>("CurrentPageNumber","0"),
                                new KeyValuePair<string,string>("PageSize",PageSize.ToString()),
                                new KeyValuePair<string, string>("approved","1"),
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
                                DraftConfirmations.Add(item);
                            }
                            //DraftConfirmations.AddRange(res.draftConfirmations);
                            totalRows = res.totalRows;

                            No_Of_Count_With_Bag_Weight = "";
                            Balance_Value = "";
                            if (res.totalRows > 0)
                            {
                                No_Of_Count_With_Bag_Weight = "" + res.totalRows + " Nos. ( " + res.confirm_value + "  )";
                                Balance_Value = res.balance_value;
                            }
                            else
                            {
                                No_Of_Count_With_Bag_Weight = "No Records.";
                                Balance_Value = "0 BAGS / 0 FCL";
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
                                new KeyValuePair<string, string>("approved","1"),
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
                                DraftConfirmations.Add(item);
                            }
                            totalRows = res.totalRows;

                            if (res.totalRows > 0)
                            {
                                No_Of_Count_With_Bag_Weight = "" + res.totalRows + " Nos. ( " + res.confirm_value + "  )";
                                Balance_Value = res.balance_value;
                            }
                            else
                            {
                                No_Of_Count_With_Bag_Weight = "No Records.";
                                Balance_Value = "0 BAGS / 0 FCL";
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

        public async Task<DraftConfirmation> StoreDraftConfirmationCommand(DraftConfirmation draftConfirmation)
        {
            //Indexes draftConfirmation = (Indexes)_draftConfirmation;
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
                            new KeyValuePair<string,string>("confirmation_no",draftConfirmation.confirmation_no.ToString()),
                            //new KeyValuePair<string,string>("dispatch_from_date",draftConfirmation.dispatch_from_date.ToString("yyyy/MM/dd")),
                            //new KeyValuePair<string,string>("dispatch_to_date",draftConfirmation.dispatch_to_date.ToString("yyyy/MM/dd")),
                            //new KeyValuePair<string,string>("payment_date",draftConfirmation.payment_date.ToString("yyyy/MM/dd")),
                            new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                            //new KeyValuePair<string,string>("enquiry_ids",draftConfirmation.enquiry_ids.ToString()),                            
                        });
                        
                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/draft_confirmation_store", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        DraftConfirmation_Store_Result res = Newtonsoft.Json.JsonConvert.DeserializeObject<DraftConfirmation_Store_Result>(response);
                        res.draftConfirmations.supplier_name = draftConfirmation.supplier_name;
                        res.draftConfirmations.customer_name = draftConfirmation.customer_name;
                        res.draftConfirmations.count_name = draftConfirmation.count_name;
                        res.draftConfirmations.count_name = draftConfirmation.count_name;
                        res.draftConfirmations.qty_unit = res.draftConfirmations.qty.ToString() + " " + res.draftConfirmations.unit;
                        res.draftConfirmations.price_per = res.draftConfirmations.price.ToString() + " " + res.draftConfirmations.per; 
                        //foreach (DraftConfirmation item in res.draftConfirmations)
                        //{
                        draftConfirmation = res.draftConfirmations;                            
                        //}

                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
             return draftConfirmation;
        }
    }
}