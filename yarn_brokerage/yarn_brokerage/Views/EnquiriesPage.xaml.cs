﻿using System;
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
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace yarn_brokerage.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class EnquiriesPage : ContentPage
    {
        EnquiriesViewModel viewModel;
        public int _transaction_type;

        //public bool AddFlag { get; set; }
        public Indexes Indexes { get; set; }
        public SearchFilter _searchFilter { get; set; }


        public EnquiriesPage(SearchFilter searchFilter = null)
        {
            InitializeComponent();

            if (Application.Current.Properties["transaction_offers_enquiry_InsertAllowed"].ToString() == "0")
                addVisible.IsEnabled = false;


            _searchFilter = searchFilter;

            Indexes = new Indexes();

            if (_searchFilter != null)
            {
                grdEnquiry.IsVisible = false;
                grdEnquiryList.IsVisible = true;
                if (_searchFilter.transaction_type == 2)
                    Title = "Enquiry List";
                else
                    Title = "Offer List";
            }
            //_transaction_type = transaction_type;
            //startDatePicker.Date =
            BindingContext = viewModel = new EnquiriesViewModel(searchFilter);


        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Indexes;
            EnquiryListView.SelectedItem = null;
            if (item == null)
                return;

            bool Edit_Flag = true;
            await Navigation.PushAsync(new AddEnquiryPage(item, startDatePicker.Date, null, Indexes, Edit_Flag));

            // Manually deselect item.

        }

        async void OnDetailListItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Indexes;
            EnquiryDetailListView.SelectedItem = null;
            if (item == null)
                return;

            await Navigation.PushAsync(new AddEnquiryPage(item, startDatePicker.Date, _searchFilter));
            // Manually deselect item.
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            if (_searchFilter != null)
            {
                if (_searchFilter.ledger_id > 0 && _searchFilter.exact_ledger_id > 0 && viewModel.EnquiryCount() > 0)
                {
                    string ledger_type = (_searchFilter.transaction_type == 2) ? " supplier" : " customer";
                    await DisplayAlert("Yarn Brokerage", "This count already exists for this" + ledger_type, "Cancel");
                    return;
                }
            }
            await Navigation.PushAsync(new AddEnquiryPage(null, startDatePicker.Date, _searchFilter, Indexes, false));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if(Indexes.Add_Flag != false)
            //  {
            //      viewModel.AddStoreItemsCommand(Indexes);
            //      Indexes.Add_Flag = false;
            //  }else if (Indexes.Edit_Flag != false)
            //  {
            //      viewModel.EditStoreItemsCommand(Indexes);


            //      Indexes.Edit_Flag = false;
            //  }
            //  else
            //  {

            if (Indexes.Edit_Flag != false)
            {
                viewModel.EditStoreItemsCommand(Indexes);
                Indexes.Edit_Flag = false;
            }
            else if (_searchFilter != null)
            {
                if (_searchFilter.search_flag == 1)
                {
                    //lblFilter.Text = "Result";
                    butClear.IsVisible = true;
                    lblClear.IsVisible = true;
                    startDatePicker.Date = _searchFilter.transaction_date_time;
                    viewModel.SearchItemsCommand.Execute(_searchFilter);
                    _searchFilter.search_flag = 0;
                    _searchFilter.filter_flag = 0;
                }
            }
            else
            {

                if (Indexes.CancelClick != 1)
                {
                    viewModel.LoadItemsCommand.Execute(startDatePicker.Date);
                    Indexes.CancelClick = 1;
                }

            }
        }


        public bool CloseApp = true;
        protected override bool OnBackButtonPressed()
        {
            if (Navigation.NavigationStack.Count <= 1)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    bool result = await this.DisplayAlert("Attention!", "Do you want to exit?", "Yes", "No");
                    if (result)
                    {
                        CloseApp = false;
                        Process.GetCurrentProcess().CloseMainWindow();
                        Process.GetCurrentProcess().Close();
                    }

                });
            }
            else
            {
                CloseApp = false;
            }
            return CloseApp;
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
            ButClear_Clicked(null, null);
        }

        private void NextDate_Clicked(object sender, EventArgs e)
        {
            startDatePicker.Date = startDatePicker.Date.AddDays(1);
            ButClear_Clicked(null, null);
        }

        private async void lblFilter_Tapped(object sender, EventArgs e)
        {
            //if (lblFilter.Text == "Filter")
            //{                

            //butFilter.IsVisible = false;
            if (_searchFilter == null)
            {
                _searchFilter = new SearchFilter();
                _searchFilter.transaction_date_time = startDatePicker.Date;
            }
            await Navigation.PushModalAsync(new NavigationPage(new FilterEnquiryPage(_searchFilter)));
            //}
        }

        private void ButClear_Clicked(object sender, EventArgs e)
        {

            lblFilter.Text = "Filter";
            butClear.IsVisible = false;
            lblClear.IsVisible = false;
            butFilter.IsVisible = true;
            _searchFilter = null;
            //startDatePicker.Date = DateTime.Now.ToLocalTime();
            viewModel.LoadItemsCommand.Execute(startDatePicker.Date);
        }
    }
}