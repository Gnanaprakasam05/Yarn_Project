using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace yarn_brokerage.Models
{
    public class Enquiry
    {

        [JsonProperty("index")]
        public List<Indexes> Index { get; set; }

        [JsonProperty("totalRows")]
        public int totalRows { get; set; }
    }

    public class Indexes
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("transaction_date_time")]
        public DateTime transaction_date_time { get; set; }
        [JsonProperty("transaction_type")]
        public int transaction_type { get; set; }
        [JsonProperty("segment")]
        public int segment { get; set; }
        [JsonProperty("ledger_id")]
        public int ledger_id { get; set; }
        [JsonProperty("ledger_name")]
        public string ledger_name { get; set; }
        [JsonProperty("exact_ledger_id")]
        public Nullable<int> exact_ledger_id { get; set; }
        [JsonProperty("exact_ledger_name")]
        public string exact_ledger_name { get; set; }
        [JsonProperty("count_id")]
        public int count_id { get; set; }
        [JsonProperty("count_name")]
        public string count_name { get; set; }
        [JsonProperty("counts")]
        public string counts { get; set; }
        [JsonProperty("bag_weight")]
        public double bag_weight { get; set; }
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
        [JsonProperty("current_price")]
        public decimal current_price { get; set; }
        [JsonProperty("description")]
        public string description { get; set; }
        [JsonProperty("user_name")]
        public string user_name { get; set; }
        [JsonProperty("admin_user")]
        public Boolean admin_user { get; set; } = false;
        [JsonProperty("description_color")]
        public string description_color { get; set; } = "Red";
        [JsonProperty("image")]
        public string image { get; set; } = "customer.png";
        [JsonProperty("reverse_image")]
        public string reverse_image { get; set; } = "buyer.png";
        [JsonProperty("offer_counts")]
        public string offer_counts { get; set; }
        [JsonProperty("best_supplier")]
        public string best_supplier { get; set; }
        [JsonProperty("best_qty")]
        public double best_qty { get; set; }
        [JsonProperty("best_price")]
        public decimal best_price { get; set; }
        [JsonProperty("confirmed")]
        public decimal confirmed { get; set; }
        [JsonProperty("confirm_list")]
        public DraftConfirmation_list confirm_list { get; set; }
        [JsonProperty("hide_confirmed")]
        public decimal hide_confirmed { get; set; }

        public int ValueCheck {get; set;}

        public int CancelClick { get; set; }

        public bool Add_Flag { get; set; }
        public bool Edit_Flag { get; set; }
        public bool Search_Flag { get; set; }
    }

    public class Enquiry_negotiation
    {

        [JsonProperty("customer_list")]
        public List<Indexes> customer_list { get; set; }

        [JsonProperty("supplier_list")]
        public List<Indexes> supplier_list { get; set; }
    }

    public class Enquiry_List
    {
        [JsonProperty("data")]
        public Indexes data { get; set; }

        [JsonProperty("best_list")]
        public Indexes best_list { get; set; }

        [JsonProperty("count")]
        public string count { get; set; }

        [JsonProperty("message")]
        public string message { get; set; }
    }
}
