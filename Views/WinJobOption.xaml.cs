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
using TaskHive.Classes;

namespace testapps.Views
{
    /// <summary>
    /// Interaction logic for WindowSetting.xaml
    /// </summary>
    public partial class WinJobOption : MetroWindow
    {

      
        public WinJobOption()
        {
            InitializeComponent();
            this.Width = GlobalApp.WidthMainForm -300;
            this.Height = GlobalApp.HeightMainForm -350;
            this.TextboxTime.Text = DateTime.Now.ToString("HH:mm");

            ClassJobs cj = new ClassJobs();
            var list = cj.SelectDuties(1);
            this.ComboJobList.ItemsSource = list;

 

        }




        public int IdDay = 0;
        public int idclient = 0;
        public int idtask;
        private void BtnSelectClient_Click(object sender, RoutedEventArgs e)
        {
            WinSelectClients wsc = new WinSelectClients();
            wsc.Owner = this;
            bool? msg = wsc.ShowDialog();

            if (msg == true) { TextBlockName.Text = wsc.fName + " " + wsc.lname ; idclient = wsc.id; }

        }
       public int con = 0;
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            switch (con)

             {

                case 1:

                    if (idclient == 0 || IdDay == 0 ) {

                        MessageBox.Show(TaskHive.Strings.selectcustomerregisterjob);
                        return;
                    
                    }

                    string des = string.IsNullOrEmpty(this.TextboxDes.Text) ? "No Comment" : this.TextboxDes.Text;
                   
                    ClassJobs cj = new ClassJobs();

                
                    cj.Insert_Task(IdDay.ToString(),this.TextBlockName.Text,this.ComboJobCaption.Text,des, this.ComboBpxCondition.Text, this.ComboJobList.Text,this.TextboxTime.Text, TaskHive.Strings.Active, idclient.ToString(),GlobalApp.uname);

                    break;

                    case 2:

                    string des1 = string.IsNullOrEmpty(this.TextboxDes.Text) ? "No Comment" : this.TextboxDes.Text;

                    ClassJobs cj1 = new ClassJobs();
                    bool? act = this.CheckboxActive.IsChecked;
                    string str_act = TaskHive.Strings.Active;
                    if (act == true) { str_act = TaskHive.Strings.Active; } else { str_act = TaskHive.Strings.Inactive; }
                        cj1.Update_Task(idclient, this.ComboJobCaption.Text, des1, this.ComboBpxCondition.Text, this.ComboJobList.Text, this.TextboxTime.Text, str_act);


                    break;



                    case 3:

                    ClassJobs cj2 = new ClassJobs();
                    MessageBoxResult msg = MessageBox.Show(TaskHive.Strings.areyousureyouwanttodelete,"",MessageBoxButton.YesNo);
                    if (msg == MessageBoxResult.Yes) { cj2.Delete_Task(idtask); } else { this.DialogResult = false; }
                    break;

            }

            this.DialogResult = true;

        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {

       

        }

        private void BtnSCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
