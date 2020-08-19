using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PaperBoy.UWP.Helpers
{
    public static class ColorHelper
    {
        public static Color PlatformColor => Color.FromHex("#8EC63F");
        public static Windows.UI.Color PlatfromAccentColro=>new Windows.UI.Color()
        {
            A = 255,
            R = 1,
            G = 174,
            B=242
        };
    }
}
