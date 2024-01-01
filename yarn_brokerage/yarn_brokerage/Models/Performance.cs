using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace yarn_brokerage.Models
{
    public class Performance
    {
        [JsonProperty("confirmed_bags")]
        public ConfirmedBags ConfirmedBags { get; set; }

        [JsonProperty("confirmed_commission")]
        public ConfirmedCommission ConfirmedCommission { get; set; }

        [JsonProperty("dispatched_bags")]
        public DispatchedBags DispatchedBags { get; set; }

        [JsonProperty("dispatched_commission")]
        public DispatchedCommission DispatchedCommission { get; set; }

        [JsonProperty("actual_commission")]
        public List<ActualCommission> ActualCommission { get; set; }


        public int PerformanceBackCheckBack { get; set; }
        public int TeamBackCheckBack { get; set; }
        public int TeamGroupBackCheckBack { get; set; }
    }

    public class ConfirmedBags
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("today_bags")]
        public string TodayBags { get; set; }

        [JsonProperty("total")]
        public string Total { get; set; }
        [JsonProperty("average_year")]
        public string AverageYear { get; set; }
        [JsonProperty("average_percentage")]
        public int AveragePercentage { get; set; }

        [JsonProperty("team_group_name")]
        public string TeamGroupName { get; set; }

        [JsonProperty("current_month_bags")]
        public string CurrentMonthBags { get; set; }

        [JsonProperty("average_bags")]
        public string AverageBags { get; set; }

        [JsonProperty("expected_average_bags")]
        public string ExpectedAverageBags { get; set; }

        [JsonProperty("apr_bags")]
        public string AprBags { get; set; }
        [JsonProperty("may_bags")]
        public string MayBags { get; set; }

        [JsonProperty("june_bags")]
        public string JuneBags { get; set; }

        [JsonProperty("july_bags")]
        public string JulyBags { get; set; }

        [JsonProperty("aug_bags")]
        public string AugBags { get; set; }

        [JsonProperty("sep_bags")]
        public string SepBags { get; set; }

        [JsonProperty("oct_bags")]
        public string OctBags { get; set; }

        [JsonProperty("nov_bags")]
        public string NovBags { get; set; }

        [JsonProperty("dec_bags")]
        public string DecBags { get; set; }

        [JsonProperty("jan_bags")]
        public string JanBags { get; set; }

        [JsonProperty("feb_bags")]
        public string FebBags { get; set; }

        [JsonProperty("mar_bags")]
        public string MarBags { get; set; }
    }

    public class ConfirmedCommission
    {
        [JsonProperty("today_commission")]
        public string TodayCommission { get; set; }



        [JsonProperty("total")]
        public string Total { get; set; }
        [JsonProperty("average_year")]
        public string AverageYear { get; set; }
        [JsonProperty("average_commission_percentage")]
        public int AverageCommissionPercentage { get; set; }





        [JsonProperty("current_month_commission")]
        public string CurrentMonthCommission { get; set; }

        [JsonProperty("average_commission")]
        public string AverageCommission { get; set; }

        [JsonProperty("expected_average_commission")]
        public string ExpectedAverageCommission { get; set; }

        [JsonProperty("apr_bags")]
        public string AprBags { get; set; }

        [JsonProperty("may_bags")]
        public string MayBags { get; set; }

        [JsonProperty("june_bags")]
        public string JuneBags { get; set; }

        [JsonProperty("july_bags")]
        public string JulyBags { get; set; }

        [JsonProperty("aug_bags")]
        public string AugBags { get; set; }

        [JsonProperty("sep_bags")]
        public string SepBags { get; set; }

        [JsonProperty("oct_bags")]
        public string OctBags { get; set; }

        [JsonProperty("nov_bags")]
        public string NovBags { get; set; }

        [JsonProperty("dec_bags")]
        public string DecBags { get; set; }

        [JsonProperty("jan_bags")]
        public string JanBags { get; set; }

        [JsonProperty("feb_bags")]
        public string FebBags { get; set; }

        [JsonProperty("mar_bags")]
        public string MarBags { get; set; }
    }

    public class DispatchedBags
    {
        [JsonProperty("today_bags")]
        public string TodayBags { get; set; }





        [JsonProperty("total")]
        public string Total { get; set; }
        [JsonProperty("average_year")]
        public string AverageYear { get; set; }
        [JsonProperty("average_percentage")]
        public int AveragePercentage { get; set; }







        [JsonProperty("current_month_bags")]
        public string CurrentMonthBags { get; set; }

        [JsonProperty("average_bags")]
        public string AverageBags { get; set; }

        [JsonProperty("expected_average_bags")]
        public string ExpectedAverageBags { get; set; }

        [JsonProperty("apr_bags")]
        public string AprBags { get; set; }

        [JsonProperty("may_bags")]
        public string MayBags { get; set; }

        [JsonProperty("june_bags")]
        public string JuneBags { get; set; }

        [JsonProperty("july_bags")]
        public string JulyBags { get; set; }

        [JsonProperty("aug_bags")]
        public string AugBags { get; set; }

        [JsonProperty("sep_bags")]
        public string SepBags { get; set; }

        [JsonProperty("oct_bags")]
        public string OctBags { get; set; }

        [JsonProperty("nov_bags")]
        public string NovBags { get; set; }

        [JsonProperty("dec_bags")]
        public string DecBags { get; set; }

        [JsonProperty("jan_bags")]
        public string JanBags { get; set; }

        [JsonProperty("feb_bags")]
        public string FebBags { get; set; }

        [JsonProperty("mar_bags")]
        public string MarBags { get; set; }
    }

    public class DispatchedCommission
    {
        [JsonProperty("today_commission")]
        public string TodayCommission { get; set; }


        [JsonProperty("total")]
        public string Total { get; set; }
        [JsonProperty("average_year")]
        public string AverageYear { get; set; }
        [JsonProperty("average_commission_percentage")]
        public int AverageCommissionPercentage { get; set; }






        [JsonProperty("current_month_commission")]
        public string CurrentMonthCommission { get; set; }

        [JsonProperty("average_commission")]
        public string AverageCommission { get; set; }

        [JsonProperty("expected_average_commission")]
        public string ExpectedAverageCommission { get; set; }

        [JsonProperty("apr_bags")]
        public string AprBags { get; set; }

        [JsonProperty("may_bags")]
        public string MayBags { get; set; }

        [JsonProperty("june_bags")]
        public string JuneBags { get; set; }

        [JsonProperty("july_bags")]
        public string JulyBags { get; set; }

        [JsonProperty("aug_bags")]
        public string AugBags { get; set; }

        [JsonProperty("sep_bags")]
        public string SepBags { get; set; }

        [JsonProperty("oct_bags")]
        public string OctBags { get; set; }

        [JsonProperty("nov_bags")]
        public string NovBags { get; set; }

        [JsonProperty("dec_bags")]
        public string DecBags { get; set; }

        [JsonProperty("jan_bags")]
        public string JanBags { get; set; }

        [JsonProperty("feb_bags")]
        public string FebBags { get; set; }

        [JsonProperty("mar_bags")]
        public string MarBags { get; set; }
    }
    public class ActualCommission
    {
        [JsonProperty("today_commission_received")]
        public string TodayCommissionReceived { get; set; }


        [JsonProperty("total")]
        public string Total { get; set; }
        [JsonProperty("average_year")]
        public string AverageYear { get; set; }
        [JsonProperty("average_commission_received_percentage")]
        public int AverageCommissionReceivedPercentage { get; set; }








        [JsonProperty("current_month_commission_received")]
        public string CurrentMonthCommissionReceived { get; set; }

        [JsonProperty("average")]
        public string Average { get; set; }

        [JsonProperty("expected_average_commission_received")]
        public string ExpectedAverageCommissionReceived { get; set; }

        [JsonProperty("apr_bags")]
        public string AprBags { get; set; }

        [JsonProperty("may_bags")]
        public string MayBags { get; set; }

        [JsonProperty("june_bags")]
        public string JuneBags { get; set; }

        [JsonProperty("july_bags")]
        public string JulyBags { get; set; }

        [JsonProperty("aug_bags")]
        public string AugBags { get; set; }

        [JsonProperty("sep_bags")]
        public string SepBags { get; set; }

        [JsonProperty("oct_bags")]
        public string OctBags { get; set; }

        [JsonProperty("nov_bags")]
        public string NovBags { get; set; }

        [JsonProperty("dec_bags")]
        public string DecBags { get; set; }

        [JsonProperty("jan_bags")]
        public string JanBags { get; set; }

        [JsonProperty("feb_bags")]
        public string FebBags { get; set; }

        [JsonProperty("mar_bags")]
        public string MarBags { get; set; }
    }
    public class TeamGroupPerformance
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("confirmed_bags_percentage")]
        public string ConfirmedBagsPercentage { get; set; }

        [JsonProperty("confirmed_commission_percentage")]
        public string ConfirmedCommissionPercentage { get; set; }

        [JsonProperty("dispatched_bags_percentage")]
        public string DispatchedBagsPercentage { get; set; }

        [JsonProperty("dispatched_commission_percentage")]
        public string DispatchedCommissionPercentage { get; set; }

        [JsonProperty("actual_commission_percentage")]
        public string ActualCommissionPercentage { get; set; }
    }
 
    public class TeamPerformance
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("team_group_name")]
        public string TeamGroupName { get; set; }

        [JsonProperty("confirmed_bags_percentage")]
        public string ConfirmedBagsPercentage { get; set; }

        [JsonProperty("confirmed_commission_percentage")]
        public string ConfirmedCommissionPercentage { get; set; }

        [JsonProperty("dispatched_bags_percentage")]
        public string DispatchedBagsPercentage { get; set; }

        [JsonProperty("dispatched_commission_percentage")]
        public string DispatchedCommissionPercentage { get; set; }

        [JsonProperty("actual_commission_percentage")]
        public string ActualCommissionPercentage { get; set; }
    }
}
