using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace yarn_brokerage.Models
{

    public class User_Data
    {
        [JsonProperty("user")]
        public List<User> user { get; set; }
        [JsonProperty("message")]
        public string message { get; set; }
    }
    public class User_list
    {
        [JsonProperty("user")]
        public User user { get; set; }
        [JsonProperty("message")]
        public string message { get; set; }

        [JsonProperty("module_permission")]
        public ModulePermission ModulePermission;


        [JsonProperty("token")]
        public string Token { get; set; }
    }

    public class User
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("user_name")]
        public string user_name { get; set; }
        [JsonProperty("user_type")]
        public string user_type { get; set; }
        [JsonProperty("link_with_attendence_system")]
        public int link_with_attendence_system { get; set; }
        [JsonProperty("trace_calls")]
        public int trace_calls { get; set; }

        [JsonProperty("user_permission_id")]
        public int UserPermissionId { get; set; }

        [JsonProperty("team_group_id")]
        public object TeamGroupId { get; set; }
    }



    public class MobileDashboard
    {


        [JsonProperty("mobile_dashboard_enquiry")]
        public int MobileDashboardEnquiry { get; set; }

        [JsonProperty("mobile_dashboard_all_user_attendance")]
        public int MobileDashboardAllUserAttendance { get; set; }

        [JsonProperty("mobile_dashboard_attendance")]
        public int MobileDashboardAttendance { get; set; }
        [JsonProperty("mobile_dashboard_bags_performance")]
        public int MobileDashboardBagPermission { get; set; }
        [JsonProperty("mobile_dashboard_commission_performance")]
        public int MobileDashboardCommissionPermission { get; set; }

        [JsonProperty("mobile_dashboard_approval")]
        public int MobileDashboardApproval { get; set; }
        [JsonProperty("mobile_dashboard_show_team_group_data")]
        public int MobileDashboardShowTeamGroupData { get; set; }

        [JsonProperty("mobile_dashboard_current_plan")]
        public int MobileDashboardCurrentPlan { get; set; }

        [JsonProperty("mobile_dashboard_dispatched")]
        public int MobileDashboardDispatched { get; set; }
        [JsonProperty("mobile_dashboard_pending_confirmation")]
        public int MobileDashboardPendingConfirmation { get; set; }

    }

    public class MobileTransactionCurrentPlan
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("user_permission_id")]
        public int UserPermissionId;

        [JsonProperty("main_module_id")]
        public string MainModuleId;

        [JsonProperty("module_id")]
        public string ModuleId;

        [JsonProperty("page_id")]
        public string PageId;

        [JsonProperty("module_name")]
        public string ModuleName;

        [JsonProperty("visible")]
        public int Visible;

        [JsonProperty("enable_all")]
        public int EnableAll;

        [JsonProperty("insert_allowed")]
        public int InsertAllowed;

        [JsonProperty("update_allowed")]
        public int UpdateAllowed;

        [JsonProperty("delete_allowed")]
        public int DeleteAllowed;

        [JsonProperty("view_allowed")]
        public int ViewAllowed;

        [JsonProperty("print_allowed")]
        public int PrintAllowed;

        [JsonProperty("export_allowed")]
        public int ExportAllowed;

        [JsonProperty("excel_export_allowed")]
        public int ExcelExportAllowed;

        [JsonProperty("email_allowed")]
        public int EmailAllowed;

        [JsonProperty("approve_allowed")]
        public int ApproveAllowed;

        [JsonProperty("find_allowed")]
        public int FindAllowed;

        [JsonProperty("cancellation_allowed")]
        public int CancellationAllowed;

        [JsonProperty("actual_team_allowed")]
        public int ActualTeamAllowed;

        [JsonProperty("merge_with_allowed")]
        public int MergeWithAllowed;

        [JsonProperty("archive_allowed")]
        public int ArchiveAllowed;

        [JsonProperty("update_tag_allowed")]
        public int UpdateTagAllowed;

        [JsonProperty("diamond_rate_allowed")]
        public int DiamondRateAllowed;

        [JsonProperty("top_menu_home")]
        public int TopMenuHome;

        [JsonProperty("top_menu_transaction")]
        public int TopMenuTransaction;

        [JsonProperty("top_menu_wastage")]
        public int TopMenuWastage;

        [JsonProperty("top_menu_sale_wastage")]
        public int TopMenuSaleWastage;

        [JsonProperty("top_menu_not_allow_credit_sale")]
        public int TopMenuNotAllowCreditSale;

        [JsonProperty("top_menu_edit_stone_weight")]
        public int TopMenuEditStoneWeight;

        [JsonProperty("top_menu_allow_dynamic_ledger")]
        public int TopMenuAllowDynamicLedger;

        [JsonProperty("top_menu_allow_customer_ledger")]
        public int TopMenuAllowCustomerLedger;

        [JsonProperty("top_menu_allow_chit_collection_detail")]
        public int TopMenuAllowChitCollectionDetail;

        [JsonProperty("top_menu_allow_to_edit_date_time")]
        public int TopMenuAllowToEditDateTime;

        [JsonProperty("top_menu_enquiry")]
        public int TopMenuEnquiry;

        [JsonProperty("mobile_dashboard_enquiry")]
        public int MobileDashboardEnquiry;

        [JsonProperty("mobile_dashboard_attendance")]
        public int MobileDashboardAttendance;

        [JsonProperty("mobile_dashboard_approval")]
        public int MobileDashboardApproval;

        [JsonProperty("mobile_dashboard_current_plan")]
        public int MobileDashboardCurrentPlan;

        [JsonProperty("mobile_dashboard_dispatched")]
        public int MobileDashboardDispatched;

        [JsonProperty("top_menu_setting")]
        public int TopMenuSetting;
    }

    public class MobileTransactionDispatched
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("user_permission_id")]
        public int UserPermissionId;

        [JsonProperty("main_module_id")]
        public string MainModuleId;

        [JsonProperty("module_id")]
        public string ModuleId;

        [JsonProperty("page_id")]
        public string PageId;

        [JsonProperty("module_name")]
        public string ModuleName;

        [JsonProperty("visible")]
        public int Visible;

        [JsonProperty("enable_all")]
        public int EnableAll;

        [JsonProperty("insert_allowed")]
        public int InsertAllowed;

        [JsonProperty("update_allowed")]
        public int UpdateAllowed;

        [JsonProperty("delete_allowed")]
        public int DeleteAllowed;

        [JsonProperty("view_allowed")]
        public int ViewAllowed;

        [JsonProperty("print_allowed")]
        public int PrintAllowed;

        [JsonProperty("export_allowed")]
        public int ExportAllowed;

        [JsonProperty("excel_export_allowed")]
        public int ExcelExportAllowed;

        [JsonProperty("email_allowed")]
        public int EmailAllowed;

        [JsonProperty("approve_allowed")]
        public int ApproveAllowed;

        [JsonProperty("find_allowed")]
        public int FindAllowed;

        [JsonProperty("cancellation_allowed")]
        public int CancellationAllowed;

        [JsonProperty("actual_team_allowed")]
        public int ActualTeamAllowed;

        [JsonProperty("merge_with_allowed")]
        public int MergeWithAllowed;

        [JsonProperty("archive_allowed")]
        public int ArchiveAllowed;

        [JsonProperty("update_tag_allowed")]
        public int UpdateTagAllowed;

        [JsonProperty("diamond_rate_allowed")]
        public int DiamondRateAllowed;

        [JsonProperty("top_menu_home")]
        public int TopMenuHome;

        [JsonProperty("top_menu_transaction")]
        public int TopMenuTransaction;

        [JsonProperty("top_menu_wastage")]
        public int TopMenuWastage;

        [JsonProperty("top_menu_sale_wastage")]
        public int TopMenuSaleWastage;

        [JsonProperty("top_menu_not_allow_credit_sale")]
        public int TopMenuNotAllowCreditSale;

        [JsonProperty("top_menu_edit_stone_weight")]
        public int TopMenuEditStoneWeight;

        [JsonProperty("top_menu_allow_dynamic_ledger")]
        public int TopMenuAllowDynamicLedger;

        [JsonProperty("top_menu_allow_customer_ledger")]
        public int TopMenuAllowCustomerLedger;

        [JsonProperty("top_menu_allow_chit_collection_detail")]
        public int TopMenuAllowChitCollectionDetail;

        [JsonProperty("top_menu_allow_to_edit_date_time")]
        public int TopMenuAllowToEditDateTime;

        [JsonProperty("top_menu_enquiry")]
        public int TopMenuEnquiry;

        [JsonProperty("mobile_dashboard_enquiry")]
        public int MobileDashboardEnquiry;

        [JsonProperty("mobile_dashboard_attendance")]
        public int MobileDashboardAttendance;

        [JsonProperty("mobile_dashboard_approval")]
        public int MobileDashboardApproval;

        [JsonProperty("mobile_dashboard_current_plan")]
        public int MobileDashboardCurrentPlan;

        [JsonProperty("mobile_dashboard_dispatched")]
        public int MobileDashboardDispatched;

        [JsonProperty("top_menu_setting")]
        public int TopMenuSetting;
    }

    public class MobileTransactionAttendanceSummary
    {

        [JsonProperty("visible")]
        public int Visible { get; set; }
    }
    public class MobileTransactionDraftConfirmation
    {
     

        [JsonProperty("visible")]
        public int Visible { get; set; }

    
   


    }

    public class MobileTransactionHome
    {
        

        [JsonProperty("visible")]
        public int Visible { get; set; }






    }

    public class MobileTransactionOffersEnquiry
    {
     

        [JsonProperty("visible")]
        public int Visible { get; set; }

  
    }

    public class MobileTransactionPendingApproval
    {
        

        [JsonProperty("visible")]
        public int Visible { get; set; }


    }

    public class MobileTransactionPendingConfirmation
    {
       

        [JsonProperty("visible")]
        public int Visible { get; set; }

    }

    public class MobileTransactionProgramApproval
    {
        

        [JsonProperty("visible")]
        public int Visible { get; set; }

 
    }

    public class MobileTransactionReports
    {
       

        [JsonProperty("visible")]
        public int Visible { get; set; }

      
    }

    public class ModulePermission
    {
        [JsonProperty("transaction_offers_enquiry")]
        public TransactionOffersEnquiry TransactionOffersEnquiry { get; set; }

        [JsonProperty("transaction_draft_confirmation")]
        public TransactionDraftConfirmation TransactionDraftConfirmation { get; set; }

        [JsonProperty("transaction_pending_approval")]
        public TransactionPendingApproval TransactionPendingApproval { get; set; }

        [JsonProperty("transaction_pending_confirmation")]
        public TransactionPendingConfirmation TransactionPendingConfirmation { get; set; }

        [JsonProperty("transaction_program_approval")]
        public TransactionProgramApproval TransactionProgramApproval { get; set; }

        [JsonProperty("transaction_current_plan")]
        public TransactionCurrentPlan TransactionCurrentPlan { get; set; }

        [JsonProperty("transaction_dispatched")]
        public TransactionDispatched TransactionDispatched { get; set; }

        [JsonProperty("mobile_dashboard")]
        public MobileDashboard MobileDashboard { get; set; }

        [JsonProperty("mobile_transaction_home")]
        public MobileTransactionHome MobileTransactionHome { get; set; }

        [JsonProperty("mobile_transaction_offers_enquiry")]
        public MobileTransactionOffersEnquiry MobileTransactionOffersEnquiry { get; set; }

        [JsonProperty("mobile_transaction_draft_confirmation")]
        public MobileTransactionDraftConfirmation MobileTransactionDraftConfirmation { get; set; }
        [JsonProperty("mobile_transaction_attendance_summary")]
        public MobileTransactionDraftConfirmation MobileTransactionAttendanceSummary { get; set; }

        [JsonProperty("mobile_transaction_pending_approval")]
        public MobileTransactionPendingApproval MobileTransactionPendingApproval { get; set; }

        [JsonProperty("mobile_transaction_pending_confirmation")]
        public MobileTransactionPendingConfirmation MobileTransactionPendingConfirmation { get; set; }

        [JsonProperty("mobile_transaction_program_approval")]
        public MobileTransactionProgramApproval MobileTransactionProgramApproval { get; set; }

        [JsonProperty("mobile_transaction_current_plan")]
        public MobileTransactionCurrentPlan MobileTransactionCurrentPlan { get; set; }

        [JsonProperty("mobile_transaction_dispatched")]
        public MobileTransactionDispatched MobileTransactionDispatched { get; set; }

        [JsonProperty("mobile_transaction_reports")]
        public MobileTransactionReports MobileTransactionReports { get; set; }
    }
    public class TransactionOffersEnquiry
    {

        [JsonProperty("visible")]
        public int Visible { get; set; }

      

        [JsonProperty("insert_allowed")]
        public int InsertAllowed { get; set; }

        [JsonProperty("update_allowed")]
        public int UpdateAllowed { get; set; }

        [JsonProperty("delete_allowed")]
        public int DeleteAllowed { get; set; }

        [JsonProperty("view_allowed")]
        public int ViewAllowed { get; set; }

        [JsonProperty("approve_allowed")]
        public int ApproveAllowed { get; set; }

  
    }
    public class TransactionDraftConfirmation
    {
        

        [JsonProperty("visible")]
        public int Visible { get; set; }


        [JsonProperty("insert_allowed")]
        public int InsertAllowed { get; set; }

        [JsonProperty("update_allowed")]
        public int UpdateAllowed { get; set; }

        [JsonProperty("delete_allowed")]
        public int DeleteAllowed { get; set; }

        [JsonProperty("view_allowed")]
        public int ViewAllowed { get; set; }

 

        [JsonProperty("approve_allowed")]
        public int ApproveAllowed { get; set; }

        [JsonProperty("find_allowed")]
        public int FindAllowed { get; set; }

    }
    public class TransactionPendingApproval
    {
       

        [JsonProperty("visible")]
        public int Visible { get; set; }


        [JsonProperty("insert_allowed")]
        public int InsertAllowed { get; set; }

        [JsonProperty("update_allowed")]
        public int UpdateAllowed { get; set; }

        [JsonProperty("delete_allowed")]
        public int DeleteAllowed { get; set; }

        [JsonProperty("view_allowed")]
        public int ViewAllowed { get; set; }

        [JsonProperty("approve_allowed")]
        public int ApproveAllowed { get; set; }


    }
    public class TransactionPendingConfirmation
    {
        
        [JsonProperty("visible")]
        public int Visible { get; set; }

        [JsonProperty("insert_allowed")]
        public int InsertAllowed { get; set; }

        [JsonProperty("update_allowed")]
        public int UpdateAllowed { get; set; }

        [JsonProperty("delete_allowed")]
        public int DeleteAllowed { get; set; }

        [JsonProperty("view_allowed")]
        public int ViewAllowed { get; set; }

       

        [JsonProperty("approve_allowed")]
        public int ApproveAllowed { get; set; }

        [JsonProperty("find_allowed")]
        public int FindAllowed { get; set; }

    }
    public class TransactionProgramApproval
    {
       

        [JsonProperty("visible")]
        public int Visible { get; set; }


        [JsonProperty("insert_allowed")]
        public int InsertAllowed { get; set; }

        [JsonProperty("update_allowed")]
        public int UpdateAllowed { get; set; }

        [JsonProperty("delete_allowed")]
        public int DeleteAllowed { get; set; }

        [JsonProperty("view_allowed")]
        public int ViewAllowed { get; set; }


        [JsonProperty("approve_allowed")]
        public int ApproveAllowed { get; set; }

        [JsonProperty("find_allowed")]
        public int FindAllowed { get; set; }

       
    }
    public class TransactionCurrentPlan
    {
      

        [JsonProperty("visible")]
        public int Visible { get; set; }

     

        [JsonProperty("insert_allowed")]
        public int InsertAllowed { get; set; }

        [JsonProperty("update_allowed")]
        public int UpdateAllowed { get; set; }

        [JsonProperty("delete_allowed")]
        public int DeleteAllowed { get; set; }

        [JsonProperty("view_allowed")]
        public int ViewAllowed { get; set; }

    
        [JsonProperty("approve_allowed")]
        public int ApproveAllowed { get; set; }

        [JsonProperty("find_allowed")]
        public int FindAllowed { get; set; }

        
    }

    public class TransactionDispatched
    {

        [JsonProperty("visible")]
        public int Visible { get; set; }

        [JsonProperty("insert_allowed")]
        public int InsertAllowed { get; set; }

        [JsonProperty("update_allowed")]
        public int UpdateAllowed { get; set; }

        [JsonProperty("delete_allowed")]
        public int DeleteAllowed { get; set; }

        [JsonProperty("view_allowed")]
        public int ViewAllowed { get; set; }


        [JsonProperty("approve_allowed")]
        public int ApproveAllowed { get; set; }

        [JsonProperty("find_allowed")]
        public int FindAllowed { get; set; }

 
    }
   

  
}