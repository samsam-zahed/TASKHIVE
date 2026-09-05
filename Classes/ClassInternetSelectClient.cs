using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;

namespace TaskHive.Classes
{
    public class Internetclient 
    {

        public int id { get; set; }
        public string? fname { get; set; }
        public string? lname { get; set; }
        public string? myemail { get; set; }
        public string? phone { get; set; }
        public string? address { get; set; }
        public string? age { get; set; }
        public string? idtelegram { get; set; }
        public string? nationality { get; set; }
        public string? personeltype { get; set; }

       



    }
    internal class ClassInternetSelectClient 
    {

   
     public ObservableCollection<Internetclient> users = new ObservableCollection<Internetclient>();

        public void Select_All_Client(DataGrid myListView)

        {

  


            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";


            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();
                using (var cmd = new SqliteCommand($"SELECT * from clients where sit='True'", conn))

                using (var reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        users.Add(new Internetclient
                        {
                            id = reader.GetInt32(0),
                            fname = reader.GetString(1),
                            lname = reader.GetString(2),
                            phone = reader.GetString(3),
                            address = reader.GetString(4),
                            myemail = reader.GetString(5),
                            age = reader.GetString(6),
                            idtelegram = !reader.IsDBNull(7) ? reader.GetString(7) : "No User Id",
                            personeltype = reader.GetString(8),
                            nationality = reader.GetString(9),

                        });

                    }

                }


                myListView.ItemsSource = users;

                conn.Close();
            }


        }


        public ObservableCollection<Internetclient> Search_Clients_By(string colname, string val)
        {



            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";

            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();

                string query = $"SELECT * FROM clients where {colname} like @sit and sit = 'True'";

                var cmd = new SqliteCommand(query, conn);
                cmd.Parameters.AddWithValue("@sit", $"{val}%");
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new Internetclient
                    {

                        id = reader.GetInt32(0),
                        fname = reader.GetString(1),
                        lname = reader.GetString(2),
                        phone = reader.GetString(3),
                        address = reader.GetString(4),
                        myemail = reader.GetString(5),
                        age = reader.GetString(6),
                        idtelegram = !reader.IsDBNull(7) ? reader.GetString(7) : "No User Id",
                        personeltype = reader.GetString(8),
                        nationality = reader.GetString(9),

                    });
                }
            }

            return users;
        }



    }
}
