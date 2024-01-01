using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;
namespace yarn_brokerage.Models
{
    public class DraftConfirmation_list
    {
        [JsonProperty("index")]
        public List<DraftConfirmation> draftConfirmations { get; set; }

        [JsonProperty("totalRows")]
        public int totalRows { get; set; }

        [JsonProperty("confirm_value")]
        public string confirm_value { get; set; }

        [JsonProperty("balance_value")]
        public string balance_value { get; set; }


        [JsonProperty("confirm_total_value")]
        public int ConfirmTotalValue { get; set; }

    }
    public class MessageGroup : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChangedEventHandler handler = PropertyChanged;
        //    handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        protected void SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("team_group_id")]
        public int TeamGroupId { get; set; }

        [JsonProperty("group_whatsapp_id")]
        public string GroupWhatsappId { get; set; }

        [JsonProperty("is_default")]
        public int IsDefault { get; set; }

        private Color backgroundColor = System.Drawing.Color.Transparent;
        public bool SendMessage { get; set; }


        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set { SetProperty(ref backgroundColor, value); }
        }


        //public Color BackgroundColor
        //{
        //    get { return backgroundColor; }
        //    set
        //    {
        //        if (backgroundColor != value)
        //        {
        //            backgroundColor = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}   

    }
    public class DraftConfirmation_Detail_list
    {
        [JsonProperty("index")]
        public List<DraftConfirmationDetails> draftConfirmationDetails { get; set; }
    }

    public class DraftConfirmation_Store_Result
    {
        [JsonProperty("index")]
        public DraftConfirmation draftConfirmations { get; set; }

        [JsonProperty("supplier_whatsapp_group")]
        public int SupplierWhatsappGroup { get; set; }

        [JsonProperty("message")]
        public string message { get; set; }
    }

    public class DraftConfirmation
    {

        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("transaction_date_time")]
        public DateTime transaction_date_time { get; set; }

        [JsonProperty("segment")]
        public int segment { get; set; }
        [JsonProperty("confirmation_no")]
        public string confirmation_no { get; set; }
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
        [JsonProperty("bag_weight")]
        public double bag_weight { get; set; }
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
        //[JsonProperty("dispatch_from_date")]
        //public DateTime dispatch_from_date { get; set; }
        //[JsonProperty("dispatch_to_date")]
        //public DateTime dispatch_to_date { get; set; }
        //[JsonProperty("payment_date")]
        //public DateTime payment_date { get; set; }
        [JsonProperty("user_name")]
        public string user_name { get; set; }
        [JsonProperty("description")]
        public string description { get; set; }
        [JsonProperty("admin_user")]
        public Boolean admin_user { get; set; } = false;
        [JsonProperty("enquiry_ids")]
        public string enquiry_ids { get; set; } = null;
        [JsonProperty("status")]
        public int status { get; set; }
        [JsonProperty("send_for_approval")]
        public int send_for_approval { get; set; }
        [JsonProperty("payment_status")]
        public int payment_status { get; set; }
        [JsonProperty("dispatch_confirm_status")]
        public int dispatch_confirm_status { get; set; }
        [JsonProperty("dispatch_status")]
        public int dispatch_status { get; set; }
        [JsonProperty("delivery_status")]
        public int delivery_status { get; set; }
        [JsonProperty("dispatch_payment_confirmation")]
        public int dispatch_payment_confirmation { get; set; }
        public string status_image { get; set; }
        [JsonProperty("confirmed_user")]
        public string confirmed_user { get; set; }
        [JsonProperty("approved_user")]
        public string approved_user { get; set; }
        [JsonProperty("rejected_user")]
        public string rejected_user { get; set; }
        [JsonProperty("payment_user")]
        public string payment_user { get; set; }
        [JsonProperty("dispatch_confirm_user")]
        public string dispatch_confirm_user { get; set; }
        [JsonProperty("dispatch_user")]
        public string dispatch_user { get; set; }
        [JsonProperty("delivery_user")]
        public string delivery_user { get; set; }
        [JsonProperty("approved_user_id")]
        public string approved_user_id { get; set; }
        [JsonProperty("rejected_user_id")]
        public string rejected_user_id { get; set; }
        [JsonProperty("payment_user_id")]
        public string payment_user_id { get; set; }
        [JsonProperty("dispatch_confirm_user_id")]
        public string dispatch_confirm_user_id { get; set; }
        [JsonProperty("dispatch_user_id")]
        public string dispatch_user_id { get; set; }
        [JsonProperty("delivery_user_id")]
        public string delivery_user_id { get; set; }
        [JsonProperty("transaction_detail")]
        public string transaction_detail { get; set; }

        [JsonProperty("item_balance_value")]
        public string item_balance_value { get; set; }





        public string TransactionDetails { get; set; }
        [JsonProperty("customer_sms_no")]
        public string CustomerSmsNo { get; set; }
        [JsonProperty("customer_email")]
        public string CustomerEmail { get; set; }

        [JsonProperty("supplier_email")]
        public string SupplierEmail { get; set; }
        [JsonProperty("supplier_sms_no")]
        public string SupplierSmsNo { get; set; }

        [JsonProperty("supplier_gst")]
        public string SupplierGst { get; set; }

        [JsonProperty("setActive")]
        public string SetActive { get; set; }


        [JsonProperty("delay_days")]
        public string DelayDays { get; set; }

        [JsonProperty("transaction_date_time1")]
        public string TransactionDateTime1 { get; set; }

        [JsonProperty("confirmed_remarks")]
        public string confirmedRemarks { get; set; }


        public decimal invoice_amount { get; set; }

        public int SupplierWhatsappGroup_Ckeck { get; set; }
        public decimal invoice_value { get; set; }
        public string CustomerSMS { get; set; }

        public bool Send_Back_Check { get; set; }
        public string StoreMessage { get; set; }


        public int Flag_Check { get; set; }
        public bool Save_Check { get; set; }
        public bool Final_Edit_Flag { get; set; }
        public bool Cancel_Flag { get; set; }
        public double TotalQty { get; set; }


        public int Cancel_Click { get; set; }
        public bool Add_Flag { get; set; }
        public bool Edit_Flag { get; set; }
        public bool Delete_Flag { get; set; }
        public bool Search_Flag { get; set; }
    }

    public class DraftConfirmationDetails
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

   public class DraftConfirmationPayment_List
    {
        [JsonProperty("index")]
        public List<DraftConfirmationPayment> draftConfirmationPayments { get; set; }
    }


    public class DraftConfirmationPayment
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
        [JsonProperty("invoice_amount")]
        public decimal invoice_amount { get; set; }
        [JsonProperty("excess_amount")]
        public decimal excess_amount { get; set; }
        [JsonProperty("utilized_amount")]
        public decimal utilized_amount { get; set; }
        [JsonProperty("from_advance_id")]
        public int from_advance_id { get; set; }
        public decimal advance_amount { get; set; }
    }

    public class DraftConfirmationDispatchDelivery {
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
        [JsonProperty("godown_unloading_id")]
        public int godown_unloading_id { get; set; }
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
        [JsonProperty("invoice_value")]
        public decimal invoice_value { get; set; }

    }


    public class InvoiceDetails
    {        
        [JsonProperty("bag_weight")]
        public decimal bag_weight { get; set; }

        [JsonProperty("rate_type")]
        public int rate_type { get; set; }

        [JsonProperty("domestic_tax_id")]
        public int domestic_tax_id { get; set; }
        [JsonProperty("domestic_tax_perc")]
        public decimal domestic_tax_perc { get; set; }
        [JsonProperty("export_tax_id")]
        public int export_tax_id { get; set; }
        [JsonProperty("export_tax_perc")]
        public decimal export_tax_perc { get; set; }
    }

}
