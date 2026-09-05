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
    public partial class WinNotify : MetroWindow
    {
        public WinNotify(string message)
        {
            InitializeComponent();

            txt.Text = message;

            Loaded += (s, e) =>
            {
                Left = SystemParameters.WorkArea.Width - Width - 10;
                Top = SystemParameters.WorkArea.Height - Height - 10;
            };

        }


    }
}
