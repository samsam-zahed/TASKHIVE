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
    public partial class WinSelectNotify : MetroWindow
    {
        public WinSelectNotify()
        {
            InitializeComponent();

            ClassJobs cg = new ClassJobs();
            ObservableCollection<TableJobs> joblist = cg.Select_All_Jobs(TaskHive.Strings.NotStarted, TaskHive.Strings.Active);
            this.ClientsDataGrid.ItemsSource = joblist;

        }

       public TableJobs? Items;
        private void ClientsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Items = this.ClientsDataGrid.SelectedItem as TableJobs;
        }

        private void btncancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnok_Click(object sender, RoutedEventArgs e)
        {
            if (Items != null)
            {

                DialogResult = true;

            }
            else { MessageBox.Show(TaskHive.Strings.selectuser);return; }
        }
    }
}
