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
    public partial class LedgerTeamNamePage : ContentPage
    {
        public LedgerTeamNameViewModel viewModel;

        public int transaction_type = 0;
        public Approval Approval_Data { get; set; }
        public SearchDispatchConfirmationFilter SearchDispatchConfirmationFilter { get; set; }
        public LedgerTeamNamePage(int _transaction_type = 1, Approval Approval = null, SearchDispatchConfirmationFilter searchDispatchConfirmationFilter = null)
        {
            InitializeComponent();
            transaction_type = _transaction_type;

            Approval_Data = Approval;

            if (searchDispatchConfirmationFilter != null)
            {
                SearchDispatchConfirmationFilter = searchDispatchConfirmationFilter;
            }
            else
            {
                SearchDispatchConfirmationFilter = new SearchDispatchConfirmationFilter();
            }

            BindingContext = viewModel = new LedgerTeamNameViewModel(_transaction_type);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            txtSearch.Focus();

            if (transaction_type == 3)
            {
                viewModel.ExecuteLoadLedgersGroupCommand("");
            }
            else
            {
                viewModel.LoadLedgersCommand.Execute("");
            }
        }
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (transaction_type == 3)
            {
                viewModel.ExecuteLoadLedgersGroupCommand(txtSearch.Text);
            }
            else
            {
                viewModel.LoadLedgersCommand.Execute(txtSearch.Text);
            }

        }

        private async void LedgersListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as LedgerTeamName;
            LedgersListView.SelectedItem = null;
            if (item == null)
                return;
            if (Approval_Data != null)
            {
                if (transaction_type == 1)
                {
                    Approval_Data.SupplierConfirmedId = Convert.ToInt32(item.Id);
                    Approval_Data.SupplierTeamName = item.Name;
                }

                if (transaction_type == 2)
                {
                    Approval_Data.CustomerConfirmedId = Convert.ToInt32(item.Id);
                    Approval_Data.CustomerTeamName = item.Name;
                }

                if (transaction_type == 5)
                {
                    Approval_Data.AttendanceTeamNameId = Convert.ToInt32(item.Id);
                    Approval_Data.AttendanceTeamName = item.Name;
                }


            }

            if (SearchDispatchConfirmationFilter != null)
            {
                if (transaction_type != 3)
                {
                    SearchDispatchConfirmationFilter.TeamId = Convert.ToInt32(item.Id);
                    SearchDispatchConfirmationFilter.TeamName = item.Name;

                    SearchDispatchConfirmationFilter.TeamGroupId = 0;
                    SearchDispatchConfirmationFilter.TeamGroupName = null;
                }
                else
                {
                    SearchDispatchConfirmationFilter.TeamGroupId = Convert.ToInt32(item.Id);
                    SearchDispatchConfirmationFilter.TeamGroupName = item.Name;

                    SearchDispatchConfirmationFilter.TeamId = 0;
                    SearchDispatchConfirmationFilter.TeamName = null;
                }
            }

            await Navigation.PopAsync();
        }
    }
}