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
using System.Net.Mail;
using System.Net;

namespace testapps.Views
{
    /// <summary>
    /// Interaction logic for WindowSetting.xaml
    /// </summary>
    public partial class WinLogin : MetroWindow
    {

  
        public WinLogin()
        {
            InitializeComponent();
        }

        int val = 0;   
        private void checkbox1_Checked(object sender, RoutedEventArgs e)
        {

            this.EmailTextBox.IsEnabled = true;
            this.PasswordBox.IsEnabled = false;
            this.btnlogin.Content = TaskHive.Strings.sendemail;
            val = 1;
        }

        private void checkbox1_Unchecked(object sender, RoutedEventArgs e)
        {
            this.EmailTextBox.IsEnabled = false;
            this.PasswordBox.IsEnabled = true;
            this.btnlogin.Content = TaskHive.Strings.login;
            val = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.btnlogin.IsEnabled = false;
            Application.Current.Shutdown();
        }
        int wrongPasswordCount = 0;
        private void btnlogin_Click(object sender, RoutedEventArgs e)
        {

            try {

                if (val==0) 
                {

                    

                    ClassUsers clu = new ClassUsers();
                    var log = clu.Login_To_Application(this.UserNameTextBox.Text, this.PasswordBox.Password);

                    switch (log) {

                        case ClassUsers.LoginResult.UserNotFound:

                            MessageBox.Show(TaskHive.Strings.userloginnotfound);
                            break;

                            case ClassUsers.LoginResult.UserInactive:

                            MessageBox.Show(TaskHive.Strings.userlogininactive);
                            break;

                            case ClassUsers.LoginResult.WrongPassword:

                            MessageBox.Show(TaskHive.Strings.userwrong);
                            break;

                        case ClassUsers.LoginResult.Success:

                        clu.Select_User_byID_or_Name(1, this.UserNameTextBox.Text);

                           bool IsExistDate = ClassYear.Check_Date_Registered(DateTime.Now.ToString("yyyy-MM-dd"));
                            if (IsExistDate == false)
                            {
                                ClassYear.AddDayToDatabase(DateTime.Now.ToString("yyyy-MM-dd"));
                                ClassYear.Load_Date(DateTime.Now.ToString("yyyy-MM-dd"));
                            }
                            else { ClassYear.Load_Date(DateTime.Now.ToString("yyyy-MM-dd")); }


                                DialogResult = true;

                        return;   
                        
                           
                    }

                    if (wrongPasswordCount > 3) { Application.Current.Shutdown(); }

                    wrongPasswordCount++;


                }


                if (val == 1)
                {

                   ClassUsers clu = new ClassUsers();
                   bool IsMatchUserAndEmail = clu.Check_Email_UserAccount(this.EmailTextBox.Text,this.UserNameTextBox.Text);

                    if (IsMatchUserAndEmail == false)
                    {

                        MessageBox.Show(TaskHive.Strings.userisnotmatchwithemail);
                        return;

                    }
                    else {


                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress(GlobalApp.CompanyEmail);
                        mail.To.Add(this.EmailTextBox.Text);
                        mail.Subject = GlobalApp.CompanyName;
                        mail.Body = $"Hi, TaskHive Applicatoin has sent this Number: {GlobalApp.pas}";

                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                        smtpClient.Port = 587;
                        smtpClient.Credentials = new NetworkCredential(GlobalApp.CompanyEmail, GlobalApp.CompanyPasswordEmailAcount);
                        smtpClient.EnableSsl = true;
                        smtpClient.Send(mail);
                        MessageBox.Show(TaskHive.Strings.msgsentemail);


                    }

                }

            } catch (Exception ex) { MessageBox.Show(ex.Message); }

            // Gmail pass acount -----> mlqs loyd fauc xjnu
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {

                         if (e.Key == Key.Enter) {

                    btnlogin_Click(sender,e);

                }

  
        }
    }
}
