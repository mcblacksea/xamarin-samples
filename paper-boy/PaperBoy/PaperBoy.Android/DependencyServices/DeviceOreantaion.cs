using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PaperBoy.Droid.DependencyServices;
using PaperBoy.Interfaces;
using Xamarin.Forms;

[assembly:Dependency(typeof(DeviceOreantaion))]
namespace PaperBoy.Droid.DependencyServices
{
    public class DeviceOreantaion : IDeviceOrentaion
    {
        public DeviceOreantaion()
        {

        }
        public DeviceOrientations GetOrientation()
        {
            IWindowManager windowManager = Android.App.Application.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

            var rotation = windowManager.DefaultDisplay.Rotation;
            bool isLandScape = rotation == SurfaceOrientation.Rotation90 || rotation == SurfaceOrientation.Rotation270;
            return isLandScape ? DeviceOrientations.Landscape : DeviceOrientations.Portrait;
        }
    }
}