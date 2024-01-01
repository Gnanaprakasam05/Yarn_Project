using System;
using System.Collections.Generic;
using System.Text;

namespace yarn_brokerage.Models
{
    public enum MenuItemType
    {
        Home,
        OffersEnquiries,
        DraftConfirmation,
        PendingApproval,
        PendingConfirmation,
        ProgramApproval,
        CurrentPlan,
        Dispatched,
        CommissionInvoice,
        CommissionReceipt,
        CallHistory,
        Reports,
        Logout
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
