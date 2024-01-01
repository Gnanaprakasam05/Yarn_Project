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
    public class DeliveryViewModel : BaseViewModel
    {
        public ObservableCollection<Delivery> Deliverys { get; set; }
        public Command LoadItemsCommand { get; set; }
        //public Command SearchItemsCommand { get; set; }
        public DateTime date { get; set; }

        //public SearchFilter SearchFilter { get; set; }
        // public Command StoreDeliveryCommand { get; set; }

        public DeliveryViewModel()
        {
            Title = "Delivery List";
            date = DateTime.Now.ToLocalTime();
            Deliverys = new ObservableCollection<Delivery>();
            //if (searchFilter == null)
                LoadItemsCommand = new Command(async(object item) => await ExecuteLoadItemsCommand());
            //else if (searchFilter != null)
            //LoadItemsCommand = new Command(async (object searchFilters) => await ExecuteStoreItemsCommand(searchFilter));
            //SearchItemsCommand = new Command(async (object searchFilters) => await ExecuteStoreItemsCommand(searchFilters));
            // StoreDeliveryCommand = new Command(async (object Delivery) => await ExecuteStoreDeliveryCommand(Delivery));
            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as Item;
            //    Delivery.Add(newItem);
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
                Deliverys.Clear();
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
                            

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/delivery_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            Delivery_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<Delivery_list>(response);
                            foreach (Delivery item in res.Deliverys)
                            {
                                if (Application.Current.Properties["user_type"].ToString() == "1")
                                    item.admin_user = true;
                                if (item.price != Convert.ToInt32(item.price))
                                    item.price = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(item.price)));
                                else
                                    item.price = Convert.ToInt32(item.price);
                                item.price_per = item.price.ToString() + " " + item.per;
                                Deliverys.Add(item);
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

        public async Task<Delivery> GetDeliveryAsync(int id)
        {
            Delivery item = new Delivery();

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


                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/delivery_list", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        Delivery_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<Delivery_list>(response);
                        item = res.Deliverys[0];
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
        public async Task<Delivery> StoreDeliveryCommand(Delivery Delivery)
        {
            //Indexes Delivery = (Indexes)_Delivery;
            Delivery item = new Delivery();
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
                            new KeyValuePair<string,string>("id",Delivery.Id.ToString()),
                            new KeyValuePair<string,string>("transaction_date_time",DateTime.Now.ToLocalTime().ToString("yyyy/MM/ddTHH:mm")),
                            new KeyValuePair<string,string>("delivery_date",Delivery.delivery_date.ToString("yyyy/MM/dd")),
                            new KeyValuePair<string,string>("delivery_remarks",(Delivery.delivery_remarks != null) ? Delivery.delivery_remarks.ToString():""),
                            new KeyValuePair<string,string>("status",Delivery.status.ToString()),                                            
                            new KeyValuePair<string,string>("user_id",Application.Current.Properties["user_id"].ToString()),
                            new KeyValuePair<string,string>("user_type",Application.Current.Properties["user_type"].ToString()),
                        });
                        
                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/delivery_store", formcontent);

                        //request.EnsureSuccessStatusCode(); 

                        var response = await request.Content.ReadAsStringAsync();

                        Delivery_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<Delivery_list>(response);
                        item = res.Deliverys[0];
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