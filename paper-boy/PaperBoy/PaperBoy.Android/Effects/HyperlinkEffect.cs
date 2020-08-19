using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PaperBoy.Droid.Effects;
using PaperBoy.Droid.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportEffect(typeof(HyperlinkEffect), "HyperlinkEffect")]
namespace PaperBoy.Droid.Effects
{
    public class HyperlinkEffect:PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var control = (Control as TextView);
                control.SetTextColor(ColorHelper.PlatformAccentColor);
                control.SetTypeface(null,TypefaceStyle.Bold);

                SetUnderLine(true);

                control.Click += (s, e) => { };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            if (args.PropertyName==Label.TextProperty.PropertyName||args.PropertyName==Label.FormattedTextProperty.PropertyName)
            {
                SetUnderLine(true);
            }
        }

        private void SetUnderLine(bool underLined)
        {
            try
            {
                var textView = (TextView) Control;
                if (underLined)
                {
                    textView.PaintFlags |= PaintFlags.UnderlineText;
                }
                else
                {
                    textView.PaintFlags &= ~PaintFlags.UnderlineText;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot underline Label. Error: ", ex.Message);
                throw;
            }
        }

        protected override void OnDetached()
        {
            SetUnderLine(false);
        }
    }
}