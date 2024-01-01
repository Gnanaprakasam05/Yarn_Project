using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using yarn_brokerage.Models;
using yarn_brokerage.ViewModels;
namespace yarn_brokerage.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AddDispatchPage : ContentPage
    {
        public Dispatch Dispatch { get; set; }
        
        DispatchViewModel viewModel;
        public DateTime date { get; set; }
        public SearchConfirmationFilter _searchFilter { get; set; }        
        //public Indexes _enquiry { get; set; }
        public AddDispatchPage(Dispatch dispatch)
        {
            InitializeComponent();            
            viewModel = new DispatchViewModel();            
            Dispatch = dispatch;
            Title = "Dispatch";


            BindingContext = this;
            if (Convert.ToDouble(txtAmount.Text) == 0)
                txtAmount.Text = "";
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Dispatch.lr_number))
            {
                await DisplayAlert("Alert", "Enter the LR Number...", "OK");
                return;
            }
            else if (string.IsNullOrWhiteSpace(Dispatch.invoice_number))
            {
                await DisplayAlert("Alert", "Enter the Invoice Number...", "OK");
                return;
            }
            Dispatch.dispatch_status = 1;
            Dispatch _Dispatch = await viewModel.StoreDispatchCommand(Dispatch);
            await Task.Delay(200);
            await Navigation.PopAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {        
            await Navigation.PopAsync();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            txtTransporterName.Text = Dispatch.transporter_name;
        }
        
        private void TxtAmount_Focused(object sender, FocusEventArgs e)
        {
            if (txtAmount.Text.Trim() != "")
            {                
                if (Convert.ToDouble(txtAmount.Text) == 0)
                {
                    txtAmount.Text = "";
                    Dispatch.price = 0;
                }
            }
            else
            {
                Dispatch.price = 0;
            }
        }

        private void TxtTransporter_Focused(object sender, EventArgs e)
        {
            if (txtTransporterName.IsEnabled == false)
                return;
           // await Navigation.PushAsync(new TransporterListPage(Dispatch));
        }
    }
}