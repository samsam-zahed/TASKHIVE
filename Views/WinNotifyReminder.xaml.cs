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
    public partial class WinNotifyReminder : MetroWindow
    {
        public WinNotifyReminder(string message)
        {
            InitializeComponent();

            txt.Text = message;

            Loaded += (s, e) =>
            {
                Left = SystemParameters.WorkArea.Width - Width - 10;
                Top = 10;
            };

        }

        public int id = 0;
        private void btnok_Click(object sender, RoutedEventArgs e)
        {

    
            this.Close();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ClassReminder cr = new ClassReminder();
            cr.AvtiveAndInactive_Reminder(id, "False");
        }
    }
}
