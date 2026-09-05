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
    public partial class WinCompanyShow : MetroWindow
    {
        public WinCompanyShow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh_List("Active");
        }

        private void Refresh_List(string status) {

            ClassCompany cc = new ClassCompany();
            ObservableCollection<Companies> ls = cc.Select_All_Companies(status);
            this.ClientsDataGrid.ItemsSource = ls;
            this.TextBlocCounts.Text = this.ClientsDataGrid.Items.Count.ToString();

        }

        private void btnadd_Click(object sender, RoutedEventArgs e)
        {
            Company wc = new Company();
            wc.Owner = this;
            wc.con = 1;
            bool? msg = wc.ShowDialog();
            if (msg == true)
            {

                Refresh_List("Active");
            }
        }
        Companies? SelectedCompany;
        private void ClientsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedCompany = ClientsDataGrid.SelectedItem as Companies;
        }

        private void btnedit_Click(object sender, RoutedEventArgs e)
        {

            if (SelectedCompany != null) {

                Company wc = new Company();

                wc.txtcopanyaddress.Text = SelectedCompany.Address;
                wc.txtcopanyaEmail.Text = SelectedCompany.Email;
                wc.txtcopanyaPhone.Text = SelectedCompany.Phone;
                wc.txtcopanyaWebsite.Text = SelectedCompany.Website;
                wc.txtcopanyname.Text = SelectedCompany.CompanyName;
                wc.txtcopanynotes.Text = SelectedCompany.Notes;
                wc.txtcopanyServiceType.Text = SelectedCompany.ServiceType;
                wc.idcompny = SelectedCompany.Id;
                string sta = SelectedCompany.Status;
                if (sta == "Active") { wc.checkbox1.IsChecked = true; } else { wc.checkbox1.IsChecked = false; }
                wc.con = 2;

                wc.checkbox1.IsEnabled = true;

                wc.Owner = this;
                wc.con = 2;
                bool? msg = wc.ShowDialog();
                if (msg == true)
                {

                    Refresh_List("Active");
                }


            } else { MessageBox.Show(TaskHive.Strings.selectuser); return; }

   

        }

        private void btndelete_Click(object sender, RoutedEventArgs e)
        {

            if (SelectedCompany != null)
            {

                Company wc = new Company();

                wc.txtcopanyaddress.Text = SelectedCompany.Address;
                wc.txtcopanyaddress.IsEnabled = false;
                wc.txtcopanyaEmail.Text = SelectedCompany.Email;
                wc.txtcopanyaEmail.IsEnabled = false;
                wc.txtcopanyaPhone.Text = SelectedCompany.Phone;
                wc.txtcopanyaPhone.IsEnabled = false;
                wc.txtcopanyaWebsite.Text = SelectedCompany.Website;
                wc.txtcopanyaWebsite.IsEnabled = false;
                wc.txtcopanyname.Text = SelectedCompany.CompanyName;
                wc.txtcopanyname.IsEnabled = false;
                wc.txtcopanynotes.Text = SelectedCompany.Notes;
                wc.txtcopanynotes.IsEnabled = false;
                wc.txtcopanyServiceType.Text = SelectedCompany.ServiceType;
                wc.txtcopanyServiceType.IsEnabled = false;
                wc.idcompny = SelectedCompany.Id;
                wc.con = 2;

                wc.checkbox1.IsEnabled = false;

                wc.Owner = this;
                wc.con = 3;
                bool? msg = wc.ShowDialog();
                if (msg == true)
                {

                    Refresh_List("Active");
                }


            }
            else { MessageBox.Show(TaskHive.Strings.selectuser); return; }

        }
        int n = 1;
        private void btnprint_Click(object sender, RoutedEventArgs e)
        {

            if (SelectedCompany == null)
            {
                MessageBox.Show(TaskHive.Strings.selectuser);
                return;
            }

            var report = new CompanyReport(SelectedCompany);



            string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PrePrint");
            string fileName = $"comany.pdf";
            string filePath = System.IO.Path.Combine(folderPath, fileName);
            report.GeneratePdf(filePath);


            OpenFileDialog fil = new OpenFileDialog();
            fil.FileName = filePath;

            string fileselected = fil.FileName;
            WinPrint wp = new WinPrint();
            wp.webprint.Source = new Uri(fileselected);
            wp.webprint.ZoomFactor = 2;
            wp.ShowDialog();

        }

        private void btnrefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh_List("Active");
        }

        private void btninactivecustomers_Click(object sender, RoutedEventArgs e)
        {
            Refresh_List("InActive");
        }



   


  

        private void textboxsearch_GotFocus(object sender, RoutedEventArgs e)
        {
            this.textboxsearch.Text="";
            textboxsearch.Foreground = Brushes.White;
        }

        private void textboxsearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textboxsearch.Text))
            {
                textboxsearch.Text = "Type your value";
                textboxsearch.Foreground = Brushes.Gray;
            }
        }

        private void textboxsearch_TextChanged_1(object sender, TextChangedEventArgs e)
        {

            try
            {

                if (this.textboxsearch.Text == "Type your value") { return; }

               

                int index = this.combo1.SelectedIndex;
                string col = "";
                if (index == 0) { col = "Id"; }
                if (index == 1) { col = "CompanyName"; }
                if (index == 2) { col = "Phone"; }

                ClassCompany cc = new ClassCompany();
                ObservableCollection<Companies> ls = cc.Search_By_Columns(col, this.textboxsearch.Text);
                this.ClientsDataGrid.ItemsSource = ls;
                this.TextBlocCounts.Text = this.ClientsDataGrid.Items.Count.ToString();


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
    }
}
