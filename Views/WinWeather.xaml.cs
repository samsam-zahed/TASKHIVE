using MahApps.Metro.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaskHive.Classes;
using static testapps.Window1;

namespace testapps.Views
{
    /// <summary>
    /// Interaction logic for WindowSetting.xaml
    /// </summary>
    public partial class WinWeather : MetroWindow
    {
        public WinWeather()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {

            string url = GlobalApp.WeatherAddress;
            this.webWeather.Source = new Uri(url);

            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            this.Width = screenWidth /2;
            this.Height = screenHeight /2;

        }

        async Task LoadMap()
        {
            HttpClient client = new HttpClient();
            string json = await client.GetStringAsync(GlobalApp.WeatherAddress);

            var location = JsonConvert.DeserializeObject<LocationData>(json);
            if (location != null)
            {

                string url = GlobalApp.WeatherAddress;
                this.webWeather.Source = new Uri(url);

            }


        }

    }
}
