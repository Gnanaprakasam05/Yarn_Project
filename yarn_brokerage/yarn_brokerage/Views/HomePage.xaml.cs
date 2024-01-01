using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts;
using Entry = Microcharts.ChartEntry;
using SkiaSharp;
using yarn_brokerage.Services;
using yarn_brokerage.Models;
using yarn_brokerage.ViewModels;
using System.Threading;
using System.Diagnostics;
using System.IO;
using Plugin.Permissions.Abstractions;

namespace yarn_brokerage.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class HomePage : ContentPage
    {
        string UserName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "remember.txt");
        List<Entry> entries = new List<Entry>
        {
            new Entry(25)
            {
                Color = SKColor.Parse("#e44e55"),
                Label = "Offers",
                ValueLabel="25",
            },
            new Entry(20)
            {
                Color = SKColor.Parse("#059142"),
                Label = "Enquiries",
                ValueLabel="20",
            },

            new Entry(8)
            {
                Color = SKColor.Parse("#15b9ee"),
                Label = "Confirm",
                ValueLabel="8",
            }
        };

        DashboardViewModel viewModel;
        Dashboard dashboard { get; set; }
        Approval Approval { get; set; }
        AttendenceDashboard attendenceDashboard { get; set; }
        TopDashboard topDashboard { get; set; }
        Performance PerformanceData { get; set; }
        Performance TeamPerformanceData { get; set; }
        public DateTime AttendanceDate { get; set; }
        public HomePage()
        {
            InitializeComponent();

            BindingContext = viewModel = new DashboardViewModel();

            attendenceDashboard = new AttendenceDashboard();

            Approval = new Approval();

            PerformanceData = new Performance();

            TeamPerformanceData = new Performance();

            Approval.AttendanceTeamName = Application.Current.Properties["username"].ToString();

            if (Application.Current.Properties["mobile_dashboard_dispatched"].ToString() == "0")
                frmDispatch.IsVisible = false;
            if (Application.Current.Properties["mobile_dashboard_enquiry"].ToString() == "0")
                frmOffersAndEnquiry.IsVisible = false;
            if (Application.Current.Properties["mobile_dashboard_approval"].ToString() == "0")
                frmApproval.IsVisible = false;
            if (Application.Current.Properties["mobile_dashboard_attendance"].ToString() == "0")
                frmAttendance.IsVisible = false;
            if (Application.Current.Properties["mobile_dashboard_current_plan"].ToString() == "0")
                frmCurrentPlan.IsVisible = false;
            if (Application.Current.Properties["mobile_dashboard_all_user_attendance"].ToString() == "0")
                userList.IsVisible = false;
            if (Application.Current.Properties["mobile_dashboard_pending_confirmation"].ToString() == "0")
                frmConfirmation.IsVisible = false;
            if (Application.Current.Properties["mobile_dashboard_bags_performance"].ToString() == "0")
                frmCompanyPerformance.IsVisible = false;
            if (Application.Current.Properties["mobile_dashboard_commission_performance"].ToString() == "0")
                frmTeamPerformance.IsVisible = false;

            if (Application.Current.Properties["team_group_id"] == null)
                frmTeamPerformance.IsVisible = false;


        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (Application.Current.Properties.ContainsKey("direct_confirmation"))
            {
                if (Application.Current.Properties["direct_confirmation"].ToString() == "1")
                {
                    Application.Current.Properties["direct_confirmation"] = 0;
                    await Navigation.PushAsync(new DraftConfirmationPage());
                }
            }
            Daily_Tapped(null, null);

            LoadAttendenceDashBoard(startAttendenceDatePicker.Date, Approval.AttendanceTeamNameId);

            txtTeamName.Text = Approval.AttendanceTeamName;

            PerformanceData = await viewModel.ExecuteLoadWhatsappDashboardsCommand();

            if (Application.Current.Properties["team_group_id"] != null)
            {
                TeamPerformanceData = await viewModel.ExecuteTeamPermissionCommand();

                lblTeamBagsPercentage.Text = ((TeamPerformanceData.ConfirmedBags.AveragePercentage + TeamPerformanceData.DispatchedBags.AveragePercentage) / 2).ToString() + "%";

                lblTeamCommissionPercentage.Text = ((TeamPerformanceData.ConfirmedCommission.AverageCommissionPercentage + TeamPerformanceData.DispatchedCommission.AverageCommissionPercentage + TeamPerformanceData.ActualCommission[0].AverageCommissionReceivedPercentage) / 3).ToString() + "%";

            }

            lblBagsPercentage.Text = ((PerformanceData.ConfirmedBags.AveragePercentage + PerformanceData.DispatchedBags.AveragePercentage) / 2).ToString() + "%";

            lblCommissionPercentage.Text = ((PerformanceData.ConfirmedCommission.AverageCommissionPercentage + PerformanceData.DispatchedCommission.AverageCommissionPercentage + PerformanceData.ActualCommission[0].AverageCommissionReceivedPercentage) / 3).ToString() + "%";

            viewModel.ExecuteLoadPendingConfirmationDelayCommand();
            viewModel.ExecuteLoadPendingConfirmationTodayCommand();
            viewModel.ExecuteLoadPendingConfirmationFutureCommand();
        }




        private async void LoadAttendenceDashBoard(DateTime dateTime, int AttendanceTeamNameId = 0) //, int Overdue_flag
        {
            var dependency = DependencyService.Get<INativeFont>();

            DateTime currentDate = dateTime;
            DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            attendenceDashboard.FromDays = firstDayOfMonth;
            attendenceDashboard.ToDays = lastDayOfMonth;
            var data = await viewModel.ExecuteLoadAttendenceDashboardsCommand(firstDayOfMonth, lastDayOfMonth, AttendanceTeamNameId); //, Overdue_flag


            if (data.Count() != 0)
            {
                for (int i = 0; i < data.Count(); i++)
                {
                    attendenceDashboard.TotalWorkingDays = data[i].TotalWorkingDays;
                    attendenceDashboard.FullDay = data[i].FullDay;
                    attendenceDashboard.HalfDay = data[i].HalfDay;
                    attendenceDashboard.Absent = data[i].Absent;
                    attendenceDashboard.PermissionMorning = data[i].PermissionMorning;
                    attendenceDashboard.PermissionEvening = data[i].PermissionEvening;
                    attendenceDashboard.LateMorning = data[i].LateMorning;
                    attendenceDashboard.LateEvening = data[i].LateEvening;
                    attendenceDashboard.EmployeeId = data[i].EmployeeId;
                }
                toldays.Text = (int.Parse(attendenceDashboard.FullDay) + int.Parse(attendenceDashboard.HalfDay) + int.Parse(attendenceDashboard.Absent)).ToString();
                fuldays.Text = attendenceDashboard.FullDay.ToString();
                halfdays.Text = attendenceDashboard.HalfDay.ToString();
                absent.Text = attendenceDashboard.Absent.ToString();
                permission.Text = (attendenceDashboard.PermissionMorning + attendenceDashboard.PermissionEvening).ToString();
                late.Text = (attendenceDashboard.LateMorning + attendenceDashboard.LateEvening).ToString();
            }
            else
            {
                toldays.Text = "0";
                fuldays.Text = "0";
                halfdays.Text = "0";
                absent.Text = "0";
                permission.Text = "0";
                late.Text = "0";

            }


        }
        private async void LoadTopDashBoard(DateTime dateTime, string time_flag)
        {
            await viewModel.ExecuteLoadTopDashboardsCommand(dateTime, time_flag);
            EnquiryListView.HeightRequest = viewModel.EnquiryCount() * 63;
        }
        private async void LoadDashBoard(DateTime dateTime, string time_flag) //, int Overdue_flag
        {
            var dependency = DependencyService.Get<INativeFont>();
            dashboard = await viewModel.ExecuteLoadDashboardsCommand(dateTime, time_flag); //, Overdue_flag
            List<Entry> entries = new List<Entry>
            {
                new Entry(dashboard.OfferCount)
                {
                    Color = SKColor.Parse("#e44e55"),
                    //Label = dashboard.OfferCount.ToString(),
                    //ValueLabel = dashboard.OfferValue,
                },
                new Entry(dashboard.EnquiryCount)
                {
                    Color = SKColor.Parse("#059142"),
                    //Label = dashboard.EnquiryCount.ToString(),
                    //ValueLabel = dashboard.EnquiryValue,
                },
                new Entry(dashboard.ConfirmCount)
                {
                    Color = SKColor.Parse("#15b9ee"),
                    //Label = dashboard.ConfirmCount.ToString(),
                    //ValueLabel = dashboard.ConfirmValue,                    
                },
                new Entry((dashboard.ConfirmCount!= 0 || dashboard.EnquiryCount !=0 || dashboard.OfferCount != 0) ? 0 : 1)
                {
                    Color = SKColor.Parse("#FFF4DE"),
                    //Label = dashboard.ConfirmCount.ToString(),
                    //ValueLabel = dashboard.ConfirmValue,                    
                }

            };
            List<Entry> BagsNotReady = new List<Entry>
            {
                new Entry((Convert.ToInt32(dashboard.bags_not_ready_count) > 0) ? Convert.ToInt32(dashboard.bags_not_ready_count) : 0.1f)
                {
                    Color = SKColor.Parse("#e44e55"),
                    //Label = "",
                    //ValueLabel = dashboard.bags_not_ready_count,
                },
                new Entry((Convert.ToInt32(dashboard.payment_not_ready_count) > 0) ? Convert.ToInt32(dashboard.payment_not_ready_count) : 0.1f)
                {
                    Color = SKColor.Parse("#4285f4"),
                    //Label = "",
                    //ValueLabel = dashboard.bags_not_ready_count,
                },
                new Entry((Convert.ToInt32(dashboard.payment_not_received_count) > 0) ? Convert.ToInt32(dashboard.payment_not_received_count) : 0.1f)
                {
                    Color = SKColor.Parse("#059142"),
                    //Label = "",
                    //ValueLabel = dashboard.bags_not_ready_count,

                },
                new Entry((Convert.ToInt32(dashboard.transporter_not_ready_count) > 0) ? Convert.ToInt32(dashboard.transporter_not_ready_count) : 0.1f)
                {
                    Color = SKColor.Parse("#FFA500"),
                    //Label = "",
                    //ValueLabel = dashboard.bags_not_ready_count,
                },
                new Entry((Convert.ToInt32(dashboard.dispatched_count) > 0) ? Convert.ToInt32(dashboard.dispatched_count) : 0.1f)
                {
                    Color = SKColor.Parse("#15c9e1"),
                    //Label = "",
                    //ValueLabel = dashboard.bags_not_ready_count,
                },

            };

            //BarChart.EnableTouchEvents = true;
            //BarChart.Chart = new BarChart { Entries = BagsNotReady, LabelTextSize = dependency.GetNativeSize(15), IsAnimated = false,  MaxValue=Convert.ToInt32(dashboard.DispatchConfirmCount), AnimationDuration = new TimeSpan(15) };


            List<Entry> PendingInvoice = new List<Entry>
            {
                 new Entry(Convert.ToInt32(dashboard.invoice_details_count))
                {
                    Color = SKColor.Parse("#e44e55"),
                    //Label = dashboard.EnquiryCount.ToString(),
                    //ValueLabel = dashboard.EnquiryValue,
                },
                new Entry(Convert.ToInt32(dashboard.after_dispatch_count) - Convert.ToInt32(dashboard.invoice_details_count))
                {
                    Color = SKColor.Parse("#FFE9EB"),
                    //Label = dashboard.OfferCount.ToString(),
                    //ValueLabel = dashboard.OfferValue,
                },


            };
            //PendingInvoicePieChart.EnableTouchEvents = true;
            //PendingInvoicePieChart.Chart = new DonutChart { Entries = PendingInvoice, LabelTextSize = dependency.GetNativeSize(15), IsAnimated = false, HoleRadius=0.7f, AnimationDuration = new TimeSpan(0) };

            List<Entry> PendingPayment = new List<Entry>
            {
                new Entry(Convert.ToInt32(dashboard.pending_payment_count))
                {
                    Color = SKColor.Parse("#15c9e1"),
                    //Label = dashboard.EnquiryCount.ToString(),
                    //ValueLabel = dashboard.EnquiryValue,
                },
                new Entry(Convert.ToInt32(dashboard.after_dispatch_count) - Convert.ToInt32(dashboard.pending_payment_count))
                {
                    Color = SKColor.Parse("#DBFAFD"),
                    //Label = dashboard.OfferCount.ToString(),
                    //ValueLabel = dashboard.OfferValue,
                },

            };
            //PendingPaymentPieChart.EnableTouchEvents = true;
            //PendingPaymentPieChart.Chart = new DonutChart { Entries = PendingPayment, LabelTextSize = dependency.GetNativeSize(15), IsAnimated = false, HoleRadius = 0.7f, AnimationDuration = new TimeSpan(0) };

            List<Entry> Transit = new List<Entry>
            {
                new Entry(Convert.ToInt32(dashboard.delivered_count))
                {
                    Color = SKColor.Parse("#059142"),
                    //Label = dashboard.EnquiryCount.ToString(),
                    //ValueLabel = dashboard.EnquiryValue,
                },
                new Entry(Convert.ToInt32(dashboard.after_dispatch_count) - Convert.ToInt32(dashboard.delivered_count))
                {
                    Color = SKColor.Parse("#DCF0E5"),
                    //Label = dashboard.OfferCount.ToString(),
                    //ValueLabel = dashboard.OfferValue,
                },


            };

            //TransitPieChart.EnableTouchEvents = true;
            //TransitPieChart.Chart = new DonutChart { Entries = Transit, LabelTextSize = dependency.GetNativeSize(15), IsAnimated = false, HoleRadius=0.7f, AnimationDuration = new TimeSpan(0) };

            List<Entry> CustomerConfirmation = new List<Entry>
            {
                new Entry(Convert.ToInt32(dashboard.acknowledgement_count))
                {
                    Color = SKColor.Parse("#FFA500"),
                    //Label = dashboard.EnquiryCount.ToString(),
                    //ValueLabel = dashboard.EnquiryValue,
                },
                new Entry(Convert.ToInt32(dashboard.after_dispatch_count) - Convert.ToInt32(dashboard.acknowledgement_count))
                {
                    Color = SKColor.Parse("#FFF4DE"),
                    //Label = dashboard.OfferCount.ToString(),
                    //ValueLabel = dashboard.OfferValue,
                },
            };
            //CustomerConfirmationPieChart.EnableTouchEvents = true;
            //CustomerConfirmationPieChart.Chart = new DonutChart { Entries = CustomerConfirmation, LabelTextSize = dependency.GetNativeSize(15), IsAnimated = false, HoleRadius=0.7f, AnimationDuration = new TimeSpan(0) };

            //PaymentBarChart.EnableTouchEvents = true;
            //PaymentBarChart.Chart = new BarChart { Entries = PaymentNotReady, LabelTextSize = dependency.GetNativeSize(15), IsAnimated = false, MaxValue = Convert.ToInt32(dashboard.DispatchConfirmCount), AnimationDuration = new TimeSpan(15) };

            //PaymentRBarChart.EnableTouchEvents = true;
            //PaymentRBarChart.Chart = new BarChart { Entries = PaymentNotReceived, LabelTextSize = dependency.GetNativeSize(15), IsAnimated = false, MaxValue = Convert.ToInt32(dashboard.DispatchConfirmCount), AnimationDuration = new TimeSpan(15) };







            lblCurrentPlan.Text = "CURRENT PLAN : " + dashboard.DispatchConfirmCount;
            lblCurrentPlanBags.Text = dashboard.DispatchConfirmValue;

            lblAfterDispatch.Text = "DISPATCHED : " + dashboard.after_dispatch_count;
            lblAfterDispatchBags.Text = dashboard.after_dispatch_value;

            lblOffers.Text = dashboard.OfferLabel;
            lblEnquiry.Text = dashboard.EnquiryLabel;
            lblConfirm.Text = dashboard.ConfirmLabel;
            lblOfferBags.Text = dashboard.OfferValue;
            lblEnquiryBags.Text = dashboard.EnquiryValue;
            lblConfirmBags.Text = dashboard.ConfirmValue;
            //lblDraftConfirmationCount.Text = dashboard.draft_confirmation_count;
            lblConfirmationCount.Text = dashboard.ConfirmationCount;
            lblApprovalCount.Text = dashboard.ApprovalCount;
            lblPaymentCount.Text = dashboard.PaymentCount;

            //lblDispatchConfirmCount.Text = dashboard.DispatchConfirmCount;
            //lblDispatchConfirmBags.Text = dashboard.DispatchConfirmValue;
            //lblOverdueCount.Text = dashboard.OverdueDispatchConfirmValue;
            //lblTodayCount.Text = dashboard.TodayDispatchConfirmValue;
            //lblTomorrowCount.Text = dashboard.TomorrowDispatchConfirmValue;
            lblBagsNotReadyCount.Text = dashboard.bags_not_ready_count + " BAGS NR";
            lblBagsNotReadyBags.Text = dashboard.bags_not_ready_value;
            lblPaymentNotReadyCount.Text = dashboard.payment_not_ready_count + " PAYMENT NR";
            lblPaymentNotReadyBags.Text = dashboard.payment_not_ready_value;
            lblPaymentNotReceivedCount.Text = dashboard.payment_not_received_count + " PYMT NOT RCVD";
            lblPaymentNotReceivedBags.Text = dashboard.payment_not_received_value;
            lblTransporterNotReadyCount.Text = dashboard.transporter_not_ready_count + " TRANSPORT NR";
            lblTransporterNotReadyBags.Text = dashboard.transporter_not_ready_value;
            lblPendingDispatchCount.Text = dashboard.dispatched_count + " PROCESSING";
            lblPendingDispatchBags.Text = dashboard.dispatched_value;
            lblPendingInvoiceCount.Text = dashboard.invoice_details_count;
            lblPendingInvoiceBags.Text = dashboard.invoice_details_value;
            lblPendingPaymentCount.Text = dashboard.pending_payment_count;
            lblPendingPaymentBags.Text = dashboard.pending_payment_value;
            lblTransitCount.Text = dashboard.delivered_count;
            lblTransitBags.Text = dashboard.delivered_value;
            lblCustomerAcknowledgementCount.Text = dashboard.acknowledgement_count;
            lblCustomerAcknowledgementBags.Text = dashboard.acknowledgement_value;
            lblDispatchCount.Text = dashboard.DispatchCount;
            lblDeliveryCount.Text = dashboard.DeliveryCount;
            lblProgramApprovalCount.Text = dashboard.ProgramApprovalCount;
            lblFormsCount.Text = dashboard.forms_count;
            lblFormsBags.Text = dashboard.forms_value;
            lblPendingCommissionCount.Text = dashboard.pending_commission_count;
            lblPendingCommissionBags.Text = dashboard.pending_commission_value;
            PieChart.EnableTouchEvents = true;
            PieChart.Chart = new DonutChart { Entries = entries, LabelTextSize = dependency.GetNativeSize(15), IsAnimated = false, AnimationDuration = new TimeSpan(0) };


        }

        private async void Enquiry_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EnquiriesPage());
        }
        private async void PendingConfirmarion_Delay_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DispatchConfirmPage(null, 0, 2));
        }
        private async void PendingConfirmarion_Today_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DispatchConfirmPage(null, 0, 1));
        }
        private async void PendingConfirmarion_Future_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DispatchConfirmPage(null, 0, 3));
        }
        private async void Negotiation_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NegotiationPage());
        }
        private async void Confirmation_Tapped(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new ConfirmationPage());           
            await Navigation.PushAsync(new DispatchConfirmPage(null, 0));
        }

        private void Menu_Tapped(object sender, EventArgs e)
        {

        }
        public bool CloseApp = true;
        protected override bool OnBackButtonPressed()
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

            return CloseApp;
        }

        private async void Logout_Clicked(object sender, EventArgs e)
        {
            bool result = await this.DisplayAlert("Attention!", "Do you want to logout?", "Yes", "No");
            if (result)
            {
                bool doesExist = File.Exists(UserName);
                if (doesExist)
                {
                    File.Delete(UserName);
                    Application.Current.Properties.Clear();
                }
                Application.Current.MainPage = new Login("LogOut");
            }
            else
            {
                return;
            }

        }
        string TimeFlag;
        string TimeFlagTop;
        private void TillDate_Tapped(object sender, EventArgs e)
        {
            lblDaily.FontAttributes = FontAttributes.None;
            lblDaily.TextColor = Color.Black;
            lblMonthly.FontAttributes = FontAttributes.None;
            lblMonthly.TextColor = Color.Black;

            startDatePicker.Date = DateTime.Now.ToLocalTime();
            startDatePicker.IsVisible = false;
            butNext.IsVisible = false;
            butPrevious.IsVisible = false;
            grdDate.IsVisible = true;
            TimeFlag = "Till";
            LoadDashBoard(startDatePicker.Date, TimeFlag);
        }

        private void Monthly_Tapped(object sender, EventArgs e)
        {
            lblDaily.FontAttributes = FontAttributes.None;
            lblDaily.TextColor = Color.Black;
            lblMonthly.FontAttributes = FontAttributes.Bold;
            lblMonthly.TextColor = Color.Orange;
            lblYearly.FontAttributes = FontAttributes.None;
            lblYearly.TextColor = Color.Black;
            startDatePicker.Date = DateTime.Now.ToLocalTime();
            startDatePicker.IsVisible = false;
            grdDate.IsVisible = false;
            startDatePicker.Format = "MMMM, yyyy";
            startDatePicker.IsVisible = true;
            butNext.IsVisible = true;
            butPrevious.IsVisible = true;
            TimeFlag = "Monthly";
            LoadDashBoard(startDatePicker.Date, TimeFlag);
        }

        private void Daily_Tapped(object sender, EventArgs e)
        {
            lblDaily.FontAttributes = FontAttributes.Bold;
            lblDaily.TextColor = Color.Orange;
            lblMonthly.FontAttributes = FontAttributes.None;
            lblMonthly.TextColor = Color.Black;
            lblYearly.FontAttributes = FontAttributes.None;
            lblYearly.TextColor = Color.Black;
            startDatePicker.Date = DateTime.Now.ToLocalTime();
            startDatePicker.IsVisible = false;
            grdDate.IsVisible = false;
            startDatePicker.Format = "dd MMM yyyy";
            startDatePicker.IsVisible = true;
            butNext.IsVisible = true;
            butPrevious.IsVisible = true;
            TimeFlag = "Daily";
            LoadDashBoard(startDatePicker.Date, TimeFlag);
        }

        private void startDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            LoadDashBoard(startDatePicker.Date, TimeFlag);
        }


        private void PreviousDate_Clicked(object sender, EventArgs e)
        {
            startDatePicker.IsVisible = false;
            if (TimeFlag == "Daily")
                startDatePicker.Date = startDatePicker.Date.AddDays(-1);
            else if (TimeFlag == "Monthly")
                startDatePicker.Date = startDatePicker.Date.AddMonths(-1);
            else if (TimeFlag == "Yearly")
                startDatePicker.Date = startDatePicker.Date.AddYears(-1);
            startDatePicker.IsVisible = true;
        }

        private void NextDate_Clicked(object sender, EventArgs e)
        {
            startDatePicker.IsVisible = false;
            if (TimeFlag == "Daily")
                startDatePicker.Date = startDatePicker.Date.AddDays(1);
            else if (TimeFlag == "Monthly")
                startDatePicker.Date = startDatePicker.Date.AddMonths(1);
            else if (TimeFlag == "Yearly")
                startDatePicker.Date = startDatePicker.Date.AddYears(1);
            startDatePicker.IsVisible = true;
        }
        private void butAttendencePreviousDate_Clicked(object sender, EventArgs e)
        {
            startAttendenceDatePicker.IsVisible = false;
            startAttendenceDatePicker.Date = startAttendenceDatePicker.Date.AddMonths(-1);
            startAttendenceDatePicker.IsVisible = true;
        }

        private void butAttendenceNextDate_Clicked(object sender, EventArgs e)
        {
            startAttendenceDatePicker.IsVisible = false;
            startAttendenceDatePicker.Date = startAttendenceDatePicker.Date.AddMonths(1);
            startAttendenceDatePicker.IsVisible = true;
        }

        private void TopPreviousDate_Tapped(object sender, EventArgs e)
        {
            topDatePicker.IsVisible = false;
            if (TimeFlagTop == "Daily")
                topDatePicker.Date = topDatePicker.Date.AddDays(-1);
            else if (TimeFlagTop == "Monthly")
                topDatePicker.Date = topDatePicker.Date.AddMonths(-1);
            topDatePicker.IsVisible = true;
        }

        private void TopNextDate_Tapped(object sender, EventArgs e)
        {
            topDatePicker.IsVisible = false;
            if (TimeFlagTop == "Daily")
                topDatePicker.Date = topDatePicker.Date.AddDays(1);
            else if (TimeFlagTop == "Monthly")
                topDatePicker.Date = topDatePicker.Date.AddMonths(1);
            topDatePicker.IsVisible = true;
        }

        private void TopEnDaily_Tapped(object sender, EventArgs e)
        {
            lblTopEnDaily.FontAttributes = FontAttributes.Bold;
            lblTopEnDaily.TextColor = Color.Orange;
            lblTopEnMonthly.FontAttributes = FontAttributes.None;
            lblTopEnMonthly.TextColor = Color.Black;
            lblTopEnTillDate.FontAttributes = FontAttributes.None;
            lblTopEnTillDate.TextColor = Color.Black;
            topDatePicker.Date = DateTime.Now.ToLocalTime();
            topDatePicker.IsVisible = false;
            grdTopEnDate.IsVisible = false;
            topDatePicker.Format = "dd MMM yyyy";
            topDatePicker.IsVisible = true;
            butNextTop.IsVisible = true;
            butPreviousTop.IsVisible = true;
            TimeFlagTop = "Daily";
            LoadTopDashBoard(topDatePicker.Date, TimeFlagTop);
        }

        private void TopEnMonthly_Tapped(object sender, EventArgs e)
        {
            lblTopEnDaily.FontAttributes = FontAttributes.None;
            lblTopEnDaily.TextColor = Color.Black;
            lblTopEnMonthly.FontAttributes = FontAttributes.Bold;
            lblTopEnMonthly.TextColor = Color.Orange;
            lblTopEnTillDate.FontAttributes = FontAttributes.None;
            lblTopEnTillDate.TextColor = Color.Black;
            topDatePicker.Date = DateTime.Now.ToLocalTime();
            topDatePicker.IsVisible = false;
            grdTopEnDate.IsVisible = false;
            topDatePicker.Format = "MMMM, yyyy";
            topDatePicker.IsVisible = true;
            butNextTop.IsVisible = true;
            butPreviousTop.IsVisible = true;
            TimeFlagTop = "Monthly";
            LoadTopDashBoard(topDatePicker.Date, TimeFlagTop);
        }

        private void TopEnTillDate_Tapped(object sender, EventArgs e)
        {
            lblTopEnDaily.FontAttributes = FontAttributes.None;
            lblTopEnDaily.TextColor = Color.Black;
            lblTopEnMonthly.FontAttributes = FontAttributes.None;
            lblTopEnMonthly.TextColor = Color.Black;
            lblTopEnTillDate.FontAttributes = FontAttributes.Bold;
            lblTopEnTillDate.TextColor = Color.Orange;
            topDatePicker.Date = DateTime.Now.ToLocalTime();
            topDatePicker.IsVisible = false;
            butNextTop.IsVisible = false;
            butPreviousTop.IsVisible = false;
            grdTopEnDate.IsVisible = true;
            TimeFlagTop = "Till";
            LoadTopDashBoard(topDatePicker.Date, TimeFlagTop);
        }

        private void topDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            LoadTopDashBoard(topDatePicker.Date, TimeFlagTop);
        }

        private async void Approval_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ApprovalPage());
        }

        private async void Payment_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PaymentPage());
        }

        private async void Dispatch_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DispatchPage());
        }

        private async void DispatchConfirm_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DispatchConfirmPage(null, 1));
        }

        private async void Delivery_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DeliveryPage());
        }

        private async void DraftConfirmation_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DraftConfirmationPage());
        }

        private async void BagNotReady_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DispatchConfirmPage(null, 2));
        }

        private async void PaymentNotReady_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DispatchConfirmPage(null, 3));
        }

        private async void TransporterNotReady_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DispatchConfirmPage(null, 4));
        }

        private async void PendingDispatch_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DispatchConfirmPage(null, 5));
        }
        private async void PendingInvoice_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DispatchConfirmPage(null, 6));
        }

        private async void Transit_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DispatchConfirmPage(null, 7));
        }

        private void Overdue_Clicked(object sender, EventArgs e)
        {
            LoadDashBoard(startDatePicker.Date, TimeFlag);
        }

        private async void ProgramApproval_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DispatchConfirmPage(null, 8));
        }

        private async void CustomerAcknowledgement_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DispatchConfirmPage(null, 9));
        }

        private async void PaymentNotReceived_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DispatchConfirmPage(null, 10));
        }

        private async void PendingPayment_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DispatchConfirmPage(null, 11));
        }

        private async void Forms_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DispatchConfirmPage(null, 12));
        }



        private async void CommissionInvoice_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CommissionInvoicePage(null));
        }

        private async void CommissionReceipt_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CommissionReceiptPage());
        }

        private async void PendingCommission_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DispatchConfirmPage(null, 13));
        }

        private async void AfterDispatch_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DispatchConfirmPage(null, 14));
        }

        private async void CallHistory_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CallHistoryPage());
        }

        private async void Attendence_Tapped(object sender, EventArgs e)
        {
            bool show_attendance_summary = (Application.Current.Properties["mobile_dashboard_all_user_attendance"].ToString() == "0" ? false : true);
            if (show_attendance_summary == false)
            {
                attendenceDashboard.User_Id = (int)Application.Current.Properties["user_id"];
                await Navigation.PushAsync(new AttendanceListPage(attendenceDashboard));
            }
            else
            {
                //attendenceDashboard.User_Id = Approval.AttendanceTeamNameId;
                await Navigation.PushAsync(new AttendanceSummary(attendenceDashboard));

            }

        }
        private async void TxtTeamName_Focused(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AttendanceUserList(Approval));
        }
        private async void ButTeamNameClear_Clicked(object sender, EventArgs e)
        {
            //txtTeamName.Text = null;
            //Approval.AttendanceTeamNameId = 0;
            //Approval.AttendanceTeamName = null;
            //butTeamNameClear.IsVisible = false;
            //toldays.Text = "0";
            //fuldays.Text = "0";
            //halfdays.Text = "0";
            //absent.Text = "0";
            //permission.Text = "0";
            //late.Text = "0";
        }
        private void startAttendenceDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            LoadAttendenceDashBoard(startAttendenceDatePicker.Date, Approval.AttendanceTeamNameId);
            AttendanceDate = startAttendenceDatePicker.Date;
        }
        private void Yearly_Tapped(object sender, EventArgs e)
        {
            lblDaily.FontAttributes = FontAttributes.None;
            lblDaily.TextColor = Color.Black;
            lblMonthly.FontAttributes = FontAttributes.None;
            lblMonthly.TextColor = Color.Black;
            lblYearly.FontAttributes = FontAttributes.Bold;
            lblYearly.TextColor = Color.Orange;

            startDatePicker.Date = DateTime.Now.ToLocalTime();
            startDatePicker.IsVisible = false;
            butNext.IsVisible = true;
            butPrevious.IsVisible = true;
            grdDate.IsVisible = false;

            startDatePicker.Format = "yyyy";
            startDatePicker.IsVisible = true;
            TimeFlag = "Yearly";
            LoadDashBoard(startDatePicker.Date, TimeFlag);
        }

        private void txtTeamName_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadAttendenceDashBoard(startAttendenceDatePicker.Date, Approval.AttendanceTeamNameId);
        }

     

        private async void CompanyBagAndCommissionDetails_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PerformancePage(1, PerformanceData));
        }

        private async void TeamBagAndCommissionDetails_Tapped(object sender, EventArgs e)
        {

            if (Application.Current.Properties["team_group_id"] != null)
            {
                await Navigation.PushAsync(new PerformancePage(2, TeamPerformanceData));
            }
            else
            {
                await Navigation.PushAsync(new PerformancePage(2, PerformanceData));
            }

        }
      
    }
}
