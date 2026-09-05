using DocumentFormat.OpenXml.Office.Word;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace TaskHive.Classes
{
    internal class s_client
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

        public bool IsSelected { get; set; }

    }

    internal class ClassClientSearch
    {

        public void Searchby(string type1, string txt, DataGrid myListView)

        {

            List<s_client> users = new List<s_client>();


            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";


            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();
                using (var cmd = new SqliteCommand($"SELECT * from clients where {type1} like '{txt}%' and sit='True'", conn)) 
               
                using (var reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        users.Add(new s_client
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

        public s_client? U;
        public bool SearchById(int id)
        {

            U = new s_client();    

            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = System.IO.Path.Combine(exePath, "DataBases", "OCServer.db");
            string connectionString = $"Data Source={dbPath}";

            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SqliteCommand(
                    "SELECT * FROM clients WHERE id = @id AND sit = 'True'", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                            return false;


                        U.id  = reader.GetInt32(0);
                        U.fname = reader.GetString(1);
                        U.lname = reader.GetString(2);
                        U.phone = reader.GetString(3);
                        U.address = reader.GetString(4);
                        U.myemail = reader.GetString(5);
                        U.age = reader.GetString(6);
                        U.idtelegram = !reader.IsDBNull(7) ? reader.GetString(7) : "No User Id";
                        U.personeltype = reader.GetString(8);
                        U.nationality = reader.GetString(9);


                        return true;
                    }
                }
            }
        }

        public void Select_All_Client(DataGrid myListView)

        {

            List<s_client> users = new List<s_client>();


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
                        users.Add(new s_client
                        {
                            id = reader.GetInt32(0),
                            fname = reader.GetString(1),
                            lname = reader.GetString(2),
                            phone  = reader.GetString(3),
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


    }
}
