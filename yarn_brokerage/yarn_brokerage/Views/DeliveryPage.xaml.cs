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
    public partial class DeliveryPage : ContentPage
    {
        DeliveryViewModel viewModel;
        //public SearchFilter _searchFilter { get; set; }

        public DeliveryPage()
        {
            InitializeComponent();
            //_searchFilter = searchFilter;
            BindingContext = viewModel = new DeliveryViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Delivery;
            // Manually deselect item.
            DeliveryListView.SelectedItem = null;
            if (item == null)
                return;

            await Navigation.PushAsync(new AddDeliveryPage(item));

            
        }
                

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (_searchFilter != null)
            //{
            //    if (_searchFilter.search_flag == 1)
            //    {
            //        //lblFilter.Text = "Result";
            //        //butClear.IsVisible = true;
            //        //lblClear.IsVisible = true;
            //        //startDatePicker.Date = _searchFilter.transaction_date_time;
            //        viewModel.SearchItemsCommand.Execute(_searchFilter);
            //        _searchFilter.search_flag = 0;
            //    }
            //}
            //else
            // if (viewModel.Delivery.Count == 0)
            viewModel.LoadItemsCommand.Execute(null);
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        //private void StartDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        //{
        //    viewModel.LoadItemsCommand.Execute(startDatePicker.Date);
        //}
        
        //private void PreviousDate_Clicked(object sender, EventArgs e)
        //{
        //    startDatePicker.Date = startDatePicker.Date.AddDays(-1);
        //}

        //private void NextDate_Clicked(object sender, EventArgs e)
        //{
        //    startDatePicker.Date = startDatePicker.Date.AddDays(1);
        //}

        //private async void lblFilter_Tapped(object sender, EventArgs e)
        //{
        //    if (_searchFilter == null)
        //    {
        //        _searchFilter = new SearchFilter();
        //        _searchFilter.transaction_date_time = startDatePicker.Date;
        //    }
        //    await Navigation.PushModalAsync(new NavigationPage(new FilterEnquiryPage(_searchFilter,1)));
        //}

        //private void ButClear_Clicked(object sender, EventArgs e)
        //{
        //    lblFilter.Text = "Filter";
        //    butClear.IsVisible = false;
        //    lblClear.IsVisible = false;
        //    butFilter.IsVisible = true;
        //    _searchFilter = null;
        //    //startDatePicker.Date = DateTime.Now.ToLocalTime();
        //    viewModel.LoadItemsCommand.Execute(startDatePicker.Date);
        //}
    }
}