using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace yarn_brokerage.Models
{
    public class Payment_list
    {
        [JsonProperty("index")]
        public List<Payment> Payments { get; set; }
        [JsonProperty("totalRows")]
        public int totalRows { get; set; }
        [JsonProperty("payment_value")]
        public string payment_value { get; set; }
    }
    
    public class Payment
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
        [JsonProperty("payment_confirm_date")]
        public DateTime payment_confirm_date { get; set; }
        [JsonProperty("amount")]
        public decimal amount { get; set; }
        [JsonProperty("urn_number")]
        public string urn_number { get; set; }        
        [JsonProperty("user_name")]
        public string user_name { get; set; }
        [JsonProperty("admin_user")]
        public Boolean admin_user { get; set; } = false;        
        [JsonProperty("status")]
        public int status { get; set; }
        [JsonProperty("payment_status")]
        public int payment_status { get; set; }
        [JsonProperty("payment_user_id")]
        public string payment_user_id { get; set; }
        [JsonProperty("payment_user")]
        public string payment_user { get; set; }
    }
}