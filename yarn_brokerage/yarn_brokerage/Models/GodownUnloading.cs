using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace yarn_brokerage.Models
{
    public class GodownUnloading_list
    {
        [JsonProperty("index")]
        public List<GodownUnloading> GodownUnloadings { get; set; }
        [JsonProperty("totalRows")]
        public int totalRows { get; set; }
    }
    
    public class GodownUnloading
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}