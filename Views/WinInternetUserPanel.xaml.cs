using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Security.Permissions;
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
    public partial class WinInternetUserPanel : MetroWindow
    {

        public int con = 0;
        public int idmsg = 0;
       
        public WinInternetUserPanel()
        {
            InitializeComponent();
            this.Width = GlobalApp.WidthMainForm - 400;
            this.Height = GlobalApp.HeightMainForm - 450;
        }
      public int idclient =0;
        private void btnselect_Click(object sender, RoutedEventArgs e)
        {
            WinSelectClients wsc = new WinSelectClients();
            wsc.Owner = this;
            bool? msg = wsc.ShowDialog();

            if (msg == true) { txtfullname.Text = wsc.fName + " " + wsc.lname; idclient = wsc.id; }
        }

        public int iduser = 0;
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            try {

                if (con == 1) {

                    ClassInternetMessageshow clm = new ClassInternetMessageshow();
                    string mytime = DateTime.Now.ToString();
                    string con = "Enable";
                    string sit = "False";
                    clm.Insert(iduser, mytime, this.txt.Text, con, sit);
                    this.DialogResult = true;

                }


                if (con == 2) {

                    ClassInternetMessageshow clm = new ClassInternetMessageshow();
                    string mytime = DateTime.Now.ToString();
                    clm.Update(idmsg, mytime,this.txt.Text);
                    this.DialogResult = true;

                }


                if (con == 3)
                {

                    ClassInternetMessageshow clm = new ClassInternetMessageshow();
                    clm.Delete(idmsg);
                    this.DialogResult = true;

                }


            } catch (Exception ex) {

                MessageBox.Show(ex.Message);
            
            }

      
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult=false;
        }
    }
}
