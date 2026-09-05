using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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
using testapps.Views;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskHive.Views
{

   
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window10 : Window
    {

        DispatcherTimer timer;

        bool StartLoadin = false;
        public Window10()
        {
            InitializeComponent();
                   


            Show_Day_Active(DateTime.Now.Day,true);
            Get_Month(DateTime.Now.Month);

            ClearAllButtonInGrid(Visibility.Hidden);
            ClearAllTextBlocksInGrid("");


            int myyear = Convert.ToInt32(this.Date1.Text);
            int mymonth = NumberOfMonth;

            DateTime mydate = new DateTime(myyear, mymonth, 1);

            StartTimer();
            StartLoadin = true;

            int indexweek = Convert.ToInt32(mydate.DayOfWeek);
            Button_Click();
        

        }


        private void Get_Month(int index) {

            RadioButton[] rb = { M1, M2, M3, M4, M5, M6, M7, M8, M9, M10, M11, M12 };

            foreach (var panel in rb)
            {
                panel.IsChecked = false; 
            }

            rb[index -1].IsChecked = true;
            NumberOfMonth = Convert.ToInt32(rb[index - 1].Tag);
      
        }

        int NumberOfMonth = 1;

        public void ClearAllTextBlocksInGrid(string val)
        {
        
            this.CA1.Text = val;
            this.CA2.Text = val;
            this.CA3.Text = val;  
            this.CA4.Text = val;
            this.CA5.Text = val;   
            this.CA6.Text = val;
            this.CA7.Text = val;
            this.CA8.Text = val;
            this.CA9.Text = val;
            this.CA10.Text = val;
            this.CA11.Text = val;
            this.CA12.Text = val;
            this.CA13.Text = val;
            this.CA14.Text = val;
            this.CA15.Text = val;
            this.CA16.Text = val;
            this.CA17.Text = val;
            this.CA18.Text = val;
            this.CA19.Text = val;
            this.CA20.Text = val;
            this.CA21.Text = val;
            this.CA22.Text = val;
            this.CA23.Text = val;
            this.CA24.Text = val;
            this.CA25.Text = val;
            this.CA26.Text = val;
            this.CA27.Text = val;
            this.CA28.Text = val;
            this.CA29.Text = val;
            this.CA30.Text = val;
            this.CA31.Text = val;
            this.CA32.Text = val;
            this.CA33.Text = val;
            this.CA34.Text = val;
            this.CA35.Text = val;
            this.CA36.Text = val;
            this.CA37.Text = val;
            this.CA38.Text = val;
            this.CA39.Text = val;

        }
        public void ClearAllButtonInGrid(Visibility val)
        {

            this.CB1.Visibility = val;
            this.CB2.Visibility = val;
            this.CB3.Visibility = val;
            this.CB4.Visibility = val;
            this.CB5.Visibility = val;
            this.CB6.Visibility = val;
            this.CB7.Visibility = val;
            this.CB8.Visibility = val;
            this.CB9.Visibility = val;
            this.CB10.Visibility = val;
            this.CB11.Visibility = val;
            this.CB12.Visibility = val;
            this.CB13.Visibility = val;
            this.CB14.Visibility = val;
            this.CB15.Visibility = val;
            this.CB16.Visibility = val;
            this.CB17.Visibility = val;
            this.CB18.Visibility = val;
            this.CB19.Visibility = val;
            this.CB20.Visibility = val;
            this.CB21.Visibility = val;
            this.CB22.Visibility = val;
            this.CB23.Visibility = val;
            this.CB24.Visibility = val;
            this.CB25.Visibility = val;
            this.CB26.Visibility = val;
            this.CB27.Visibility = val;
            this.CB28.Visibility = val;
            this.CB29.Visibility = val;
            this.CB30.Visibility = val;
            this.CB31.Visibility = val;
            this.CB32.Visibility = val;
            this.CB33.Visibility = val;
            this.CB34.Visibility = val;
            this.CB35.Visibility = val;
            this.CB36.Visibility = val;
            this.CB37.Visibility = val;
            this.CB38.Visibility = val;
            this.CB39.Visibility = val;

        }

        List<TextBlock> calendarBlocks = new List<TextBlock>();

        private void GetCalendarBlocks(DependencyObject parent)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is TextBlock tb && tb.Tag?.ToString() == "cal")
                {
                    calendarBlocks.Add(tb);
                }

                GetCalendarBlocks(child);
            }
        }
        private void FillCalendar()
        {
            calendarBlocks.Clear();

            GetCalendarBlocks(this.Middle); 

           
            int myyear =Convert.ToInt32(Date1.Text);
            int mymont = NumberOfMonth;

            DateTime firstDay = new DateTime(myyear, mymont, 1);

            int daysInMonth = DateTime.DaysInMonth(myyear, mymont);

            int startIndex = 0;
      
            int day = 1;

            for (int i = 0; i < calendarBlocks.Count; i++)
            {
                if (i >= startIndex && day <= daysInMonth)
                {
                    calendarBlocks[i].Text = day.ToString();
                    day++;
                }
                else
                {
                    calendarBlocks[i].Text = "";
                }
            }
        }



        private void Button_Click()
        {

            int myyear =Convert.ToInt32(this.Date1.Text);
            int mymonth = NumberOfMonth;

            DateTime mydate = new DateTime(myyear, mymonth, 1);
            int indexweek = Convert.ToInt32(mydate.DayOfWeek);

    
           
            ClearAllButtonInGrid(Visibility.Hidden);
            ClearAllTextBlocksInGrid("");
            Change_Caption_Day(indexweek);
            FillCalendar();

            string YearNow = DateTime.Now.Year.ToString();
            string MonthCurrent = DateTime.Now.Month.ToString();
            string total_1 = YearNow + MonthCurrent;
            string total_2 = myyear.ToString() + mymonth.ToString();
            if (total_1 == total_2)
            {

                Show_Day_Active(DateTime.Now.Day, true);

            }
            else { Show_Day_Active(DateTime.Now.Day, false); }

            couter1 = 0;
            AddEventsToCalander(this.Middle);
            UpdateToday(true);

        }
      
   

        private void UpdateToday(bool IsDayNow)
        {

            if (IsDayNow==false) { return; }

            TextBlock[] textblocks = { LTMon, LTTue, LTWed, LTThu, LTFri, LTSat, LTSun };
            Label[] lb = { w1 , w2 , w3 , w4 , w5 , w6 , w7 };

            Brush normalColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF222222"));
            Brush activeColor = Brushes.LightGreen;

        
            string todayName = DateTime.Now.DayOfWeek.ToString().Substring(0, 3);

            DateTime dateday_1 = DateTime.Now;
            string year1 = dateday_1.Year.ToString();
            string month1 = dateday_1.Month.ToString();
            string day1 = dateday_1.Day.ToString(); 
            string total_1 = year1 + month1 + day1;

            string year2 = this.Date1.Text.ToString();
            string month2 = NumberOfMonth.ToString();
            string total_2 = year2 + month2 + day1;
            


            for (int i = 0; i < 7; i++)
            {
              
                textblocks[i].Text = "00:00";
                textblocks[i].Foreground = normalColor;

            }

    
            for (int i = 0; i < 7; i++)
            {
                if (lb[i].Content.ToString() == todayName && total_1 == total_2)
                {

                    textblocks[i].Text = DateTime.Now.ToString("HH:mm:ss");
                    textblocks[i].Foreground = activeColor;
                    break;
                }
      
            }
        }
        private void Change_Caption_Day(int dayofweek)
        {
            string[] days = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
            Label[] labels = { w1, w2, w3, w4, w5, w6, w7 };

            w1.Content = days[(0 + dayofweek) % 7];
            w2.Content = days[(1 + dayofweek) % 7];
            w3.Content = days[(2 + dayofweek) % 7];
            w4.Content = days[(3 + dayofweek) % 7];
            w5.Content = days[(4 + dayofweek) % 7];
            w6.Content = days[(5 + dayofweek) % 7];
            w7.Content = days[(6 + dayofweek) % 7];

            for (int i = 0; i < 7; i++)
            {
                string dayName = days[(i + dayofweek) % 7];
                labels[i].Content = dayName;

                if (dayName == "Sun")
                    labels[i].Foreground = Brushes.Red;
                else
                    labels[i].Foreground = Brushes.Gray; 
            }

        }

        int couter1 = 0;
        private void AddEventsToCalander(DependencyObject parent)
        {

            Button[] events = {CB1,CB2, CB3, CB4, CB5, CB6, CB7, CB8, CB9, CB10
            , CB11, CB12, CB13, CB14, CB15, CB16, CB17, CB18, CB19, CB20
            , CB21, CB22, CB23, CB24, CB25, CB26, CB27, CB28, CB29, CB30
            , CB31, CB32, CB33, CB34, CB35, CB36, CB37, CB38, CB39 };

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is TextBlock tb && tb.Tag?.ToString() == "cal")
                {

                    int year = Convert.ToInt32(this.Date1.Text);
                    if (tb.Text == "") { return; }
                    int day = Convert.ToInt32(tb.Text);
                    ReturnEvents(year,NumberOfMonth,day);

                    if (hasdata==true) {

                        events[couter1].Visibility = Visibility.Visible;
                        events[couter1].Content = "Event(s): " + NumberEvents;
                        events[couter1].Tag = jobid;
                        hasdata=false;

                    }
             

                    couter1++;
                }

                AddEventsToCalander(child);
            }
        }
       
        int jobid = 0;
        int NumberEvents = 0;
        bool hasdata = false;
        private void ReturnEvents(int year,int month,int day) {


            int StartDateYear = Convert.ToInt32(Date1.Text);
            DateTime StartDate = new DateTime(StartDateYear, NumberOfMonth, 1);
            int lastDay = DateTime.DaysInMonth(StartDateYear, NumberOfMonth);
            DateTime EndDate = new DateTime(StartDateYear, NumberOfMonth, lastDay);

            var mylist = LoadEventsForMonth(StartDate, EndDate);
            string total_2 = year.ToString() + month.ToString() + day.ToString();

            foreach (var item in mylist)
            {
                DateTime t = item.Date;
                string y = t.Year.ToString();
                string m = t.Month.ToString();
                string d = t.Day.ToString();
                string total_1 = (y + m + d);

                if (total_1 == total_2)
                {

                    jobid = item.JobIdday;
                    NumberEvents = item.EventCount;
                    hasdata = true;

                }

            }
        }

   


        private void Show_Day_Active(int index,bool showcolor)
        {
            StackPanel[] st =
            {
        sp1, sp2, sp3, sp4, sp5, sp6, sp7, sp8, sp9, sp10,
        sp11, sp12, sp13, sp14, sp15, sp16, sp17, sp18, sp19, sp20,
        sp21, sp22, sp23, sp24, sp25, sp26, sp27, sp28, sp29, sp30,
        sp31, sp32, sp33, sp34, sp35, sp36, sp37, sp38, sp39
    };

            if (index < 0 || index >= st.Length)
                return;

            foreach (var panel in st)
            {
                panel.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF222222"));
            }

            if (showcolor == true) { st[index - 1].Background = Brushes.LightSeaGreen; }
           
        }

        private void M1_Checked(object sender, RoutedEventArgs e)
        {
            NumberOfMonth =Convert.ToInt32(this.M1.Tag);
            Button_Click();
        }

        private void M2_Checked(object sender, RoutedEventArgs e)
        {
            if (StartLoadin == false) { return; }
            NumberOfMonth = Convert.ToInt32(this.M2.Tag);
            Button_Click();
        }

        private void M3_Checked(object sender, RoutedEventArgs e)
        {
            if (StartLoadin == false) { return; }
            NumberOfMonth = Convert.ToInt32(this.M3.Tag);
            Button_Click();
        }

        private void M4_Checked(object sender, RoutedEventArgs e)
        {
            if (StartLoadin == false) { return; }
            NumberOfMonth = Convert.ToInt32(this.M4.Tag);
            Button_Click();
        }

        private void M5_Checked(object sender, RoutedEventArgs e)
        {
            if (StartLoadin == false) { return; }
            NumberOfMonth = Convert.ToInt32(this.M5.Tag);
            Button_Click();
        }

        private void M6_Checked(object sender, RoutedEventArgs e)
        {
            if (StartLoadin == false) { return; }
            NumberOfMonth = Convert.ToInt32(this.M6.Tag);
            Button_Click();
        }

        private void M7_Checked(object sender, RoutedEventArgs e)
        {
            if (StartLoadin == false) { return; }
            NumberOfMonth = Convert.ToInt32(this.M7.Tag);
            Button_Click();
        }

        private void M8_Checked(object sender, RoutedEventArgs e)
        {
            if (StartLoadin == false) { return; }
            NumberOfMonth = Convert.ToInt32(this.M8.Tag);
            Button_Click();
        }

        private void M9_Checked(object sender, RoutedEventArgs e)
        {
            if (StartLoadin == false) { return; }
            NumberOfMonth = Convert.ToInt32(this.M9.Tag);
            Button_Click();
        }

        private void M10_Checked(object sender, RoutedEventArgs e)
        {
            if (StartLoadin == false) { return; }
            NumberOfMonth = Convert.ToInt32(this.M10.Tag);
            Button_Click();
        }

        private void M11_Checked(object sender, RoutedEventArgs e)
        {
            if (StartLoadin == false) { return; }
            NumberOfMonth = Convert.ToInt32(this.M11.Tag);
            Button_Click();
        }

        private void M12_Checked(object sender, RoutedEventArgs e)
        {
            if (StartLoadin == false) { return; }
            NumberOfMonth = Convert.ToInt32(this.M12.Tag);
            Button_Click();
        }

        private void Date1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Date1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter) {

                int a =Convert.ToInt32(this.Date1.Text);
                if (a < 2025)
                {

                    MessageBox.Show("please type a number bigger than 2025");
                    this.Date1.Text = DateTime.Now.Year.ToString();
                    return;


                }
                else {

                    Button_Click();

                }
            
            }
        }


        public List<CalendarEvent> LoadEventsForMonth(DateTime startDate, DateTime endDate)
        {
            var list = new List<CalendarEvent>();

            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = System.IO.Path.Combine(exePath, "DataBases", "OCServer.db");
            string connectionString = $"Data Source={dbPath}";

            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SqliteCommand(
                    @"SELECT TableDays.id AS JobIdday,TableDays.dayname AS Date,
                    COUNT(TableJob.id) AS EventCount FROM TableDays
                    LEFT JOIN TableJob ON TableJob.idday = TableDays.id
                    WHERE TableDays.dayname >= @startdate
                    AND TableDays.dayname < @enddate
                    GROUP BY TableDays.id, TableDays.dayname", conn))
                {
                    cmd.Parameters.AddWithValue("@startdate", startDate.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@enddate", endDate.ToString("yyyy-MM-dd"));

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new CalendarEvent
                            {
                                JobIdday = reader.GetInt32(0),
                                Date = reader.GetDateTime(1),
                                EventCount = reader.GetInt32(2)
                            });
                        }
                    }
                }
            }

            return list;
        }

       

        private void StartTimer()
        {
            timer = new DispatcherTimer();

           
            timer.Interval = TimeSpan.FromMicroseconds(1);
    
            timer.Tick += Timer_Tick;

            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateToday(true);
        }

        private void CB1_Click(object sender, RoutedEventArgs e)
        {

            WinJobs wj = new WinJobs();
            wj.ShowDialog();    

        }

        private void CB2_Click(object sender, RoutedEventArgs e)
        {

            WinJobs wj = new WinJobs();
            wj.ShowDialog();

        }

        private void CB3_Click(object sender, RoutedEventArgs e)
        {
            WinJobs wj = new WinJobs();
            wj.ShowDialog();
        }

        private void CB4_Click(object sender, RoutedEventArgs e)
        {
            WinJobs wj = new WinJobs();
            wj.ShowDialog();
        }

        private void CB5_Click(object sender, RoutedEventArgs e)
        {
            WinJobs wj = new WinJobs();
            wj.ShowDialog();
        }

        private void CB6_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB7_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB8_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB9_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB10_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB11_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB12_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB13_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB14_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB15_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB16_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB17_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB18_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB19_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB20_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB21_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB22_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB23_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB24_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB25_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB26_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB27_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB28_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB29_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB30_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB31_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB32_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB33_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB34_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB35_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB36_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB37_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB38_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB39_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
