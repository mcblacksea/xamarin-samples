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
using PaperBoy.Droid.Helpers;
using PaperBoy.Droid.Renders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Button = Xamarin.Forms.Button;
using Xamarin.Forms;

[assembly:ExportRenderer(typeof(Xamarin.Forms.Button),typeof(AccentColorButtonRenderer))]
namespace PaperBoy.Droid.Renders
{
    public class AccentColorButtonRenderer:ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control !=null)
            {
                var button = e.NewElement;
                button.BackgroundColor = AccentColor;
                button.TextColor = Color.White;
                button.FontAttributes = FontAttributes.Bold;
                button.CornerRadius = 22;
            }
        }

        private Color AccentColor => ColorHelper.PlatformColor;
    }
}