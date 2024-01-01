using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace yarn_brokerage.Models
{
    public class Negotiation_list
    {

        [JsonProperty("index")]
        public List<Negotiation> negotiations { get; set; }
    }

    public class Negotiation
    {
        [JsonProperty("id")]
        public int id { get; set; }
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
        [JsonProperty("customer_enquiry_id")]
        public int customer_enquiry_id { get; set; }
        [JsonProperty("supplier_enquiry_id")]
        public int supplier_enquiry_id { get; set; }
        [JsonProperty("initial_offer_price")]
        public decimal initial_offer_price { get; set; }
        [JsonProperty("initial_bid_price")]
        public decimal initial_bid_price { get; set; }
        [JsonProperty("last_offer_price")]
        public decimal last_offer_price { get; set; }
        [JsonProperty("last_bid_price")]
        public decimal last_bid_price { get; set; }
        [JsonProperty("current_offer_price")]
        public decimal current_offer_price { get; set; }
        [JsonProperty("current_bid_price")]
        public decimal current_bid_price { get; set; }
        [JsonProperty("count_id")]
        public int count_id { get; set; }
        [JsonProperty("count_name")]
        public string count_name { get; set; }
        [JsonProperty("bags")]
        public double bags { get; set; }

        [JsonProperty("available_bags")]
        public double available_bags { get; set; }
        
        [JsonProperty("user_name")]
        public string user_name { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("admin_user")]
        public Boolean admin_user { get; set; } = false;

    }
}
