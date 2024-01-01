using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using Plugin.Permissions;
using Plugin.CurrentActivity;
using Android.Support.V4.Content;
using Android.Support.V4.App;
using Android;

namespace yarn_brokerage.Droid
{
    [Activity(Label = "Yarn Brokerage", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation =ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        //public static Context Instance { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this);
            //Rg.Plugins.Popup.Popup.Init(this,savedInstanceState);
            //Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);            
            Plugin.InputKit.Platforms.Droid.Config.Init(this, savedInstanceState);
            XamEffects.Droid.Effects.Init();
            LoadApplication(new App());
            //if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadCallLog) != Android.Content.PM.Permission.Granted)
            //{
            //    ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.ReadCallLog }, 1);
            //}
            //if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadContacts) != Android.Content.PM.Permission.Granted)
            //{
            //    ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.ReadContacts }, 1);
            //}            
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            //Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            //base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public override void OnBackPressed()
        {
            Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed);
        }
    }
}