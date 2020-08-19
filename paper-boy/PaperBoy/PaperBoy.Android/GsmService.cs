using System;
using System.Collections.Generic;
using System.Text;

using Android.App;
using Android.Content;
using Android.Support.V4.App;
using Gcm.Client;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using PaperBoy.Common;
using PaperBoy.Data;

[assembly: Permission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]
//GET_ACCOUNTS is only needed for android versions 4.0.3 and below
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
namespace PaperBoy.Droid
{
    [BroadcastReceiver(Permission = Gcm.Client.Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_MESSAGE }, Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK }, Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_LIBRARY_RETRY }, Categories = new string[] { "@PACKAGE_NAME@" })]
    public class PushHandlerBroadcastReceiver : GcmBroadcastReceiverBase<GcmService>
    {
        public static string[] SENDER_IDS = new string[] { CoreConstants.GcmRemoteId };
    }

    [Service]
    public class GcmService : GcmServiceBase
    {
        public static string RegistrationID { get; private set; }

        public GcmService()
            : base(PushHandlerBroadcastReceiver.SENDER_IDS) { }

        protected override void OnUnRegistered(Context context, string registrationId)
        {

        }

        protected override void OnError(Context context, string errorId)
        {

        }

        protected override void OnMessage(Context context, Intent intent)
        {
            var msg = new StringBuilder();

            if (intent != null && intent.Extras != null)
            {
                foreach (var key in intent.Extras.KeySet())
                    msg.AppendLine(key + "=" + intent.Extras.Get(key).ToString());
            }

            string title = intent.Extras.Get("message").ToString();

            //Store the message
            var prefs = GetSharedPreferences(context.PackageName, FileCreationMode.Private);
            var edit = prefs.Edit();
            edit.PutString("last_msg", msg.ToString());
            edit.Commit();

            CreateNotification(title,"A new Favorite has been added");

            return;

        }

        async void CreateNotification(string title, string description)
        {
            var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;

            var uiIntent = new Intent(this, typeof(MainActivity));

            NotificationCompat.Builder builder = new NotificationCompat.Builder(this);

            Android.Graphics.BitmapFactory.Options options = new Android.Graphics.BitmapFactory.Options
            {
                InJustDecodeBounds = true
            };

            Android.Graphics.Bitmap largeIcon = await Android.Graphics.BitmapFactory.DecodeResourceAsync(Resources, Resource.Drawable.icon, options);

            Helpers.ToastHelper.ProcessNotification(this, notificationManager, uiIntent, builder, title, description, largeIcon);
        }

        protected override void OnRegistered(Context context, string registrationId)
        {
            RegistrationID = registrationId;

            var push = FavoriteManager.DefaultManager.CurrentClient.GetPush();

            MainActivity.CurrentActivity.RunOnUiThread(() => Register(push, null));
        }

        public async void Register(Push push, IEnumerable<string> tags)
        {
            try
            {
                const string templateBodyGCM = "{\"data\":{\"message\":\"$(messageParam)\"}}";

                JObject templates = new JObject();
                templates["genericMessage"] = new JObject
                {
                    {"body", templateBodyGCM}
                };

                await push.RegisterAsync(RegistrationID, templates);

            }
            catch (Exception ex)
            {

            }
        }

    }

}