using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace MyAlarm.Droid.Services
{
    [BroadcastReceiver]
    public class MyReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Toast.MakeText(context, "Alarm Ringing!", ToastLength.Short).Show();

            // Instantiate the notification builder and enable sound:
            NotificationCompat.Builder builder = new NotificationCompat.Builder(context, "MyAlarmChannel")
                .SetContentTitle("Sample Notification")
                .SetContentText("Hello World! This is my first notification!")
                .SetSmallIcon(Resource.Mipmap.ic_launcher)
                .SetDefaults((int)(NotificationDefaults.Sound))
                .SetPriority((int)NotificationPriority.High)
                .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Alarm))
                ;

            // Build the notification:
            Notification notification = builder.Build();

            // Get the notification manager:
            NotificationManager notificationManager =
                context.GetSystemService(Context.NotificationService) as NotificationManager;

            // Publish the notification:
            notificationManager.Notify(1000, notification);
        }

    }
}