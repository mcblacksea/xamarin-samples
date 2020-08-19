using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Foundation;
using PaperBoy.iOS.Helpers;
using PaperBoy.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("Xamarin")]
[assembly: ExportEffect(typeof(HyperLinkEffect), "HyperlinkEffect")]
namespace PaperBoy.iOS.Effects
{
    public class HyperLinkEffect:PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var control = Control as UILabel;
                control.TextColor = ColorHelper.PlatformAccentColor;

                UITapGestureRecognizer labelTap=new UITapGestureRecognizer(() =>
                {

                });

                control.UserInteractionEnabled = true;
                control.AddGestureRecognizer(labelTap);

                SetUnderline(true);
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
            if (args.PropertyName == Label.TextProperty.PropertyName || args.PropertyName == Label.FormattedTextProperty.PropertyName)
            {
                SetUnderline(true);
            }
        }

        private void SetUnderline(bool underLined)
        {
            try
            {
                var label = (UILabel) Control;
                var text = (NSMutableAttributedString) label.AttributedText;
                var rang=new NSRange(0,text.Length);

                if (underLined)
                {
                    text.AddAttribute(UIStringAttributeKey.UnderlineStyle,NSNumber.FromInt32((int)NSUnderlineStyle.Single),rang);
                }
                else
                {
                    text.RemoveAttribute(UIStringAttributeKey.UnderlineStyle,rang);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        protected override void OnDetached()
        {
           SetUnderline(false);
        }
    }
}