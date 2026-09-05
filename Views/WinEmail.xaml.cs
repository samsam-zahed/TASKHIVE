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
    public partial class WinEmail : MetroWindow
    {
        public WinEmail()
        {
            InitializeComponent();
            this.txtFrom.Text = GlobalApp.CompanyEmail;
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string path = System.IO.Path.Combine(
    AppDomain.CurrentDomain.BaseDirectory,
    "HtmlEditor",
    "editor.html");

            browser.Source = new Uri(path);
        }

        private async void btnsend_Click(object sender, RoutedEventArgs e)
        {

            try {

                string result = await browser.ExecuteScriptAsync("getContent();");
                string? html = System.Text.Json.JsonSerializer.Deserialize<string>(result);
          

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(GlobalApp.CompanyEmail);
                mail.To.Add(this.txtTo.Text);
                mail.Subject = this.txttitle.Text;
                mail.Body = html;
                mail.IsBodyHtml = true;

                Attachment attachment = new Attachment(this.txtfile.Text);
                mail.Attachments.Add(attachment);

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential(GlobalApp.CompanyEmail, GlobalApp.CompanyPasswordEmailAcount);
                smtpClient.EnableSsl = true;
                smtpClient.Send(mail);
                MessageBox.Show(TaskHive.Strings.msgsentemail);
                this.Close();

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }


        }

        private void btnopenfile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == true)
            {

                FileInfo fi = new FileInfo(dlg.FileName);

                if (fi.Length > 20 * 1024 * 1024)
                {
                    MessageBox.Show(TaskHive.Strings.Filelarge);
                    return;
                }

                this.txtfile.Text = dlg.FileName;
            }
        }
    }
}
