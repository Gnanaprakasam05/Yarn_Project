using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
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
    public partial class LogPage : ContentPage
    {
        public DraftConfirmation DraftConfirmation { get; set; }
        
        ApprovalViewModel viewModel;
        public DateTime date { get; set; }
        public SearchConfirmationFilter _searchFilter { get; set; }        
        //public Indexes _enquiry { get; set; }
        public LogPage(DraftConfirmation draftConfirmation)
        {
            InitializeComponent();            
            //viewModel = new ApprovalViewModel();
            DraftConfirmation = draftConfirmation;
            if (DraftConfirmation.rejected_user == null)
                DraftConfirmation.rejected_user = "";
            if (DraftConfirmation.approved_user == null)
                DraftConfirmation.approved_user = "";
            Title = "Log";
            BindingContext = this;
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();            
        }
    }
}