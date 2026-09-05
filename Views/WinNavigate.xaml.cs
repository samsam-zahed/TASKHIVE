using DocumentFormat.OpenXml.ExtendedProperties;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
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
    public partial class WinNavigate : MetroWindow
    {
        public WinNavigate()
        {
            InitializeComponent();
        }

        public string address1 = "";
        public string address2 = "";
        int focusaddress = 1;

        public bool b;
        private void btnnavigate_Click(object sender, RoutedEventArgs e)
        {

            if (this.txtaddress1.Text == "") { MessageBox.Show(TaskHive.Strings.NoAddressspecified + "1"); return; }
            if (this.txtaddress2.Text == "") { MessageBox.Show(TaskHive.Strings.NoAddressspecified + "2"); return; }
            address1 = txtaddress1.Text;
            address2 = txtaddress2.Text;

            GlobalApp.Address_1 = address1;
            GlobalApp.Address_2 = address2;

            Window1 main = (Window1)System.Windows.Application.Current.MainWindow;
            main.TabControlLeft.SelectedIndex = 5;

            string ad1 = address1;
            string ad2 = address2;
            string url = $"https://www.google.com/maps/dir/?api=1&origin={ad1}&destination={ad2}&travelmode=driving";
            main.WebvieweGoogleMap.Source = new Uri(url);
            main.Activate();
            this.Close();

        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            if (focusaddress == 1) { txtaddress1.Text = GlobalApp.CompanyAddress; }
            if (focusaddress == 2) { txtaddress2.Text = GlobalApp.CompanyAddress; }
        }

        private void txtaddress1_GotFocus(object sender, RoutedEventArgs e)
        {
            focusaddress = 1;
        }

        private void txtaddress2_GotFocus(object sender, RoutedEventArgs e)
        {
            focusaddress = 2;
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {

            WinSelectClients wsc = new WinSelectClients();
            wsc.Owner = this;
            bool? msg = wsc.ShowDialog();
            if (msg == true)
            {
                if (focusaddress == 1) { txtaddress1.Text = wsc.address; }
                if (focusaddress == 2) { txtaddress2.Text = wsc.address; }
            }
        }

        private void btncancel_Click(object sender, RoutedEventArgs e)
        {
            b = false;
            this.Close();
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            WinSelectCompany wf = new WinSelectCompany();
           wf.Owner = this;
            bool? msg = wf.ShowDialog();

            if (msg == true)
            {
                if (focusaddress == 1) { this.txtaddress2.Text = wf.Items.Address; }
                if (focusaddress == 2) { this.txtaddress2.Text = wf.Items.Address; }
              
            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtaddress1.Text = GlobalApp.Address_1;
            this.txtaddress2.Text = GlobalApp.Address_2;
        }

    
        private void BtnNavigateChange_Click(object sender, RoutedEventArgs e)
        {

                string ad1 = this.txtaddress1.Text;
                string ad2 = this.txtaddress2.Text;
                this.txtaddress1.Text = ad2;
                this.txtaddress2.Text = ad1;

        }
    }
}
