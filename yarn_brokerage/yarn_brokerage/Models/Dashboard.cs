using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using Newtonsoft.Json;
namespace yarn_brokerage.Models
{
    public class Dashboard
    {
        [JsonProperty("offers_count")]
        public int OfferCount { get; set; }
        [JsonProperty("enquiry_count")]
        public int EnquiryCount { get; set; }
        [JsonProperty("confirm_count")]
        public int ConfirmCount { get; set; }        
        [JsonProperty("offer_label")]
        public string OfferLabel { get; set; }
        [JsonProperty("enquiry_label")]
        public string EnquiryLabel { get; set; }
        [JsonProperty("confirm_label")]
        public string ConfirmLabel { get; set; }        
        [JsonProperty("offers_value")]
        public string OfferValue { get; set; }
        [JsonProperty("enquiry_value")]
        public string EnquiryValue { get; set; }
        [JsonProperty("confirm_value")]
        public string ConfirmValue { get; set; }
        [JsonProperty("approval_count")]
        public string ApprovalCount { get; set; }
        [JsonProperty("confirmation_count")]
        public string ConfirmationCount { get; set; }
        [JsonProperty("payment_count")]
        public string PaymentCount { get; set; }
        [JsonProperty("dispatch_confirm_count")]
        public string DispatchConfirmCount { get; set; }
        [JsonProperty("dispatch_confirm_value")]
        public string DispatchConfirmValue { get; set; }
        [JsonProperty("bags_not_ready_count")]
        public string bags_not_ready_count { get; set; }
        [JsonProperty("bags_not_ready_value")]
        public string bags_not_ready_value { get; set; }
        [JsonProperty("payment_not_ready_count")]
        public string payment_not_ready_count { get; set; }
        [JsonProperty("payment_not_ready_value")]
        public string payment_not_ready_value { get; set; }
        [JsonProperty("payment_not_received_count")]
        public string payment_not_received_count { get; set; }
        [JsonProperty("payment_not_received_value")]
        public string payment_not_received_value { get; set; }
        [JsonProperty("transporter_not_ready_count")]
        public string transporter_not_ready_count { get; set; }
        [JsonProperty("transporter_not_ready_value")]
        public string transporter_not_ready_value { get; set; }
        [JsonProperty("dispatched_count")]
        public string dispatched_count { get; set; }
        [JsonProperty("dispatched_value")]
        public string dispatched_value { get; set; }
        [JsonProperty("invoice_details_count")]
        public string invoice_details_count { get; set; }
        [JsonProperty("invoice_details_value")]
        public string invoice_details_value { get; set; }
        [JsonProperty("pending_payment_count")]
        public string pending_payment_count { get; set; }
        [JsonProperty("pending_payment_value")]
        public string pending_payment_value { get; set; }
        [JsonProperty("delivered_count")]
        public string delivered_count { get; set; }
        [JsonProperty("delivered_value")]
        public string delivered_value { get; set; }
        [JsonProperty("dispatch_count")]
        public string DispatchCount { get; set; }
        [JsonProperty("delivery_count")]
        public string DeliveryCount { get; set; }
        [JsonProperty("program_approval_count")]
        public string ProgramApprovalCount { get; set; }
        [JsonProperty("draft_confirmation_count")]
        public string draft_confirmation_count { get; set; }
        [JsonProperty("acknowledgement_count")]
        public string acknowledgement_count { get; set; }
        [JsonProperty("acknowledgement_value")]
        public string acknowledgement_value { get; set; }
        [JsonProperty("forms_count")]
        public string forms_count { get; set; }
        [JsonProperty("forms_value")]
        public string forms_value { get; set; }
        [JsonProperty("pending_commission_count")]
        public string pending_commission_count { get; set; }
        [JsonProperty("pending_commission_value")]
        public string pending_commission_value { get; set; }
        [JsonProperty("after_dispatch_count")]
        public string after_dispatch_count { get; set; }
        [JsonProperty("after_dispatch_value")]
        public string after_dispatch_value { get; set; }
    }
    

    public class TopDashboard
    {
        [JsonProperty("count_name")]
        public string count_name { get; set; }
        [JsonProperty("Qty")]
        public decimal Qty { get; set; }
        [JsonProperty("offer_qty")]
        public decimal OfferQty { get; set; }
        [JsonProperty("confirm_qty")]
        public decimal ConfirmQty { get; set; }
    }



    public class GtexEpayEmployee
    {
        [JsonProperty("id")]
        public int Id{ get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class GtexEpayGroup
    {
        [JsonProperty("id")]
        public int Id{ get; set; }

        [JsonProperty("name")]
        public string Name{ get; set; }
    }

    public class AttendenceDashboard
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("employee_id")]
        public int EmployeeId { get; set; }
        [JsonProperty("yarn_user_id")]
        public int YarnUserId { get; set; }

        [JsonProperty("employee_name")]
        public string EmployeeName { get; set; }

        [JsonProperty("group_name")]
        public string GroupName { get; set; }

        [JsonProperty("full_day")]
        public string FullDay { get; set; }

        [JsonProperty("half_day")]
        public string HalfDay { get; set; }

        [JsonProperty("absent")]
        public string Absent { get; set; }

        [JsonProperty("late_morning")]
        public int LateMorning { get; set; }

        [JsonProperty("late_evening")]
        public int LateEvening { get; set; }

        [JsonProperty("permission_morning")]
        public int PermissionMorning { get; set; }

        [JsonProperty("permission_evening")]
        public int PermissionEvening { get; set; }

        [JsonProperty("late_total")]
        public string LateTotal { get; set; }

        [JsonProperty("permission_total")]
        public string PermissionTotal { get; set; }

        [JsonProperty("setActive")]
        public int SetActive { get; set; }

        [JsonProperty("sno")]
        public int Sno { get; set; }

        [JsonProperty("total_working_days")]

        public int User_Id { get; set; }
        public string TotalWorkingDays { get; set; }
        public DateTime FromDays { get; set; }
        public DateTime ToDays { get; set; }
    }

    public class DashboardAttendence
    {
        

        [JsonProperty("index")]
        public List<AttendenceDashboard> AttendenceDashboard { get; set; }

        [JsonProperty("totalRows")]
        public int TotalRows{ get; set; }

        [JsonProperty("gtex_epay_groups")]
        public List<GtexEpayGroup> GtexEpayGroups{ get; set; }

        [JsonProperty("gtex_epay_employees")]
        public List<GtexEpayEmployee> GtexEpayEmployees{ get; set; }
       
    }

    public class DayPresent
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name{ get; set; }

        [JsonProperty("short_name")]
        public string ShortName{ get; set; }

        [JsonProperty("is_archive")]
        public int IsArchive{ get; set; }
    }

    public class AttendanceList
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("transaction_date")]
        public string TransactionDate { get; set; }

        [JsonProperty("transaction_id")]
        public int TransactionId { get; set; }

        [JsonProperty("employee_id")]
        public int EmployeeId { get; set; }

        [JsonProperty("ticket_no")]
        public int TicketNo { get; set; }

        [JsonProperty("employee_name")]
        public string EmployeeName { get; set; }

        [JsonProperty("day_present_id")]
        public int DayPresentId { get; set; }

        [JsonProperty("group_id")]
        public int GroupId { get; set; }

        [JsonProperty("day_present")]
        public string DayPresent { get; set; }

        [JsonProperty("overtime")]
        public string Overtime { get; set; }

        [JsonProperty("late_day")]
        public int LateDay { get; set; }

        [JsonProperty("late_day_morning")]
        public int LateDayMorning { get; set; }

        [JsonProperty("late_day_lunch")]
        public int LateDayLunch { get; set; }

        [JsonProperty("late_day_evening")]
        public int LateDayEvening { get; set; }

        [JsonProperty("permission_day_morning")]
        public int PermissionDayMorning { get; set; }

        [JsonProperty("permission_day_evening")]
        public int PermissionDayEvening { get; set; }

        [JsonProperty("selected_row")]
        public int SelectedRow { get; set; }
        public string Attendance_List { get; set; }

        public string PermissionDay { get; set; }
        public string LateDays { get; set; }

        public Color Text_Color { get; set; }

    }
    public class DataList
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        public int Sno { get; set; }

        [JsonProperty("log_time")]
        public string LogTime { get; set; }
    }

    public class AttendanceTimeSheet
    {
        [JsonProperty("data")]
        public List<DataList> DataList { get; set; }
    }
    public class AttendanceListData
    {
        [JsonProperty("index")]
        public List<AttendanceList> AttendanceList { get; set; }

        [JsonProperty("totalRows")]
        public int TotalRows { get; set; }

        [JsonProperty("day_presents")]
        public List<DayPresent> DayPresents { get; set; }
    }








}