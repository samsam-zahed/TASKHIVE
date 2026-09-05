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
    public partial class WinSelectClients : MetroWindow
    {
        public WinSelectClients()
        {
            InitializeComponent();

            this.Width = SystemParameters.PrimaryScreenWidth / 2;
            this.Height = SystemParameters.PrimaryScreenHeight / 2;

        }

        private void Textbox_Text_Changed(object sender, TextChangedEventArgs e)
        {
           




        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.TexboxSearch.Focus();
            ClassClientSearch cs = new ClassClientSearch();
            cs.Select_All_Client(this.ClientsDataGrid);

        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {

            if (id == 0) {

                MessageBox.Show(TaskHive.Strings.selectuser);
                return;
            
            }

            DialogResult = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public string fName = "";
        public string lname = "";
        public int id = 0;
        public string address = "";

        private void LabelFilter_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                var item = comboxsearch.SelectedItem as ComboBoxItem;

                string myfield = item?.Tag?.ToString() ?? "";

                string mytext = this.TexboxSearch.Text;



                if (myfield != null)
                {

                    ClassClientSearch cs = new ClassClientSearch();
                    cs.Searchby(myfield, mytext, this.ClientsDataGrid);

                }



            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void ClientsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientsDataGrid.SelectedItem == null) return;

            { 

                var item = ClientsDataGrid.SelectedItem;
                var properties = item.GetType().GetProperties();


                id = (int?)properties[0].GetValue(item) ?? 0;
                fName = properties[1].GetValue(item)?.ToString() ?? "";
                lname = properties[2].GetValue(item)?.ToString() ?? "";
                address = properties[5].GetValue(item)?.ToString() ?? "";
                this.fullname.Text = fName + " " + lname;


            }

    

        }
    }

}
