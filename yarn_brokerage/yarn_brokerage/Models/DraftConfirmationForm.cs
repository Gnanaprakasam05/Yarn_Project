using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace yarn_brokerage.Models
{
   public class DraftConfirmationForm_List
    {
        [JsonProperty("index")]
        public List<DraftConfirmationForm> draftConfirmationForms { get; set; }
    }


    public class DraftConfirmationForm
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("draft_confirmation_id")]
        public int draft_confirmation_id { get; set; }
        [JsonProperty("draft_confirmation_detail_id")]
        public int draft_confirmation_detail_id { get; set; }
        [JsonProperty("form_id")]
        public int form_id { get; set; }
        [JsonProperty("form_name")]
        public string form_name { get; set; }
        [JsonProperty("received_date")]
        public DateTime received_date { get; set; }
        [JsonProperty("issued_date")]
        public DateTime issued_date { get; set; }
        [JsonProperty("remarks")]
        public string remarks { get; set; }        
        public string status { get; set; }
        [JsonProperty("received")]
        public int received { get; set; } = 0;
        [JsonProperty("issued")]
        public int issued { get; set; } = 0;
    }


}
