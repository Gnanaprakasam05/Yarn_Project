using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace yarn_brokerage.Models
{
    public class Item
    {       
        public string Id { get; set; }        
        public string Text { get; set; }
        public string Description { get; set; }
    }
}