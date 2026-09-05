using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
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
    public partial class WinReminderadd : MetroWindow
    {
        public WinReminderadd()
        {
            InitializeComponent();

            MybuttonDate.Click += (s, e) =>
            {
                mypopup.IsOpen = !mypopup.IsOpen;
            };

            // Update TextBox when a date is selected
            MyCalender.SelectedDatesChanged += (s, e) =>
            {
                if (MyCalender.SelectedDate.HasValue)
                {
                    MyDatetextbox.Text = MyCalender.SelectedDate.Value.ToString("yyyy-MM-dd");
                    mypopup.IsOpen = false;
                }
            };

        }
        public int con = 0;
        public int idrem = 0;
        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            try {

                if (con == 1) {

                    string mydate = this.MyDatetextbox.Text;
                    string mytime = this.txttime.Text;
                    DateTime mydatandtime = DateTime.Parse($"{mydate} {mytime}");
                    ClassReminder cr = new ClassReminder();
                    string sit = "";
                    if (checkbox1.IsChecked == true) { sit = "True"; }
                    if (checkbox1.IsChecked == false) { sit = "False"; }
                    cr.Insert_Reminder(mydatandtime, this.txtreachdes.Text, sit);
                    this.DialogResult = true;

                }


                if (con == 2)
                {

                    string mydate = this.MyDatetextbox.Text;
                    string mytime = this.txttime.Text;
                    DateTime mydatandtime = DateTime.Parse($"{mydate} {mytime}");
                    ClassReminder cr = new ClassReminder();
                    string sit = "";
                    if (checkbox1.IsChecked == true) { sit = "True"; }
                    if (checkbox1.IsChecked == false) { sit = "False"; }
                    cr.Update_Reminder(idrem, mydatandtime, this.txtreachdes.Text, sit);
                    this.DialogResult = true;

                }

                if (con == 3)
                {

                
                    ClassReminder cr = new ClassReminder();
                    cr.Delete_Reminder(idrem);
                    this.DialogResult = true;

                }


            } catch (Exception ex){ MessageBox.Show(ex.Message); }

        }

        private void btncancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void txttime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
        }

        private void txttime_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txttime.Text.Length == 4 && int.TryParse(txttime.Text, out int val))
            {
                txttime.Text = txttime.Text.Insert(2, ":"); 
            }
        }
    }
}
