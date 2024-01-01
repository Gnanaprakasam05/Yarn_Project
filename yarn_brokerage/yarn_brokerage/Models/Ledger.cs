using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace yarn_brokerage.Models
{
    public class Ledger_list
    {
        [JsonProperty("index")]
        public List<Ledger> ledgers { get; set; }
        [JsonProperty("totalRows")]
        public int totalRows { get; set; }

    }
    public class Ledger
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("ledger_type")]
        public int LedgerType { get; set; }
        [JsonProperty("mobile_no")]
        public string MobileNo { get; set; }
        [JsonProperty("counts")]
        public string counts { get; set; }
        [JsonProperty("bag_weight")]
        public double bag_weight { get; set; }
        [JsonProperty("commission_type")]
        public int commission_type { get; set; }
        [JsonProperty("company_id")]
        public Nullable<int> company_id { get; set; }
        [JsonProperty("company_name")]
        public string company_name { get; set; }
        [JsonProperty("commission_value")]
        public double commission_value { get; set; }
    }


    public class LedgerTeamName
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("is_default")]
        public string IsDefault { get; set; }
    }
}