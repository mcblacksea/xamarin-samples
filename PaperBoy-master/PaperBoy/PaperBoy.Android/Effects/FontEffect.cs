using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using Android.Graphics;
using System.ComponentModel;
using PaperBoy.Droid.Effects;
using PaperBoy.Effects;

[assembly:ResolutionGroupName("Xamarin")]
[assembly:ExportEffect(typeof(FontEffect), "FontEffect")]
namespace PaperBoy.Droid.Effects
{
    public class FontEffect:PlatformEffect
    {
        private TextView control;
        protected override void OnAttached()
        {
            try
            {
                control=control as TextView;

                string fontPath = "Fonts/" + CustomFontEffect.GetFontFileName(Element) + ".ttf";
                Typeface font=Typeface.CreateFromAsset(Forms.Context.Assets,fontPath);
                control.Typeface = font;
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", e.Message);
            }
        }

        protected override void OnDetached()
        {
            throw new NotImplementedException();
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName==CustomFontEffect.FontFileNameProperty.PropertyName)
            {
                Typeface font=Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets,"Fonts/"+CustomFontEffect.GetFontFileName(Element)+".ttf");
                control.Typeface = font;
            }
        }
    }
}