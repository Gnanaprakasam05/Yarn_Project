using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace yarn_brokerage.Models
{
    public class CheckDraftConfirmationDetails
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("draft_confirmation_id")]
        public int draft_confirmation_id { get; set; }
        [JsonProperty("dispatch_date")]
        public DateTime dispatch_date { get; set; }
        [JsonProperty("payment_date")]
        public DateTime payment_date { get; set; }
        [JsonProperty("qty")]
        public decimal qty { get; set; }
        [JsonProperty("balance_qty")]
        public decimal balance_qty { get; set; }
        [JsonProperty("unit")]
        public string unit { get; set; }
        [JsonProperty("qty_unit")]
        public string qty_unit { get; set; }
        [JsonProperty("bag_weight")]
        public decimal bag_weight { get; set; }
        [JsonProperty("gross_weight")]
        public decimal gross_weight { get; set; }
        [JsonProperty("no_of_boxes")]
        public decimal no_of_boxes { get; set; }
        [JsonProperty("rate_type")]
        public int rate_type { get; set; }
        [JsonProperty("gross_amount")]
        public decimal gross_amount { get; set; }
        [JsonProperty("tax_id")]
        public int tax_id { get; set; }
        [JsonProperty("tax_prec")]
        public decimal tax_prec { get; set; }
        [JsonProperty("tax_amount")]
        public decimal tax_amount { get; set; }
        [JsonProperty("frieght")]
        public decimal frieght { get; set; }
        [JsonProperty("insurance")]
        public decimal insurance { get; set; }
        [JsonProperty("other_charges")]
        public decimal other_charges { get; set; }
        [JsonProperty("invoice_value")]
        public decimal invoice_value { get; set; }
        [JsonProperty("cancel_dispatch")]
        public int cancel_dispatch { get; set; }

        [JsonProperty("remarks")]
        public string remarks { get; set; }

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
        [JsonProperty("dispatched_date")]
        public DateTime dispatched_date { get; set; }

        [JsonProperty("customer_acknowledgement")]
        public int customer_acknowledgement { get; set; }
        [JsonProperty("acknowledgement_date")]
        public DateTime acknowledgement_date { get; set; }
        [JsonProperty("acknowledgement_remarks")]
        public string acknowledgement_remarks { get; set; }
        [JsonProperty("program_approval")]
        public int program_approval { get; set; }
        [JsonProperty("program_approved")]
        public int program_approved { get; set; }
        [JsonProperty("allow_credit_billing")]
        public int allow_credit_billing { get; set; }
        public string dispatch_status { get; set; }
        public int dispatch_status_visible { get; set; }
        public string TextColor { get; set; }
        [JsonProperty("amend")]
        public int amend { get; set; }
        [JsonProperty("status")]
        public int status { get; set; }
        [JsonProperty("send_for_approval")]
        public int send_for_approval { get; set; }
        //[JsonProperty("payment_details")]
        //public List<DraftConfirmationPayment> draft_confirmation_payment_detail { get; set; }
    }

}
