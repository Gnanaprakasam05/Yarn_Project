using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using yarn_brokerage.Models;

namespace yarn_brokerage.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerformanceDetailPage : ContentPage
    {
        int DC_details;


        Performance Performance { get; set; }
        public PerformanceDetailPage(int title = 0, Performance performance = null)
        {
            InitializeComponent();
            DC_details = title;

            Performance = performance;



            if (DC_details == 1)
            {
                Title = "Company Performance Bags";
                frmCompanyBags.IsVisible = true;



            }
            else if (DC_details == 2)
            {


                Title = "Company Performance Commission";
                frmCompanyCommission.IsVisible = true;

              

            }
            else if (DC_details == 3)
            {
                frmCompanyBags.IsVisible = true;
                Title = "Team Performance Bags";
            }
            else if (DC_details == 4)
            {
                frmCompanyCommission.IsVisible = true;
                Title = "Team Performance Commission";
            }
            else if (DC_details == 6)
            {
                frmCompanyCommission.IsVisible = true;
                Title = "Team Performance Commission";
            }
        }



        protected override void OnAppearing()
        {
            base.OnAppearing();



            if (DC_details == 1 || DC_details == 3)
            {
                lblConfirmedAplBags.Text = int.Parse(Performance.ConfirmedBags.AprBags).ToString("N0");
                lblConfirmedMayBags.Text = int.Parse(Performance.ConfirmedBags.MayBags).ToString("N0");
                lblConfirmedJuneBags.Text = int.Parse(Performance.ConfirmedBags.JuneBags).ToString("N0");
                lblConfirmedJulyBags.Text = int.Parse(Performance.ConfirmedBags.JulyBags).ToString("N0");
                lblConfirmedAugBags.Text = int.Parse(Performance.ConfirmedBags.AugBags).ToString("N0");
                lblConfirmedSepBags.Text = int.Parse(Performance.ConfirmedBags.SepBags).ToString("N0");
                lblConfirmedOctBags.Text = int.Parse(Performance.ConfirmedBags.OctBags).ToString("N0");
                lblConfirmedNovBags.Text = int.Parse(Performance.ConfirmedBags.NovBags).ToString("N0");
                lblConfirmedDecBags.Text = int.Parse(Performance.ConfirmedBags.DecBags).ToString("N0");
                lblConfirmedJanBags.Text = int.Parse(Performance.ConfirmedBags.JanBags).ToString("N0");
                lblConfirmedFebBags.Text = int.Parse(Performance.ConfirmedBags.FebBags).ToString("N0");
                lblConfirmedMarBags.Text = int.Parse(Performance.ConfirmedBags.MarBags).ToString("N0");
                lbldispatched_bags_apr_bags.Text = int.Parse(Performance.DispatchedBags.AprBags).ToString("N0");
                lbldispatched_bags_may_bags.Text = int.Parse(Performance.DispatchedBags.MayBags).ToString("N0");
                lbldispatched_bags_june_bags.Text = int.Parse(Performance.DispatchedBags.JuneBags).ToString("N0");
                lbldispatched_bags_july_bags.Text = int.Parse(Performance.DispatchedBags.JulyBags).ToString("N0");
                lbldispatched_bags_aug_bags.Text = int.Parse(Performance.DispatchedBags.AugBags).ToString("N0");
                lbldispatched_bags_sep_bags.Text = int.Parse(Performance.DispatchedBags.SepBags).ToString("N0");
                lbldispatched_bags_oct_bags.Text = int.Parse(Performance.DispatchedBags.OctBags).ToString("N0");
                lbldispatched_bags_nov_bags.Text = int.Parse(Performance.DispatchedBags.NovBags).ToString("N0");
                lbldispatched_bags_dec_bags.Text = int.Parse(Performance.DispatchedBags.DecBags).ToString("N0");
                lbldispatched_bags_jan_bags.Text = int.Parse(Performance.DispatchedBags.JanBags).ToString("N0");
                lbldispatched_bags_feb_bags.Text = int.Parse(Performance.DispatchedBags.FebBags).ToString("N0");
                lbldispatched_bags_mar_bags.Text = int.Parse(Performance.DispatchedBags.MarBags).ToString("N0");
            }
            else
            {
                lblConfirmedCommissionAplBags.Text = int.Parse(Performance.ConfirmedCommission.AprBags).ToString("N0");
                lblConfirmedCommissionMayBags.Text = int.Parse(Performance.ConfirmedCommission.MayBags).ToString("N0");
                lblConfirmedCommissionJuneBags.Text = int.Parse(Performance.ConfirmedCommission.JuneBags).ToString("N0");
                lblConfirmedCommissionJulyBags.Text = int.Parse(Performance.ConfirmedCommission.JulyBags).ToString("N0");
                lblConfirmedCommissionAugBags.Text = int.Parse(Performance.ConfirmedCommission.AugBags).ToString("N0");
                lblConfirmedCommissionSepBags.Text = int.Parse(Performance.ConfirmedCommission.SepBags).ToString("N0");
                lblConfirmedCommissionOctBags.Text = int.Parse(Performance.ConfirmedCommission.OctBags).ToString("N0");
                lblConfirmedCommissionNovBags.Text = int.Parse(Performance.ConfirmedCommission.NovBags).ToString("N0");
                lblConfirmedCommissionDecBags.Text = int.Parse(Performance.ConfirmedCommission.DecBags).ToString("N0");
                lblConfirmedCommissionJanBags.Text = int.Parse(Performance.ConfirmedCommission.JanBags).ToString("N0");
                lblConfirmedCommissionFebBags.Text = int.Parse(Performance.ConfirmedCommission.FebBags).ToString("N0");
                lblConfirmedCommissionMarBags.Text = int.Parse(Performance.ConfirmedCommission.MarBags).ToString("N0");

                lbldispatchedCommission_bags_apr_bags.Text = int.Parse(Performance.DispatchedCommission.AprBags).ToString("N0");
                lbldispatchedCommission_bags_may_bags.Text = int.Parse(Performance.DispatchedCommission.MayBags).ToString("N0");
                lbldispatchedCommission_bags_june_bags.Text = int.Parse(Performance.DispatchedCommission.JuneBags).ToString("N0");
                lbldispatchedCommission_bags_july_bags.Text = int.Parse(Performance.DispatchedCommission.JulyBags).ToString("N0");
                lbldispatchedCommission_bags_aug_bags.Text = int.Parse(Performance.DispatchedCommission.AugBags).ToString("N0");
                lbldispatchedCommission_bags_sep_bags.Text = int.Parse(Performance.DispatchedCommission.SepBags).ToString("N0");
                lbldispatchedCommission_bags_oct_bags.Text = int.Parse(Performance.DispatchedCommission.OctBags).ToString("N0");
                lbldispatchedCommission_bags_nov_bags.Text = int.Parse(Performance.DispatchedCommission.NovBags).ToString("N0");
                lbldispatchedCommission_bags_dec_bags.Text = int.Parse(Performance.DispatchedCommission.DecBags).ToString("N0");
                lbldispatchedCommission_bags_jan_bags.Text = int.Parse(Performance.DispatchedCommission.JanBags).ToString("N0");
                lbldispatchedCommission_bags_feb_bags.Text = int.Parse(Performance.DispatchedCommission.FebBags).ToString("N0");
                lbldispatchedCommission_bags_mar_bags.Text = int.Parse(Performance.DispatchedCommission.MarBags).ToString("N0");

                lbldispatched_commission_apr_bags.Text = int.Parse(Performance.ActualCommission[0].AprBags).ToString("N0");
                lbldispatched_commission_may_bags.Text = int.Parse(Performance.ActualCommission[0].MayBags).ToString("N0");
                lbldispatched_commission_june_bags.Text = int.Parse(Performance.ActualCommission[0].JuneBags).ToString("N0");
                lbldispatched_commission_july_bags.Text = int.Parse(Performance.ActualCommission[0].JulyBags).ToString("N0");
                lbldispatched_commission_aug_bags.Text = int.Parse(Performance.ActualCommission[0].AugBags).ToString("N0");
                lbldispatched_commission_sep_bags.Text = int.Parse(Performance.ActualCommission[0].SepBags).ToString("N0");
                lbldispatched_commission_oct_bags.Text = int.Parse(Performance.ActualCommission[0].OctBags).ToString("N0");
                lbldispatched_commission_nov_bags.Text = int.Parse(Performance.ActualCommission[0].NovBags).ToString("N0");
                lbldispatched_commission_dec_bags.Text = int.Parse(Performance.ActualCommission[0].DecBags).ToString("N0");
                lbldispatched_commission_jan_bags.Text = int.Parse(Performance.ActualCommission[0].JanBags).ToString("N0");
                lbldispatched_commission_feb_bags.Text = int.Parse(Performance.ActualCommission[0].FebBags).ToString("N0");
                lbldispatched_commission_mar_bags.Text = int.Parse(Performance.ActualCommission[0].MarBags).ToString("N0");
            }
        }
    }
}