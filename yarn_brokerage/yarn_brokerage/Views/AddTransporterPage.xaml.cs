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
	public partial class AddTransporterPage : ContentPage
	{
        public Transporter Transporter { get; set; }
        private int _dynamicflag = 0;
        TransporterViewModel viewModel;
        public AddTransporterPage(Transporter _Transporter, int dynamicflag = 0)
		{
			InitializeComponent ();
            _dynamicflag = dynamicflag;
            viewModel = new TransporterViewModel();
            if (Transporter == null)
                Transporter = new Transporter();
            else
            {
                Transporter = _Transporter;
                Title = "Edit Transporter";
            }            
            BindingContext = this;
        }
        async void Save_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Transporter.Name))
            {
                await DisplayAlert("Alert", "Enter a Transporter Name", "OK");
            }
            else if (!string.IsNullOrWhiteSpace(Transporter.Name))
            {
                //MessagingCenter.Send(this, "AddTransporter", Transporter);
                Transporter.Name = Transporter.Name.Trim();
                viewModel.StoreTransporterCommand(Transporter);
                //string message = await App.Database.SaveTransporterAsync(Transporter);
                await Task.Delay(200);
                
                await Navigation.PopModalAsync();
                //if (message == "Duplicate")
                //{
                //    await DisplayAlert("Alert", "Transporter already exists", "OK");
                //}
                //else
                //{
                //    if (Title == "Edit Transporter" || _dynamicflag == 1) { await Navigation.PopModalAsync(); }
                //    else
                //    {
                //        await Task.Delay(100);
                //        Transporter = new Transporter();
                //        TxtTransporterName.Text = "";
                //        TxtTransporterName.Focus();
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
            TxtTransporterName.Focus();            
        }
    }
}