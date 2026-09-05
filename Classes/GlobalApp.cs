using SharpAdbClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TaskHive.Classes
{
   public static class GlobalApp
    {


        public static int iduser { get; set; }
        public static string phone { get; set; } = string.Empty;
        public static string uname { get; set; } = string.Empty;
        public static string email { get; set; } = string.Empty;
        public static string telid { get; set; } = string.Empty;
        public static string deviceid { get; set; } = string.Empty;
        public static string si { get; set; } = string.Empty;
        public static string pas { get; set; } = string.Empty;

        public static bool device_server_condition { get; set; } =false ;



        //------------------------------------------------------------------

        public static string CompanyAddress { get; set; } = string.Empty;
        public static string CompanyName { get; set; } = string.Empty;
        public static string CompanyPhone { get; set; } = string.Empty;
        public static string CompanyPasswordEmailAcount { get; set; } = string.Empty;
        public static string CompanyEmail { get; set; } = "null";
        public static string CompanyWebsite { get; set; } = string.Empty;

        public static string WeatherAddress { get; set; } = string.Empty;

        public static double WidthMainForm;
        public static double HeightMainForm;
        public static void ChangeLanguage_Application(string culturename)
        {

            var culture = new CultureInfo(culturename);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

        }



        public static DeviceData? connectedDevice;

        public static string Address_1 { get; set; } = string.Empty;
        public static string Address_2 { get; set; } = string.Empty;

    }
}
