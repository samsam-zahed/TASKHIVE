using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using SharpAdbClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;
using TaskHive.Classes;

namespace testapps.Views
{
    /// <summary>
    /// Interaction logic for WindowSetting.xaml
    /// </summary>
    public partial class WinDevice : MetroWindow
    {
        public WinDevice()
        {
            InitializeComponent();

  

        }


        public bool CheckAdbInstalled()
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/c adb devices";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                if (output.Contains("List of devices"))
                {
                    return true; 
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false; 
            }
        }

        public int con = 0;
        public int idrem = 0;
        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            try {


                GlobalApp.connectedDevice = AdbHelper.GetFirstDevice();
                if (GlobalApp.connectedDevice != null)
                {
                    MessageBox.Show(TaskHive.Strings.deviceconneted);
                    GlobalApp.deviceid = GlobalApp.connectedDevice.Serial;
                    GlobalApp.device_server_condition = true;
                    this.DialogResult = true;
                    timertime.Start();
                }
                else
                {
                    GlobalApp.device_server_condition = false;
                    MessageBox.Show(TaskHive.Strings.Nodeviceconnected);
                }

            } catch (Exception ex){ MessageBox.Show(ex.Message); }

        }

        DispatcherTimer timertime = new DispatcherTimer();
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {

            timertime.Interval = TimeSpan.FromSeconds(5);
            timertime.Tick += Timer_Tick_Time;
          
        }

        private  void Timer_Tick_Time(object sender, EventArgs e)
        {

            GlobalApp.connectedDevice = AdbHelper.GetFirstDevice();
            Window1 main = (Window1)System.Windows.Application.Current.MainWindow;
            if (GlobalApp.connectedDevice != null)
            {

            
                main.lab_dev.Foreground = Brushes.LightGreen;

            }
            else { main.lab_dev.Foreground = Brushes.Red; }

        }

        private void btncancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnruncmd_Click(object sender, RoutedEventArgs e)
        {

            try {

                RunSDK();




            } catch (Exception ex) { MessageBox.Show(ex.Message); }


 
           
        }

        private void RunSDK() {

            string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "File");
            string fileName = "device.bat";
            string filePath = System.IO.Path.Combine(folderPath, fileName);

            if (File.Exists(filePath))
            {

                var process = Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true,
                    WorkingDirectory = folderPath,
                });
                process.WaitForExit();

            }

            MessageBox.Show(TaskHive.Strings.DeviceExecuted_successfully);

        }
        private void btnInstall_Click(object sender, RoutedEventArgs e)
        {

      
            bool installedadb = CheckAdbInstalled();

            if (installedadb == true)
            {

                MessageBox.Show(TaskHive.Strings.ADBIsInstalled);
                return;
            
            }
            else {

         
                string url = "https://developer.android.com/tools/releases/platform-tools";

                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });

            }

        

        }


    }
}
