using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaskHive.Classes;
using static testapps.Views.WinSelectInternetUser;

namespace testapps.Views
{
    /// <summary>
    /// Interaction logic for WindowSetting.xaml
    /// </summary>
    public partial class WinSelectInternetUser : MetroWindow
    {



        public WinSelectInternetUser()
        {
            InitializeComponent();

            this.Width = SystemParameters.PrimaryScreenWidth / 2;
            this.Height = SystemParameters.PrimaryScreenHeight / 2;

        }



        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.TexboxSearch.Focus();
            ClassInternetSelectClient Ci = new ClassInternetSelectClient();
            Ci.Select_All_Client(this.ClientsDataGrid);

        }



        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        bool? IsHasDataCounter = false;
        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {

            StringBuilder sb = new StringBuilder();
            int counter = 0;
            foreach (var item in ClientsDataGrid.Items)
            {
                DataGridRow row = (DataGridRow)ClientsDataGrid.ItemContainerGenerator.ContainerFromItem(item);

                if (row != null)
                {
                    var presenter = FindVisualChild<DataGridCellsPresenter>(row);

                    var cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(0);

                    var chk = FindVisualChild<CheckBox>(cell);

                    if (chk != null && chk.IsChecked == true)
                    {
                        var user = item as Internetclient;

                        ClassInternetCreateUser cu = new ClassInternetCreateUser();

                        if (user != null)
                        {

                            bool IsExisted = cu.Check_User_IsExisted(user.id.ToString());

                            if (IsExisted == true)
                            {

                                MessageBox.Show(TaskHive.Strings.UserIDRegistered + " " + user.id.ToString());
                     

                            }
                            else if (IsExisted == false)
                            {

                                int UserId = user.id;
                                string UserEmail = user.myemail ?? "";
                                cu.Insert(user.id.ToString(), UserEmail);
                           
                                counter++;

                            }


                        }


                    }
               
                }
            }


          

            if (counter == 0)
            { MessageBox.Show(TaskHive.Strings.Noitemselected); return; }
         

            MessageBox.Show(counter.ToString() + " " + TaskHive.Strings.itemsSuccessfully); this.DialogResult = true;

        }


        public static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T t)
                    return t;

                var result = FindVisualChild<T>(child);
                if (result != null)
                    return result;
            }
            return null;
        }

        private void LabelFilter_Click(object sender, RoutedEventArgs e)
        {

            try {

                var item = comboxsearch.SelectedItem as ComboBoxItem;

                string myfield = item?.Tag?.ToString() ?? "";

                string mytext = this.TexboxSearch.Text;

                ClassInternetSelectClient Ci = new ClassInternetSelectClient();
                Ci.Search_Clients_By(myfield, mytext);
                this.ClientsDataGrid.ItemsSource = Ci.users;

                this.checkboxall.IsChecked = false;
                IsHasDataCounter = false;

            } catch (Exception ex) { MessageBox.Show(ex.Message); }


        }

        private void checkboxall_Checked(object sender, RoutedEventArgs e)
        {

           

            foreach (var item in ClientsDataGrid.Items)
            {
                DataGridRow row = (DataGridRow)ClientsDataGrid.ItemContainerGenerator.ContainerFromItem(item);

                if (row != null)
                {
                    var presenter = FindVisualChild<DataGridCellsPresenter>(row);

                    var cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(0);

                    var chk = FindVisualChild<CheckBox>(cell);
                    chk.IsChecked = true;

                }
            }


        }

        private void checkboxall_Unchecked(object sender, RoutedEventArgs e)
        {

            foreach (var item in ClientsDataGrid.Items)
            {
                DataGridRow row = (DataGridRow)ClientsDataGrid.ItemContainerGenerator.ContainerFromItem(item);

                if (row != null)
                {
                    var presenter = FindVisualChild<DataGridCellsPresenter>(row);

                    var cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(0);

                    var chk = FindVisualChild<CheckBox>(cell);
                    chk.IsChecked = false;
                    

                }
            }

            IsHasDataCounter = false;

        }



    }

}
