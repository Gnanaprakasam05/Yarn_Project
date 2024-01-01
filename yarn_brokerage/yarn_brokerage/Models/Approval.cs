using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace yarn_brokerage.Models
{
    public class Approval_list
    {
        [JsonProperty("index")]
        public List<Approval> Approvals { get; set; }

        [JsonProperty("totalRows")]
        public int totalRows { get; set; }
        [JsonProperty("approval_value")]
        public string approval_value { get; set; }

    }

    public class Approval
    {
        [JsonProperty("id")]
        public int Id { get; set; }
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
        [JsonProperty("count_id")]
        public int count_id { get; set; }
        [JsonProperty("count_name")]
        public string count_name { get; set; }
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
        [JsonProperty("dispatch_from_date")]
        public DateTime dispatch_from_date { get; set; }
        [JsonProperty("dispatch_to_date")]
        public DateTime dispatch_to_date { get; set; }
        [JsonProperty("payment_date")]
        public DateTime payment_date { get; set; }
        [JsonProperty("user_name")]
        public string user_name { get; set; }
        [JsonProperty("admin_user")]
        public Boolean admin_user { get; set; } = false;
        [JsonProperty("customer_md")]
        public string customer_md { get; set; }
        [JsonProperty("supplier_md")]
        public string supplier_md { get; set; }
        [JsonProperty("customer_md_area")]
        public string customer_md_area { get; set; }
        [JsonProperty("supplier_md_area")]
        public string supplier_md_area { get; set; }
        [JsonProperty("customer_md_mobile_no")]
        public string customer_md_mobile_no { get; set; }
        [JsonProperty("supplier_md_mobile_no")]
        public string supplier_md_mobile_no { get; set; }
        [JsonProperty("confirm_customer_bags")]
        public string confirm_customer_bags { get; set; }
        [JsonProperty("confirm_customer_fcl")]
        public string confirm_customer_fcl { get; set; }
        [JsonProperty("confirm_supplier_bags")]
        public string confirm_supplier_bags { get; set; }
        [JsonProperty("confirm_supplier_fcl")]
        public string confirm_supplier_fcl { get; set; }
        [JsonProperty("pending_customer_bags")]
        public string pending_customer_bags { get; set; }
        [JsonProperty("pending_customer_fcl")]
        public string pending_customer_fcl { get; set; }
        [JsonProperty("pending_supplier_bags")]
        public string pending_supplier_bags { get; set; }
        [JsonProperty("pending_supplier_fcl")]
        public string pending_supplier_fcl { get; set; }
        public string confirm_customer { get; set; }
        public string confirm_supplier { get; set; }
        public string pending_customer { get; set; }
        public string pending_supplier { get; set; }
        [JsonProperty("approved_remarks")]
        public string approved_remarks { get; set; }
        [JsonProperty("status")]
        public int status { get; set; }

        [JsonProperty("approved_user_id")]
        public string approved_user_id { get; set; }
        [JsonProperty("approved_user")]
        public string approved_user { get; set; }
        [JsonProperty("rejected_user_id")]
        public string rejected_user_id { get; set; }
        [JsonProperty("rejected_user")]
        public string rejected_user { get; set; }

        [JsonProperty("dispatch_details")]
        public List<DraftConfirmationDetails> dispatch_details { get; set; }
        [JsonProperty("transaction_detail")]
        public string transaction_detail { get; set; }


        [JsonProperty("customer_team_id")]
        public int CustomerTeamId { get; set; }

        [JsonProperty("supplier_team_id")]
        public int SupplierTeamId { get; set; }

        [JsonProperty("supplier_team_name")]
        public string SupplierTeamName { get; set; }

        [JsonProperty("customer_team_name")]
        public string CustomerTeamName { get; set; }

        [JsonProperty("actual_supplier_team_name")]
        public string ActualSupplierTeamName { get; set; }

        [JsonProperty("actual_customer_team_name")]
        public string ActualCustomerTeamName { get; set; }

        [JsonProperty("customer_confirmed_id")]
        public int CustomerConfirmedId { get; set; }

        [JsonProperty("supplier_confirmed_id")]
        public int SupplierConfirmedId { get; set; }



        [JsonProperty("confirmation_no")]
        public string ConfirmationNo { get; set; }

        [JsonProperty("delay_days")]
        public int DelayDays { get; set; }
        public string TransactionDetails { get; set; }
        public int CancelClick { get; set; }
        public bool Add_Flag { get; set; }
        public bool Edit_Flag { get; set; }
        public string AttendanceTeamName { get; set; }
        public int AttendanceTeamNameId { get; set; }
    }
}