using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using yarn_brokerage.Droid;
using yarn_brokerage.Services;
using yarn_brokerage.Models;
using yarn_brokerage.ViewModels;
using Android.Provider;
using System.Threading;
using System.IO;
using Plugin.CurrentActivity;

[assembly: Dependency(typeof(StartService_Android))]
namespace yarn_brokerage.Droid
{
    public class StartService_Android : IStartService
    {
        public void StartForegroundServiceCompat()
        {
            var currentContext = CrossCurrentActivity.Current.AppContext;//Android.App.Application.Context;
            var intent = new Intent(currentContext, typeof(myLocationService));


            /*if (Android.OS.Build.VERSION.SdkInt > Android.OS.BuildVersionCodes.P)
            {
                currentContext.StartForegroundService(intent);
            }
            else
            {*/
                currentContext.StartService(intent);
            //}
        }
    }

    [Service]
    public class myLocationService : Service
    {
        //static readonly string TAG = "X:" + typeof(SimpleStartedService).Name;
        static readonly int TimerWait = 10000;
        Timer timer;
        DateTime startTime;
        bool isStarted = false;
        bool flag = false;

        public override void OnCreate()
        {
            base.OnCreate();
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
            //return base.OnBind(intent);
        }

        string fileName = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "call_tick.txt");
        string call_tick = "";

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            // Code not directly related to publishing the notification has been omitted for clarity.
            // Normally, this method would hold the code to be run when the service is started.
            //private static Timer timer = new Timer();
            //timer.scheduleAtFixedRate(new mainTask(), 0, 5000);
            //return StartCommandResult.NotSticky;
                        
            if (isStarted)
            {
                TimeSpan runtime = DateTime.UtcNow.Subtract(startTime);            
            }
            else
            {
                startTime = DateTime.UtcNow;                
                timer = new Timer(CallHistory, startTime, 0, TimerWait);
                isStarted = true;
            }
            return StartCommandResult.NotSticky;
        }
        
        void CallHistory(object state)
        {
            if (flag == false)
            {
                flag = true;
                CallLogViewModel callLogViewModel = new CallLogViewModel();
                bool doesExist = File.Exists(fileName);
                if (doesExist)
                {
                    call_tick = File.ReadAllText(fileName);
                }
                if (call_tick == "")
                {
                    string formatString = ((DateTime.UtcNow.Date.Ticks) - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks).ToString();
                    call_tick = formatString.Remove(formatString.Length-4);
                }
                    //call_tick = (DateTime.UtcNow.Date.Ticks - new DateTime(1970, 1, 1,0,0,0).Ticks).ToString();

                    //call_tick = DateTime.Now.Ticks.ToString();
                    //Write want you want to do here
                var phoneContacts = new List<CallLogModel>();
                // filter in desc order limit by no
                string querySorter = String.Format("{0} asc ", CallLog.Calls.Date);

                String where = CallLog.Calls.Type + " = ?";
                if (call_tick != "")
                    where = where + " AND " + CallLog.Calls.Date + " > ? ";

                string[] args = { 2.ToString() };
                if (call_tick != "")
                {
                    Array.Resize(ref args, 2);
                    args[1] = call_tick;
                }
                using (var phones = Android.App.Application.Context.ContentResolver.Query(CallLog.Calls.ContentUri, null, where, args, querySorter))
                {
                    if (phones != null)
                    {
                        while (phones.MoveToNext())
                        {
                            try
                            {
                                string callNumber = phones.GetString(phones.GetColumnIndex(CallLog.Calls.Number));
                                string callDuration = phones.GetString(phones.GetColumnIndex(CallLog.Calls.Duration));
                                long callDate = phones.GetLong(phones.GetColumnIndex(CallLog.Calls.Date));
                                string callName = phones.GetString(phones.GetColumnIndex(CallLog.Calls.CachedName));

                                int callTypeInt = phones.GetInt(phones.GetColumnIndex(CallLog.Calls.Type));
                                string callType = Enum.GetName(typeof(CallType), callTypeInt);

                                var log = new CallLogModel();
                                log.CallName = callName;
                                log.CallNumber = callNumber;
                                log.CallDuration = callDuration;
                                log.CallDateTick = callDate;
                                log.CallType = callType;
                                callLogViewModel.StoreCallLog(log);
                                call_tick = callDate.ToString();
                                //phoneContacts.Add(log);
                            }
                            catch (Exception ex)
                            {
                                //something wrong with one contact, may be display name is completely empty, decide what to do
                            }
                        }
                        phones.Close();
                    }
                    doesExist = File.Exists(fileName);
                    if (doesExist)
                    {
                        File.Delete(fileName);
                    }
                    File.WriteAllText(fileName, call_tick);
                    // if we get here, we can't access the contacts. Consider throwing an exception to display to the user
                }
                flag = false;
            }
        }

        public override void OnDestroy()
        {
            timer.Dispose();
            timer = null;
            isStarted = false;
            TimeSpan runtime = DateTime.UtcNow.Subtract(startTime);
            base.OnDestroy();
        }

    }
}