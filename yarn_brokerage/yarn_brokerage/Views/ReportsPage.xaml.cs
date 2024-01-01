using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace yarn_brokerage.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ReportsPage : ContentPage
	{
		public ReportsPage ()
		{
			InitializeComponent ();
		}

		private async void ConfirmationReport_Tapped(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ConfirmationReportPage(null));
		}

		private void CommissionInvoiceReport_Tapped(object sender, EventArgs e)
		{
			Navigation.PushAsync(new CommissionInvoiceReportPage());
		}

        public bool CloseApp = true;
        protected override bool OnBackButtonPressed()
        {
            if (Navigation.NavigationStack.Count <= 1)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    bool result = await this.DisplayAlert("Attention!", "Do you want to exit?", "Yes", "No");
                    if (result)
                    {
                        CloseApp = false;
                        Process.GetCurrentProcess().CloseMainWindow();
                        Process.GetCurrentProcess().Close();
                    }

                });
            }
            else
            {
                CloseApp = false;
            }
            return CloseApp;
        }
    }
}