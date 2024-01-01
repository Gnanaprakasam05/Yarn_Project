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
    public partial class TeamGroupPerformancePage : ContentPage
    {
        PerformanceViewModel viewModel;

        Performance Performance { get; set; }
        Performance TeamGroupPerformance { get; set; }

        private int TeamGroupid;
        public TeamGroupPerformancePage(int teamGroupid, Performance performance)
        {
            InitializeComponent();

            Performance = performance;

            TeamGroupPerformance = Performance ;

            TeamGroupid = teamGroupid;


            BindingContext = viewModel = new PerformanceViewModel();


        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();


            if (Performance.TeamBackCheckBack == 0)
            {
                viewModel.ExecuteLoadTeamPerformanceDataCommand(TeamGroupid);

                TeamGroupPerformance = await viewModel.ExecuteTeamPerformanceCommand(TeamGroupid);

                Performance.PerformanceBackCheckBack = 1;
            }

            lbltodayconfirmed.Text = int.Parse(TeamGroupPerformance.ConfirmedBags.TodayBags).ToString("N0");
            lblmonthconfirmed.Text = int.Parse(TeamGroupPerformance.ConfirmedBags.CurrentMonthBags).ToString("N0");
            lblaverageconfirmed.Text = int.Parse(TeamGroupPerformance.ConfirmedBags.AverageBags).ToString("N0");
            lblexpectedconfirmed.Text = int.Parse(TeamGroupPerformance.ConfirmedBags.ExpectedAverageBags).ToString("N0");

            lbltodaydispatched.Text = int.Parse(TeamGroupPerformance.DispatchedBags.TodayBags).ToString("N0");
            lblmonthdispatched.Text = int.Parse(TeamGroupPerformance.DispatchedBags.CurrentMonthBags).ToString("N0");
            lblaveragedispatched.Text = int.Parse(TeamGroupPerformance.DispatchedBags.AverageBags).ToString("N0");
            lblexpecteddispatched.Text = int.Parse(TeamGroupPerformance.DispatchedBags.ExpectedAverageBags).ToString("N0");

            lbltodaycommissionconfirmed.Text = int.Parse(TeamGroupPerformance.ConfirmedCommission.TodayCommission).ToString("N0");
            lblmonthcommissionconfirmed.Text = int.Parse(TeamGroupPerformance.ConfirmedCommission.CurrentMonthCommission).ToString("N0");
            lblaveragecommissionconfirmed.Text = int.Parse(TeamGroupPerformance.ConfirmedCommission.AverageCommission).ToString("N0");
            lblexpectedcommissionconfirmed.Text = int.Parse(TeamGroupPerformance.ConfirmedCommission.ExpectedAverageCommission).ToString("N0");

            lbltodaycommissiondispatched.Text = int.Parse(TeamGroupPerformance.DispatchedCommission.TodayCommission).ToString("N0");
            lblmonthcommissiondispatched.Text = int.Parse(TeamGroupPerformance.DispatchedCommission.CurrentMonthCommission).ToString("N0");
            lblaveragecommissiondispatched.Text = int.Parse(TeamGroupPerformance.DispatchedCommission.AverageCommission).ToString("N0");
            lblexpectedcommissiondispatched.Text = int.Parse(TeamGroupPerformance.DispatchedCommission.ExpectedAverageCommission).ToString("N0");

            lbltodaycommissionreceived.Text = int.Parse(TeamGroupPerformance.ActualCommission[0].TodayCommissionReceived).ToString("N0");
            lblmonthcommissionreceived.Text = int.Parse(TeamGroupPerformance.ActualCommission[0].CurrentMonthCommissionReceived).ToString("N0");
            lblaveragecommissionreceived.Text = int.Parse(TeamGroupPerformance.ActualCommission[0].Average).ToString("N0");
            lblexpectedcommissionreceived.Text = int.Parse(TeamGroupPerformance.ActualCommission[0].ExpectedAverageCommissionReceived).ToString("N0");


            lblThisYearBagsConfirmed.Text = int.Parse(TeamGroupPerformance.ConfirmedBags.Total).ToString("N0");
            lblAverageMonthBagsConfirmed.Text = int.Parse(TeamGroupPerformance.ConfirmedBags.AverageYear).ToString("N0");
            lblPercentageBagsConfirmed.Text = TeamGroupPerformance.ConfirmedBags.AveragePercentage.ToString() + "%";

            lblThisYearBagsDispatched.Text = int.Parse(TeamGroupPerformance.DispatchedBags.Total).ToString("N0");
            lblAverageMonthBagDispatched.Text = int.Parse(TeamGroupPerformance.DispatchedBags.AverageYear).ToString("N0");
            lblPercentageBagsDispatched.Text = TeamGroupPerformance.DispatchedBags.AveragePercentage.ToString() + "%";

            lblThisYearCommissionConfirmed.Text = int.Parse(TeamGroupPerformance.ConfirmedCommission.Total).ToString("N0");
            lblAverageCommissionConfirmed.Text = int.Parse(TeamGroupPerformance.ConfirmedCommission.AverageYear).ToString("N0");
            lblPerformanceCommissionConfirmed.Text = TeamGroupPerformance.ConfirmedCommission.AverageCommissionPercentage.ToString() + "%";

            lblThisYearCommissionDispatched.Text = int.Parse(TeamGroupPerformance.DispatchedCommission.Total).ToString("N0");
            lblAverageCommissionDispatched.Text = int.Parse(TeamGroupPerformance.DispatchedCommission.AverageYear).ToString("N0");
            lblPerformanceCommissionDispatched.Text = TeamGroupPerformance.DispatchedCommission.AverageCommissionPercentage.ToString() + "%";

            lblThisYearCommissionCollected.Text = int.Parse(TeamGroupPerformance.ActualCommission[0].Total).ToString("N0");
            lblAverageCommissionCollected.Text = int.Parse(TeamGroupPerformance.ActualCommission[0].AverageYear).ToString("N0");
            lblPerformanceCommissionCollected.Text = TeamGroupPerformance.ActualCommission[0].AverageCommissionReceivedPercentage.ToString() + "%";
        }
        private async void CompanyBagDetails_Tapped(object sender, EventArgs e)
        {

                await Navigation.PushAsync(new PerformanceDetailPage(3, TeamGroupPerformance));
        }
        
        private async void CompanyCommissionDetails_Tapped(object sender, EventArgs e)
        {
                  
                await Navigation.PushAsync(new PerformanceDetailPage(4, TeamGroupPerformance));
        }
        
        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as TeamPerformance;

            TeamProgramView_List.SelectedItem = null;

            if (item == null)
                return;

            await Navigation.PushAsync(new PerformancePage(2, Performance, item.Id));
        }
    }
}