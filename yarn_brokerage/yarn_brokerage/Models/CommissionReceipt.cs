using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace yarn_brokerage.Models
{
    public class CommissionReceipt_list
    {
        [JsonProperty("index")]
        public List<CommissionReceipt> CommissionReceipts { get; set; }

        [JsonProperty("totalRows")]
        public int totalRows { get; set; }

        [JsonProperty("confirm_value")]
        public string confirm_value { get; set; }

        [JsonProperty("balance_value")]
        public string balance_value { get; set; }
        
    }
    
    public class CommissionReceipt_Detail_list
    {
        [JsonProperty("index")]
        public List<CommissionReceiptDetail> CommissionReceiptDetails { get; set; }
    }

    public class CommissionReceipt_Store_Result
    {
        [JsonProperty("index")]
        public CommissionReceipt CommissionReceipts { get; set; }

        [JsonProperty("message")]
        public string message { get; set; }
    }

    public class CommissionReceipt
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("receipt_no")]
        public int receipt_no { get; set; }
        [JsonProperty("receipt_date")]
        public DateTime receipt_date { get; set; }     
        [JsonProperty("ledger_type")]
        public int ledger_type { get; set; }
        [JsonProperty("ledger_id")]
        public int ledger_id { get; set; }
        [JsonProperty("ledger_name")]
        public string ledger_name { get; set; }
        [JsonProperty("company_name")]
        public string company_name { get; set; }
        [JsonProperty("company_id")]
        public Nullable<int> company_id { get; set; }        
        [JsonProperty("total_receipt_amount")]
        public double total_receipt_amount { get; set; }

        [JsonProperty("total_adjusted_amount")]
        public double total_adjusted_amount { get; set; }

        [JsonProperty("total_balance")]
        public double total_balance { get; set; }

        [JsonProperty("image")]
        public string image { get; set; } = "customer.png";
        [JsonProperty("receipt_details")]
        public string receipt_details { get; set; }
        [JsonProperty("commission_type_string")]
        public string commission_type_string { get; set; }
        public string exclude_commission_receipt_id { get; set; }
    }

    public class CommissionReceiptDetail
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("commission_receipt_id")]
        public int commission_receipt_id { get; set; }        
        [JsonProperty("commission_invoice_id")]
        public int commission_invoice_id { get; set; }
        [JsonProperty("invoice_no")]
        public string invoice_no { get; set; }
        [JsonProperty("invoice_date")]
        public DateTime invoice_date { get; set; }
        [JsonProperty("ledger_id")]
        public int ledger_id { get; set; }
        [JsonProperty("ledger_name")]
        public string ledger_name { get; set; }

        [JsonProperty("commission_receipt_amount")]
        public double commission_receipt_amount { get; set; }

        [JsonProperty("balance_commission")]
        public double balance_commission { get; set; }
        [JsonProperty("total_commission")]
        public double total_commission { get; set; }
        [JsonProperty("image")]
        public string image { get; set; } = "customer.png";
        [JsonProperty("receipt_details")]
        public string receipt_details { get; set; }
    }

    public class AddCommissionReceiptDetail_list
    {
        [JsonProperty("index")]
        public List<AddCommissionReceiptDetail> AddCommissionReceiptDetails { get; set; }

        [JsonProperty("totalRows")]
        public int totalRows { get; set; }

    }

    public class AddCommissionReceiptDetail
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("commission_invoice_id")]
        public int commission_invoice_id { get; set; }
        [JsonProperty("invoice_no")]
        public string invoice_no { get; set; }
        [JsonProperty("invoice_date")]
        public DateTime invoice_date { get; set; }
        //[JsonProperty("ledger_type")]
        //public int ledger_type { get; set; }        
        [JsonProperty("ledger_id")]
        public int ledger_id { get; set; }
        [JsonProperty("ledger_name")]
        public string ledger_name { get; set; }        
        [JsonProperty("balance_commission")]
        public double balance_commission { get; set; }
        [JsonProperty("total_commission")]
        public double total_commission { get; set; }
        [JsonProperty("image")]
        public string image { get; set; } = "customer.png";
        [JsonProperty("receipt_details")]
        public string receipt_details { get; set; }
        [JsonProperty("setActive")]
        public Boolean setActive { get; set; }
    }

   public class CommissionReceiptPayment_List
    {
        [JsonProperty("index")]
        public List<CommissionReceiptPayment> CommissionReceiptPayments { get; set; }
    }


    public class CommissionReceiptPayment
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("draft_confirmation_id")]
        public int draft_confirmation_id { get; set; }
        [JsonProperty("draft_confirmation_detail_id")]
        public int draft_confirmation_detail_id { get; set; }
        [JsonProperty("payment_date")]
        public DateTime payment_date { get; set; }
        [JsonProperty("amount")]
        public decimal amount { get; set; }
        [JsonProperty("utr_number")]
        public string utr_number { get; set; }        
        public string utr_number_string { get; set; }
        [JsonProperty("send_for_approval")]
        public int send_for_approval { get; set; } = 0;
        [JsonProperty("Receipt_amount")]
        public decimal Receipt_amount { get; set; }
        [JsonProperty("excess_amount")]
        public decimal excess_amount { get; set; }
        [JsonProperty("utilized_amount")]
        public decimal utilized_amount { get; set; }
        [JsonProperty("from_advance_id")]
        public int from_advance_id { get; set; }
        public decimal advance_amount { get; set; }
    }

    public class CommissionReceiptDispatchDelivery {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("draft_confirmation_id")]
        public int draft_confirmation_id { get; set; }
        [JsonProperty("customer_id")]
        public int customer_id { get; set; }
        [JsonProperty("supplier_id")]
        public int supplier_id { get; set; }
        [JsonProperty("bags_ready")]
        public int bags_ready { get; set; }
        [JsonProperty("payment_ready")]
        public int payment_ready { get; set; }
        [JsonProperty("payment_received")]
        public int payment_received { get; set; }
        [JsonProperty("transporter_ready")]
        public int transporter_ready { get; set; }
        [JsonProperty("transporter_id")]
        public int transporter_id { get; set; }
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
        [JsonProperty("Receipt_details")]
        public int Receipt_details { get; set; }
        [JsonProperty("Receipt_number")]
        public string Receipt_number { get; set; }

        [JsonProperty("Receipt_date")]
        public DateTime Receipt_date { get; set; }

        [JsonProperty("Receipt_amount")]
        public decimal Receipt_amount { get; set; }

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
        [JsonProperty("Receipt_value")]
        public decimal Receipt_value { get; set; }

    }


    //public class ReceiptDetails
    //{        
    //    [JsonProperty("bag_weight")]
    //    public decimal bag_weight { get; set; }

    //    [JsonProperty("rate_type")]
    //    public int rate_type { get; set; }

    //    [JsonProperty("domestic_tax_id")]
    //    public int domestic_tax_id { get; set; }
    //    [JsonProperty("domestic_tax_perc")]
    //    public decimal domestic_tax_perc { get; set; }
    //    [JsonProperty("export_tax_id")]
    //    public int export_tax_id { get; set; }
    //    [JsonProperty("export_tax_perc")]
    //    public decimal export_tax_perc { get; set; }
    //}

}
