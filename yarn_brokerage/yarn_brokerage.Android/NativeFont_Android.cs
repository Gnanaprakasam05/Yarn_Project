using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using yarn_brokerage.Droid;
using yarn_brokerage.Services;
using Android.Util;
using Android.Support.V4.Content;
using Android.Support.V4.App;
using Android;
using System.Threading.Tasks;
using Plugin.CurrentActivity;

[assembly: Dependency(typeof(NavtiveFont_Android))]
namespace yarn_brokerage.Droid
{
    public class NavtiveFont_Android : INativeFont
    {
        public float GetNativeSize(float size)
        {
            var displayMetrics = Android.App.Application.Context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, size, displayMetrics);
        }

        public async Task GrandPermission(string Permission)
        {
            //var thisActivity =  Android.App.Application.Context as Activity;
            var context = CrossCurrentActivity.Current.AppContext;
            var activity = CrossCurrentActivity.Current.Activity;
            if (Permission == "All")
            {
                if (ContextCompat.CheckSelfPermission(context, Manifest.Permission.ReadContacts) != Android.Content.PM.Permission.Granted && ContextCompat.CheckSelfPermission(context, Manifest.Permission.ReadCallLog) != Android.Content.PM.Permission.Granted)
                {
                    ActivityCompat.RequestPermissions(activity, new String[] { Manifest.Permission.ReadContacts, Manifest.Permission.ReadCallLog }, 1);
                }
                else if (ContextCompat.CheckSelfPermission(context, Manifest.Permission.ReadContacts) != Android.Content.PM.Permission.Granted)
                {
                    ActivityCompat.RequestPermissions(activity, new String[] { Manifest.Permission.ReadContacts }, 1);
                }
                else if (ContextCompat.CheckSelfPermission(context, Manifest.Permission.ReadCallLog) != Android.Content.PM.Permission.Granted)
                {
                    ActivityCompat.RequestPermissions(activity, new String[] { Manifest.Permission.ReadCallLog }, 1);
                }
            }
            if (Permission == "Contacts")
            {
                if (ContextCompat.CheckSelfPermission(context, Manifest.Permission.ReadContacts) != Android.Content.PM.Permission.Granted)
                {
                    ActivityCompat.RequestPermissions(activity, new String[] { Manifest.Permission.ReadContacts }, 1);
                }
            }
            if (Permission == "CallLog")
            {
                if (ContextCompat.CheckSelfPermission(context, Manifest.Permission.ReadCallLog) != Android.Content.PM.Permission.Granted)
                {
                    ActivityCompat.RequestPermissions(activity, new String[] { Manifest.Permission.ReadCallLog }, 1);
                }
            }
        }
    }
}