using DocumentFormat.OpenXml.Spreadsheet;
using MahApps.Metro.Controls;
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

namespace testapps.Views
{
    /// <summary>
    /// Interaction logic for WindowSetting.xaml
    /// </summary>
    public partial class WinOngoing : MetroWindow
    {
        public WinOngoing()
        {
            InitializeComponent();

            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            this.WindowStartupLocation = WindowStartupLocation.Manual;

          
            this.Width = screenWidth / 2;
            this.Height = screenHeight;

          
            double finalLeft = screenWidth - this.Width;

           
            this.Left = screenWidth / 2;
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
            ObservableCollection<TableJobssub> list = ct.Select_All_Ongoing("True");
            this.DataGridjobs.ItemsSource = list;

        }
        bool MyNull = false;
        TableJobssub? items;
        int idjob = 0;
        int idsub = 0;
        private void DataGridjobs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

           

            items = this.DataGridjobs.SelectedItem as TableJobssub;
            if (items == null) { return; }
            this.txtfullname.Text = items.customername;
            idsub = items.idsub;
            idjob = items.idjob;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (items == null) { MessageBox.Show(TaskHive.Strings.selectuser); return; }

            ClassTableJobsub ct = new ClassTableJobsub();
            ct.Update(idsub, txrcomment.Text,"False");
            ClassJobs cj = new ClassJobs();
            cj.Change_jobsit(idjob, TaskHive.Strings.Completed);
            MessageBox.Show(TaskHive.Strings.Savedsuccessfully);
            RefreshList();
            this.txrcomment.Text = "Comment";
            this.txtfullname.Text = "";

        }
    }
}
