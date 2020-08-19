using PaperBoy.Interfaces;
using PaperBoy.UWP.DependencyServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Xamarin.Forms;

[assembly:Dependency(typeof(DeviceOrientaion))]
namespace PaperBoy.UWP.DependencyServices
{
    public class DeviceOrientaion : IDeviceOrentaion
    {
        public DeviceOrientaion()
        {

        }
        public DeviceOrientations GetOrientation()
        {
            var orientation = ApplicationView.GetForCurrentView().Orientation;
            if (orientation == ApplicationViewOrientation.Landscape)
                return DeviceOrientations.Landscape;
            else
                return DeviceOrientations.Portrait;
        }
    }
}
