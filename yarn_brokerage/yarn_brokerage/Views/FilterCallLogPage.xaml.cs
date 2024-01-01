using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using yarn_brokerage.Models;
using yarn_brokerage.ViewModels;
namespace yarn_brokerage.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class FilterCallLogPage : ContentPage
    {
        public SearchCallLogFilter SearchCallLogFilter { get; set; }
        public int _transaction_type;
        CallLogViewModel viewModel;
        
        public FilterCallLogPage(SearchCallLogFilter searchCallLogFilter)
        {
            InitializeComponent();
            //_transaction_type = transaction_type;
            viewModel = new CallLogViewModel();
            Title = "Filter For Call History";

            if (Application.Current.Properties["user_type"].ToString() != "1")
            {
                lblUser.IsVisible = false;
                txtUsername.IsVisible = false;
                butUserClear.IsVisible = false;
            }

            if (searchCallLogFilter == null)
            {
                SearchCallLogFilter = new SearchCallLogFilter();
                SearchCallLogFilter.call_date_from = DateTime.Now.ToLocalTime();
                SearchCallLogFilter.call_date_to = DateTime.Now.ToLocalTime();
            }
            else
            {
                SearchCallLogFilter = searchCallLogFilter;                
                if (SearchCallLogFilter.call_date == 1) { RdoConfirm.IsChecked = true; CallLog_Clicked(null, null); }                
            }
           
            BindingContext = this;          
        }

       
        private async void butFilter_Clicked(object sender, EventArgs e)
        {            
                SearchCallLogFilter.search_flag = 1;
                SearchCallLogFilter.filter_flag = 1;
                await Navigation.PopModalAsync();            
        }
        
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();            
            txtUsername.Text = SearchCallLogFilter.user_name;
        }
                
        private async void TxtUsername_Focused(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new UserListPage(null,null,null,null,null,SearchCallLogFilter)));
        }

        private void txtFDateTime_DateSelected(object sender, DateChangedEventArgs e)
        {
            SearchCallLogFilter.call_date_from = txtFDateTime.Date;
        }
        private void txtTDateTime_DateSelected(object sender, DateChangedEventArgs e)
        {
            SearchCallLogFilter.call_date_to = txtTDateTime.Date;
        }
        
        private void ButUserClear_Clicked(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            SearchCallLogFilter.user_id = 0;
            SearchCallLogFilter.user_name = "";
        }

        private void CallLog_Clicked(object sender, EventArgs e)
        {
            if (RdoConfirm.IsChecked == true)
            {
                SearchCallLogFilter.call_date = 1;
                txtFDateTime.IsEnabled = true;
                txtTDateTime.IsEnabled = true;
            }
            else
            {
                SearchCallLogFilter.call_date = 0;
                txtFDateTime.IsEnabled = false;
                txtTDateTime.IsEnabled = false;
            }
        }
    }
}