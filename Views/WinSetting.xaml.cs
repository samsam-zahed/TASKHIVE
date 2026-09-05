using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection.PortableExecutable;
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

    public partial class WinSetting : MetroWindow
    {
        public WinSetting()
        {
            InitializeComponent();
          

        }

        private void BtnApply_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {

 
                ClassSetting cs = new ClassSetting();
                cs.updatesetting(1, this.companyName.Text);
                cs.updatesetting(2, this.companyPassword.Password);
                cs.updatesetting(3, this.companyEmail.Text);
                cs.updatesetting(4, this.companyaddress.Text);
                cs.updatesetting(5, this.companyPhone.Text);
                cs.updatesetting(6, this.companywebsite.Text);
                cs.updatesetting(7, this.weatheraddress.Text);
                this.DialogResult = true;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void WInSetting1_Loaded(object sender, RoutedEventArgs e)
        {

            this.companyaddress.Text = GlobalApp.CompanyAddress;
            this.companyEmail.Text = GlobalApp.CompanyEmail;
            this.companyPassword.Password = GlobalApp.CompanyPasswordEmailAcount;
            this.companyName.Text = GlobalApp.CompanyName;
            this.companyPhone.Text = GlobalApp.CompanyPhone;
            this.companywebsite.Text = GlobalApp.CompanyWebsite;
            this.weatheraddress.Text = GlobalApp.WeatherAddress;


        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        public class Device
        {
            public int? id { get; set; }
            public string? PhoneNumber { get; set; }
            public string? DeviceId { get; set; }
            public string? sit { get; set; }
        }
        List<Device> devices = new List<Device>();


  

        private void companyPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
        }

   
    }
}
