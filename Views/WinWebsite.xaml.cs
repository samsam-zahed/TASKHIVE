using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
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

namespace testapps.Views
{
    /// <summary>
    /// Interaction logic for WindowSetting.xaml
    /// </summary>
    public partial class WinWebsite : MetroWindow
    {
        public WinWebsite()
        {
            InitializeComponent();

            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            this.WindowStartupLocation = WindowStartupLocation.Manual;

          
            this.Width = screenWidth;
            this.Height = screenHeight;

          
            double finalLeft = screenWidth - this.Width;

           
            this.Left = screenWidth;
            this.Top = 0;

           
            DoubleAnimation slideAnimation = new DoubleAnimation
            {
                From = screenWidth,
                To = finalLeft,
                Duration = TimeSpan.FromMilliseconds(400),
                EasingFunction = new QuadraticEase()  
            };

            this.BeginAnimation(Window.LeftProperty, slideAnimation);

        }

        private void btnclose_Click(object sender, RoutedEventArgs e)
        {
            this.webbrowser1.Visibility = Visibility.Hidden;
            DoubleAnimation fadeOut = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(400)
            };

            fadeOut.Completed += (s, _) =>
            {
                this.Close();  
            };

            this.BeginAnimation(Window.OpacityProperty, fadeOut);
        }
    }
}
