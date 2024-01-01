using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using yarn_brokerage.Models;
using yarn_brokerage.Services;
using System.IO;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Essentials;

namespace yarn_brokerage.Views
{
    public partial class Login : ContentPage
    {
        User user { get; set; }

        string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "remember.txt");

        public Login(string ex ="")
        {
            InitializeComponent();
            //Application.Current.Properties["AttendenceURL"] = "http://devyarnpay.adwayit.in/";
            //Application.Current.Properties["AttendenceURL"] = "https://yarnpay.adwayit.com/";
            Application.Current.Properties["HOSTURL"] = "https://yarn.adwayit.com/";
            Application.Current.SavePropertiesAsync();
            //var todoLogin = (TodoLogin)BindingContext;
            //var count = App.Database.CountItemAsync();        
            user = new User();
            RdoRememberMe.IsChecked = true;
            bool doesExist = File.Exists(fileName);
            if (doesExist)
            {                
                Username.Text = File.ReadAllText(fileName);
                string[] credentials = File.ReadAllText(fileName).Split(';');
                try
                {
                    Username.Text = (string.IsNullOrWhiteSpace(credentials[0])) ? "" : credentials[0];
                    Password.Text = (string.IsNullOrWhiteSpace(credentials[1])) ? "" : credentials[1];
                    if (ex == "")
                        prelogin();
                }
                catch (IndexOutOfRangeException e)
                {

                }
            }
            //BindingContext = Data;
            Username.ReturnCommand = new Command(() => Password.Focus());
            //if (ex == "")
            //    prelogin();
            
        }
        private async void prelogin()
        {
            pnlLogin.IsVisible = false;
            NavigationPage.SetHasNavigationBar(this, false);
            //BindingContext = Data;
            validation(null, null);
            //await LoadCallLog();
            //Data = await App.Database.GetItemsAsync();
            //if (Data != null)
            //{               
            //    if (Data.LogInFlag == true)
            //    {
            //        pnlLogin.IsVisible = false;
            //        NavigationPage.SetHasNavigationBar(this, false);
            //        BindingContext = Data;
            //        validation(null, null);
            //    }
            //}            
        }
        public object JsonConvert { get; private set; }

        protected async void Registration_Clicked(object sender, EventArgs e)
        {
            ////Navigation.PushAsync(new webview("Points"));
            //if (Device.OS == TargetPlatform.iOS)
            //{
            //    var navigationPage = new NavigationPage(new RegistrationPage());
            //    await Navigation.PushModalAsync(navigationPage, true); 
            //}
            //else
            //{
            //    //Device.OpenUri(new Uri("http://taogps.aptinfoway.com:2280/#/api/tao_application"));
            //    await Browser.OpenAsync(new Uri("http://taogps.aptinfoway.com:2280/#/api/tao_application"), BrowserLaunchMode.SystemPreferred);
            //}
        }

        private async void validation(object sender, EventArgs e)
        {
            var username = Username.Text;
            var password = Password.Text;
            //if (string.IsNullOrWhiteSpace(Data.ClinicLocationName))
            //{
            //    await DisplayAlert("Alert", "Please Select Clinic Location", "OK");
            //}
            //else  !string.IsNullOrWhiteSpace(Data.ClinicLocationName) &&
            if (string.IsNullOrWhiteSpace(username) && string.IsNullOrWhiteSpace(password))
            {
                pnlLogin.IsVisible = true;
                await DisplayAlert("Alert", "Enter login credentials", "OK");
            }
            else if (string.IsNullOrWhiteSpace(username))
            {
                pnlLogin.IsVisible = true;
                await DisplayAlert("Alert", "Enter Username", "OK");
            }
            else if (string.IsNullOrWhiteSpace(password))
            {
                pnlLogin.IsVisible = true;
                await DisplayAlert("Alert", "Enter Password", "OK");
            }
            else if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {               
                try
                {
                    var current = Xamarin.Essentials.Connectivity.NetworkAccess;

                    if (current == Xamarin.Essentials.NetworkAccess.Internet)
                    {
                        using (var cl = new HttpClient())
                        {
                            var formcontent = new FormUrlEncodedContent(new[]
                            {
                                    new KeyValuePair<string,string>("user_name",username),
                                    new KeyValuePair<string, string>("password",password),
                                    new KeyValuePair<string, string>("version",lblVersion.Text)
                            });
                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "user_signin", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            User_list res = Newtonsoft.Json.JsonConvert.DeserializeObject<User_list>(response);

                            if (res.message == "Success")
                            {
                                if (res.user.user_type != null)
                                {
                                    

                                    Application.Current.Properties["username"] = res.user.user_name;
                                    Application.Current.Properties["user_id"] = res.user.id;
                                    Application.Current.Properties["user_type"] = res.user.user_type;
                                    Application.Current.Properties["team_group_id"] = res.user.TeamGroupId;
                                    Application.Current.Properties["link_with_attendence_system"] = res.user.link_with_attendence_system;
                                    Application.Current.Properties["trace_calls"] = res.user.trace_calls;

                                    //DASHBOARD USER PERMISSION
                                    Application.Current.Properties["mobile_dashboard_enquiry"] = res.ModulePermission.MobileDashboard.MobileDashboardEnquiry;
                                    Application.Current.Properties["mobile_dashboard_attendance"] = res.ModulePermission.MobileDashboard.MobileDashboardAttendance;
                                    Application.Current.Properties["mobile_dashboard_approval"] = res.ModulePermission.MobileDashboard.MobileDashboardApproval;
                                    Application.Current.Properties["mobile_dashboard_bags_performance"] = res.ModulePermission.MobileDashboard.MobileDashboardBagPermission;
                                    Application.Current.Properties["mobile_dashboard_commission_performance"] = res.ModulePermission.MobileDashboard.MobileDashboardCommissionPermission;
                                    Application.Current.Properties["mobile_dashboard_current_plan"] = res.ModulePermission.MobileDashboard.MobileDashboardCurrentPlan;
                                    Application.Current.Properties["mobile_dashboard_dispatched"] = res.ModulePermission.MobileDashboard.MobileDashboardDispatched;
                                    Application.Current.Properties["mobile_dashboard_all_user_attendance"] = res.ModulePermission.MobileDashboard.MobileDashboardAllUserAttendance;
                                    Application.Current.Properties["mobile_dashboard_pending_confirmation"] = res.ModulePermission.MobileDashboard.MobileDashboardPendingConfirmation;
                                    Application.Current.Properties["mobile_dashboard_show_team_group_data"] = res.ModulePermission.MobileDashboard.MobileDashboardShowTeamGroupData;

                                    //MENU USER PERMISSION
                                    Application.Current.Properties["mobile_transaction_home_visible"] = res.ModulePermission.MobileTransactionHome.Visible;
                                    Application.Current.Properties["mobile_transaction_offers_enquiry_visible"] = res.ModulePermission.MobileTransactionOffersEnquiry.Visible;
                                    Application.Current.Properties["mobile_transaction_draft_confirmation_visible"] = res.ModulePermission.MobileTransactionDraftConfirmation.Visible;
                                    Application.Current.Properties["mobile_transaction_pending_approval_visible"] = res.ModulePermission.MobileTransactionPendingApproval.Visible;
                                    Application.Current.Properties["mobile_transaction_pending_confirmation_visible"] = res.ModulePermission.MobileTransactionPendingConfirmation.Visible;
                                    Application.Current.Properties["mobile_transaction_program_approval_visible"] = res.ModulePermission.MobileTransactionProgramApproval.Visible;
                                    Application.Current.Properties["mobile_transaction_current_plan_visible"] = res.ModulePermission.MobileTransactionCurrentPlan.Visible;
                                    Application.Current.Properties["mobile_transaction_dispatched_visible"] = res.ModulePermission.MobileTransactionDispatched.Visible;
                                    Application.Current.Properties["mobile_transaction_reports_visible"] = res.ModulePermission.MobileTransactionReports.Visible;
                                    Application.Current.Properties["mobile_transaction_attendance_summary"] = res.ModulePermission.MobileTransactionAttendanceSummary.Visible;


                                    Application.Current.Properties["transaction_offers_enquiry_InsertAllowed"] = res.ModulePermission.TransactionOffersEnquiry.InsertAllowed;
                                    Application.Current.Properties["transaction_offers_enquiry_UpdateAllowed"] = res.ModulePermission.TransactionOffersEnquiry.UpdateAllowed;
                                    Application.Current.Properties["transaction_draft_confirmation_InsertAllowed"] = res.ModulePermission.TransactionDraftConfirmation.InsertAllowed;
                                    Application.Current.Properties["transaction_draft_confirmation_UpdateAllowed"] = res.ModulePermission.TransactionDraftConfirmation.UpdateAllowed;
                                    Application.Current.Properties["transaction_draft_confirmation"] = res.ModulePermission.TransactionDraftConfirmation.ApproveAllowed;
                                    Application.Current.Properties["transaction_draft_confirmation_FindAllowed"] = res.ModulePermission.TransactionDraftConfirmation.FindAllowed;
                                    Application.Current.Properties["transaction_pending_approval_ApproveAllowed"] = res.ModulePermission.TransactionPendingApproval.ApproveAllowed;
                                    Application.Current.Properties["transaction_pending_confirmation_UpdateAllowed"] = res.ModulePermission.TransactionPendingConfirmation.UpdateAllowed;
                                    Application.Current.Properties["transaction_pending_confirmation_FindAllowed"] = res.ModulePermission.TransactionPendingConfirmation.FindAllowed;
                                    Application.Current.Properties["transaction_program_approval_ApproveAllowed"] = res.ModulePermission.TransactionProgramApproval.ApproveAllowed;
                                    Application.Current.Properties["transaction_program_approval_FindAllowed"] = res.ModulePermission.TransactionProgramApproval.FindAllowed;
                                    Application.Current.Properties["transaction_current_plan_FindAllowed"] = res.ModulePermission.TransactionCurrentPlan.FindAllowed;
                                    Application.Current.Properties["transaction_current_plan_UpdateAllowed"] = res.ModulePermission.TransactionCurrentPlan.UpdateAllowed;
                                    Application.Current.Properties["transaction_dispatched_UpdateAllowed"] = res.ModulePermission.TransactionDispatched.UpdateAllowed;
                                    Application.Current.Properties["transaction_dispatched_FindAllowed"] = res.ModulePermission.TransactionDispatched.FindAllowed;





                                    await Application.Current.SavePropertiesAsync();
                                    Application.Current.MainPage = new MainPage();
                                }
                                else
                                {
                                    pnlLogin.IsVisible = true;
                                    await DisplayAlert("Alert", "Invalid User.\n\nPlease contact administrator for further help.", "OK");
                                }
                            }
                            else
                            {
                                pnlLogin.IsVisible = true;
                                await DisplayAlert("Alert", res.message, "OK");                               
                            }
                        }
                    }
                    else
                    {
                        LocalLogin(username, password);
                    }
                }
                catch(Exception ex)
                {
                    LocalLogin(username, password);
                }
                bool doesExist = File.Exists(fileName);
                if (doesExist)
                {
                    File.Delete(fileName);
                }
                if (RdoRememberMe.IsChecked == true)
                {
                    File.WriteAllText(fileName, username + ";" + password + ";");
                }
            }
        }

        
        //public async Task LoadCallLog()
        //{
        //    activity_indicator.IsRunning = true;
        //    activity_indicator.IsVisible = true;

        //    try
        //    {
        //        var statusContact = PermissionStatus.Unknown;
        //        var statusPhone = PermissionStatus.Unknown;
        //        statusContact = await CrossPermissions.Current.CheckPermissionStatusAsync<ContactsPermission>();
        //        statusPhone = await CrossPermissions.Current.CheckPermissionStatusAsync<PhonePermission>();
        //        if (statusContact != PermissionStatus.Granted || statusPhone != PermissionStatus.Granted)
        //        {
        //            //if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Contacts))
        //            //{
        //            //    await DisplayAlert("Yarn Brokerage", "permissions are required to access contacts", "OK");
        //            //}

        //            //PermissionStatus status = await CrossPermissions.Current.RequestPermissionAsync<ContactsPermission>();
        //            //Best practice to always check that the key exists
        //            //if (results.ContainsKey(Permission.Contacts))
        //            //statusContact = results[Permission.Contacts];
        //            //if (results.ContainsKey(Permission.Phone))
        //            //    statusPhone = results[Permission.Phone];
        //        }

        //        if (statusContact == Plugin.Permissions.Abstractions.PermissionStatus.Granted && statusPhone == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
        //        {
        //            DependencyService.Get<IStartService>().StartForegroundServiceCompat();
        //        }
        //        else if (statusContact != Plugin.Permissions.Abstractions.PermissionStatus.Unknown || statusPhone == Plugin.Permissions.Abstractions.PermissionStatus.Unknown)
        //        {
        //            await DisplayAlert("Yarn Brokerage", "Permission was denied. We cannot continue, please try again.", "OK");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        activity_indicator.IsRunning = false;
        //        activity_indicator.IsVisible = false;

        //        await DisplayAlert("Call Log", "A problem has occurred, contact customer support.Technical report: " + ex.Message, "OK");
        //    }
        //    finally
        //    {
        //        activity_indicator.IsRunning = false;
        //        activity_indicator.IsVisible = false;
        //    }
        //}

        private async void LocalLogin(string username, string password)
        {
            //if (Data != null)
            //{
            //    if (username == Data.UserName && password == Data.Password)
            //    {
            //        Application.Current.Properties["username"] = Data.UserName;
            //        Application.Current.Properties["name"] = Data.Name;
            //        Application.Current.Properties["password"] = Data.Password;
            //        Application.Current.Properties["URL"] = Data.URL;
            //        Application.Current.Properties["member_type"] = Data.MemberType;
            //        Application.Current.Properties["ClicniLocationId"] = Data.ClinicLocationId;
            //        Application.Current.Properties["ClicniLocationName"] = Data.ClinicLocationName;
            //        Data.LogInFlag = true;                    
            //        //await App.Database.SaveItemAsync(Data);
            //        Application.Current.MainPage = new MainPage();
            //    }
            //    else
            //    {
            //        await DisplayAlert("Alert", "Check your internet connection and try again", "OK");
            //    }
            //}
            //else
            //{
            //    await DisplayAlert("Alert", "Check your internet connection and try again", "OK");
            //}
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var dependency = DependencyService.Get<INativeFont>();
            await dependency.GrandPermission("All");
            //if (user != null)
            //    Username.Text = user.user_name;
        }

        private async void TxtUsername_Focused(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new UserListPage(user)));
        }
    }
}
