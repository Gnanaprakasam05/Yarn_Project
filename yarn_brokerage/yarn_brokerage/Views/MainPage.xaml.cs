using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using yarn_brokerage.Models;
using yarn_brokerage.Services;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.IO;
namespace yarn_brokerage.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        string UserName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "remember.txt");
        public MainPage()
        {
            InitializeComponent();
            if (Application.Current.Properties["trace_calls"].ToString() == "1")
                LoadCallLog();
            if (Device.Idiom == TargetIdiom.Phone)
            {
                this.MasterBehavior = MasterBehavior.Split;
            }
            else
            {
                this.MasterBehavior = MasterBehavior.Popover;
            }
            MenuPages.Add((int)MenuItemType.Home, (NavigationPage)Detail);
        }

       
        public async void LoadCallLog()
        {
            //activity_indicator.IsRunning = true;
            //activity_indicator.IsVisible = true;

            try
            {
                var statusContact = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Contacts);
                var statusPhone = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Phone);
                if (statusContact != PermissionStatus.Granted || statusPhone != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Contacts))
                    {
                        await DisplayAlert("Yarn Brokerage", "permissions are required to access contacts", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Contacts, Permission.Phone);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Contacts))
                        statusContact = results[Permission.Contacts];
                    if (results.ContainsKey(Permission.Phone))
                        statusPhone = results[Permission.Phone];
                }

                if (statusContact == PermissionStatus.Granted && statusPhone == PermissionStatus.Granted)
                {
                    DependencyService.Get<IStartService>().StartForegroundServiceCompat();
                }
                else if (statusContact != PermissionStatus.Unknown || statusPhone == PermissionStatus.Unknown)
                {
                    await DisplayAlert("Yarn Brokerage", "Permission was denied. We cannot continue, please try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                //activity_indicator.IsRunning = false;
                //activity_indicator.IsVisible = false;

                await DisplayAlert("Call Log", "A problem has occurred, contact customer support.Technical report: " + ex.Message, "OK");
            }
            finally
            {
                //activity_indicator.IsRunning = false;
                //activity_indicator.IsVisible = false;
            }
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Home:
                        MenuPages.Add(id, new NavigationPage(new HomePage()));
                        break;
                    case (int)MenuItemType.OffersEnquiries:
                        MenuPages.Add(id, new NavigationPage(new EnquiriesPage()));
                        break;
                    case (int)MenuItemType.DraftConfirmation:
                        MenuPages.Add(id, new NavigationPage(new DraftConfirmationPage(null)));
                        break;
                    case (int)MenuItemType.PendingApproval:
                        MenuPages.Add(id, new NavigationPage(new ApprovalPage()));
                        break;
                    case (int)MenuItemType.PendingConfirmation:
                        MenuPages.Add(id, new NavigationPage(new DispatchConfirmPage(null, 0)));
                        break;
                    case (int)MenuItemType.ProgramApproval:
                        MenuPages.Add(id, new NavigationPage(new DispatchConfirmPage(null, 8)));
                        break;
                    case (int)MenuItemType.CurrentPlan:
                        MenuPages.Add(id, new NavigationPage(new DispatchConfirmPage(null, 1)));
                        break;
                    case (int)MenuItemType.Dispatched:
                        MenuPages.Add(id, new NavigationPage(new DispatchConfirmPage(null, 14)));
                        break;
                    //case (int)MenuItemType.CommissionInvoice:
                    //    MenuPages.Add(id, new NavigationPage(new CommissionInvoicePage(null)));
                    //    break;
                    //case (int)MenuItemType.CommissionReceipt:
                    //    MenuPages.Add(id, new NavigationPage(new CommissionReceiptPage()));
                    //    break;
                    //case (int)MenuItemType.CallHistory:
                    //    MenuPages.Add(id, new NavigationPage(new CallHistoryPage()));
                    //    break;
                    case (int)MenuItemType.Reports:
                        MenuPages.Add(id, new NavigationPage(new ReportsPage()));
                        break;
                    case (int)MenuItemType.Logout:
                        IsPresented = false;
                        bool result = await this.DisplayAlert("Attention!", "Do you want to logout?", "Yes", "No");
                        if (result)
                        {
                            bool doesExist = File.Exists(UserName);
                            if (doesExist)
                            {
                                File.Delete(UserName);
                                Application.Current.Properties.Clear();
                            }
                            MenuPages.Add(id, new NavigationPage(new Login("LogOut")));
                        }
                        else
                        {
                            return;
                        }
                        break;
                }
            }
           
               
            
            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
            else
            {
                IsPresented = false;
            }
        }
    }
}