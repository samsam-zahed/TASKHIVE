using MahApps.Metro.Controls;
using Microsoft.Win32;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaskHive;
using TaskHive.Classes;
using TaskHive.Reports;


namespace testapps.Views
{
    /// <summary>
    /// Interaction logic for WindowSetting.xaml
    /// </summary>
    public partial class WinJobs : MetroWindow
    {
        bool? comborun = true;
        public WinJobs()
        {
            InitializeComponent();
            comborun=false;
            this.toolbar1.Width = GlobalApp.WidthMainForm;

   

        }

 
        int idday = 0;
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {

            ClassJobs cg = new ClassJobs();

            ObservableCollection<TableJobs> joblist = cg.Select_All_Jobs(TaskHive.Strings.NotStarted, TaskHive.Strings.Active);
            this.DataGridjobs.ItemsSource = joblist;
           

        }

        private void ComboboxConditionJob_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (!comborun == true) {

                try
                {

                    ComboBoxItem item = (ComboBoxItem)ComboboxConditionJob.SelectedItem;
                    string value = item.Content.ToString() ?? "";
                    ClassJobs cg = new ClassJobs();
                    ObservableCollection<TableJobs> joblist = cg.Select_All_Jobs(value, TaskHive.Strings.Active);
                    this.DataGridjobs.ItemsSource = joblist;

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            }

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {


    

                    WinJobOption wj = new WinJobOption();
                
                    wj.MyDatetextbox.Text = ClassYear.DateDay;
                    wj.IdDay = ClassYear.DateId;
               
                    wj.CheckboxActive.IsEnabled = false;
                    wj.ComboBpxCondition.IsEnabled = false;
                    wj.ComboBpxCondition.SelectedIndex = 0;   
                    wj.con = 1;
                    wj.Owner = this;
                    bool? msg =  wj.ShowDialog();

                    if (msg == true) { Btnrefresh_Click(sender,e); }

                


            }

      

        private void TextboxID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) {
                BtnAdd_Click(sender, e);
            }
         
                
         }

        private void TextboxID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

            if (Items == null) { return; }
            else
            {

                WinJobOption wj = new WinJobOption();
                wj.con = 3;

                wj.MyDatetextbox.Text = ClassYear.Return_DateName(Items.idday);
                wj.TextBlockName.Text = Items.customername;

                wj.ComboJobCaption.Text = Items.jobcaption;
                wj.ComboJobList.Text = Items.jobduty;
                wj.TextboxDes.Text = Items.jobcomment;
                wj.TextboxTime.Text = Items.jobtime;
                wj.ComboBpxCondition.Text = Items.jobsit;
                wj.idclient = Items.id;
                wj.idtask = Items.id;
              
                wj.ComboBpxCondition.IsEnabled = false;
                wj.ComboJobCaption.IsEnabled = false;
                wj.BtnSelectClient.IsEnabled = false;
                wj.CheckboxActive.IsEnabled = false;
                wj.ComboJobList.IsEnabled = false;
                wj.TextboxDes.IsEnabled = false;
                wj.TextboxTime.IsEnabled = false;


                wj.Owner = this;
                bool? msg = wj.ShowDialog();

            }




        }

        TableJobs? Items;
        private void DataGridjobs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Items = this.DataGridjobs.SelectedItem as TableJobs ;
          

        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (Items == null) { return; } else {

                WinJobOption wj = new WinJobOption();
                wj.con =2 ;

                wj.MyDatetextbox.Text = ClassYear.Return_DateName(Items.idday);
                wj.TextBlockName.Text = Items.customername;
             
                wj.ComboJobCaption.Text = Items.jobcaption;
                wj.ComboJobList.Text = Items.jobduty;
                wj.TextboxDes.Text = Items.jobcomment;
                wj.TextboxTime.Text = Items.jobtime;
                wj.ComboBpxCondition.Text = Items.jobsit;
                wj.idclient = Items.id;
                wj.BtnSelectClient.IsEnabled = false;
                wj.CheckboxActive.IsEnabled = true;
                wj.ComboBpxCondition.IsEnabled = true;

                wj.Owner = this;
                bool? msg = wj.ShowDialog();

            }
        }


        private void Btnrefresh_Click(object sender, RoutedEventArgs e)

        {
            ClassJobs cg = new ClassJobs();
            ObservableCollection<TableJobs> joblist = cg.Select_All_Jobs(TaskHive.Strings.NotStarted, TaskHive.Strings.Active);
            this.DataGridjobs.ItemsSource = joblist;

        }

        private void Btnstatus_Click(object sender, RoutedEventArgs e)
        {
            ClassJobs cg = new ClassJobs();
            ObservableCollection<TableJobs> joblist = cg.Select_All_Jobs(TaskHive.Strings.NotStarted, TaskHive.Strings.Inactive);
            this.DataGridjobs.ItemsSource = joblist;
        }

        private void Btnsearch_Click(object sender, RoutedEventArgs e)
        {
            WinClientOption win = new WinClientOption();
            win.Owner = this;
            bool? winbool =  win.ShowDialog();
            if (winbool == true) {

                this.DataGridjobs.ItemsSource = win.joblist;
            
            }
        }

        private void BtnrePrint_Click(object sender, RoutedEventArgs e)
        {

            List<TableJobs> SelectedClients = new List<TableJobs>();

            foreach (TableJobs cli in DataGridjobs.SelectedItems)
            {
                SelectedClients.Add(cli);
            }

            if (SelectedClients.Count == 0)
            {
                foreach (TableJobs cli in DataGridjobs.Items)
                {
                    SelectedClients.Add(cli);
                }
            }

            var report = new ReportJobs(SelectedClients);

            string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PrePrint");
            string fileName = "ReportJobs.pdf";
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

        private void Btnselectuser_Click(object sender, RoutedEventArgs e)
        {

            WinInternetUserPanel win = new WinInternetUserPanel();
            win.txtfullname.Text = Items.customername;
            win.idclient = Items.idcustomer;
            win.Owner = this;
            bool? winbool = win.ShowDialog();

        }
    }
}
