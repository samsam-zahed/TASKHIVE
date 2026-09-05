using ControlzEx.Standard;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using MahApps.Metro.Controls;
using Microsoft.Data.Sqlite;
using SharpVectors.Dom.Events;
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
using TaskHive.Classes;


namespace testapps.Views
{

    public class CalendarRow
    {
        public CalendarDay Mon { get; set; } = new CalendarDay();
        public CalendarDay Tue { get; set; } = new CalendarDay();
        public CalendarDay Wed { get; set; } = new CalendarDay();
        public CalendarDay Thu { get; set; } = new CalendarDay();
        public CalendarDay Fri { get; set; } = new CalendarDay();
        public CalendarDay Sat { get; set; } = new CalendarDay();
        public CalendarDay Sun { get; set; } = new CalendarDay();
    }
    public class CalendarDay
    {
        public int? DayNumber { get; set; }
        public int EventCount { get; set; }
    }

    public class CalendarEvent
    {

        public int JobIdday { get; set; }
        public DateTime Date { get; set; }
        public int EventCount { get; set; }


    }


    /// <summary>
    /// Interaction logic for WindowSetting.xaml
    /// </summary>
    public partial class WinCalendar : MetroWindow
    {
        public ObservableCollection<CalendarRow> CalendarRows { get; set; } = new ObservableCollection<CalendarRow>();
        public WinCalendar()
        {
            InitializeComponent();
                 
        }

        private void RefreshCalendar()
        {
            if (!int.TryParse(TextboxYear.Text, out int year))
                return;

            if (!int.TryParse(TextboxMonth.Text, out int month))
                return;

            if (month < 1 || month > 12)
                return;

            CalendarRows = GenerateCalendar(year, month);
            DataGridCalendar.ItemsSource = CalendarRows;
          


        }


        
      
        public ObservableCollection<CalendarRow> GenerateCalendar(int year, int month)
        {
            var now = DateTime.Now;
            DateTime start = new DateTime(now.Year, now.Month, 1);
            DateTime end = start.AddMonths(1);

            var result = new ObservableCollection<CalendarRow>();
            var events = LoadEventsForMonth(start, end); 

            var firstDay = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);

            int startDay = (int)firstDay.DayOfWeek;
            if (startDay == 0) startDay = 7;

            int currentDay = 1;

            while (currentDay <= daysInMonth)
            {
                var row = new CalendarRow();

                for (int i = 1; i <= 7; i++)
                {
                    if ((result.Count == 0 && i < startDay) || currentDay > daysInMonth)
                    {
                        SetDay(row, i, null);
                    }
                    else
                    {
                         SetDay(row, i, currentDay);

                        // اضافه کردن رویدادها به روز فعلی
                        CalendarDay day = GetDayFromRow(row, i);

                        var dayDate = new DateTime(year, month, currentDay);

                        if (day != null)
                        day.EventCount = events.Count(ev => ev.Date.Date == dayDate.Date);


                        currentDay++;
                    }
                }

                result.Add(row);
            }

            return result;
        }

        private CalendarDay GetDayFromRow(CalendarRow row, int dayIndex)
        {
            switch (dayIndex)
            {
                case 1: return row.Mon;
                case 2: return row.Tue;
                case 3: return row.Wed;
                case 4: return row.Thu;
                case 5: return row.Fri;
                case 6: return row.Sat;
                case 7: return row.Sun;
                default: return null;
            }
        }





        private void SetDay(CalendarRow row, int dayIndex, int? value)
        {
            switch (dayIndex)
            {
                case 1: row.Mon.DayNumber = value; break;
                case 2: row.Tue.DayNumber = value; break;
                case 3: row.Wed.DayNumber = value; break;
                case 4: row.Thu.DayNumber = value; break;
                case 5: row.Fri.DayNumber = value; break;
                case 6: row.Sat.DayNumber = value; break;
                case 7: row.Sun.DayNumber = value; break;
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



        private void TextboxMonth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) { 
            
                RefreshCalendar();
               
            }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            //ShowAllCells();
        }

        //private void ShowAllCells()
        //{
          

        //    foreach (var item in DataGridCalendar.Items)
        //    {
        //        foreach (var column in DataGridCalendar.Columns)
        //        {
        //            if (column is DataGridTextColumn textColumn)
        //            {
        //                var binding = textColumn.Binding as Binding;
        //                string propertyName = binding.Path.Path;

        //                var value = item.GetType()
        //                                .GetProperty(propertyName)
        //                                ?.GetValue(item);

        //                if (value == null || value.ToString() == "")
        //                {
        //                    var cellContent = column.GetCellContent(item);
        //                    if (cellContent != null && cellContent.Parent is DataGridCell cell)
        //                    {
        //                        cell.Background = Brushes.DarkGray;
        //                    }
        //                }
        //            }
        //        }

            
        //    }

         
        //}

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {

            var now = DateTime.Now;

            TextboxYear.Text = now.Year.ToString();
            TextboxMonth.Text = now.Month.ToString();

            CalendarRows = GenerateCalendar(now.Year, now.Month);

            DateTime start = new DateTime(now.Year,now.Month,1);
            DateTime end = start.AddMonths(1);

            DataContext = this;

            RefreshCalendar();
        }

        private void DataGridCalendar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Find the clicked cell
            var depObj = (DependencyObject)e.OriginalSource;
            while (depObj != null && !(depObj is DataGridCell))
                depObj = VisualTreeHelper.GetParent(depObj);

            if (depObj == null)
                return;

            var cell = depObj as DataGridCell;
            var row = FindVisualParent<DataGridRow>(cell);
            if (row == null)
                return;

            var calendarRow = row.Item as CalendarRow;
            if (calendarRow == null)
                return;

            // Determine which column was clicked
            var columnHeader = cell.Column.Header.ToString();

            CalendarDay? day = columnHeader 
                switch
            {
                "Mon" => calendarRow.Mon,
                "Tue" => calendarRow.Tue,
                "Wed" => calendarRow.Wed,
                "Thu" => calendarRow.Thu,
                "Fri" => calendarRow.Fri,
                "Sat" => calendarRow.Sat,
                "Sun" => calendarRow.Sun,
                _ => null
            };

            if (day != null)
            {
                MessageBox.Show($"Day: {day.DayNumber}, EventCount: {day.EventCount}");
            }
        }


        public static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);
            while (parent != null && !(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return parent as T;
        }

    }
}
