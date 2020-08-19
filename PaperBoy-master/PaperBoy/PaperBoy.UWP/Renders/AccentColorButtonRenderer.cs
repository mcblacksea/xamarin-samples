using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaperBoy.UWP.Helpers;
using PaperBoy.UWP.Renders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
[assembly:ExportRenderer(typeof(Xamarin.Forms.Button),typeof(AccentColorButtonRenderer))]
namespace PaperBoy.UWP.Renders
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
