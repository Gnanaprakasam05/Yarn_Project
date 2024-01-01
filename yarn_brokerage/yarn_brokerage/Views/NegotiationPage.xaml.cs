using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NegotiationPage : ContentPage
    {
        NegotiationViewModel viewModel;
        
        public NegotiationPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new NegotiationViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Negotiation;
            NegotiationListView.SelectedItem = null;
            if (item == null)
                return;
            await Navigation.PushModalAsync(new NavigationPage(new AddNegotiationPage(item)));
            // Manually deselect item.
            
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddNegotiationPage(null)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

           // if (viewModel.Negotiation.Count == 0)
                viewModel.LoadItemsCommand.Execute(startDatePicker.Date);
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void StartDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            viewModel.LoadItemsCommand.Execute(startDatePicker.Date);
        }

        private void PreviousDate_Clicked(object sender, EventArgs e)
        {
            startDatePicker.Date = startDatePicker.Date.AddDays(-1);
        }

        private void NextDate_Clicked(object sender, EventArgs e)
        {
            startDatePicker.Date = startDatePicker.Date.AddDays(1);
        }
    }
}