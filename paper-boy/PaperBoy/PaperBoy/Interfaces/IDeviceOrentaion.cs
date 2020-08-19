using System;
using System.Collections.Generic;
using System.Text;

namespace PaperBoy.Interfaces
{
    public enum DeviceOrientations
    {
        Undefined,
        Landscape,
        Portrait
    }
    public interface IDeviceOrentaion
    {
        DeviceOrientations GetOrientation();
    }
}
