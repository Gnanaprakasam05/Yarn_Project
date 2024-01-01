using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using yarn_brokerage.Models;
using yarn_brokerage.ViewModels;
namespace yarn_brokerage.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddLedgerPage : ContentPage
	{
        public Ledger Ledger { get; set; }
        private int _ledger_type = 0;
        LedgersViewModel viewModel;
        public AddLedgerPage(Ledger _Ledger, int ledger_type)
		{
			InitializeComponent ();
            _ledger_type = ledger_type;
            viewModel = new LedgersViewModel(ledger_type);
            if (Ledger == null)
            {
                Ledger = new Ledger();
                if (ledger_type == 1) 
                    Title = "Add Supplier";
                else
                    Title = "Add Customer";
            }
            else
            {
                Ledger = _Ledger;
                if (ledger_type == 1)
                    Title = "Edit Supplier";
                else
                    Title = "Edit Customer";
            }
            Ledger.LedgerType = _ledger_type;
            BindingContext = this;
        }
        async void Save_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Ledger.Name))
            {
                await DisplayAlert("Alert", "Enter a Ledger Name", "OK");
            }
            else if (!string.IsNullOrWhiteSpace(Ledger.Name))
            {
                //MessagingCenter.Send(this, "AddLedger", Ledger);
                Ledger.Name = Ledger.Name.Trim();
                viewModel.StoreLedgerCommand(Ledger);
                //string message = await App.Database.SaveLedgerAsync(Ledger);
                await Task.Delay(200);
                await Navigation.PopModalAsync();
                //if (message == "Duplicate")
                //{
                //    await DisplayAlert("Alert", "Problem already exists", "OK");
                //}
                //else
                //{
                //    if (Title == "Edit Problem" || _dynamicflag == 1) { await Navigation.PopModalAsync(); }
                //    else
                //    {
                //        await Task.Delay(100);
                //        Ledger = new Ledger();
                //        TxtLedgerName.Text = "";
                //        TxtLedgerName.Focus();
                //    }
                //}
            }
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(600);
            TxtLedgerName.Focus();
        }
    }
}