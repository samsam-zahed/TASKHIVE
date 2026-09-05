using ClosedXML.Excel;
using ControlzEx.Standard;
using MahApps.Metro.Controls;
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
    public partial class WinReminder : MetroWindow
    {
        public WinReminder()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh_Lits();
        }

        ObservableCollection<Reminder> RM = new ObservableCollection<Reminder>();
        private void Refresh_Lits()
        {

            ClassReminder cr = new ClassReminder();
            RM = cr.Select_All_Reminder();
            this.UsersDataGrid.ItemsSource = RM;
            this.TextBlocCounts.Text = this.UsersDataGrid.Items.Count.ToString();

        }

    

        private void btnadd_Click(object sender, RoutedEventArgs e)
        {
            WinReminderadd wu = new WinReminderadd();
            wu.txttime.Text = DateTime.Now.ToString("HH:mm");
            wu.MyDatetextbox.Text = DateTime.Now.ToString("yyyy-MM-dd");
            wu.con = 1;
            bool? msg = wu.ShowDialog();

            if (msg == true)
            {


                Refresh_Lits();

            }

        }


        private void btnedit_Click(object sender, RoutedEventArgs e)
        {

        

            if (selectedReminder == null)
            {

                MessageBox.Show(TaskHive.Strings.selectuser);
                return;

            }

            WinReminderadd wu = new WinReminderadd();
            wu.con = 2;
            wu.idrem = selectedReminder.ID;
            DateTime dt;
            if (selectedReminder.ReminderDateTime.HasValue)
            {
               dt = selectedReminder.ReminderDateTime.Value;
               wu.MyDatetextbox.Text = dt.ToString("yyyy-MM-dd");
               wu.txttime.Text = dt.ToString("HH:mm");
                wu.txtreachdes.Text = selectedReminder.Description;
                string? sit = selectedReminder.IsActive;
                if (sit == "True") { wu.checkbox1.IsChecked = true; }
                if (sit == "False") { wu.checkbox1.IsChecked = false; }

            }



            bool? msg = wu.ShowDialog();

            if (msg == true)
            {


                Refresh_Lits();

            }


        }

        private void btndelete_Click(object sender, RoutedEventArgs e)
        {


            if (selectedReminder == null)
            {

                MessageBox.Show(TaskHive.Strings.selectuser);
                return;

            }

            WinReminderadd wu = new WinReminderadd();
            wu.con = 3;
            wu.idrem = selectedReminder.ID;
            wu.MybuttonDate.IsEnabled = false;
            wu.txttime.IsEnabled = false;
            wu.txtreachdes.IsEnabled = false;
            wu.checkbox1.IsEnabled = false;

            DateTime dt;
            if (selectedReminder.ReminderDateTime.HasValue)
            {
                dt = selectedReminder.ReminderDateTime.Value;
                wu.MyDatetextbox.Text = dt.ToString("yyyy-MM-dd");
                wu.txttime.Text = dt.ToString("hh:mm");
                wu.txtreachdes.Text = selectedReminder.Description;
                string? sit = selectedReminder.IsActive;
                if (sit == "True") { wu.checkbox1.IsChecked = true; }
                if (sit == "False") { wu.checkbox1.IsChecked = false; }

            }



            bool? msg = wu.ShowDialog();

            if (msg == true)
            {


                Refresh_Lits();

            }



        }
     

        private void btnrefresh_Click(object sender, RoutedEventArgs e)
        {

            if (selectedReminder == null)
            {
                MessageBox.Show(TaskHive.Strings.selectuser);
                return;
            }

     
            ClassReminder cr = new ClassReminder();
            string? sit = selectedReminder.IsActive;
            if (sit == "True") { cr.AvtiveAndInactive_Reminder(selectedReminder.ID,"False"); }
            if (sit == "False") { cr.AvtiveAndInactive_Reminder(selectedReminder.ID, "True"); }
            Refresh_Lits();


        }

        private void btninactivecustomers_Click(object sender, RoutedEventArgs e)
        {
         
        }
        Reminder? selectedReminder;
        private void UsersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedReminder = UsersDataGrid.SelectedItem as Reminder;
        }
    }
}
