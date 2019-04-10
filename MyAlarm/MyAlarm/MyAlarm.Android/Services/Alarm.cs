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
using Java.Util;
using MyAlarm.Droid.Services;
using MyAlarm.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(Alarm))]

namespace MyAlarm.Droid.Services
{
    public class Alarm : IAlarm
    {
        public void SetAlarm(DateTime time)
        {

            Intent alarmIntent = new Intent(Android.App.Application.Context, typeof(MyReceiver));
            //alarmIntent.PutExtra("message", message);
            //alarmIntent.PutExtra("title", title);

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);
            AlarmManager alarmManager = (AlarmManager)Android.App.Application.Context.GetSystemService(Context.AlarmService);



            // alarm after 5 second from now
            //var second = 5;
            //alarmManager.SetExact(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + second * 1000, pendingIntent);

            var dateTime = DateTime.Now;
            dateTime = dateTime.AddSeconds(10);
            //Calendar dayCalendar = Calendar.GetInstance(Java.Util.TimeZone.Default);
            Calendar dayCalendar = Calendar.Instance;
            dayCalendar.Set(CalendarField.Year, dateTime.Year);
            // Thang 1 = 0
            dayCalendar.Set(CalendarField.Month, dateTime.Month - 1);
            dayCalendar.Set(CalendarField.DayOfMonth, dateTime.Day);
            dayCalendar.Set(CalendarField.HourOfDay, dateTime.Hour);
            dayCalendar.Set(CalendarField.Minute, dateTime.Minute);
            dayCalendar.Set(CalendarField.Second, dateTime.Second);
            alarmManager.SetExact(AlarmType.RtcWakeup, dayCalendar.TimeInMillis, pendingIntent);


        }
    }
}