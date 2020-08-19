using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using PaperBoy.Interfaces;
using PaperBoy.iOS.DependencyServices;
using UIKit;
using Xamarin.Forms;

[assembly:Dependency(typeof(PlatformLabel))]
namespace PaperBoy.iOS.DependencyServices
{
    public class PlatformLabel : IPlatformLabel
    {
        public PlatformLabel()
        {

        }
        public string GetLabel()
        {
            return "iOS";
        }

        public string GetLabel(string additionalLabel)
        {
            return $"{additionalLabel} iOS";
        }

        public string GetLabel(string additionalLabel, bool addOsVersion)
        {
            string label = (addOsVersion) ? label = $"{additionalLabel} iOS {GetOsVersion()}" : $"{additionalLabel} iOS";
            return label;
        }
        private string GetOsVersion()
        {
            return UIDevice.CurrentDevice.SystemVersion;
        }
    }
}