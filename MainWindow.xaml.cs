using ClosedXML.Excel;
using ControlzEx.Standard;
using ControlzEx.Theming;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml;
using DocumentFormat.OpenXml.Wordprocessing;
using MahApps.Metro.Controls;
using Microsoft.VisualBasic;
using Microsoft.Web.WebView2.WinForms;
using Microsoft.Win32;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SharpAdbClient;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks.Dataflow;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using TaskHive.Classes;
using TaskHive.Reports;
using testapps.Views;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json;






namespace testapps
{
    
    public partial class Window1 : MetroWindow
    {

        bool LoadProgram = true;
        public Window1()
        {
            InitializeComponent();
            LoadProgram = false;

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            Directory.CreateDirectory(System.IO.Path.Combine(exepath, "Data"));

            string filepath = System.IO.Path.Combine(exepath, "Data", "Language.txt");
            if (!File.Exists(filepath))
            {

                File.WriteAllText(filepath, "1");

            }
            else {

                string text = File.ReadAllText(filepath);
                if (text == "1") { ComboboxLanguage.SelectedIndex = 1; } else { ComboboxLanguage.SelectedIndex = 0; }
            
            }
        }

        // Internet Connection

        private void CheckInternetRepeatedly()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (IsInternetAvailable())
            {
                Label_Internet.Content = TaskHive.Strings.Internetconnected;
                Lable_status.Foreground = Brushes.LightGreen;
            }
            else
            {
                Label_Internet.Content = TaskHive.Strings.Internetdisconnected;
                Lable_status.Foreground = Brushes.Red;
            }




        }

        private bool IsInternetAvailable()
        {
            try
            {
                Ping ping = new Ping();
                PingReply reply = ping.Send("8.8.8.8", 1000);
                return reply.Status == IPStatus.Success;
            }
            catch
            {
                return false;
            }
        }


        // End




        private void Refresh_Language_Application()
        {

            this.Email.Header = TaskHive.Strings.Email;
            this.GoogleMap.Header = TaskHive.Strings.GoogleMap;
            this.MyWebsite.Header = TaskHive.Strings.MyWebsite;
            this.PDF.Header = TaskHive.Strings.PDF;
            this.Telegram.Header = TaskHive.Strings.Telegram;
            this.WhatsApp.Header = TaskHive.Strings.WhatsApp;
            this.Label_PNC.Content = TaskHive.Strings.Nodeviceconnected;
            this.Label_Internet.Content = TaskHive.Strings.Internetdisconnected;
            this.btnshownotify.Content = TaskHive.Strings.ViewNotifications;
            this.btnsCustomerEmail.Content = TaskHive.Strings.Email;
            this.btnselectcompany.Content = TaskHive.Strings.CompanySelection;
            this.LableCompanyName.Content = TaskHive.Strings.companyname;
            this.labelcompanyaddress.Content = TaskHive.Strings.CompanyAddress;
            this.txtcompanyaddress.Text = TaskHive.Strings.CompanyAddress;
            this.labelcompanywebsite.Content = TaskHive.Strings.companywebsite;
            this.labelcompanyphone.Content = TaskHive.Strings.companyphone;
            this.Labelcompanyemail.Content = TaskHive.Strings.EmailCompany;
            this.labelServiceStatus.Content = TaskHive.Strings.ServiceStatus;
            this.labeldescription.Content = TaskHive.Strings.description;
            this.labelcompanyservice.Content = TaskHive.Strings.ServiceType;
            this.labelcompanynote.Content = TaskHive.Strings.Notes;
            this.MenuLock.Header = TaskHive.Strings.User_Login_Mode;
            this.Menushare.Header = TaskHive.Strings.File_Transfer;
            this.MenuItem_tasks.Header = TaskHive.Strings.Task_Management;
            this.Menuweather.Header = TaskHive.Strings.WeatherStatus;
            this.laberlstatus1.Content = TaskHive.Strings.Youhave;
            this.laberlstatu2.Content = TaskHive.Strings.Notification;
            this.btnCompleted.Content = TaskHive.Strings.Completed;
            this.btnshowongoing.Content = TaskHive.Strings.Ongoing;
            this.TaskList.Header = TaskHive.Strings.tasklist;
            this.btnCompanyWebsit.Content = TaskHive.Strings.companywebsite;
            this.MenuCalling.Header = TaskHive.Strings.CallMessageForm;
            this.btnemail.Content = TaskHive.Strings.Email;
            this.Menudeviceconnect.Header = TaskHive.Strings.Establishconnectiondevice;
            this.btnsavejob.Content = TaskHive.Strings.Save;
            this.btnCancelJob.Content = TaskHive.Strings.Cancel;
            this.MenuItem_File.Header = TaskHive.Strings.File;
            this.MenuItem_Security.Header = TaskHive.Strings.Security;
            this.MenuItem_Tools.Header = TaskHive.Strings.Tools;
            this.MenuItem_Restore.Header = TaskHive.Strings.Restore;
            this.MenuItem_backup.Header = TaskHive.Strings.Backup;
            this.MenuMU.Header = TaskHive.Strings.ManageUser;
            this.MenuRminder.Header = TaskHive.Strings.Reminder;
            this.MenuAM.Header = TaskHive.Strings.ApplicantManagement;
            this.MenuMC.Header = TaskHive.Strings.ApplicantManagement;
            this.MenuNavigate.Header = TaskHive.Strings.navigate;
            this.MenuItem_Setting.Header = TaskHive.Strings.Settings;
            this.Menuoffice.Header = TaskHive.Strings.BasicInformation;
            this.TabWeather.Header = TaskHive.Strings.Weather_conditions;
            

        }


        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            if (this.LoadProgram)
            {
                return;
            }

            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            string folderPath = System.IO.Path.Combine(exePath, "Data");
            Directory.CreateDirectory(folderPath);
            string filePath = System.IO.Path.Combine(folderPath, "Language.txt");

            if (this.ComboboxLanguage.SelectedIndex == 0)
            {
                string chooselang = "en";
                TaskHive.Classes.GlobalApp.ChangeLanguage_Application(chooselang);
                Refresh_Language_Application();

                File.WriteAllText(filePath, this.ComboboxLanguage.SelectedIndex.ToString());

            }
            else if (this.ComboboxLanguage.SelectedIndex == 1)  
            {
                string chooselang = "fi-FI";
                TaskHive.Classes.GlobalApp.ChangeLanguage_Application(chooselang);
                Refresh_Language_Application();

                File.WriteAllText(filePath, this.ComboboxLanguage.SelectedIndex.ToString());

            }

         

            Load_Notification_messages();
        }

        private void btnsetting_Click(object sender, RoutedEventArgs e)
        {

            WinSetting wins = new WinSetting();
            bool? msg = wins.ShowDialog();
            if (msg == true) {

                ReSetting();

            }

        }

        private void MainForm_Loaded(object sender, RoutedEventArgs e)
        {

            GlobalApp.WidthMainForm = this.ActualWidth;
            GlobalApp.HeightMainForm = this.ActualHeight;

            this.toolbar1.Width = GlobalApp.WidthMainForm;

            DispatcherTimer timertime_2 = new DispatcherTimer();
            timertime_2.Interval = TimeSpan.FromSeconds(5);
            timertime_2.Tick += Timer_Tick_Time_2;
            timertime_2.Start();

            DispatcherTimer timertime = new DispatcherTimer();
            timertime.Interval = TimeSpan.FromSeconds(5);
            timertime.Tick += Timer_Tick_Time;
            timertime.Start();

            string DateToday = DateTime.Now.ToString("yyyy-MM-dd");
            string weekname = DateTime.Now.ToString("dddd");
            string monthname = DateTime.Now.ToString("MMMM");
            this.Label_date.Content = DateToday + " " + weekname + " " + monthname;
            this.Label_Time.Content = DateTime.Now.ToLongTimeString();


            labeltime.Content = "00:00";
            txtserbicetype.Text = "---";
            txtdes.Text = "---";
            txtcustomeremail.Text = "---";
            txtcompanyemail.Text = "---";
            txtparty.Text = "---";
            txtcompanyaddress.Text = "---";
            txtcompanywebsite.Text = "https://";
            comboboxcompanyphone.Text = "";
            txtcompanyservice.Text = "---";
            txtcompanynote.Text = "---";
            txtid.Text = "0";
            txtfullname.Text = "---";
            combophone.Text = "";
        
            txtaddress.Text = "---";

            CheckInternetRepeatedly();
        }

        private async void Timer_Tick_Time(object sender, EventArgs e)
        {

            this.Label_Time.Content = DateTime.Now.ToString("HH:mm");
            ClassJobs cj = new ClassJobs();
            bool hasNewOrder = await cj.GetNewRecordedTaskListAsync();

            if (hasNewOrder)
            {
                ShowOrderNotification();
            }

        }

        private async void Timer_Tick_Time_2(object sender, EventArgs e)
        {
        
           ClassReminder cr = new ClassReminder();
           Reminder? rm = cr.GetReminder();
            if (rm != null && rm.Description != null)
            {
                ShowOReminderNotification(rm.Description, rm.ID);
            }


            if (IsDeviceConnected())
            {

                AdbHelper.DeviceConnected = true;
                Label_PNC.Content = TaskHive.Strings.DeviceConnected;
            }
            else
            {
                AdbHelper.DeviceConnected = false;
                Label_PNC.Content = TaskHive.Strings.Nodeviceconnected;
            }

            //if (!IsPortOpen("127.0.0.1", 5037))
            //{

            //    lab_dev.Foreground = Brushes.Red;
            //    GlobalApp.device_server_condition = false;

            //}
            //else
            //{

            //    lab_dev.Foreground = Brushes.LightGreen;
            //    GlobalApp.device_server_condition = true;

            //}

        }

        private  bool IsDeviceConnected()
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "adb",
                Arguments = "devices",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process =  Process.Start(psi))
            {
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                return output.Contains("\tdevice");
            }
        }
        private bool IsPortOpen(string host, int port)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    var result = client.BeginConnect(host, port, null, null);
                    bool success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(200));
                    if (!success)
                        return false;

                    client.EndConnect(result);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public void ShowOrderNotification()
        {

            var win = new WinNotify(TaskHive.Strings.newmessage);
            win.Show();

            Task.Delay(5000).ContinueWith(t =>
            {
                Dispatcher.Invoke(() => win.Close());
            });

            Load_Notification_messages();

        }

        public void ShowOReminderNotification(string msg,int id)
        {

            var win = new WinNotifyReminder(msg);
            win.id = id;
            win.Show();

        }

        private void Load_Notification_messages() {

            ClassJobs cj = new ClassJobs();
            this.BtnNotification.Content = cj.Get_Cout_Notification_Received();

        }
        private void ReSetting() {


            ClassSetting cs = new ClassSetting();
            cs.Select_Setting_Application();

            string mailaddress = GlobalApp.CompanyEmail;
            string ma = "";

            if (mailaddress.EndsWith("@gmail.com"))
            { ma = "https://mail.google.com/"; }
            else if (mailaddress.EndsWith("@yahoo.com"))
            { ma = "https://mail.yahoo.com/"; }
            else if (mailaddress.EndsWith("@outlook.com") || mailaddress.EndsWith("@hotmail.com"))
            { ma = "https://outlook.live.com/mail/"; }

            this.WebviewEmail.Source = new Uri(ma);

            //string gm = TaskHive.Classes.GlobalApp.CompanyAddress ?? "";
            //string url = "https://www.google.com/maps/place/" + Uri.EscapeDataString(gm);
            //this.WebvieweGoogleMap.Source = new Uri(url);

          
            string urlweb_telegram = $"https://web.telegram.org/a/";
            WebviewTelegram.Source = new Uri(urlweb_telegram);

            string urlweb_whatsapp = $"https://web.whatsapp.com/send?phone={GlobalApp.phone}";
            this.WebviewWhatsApp.Source = new Uri(urlweb_whatsapp);

            this.Websiteoffice.Source = new Uri(GlobalApp.CompanyWebsite);

        }
        private void btnuser_Click(object sender, RoutedEventArgs e)
        {
       
            WinLogin wg = new WinLogin();
            wg.ShowDialog();

    

        }

        private void MainForm_ContentRendered(object sender, EventArgs e)
        {
            WinLogin wg = new WinLogin();
            wg.Owner = this;
            bool? msg = wg.ShowDialog();
            if (msg == true) {

                ReSetting();

              
                this.TextBoxStatusEmail.Text = GlobalApp.email;
                this.TextBoxStatusUsername.Text = GlobalApp.uname;
                this.lab_username.Foreground = Brushes.LightGreen;
                this.lab_Email.Foreground = Brushes.LightGreen;

            } else if (msg == false) { }

            Load_Notification_messages();

        }


        private void BtnOffice_Click(object sender, RoutedEventArgs e)
        {

            Btnoffice.ContextMenu.PlacementTarget = Btnoffice;
            Btnoffice.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            Btnoffice.ContextMenu.IsOpen = true;
        }

        private void MenuItem_Word_Click(object sender, RoutedEventArgs e)
        {
            this.TabControlLeft.SelectedIndex = 6;
            this.WebvieweOffice.Source = new Uri("https://word.office.com/");
        }

        private void MenuItem_Excel_Click(object sender, RoutedEventArgs e)
        {
            this.TabControlLeft.SelectedIndex = 6;
            this.WebvieweOffice.Source = new Uri("https://excel.office.com/");
        }

  
        int onc = 0;


        private void MenuItem_MakeTeplate_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Excel");

                MessageBoxResult msg = MessageBox.Show(TaskHive.Strings.newtemplateoredit,"Template File",MessageBoxButton.YesNo);

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


                } else if (msg == MessageBoxResult.No) 
                
                
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


        private List<Client> clients = new List<Client>();
        private string selectedFile = "";



        private void Delete_All_PDF_Files_NoUse(string folderPath)
        {
           
            if (!Directory.Exists(folderPath))
                return;

          
            string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf");

            foreach (string file in pdfFiles)
            {
                try
                {
                    File.Delete(file); 
                }
                catch (Exception ex)
                {
                
                    MessageBox.Show($"Error deleting {file}: {ex.Message}");
                }
            }
        }

        private void MainForm_Closed(object sender, EventArgs e)
        {
            string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PrePrint");
            Delete_All_PDF_Files_NoUse(folderPath);
        }


  

        private void Menu_CompanyForm_Click(object sender, RoutedEventArgs e)
        {
            WinCompanyShow winCompany = new WinCompanyShow();
            winCompany.Owner = this;
            winCompany.Show();
        }

  

        int idjob = 0;
        private void btnshownotify_Click(object sender, RoutedEventArgs e)
        {
            WinSelectNotify wf = new WinSelectNotify();
        
            bool? msg = wf.ShowDialog();

            if (msg == true) {

          
                ClassClient cc = new ClassClient();
                ObservableCollection<Client> mylist = cc.Select_One_Clients(wf.Items.idcustomer, "True");

               
                this.txtid.Text = mylist[0].id.ToString();
                string selectedPath = mylist[0].image; 

                if (!string.IsNullOrWhiteSpace(selectedPath) && File.Exists(selectedPath))
                {
                  
                    img.Source = new BitmapImage(new Uri(selectedPath, UriKind.Absolute));
                }
                else
                {
                   
                    img.Source = new BitmapImage(
                        new Uri("pack://application:,,,/user.png", UriKind.Absolute));
                }



                idjob = wf.Items.id;
                this.txtfullname.Text = wf.Items.customername;
                this.txtdes.Text = wf.Items.jobcomment;
                this.txtid.Text = wf.Items.idcustomer.ToString();
                this.txtaddress.Text = mylist[0].address;
                this.txtcustomeremail.Text = mylist[0].myemail;
             
                string a = mylist[0].phone;
                string[] numbers = a.Split(',');
                this.combophone.ItemsSource = numbers;
                this.combophone.SelectedIndex = 0;
                this.txtserbicetype.Text = wf.Items.jobcaption;
                this.TabControlLeft.SelectedIndex = 8;
                this.btnselectcompany.IsEnabled = true;
                this.labeltime.Content = wf.Items.jobtime;
                this.btnsCustomerEmail.IsEnabled = true;
             


            }

        }
        int idcompany = 0;
        private void btnselectcompany_Click(object sender, RoutedEventArgs e)
        {

            WinSelectCompany wf = new WinSelectCompany();

            bool? msg = wf.ShowDialog();

            if (msg == true)
            {

                idcompany = wf.Items.Id;
                this.txtparty.Text = wf.Items.CompanyName;
                this.txtcompanyaddress.Text = wf.Items.Address;
                this.txtcompanywebsite.Text = wf.Items.Website;
                string a = wf.Items.Phone;
                string[] numbers = a.Split(',');
                this.comboboxcompanyphone.ItemsSource = numbers ;
                this.txtcompanyservice.Text = wf.Items.ServiceType;
                this.txtcompanynote.Text = wf.Items.Notes;
                this.comboboxcompanyphone.SelectedIndex = 0;
                this.txtcompanyemail.Text = wf.Items.Email;

                this.btnCancelJob.IsEnabled = true;
                this.btnsavejob.IsEnabled = true;
                this.btnshownotify.IsEnabled = false;
                this.btnCompanyWebsit.IsEnabled = true;
            
                this.btnemail.IsEnabled = true;
          

            }


        }

        private void btnCompanyWebsit_Click(object sender, RoutedEventArgs e)
        {


            if (this.txtcompanywebsite.Text == "https://") { return; }

            WinWebsite wb = new WinWebsite();
            wb.Owner = this;
            wb.webbrowser1.Source = new Uri(this.txtcompanywebsite.Text);
            wb.Show();



        }


        private void btnCancelJob_Click(object sender, RoutedEventArgs e)
        {
            this.btnCancelJob.IsEnabled = false;
            this.btnsavejob.IsEnabled = false;
            this.btnshownotify.IsEnabled = true;
            this.btnCompanyWebsit.IsEnabled = false;
        
            this.btnemail.IsEnabled = false;
            this.btnsCustomerEmail.IsEnabled = false;
            this.btnselectcompany.IsEnabled = false;


            labeltime.Content = "00:00";
            txtserbicetype.Text = "---";
            txtdes.Text = "---";
            txtcustomeremail.Text = "---";
            txtcompanyemail.Text = "---";
            txtparty.Text = "---";
            txtcompanyaddress.Text = "---";
            txtcompanywebsite.Text = "https://";
            comboboxcompanyphone.Text = "";
            txtcompanyservice.Text = "---";
            txtcompanynote.Text = "---";
            txtid.Text = "0";
            txtfullname.Text = "---";
            combophone.Text = "";
 
            txtaddress.Text = "---";
            img.Source = new BitmapImage(new Uri("pack://application:,,,/user.png", UriKind.Absolute));

        }

        private void btnemail_Click(object sender, RoutedEventArgs e)
        {
            
            WinEmail we = new WinEmail();
            we.Width = this.Width - 550;
            we.Height = this.Height - 350;
            we.txtTo.Text = this.txtcompanyemail.Text;
            we.txttitle.Text = this.txtparty.Text;
            we.Owner = this;
            we.Topmost = true;
            we.Show();

        }

        private void btnsavejob_Click(object sender, RoutedEventArgs e)
        {
            try {

                string _idjob = idjob.ToString();
                string _idcompany = idcompany.ToString();
                string _id = _idjob + _idcompany;
                int id = Convert.ToInt32(_id);

                int idcustomer = Convert.ToInt32(txtid.Text);

                ClassTableJobsub ctj = new ClassTableJobsub();
                ctj.Insert(id,idcustomer,idjob,idcompany,"---","True");

                ClassJobs cj = new ClassJobs();
                cj.Change_jobsit(idjob, TaskHive.Strings.Ongoing);

                MessageBox.Show(TaskHive.Strings.Savedsuccessfully);
                btnCancelJob_Click(sender,e);
                Load_Notification_messages();


            } catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void btnsCustomerEmail_Click(object sender, RoutedEventArgs e)
        {

            WinEmail we = new WinEmail();
            we.Width = this.Width - 550;
            we.Height = this.Height - 350;
            we.txtTo.Text = this.txtcustomeremail.Text;
            we.txttitle.Text = this.txtserbicetype.Text;
            we.Owner = this;
            we.Topmost = true;
            we.Show();

        }

  

        private void btnshowongoing_Click(object sender, RoutedEventArgs e)
        {

            foreach (Window w in Application.Current.Windows)
            {
                if (w is WinOngoing)
                {
                    w.Activate();
                    return;
                }
            }

            WinOngoing wo = new WinOngoing();
            wo.Show();  

        }

        private void btnCompleted_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window w in Application.Current.Windows)
            {
                if (w is WinCompleted)
                {
                    w.Activate();
                    return;
                }
            }
            WinCompleted wo = new WinCompleted();
            wo.Show();

        }


        private void Manage_Customer_Click(object sender, RoutedEventArgs e)
        {

            foreach (Window w in Application.Current.Windows)
            {
                if (w is WinCliensShow)
                {
                    w.Activate();
                    return;
                }
            }

            WinCliensShow ws = new WinCliensShow();
            ws.Owner = this;
            ws.Show();

        }


        private void Manage_Company_Click(object sender, RoutedEventArgs e)
        {

            foreach (Window w in Application.Current.Windows)
            {
                if (w is WinCompanyShow)
                {
                    w.Activate();
                    return;
                }
            }

            WinCompanyShow ws = new WinCompanyShow();
            ws.Owner = this;
            ws.Show();

        }

        private void Manage_User_Click(object sender, RoutedEventArgs e)
        {

            foreach (Window w in Application.Current.Windows)
            {
                if (w is WinCompanyShow)
                {
                    w.Activate();
                    return;
                }
            }

            WinUserShow ws = new WinUserShow();
            ws.Owner = this;
            ws.Show();

        }

        private void Manage_Reminder_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window w in Application.Current.Windows)
            {
                if (w is WinReminder)
                {
                    w.Activate();
                    return;
                }
            }

            WinReminder ws = new WinReminder();
            ws.Owner = this;
            ws.Show();
        }

        private async void BtnConnectDevice_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                WinDevice ws = new WinDevice();
                ws.Owner = this;
                bool? wsb = ws.ShowDialog();
                if (wsb == true) {

                    lab_dev.Foreground = Brushes.LightGreen;
                    Label_PNC.Content = TaskHive.Strings.DeviceConnected;

                }


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void MenuItem_Office_Click(object sender, RoutedEventArgs e)
        {

            this.TabControlLeft.SelectedIndex = 7;
            this.WebvieweOffice365.Source = new Uri("https://www.office.com/");
          
        }

        private void BtnPDF_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog of = new OpenFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf"
            };

            if (of.ShowDialog() == true && File.Exists(of.FileName))
            {
                TabControlLeft.SelectedIndex = 4;
                WebviewPDF.Source = new Uri(of.FileName);
            }

        }





        private void Btnjob_Click(object sender, RoutedEventArgs e)
        {
            WinJobs wj = new WinJobs();
            wj.Owner = this;
            wj.ShowDialog();
        }

        private void Btnshare_Click(object sender, RoutedEventArgs e)
        {

            WinShareFolder ws = new WinShareFolder();
            ws.Owner = this;
            ws.Show();

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            WinBackUp ws = new WinBackUp();
            ws.Owner = this;
            ws.radiobackup.IsChecked = true;
            ws.radiorestore.IsEnabled = false;
            ws.btnopenfilerestore.IsEnabled = false;
            ws.btnsave.Content = TaskHive.Strings.Backup;
           
            ws.con = 1;
            ws.ShowDialog();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            WinBackUp ws = new WinBackUp();
            ws.Owner = this;
            ws.radiobackup.IsEnabled = false;
            ws.radiorestore.IsChecked = true;
            ws.btnopenfile.IsEnabled = false;
            ws.btnsave.Content = TaskHive.Strings.Restore;
            ws.con = 2;
            ws.ShowDialog();
        }

        private void Btnnavigate_Click(object sender, RoutedEventArgs e)
        {

            foreach (Window w in Application.Current.Windows)
            {
                if (w is WinNavigate)
                {
                    w.Activate();
                    return;
                }
            }
            this.TabControlLeft.SelectedIndex = 5;
            WinNavigate wn = new WinNavigate();
            wn.Owner = this;
            wn.Show();


        }

        private void Manage_Calling_Click(object sender, RoutedEventArgs e)
        {

            if (AdbHelper.DeviceConnected == false) { MessageBox.Show(TaskHive.Strings.Nodeviceconnected); return; }

            foreach (Window w in Application.Current.Windows)
            {
                if (w is WinCalling)
                {
                    w.Activate();
                    return;
                }
            }

            WinCalling ws = new WinCalling();
            ws.Owner = this;
            ws.Show();

        }

        private void Menudeviceconnect_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                WinDevice ws = new WinDevice();
                ws.Owner = this;
                bool? wsb = ws.ShowDialog();
                if (wsb == true)
                {

                    lab_dev.Foreground = Brushes.LightGreen;
                    Label_PNC.Content = TaskHive.Strings.DeviceConnected;

                }


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }


        }

        private void MenuNavigate_Click(object sender, RoutedEventArgs e)
        {

            foreach (Window w in Application.Current.Windows)
            {
                if (w is WinNavigate)
                {
                    w.Activate();
                    return;
                }
            }

            WinNavigate wn = new WinNavigate();
            wn.Owner = this;
            wn.Show();

        }

        private void Menuoffice_Click(object sender, RoutedEventArgs e)
        {

            WinSetting wins = new WinSetting();
            bool? msg = wins.ShowDialog();
            if (msg == true)
            {

                ReSetting();

            }

        }

        private void MenuPDF_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf"
            };

            if (of.ShowDialog() == true && File.Exists(of.FileName))
            {
                TabControlLeft.SelectedIndex = 4;
                WebviewPDF.Source = new Uri(of.FileName);
            }
        }

        private void MenuLock_Click(object sender, RoutedEventArgs e)
        {
            WinLogin wg = new WinLogin();
            wg.ShowDialog();
        }

        private void Menushare_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window w in Application.Current.Windows)
            {
                if (w is WinShareFolder)
                {
                    w.Activate();
                    return;
                }
            }

            WinShareFolder ws = new WinShareFolder();
            ws.Owner = this;
            ws.Show();
        }

        private void MenuItem_tasks_Click(object sender, RoutedEventArgs e)
        {

            WinJobs wj = new WinJobs();
            wj.Owner = this;
            wj.ShowDialog();

        }

        private void BtnWeather_Click(object sender, RoutedEventArgs e)
        {

            this.TabControlLeft.SelectedIndex = 9;

            if (this.WebvieweWeather.Source != null)
            {

                WebvieweWeather.Dispose();              
                WebvieweWeather = new Microsoft.Web.WebView2.Wpf.WebView2();
                TabWeather.Content = WebvieweWeather;
            }
            else
            {
            
                string url = GlobalApp.WeatherAddress;
                this.WebvieweWeather.Source = new Uri(url);
            }

        }
        public class LocationData
        {
            public double lat { get; set; }
            public double lon { get; set; }
        }

        private async void Menuweather_Click(object sender, RoutedEventArgs e)
        {

            foreach (Window w in Application.Current.Windows)
            {
                if (w is WinWeather)
                {
                    w.Activate();
                    return;
                }
            }

            WinWeather ws = new WinWeather();
            ws.Owner = this;
            ws.Show();

        }

        private void GoogleMap_LostFocus(object sender, RoutedEventArgs e)
        {

            WebvieweGoogleMap.Dispose();
            WebvieweGoogleMap = new Microsoft.Web.WebView2.Wpf.WebView2();
            GoogleMap.Content = WebvieweGoogleMap;


        }

        private void TabWeather_LostFocus(object sender, RoutedEventArgs e)
        {
            WebvieweWeather.Dispose();
            WebvieweWeather = new Microsoft.Web.WebView2.Wpf.WebView2();
            TabWeather.Content = WebvieweWeather;
        }

        private void MenuInternetLogin_Click(object sender, RoutedEventArgs e)
        {
            WinInternetUser ws = new WinInternetUser();
            ws.ShowDialog();

        }

        private void Menusendmessage_Click(object sender, RoutedEventArgs e)
        {

            foreach (Window w in Application.Current.Windows)
            {
                if (w is WinInternetMessageShow)
                {
                    w.Activate();
                    return;
                }
            }

            WinInternetMessageShow ws = new WinInternetMessageShow();
            ws.Owner = this;
            ws.Show();

        }
    }
}
