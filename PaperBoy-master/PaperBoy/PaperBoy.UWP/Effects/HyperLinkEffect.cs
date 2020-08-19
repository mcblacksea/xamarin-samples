using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;
using PaperBoy.UWP.Effects;
using PaperBoy.UWP.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ResolutionGroupName("Xamarin")]
[assembly:ExportEffect(typeof(HyperLinkEffect), "HyperlinkEffect")]
namespace PaperBoy.UWP.Effects
{
    public class HyperLinkEffect:PlatformEffect
    {
        protected override void OnAttached()
        {
            var control = Control as TextBlock;

            control.Foreground=new SolidColorBrush(ColorHelper.PlatfromAccentColro);
            control.FontWeight=FontWeights.Bold;

            control.IsTapEnabled = true;
            control.Tapped += (s, e) => { };

            UnderLined(control, control.Text);
        }

        private void UnderLined(TextBlock control, string controlText)
        {

            control.Text = string.Empty;
            Underline underline=new Underline();
            Run run=new Run();
            run.Text = controlText;
            underline.Inlines.Add(run);
            control.Inlines.Add(underline);
        }

        protected override void OnDetached()
        {
            throw new NotImplementedException();
        }
    }
}
