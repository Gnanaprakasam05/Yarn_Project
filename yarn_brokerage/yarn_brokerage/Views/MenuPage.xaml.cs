using yarn_brokerage.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using Microcharts;
using Entry = Microcharts.ChartEntry;
using SkiaSharp;
using yarn_brokerage.Services;
using yarn_brokerage.ViewModels;
using System.Threading;

namespace yarn_brokerage.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]    
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage;}
        List<HomeMenuItem> menuItems;
        
        public MenuPage()
        {
            InitializeComponent();
            
            
            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Home, Title="Home" },
                new HomeMenuItem {Id = MenuItemType.OffersEnquiries, Title="Offers / Enquiries" },
                new HomeMenuItem {Id = MenuItemType.DraftConfirmation, Title="Draft Confirmation" },
                new HomeMenuItem {Id = MenuItemType.PendingApproval, Title="Pending Approval" },
                new HomeMenuItem {Id = MenuItemType.PendingConfirmation, Title="Pending Confirmation" },
                new HomeMenuItem {Id = MenuItemType.ProgramApproval, Title="Program Approval" },
                new HomeMenuItem {Id = MenuItemType.CurrentPlan, Title="Current Plan" },
                new HomeMenuItem {Id = MenuItemType.Dispatched, Title="Dispatched" },
           
                //new HomeMenuItem {Id = MenuItemType.CommissionInvoice, Title="Commission Invoice" },
                //new HomeMenuItem {Id = MenuItemType.CommissionReceipt, Title="Commission Receipt" },
                //new HomeMenuItem {Id = MenuItemType.CallHistory, Title="Call History" },
                new HomeMenuItem {Id = MenuItemType.Reports, Title="Reports" },
                new HomeMenuItem {Id = MenuItemType.Logout, Title="Logout" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                ListViewMenu.SelectedItem = null;
                await RootPage.NavigateFromMenu(id);
            };
        }

        [Obsolete]
        private void Website_Tapped(object sender, EventArgs e)
        {
            var url = "http://adwayit.com/";
            Device.OpenUri(new Uri(url));
        }
    }
}