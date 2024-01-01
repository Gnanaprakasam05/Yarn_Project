using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace yarn_brokerage.Models
{
    public class Attendances
    {

        [JsonProperty("index")]
        public List<Attendance> Attendance { get; set; }

        [JsonProperty("totalRows")]
        public int totalRows { get; set; }
    }

    public class Attendance
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("employee_id")]
        public int employee_id { get; set; }
        [JsonProperty("transaction_date")]
        public DateTime transaction_date { get; set; }
        [JsonProperty("total_days")]
        public float total_days { get; set; }
        [JsonProperty("full_days")]
        public float full_days { get; set; }
        [JsonProperty("late_minute")]
        public float late_minute { get; set; }
        [JsonProperty("permission_minute")]
        public float permission_minute { get; set; }
        [JsonProperty("remarks")]
        public string remarks { get; set; }
    }

    public class AttendanceDetail
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("log_time")]
        public TimeSpan log_time { get; set; }
        [JsonProperty("log_status")]
        public string log_status { get; set; }
        
    }
    
    public class Attendance_Detail_list
    {
        [JsonProperty("data")]
        public List<AttendanceDetail> AttendanceDetails { get; set; }
    }

    public class Attendance_Store_Result
    {
        [JsonProperty("index")]
        public Attendance Attendances { get; set; }

        [JsonProperty("message")]
        public string message { get; set; }
    }
}
