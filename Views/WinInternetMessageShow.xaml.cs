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
    public partial class WinInternetMessageShow : MetroWindow
    {
        public WinInternetMessageShow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
          
        }

        ObservableCollection<InMessageUser> UserMessages = new ObservableCollection<InMessageUser>();
        private void Refresh_Lits_Users(int id)
        {

            ClassInternetMessageshow cu = new ClassInternetMessageshow();
            UserMessages = cu.Select_All_Messages(id);
            this.UsersDataGrid.ItemsSource = UserMessages;
            this.TextBlocCounts.Text = this.UsersDataGrid.Items.Count.ToString();

        }

    

        private void btnadd_Click(object sender, RoutedEventArgs e)
        {
            if (id == 0) { MessageBox.Show(TaskHive.Strings.userloginnotfound); return; }
            WinInternetUserPanel wininmesadd = new WinInternetUserPanel();
            wininmesadd.iduser = this.id;
            wininmesadd.txtfullname.Text = this.txtuser.Text;
            wininmesadd.btnselect.IsEnabled = false;
            wininmesadd.con = 1;
            bool? winmsg =  wininmesadd.ShowDialog();

            if (winmsg == true) {

                Refresh_Lits_Users(wininmesadd.iduser);


            }




        }



  
        InMessageUser? selecteduser;
        private void UsersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selecteduser = UsersDataGrid.SelectedItem as InMessageUser;
        }

        int id = 0;
        private void btnselectuser_Click(object sender, RoutedEventArgs e)
        {
            WinSelectClients winInternetUser = new WinSelectClients();
            bool? winmsg =  winInternetUser.ShowDialog();

            if (winmsg == true) {

                this.txtuser.Text = winInternetUser.fName + " " + winInternetUser.lname;
                id = winInternetUser.id;
                Refresh_Lits_Users(id);

            }
        }

        private void btnedit_Click(object sender, RoutedEventArgs e)
        {

            if (id == 0) { MessageBox.Show(TaskHive.Strings.userloginnotfound); return; }
            WinInternetUserPanel wininmesadd = new WinInternetUserPanel();
            wininmesadd.iduser = this.id;
            wininmesadd.txtfullname.Text = this.txtuser.Text;
            wininmesadd.btnselect.IsEnabled = false;
            wininmesadd.con = 2;
            if (selecteduser != null) { wininmesadd.txt.Text = selecteduser.describ; wininmesadd.idmsg = selecteduser.Id; } else { MessageBox.Show(TaskHive.Strings.selectuser); return; }
           
            bool? winmsg = wininmesadd.ShowDialog();

            if (winmsg == true)
            {

                Refresh_Lits_Users(wininmesadd.iduser);


            }


        }

        private void btndelete_Click(object sender, RoutedEventArgs e)
        {

            if (id == 0) { MessageBox.Show(TaskHive.Strings.userloginnotfound); return; }
            WinInternetUserPanel wininmesadd = new WinInternetUserPanel();
            wininmesadd.iduser = this.id;
            wininmesadd.txtfullname.Text = this.txtuser.Text;
            wininmesadd.txt.IsEnabled = false;
            wininmesadd.btnselect.IsEnabled = false;
            wininmesadd.con = 3;
            if (selecteduser != null) { wininmesadd.txt.Text = selecteduser.describ; wininmesadd.idmsg = selecteduser.Id; } else { MessageBox.Show(TaskHive.Strings.selectuser); return; }

            bool? winmsg = wininmesadd.ShowDialog();

            if (winmsg == true)
            {

                Refresh_Lits_Users(wininmesadd.iduser);


            }


        }

        private void btnrefresh_Click(object sender, RoutedEventArgs e)
        {

            if (id == 0) { MessageBox.Show(TaskHive.Strings.userloginnotfound); return; }
 
            if (selecteduser != null) { 
            
                ClassInternetMessageshow cls = new ClassInternetMessageshow();
                cls.EnableDesable(selecteduser.Id,selecteduser.ConditionMessage);
            
            
            } else { MessageBox.Show(TaskHive.Strings.selectuser); return; }

             Refresh_Lits_Users(this.id);



        }
    }
}
