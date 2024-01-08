using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class PerformancePage : ContentPage
    {
        PerformanceViewModel viewModel;
        SearchDispatchConfirmationFilter SearchDispatchConfirmationFilter { get; set; }
        Performance Performance { get; set; }
        Performance TeamPerformanceData { get; set; }

        public ObservableCollection<TeamGroupPerformance> TeamGroupPerformance { get; set; }

        int DC_details;
        int Filter_Team;

        int currentYear { get; set; }
        int TeamGroupPerformanceId;
        public PerformancePage(int title = 0, Performance performance = null, int teamGroup = 0 , string teamGroupName = null)
        {
            InitializeComponent();

            SearchDispatchConfirmationFilter = new SearchDispatchConfirmationFilter();

            TeamGroupPerformance = new ObservableCollection<TeamGroupPerformance>();


            DC_details = title;

            if (performance != null)
            {
                Performance = performance;
            }


            TeamPerformanceData = new Performance();

            TeamGroupPerformanceId = teamGroup;

            if (DC_details == 1)
            {
                Title = "Company Performance";
                TeamGroupProgramView_ListData.IsVisible = true;
            }
            else if (DC_details == 2)
            {
                Title = "Team Performance";


                if (TeamGroupPerformanceId > 0)
                {
                    lblTeamName.IsVisible = true;
                    lblTeamName.Text = teamGroupName;

                }
            }

            BindingContext = viewModel = new PerformanceViewModel();

        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();








            //if (Performance.IsCheckBack == 0)
            //{
            //    if (DC_details == 1)
            //    {               
            //            viewModel.ExecuteLoadTeamGroupDataCommand();          
            //    }
            //}

          
                if (TeamGroupPerformanceId == 0)
                {
                    lbltodayconfirmed.Text = int.Parse(Performance.ConfirmedBags.TodayBags).ToString("N0");
                    lblmonthconfirmed.Text = int.Parse(Performance.ConfirmedBags.CurrentMonthBags).ToString("N0");
                    lblaverageconfirmed.Text = int.Parse(Performance.ConfirmedBags.AverageBags).ToString("N0");
                    lblexpectedconfirmed.Text = int.Parse(Performance.ConfirmedBags.ExpectedAverageBags).ToString("N0");

                    lbltodaydispatched.Text = int.Parse(Performance.DispatchedBags.TodayBags).ToString("N0");
                    lblmonthdispatched.Text = int.Parse(Performance.DispatchedBags.CurrentMonthBags).ToString("N0");
                    lblaveragedispatched.Text = int.Parse(Performance.DispatchedBags.AverageBags).ToString("N0");
                    lblexpecteddispatched.Text = int.Parse(Performance.DispatchedBags.ExpectedAverageBags).ToString("N0");

                    lbltodaycommissionconfirmed.Text = int.Parse(Performance.ConfirmedCommission.TodayCommission).ToString("N0");
                    lblmonthcommissionconfirmed.Text = int.Parse(Performance.ConfirmedCommission.CurrentMonthCommission).ToString("N0");
                    lblaveragecommissionconfirmed.Text = int.Parse(Performance.ConfirmedCommission.AverageCommission).ToString("N0");
                    lblexpectedcommissionconfirmed.Text = int.Parse(Performance.ConfirmedCommission.ExpectedAverageCommission).ToString("N0");

                    lbltodaycommissiondispatched.Text = int.Parse(Performance.DispatchedCommission.TodayCommission).ToString("N0");
                    lblmonthcommissiondispatched.Text = int.Parse(Performance.DispatchedCommission.CurrentMonthCommission).ToString("N0");
                    lblaveragecommissiondispatched.Text = int.Parse(Performance.DispatchedCommission.AverageCommission).ToString("N0");
                    lblexpectedcommissiondispatched.Text = int.Parse(Performance.DispatchedCommission.ExpectedAverageCommission).ToString("N0");

                    lbltodaycommissionreceived.Text = int.Parse(Performance.ActualCommission[0].TodayCommissionReceived).ToString("N0");
                    lblmonthcommissionreceived.Text = int.Parse(Performance.ActualCommission[0].CurrentMonthCommissionReceived).ToString("N0");
                    lblaveragecommissionreceived.Text = int.Parse(Performance.ActualCommission[0].Average).ToString("N0");
                    lblexpectedcommissionreceived.Text = int.Parse(Performance.ActualCommission[0].ExpectedAverageCommissionReceived).ToString("N0");


                    lblThisYearBagsConfirmed.Text = int.Parse(Performance.ConfirmedBags.Total).ToString("N0");
                    lblAverageMonthBagsConfirmed.Text = int.Parse(Performance.ConfirmedBags.AverageYear).ToString("N0");
                    lblPercentageBagsConfirmed.Text = Performance.ConfirmedBags.AveragePercentage.ToString() + "%";

                    lblThisYearBagsDispatched.Text = int.Parse(Performance.DispatchedBags.Total).ToString("N0");
                    lblAverageMonthBagDispatched.Text = int.Parse(Performance.DispatchedBags.AverageYear).ToString("N0");
                    lblPercentageBagsDispatched.Text = Performance.DispatchedBags.AveragePercentage.ToString() + "%";

                    lblThisYearCommissionConfirmed.Text = int.Parse(Performance.ConfirmedCommission.Total).ToString("N0");
                    lblAverageCommissionConfirmed.Text = int.Parse(Performance.ConfirmedCommission.AverageYear).ToString("N0");
                    lblPerformanceCommissionConfirmed.Text = Performance.ConfirmedCommission.AverageCommissionPercentage.ToString() + "%";

                    lblThisYearCommissionDispatched.Text = int.Parse(Performance.DispatchedCommission.Total).ToString("N0");
                    lblAverageCommissionDispatched.Text = int.Parse(Performance.DispatchedCommission.AverageYear).ToString("N0");
                    lblPerformanceCommissionDispatched.Text = Performance.DispatchedCommission.AverageCommissionPercentage.ToString() + "%";

                    lblThisYearCommissionCollected.Text = int.Parse(Performance.ActualCommission[0].Total).ToString("N0");
                    lblAverageCommissionCollected.Text = int.Parse(Performance.ActualCommission[0].AverageYear).ToString("N0");
                    lblPerformanceCommissionCollected.Text = Performance.ActualCommission[0].AverageCommissionReceivedPercentage.ToString() + "%";

             
               if (Performance.PerformanceBackCheckBack == 0)
                {
                    if (DC_details == 1)
                    {
                          viewModel.ExecuteLoadTeamGroupDataCommand();

                     
                          Performance.TeamBackCheckBack = 1;
                    }
                }
                
            }
            else if (TeamGroupPerformanceId > 0)
            {
                TeamPerformanceData = await viewModel.ExecuteTeamPerformanceDataCommand(TeamGroupPerformanceId);

           
                lbltodayconfirmed.Text = int.Parse(TeamPerformanceData.ConfirmedBags.TodayBags).ToString("N0");
                lblmonthconfirmed.Text = int.Parse(TeamPerformanceData.ConfirmedBags.CurrentMonthBags).ToString("N0");
                lblaverageconfirmed.Text = int.Parse(TeamPerformanceData.ConfirmedBags.AverageBags).ToString("N0");
                lblexpectedconfirmed.Text = int.Parse(TeamPerformanceData.ConfirmedBags.ExpectedAverageBags).ToString("N0");

                lbltodaydispatched.Text = int.Parse(TeamPerformanceData.DispatchedBags.TodayBags).ToString("N0");
                lblmonthdispatched.Text = int.Parse(TeamPerformanceData.DispatchedBags.CurrentMonthBags).ToString("N0");
                lblaveragedispatched.Text = int.Parse(TeamPerformanceData.DispatchedBags.AverageBags).ToString("N0");
                lblexpecteddispatched.Text = int.Parse(TeamPerformanceData.DispatchedBags.ExpectedAverageBags).ToString("N0");

                lbltodaycommissionconfirmed.Text = int.Parse(TeamPerformanceData.ConfirmedCommission.TodayCommission).ToString("N0");
                lblmonthcommissionconfirmed.Text = int.Parse(TeamPerformanceData.ConfirmedCommission.CurrentMonthCommission).ToString("N0");
                lblaveragecommissionconfirmed.Text = int.Parse(TeamPerformanceData.ConfirmedCommission.AverageCommission).ToString("N0");
                lblexpectedcommissionconfirmed.Text = int.Parse(TeamPerformanceData.ConfirmedCommission.ExpectedAverageCommission).ToString("N0");

                lbltodaycommissiondispatched.Text = int.Parse(TeamPerformanceData.DispatchedCommission.TodayCommission).ToString("N0");
                lblmonthcommissiondispatched.Text = int.Parse(TeamPerformanceData.DispatchedCommission.CurrentMonthCommission).ToString("N0");
                lblaveragecommissiondispatched.Text = int.Parse(TeamPerformanceData.DispatchedCommission.AverageCommission).ToString("N0");
                lblexpectedcommissiondispatched.Text = int.Parse(TeamPerformanceData.DispatchedCommission.ExpectedAverageCommission).ToString("N0");

                lbltodaycommissionreceived.Text = int.Parse(TeamPerformanceData.ActualCommission[0].TodayCommissionReceived).ToString("N0");
                lblmonthcommissionreceived.Text = int.Parse(TeamPerformanceData.ActualCommission[0].CurrentMonthCommissionReceived).ToString("N0");
                lblaveragecommissionreceived.Text = int.Parse(TeamPerformanceData.ActualCommission[0].Average).ToString("N0");
                lblexpectedcommissionreceived.Text = int.Parse(TeamPerformanceData.ActualCommission[0].ExpectedAverageCommissionReceived).ToString("N0");





                lblThisYearBagsConfirmed.Text = int.Parse(TeamPerformanceData.ConfirmedBags.Total).ToString("N0");
                lblAverageMonthBagsConfirmed.Text = int.Parse(TeamPerformanceData.ConfirmedBags.AverageYear).ToString("N0");
                lblPercentageBagsConfirmed.Text = TeamPerformanceData.ConfirmedBags.AveragePercentage.ToString() + "%";

                lblThisYearBagsDispatched.Text = int.Parse(TeamPerformanceData.DispatchedBags.Total).ToString("N0");
                lblAverageMonthBagDispatched.Text = int.Parse(TeamPerformanceData.DispatchedBags.AverageYear).ToString("N0");
                lblPercentageBagsDispatched.Text = TeamPerformanceData.DispatchedBags.AveragePercentage.ToString() + "%";

                lblThisYearCommissionConfirmed.Text = int.Parse(TeamPerformanceData.ConfirmedCommission.Total).ToString("N0");
                lblAverageCommissionConfirmed.Text = int.Parse(TeamPerformanceData.ConfirmedCommission.AverageYear).ToString("N0");
                lblPerformanceCommissionConfirmed.Text = TeamPerformanceData.ConfirmedCommission.AverageCommissionPercentage.ToString() + "%";

                lblThisYearCommissionDispatched.Text = int.Parse(TeamPerformanceData.DispatchedCommission.Total).ToString("N0");
                lblAverageCommissionDispatched.Text = int.Parse(TeamPerformanceData.DispatchedCommission.AverageYear).ToString("N0");
                lblPerformanceCommissionDispatched.Text = TeamPerformanceData.DispatchedCommission.AverageCommissionPercentage.ToString() + "%";

                lblThisYearCommissionCollected.Text = int.Parse(TeamPerformanceData.ActualCommission[0].Total).ToString("N0");
                lblAverageCommissionCollected.Text = int.Parse(TeamPerformanceData.ActualCommission[0].AverageYear).ToString("N0");
                lblPerformanceCommissionCollected.Text = TeamPerformanceData.ActualCommission[0].AverageCommissionReceivedPercentage.ToString() + "%";

                Performance.TeamBackCheckBack = 1;


            }
      
        }


        int Change_Details;     
        private async void CompanyBagDetails_Tapped(object sender, EventArgs e)
        {
            if (DC_details == 1)
            {
                Change_Details = 1;
            }
            else
            {
                Change_Details = 3;
            }

            if (TeamGroupPerformanceId == 0)
            {
                await Navigation.PushAsync(new PerformanceDetailPage(Change_Details, Performance));
            }
            else
            {
                await Navigation.PushAsync(new PerformanceDetailPage(Change_Details, TeamPerformanceData));
            }
        }
        private async void CompanyCommissionDetails_Tapped(object sender, EventArgs e)
        {
            if (DC_details == 1)
            {
                Change_Details = 2;
            }
            else
            {
                Change_Details = 4;
            }

            if (TeamGroupPerformanceId == 0)
            {
                await Navigation.PushAsync(new PerformanceDetailPage(Change_Details, Performance));
            }
            else
            {
                await Navigation.PushAsync(new PerformanceDetailPage(Change_Details, TeamPerformanceData));
            }
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as TeamGroupPerformance;

            TeamGroupProgramView_List.SelectedItem = null;

            if (item == null)
                return;

            Performance.TeamBackCheckBack = 0;

            await Navigation.PushAsync(new TeamGroupPerformancePage(item.Id, Performance));

        }




    }
}