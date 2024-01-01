using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using yarn_brokerage.Services;
using yarn_brokerage.Views;

namespace yarn_brokerage
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //Application.Current.Properties.Clear();
            DependencyService.Register<MockDataStore>();
            NavigationPage navPage = new NavigationPage
            {
                BarBackgroundColor = Color.FromHex("#1FBED6"),
                BarTextColor = Color.FromHex("#000000")
            };
            MainPage = new Login(); //new NavigationPage(new Login());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
