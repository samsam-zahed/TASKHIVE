using ClosedXML.Excel;
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
    public partial class WinCliensShow : MetroWindow
    {
        public WinCliensShow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ClassClient cc = new ClassClient();
            ObservableCollection<Client> ls = cc.Select_All_Clients("True");
            this.ClientsDataGrid.ItemsSource = ls;
            this.TextBlocCounts.Text = this.ClientsDataGrid.Items.Count.ToString();
        }

        private void btnadd_Click(object sender, RoutedEventArgs e)
        {
            WinClients wc = new WinClients();
            wc.Owner = this;
            wc.con = 1;
            bool? msg = wc.ShowDialog();
            if (msg == true)
            {

                ClassClient cc = new ClassClient();
                ObservableCollection<Client> ls = cc.Select_All_Clients("True");
                this.ClientsDataGrid.ItemsSource = ls;
                this.TextBlocCounts.Text = this.ClientsDataGrid.Items.Count.ToString();
            }
        }
        Client? SelectedClient;
        private void ClientsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedClient = ClientsDataGrid.SelectedItem as Client;
        }

        private void btnedit_Click(object sender, RoutedEventArgs e)
        {

            WinClients wc = new WinClients();

            wc.TextboxID.IsEnabled = false;
            wc.TextboxID.Text = SelectedClient.id.ToString();
            wc.Textboxfname.Text = SelectedClient.fname;
            wc.Textboxlname.Text = SelectedClient.lname;
            wc.Textboxphone.Text = SelectedClient.phone;
            wc.Textboxaddress.Text = SelectedClient.address;
            wc.Textboxemail.Text = SelectedClient.myemail;
            wc.Textboxeage.Text = SelectedClient.age.ToString();
            wc.Textboxetelegram.Text = SelectedClient.idtelegram;
            string? sex = SelectedClient.personeltype?.Trim();
            if (sex == "Male" || sex == "Mies") { wc.RadioMale.IsChecked = true; }
            if (sex == "Female" || sex == "Nainen") { wc.RadioFemale.IsChecked = true; }
            if (sex == "Other" || sex == "Muu") { wc.RadioOther.IsChecked = true; }
            if (sex == "Prefer not to say" || sex == "En halua sanoa") { wc.Radiop.IsChecked = true; }
            wc.Textboxenationality.Text = SelectedClient.nationality;
            wc.Textboxeimage.Text = SelectedClient.image;
            string sit = SelectedClient.sit;
            if (sit == "True" || sit == "Tosi") { wc.RadioTrue.IsChecked = true; }
            if (sit == "False" || sit == "Epätosi") { wc.RadioFalse.IsChecked = true; }
            wc.TextboxeDes.Text = SelectedClient.des;

            wc.checkbox1.IsEnabled = false;

            wc.Owner = this;
            wc.con = 2;
            bool? msg = wc.ShowDialog();
            if (msg == true)
            {

                ClassClient cc = new ClassClient();
                ObservableCollection<Client> ls = cc.Select_All_Clients("True");
                this.ClientsDataGrid.ItemsSource = ls;
                this.TextBlocCounts.Text = this.ClientsDataGrid.Items.Count.ToString();
            }

        }

        private void btndelete_Click(object sender, RoutedEventArgs e)
        {

            WinClients wc = new WinClients();


            foreach (UIElement element in wc.grid.Children)
            {

                if (!(element is StackPanel sc) || sc.Name != "sc")
                {
                    element.IsEnabled = false;
                }
            }

            if (SelectedClient != null) {

                wc.TextboxID.Text = SelectedClient.id.ToString();

            }
           

            wc.Owner = this;
            wc.con = 3;
            bool? msg = wc.ShowDialog();
            if (msg == true)
            {

                ClassClient cc = new ClassClient();
                ObservableCollection<Client> ls = cc.Select_All_Clients("True");
                this.ClientsDataGrid.ItemsSource = ls;
                this.TextBlocCounts.Text = this.ClientsDataGrid.Items.Count.ToString();
            }

        }
        
        private void btnprint_Click(object sender, RoutedEventArgs e)
        {

            List<Client> SelectedClients = new List<Client>();

            if (ClientsDataGrid.SelectedItems.Count > 0)
            {
                foreach (Client job in ClientsDataGrid.SelectedItems)
                {
                    SelectedClients.Add(new Client
                    {
                        id = job.id,
                        fname = job.fname,
                        lname = job.lname,
                        phone = job.phone,
                        age = job.age,
                        nationality = job.nationality,
                        idtelegram = job.idtelegram,
                        personeltype = job.personeltype,
                        myemail = job.myemail,
                        address = job.address,
                        image = job.image,
                        sit = job.sit,
                        des = job.des,
                        fullname = job.fullname
                    });
                }
            }
            else
            {
                foreach (Client job in ClientsDataGrid.Items)
                {
                    SelectedClients.Add(new Client
                    {
                        id = job.id,
                        fname = job.fname,
                        lname = job.lname,
                        phone = job.phone,
                        age = job.age,
                        nationality = job.nationality,
                        idtelegram = job.idtelegram,
                        personeltype = job.personeltype,
                        myemail = job.myemail,
                        address = job.address,
                        image = job.image,
                        sit = job.sit,
                        des = job.des,
                        fullname = job.fullname
                    });
                }
            }

            var report = new ReportClients(SelectedClients);

            string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PrePrint");
            string fileName = "clients.pdf";
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
            ClassClient cc = new ClassClient();
            ObservableCollection<Client> ls = cc.Select_All_Clients("True");
            this.ClientsDataGrid.ItemsSource = ls;
            this.TextBlocCounts.Text = this.ClientsDataGrid.Items.Count.ToString();
        }

        private void btninactivecustomers_Click(object sender, RoutedEventArgs e)
        {
            ClassClient cc = new ClassClient();
            ObservableCollection<Client> ls = cc.Select_All_Clients("False");
            this.ClientsDataGrid.ItemsSource = ls;
            this.TextBlocCounts.Text = this.ClientsDataGrid.Items.Count.ToString();
        }

        private void btnExcelAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Excel");

                MessageBoxResult msg = MessageBox.Show(TaskHive.Strings.newtemplateoredit, "Template File", MessageBoxButton.YesNo);

                if (msg == MessageBoxResult.Yes)

                {
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    string fileName = "ClientTemplate.xlsx";
                    string filePath = System.IO.Path.Combine(folderPath, fileName);

                    Process.Start(new ProcessStartInfo
                    {
                        FileName = filePath,
                        UseShellExecute = true
                    });


                }
                else if (msg == MessageBoxResult.No)


                {

                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    string fileName = "ClientTemplate.xlsx";
                    string filePath = System.IO.Path.Combine(folderPath, fileName);

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Clients");


                        worksheet.Cell(1, 1).Value = "ID";
                        worksheet.Cell(1, 2).Value = "First Name";
                        worksheet.Cell(1, 3).Value = "Last Name";
                        worksheet.Cell(1, 4).Value = "Phone";
                        worksheet.Cell(1, 5).Value = "Address";
                        worksheet.Cell(1, 6).Value = "Email";
                        worksheet.Cell(1, 7).Value = "Age";
                        worksheet.Cell(1, 8).Value = "Telegram ID";
                        worksheet.Cell(1, 9).Value = "Personnel Type";
                        worksheet.Cell(1, 10).Value = "Nationality";
                        worksheet.Cell(1, 11).Value = "Description";



                        worksheet.Range("I2:I100").Value = "Male";
                        worksheet.Range("I2:I100").Clear();

                        worksheet.Columns().AdjustToContents();

                        workbook.SaveAs(filePath);

                        Process.Start(new ProcessStartInfo { FileName = System.IO.Path.GetFullPath(filePath), UseShellExecute = true });
                    }

                }







            }
            catch (System.ComponentModel.Win32Exception ex)
            {

                MessageBox.Show(TaskHive.Strings.Noexcelinstall);

            }

        }

        private void btnExcelTemplate_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Excel");
                string filePath = System.IO.Path.Combine(folderPath, "ClientTemplate.xlsx");

                if (!Directory.Exists(folderPath))
                {
                    MessageBox.Show(TaskHive.Strings.Excelfoldernotfound);
                    return;
                }

                if (!File.Exists(filePath))
                {
                    MessageBox.Show(TaskHive.Strings.excelfileempty);
                    return;
                }

                var clients = ReadExcel(filePath);

                if (clients.Count == 0)
                {
                    MessageBox.Show(TaskHive.Strings.excelfileempty);
                    return;
                }

                InsertClientsToDb(clients);
                MessageBox.Show($" ({clients.Count}) {TaskHive.Strings.imprtsuccess}");
                btnrefresh_Click(sender,e);


            }
            catch (Exception ex) { MessageBox.Show("Error reading Excel file:\n" + ex.Message); }

        }

   

        private void InsertClientsToDb(List<Client> clients)
        {

            foreach (var c in clients)
            {
                ClassClient cl = new ClassClient();
                bool bi = cl.Check_Client_Exsist(c.id);

                if (bi == true)
                {

                    cl.Update_Client(Convert.ToInt32(c.id), c.fname, c.lname, c.phone, c.address, c.myemail, c.age.ToString(),
                 c.idtelegram, c.personeltype, c.nationality, "/user.png", c.sit, c.des);

                }
                else if (bi == false)
                {

                    cl.Insert_Client(Convert.ToInt32(c.id), c.fname, c.lname, c.phone, c.address, c.myemail, c.age.ToString(),
                  c.idtelegram, c.personeltype, c.nationality, "/user.png", c.sit, c.des);

                }


            }
        }

        private List<Client> ReadExcel(string path)
        {
            var list = new List<Client>();
            using var workbook = new XLWorkbook(path);
            var worksheet = workbook.Worksheets.First();
            var rows = worksheet.RowsUsed().Skip(1);

            foreach (var row in rows)
            {
                list.Add(new Client
                {
                    id = row.Cell(1).GetValue<int>(),
                    fname = row.Cell(2).GetValue<string>(),
                    lname = row.Cell(3).GetValue<string>(),
                    phone = row.Cell(4).GetValue<string>(),
                    address = row.Cell(5).GetValue<string>(),
                    myemail = row.Cell(6).GetValue<string>(),
                    age = row.Cell(7).GetValue<int>(),
                    idtelegram = row.Cell(8).GetValue<string>(),
                    personeltype = row.Cell(9).GetValue<string>(),
                    nationality = row.Cell(10).GetValue<string>(),
                    sit = "True",
                    des = row.Cell(11).GetValue<string>()
                });
            }
            return list;
        }

        private void textboxsearch_TextChanged(object sender, TextChangedEventArgs e)
        {

            try
            {

                if (this.textboxsearch.Text == "Type your value") { return; }

                int index = this.combo1.SelectedIndex;
                string col = "";
                if (index == 0) { col = "id"; }
                if (index == 1) { col = "fname"; }
                if (index == 2) { col = "lname"; }
                if (index == 3) { col = "phone"; }
                if (index == 4) { col = "age"; }
                if (index == 5) { col = "personeltype"; }
                if (index == 6) { col = "nationality"; }

                ClassClient cc = new ClassClient();
                ObservableCollection<Client> ls = cc.Search_Clients_By(col, this.textboxsearch.Text);
                this.ClientsDataGrid.ItemsSource = ls;
                this.TextBlocCounts.Text = this.ClientsDataGrid.Items.Count.ToString();

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

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
    }
}
