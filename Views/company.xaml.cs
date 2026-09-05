using MahApps.Metro.Controls;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
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
using TaskHive.Classes;

namespace testapps.Views
{
    /// <summary>
    /// Interaction logic for WindowSetting.xaml
    /// </summary>
    /// 
  
    public partial class Company : MetroWindow
    {

       public int con = 0;    
       public int idcompny = 0;
        public Company()
        {
            InitializeComponent();
        }

        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            try {




                if (this.txtcopanyname.Text == "" || string.IsNullOrWhiteSpace(this.txtcopanyname.Text)) { this.txtcopanyname.Focus(); return; }
                if (this.txtcopanyaddress.Text == "" || string.IsNullOrWhiteSpace(this.txtcopanyaddress.Text)) { this.txtcopanyaddress.Text = "No Address"; }
                if (this.txtcopanyaPhone.Text == "" || string.IsNullOrWhiteSpace(this.txtcopanyaPhone.Text)) { this.txtcopanyaPhone.Focus(); return; }
                if (this.txtcopanyaEmail.Text == "" || string.IsNullOrWhiteSpace(this.txtcopanyaEmail.Text)) { this.txtcopanyaEmail.Text="No Email"; }
                if (this.txtcopanyServiceType.Text == "" || string.IsNullOrWhiteSpace(this.txtcopanyServiceType.Text)) { this.txtcopanyServiceType.Text="No Service Type"; }

                if (con == 1) {

                    ClassCompany cp = new ClassCompany();
                    cp.Insert_Company(this.txtcopanyname.Text, this.txtcopanyaddress.Text, this.txtcopanyaPhone.Text
                        , this.txtcopanyaEmail.Text, this.txtcopanyaWebsite.Text, this.txtcopanyServiceType.Text, this.txtcopanynotes.Text);
                    DialogResult=true;

                }


                if (con == 2)
                {
                
                    ClassCompany cp = new ClassCompany();
                    string sta = "Active";
                    if (this.checkbox1.IsChecked == true) { sta = "Active"; } else { sta = "InActive"; }
                    cp.Update_Company(idcompny,this.txtcopanyname.Text, this.txtcopanyaddress.Text, this.txtcopanyaPhone.Text,this.txtcopanyaEmail.Text, this.txtcopanyaWebsite.Text, this.txtcopanyServiceType.Text, this.txtcopanynotes.Text, sta);
                    DialogResult = true;

                }

                if (con == 3)
                {
                    MessageBoxResult msg = MessageBox.Show(TaskHive.Strings.MsgDelete,"",MessageBoxButton.YesNo);
                    if (msg == MessageBoxResult.Yes)
                    {

                        ClassCompany cp = new ClassCompany();
                        cp.Delete_Company(idcompny);
                        DialogResult = true;

                    }
                    else { 
                    }
                 

                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void txtcopanyaPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+"); 
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
