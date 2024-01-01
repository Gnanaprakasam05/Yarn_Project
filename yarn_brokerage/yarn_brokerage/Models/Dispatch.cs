using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace yarn_brokerage.Models
{
    public class Dispatch_list
    {
        [JsonProperty("index")]
        public List<Dispatch> Dispatchs { get; set; }
        [JsonProperty("totalRows")]
        public int totalRows { get; set; }
    }
    
    public class Dispatch
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("transaction_date_time")]
        public DateTime transaction_date_time { get; set; }
        [JsonProperty("customer_id")]
        public int customer_id { get; set; }
        [JsonProperty("customer_name")]
        public string customer_name { get; set; }
        [JsonProperty("supplier_id")]
        public int supplier_id { get; set; }
        [JsonProperty("supplier_name")]
        public string supplier_name { get; set; }
        [JsonProperty("count_id")]
        public int count_id { get; set; }
        [JsonProperty("count_name")]
        public string count_name { get; set; }        
        [JsonProperty("qty")]
        public double qty { get; set; }
        [JsonProperty("unit")]
        public string unit { get; set; }
        [JsonProperty("qty_unit")]
        public string qty_unit { get; set; }
        [JsonProperty("price")]
        public decimal price { get; set; }
        [JsonProperty("per")]
        public string per { get; set; }
        [JsonProperty("price_per")]
        public string price_per { get; set; }
        [JsonProperty("lr_number")]
        public string lr_number { get; set; }
        [JsonProperty("lr_date")]
        public DateTime lr_date { get; set; }
        [JsonProperty("truck_no")]
        public string truck_no { get; set; }
        [JsonProperty("driver_name")]
        public string driver_name { get; set; }
        [JsonProperty("driver_number")]
        public string driver_number { get; set; }
        [JsonProperty("transporter_id")]
        public int transporter_id { get; set; }
        [JsonProperty("transporter_name")]
        public string transporter_name { get; set; }
        [JsonProperty("invoice_date")]
        public DateTime invoice_date { get; set; }
        [JsonProperty("invoice_number")]
        public string invoice_number { get; set; }
        [JsonProperty("invoice_amount")]
        public decimal invoice_amount { get; set; }

        [JsonProperty("user_name")]
        public string user_name { get; set; }
        [JsonProperty("admin_user")]
        public Boolean admin_user { get; set; } = false;        
        [JsonProperty("status")]
        public int status { get; set; }
        [JsonProperty("dispatch_status")]
        public int dispatch_status { get; set; }
        [JsonProperty("dispatch_user_id")]
        public string dispatch_user_id { get; set; }
        [JsonProperty("dispatch_user")]
        public string dispatch_user { get; set; }
    }
}