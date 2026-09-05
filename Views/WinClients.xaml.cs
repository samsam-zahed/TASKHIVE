using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Security.Policy;
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
using System.Xml.Linq;
using TaskHive.Classes;

namespace testapps.Views
{
    /// <summary>
    /// Interaction logic for WindowSetting.xaml
    /// </summary>
    public partial class WinClients : MetroWindow
    {

        public int con = 1;

    
        public WinClients()
        {
            InitializeComponent();

         

            this.LabelClientIDNot.Content = TaskHive.Strings.notclientid;
            this.LabelClientnotphone.Content = TaskHive.Strings.notclientphone;
            this.LabelClientnottelegram.Content = TaskHive.Strings.notidtelegram;
            this.LabelClientnotsex.Content = TaskHive.Strings.notgender;
            this.LabelClientnotsit.Content = TaskHive.Strings.notsit;

            this.LabelClientID.Content = TaskHive.Strings.ID;
           this.LabelClientfname.Content = TaskHive.Strings.fname;
            this.LabelClientlname.Content = TaskHive.Strings.lname;
            this.LabelClientphone.Content = TaskHive.Strings.Phone;
            this.LabelClientaddress.Content = TaskHive.Strings.address;
            this.LabelClientemail.Content = TaskHive.Strings.Email;
            this.LabelClientage.Content = TaskHive.Strings.age;
            this.LabelClienttelegram.Content = TaskHive.Strings.TelegramID;
            this.LabelClienttesex.Content = TaskHive.Strings.gender;
            this.LabelClienttenationality.Content = TaskHive.Strings.nationality;
            this.LabelClientteimage.Content = TaskHive.Strings.image;
            this.LabelClientteDes.Content = TaskHive.Strings.description;
            this.checkbox1.Content = TaskHive.Strings.nocloseaftersave;

            this.RadioFalse.Content = TaskHive.Strings.False;
            this.RadioFemale.Content = TaskHive.Strings.Female;
            this.RadioMale.Content = TaskHive.Strings.Male;
            this.RadioOther.Content = TaskHive.Strings.Other;
            this.RadioTrue.Content = TaskHive.Strings.True;
            this.Radiop.Content = TaskHive.Strings.Prefernottosay;




        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            try {

                if (con==1) {

                    if (string.IsNullOrWhiteSpace(this.TextboxID.Text)) { this.TextboxID.BorderBrush = Brushes.Red; return; }
                    else { this.TextboxID.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF444444")); ; }

                    ClassClient cl = new ClassClient();

                    int ID;
                    ID = Convert.ToInt32(this.TextboxID.Text);

                    string fname = string.IsNullOrWhiteSpace(Textboxfname.Text) ? "---" : Textboxfname.Text;
                    string lname = string.IsNullOrWhiteSpace(Textboxlname.Text) ? "---" : Textboxlname.Text;
                    string phone = string.IsNullOrWhiteSpace(Textboxphone.Text) ? "358---" : Textboxphone.Text;
                    string address = string.IsNullOrWhiteSpace(Textboxaddress.Text) ? "---" : Textboxaddress.Text;
                    string telegram = string.IsNullOrWhiteSpace(Textboxetelegram.Text) ? "@No" : Textboxetelegram.Text;
                    string email = string.IsNullOrWhiteSpace(Textboxemail.Text) ? "example@yahoo.com" : Textboxemail.Text;
                    string age = string.IsNullOrWhiteSpace(Textboxeage.Text) ? "0" : Textboxeage.Text;
                    string natoinal = string.IsNullOrWhiteSpace(Textboxenationality.Text) ? "No" : Textboxenationality.Text;
                    string img = string.IsNullOrWhiteSpace(Textboxeimage.Text) ? "/user.png" : Textboxeimage.Text;
                    string des = string.IsNullOrWhiteSpace(TextboxeDes.Text) ? "No comment" : TextboxeDes.Text;
           
                    string sit = "";
                    if (RadioFalse.IsChecked == true) { sit = "False" ?? ""; }
                    if (RadioTrue.IsChecked == true) { sit = "True" ?? ""; }
                    string gender = "";
                    if (RadioMale.IsChecked == true) { gender = RadioMale.Content.ToString() ?? ""; }
                    if (RadioFemale.IsChecked == true) { gender = RadioFemale.Content.ToString() ?? ""; }
                    if (RadioOther.IsChecked == true) { gender = RadioOther.Content.ToString() ?? ""; }
                    if (Radiop.IsChecked == true) { gender = Radiop.Content.ToString() ?? ""; }

                    cl.Insert_Client(ID, fname, lname, phone, address, email, age, telegram, gender,
                     natoinal, img, sit, des);

                    ClearAllTextBoxes(grid);

                    if (this.checkbox1.IsChecked == true) { } else { DialogResult = true; }



                }

                //------------------------------------------------------

                if (con == 2) {

                    int ID;
                    ID = Convert.ToInt32(this.TextboxID.Text);

                    string fname = string.IsNullOrWhiteSpace(Textboxfname.Text) ? "---" : Textboxfname.Text;
                    string lname = string.IsNullOrWhiteSpace(Textboxlname.Text) ? "---" : Textboxlname.Text;
                    string phone = string.IsNullOrWhiteSpace(Textboxphone.Text) ? "358---" : Textboxphone.Text;
                    string address = string.IsNullOrWhiteSpace(Textboxaddress.Text) ? "---" : Textboxaddress.Text;
                    string telegram = string.IsNullOrWhiteSpace(Textboxetelegram.Text) ? "@No" : Textboxetelegram.Text;
                    string email = string.IsNullOrWhiteSpace(Textboxemail.Text) ? "example@yahoo.com" : Textboxemail.Text;
                    string age = string.IsNullOrWhiteSpace(Textboxeage.Text) ? "0" : Textboxeage.Text;
                    string natoinal = string.IsNullOrWhiteSpace(Textboxenationality.Text) ? "No" : Textboxenationality.Text;
                    string img = string.IsNullOrWhiteSpace(Textboxeimage.Text) ? "/user.png" : Textboxeimage.Text;
                    string des = string.IsNullOrWhiteSpace(TextboxeDes.Text) ? "No comment" : TextboxeDes.Text;

                    string sit = "";
                    if (RadioFalse.IsChecked == true) { sit = "False" ?? ""; }
                    if (RadioTrue.IsChecked == true) { sit = "True" ?? ""; }
                    string gender = "";
                    if (RadioMale.IsChecked == true) { gender = RadioMale.Content.ToString() ?? ""; }
                    if (RadioFemale.IsChecked == true) { gender = RadioFemale.Content.ToString() ?? ""; }
                    if (RadioOther.IsChecked == true) { gender = RadioOther.Content.ToString() ?? ""; }
                    if (Radiop.IsChecked == true) { gender = Radiop.Content.ToString() ?? ""; }

                    ClassClient cl = new ClassClient();
                    cl.Update_Client(ID, fname, lname, phone, address, email, age, telegram, gender,
                  natoinal, img, sit, des);

                    DialogResult = true;
                }

                //------------------------------------------------------

                if (con == 3)
                {

                    MessageBoxResult msg = MessageBox.Show(TaskHive.Strings.MsgDelete, "", MessageBoxButton.YesNo);
                    if (msg == MessageBoxResult.Yes) {

                        int ID;
                        ID = Convert.ToInt32(this.TextboxID.Text);
                        ClassClient cl = new ClassClient();
                        cl.Delete_Client(ID);
                        DialogResult = true;


                    }
                }

            }
            catch (Exception ex) {

                MessageBox.Show(TaskHive.Strings.ErrorClientIdRegistered);
            
            }


      



        }

        private void ClearAllTextBoxes(DependencyObject parent)
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is TextBox textBox)
                {
                  
                    if (textBox == TextboxID)
                    {
                        if (int.TryParse(textBox.Text, out int value))
                            textBox.Text = (value + 1).ToString();
                        else
                            textBox.Text = "1";
                    }
                    else
                    {
                     
                        textBox.Text = "";
                    }
                }
                else
                {
                    ClearAllTextBoxes(child); 
                }
            }
        }

        private void btnimage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Title = "Select Picture";
            dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
               this.Textboxeimage.Text = dlg.FileName;   
                                                  
            }
        }

        private void TextboxID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^\d+$");
        }

        private void Textboxphone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[0-9,]+$");
        }

        private void PhoneTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                string? text = e.DataObject.GetData(DataFormats.Text) as string;
                if (!Regex.IsMatch(text, @"^[0-9,]+$"))
                    e.CancelCommand();
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void Textboxetelegram_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[a-zA-Z0-9]+$");
        }

        private void Textboxeage_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^\d+$");
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
