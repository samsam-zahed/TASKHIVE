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
using MahApps.Metro.Controls;
using Microsoft.Win32;
using TaskHive.Classes;

namespace testapps.Views
{
    /// <summary>
    /// Interaction logic for WindowSetting.xaml
    /// </summary>
    public partial class WinBackUp : MetroWindow
    {
        public int con = 0;
        public WinBackUp()
        {
            InitializeComponent();


            this.Width = SystemParameters.PrimaryScreenWidth / 2;
            this.Height = SystemParameters.PrimaryScreenHeight / 2;

        }

        private void btnopenfile_Click(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog of = new OpenFolderDialog();
            if (of.ShowDialog() == true)
            {
                txtbackuppath.Text = of.FolderName;
                string myyear = DateTime.Now.ToString("yyyy");
                string myMonth = DateTime.Now.ToString("MM");
                string myDay = DateTime.Now.ToString("dd");
                string mydate = myyear + "_" + myMonth + "_" + myDay + ".db";
                txtbackfilename.Text = "BackUp_" + mydate;
            }
        }

        private async void btnsave_Click(object sender, RoutedEventArgs e)
        {

            try {


                if (con == 1)
                {
                    if (txtbackuppath.Text == "") { MessageBox.Show(TaskHive.Strings.MessageSelectPath); return; }
                    ClassBackupAndRestore cb = new ClassBackupAndRestore();
                    await cb.BackUp(txtbackuppath.Text, txtbackfilename.Text);

                }

                if (con == 2)
                {
                    if (txtRestorepath.Text == "") { MessageBox.Show(TaskHive.Strings.MessageFileRestore); return; }
                    ClassBackupAndRestore cb = new ClassBackupAndRestore();
                    await cb.RestoreAsync(txtRestorepath.Text);

                }


            } catch (Exception ex) { MessageBox.Show(ex.Message); }

   
        }

        private void btnopenfilerestore_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            if (of.ShowDialog() == true)
            { 
            
                this.txtRestorepath.Text = of.FileName;
            
            }



            }

        private void btncancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
