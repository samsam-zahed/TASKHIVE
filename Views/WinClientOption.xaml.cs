using DocumentFormat.OpenXml.Office.PowerPoint.Y2021.M06.Main;
using MahApps.Metro.Controls;
using Microsoft.Windows.Themes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    public partial class WinClientOption : MetroWindow
    {
        public int con = 0;
        public WinClientOption()
        {
            InitializeComponent();

           
            ClassJobs cj = new ClassJobs();
            var list = cj.SelectDuties(1);
            this.ComboBoxResponsible.ItemsSource = list;
            this.ComboBoxResponsible.SelectedIndex = 0;

            this.ComboBoxCondition.SelectedIndex = 0;
            this.ComboBoxCaption.SelectedIndex = 0;
            this.ComboBoxEnable.SelectedIndex = 0;
            

        }


        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {

            string qurysql = "Select * From TableJob";
            string mywhere = " Where";
            string resutl = "";
            //------------------------------------------------

            int couter = 0;
            foreach (var child in this.grid.Children)
            {



                if (child is CheckBox cb)
                {


                    bool isChecked = cb.IsChecked ?? false;
                    if (cb.Name == "CheckBoxFrom" && cb.IsEnabled && isChecked)
                    {
                        if (this.DateFrom.SelectedDate == null || this.DateTo.SelectedDate == null) { MessageBox.Show("Please select a date!", "#FFE7FF12"); return; }

                        string d1 = this.DateFrom.SelectedDate.Value.ToString("yyyy-MM-dd");
                        string d2 = this.DateTo.SelectedDate.Value.ToString("yyyy-MM-dd");
                        int val1 = ClassYear.Return_DateId(d1);
                        int val2 = ClassYear.Return_DateId(d2);

                        string coldate = $" idday between {val1} and {val2}";
                        mywhere += coldate;
             


                    }
                    else { couter += 1; }


                    //--------------------------------------------------------------

                    if (cb.Name == "CheckBoxCaption" && cb.IsEnabled && isChecked)
                    {

                        string val1 = ComboBoxCaption.Text;
                        string colcaption = $" and jobcaption = '{val1}'";
                        mywhere += colcaption;
                     

                    }
                    else { couter += 1; }

                    //--------------------------------------------------------------

                    if (cb.Name == "CheckBoxCondition" && cb.IsEnabled && isChecked)
                    {

                        string val1 = ComboBoxCondition.Text;
                        string colCondition = $" and jobsit = '{val1}'";
                        mywhere += colCondition;
                 

                    }
                    else { couter += 1; }

                    //--------------------------------------------------------------

                    if (cb.Name == "CheckBoxResponsible" && cb.IsEnabled && isChecked)
                    {

                        string val1 = ComboBoxResponsible.Text;
                        string colResponsible = $" and jobduty = '{val1}'";
                        mywhere += colResponsible;
                     

                    }
                    else { couter += 1; }

                    //--------------------------------------------------------------

                    if (cb.Name == "CheckBoxEnable" && cb.IsEnabled && isChecked)
                    {

                        string val1 = ComboBoxEnable.Text;
                        string colEnable = $" and jobenable = '{val1}'";
                        mywhere += colEnable;
                    

                    }
                    else { couter += 1; }

                    //--------------------------------------------------------------

                    if (cb.Name == "CheckBoxTextSearch" && cb.IsEnabled && isChecked)
                    {
                        string mycol = "";
                        if (this.ComboBoxTextSearch.SelectedIndex == 0) { mycol = "idcustomer"; } else if (this.ComboBoxTextSearch.SelectedIndex == 1) { mycol = "customername"; }
                        string val1 = TextBoxSearch.Text;

                        string colSearch = $" and {mycol} like '{val1}%'";
                        mywhere += colSearch;
                

                    }
                    else { couter += 1; }

                    //--------------------------------------------------------------

                    if (cb.Name == "CheckBoxTime" && cb.IsEnabled && isChecked)
                    {

                        string val1 = TextBoxTime.Text;
                        string colTime = $" and jobtime = '{val1}'";
                        mywhere += colTime;
               
                    }
                    else { couter += 1; }


                }

            }

            if (this.CheckBoxFrom.IsChecked == true)
            { resutl = mywhere; }
            else
            {
                resutl = ClearFirsAndFromQueryStringSQL(mywhere);
            }
 
            if (couter == 49)
            { searchbyquerystring(qurysql); }
            else { searchbyquerystring(qurysql + resutl); }



                GlSearchJob.CheckBoxTextSearch = CheckBoxTextSearch.IsChecked;
                GlSearchJob.ComboboxIndexBoxTextSearch = this.ComboBoxTextSearch.SelectedIndex;
                GlSearchJob.TextboxText = this.TextBoxSearch.Text;

                GlSearchJob.CheckBoxFrom = CheckBoxFrom.IsChecked;
                GlSearchJob.TextboxTextDate1 = DateFrom.Text;
                GlSearchJob.TextboxTextDate2 = DateTo.Text;

                GlSearchJob.CheckBoxCaption = CheckBoxCaption.IsChecked;
                GlSearchJob.ComboboxCaption = this.ComboBoxCaption.SelectedIndex; 

                GlSearchJob.CheckBoxCondition = CheckBoxCondition.IsChecked;
                GlSearchJob.ComboboxCondition = this.ComboBoxCondition.SelectedIndex;

                GlSearchJob.CheckBoxResponsible = CheckBoxResponsible.IsChecked;
                GlSearchJob.ComboboxResponsible = this.ComboBoxResponsible.SelectedIndex;

                GlSearchJob.CheckBoxEnable = CheckBoxEnable.IsChecked;
                GlSearchJob.ComboboxEnable = this.ComboBoxEnable.SelectedIndex;

                GlSearchJob.CheckBoxTime = CheckBoxTime.IsChecked;
                GlSearchJob.textboxttime = this.TextBoxTime.Text;


            this.DialogResult = true;

        }

        public ObservableCollection<TableJobs> joblist;
        private void searchbyquerystring(string querysting)
        {
            ClassJobs cj = new ClassJobs();
            string sq = querysting;
            joblist = cj.SelectJobTask_by_IDday(sq);

        }

        private string ClearFirsAndFromQueryStringSQL(string _text)
        {
            string text = _text;

            var match = System.Text.RegularExpressions.Regex.Match(
                text,
                @"\band\b",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            if (match.Success)
            {
                text = text.Remove(match.Index, match.Length);
            }

            return text;
        }
        private void TextBoxTime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            TextBox textBox = sender as TextBox;


            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
                return;
            }


            int selectionStart = textBox.SelectionStart;
            int selectionLength = textBox.SelectionLength;
            string currentText = textBox.Text.Remove(selectionStart, selectionLength);


            string newText = currentText.Insert(selectionStart, e.Text);


            string digitsOnly = newText.Replace(":", "");
            if (digitsOnly.Length > 4)
            {
                e.Handled = true;
                return;
            }


            if (digitsOnly.Length > 2)
                digitsOnly = digitsOnly.Insert(2, ":");

            textBox.Text = digitsOnly;

            textBox.CaretIndex = textBox.Text.Length;
            e.Handled = true;

        }

        private void btncancle_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void WinUser_Loaded(object sender, RoutedEventArgs e)
        {

            if (GlSearchJob.CheckBoxCaption == true) 
            { 

                this.CheckBoxCaption.IsChecked = true;
                this.ComboBoxCaption.SelectedIndex = GlSearchJob.ComboboxCaption;

            } 
            else
            {
                this.CheckBoxCaption.IsChecked = false;
                this.ComboBoxCaption.SelectedIndex = 0;
            }




            if (GlSearchJob.CheckBoxCondition == true) 
            { 
                this.CheckBoxCondition.IsChecked = true;
                this.ComboBoxCondition.SelectedIndex = GlSearchJob.ComboboxCondition;
            } 
            else 
            {
                this.CheckBoxCondition.IsChecked = false;
                this.ComboBoxCondition.SelectedIndex = 0;
            }



            if (GlSearchJob.CheckBoxEnable == true) 
            {
                CheckBoxEnable.IsChecked = true;
                this.ComboBoxEnable.SelectedIndex = GlSearchJob.ComboboxEnable;
            } else 
            {
                CheckBoxEnable.IsChecked = false;
                this.ComboBoxEnable.SelectedIndex = 0;
            }

            if (GlSearchJob.CheckBoxFrom == true) 
            {
                CheckBoxFrom.IsChecked = true;
                DateFrom.Text = GlSearchJob.TextboxTextDate1;
                DateTo.Text = GlSearchJob.TextboxTextDate2;
            }
            else
            {

                CheckBoxFrom.IsChecked = false;
                DateFrom.Text = "";
                DateTo.Text = "";

            }

            if (GlSearchJob.CheckBoxResponsible == true) 
            {
                CheckBoxResponsible.IsChecked = true;
                ComboBoxResponsible.SelectedIndex = GlSearchJob.ComboboxResponsible;
            } 
            else
            {
                CheckBoxResponsible.IsChecked = false;
                ComboBoxResponsible.SelectedIndex =0;
            }

            if (GlSearchJob.CheckBoxTextSearch == true) 
            { 
                CheckBoxTextSearch.IsChecked = true;
                ComboBoxTextSearch.SelectedIndex = GlSearchJob.ComboboxIndexBoxTextSearch;
                this.TextBoxSearch.Text=GlSearchJob.TextboxText;
            } 
            else 
            {
                CheckBoxTextSearch.IsChecked = false;
                ComboBoxTextSearch.SelectedIndex = 0;

            }
            if (GlSearchJob.CheckBoxTime == true) {

                CheckBoxTime.IsChecked = true;
                TextBoxTime.Text = GlSearchJob.textboxttime;
            } else {

                CheckBoxTime.IsChecked = false;
                TextBoxTime.Text = "00:00";

            }

        }
    }
}
