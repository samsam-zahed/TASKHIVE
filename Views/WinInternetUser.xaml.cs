using ClosedXML.Excel;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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
using TaskHive.Reports;

namespace testapps.Views
{
    /// <summary>
    /// Interaction logic for WindowSetting.xaml
    /// </summary>
    public partial class WinInternetUser : MetroWindow
    {
        public WinInternetUser()
        {
            InitializeComponent();
        }


        private void RefreshList() {


            ClassInternetCreateUser CI = new ClassInternetCreateUser();
            ObservableCollection<InterNetUsers> ls = CI.Select_All_InternetUsers();
            this.ClientsInternetDataGrid.ItemsSource = ls;
            this.TextBlocCounts.Text = this.ClientsInternetDataGrid.Items.Count.ToString();


        }
        private void btnadd_Click(object sender, RoutedEventArgs e)
        {
            WinSelectInternetUser ws = new WinSelectInternetUser();
            bool? msg = ws.ShowDialog();

            if (msg == true) {

                RefreshList();


            }


        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshList();
        }

        private void btnrefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshList();
        }

        private void btndelete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedClient != null) {

                MessageBoxResult msg = MessageBox.Show(TaskHive.Strings.MsgDelete +" "+ SelectedClient.UserName ,"",MessageBoxButton.YesNo);
                if (msg == MessageBoxResult.Yes) {

                    ClassInternetCreateUser CI = new ClassInternetCreateUser();
                    CI.Deletet(SelectedClient.Id);
                    RefreshList();
                }
           

            }
            else { MessageBox.Show(TaskHive.Strings.selectuser); }
          

        }

        InterNetUsers? SelectedClient;
        private void ClientsInternetDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedClient = ClientsInternetDataGrid.SelectedItem as InterNetUsers;
        }
    }
}
