using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using PaperBoy.iOS.Helpers;
using PaperBoy.iOS.Renders;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly:ExportRenderer(typeof(Xamarin.Forms.Button),typeof(AccentColorButtonRenderer))]
namespace PaperBoy.iOS.Renders
{
    public class AccentColorButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
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