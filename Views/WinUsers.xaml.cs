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
using MahApps.Metro.Controls;
using TaskHive.Classes;
using System.Text.RegularExpressions;

namespace testapps.Views
{
    /// <summary>
    /// Interaction logic for WindowSetting.xaml
    /// </summary>
    public partial class WinUsers : MetroWindow
    {

  
        public WinUsers()
        {
            InitializeComponent();
        }

       
        private void WinUser_Loaded(object sender, RoutedEventArgs e)
        {
            ClassUsers cu = new ClassUsers();
            ObservableCollection<User> users;
            users = cu.Select_All_User();
            //this.UsersListView.ItemsSource = users;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        public int sit = 1;
        public int iduser = 0;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            try {

                if (sit == 1) {

                    ClassUsers us = new ClassUsers();
                    string LastFoureCahr = this.PhoneTextBox.Text;
                    string code = LastFoureCahr.Substring(LastFoureCahr.Length - 4);
                    string devicecode = "Null" + code + 1;
                    us.Insert_User(Convert.ToInt32(code), this.PhoneTextBox.Text, this.UserNameTextBox.Text, this.EmailTextBox.Text, this.TelegramIdTextBox.Text, devicecode, "True", this.PasswordBox.Password);
                    us.EnableSelectedUser(Convert.ToInt32(code));
                    this.DialogResult = true;

                }

                if (sit == 2)
                {

                    ClassUsers us = new ClassUsers();
                    us.Update_User(iduser, this.PhoneTextBox.Text, this.UserNameTextBox.Text, this.EmailTextBox.Text, this.TelegramIdTextBox.Text, this.PasswordBox.Password);
                    this.DialogResult = true;

                }

            }
            catch { MessageBox.Show(TaskHive.Strings.ErrorAddUser);  }
               

    

        }

        private void PhoneTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
        }

        private void TelegramIdTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text=="@")
                e.Handled=true;

        }

        private void TelegramIdTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Back || e.Key == Key.Delete) && TelegramIdTextBox.CaretIndex <= 1)
            {
                e.Handled = true;
            }
        }

        private void TelegramIdTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TelegramIdTextBox.Text == "@")
                { TelegramIdTextBox.CaretIndex = TelegramIdTextBox.Text.Length; }
        }

        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string email = EmailTextBox.Text;

            bool isValid = Regex.IsMatch(
                email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"
            );

            EmailTextBox.BorderBrush = isValid || email == ""
                ? Brushes.Gray
                : Brushes.Red;
        }
    }
}
