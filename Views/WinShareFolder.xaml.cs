using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace testapps.Views
{

 

    public class FolderItem
    {
        public string Name { get; set; } = string.Empty;
        public string FullPath { get; set; } = string.Empty ;
    }

    public partial class WinShareFolder : MetroWindow
    {
        public WinShareFolder()
        {
            InitializeComponent();

            this.Width = SystemParameters.PrimaryScreenWidth / 2 ;
            this.Height = SystemParameters.PrimaryScreenHeight / 2 ;

        }

        [DllImport("mpr.dll")]
        private static extern int WNetAddConnection2(ref NETRESOURCE netResource, string password, string username, int flags);

        [DllImport("mpr.dll")]
        private static extern int WNetCancelConnection2(string name, int flags, bool force);

        [StructLayout(LayoutKind.Sequential)]
        public struct NETRESOURCE
        {
            public int dwScope;
            public int dwType;
            public int dwDisplayType;
            public int dwUsage;
            public string lpLocalName;
            public string lpRemoteName;
            public string lpComment;
            public string lpProvider;
        }

        private async Task LoadFolders(string ip, string folderName, string username = "", string password = "")
        {
            string uncPath = $@"\\{ip}\{folderName}";

            bool exists = await Task.Run(() => Directory.Exists(uncPath));

            if (exists)
            {
                DisplayFolders(uncPath);
                return;
            }

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                CredentialWindow cred = new CredentialWindow();
                if (cred.ShowDialog() == true)
                {
                    username = cred.txtUsername.Text;
                    password = cred.txtPassword.Password;
                }
                else
                {
                    MessageBox.Show(TaskHive.Strings.Accesscancelled);
                    return;
                }
            }

            NETRESOURCE nr = new NETRESOURCE
            {
                dwType = 1,
                lpRemoteName = uncPath
            };

            int result = await Task.Run(() => WNetAddConnection2(ref nr, password, username, 0));

            if (result != 0)
            {
                MessageBox.Show($"{TaskHive.Strings.UnableConnecttonetwork} : {result}");
                return;
            }

            try
            {
                DisplayFolders(uncPath);
            }
            finally
            {
                WNetCancelConnection2(uncPath, 0, true);
            }
        }
        private void DisplayFolders(string path)
        {
            var folders = new List<FolderItem>();
            foreach (var dir in Directory.GetDirectories(path))
            {
                DirectoryInfo di = new DirectoryInfo(dir);
                folders.Add(new FolderItem { Name = di.Name, FullPath = di.FullName });
            }
            FoldersListView.ItemsSource = folders;
        }

        private void BtnFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf|WORD files (*.doc;*.docx)|*.doc;*.docx|EXCEL files (*.xls;*.xlsx)|*.xls;*.xlsx|All files (*.*)|*.*"
            };

            if (of.ShowDialog() == true && File.Exists(of.FileName))
            {
          
                txtFilename.Text = of.FileName;

            }
        }

        private async void Btnshare_Click(object sender, RoutedEventArgs e)
        {


            try {


                Ping ping = new Ping();
                var reply = await ping.SendPingAsync(this.txtip.Text, 1000);

                if (reply.Status != IPStatus.Success)
                {
                    MessageBox.Show("Host is not reachable");
                    return;
                }

                await LoadFolders(txtip.Text,txtsharname.Text);


            } catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
           this.Close();
        }

        private async Task FileTransfer()
        {

            if (this.txtName.Text == "" || txtFilename.Text == "") { MessageBox.Show(TaskHive.Strings.selectuser); return; }
            string sourceFile = txtFilename.Text;
            string destFile = System.IO.Path.Combine(txtPath.Text, System.IO.Path.GetFileName(sourceFile));

            try
            {

               await CopyFileWithProgress(sourceFile, destFile,this.MyprogressBar);
          
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private async Task CopyFileWithProgress(string sourceFile, string destFile, ProgressBar progressBar)
        {
            const int bufferSize = 1024 * 1024; // 1 MB
            byte[] buffer = new byte[bufferSize];
            long totalBytes = new FileInfo(sourceFile).Length;
            long totalRead = 0;

            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read))
            using (FileStream destStream = new FileStream(destFile, FileMode.Create, FileAccess.Write))
            {
                int bytesRead;
                while ((bytesRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    await destStream.WriteAsync(buffer, 0, bytesRead);
                    totalRead += bytesRead;

                    // محاسبه درصد پیشرفت
                    double progress = (double)totalRead / totalBytes * 100;
                    progressBar.Value = progress;
                }
            }

            MessageBox.Show(TaskHive.Strings.Filecopiedsuccessfully);
        }

        private void FoldersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FoldersListView.SelectedItem is FolderItem selectedFolder)
            {
                txtPath.Text = selectedFolder.FullPath;
                txtName.Text = selectedFolder.Name;
            }
        }

        private async void BtnSent_Click(object sender, RoutedEventArgs e)
        {
            this.MyprogressBar.Visibility = Visibility.Visible;
           await FileTransfer();
        }
    }
}
