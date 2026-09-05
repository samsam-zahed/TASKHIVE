using SharpAdbClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskHive.Classes
{
    public class AdbHelper
    {

        public static DeviceData GetFirstDevice()
        {
            var adb = new AdbClient();
            var device = adb.GetDevices().FirstOrDefault();

            if (device != null)
            {

                return device;
            }
            else
            {

                return null;
            }
        }

        public static bool DeviceConnected { get; set; } = false;
    }
}
