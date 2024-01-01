using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using yarn_brokerage.ViewModels;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Pages;
using yarn_brokerage.Models;

namespace yarn_brokerage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ReceiptAmountPopupView : PopupPage
    {
       //PeriodsViewModel viewModel;
        public CommissionReceiptDetail CommissionReceiptDetail { get; set; }
        
        public ReceiptAmountPopupView(CommissionReceiptDetail commissionReceiptDetail)
		{
			InitializeComponent ();
            CommissionReceiptDetail = commissionReceiptDetail;
            BindingContext = this; // viewModel = new PeriodsViewModel();
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }

        private void txtReceiptAmount_Focused(object sender, FocusEventArgs e)
        {
            if (txtReceiptAmount.Text.Trim() != "")
            {
                txtReceiptAmount.Text = string.Format("{0:0.00}", Convert.ToDouble(txtReceiptAmount.Text));
                if (Convert.ToDouble(txtReceiptAmount.Text) == 0)
                {
                    txtReceiptAmount.Text = "";                    
                }
            }
            else
            {
                
            }
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }
    }
}