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
    public partial class AddDispatchConfirmPage : ContentPage
    {
        public DispatchConfirm DispatchConfirm { get; set; }
        
        DispatchConfirmViewModel viewModel;
        public DateTime date { get; set; }
        public SearchConfirmationFilter _searchFilter { get; set; }        
        //public Indexes _enquiry { get; set; }
        public AddDispatchConfirmPage(DispatchConfirm dispatchConfirm)
        {
            InitializeComponent();            
            viewModel = new DispatchConfirmViewModel();            
            DispatchConfirm = dispatchConfirm;
            Title = "Dispatch Confirmation";


            BindingContext = this;
            
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            DispatchConfirm.dispatch_confirm_status = 1;
            DispatchConfirm _DispatchConfirm = await viewModel.StoreDispatchConfirmCommand(DispatchConfirm);
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