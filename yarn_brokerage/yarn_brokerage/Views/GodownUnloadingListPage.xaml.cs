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
	public partial class GodownUnloadingListPage : ContentPage
	{
        GodownUnloadingViewModel viewModel;
        public Dispatch Dispatch { get; set; }
        public DraftConfirmationDispatchDelivery DraftConfirmationDispatchDelivery { get; set; }
        public SearchConfirmationFilter SearchFilter { get; set; }
        public GodownUnloadingListPage (DraftConfirmationDispatchDelivery draftConfirmationDispatchDelivery )
		{
			InitializeComponent ();
            DraftConfirmationDispatchDelivery = draftConfirmationDispatchDelivery;          
            txtSearch.Focus();
            BindingContext = viewModel = new GodownUnloadingViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as GodownUnloading;
            GodownUnloadingListView.SelectedItem = null;
            if (item == null)
                return;
            if (DraftConfirmationDispatchDelivery != null)
            {
                DraftConfirmationDispatchDelivery.godown_unloading_id = item.Id;
                DraftConfirmationDispatchDelivery.godown_unloading_name = item.Name;
            }            
            await Navigation.PopAsync();            
        }
        async void AddGodownUnloading_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new NavigationPage(new AddGodownUnloadingPage(null,1)));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadGodownUnloadingsCommand.Execute(null);
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.LoadGodownUnloadingsCommand.Execute(txtSearch.Text);
        }
    }
}