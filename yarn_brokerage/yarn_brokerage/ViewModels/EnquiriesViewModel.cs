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
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace yarn_brokerage.ViewModels
{
    public class EnquiriesViewModel : BaseViewModel
    {
        public InfiniteScrollCollection<Indexes> Enquiries { get; set; }

        private Indexes _draftData;

        public Indexes Indexes
        {
            get { return _draftData; }
            set { SetProperty(ref _draftData, value); }
        }

        public ObservableCollection<Indexes> Index_List { get; set; } = new ObservableCollection<Indexes>();

        public Command LoadItemsCommand { get; set; }
        public Command SearchItemsCommand { get; set; }

        //public Command AddItemsCommand { get; set; }
        public DateTime date { get; set; }

        private const int PageSize = 10;
   
        public Indexes indexes;
        public SearchFilter SearchFilter { get; set; }
        // public Command StoreEnquiryCommand { get; set; }

        public EnquiriesViewModel(SearchFilter searchFilter=null)
        {
            
            SearchFilter = searchFilter;
            date = DateTime.Now.ToLocalTime();

            Enquiries = new InfiniteScrollCollection<Indexes>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = Enquiries.Count / PageSize;

                    //var items = await _dataService.GetItemsAsync(page, PageSize);

                    using (var cl = new HttpClient())
                    {
                        HttpContent formcontent = null;
                        if (SearchFilter != null)
                        {
                            formcontent = new FormUrlEncodedContent(new[]
                            {
                                new KeyValuePair<string,string>("transaction_type",SearchFilter.transaction_type.ToString()),
                                new KeyValuePair<string,string>("count_id",SearchFilter.count_id.ToString()),
                                new KeyValuePair<string,string>("segment",SearchFilter.segment.ToString()),
                                new KeyValuePair<string,string>("ledgers_id",SearchFilter.ledger_id.ToString()),
                                new KeyValuePair<string,string>("ledger_id",SearchFilter.ledger_id.ToString()),
                                new KeyValuePair<string,string>("exact_ledger_id",SearchFilter.exact_ledger_id.ToString()),
                                new KeyValuePair<string,string>("supplier_id",SearchFilter.supplier_id.ToString()),
                                new KeyValuePair<string,string>("customer_id",SearchFilter.customer_id.ToString()),
                                new KeyValuePair<string,string>("filter_flag",SearchFilter.filter_flag.ToString()),
                                new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                new KeyValuePair<string,string>("s_user_id",SearchFilter.user_id.ToString()),
                                new KeyValuePair<string,string>("enquiry_date", SearchFilter.transaction_date_time.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string, string>("CurrentPageNumber", page.ToString()),
                                new KeyValuePair<string, string>("PageSize", PageSize.ToString())
                            });
                        }
                        else
                        {
                            if (Title == "Offers")
                            {
                                formcontent = new FormUrlEncodedContent(new[]
                                {
                                    //new KeyValuePair<string,string>("transaction_type","1"),
                                    new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                    new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                    new KeyValuePair<string,string>("enquiry_date",_enquiry_date.ToString("yyyy-MM-dd")),
                                    new KeyValuePair<string, string>("CurrentPageNumber", page.ToString()),
                                    new KeyValuePair<string, string>("PageSize", PageSize.ToString())
                                });
                            }
                            else
                            {
                                formcontent = new FormUrlEncodedContent(new[]
                                {
                                    //new KeyValuePair<string,string>("transaction_type","2"),
                                    new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                    new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                    new KeyValuePair<string,string>("enquiry_date",_enquiry_date.ToString("yyyy-MM-dd")),
                                    new KeyValuePair<string, string>("CurrentPageNumber", page.ToString()),
                                    new KeyValuePair<string, string>("PageSize", PageSize.ToString())
                                });
                            }
                        }

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/enquiry_list_mobile", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        Enquiry res = Newtonsoft.Json.JsonConvert.DeserializeObject<Enquiry>(response);
                        Enquiry enquiry_List = new Enquiry();
                        enquiry_List.totalRows = res.totalRows;


                        enquiry_List.Index = new List<Indexes>();

                        foreach (Indexes item in res.Index)
                        {
                            if (Application.Current.Properties["user_type"].ToString() == "1")
                                item.admin_user = true;
                            if (item.transaction_type == 2)
                            {
                                item.description_color = "Green";
                                item.image = "buyer.png";
                                item.reverse_image = "customer.png";
                            }
                            if (item.price != Convert.ToInt32(item.price))
                                item.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.price)));
                            else
                                item.price = Convert.ToInt32(item.price);
                            if (item.current_price != Convert.ToInt32(item.current_price))
                                item.current_price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.current_price)));
                            else
                                item.current_price = Convert.ToInt32(item.current_price);
                            item.price_per = item.current_price.ToString() + " " + item.per;


                            Enquiries.Add(item);

                           
                            //foreach (var item1 in Enquiries)
                            //{
                            //    item1.qty_unit = "Test";
                            //    item1.count_name = "Test";
                            //}
                        }

                      

                        IsBusy = false;
                        return enquiry_List.Index;
                    }
                    // return the items that need to be added

                },
                OnCanLoadMore = () =>
                {
                    return Enquiries.Count < totalRows;
                }
            };

            

            if (searchFilter == null)
                LoadItemsCommand = new Command(async (object enquiry_date) => await ExecuteLoadItemsCommand(enquiry_date));
            else if(searchFilter != null)
                LoadItemsCommand = new Command(async (object searchFilters) => await ExecuteStoreItemsCommand(searchFilter));
            SearchItemsCommand = new Command(async (object searchFilters) => await ExecuteStoreItemsCommand(searchFilters));
            //AddItemsCommand = new Command(async (object Enquiry) => await AddStoreItemsCommand(Enquiry));
            // StoreEnquiryCommand = new Command(async (object enquiry) => await ExecuteStoreEnquiryCommand(enquiry));
            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as Item;
            //    Enquiries.Add(newItem);
            //    await DataStore.AddItemAsync(newItem);
            //});
        }

        DateTime _enquiry_date;
        private int totalRows = 0;
        async Task ExecuteLoadItemsCommand(object enquiry_date)
        {

            if (IsBusy)
                return;

            if (enquiry_date != null)
                _enquiry_date = (DateTime)enquiry_date;
            else
            {
                _enquiry_date = date;
            }

            IsBusy = true;

            try
            {
                Enquiries.Clear();

                try
                {
                    var current = Connectivity.NetworkAccess;

                    string access = Application.Current.Properties["user_token"].ToString();

                    if (current == NetworkAccess.Internet)
                    {
                        using (var cl = new HttpClient())
                        {
                            HttpContent formcontent = null;
                            if (Title == "Offers")
                            {
                                formcontent = new FormUrlEncodedContent(new[]
                                {
                                    //new KeyValuePair<string,string>("transaction_type","1"),
                                    new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                    new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                    new KeyValuePair<string,string>("enquiry_date",_enquiry_date.ToString("yyyy-MM-dd")),
                                    new KeyValuePair<string, string>("CurrentPageNumber", "0".ToString()),
                                    new KeyValuePair<string, string>("PageSize", PageSize.ToString())
                                });
                            }
                            else
                            {
                                formcontent = new FormUrlEncodedContent(new[]
                                {
                                    //new KeyValuePair<string,string>("transaction_type","2"),
                                    new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                    new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                    new KeyValuePair<string,string>("enquiry_date",_enquiry_date.ToString("yyyy-MM-dd")),
                                    new KeyValuePair<string, string>("CurrentPageNumber", "0".ToString()),
                                    new KeyValuePair<string, string>("PageSize", PageSize.ToString())
                                });
                            }
                            
                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/enquiry_list_mobile", formcontent);
                                

                            var response = await request.Content.ReadAsStringAsync();

                            Enquiry res = Newtonsoft.Json.JsonConvert.DeserializeObject<Enquiry>(response);
                            foreach (Indexes item in res.Index)
                            {
                                if (Application.Current.Properties["user_type"].ToString() == "1")
                                    item.admin_user = true;
                                if (item.transaction_type == 2)
                                {
                                    item.description_color = "Green";
                                    item.image = "buyer.png";
                                    item.reverse_image = "customer.png";
                                }
                                if (item.price != Convert.ToInt32(item.price))
                                    item.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.price)));
                                else
                                    item.price = Convert.ToInt32(item.price);
                                if (item.current_price != Convert.ToInt32(item.current_price))
                                    item.current_price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.current_price)));
                                else
                                    item.current_price = Convert.ToInt32(item.current_price);
                                item.price_per = item.current_price.ToString() + " " + item.per;

                               

                                Enquiries.Add(item);
                            }

                            totalRows = res.totalRows;

                            if (res.totalRows > 0)
                            {
                                No_Of_Count_With_Bag_Weight = "" + res.totalRows + " Nos.";

                            }
                            else
                            {
                                No_Of_Count_With_Bag_Weight = "No Records.";
                              
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

        internal async void StoreEnquiryCommand(Indexes enquiry)
        {
                      // return "Sucess";
        }

        async Task ExecuteStoreItemsCommand(object searchFilter)
        {
            if (IsBusy)
                return;
            if (searchFilter == null)
                return;


            SearchFilter = new SearchFilter();

            SearchFilter = (SearchFilter)searchFilter;


            IsBusy = true;

            try
            {
                Enquiries.Clear();

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
                                new KeyValuePair<string,string>("ledgers_id",SearchFilter.ledger_id.ToString()),
                                //new KeyValuePair<string,string>("ledger_id",SearchFilter.ledger_id.ToString()),
                                new KeyValuePair<string,string>("exact_ledger_id",SearchFilter.exact_ledger_id.ToString()),
                                new KeyValuePair<string,string>("supplier_id",SearchFilter.supplier_id.ToString()),
                                new KeyValuePair<string,string>("customer_id",SearchFilter.customer_id.ToString()),
                                new KeyValuePair<string,string>("filter_flag",SearchFilter.filter_flag.ToString()),
                                new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                new KeyValuePair<string,string>("s_user_id",SearchFilter.user_id.ToString()),
                                new KeyValuePair<string,string>("enquiry_date", SearchFilter.transaction_date_time.ToString("yyyy-MM-dd")),
                                new KeyValuePair<string, string>("CurrentPageNumber", "0".ToString()),
                                new KeyValuePair<string, string>("PageSize", PageSize.ToString())
                            });

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/enquiry_list_mobile", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            Enquiry res = Newtonsoft.Json.JsonConvert.DeserializeObject<Enquiry>(response);
                            foreach (Indexes item in res.Index)
                            {
                                if (Application.Current.Properties["user_type"].ToString() == "1")
                                    item.admin_user = true;
                                if (item.transaction_type == 2)
                                {
                                    item.description_color = "Green";
                                    item.image = "buyer.png";
                                }
                                if (item.price != Convert.ToInt32(item.price))
                                    item.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.price)));
                                else
                                    item.price = Convert.ToInt32(item.price);
                                if (item.current_price != Convert.ToInt32(item.current_price))
                                    item.current_price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.current_price)));
                                else
                                    item.current_price = Convert.ToInt32(item.current_price);
                                item.price_per = item.current_price.ToString() + " " + item.per;
                                Enquiries.Add(item);

                            }
                            SearchFilter.offer_counts = (SearchFilter.transaction_type == 2) ? (Enquiries.Count > 1) ? Enquiries.Count + " Enquiries" : Enquiries.Count + " Enquiry" : (Enquiries.Count > 1) ? Enquiries.Count + " Offers" : Enquiries.Count + " Offer";
                            totalRows = res.totalRows;

                            if (res.totalRows > 0)
                            {
                                No_Of_Count_With_Bag_Weight = "" + res.totalRows + " Nos.";

                            }
                            else
                            {
                                No_Of_Count_With_Bag_Weight = "No Records.";

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


       public async void AddStoreItemsCommand(Indexes indexes)
        {

            if (IsBusy)
                return;
     
            IsBusy = true;

            try
            {


                Enquiries.Add( new Indexes
                {
                    id = indexes.id,
                    transaction_date_time = indexes.transaction_date_time,
                    segment = indexes.segment,
                    transaction_type = indexes.transaction_type,
                    ledger_id = indexes.ledger_id,
                    ledger_name = indexes.ledger_name,
                    exact_ledger_id = indexes.exact_ledger_id,
                    exact_ledger_name = indexes.exact_ledger_name,
                    count_id = indexes.count_id,
                    count_name = indexes.count_name,
                    bag_weight = indexes.bag_weight,
                    qty = indexes.qty,
                    unit = indexes.unit,
                    qty_unit = indexes.qty_unit,
                    price = indexes.price,
                    per = indexes.per,
                    price_per = indexes.price_per,
                    user_name = indexes.user_name,
                    description = indexes.description,
                    admin_user = indexes.admin_user,
                    counts = indexes.counts,
                    current_price = indexes.current_price,
                    description_color = indexes.description_color,
                    image = indexes.image,
                    reverse_image = indexes.reverse_image,
                    offer_counts = indexes.offer_counts,
                    best_supplier = indexes.best_supplier,
                    best_qty = indexes.best_qty,
                    best_price = indexes.best_price,
                    confirmed = indexes.confirmed,
                    hide_confirmed = indexes.hide_confirmed,
                    ValueCheck = indexes.ValueCheck,
                    CancelClick = indexes.CancelClick,
                    Add_Flag = indexes.Add_Flag,
                    Edit_Flag = indexes.Edit_Flag,
                    Search_Flag = indexes.Search_Flag,
                });
                No_Of_Count_With_Bag_Weight = "" + Enquiries.Count() + " Nos.";
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


      
        public async void EditStoreItemsCommand(Indexes indexes)
        {

            if (IsBusy)
                return;

            IsBusy = true;

            try
            {

               
                var index_value = Enquiries.IndexOf(Enquiries.Where(X => X.id == indexes.id).FirstOrDefault());

                Enquiries[index_value] = new Indexes()
                {
                    id = indexes.id,
                    transaction_date_time = indexes.transaction_date_time,
                    segment = indexes.segment,
                    transaction_type = indexes.transaction_type,
                    ledger_id = indexes.ledger_id,
                    ledger_name = indexes.ledger_name,
                    exact_ledger_id = indexes.exact_ledger_id,
                    exact_ledger_name = indexes.exact_ledger_name,
                    count_id = indexes.count_id,
                    count_name = indexes.count_name,
                    bag_weight = indexes.bag_weight,
                    qty = indexes.qty,
                    unit = indexes.unit,
                    qty_unit = indexes.qty_unit,
                    price = indexes.price,
                    per = indexes.per,
                    price_per = indexes.price_per,
                    user_name = indexes.user_name,
                    description = indexes.description,
                    admin_user = indexes.admin_user,
                    counts = indexes.counts,
                    current_price = indexes.current_price,
                    description_color = indexes.description_color,
                    image = indexes.image,
                    reverse_image = indexes.reverse_image,
                    offer_counts = indexes.offer_counts,
                    best_supplier = indexes.best_supplier,
                    best_qty = indexes.best_qty,
                    best_price = indexes.best_price,
                    confirmed = indexes.confirmed,
                    hide_confirmed = indexes.hide_confirmed,
                    ValueCheck = indexes.ValueCheck,
                    CancelClick = indexes.CancelClick,
                    Add_Flag = indexes.Add_Flag,
                    Edit_Flag = indexes.Edit_Flag,
                    Search_Flag = indexes.Search_Flag,
                   

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
        public int EnquiryCount()
        {
            return Enquiries.Count();
        }
    }
}