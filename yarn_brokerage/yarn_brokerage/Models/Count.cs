using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace yarn_brokerage.Models
{
    public class Count_list
    {
        [JsonProperty("index")]
        public List<Count> counts { get; set; }
        [JsonProperty("totalRows")]
        public int totalRows { get; set; }
    }
    
    public class Count
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}