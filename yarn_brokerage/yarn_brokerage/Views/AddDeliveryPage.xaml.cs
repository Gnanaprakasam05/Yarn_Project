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
    public partial class AddDeliveryPage : ContentPage
    {
        public Delivery Delivery { get; set; }
        
        DeliveryViewModel viewModel;
        public DateTime date { get; set; }
        public SearchConfirmationFilter _searchFilter { get; set; }        
        //public Indexes _enquiry { get; set; }
        public AddDeliveryPage(Delivery delivery)
        {
            InitializeComponent();            
            viewModel = new DeliveryViewModel();            
            Delivery = delivery;
            Title = "Delivery";


            BindingContext = this;
            
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(Delivery.delivery_remarks))
            //{
            //    await DisplayAlert("Alert", "Enter the Remarks...", "OK");
            //    return;
            //}            
            Delivery.delivery_status = 1;
            Delivery _Delivery = await viewModel.StoreDeliveryCommand(Delivery);
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
        }        
    }
}