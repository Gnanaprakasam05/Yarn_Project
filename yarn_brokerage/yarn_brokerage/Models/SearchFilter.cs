using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace yarn_brokerage.Models
{
    public class search_string
    {
        public DateTime current_date { get; set; }
        public string Search_string { get; set; } = "";
    }

    public class dispatch_search_string
    {
        public int bags_ready { get; set; }
        public int payment_ready { get; set; }
        public int payment_received { get; set; }
        public int transporter_ready { get; set; }
        public DateTime current_date { get; set; }
        public string Search_string { get; set; } = "";
        public int overdue_flag { get; set; }
    }

    public class SearchFilter
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
        [JsonProperty("qty_unit")]
        public string qty_unit { get; set; }
        [JsonProperty("price_per")]
        public string price_per { get; set; }
        [JsonProperty("current_price")]
        public decimal current_price { get; set; }
        [JsonProperty("user_id")]
        public int user_id { get; set; }
        [JsonProperty("user_name")]
        public string user_name { get; set; }
        [JsonProperty("admin_user")]
        public Boolean admin_user { get; set; } = false;
        [JsonProperty("image")]
        public string image { get; set; } = "customer.png";
        [JsonProperty("offer_counts")]
        public string offer_counts { get; set; }
        [JsonProperty("search_flag")]
        public int search_flag { get; set; }
        public int filter_flag { get; set; }
        public int cancel_flag { get; set; }


    }

    public class SearchConfirmationFilter
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("confirmation_date_from")]
        public DateTime confirmation_date_from { get; set; }
        [JsonProperty("confirmation_date_to")]
        public DateTime confirmation_date_to { get; set; }
        [JsonProperty("transaction_type")]
        public int transaction_type { get; set; }
        [JsonProperty("segment")]
        public int segment { get; set; }
        [JsonProperty("ledger_id")]
        public int ledger_id { get; set; }
        [JsonProperty("ledger_name")]
        public string ledger_name { get; set; }
        [JsonProperty("exact_ledger_id")]
        public int exact_ledger_id { get; set; }
        [JsonProperty("exact_ledger_name")]
        public string exact_ledger_name { get; set; }
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
        [JsonProperty("qty_unit")]
        public string qty_unit { get; set; }
        [JsonProperty("price_per")]
        public string price_per { get; set; }
        [JsonProperty("current_price")]
        public decimal current_price { get; set; }
        [JsonProperty("user_id")]
        public int user_id { get; set; }
        [JsonProperty("user_name")]
        public string user_name { get; set; }
        [JsonProperty("admin_user")]
        public Boolean admin_user { get; set; } = false;
        [JsonProperty("image")]
        public string image { get; set; } = "customer.png";
        [JsonProperty("offer_counts")]
        public string offer_counts { get; set; }
        [JsonProperty("search_flag")]
        public int search_flag { get; set; }
        public int filter_flag { get; set; }

        [JsonProperty("dispatch_date_from")]
        public DateTime dispatch_date_from { get; set; }
        [JsonProperty("dispatch_date_to")]
        public DateTime dispatch_date_to { get; set; }
        [JsonProperty("payment_date_from")]
        public DateTime payment_date_from { get; set; }
        [JsonProperty("payment_date_to")]
        public DateTime payment_date_to { get; set; }

        [JsonProperty("confirmation_date")]
        public int confirmation_date { get; set; }
        [JsonProperty("dispatch_date")]
        public int dispatch_date { get; set; }
        [JsonProperty("payment_date")]
        public int payment_date { get; set; }
        public string search_string { get; set; } = "";
        [JsonProperty("confirmation_no")]
        public string confirmation_no { get; set; } = "";

        public int approved { get; set; }
        public int bags_ready { get; set; }
        public int payment_ready { get; set; }
        public int payment_received { get; set; }
        public int transporter_ready { get; set; }
        public int dispatched { get; set; }
        public int invoiced { get; set; }
        public int delivered { get; set; }
        public int customer_confirmed { get; set; }

        public int not_approved { get; set; }
        public int not_bags_ready { get; set; }
        public int not_payment_ready { get; set; }
        public int not_payment_received { get; set; }
        public int not_transporter_ready { get; set; }
        public int not_dispatched { get; set; }
        public int not_invoiced { get; set; }
        public int not_delivered { get; set; }
        public int not_customer_confirmed { get; set; }

        public int group_by_customer { get; set; }
        public int group_by_supplier { get; set; }
        public int group_by_count { get; set; }
        public int group_by_segment { get; set; }
        public int group_by_month { get; set; }
        public int group_by_year { get; set; }



        public bool Search_Edit { get; set; }
        public bool Date_Change { get; set; }



    }

    public class SearchApprovalFilter
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("confirmation_date_from")]
        public DateTime confirmation_date_from { get; set; }
        [JsonProperty("confirmation_date_to")]
        public DateTime confirmation_date_to { get; set; }
        [JsonProperty("transaction_type")]
        public int transaction_type { get; set; }
        [JsonProperty("ledger_id")]
        public int ledger_id { get; set; }
        [JsonProperty("ledger_name")]
        public string ledger_name { get; set; }
        [JsonProperty("exact_ledger_id")]
        public int exact_ledger_id { get; set; }
        [JsonProperty("exact_ledger_name")]
        public string exact_ledger_name { get; set; }
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
        [JsonProperty("qty_unit")]
        public string qty_unit { get; set; }
        [JsonProperty("price_per")]
        public string price_per { get; set; }
        [JsonProperty("current_price")]
        public decimal current_price { get; set; }
        [JsonProperty("user_id")]
        public int user_id { get; set; }
        [JsonProperty("user_name")]
        public string user_name { get; set; }
        [JsonProperty("admin_user")]
        public Boolean admin_user { get; set; } = false;
        [JsonProperty("image")]
        public string image { get; set; } = "customer.png";
        [JsonProperty("offer_counts")]
        public string offer_counts { get; set; }
        [JsonProperty("search_flag")]
        public int search_flag { get; set; }
        public int filter_flag { get; set; }

        [JsonProperty("approved_date_from")]
        public DateTime approved_date_from { get; set; }
        [JsonProperty("approved_date_to")]
        public DateTime approved_date_to { get; set; }

        [JsonProperty("confirmation_date")]
        public int confirmation_date { get; set; }
        [JsonProperty("approved_date")]
        public int approved_date { get; set; }
        public string search_string { get; set; } = "";

        public bool Date_Change { get; set; }
        public bool Search_Edit { get; set; }
    }

    public class SearchDispatchConfirmationFilter
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("confirmation_date_from")]
        public DateTime confirmation_date_from { get; set; }
        [JsonProperty("confirmation_date_to")]
        public DateTime confirmation_date_to { get; set; }
        [JsonProperty("transaction_type")]
        public int transaction_type { get; set; }
        [JsonProperty("ledger_id")]
        public int ledger_id { get; set; }
        [JsonProperty("ledger_name")]
        public string ledger_name { get; set; }
        [JsonProperty("exact_ledger_id")]
        public int exact_ledger_id { get; set; }
        [JsonProperty("exact_ledger_name")]
        public string exact_ledger_name { get; set; }
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
        [JsonProperty("qty_unit")]
        public string qty_unit { get; set; }
        [JsonProperty("price_per")]
        public string price_per { get; set; }
        [JsonProperty("current_price")]
        public decimal current_price { get; set; }
        [JsonProperty("user_id")]
        public int user_id { get; set; }
        [JsonProperty("user_name")]
        public string user_name { get; set; }
        [JsonProperty("admin_user")]
        public Boolean admin_user { get; set; } = false;
        [JsonProperty("image")]
        public string image { get; set; } = "customer.png";
        [JsonProperty("offer_counts")]
        public string offer_counts { get; set; }
        [JsonProperty("search_flag")]
        public int search_flag { get; set; }
        public int filter_flag { get; set; }

        [JsonProperty("approved_date_from")]
        public DateTime approved_date_from { get; set; }
        [JsonProperty("approved_date_to")]
        public DateTime approved_date_to { get; set; }

        [JsonProperty("dispatched_date_from")]
        public DateTime dispatched_date_from { get; set; }
        [JsonProperty("dispatched_date_to")]
        public DateTime dispatched_date_to { get; set; }

        [JsonProperty("confirmation_date")]
        public int confirmation_date { get; set; }
        [JsonProperty("approved_date")]
        public int approved_date { get; set; }
        [JsonProperty("dispatched_date")]
        public int dispatched_date { get; set; }
        [JsonProperty("include_pending")]
        public int include_pending { get; set; }
        [JsonProperty("include_received_form")]
        public int include_received_form { get; set; }
        public string search_string { get; set; } = "";
        public int outersearch { get; set; }
        [JsonProperty("confirmation_no")]
        public string confirmation_no { get; set; } = "";
        public int bags_ready { get; set; }
        public int payment_ready { get; set; }
        public int payment_received { get; set; }
        public int transporter_ready { get; set; }
        public int filter_check { get; set; }

        public bool DispatchDate_Change { get; set; }
        public bool ConfirmationDate_Change { get; set; }
        public bool Dispatched_Change { get; set; }
        public bool Search_Edit { get; set; }

        public int TeamId { get; set; }

        public string TeamName { get; set; }
        public int TeamGroupId { get; set; }

        public string TeamGroupName { get; set; }

    }


    public class SearchCallLogFilter
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("call_date")]
        public int call_date { get; set; }
        [JsonProperty("call_date_from")]
        public DateTime call_date_from { get; set; }
        [JsonProperty("call_date_to")]
        public DateTime call_date_to { get; set; }
        [JsonProperty("user_id")]
        public int user_id { get; set; }
        [JsonProperty("user_name")]
        public string user_name { get; set; }
        [JsonProperty("admin_user")]
        public Boolean admin_user { get; set; } = false;
        [JsonProperty("search_flag")]
        public int search_flag { get; set; }
        public int filter_flag { get; set; }
        public string search_string { get; set; } = "";
    }

    public class SearchOutStandingFilter
    {
        public int contact_type_id { get; set; }
        public int contact_id { get; set; }
        public string ledger_name { get; set; }
        public DateTime invoice_date_from { get; set; }
        public DateTime invoice_date_to { get; set; }
        public int commission_date_flg { get; set; }
        public string search_string { get; set; } = "";
    }
}
