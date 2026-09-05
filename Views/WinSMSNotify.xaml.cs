using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaskHive.Classes;

namespace testapps.Views
{
    /// <summary>
    /// Interaction logic for WindowSetting.xaml
    /// </summary>
    public partial class WinSMSNotify : MetroWindow
    {
        public WinSMSNotify()
        {
            InitializeComponent();

            txt.Text = TaskHive.Strings.NewSMSmessage;

        }

   
        private void btnok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(5000); 
            this.Close();
        }
    }
}
