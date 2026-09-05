using DocumentFormat.OpenXml.Drawing.Charts;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using SharpAdbClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using TaskHive.Classes;

namespace testapps.Views
{
    public partial class WinCalling : MetroWindow
    {


        public class SMS
        {
            public string Address { get; set; } = string.Empty;
            public string Body { get; set; } = string.Empty;
            public string Date { get; set; } = string.Empty;
        }
        public WinCalling()
        {
            InitializeComponent();
            this.Width = SystemParameters.PrimaryScreenWidth / 2;
            this.Height = SystemParameters.PrimaryScreenHeight / 2;
        }

        
        //Start Device Connection ------------------------------------------------

        int lastSmsCount = 0;
        private async Task LoadSMSAsync()
        {
            if (GlobalApp.connectedDevice == null)
                return;

            var receiver = new ConsoleOutputReceiver();
            var adb = new AdbClient();

            adb.ExecuteRemoteCommand(
                "content query --uri content://sms --projection address,body,date --sort \"date desc\"",
                GlobalApp.connectedDevice,
                receiver);

            string output = receiver.ToString();

            var smsList = ParseSmsOutput(output);

            if (smsList.Count > lastSmsCount && lastSmsCount != 0)
            {
                WinSMSNotify wsms = new WinSMSNotify();
                wsms.Show();
            }

            lastSmsCount = smsList.Count;

            lvSMS.ItemsSource = smsList;
            Lable_msgcount.Content = smsList.Count.ToString();
        }
        private List<SMS> ParseSmsOutput(string adbOutput)
        {
            var list = new List<SMS>();

            var lines = adbOutput.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                try
                {
                    string address = Regex.Match(line, @"address=([^,]+)").Groups[1].Value;
                    string body = Regex.Match(line, @"body=([^,]+)").Groups[1].Value;
                    string dateStr = Regex.Match(line, @"date=(\d+)").Groups[1].Value;

                    string dateFormatted = "";

                    if (long.TryParse(dateStr, out long timestamp))
                    {
                        var dt = DateTimeOffset
                            .FromUnixTimeMilliseconds(timestamp)
                            .LocalDateTime;

                        dateFormatted = dt.ToString("yyyy/MM/dd HH:mm");
                    }

                    list.Add(new SMS
                    {
                        Address = address,
                        Body = body,
                        Date = dateFormatted
                    });
                }
                catch
                {

                }
            }

            return list;
        }



        // End Device Connection -----------------------------------------------------


        private void txtCall_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }
        private async void btnsend_Click(object sender, RoutedEventArgs e)
        {

            string phoneNumber = txtCall.Text;
            string message = txtmessage.Text;

            if (GlobalApp.device_server_condition == true)
            {

                if (phoneNumber == "") { MessageBox.Show(TaskHive.Strings.Nonumberentered); return; }
                if (message == "") { MessageBox.Show(TaskHive.Strings.Entemessagesend); return; }

                var adb = new AdbClient();

                string command = $"service call isms 7 i32 0 s16 \"com.android.mms.service\" s16 \"{phoneNumber}\" s16 \"null\" s16 \"{message}\" s16 \"null\" s16 \"null\"";

                var receiver = new ConsoleOutputReceiver();
                adb.ExecuteRemoteCommand(command, GlobalApp.connectedDevice, receiver);


                if (receiver.ToString().Contains("Result: Parcel"))
                {

                    MessageBox.Show(TaskHive.Strings.Messagesentsuccessfully, "", MessageBoxButton.OK, MessageBoxImage.Information);
                    await LoadSMSAsync();

                }

                else { MessageBox.Show(TaskHive.Strings.Messagesendingfailed, "", MessageBoxButton.OK, MessageBoxImage.Error); }
                   

                txtmessage.Text = "";
            }
            else { 
            
            
             MessageBox.Show(TaskHive.Strings.MessageDevicdconnectedbutNoconnection, "", MessageBoxButton.OK , MessageBoxImage.Information);
 
            }


        }


        private void btncall_Click(object sender, RoutedEventArgs e)
        {

            string phoneNumber = txtCall.Text;

            if (GlobalApp.device_server_condition == true)
            {

                if (txtCall.Text == "") { MessageBox.Show(TaskHive.Strings.Nonumberentered); return; }

                var adb = new AdbClient();

                adb.ExecuteRemoteCommand(
                    $"am start -a android.intent.action.CALL -d tel:{phoneNumber}",
                    GlobalApp.connectedDevice,
                    new ConsoleOutputReceiver());

            }
            else
            {


                MessageBox.Show(TaskHive.Strings.MessageDevicdconnectedbutNoconnection, "", MessageBoxButton.OK, MessageBoxImage.Information);

            }

        }

        private void lvSMS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvSMS.SelectedItem != null)
            {
                var row = lvSMS.SelectedItem;

                var cellValue = lvSMS.SelectedCells[2].Column.GetCellContent(row) as TextBlock;

                if (cellValue != null)
                {
                    string value = cellValue.Text;
                    this.txtmessage.Text = value;
                }
            }
        }

        DispatcherTimer timertime = new DispatcherTimer();
        private async void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtEnterthephonenumberyouwant.Text = TaskHive.Strings.Enterthephonenumberyouwant;
            this.labelphonenumber.Content = TaskHive.Strings.Phonenumber;
            this.labelmessage.Content = TaskHive.Strings.Message;
            this.btnsend.Content = TaskHive.Strings.Sendmessage;

     
           await LoadSMSAsync();

        
            timertime.Interval = TimeSpan.FromSeconds(3);
            timertime.Tick += Timer_Tick_Time;
            timertime.Start();

        }

        private bool messageShown = false;
        private async void Timer_Tick_Time(object sender, EventArgs e)
        {

            if (AdbHelper.DeviceConnected == false &&  !messageShown) {

                messageShown = true;
                timertime.Stop();
                MessageBox.Show(TaskHive.Strings.Nodeviceconnected,"",MessageBoxButton.OK,MessageBoxImage.Information); 
                this.Close(); }

        }

  
    }
}
