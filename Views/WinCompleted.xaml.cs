using DocumentFormat.OpenXml.Spreadsheet;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaskHive.Classes;
using TaskHive.Reports;

namespace testapps.Views
{
    /// <summary>
    /// Interaction logic for WindowSetting.xaml
    /// </summary>
    public partial class WinCompleted : MetroWindow
    {
        public WinCompleted()
        {
            InitializeComponent();

            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            this.WindowStartupLocation = WindowStartupLocation.Manual;

          
            this.Width = screenWidth ;
            this.Height = screenHeight;

          
            double finalLeft = screenWidth - this.Width;

           
            this.Left = screenWidth ;
            this.Top = 0;

           
            DoubleAnimation slideAnimation = new DoubleAnimation
            {
                From = screenWidth,
                To = finalLeft,
                Duration = TimeSpan.FromMilliseconds(400),
                EasingFunction = new QuadraticEase()  
            };

            this.BeginAnimation(Window.LeftProperty, slideAnimation);

        }

        private void btnclose_Click(object sender, RoutedEventArgs e)
        {
            //this.webbrowser1.Visibility = Visibility.Hidden;
            DoubleAnimation fadeOut = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(400)
            };

            fadeOut.Completed += (s, _) =>
            {
                this.Close();  
            };

            this.BeginAnimation(Window.OpacityProperty, fadeOut);
        }

     

    

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {

            RefreshList();

        }

        private void RefreshList()
        {

            ClassTableJobsub ct = new ClassTableJobsub();
            ObservableCollection<TableJobssub> list = ct.Select_All_Status(TaskHive.Strings.Completed);
            this.DataGridjobs.ItemsSource = list;
            this.txtcount.Text = DataGridjobs.Items.Count.ToString();

        }
        bool MyNull = false;
        TableJobssub? items;
    
        private void DataGridjobs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

           

            items = this.DataGridjobs.SelectedItem as TableJobssub;
            if (items == null) { return; }
            this.txtfullname.Text = items.customername;
        
        }

        private void BtnFind_Click(object sender, RoutedEventArgs e)
        {

            ClassTableJobsub ts = new ClassTableJobsub();
            ObservableCollection<TableJobssub> list = ts.Search_by(this.comboSearch.SelectedIndex, this.txtSearch.Text);
            this.DataGridjobs.ItemsSource = list;
            this.txtcount.Text = DataGridjobs.Items.Count.ToString();

        }

        private void Btnprint_Click(object sender, RoutedEventArgs e)
        {

            if (items == null) { MessageBox.Show(TaskHive.Strings.selectrequest);return; }

            var selected = DataGridjobs.SelectedItems
     .Cast<TableJobssub>()
     .ToList();

            var report = new JobCompletedReport(selected);

            string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PrePrint");
            string fileName = "report.pdf";
            string filePath = System.IO.Path.Combine(folderPath, fileName);
            report.GeneratePdf(filePath);


            OpenFileDialog fil = new OpenFileDialog();
            fil.FileName = filePath;

            string fileselected = fil.FileName;
            WinPrint wp = new WinPrint();
            wp.webprint.Source = new Uri(fileselected);
            wp.webprint.ZoomFactor = 2;
            wp.Topmost = true;
            wp.ShowDialog();


        }
    }
}
