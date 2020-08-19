using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.Support.V4.App;

namespace PaperBoy.Droid.Helpers
{
    public static class ToastHelper
    {
        public static void ProcessNotification(GcmService service, NotificationManager notificationManager,
            Intent uiIntent, NotificationCompat.Builder builder, string title, string description, Bitmap largIcon)
        {
            var notification = builder.SetContentIntent(PendingIntent.GetActivity(service, 0, uiIntent, 0))
                .SetSmallIcon(Resource.Drawable.icon)
                .SetLargeIcon(largIcon)
                .SetTicker(title)
                .SetContentTitle(title)
                .SetContentText(description)
                .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                .SetAutoCancel(true).Build();

            notificationManager.Notify(1,notification);
        }
    }
}