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
    public partial class WinUserShow : MetroWindow
    {
        public WinUserShow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh_Lits_Users();
        }

        ObservableCollection<User> users = new ObservableCollection<User>();
        private void Refresh_Lits_Users()
        {

            ClassUsers cu = new ClassUsers();
            users = cu.Select_All_User();
            this.UsersDataGrid.ItemsSource = users;
            this.TextBlocCounts.Text = this.UsersDataGrid.Items.Count.ToString();

        }

    

        private void btnadd_Click(object sender, RoutedEventArgs e)
        {
            WinUsers wu = new WinUsers();
            wu.Height = 400;
            wu.Width = 700;
            wu.sit = 1;
            bool? msg = wu.ShowDialog();

            if (msg == true)
            {


                Refresh_Lits_Users();

            }
        }


        private void btnedit_Click(object sender, RoutedEventArgs e)
        {

        

            if (selecteduser == null)
            {

                MessageBox.Show(TaskHive.Strings.selectuser);
                return;

            }

            WinUsers wu = new WinUsers();
            wu.Height = 400;
            wu.Width = 700;
            wu.sit = 2;
            wu.iduser = selecteduser.IdUser;
            wu.TelegramIdTextBox.Text = selecteduser.TelId;
            wu.EmailTextBox.Text = selecteduser.Email;
            wu.PhoneTextBox.Text = selecteduser.Phone;
            wu.UserNameTextBox.Text = selecteduser.UName;
            wu.PasswordBox.Password = selecteduser.Pas;
            wu.btnadd.Content = TaskHive.Strings.Edit;

            bool? msg = wu.ShowDialog();

            if (msg == true)
            {


                Refresh_Lits_Users();

            }


        }

        private void btndelete_Click(object sender, RoutedEventArgs e)
        {

            if (selecteduser == null)
            {

                MessageBox.Show(TaskHive.Strings.selectuser);
                return;

            }
            else if (selecteduser.Si == "True")
            {
                MessageBox.Show(TaskHive.Strings.checkuseractive);
                return;
            }


            if (this.UsersDataGrid.Items.Count < 2)
            {

                MessageBox.Show(TaskHive.Strings.ErroDeleteUser);

            }
            else
            {

                MessageBoxResult msg = MessageBox.Show(TaskHive.Strings.MsgDelUser, "", MessageBoxButton.YesNo);

                if (msg == MessageBoxResult.Yes)
                {

                    ClassUsers us = new ClassUsers();
                    us.Delete_User(selecteduser.IdUser);
                    Refresh_Lits_Users();
                }


                ;
            }



        }
     

        private void btnrefresh_Click(object sender, RoutedEventArgs e)
        {

            if (selecteduser == null)
            {
                MessageBox.Show(TaskHive.Strings.selectuser);
                return;
            }

            ClassUsers us = new ClassUsers();
            us.EnableSelectedUser(selecteduser.IdUser);
            Refresh_Lits_Users();
        }

        private void btninactivecustomers_Click(object sender, RoutedEventArgs e)
        {
         
        }
        User? selecteduser;
        private void UsersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selecteduser = UsersDataGrid.SelectedItem as User;
        }
    }
}
