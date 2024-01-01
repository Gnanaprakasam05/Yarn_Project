using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace yarn_brokerage.Models
{
    public class DispatchConfirm_list
    {
        [JsonProperty("index")]
        public List<DispatchConfirm> DispatchConfirms { get; set; }
        [JsonProperty("totalRows")]
        public int totalRows { get; set; }
        [JsonProperty("dispatch_confirm_value")]
        public string dispatch_confirm_value { get; set; }
    }
    public class GroupBy_list
    {
        [JsonProperty("index")]
        public List<GroupBy> GroupBy { get; set; }
        [JsonProperty("totalRows")]
        public int totalRows { get; set; }
        [JsonProperty("dispatch_confirm_value")]
        public string dispatch_confirm_value { get; set; }
    }

    public class GroupBy
    {
        [JsonProperty("group_by_name")]
        public string group_by_name { get; set; }
        [JsonProperty("bag_weight")]
        public string bag_weight { get; set; }
        [JsonProperty("fcl_weight")]
        public string fcl_weight { get; set; }
        public string weight { get; set; }
    }

    public class DispatchConfirm
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("draft_confirmation_id")]
        public int draft_confirmation_id { get; set; }
        [JsonProperty("transaction_date_time")]
        public DateTime transaction_date_time { get; set; }
        [JsonProperty("transaction_date_time1")]
        public string transaction_date_time1 { get; set; }
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
        [JsonProperty("dispatch_date")]
        public DateTime dispatch_date { get; set; }
        [JsonProperty("dispatch_confirm_date")]
        public DateTime dispatch_confirm_date { get; set; }
        [JsonProperty("user_name")]
        public string user_name { get; set; }
        [JsonProperty("admin_user")]
        public Boolean admin_user { get; set; } = false;
        [JsonProperty("status")]
        public int status { get; set; }
        [JsonProperty("dispatch_confirm_status")]
        public int dispatch_confirm_status { get; set; }
        [JsonProperty("dispatch_confirm_user_id")]
        public string dispatch_confirm_user_id { get; set; }
        [JsonProperty("dispatch_confirm_user")]
        public string dispatch_confirm_user { get; set; }
        [JsonProperty("transaction_detail")]
        public string transaction_detail { get; set; }
        [JsonProperty("confirmation_no")]
        public string confirmation_no { get; set; }
        [JsonProperty("invoice_value")]
        public string invoice_value { get; set; }

        [JsonProperty("bags_ready")]
        public int bags_ready { get; set; }
        [JsonProperty("payment_ready")]
        public int payment_ready { get; set; }
        [JsonProperty("payment_received")]
        public int payment_received { get; set; }
        [JsonProperty("transporter_ready")]
        public int transporter_ready { get; set; }
        [JsonProperty("transporter_id")]
        public int? transporter_id { get; set; }
        [JsonProperty("transporter_name")]
        public string transporter_name { get; set; }
        [JsonProperty("dispatched")]
        public int dispatched { get; set; }
        [JsonProperty("lr_number")]
        public string lr_number { get; set; }
        [JsonProperty("dispatched_date")]
        public DateTime dispatched_date { get; set; }
        [JsonProperty("driver_name")]
        public string driver_name { get; set; }
        [JsonProperty("truck_no")]
        public string truck_no { get; set; }
        [JsonProperty("godown_unloading_id")]
        public int? godown_unloading_id { get; set; }
        [JsonProperty("godown_unloading_name")]
        public string godown_unloading_name { get; set; }
        [JsonProperty("invoice_details")]
        public int invoice_details { get; set; }
        [JsonProperty("invoice_number")]
        public string invoice_number { get; set; }

        [JsonProperty("invoice_date")]
        public DateTime invoice_date { get; set; }

        [JsonProperty("invoice_amount")]
        public decimal invoice_amount { get; set; }

        [JsonProperty("delivered")]
        public int delivered { get; set; }
        [JsonProperty("delivery_date")]
        public DateTime delivery_date { get; set; }
        [JsonProperty("delivery_remarks")]
        public string delivery_remarks { get; set; }

        [JsonProperty("customer_acknowledgement")]
        public int customer_acknowledgement { get; set; }
        [JsonProperty("acknowledgement_date")]
        public DateTime acknowledgement_date { get; set; }
        [JsonProperty("acknowledgement_remarks")]
        public string acknowledgement_remarks { get; set; }
        [JsonProperty("allow_credit_billing")]
        public int allow_credit_billing { get; set; }
        [JsonProperty("program_approval")]
        public int program_approval { get; set; }
        [JsonProperty("program_approved")]
        public int program_approved { get; set; }


        [JsonProperty("customer_whatsapp_group")]
        public string CustomerWhatsAppGroup { get; set; }

        [JsonProperty("customer_email")]
        public string CustomerEmail { get; set; }

        [JsonProperty("dispatched_sms")]
        public int DispatchedSms { get; set; }

        [JsonProperty("invoiced_sms")]
        public int InvoicedSms { get; set; }



        [JsonProperty("supplier_sms_no")]
        public string SupplierSmsNo { get; set; }

        [JsonProperty("supplier_email")]
        public string SupplierEmail { get; set; }

        [JsonProperty("supplier_whatsapp_no")]
        public string SupplierWhatsappNo { get; set; }

        [JsonProperty("supplier_address1_2")]
        public string SupplierAddress12 { get; set; }


        public Boolean invoice_visible_on { get; set; } = false;
        public Boolean invoice_visible_off { get; set; } = false;

        public string TransactionDetails { get; set; }


        [JsonProperty("delay_days")]
        public string DelayDays { get; set; }
    }
}