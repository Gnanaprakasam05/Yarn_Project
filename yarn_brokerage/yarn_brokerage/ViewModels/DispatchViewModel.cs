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

namespace yarn_brokerage.ViewModels
{
    public class DispatchViewModel : BaseViewModel
    {
        public ObservableCollection<Dispatch> Dispatchs { get; set; }
        public Command LoadItemsCommand { get; set; }
        //public Command SearchItemsCommand { get; set; }
        public DateTime date { get; set; }

        //public SearchFilter SearchFilter { get; set; }
        // public Command StoreDispatchCommand { get; set; }

        public DispatchViewModel()
        {
            Title = "Dispatch List";
            date = DateTime.Now.ToLocalTime();
            Dispatchs = new ObservableCollection<Dispatch>();
            //if (searchFilter == null)
                LoadItemsCommand = new Command(async(object item) => await ExecuteLoadItemsCommand());
            //else if (searchFilter != null)
            //LoadItemsCommand = new Command(async (object searchFilters) => await ExecuteStoreItemsCommand(searchFilter));
            //SearchItemsCommand = new Command(async (object searchFilters) => await ExecuteStoreItemsCommand(searchFilters));
            // StoreDispatchCommand = new Command(async (object Dispatch) => await ExecuteStoreDispatchCommand(Dispatch));
            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as Item;
            //    Dispatch.Add(newItem);
            //    await DataStore.AddItemAsync(newItem);
            //});
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;            
            IsBusy = true;

            //string _SearchString = "";
            //if (SearchString != null)
            //    _SearchString = (string)SearchString;

            try
            {
                Dispatchs.Clear();
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
                                    new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                                    new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                                    //new KeyValuePair<string,string>("confirmation_date",_confirmation_date.ToString("yyyy-MM-dd"))
                                });
                            

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/dispatch_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            Dispatch_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<Dispatch_list>(response);
                            foreach (Dispatch item in res.Dispatchs)
                            {
                                if (Application.Current.Properties["user_type"].ToString() == "1")
                                    item.admin_user = true;
                                if (item.price != Convert.ToInt32(item.price))
                                    item.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.price)));
                                else
                                    item.price = Convert.ToInt32(item.price);
                                item.price_per = item.price.ToString() + " " + item.per;
                                Dispatchs.Add(item);
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

        public async Task<Dispatch> GetDispatchAsync(int id)
        {
            Dispatch item = new Dispatch();

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


                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/dispatch_list", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        Dispatch_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<Dispatch_list>(response);
                        item = res.Dispatchs[0];
                        if (Application.Current.Properties["user_type"].ToString() == "1")
                            item.admin_user = true;
                        if (item.price != Convert.ToInt32(item.price))
                            item.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.price)));
                        else
                            item.price = Convert.ToInt32(item.price);
                        item.price_per = item.price.ToString() + " " + item.per;                        
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return item;
        }
        public async Task<Dispatch> StoreDispatchCommand(Dispatch Dispatch)
        {
            //Indexes Dispatch = (Indexes)_Dispatch;
            Dispatch item = new Dispatch();
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
                            new KeyValuePair<string,string>("id",Dispatch.Id.ToString()),
                            new KeyValuePair<string,string>("transaction_date_time",DateTime.Now.ToLocalTime().ToString("yyyy/MM/ddTHH:mm")),
                            new KeyValuePair<string,string>("lr_date",Dispatch.lr_date.ToString("yyyy/MM/dd")),
                            new KeyValuePair<string,string>("invoice_date",Dispatch.invoice_date.ToString("yyyy/MM/dd")),
                            new KeyValuePair<string,string>("lr_number",(Dispatch.lr_number != null) ? Dispatch.lr_number.ToString():""),
                            new KeyValuePair<string,string>("truck_no",(Dispatch.truck_no !=null) ? Dispatch.truck_no.ToString():""),
                            new KeyValuePair<string,string>("driver_name",(Dispatch.driver_name!= null) ? Dispatch.driver_name.ToString():""),
                            new KeyValuePair<string,string>("driver_number",(Dispatch.driver_number != null) ? Dispatch.driver_number.ToString():""),
                            new KeyValuePair<string,string>("transporter_id",Dispatch.transporter_id.ToString()),
                            new KeyValuePair<string,string>("invoice_number",(Dispatch.invoice_number != null) ? Dispatch.invoice_number.ToString():""),
                            new KeyValuePair<string,string>("invoice_amount",Dispatch.invoice_amount.ToString()),
                            new KeyValuePair<string,string>("status",Dispatch.status.ToString()),
                            new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                            new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                        });
                        
                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/dispatch_store", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        Dispatch_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<Dispatch_list>(response);
                        item = res.Dispatchs[0];
                        if (Application.Current.Properties["user_type"].ToString() == "1")
                            item.admin_user = true;
                        if (item.price != Convert.ToInt32(item.price))
                            item.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.price)));
                        else
                            item.price = Convert.ToInt32(item.price);
                        item.price_per = item.price.ToString() + " " + item.per;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
             return item;
        }
    }
}