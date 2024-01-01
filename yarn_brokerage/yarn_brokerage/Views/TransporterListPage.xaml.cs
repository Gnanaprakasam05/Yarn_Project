using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using yarn_brokerage.Models;
using yarn_brokerage.Views;
using yarn_brokerage.ViewModels;

namespace yarn_brokerage.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TransporterListPage : ContentPage
	{
        TransporterViewModel viewModel;
        public Dispatch Dispatch { get; set; }
        public DraftConfirmationDispatchDelivery DraftConfirmationDispatchDelivery { get; set; }
        public SearchConfirmationFilter SearchFilter { get; set; }
        public TransporterListPage (DraftConfirmationDispatchDelivery draftConfirmationDispatchDelivery )
		{
			InitializeComponent ();
            DraftConfirmationDispatchDelivery = draftConfirmationDispatchDelivery;          
            txtSearch.Focus();
            BindingContext = viewModel = new TransporterViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Transporter;
            TransporterListView.SelectedItem = null;
            if (item == null)
                return;
            if (DraftConfirmationDispatchDelivery != null)
            {
                DraftConfirmationDispatchDelivery.transporter_id = item.Id;
                DraftConfirmationDispatchDelivery.transporter_name = item.Name;
            }            
            await Navigation.PopAsync();            
        }
        async void AddTransporter_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddTransporterPage(null,1)));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadTransportersCommand.Execute(null);
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.LoadTransportersCommand.Execute(txtSearch.Text);
        }
    }
}