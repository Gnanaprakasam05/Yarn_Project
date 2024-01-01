using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace yarn_brokerage.Models
{
    public class Transporter_list
    {
        [JsonProperty("index")]
        public List<Transporter> Transporters { get; set; }
        [JsonProperty("totalRows")]
        public int totalRows { get; set; }
    }
    
    public class Transporter
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}