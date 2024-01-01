using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;

namespace yarn_brokerage.Models
{
    public class CallLog_list
    {
        [JsonProperty("index")]
        public List<CallLogModel> CallLogs { get; set; }

        [JsonProperty("totalRows")]
        public int totalRows { get; set; }

    }
    public class CallLogModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("call_name")]
        public string CallName { get; set; }        
        [JsonProperty("ledger_id")]
        public Nullable<int> ledger_id { get; set; }
        [JsonProperty("ledger_name")]
        public string LedgerName { get; set; }
        [JsonProperty("call_number")]
        public string CallNumber { get; set; }
        [JsonProperty("call_duration")]
        public string CallDuration { get; set; }
        [JsonProperty("call_duration_format")]
        public string CallDurationFormat {
            get
            {
                var intDuration = Convert.ToInt32(CallDuration);
                TimeSpan time = TimeSpan.FromSeconds(intDuration);

                //here backslash is must to tell that colon is
                //not the part of format, it just a character that we want in output
                return time.ToString(@"hh\:mm\:ss\:fff");
            }
        }
        [JsonProperty("call_date_tick")]
        public long CallDateTick { get; set; }
        [JsonProperty("call_date")]
        public DateTime CallDate { get
            {
                return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(this.CallDateTick).AddMinutes(330);
            }
        }
        [JsonProperty("call_type")]
        public string CallType { get; set; }
        [JsonProperty("remarks")]
        public string remarks { get; set; }        
        [JsonProperty("user_name")]
        public string user_name { get; set; }
        [JsonProperty("call_title")]
        public string CallTitle { get => $"{CallNumber} - {CallName}"; }
        [JsonProperty("call_description")]
        public string CallDescription { get => $"{CallDate.ToString("g", CultureInfo.CreateSpecificCulture("en-IN"))} - {CallType} | Duration: {CallDurationFormat}"; }
        [JsonProperty("admin_user")]
        public Boolean admin_user { get; set; } = false;        
        public Boolean normal_user { get {
                return !admin_user;
            }  } 
        [JsonProperty("user_name1")]
        public string user_name1 { get; set; }
    }
}
