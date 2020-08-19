using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace PaperBoy.iOS.Helpers
{
    public static class ColorHelper
    {
        public static UIColor PlatformAccentColor => UIColor.FromRGB(139, 96, 168);

        public static Xamarin.Forms.Color PlatformColor => Xamarin.Forms.Color.FromHex("#8B60A8");
    }
}