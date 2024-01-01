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
	public partial class AddCountPage : ContentPage
	{
        public Count Count { get; set; }
        private int _dynamicflag = 0;
        CountViewModel viewModel;
        public AddCountPage(Count _Count, int dynamicflag = 0)
		{
			InitializeComponent ();
            _dynamicflag = dynamicflag;
            viewModel = new CountViewModel();
            if (Count == null)
                Count = new Count();
            else
            {
                Count = _Count;
                Title = "Edit Count";
            }            
            BindingContext = this;
        }
        async void Save_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Count.Name))
            {
                await DisplayAlert("Alert", "Enter a Count Name", "OK");
            }
            else if (!string.IsNullOrWhiteSpace(Count.Name))
            {
                //MessagingCenter.Send(this, "AddCount", Count);
                Count.Name = Count.Name.Trim();
                viewModel.StoreCountCommand(Count);
                //string message = await App.Database.SaveCountAsync(Count);
                await Task.Delay(200);
                
                await Navigation.PopModalAsync();
                //if (message == "Duplicate")
                //{
                //    await DisplayAlert("Alert", "Count already exists", "OK");
                //}
                //else
                //{
                //    if (Title == "Edit Count" || _dynamicflag == 1) { await Navigation.PopModalAsync(); }
                //    else
                //    {
                //        await Task.Delay(100);
                //        Count = new Count();
                //        TxtCountName.Text = "";
                //        TxtCountName.Focus();
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
            TxtCountName.Focus();            
        }
    }
}