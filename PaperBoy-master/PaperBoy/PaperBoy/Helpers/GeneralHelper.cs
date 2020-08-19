using PaperBoy.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PaperBoy.Helpers
{
    public class GeneralHelper
    {
        public static void Speak(string text)
        {
            DependencyService.Get<ITextSpeecher>().Speak(text);
        }
        public static DeviceOrientations GetOrientation()
        {
            var orientation = DependencyService.Get<IDeviceOrentaion>().GetOrientation();
            return orientation;
        }
        public static string GetLabel()
        {
            var label = DependencyService.Get<IPlatformLabel>().GetLabel();
            return label;
        }
        public static string GetLabel(string additionalLabel)
        {
            var label = DependencyService.Get<IPlatformLabel>().GetLabel(additionalLabel);
            return label;
        }
        public static string GetLabel(string additionalLabel,bool addOsVersion)
        {
            var label = DependencyService.Get<IPlatformLabel>().GetLabel(additionalLabel, addOsVersion);
            return label;
        }
    }
}
