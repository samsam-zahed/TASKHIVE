using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Windows.Controls;
using System.Xml.Linq;

namespace TaskHive.Classes
{

    public class Client
    {
        public int id { get; set; }
        public string fname { get; set; } = string.Empty;
        public string lname { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
        public int age { get; set; }
        public string nationality { get; set; } = string.Empty;
        public string idtelegram { get; set; } = string.Empty;
        public string personeltype { get; set; } = string.Empty; 
        public string myemail { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
        public string image { get; set; } = string.Empty;
        public string sit { get; set; } = string.Empty;
        public string des { get; set; } = string.Empty;
        public string fullname { get; set; } = string.Empty;

        public string IsSelected { get; set; } = string.Empty;
    }


    internal class ClassClient
    {

        public bool Check_Client_Exsist(int id)
        {

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";

            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();

                string query = $"SELECT COUNT(1) FROM clients where id = @id";

                var cmd = new SqliteCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                object? result = cmd.ExecuteScalar();
                long count = result != null ? Convert.ToInt64(result) : 0;



                return count > 0;
               
            }

            


        }

        public ObservableCollection<Client> Select_All_Clients(string Situation)
        {

            ObservableCollection<Client> MyClients = new ObservableCollection<Client>();

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";

            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();

                string query = $"SELECT * FROM clients where sit = @sit";
               
                var cmd = new SqliteCommand(query, conn);
                cmd.Parameters.AddWithValue("@sit", Situation);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    MyClients.Add(new Client
                    {
                        id = reader.GetInt32(0),
                        fname = reader.GetString(1),
                        lname = reader.GetString(2),
                        phone = reader.GetString(3),
                        address = reader.GetString(4),
                        myemail = reader.GetString(5),
                        age = reader.GetInt32(6),
                        idtelegram = reader.GetString(7),
                        personeltype = reader.GetString(8),
                        nationality = reader.GetString(9),
                        image = reader.GetString(10),
                        sit = reader.GetString(11),
                        des = reader.GetString(12),
                        fullname = reader.GetString(1) + " " + reader.GetString(2)

                    });
                }
            }

            return MyClients;
        }

        public ObservableCollection<Client> Select_One_Clients(int id,string Situation)
        {

            ObservableCollection<Client> MyClients = new ObservableCollection<Client>();

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";

            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();

                string query = "SELECT * FROM clients where id=@id and sit = @sit";

                var cmd = new SqliteCommand(query, conn);
                cmd.Parameters.AddWithValue("@sit", Situation);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    MyClients.Add(new Client
                    {
                        id = reader.GetInt32(0),
                        fname = reader.GetString(1),
                        lname = reader.GetString(2),
                        phone = reader.GetString(3),
                        address = reader.GetString(4),
                        myemail = reader.GetString(5),
                        age = reader.GetInt32(6),
                        idtelegram = reader.GetString(7),
                        personeltype = reader.GetString(8),
                        nationality = reader.GetString(9),
                        image = reader.GetString(10),
                        sit = reader.GetString(11),
                        des = reader.GetString(12),
                        fullname = reader.GetString(1) + " " + reader.GetString(2)

                    });
                }
            }

            return MyClients;
        }

        public ObservableCollection<Client> Search_Clients_By(string colname,string val)
        {

            ObservableCollection<Client> MyClients = new ObservableCollection<Client>();

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
                    MyClients.Add(new Client
                    {
                        id = reader.GetInt32(0),
                        fname = reader.GetString(1),
                        lname = reader.GetString(2),
                        phone = reader.GetString(3),
                        address = reader.GetString(4),
                        myemail = reader.GetString(5),
                        age = reader.GetInt32(6),
                        idtelegram = reader.GetString(7),
                        personeltype = reader.GetString(8),
                        nationality = reader.GetString(9),
                        image = reader.GetString(10),
                        sit = reader.GetString(11),
                        des = reader.GetString(12),
                        fullname = reader.GetString(1) + " " + reader.GetString(2)

                    });
                }
            }

            return MyClients;
        }

        public void Insert_Client(int id, string fname, string lname, string phone, string address, string myemail, string age, string idtelegram, string personeltype, string nationality, string image, string sit, string des)
        {

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";
            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();
                string query = $"INSERT INTO clients (id,fname,lname,phone,address,myemail,age,idtelegram,personeltype,nationality,image,sit,des) VALUES (@id,@fname,@lname,@phone,@address,@myemail,@age,@idtelegram,@personeltype,@nationality,@image,@sit,@des);";
                var cmd = new SqliteCommand(query, conn);



                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@lname", lname);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@myemail", myemail);
                cmd.Parameters.AddWithValue("@age", age);
                cmd.Parameters.AddWithValue("@idtelegram", idtelegram);
                cmd.Parameters.AddWithValue("@personeltype", personeltype);
                cmd.Parameters.AddWithValue("@nationality", nationality);
                cmd.Parameters.AddWithValue("@image", image);
                cmd.Parameters.AddWithValue("@sit", sit);
                cmd.Parameters.AddWithValue("@des", des);


                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }

        public void Update_Client(int id, string fname, string lname, string phone, string address, string myemail, string age, string idtelegram, string personeltype, string nationality, string image, string sit, string des)
        {

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";
            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();
                string query = $"Update clients set fname=@fname,lname=@lname,phone=@phone,address=@address,myemail=@myemail,age=@age,idtelegram=@idtelegram,personeltype=@personeltype,nationality=@nationality,image=@image,sit=@sit,des=@des where id = @id;";
                var cmd = new SqliteCommand(query, conn);



                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@lname", lname);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@myemail", myemail);
                cmd.Parameters.AddWithValue("@age", age);
                cmd.Parameters.AddWithValue("@idtelegram", idtelegram);
                cmd.Parameters.AddWithValue("@personeltype", personeltype);
                cmd.Parameters.AddWithValue("@nationality", nationality);
                cmd.Parameters.AddWithValue("@image", image);
                cmd.Parameters.AddWithValue("@sit", sit);
                cmd.Parameters.AddWithValue("@des", des);


                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }
        public void Delete_Client(int id)
        {

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";
            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();
                string query = $"Update clients set sit=@sit where id = @id;";
                var cmd = new SqliteCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@sit", "del");

                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }

    }
}
