using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Windows.Controls;
using static testapps.Views.WinSetting;

namespace TaskHive.Classes
{

    public class User
    {
        public int IdUser { get; set; }
        public string? Phone { get; set; }
        public string? UName { get; set; }
        public string? Email { get; set; }
        public string? TelId { get; set; }
        public string? DeviceId { get; set; }
        public string? Si { get; set; }
        public string? Pas { get; set; }
    }

    class ClassUsers
    {
        ObservableCollection<User> users = new ObservableCollection<User>();
        public void Insert_User(int iduser,string phone, string uname, string email, string telid, string deviceid, string si, string pas)
        {

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";
            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();
                string query = $"INSERT INTO Users (iduser,phone, uname, email, telid, deviceid, si, pas) VALUES (@iduser,@phone, @uname, @email, @telid, @deviceid, @si, @pas);";
                var cmd = new SqliteCommand(query, conn);

                cmd.Parameters.AddWithValue("@iduser", iduser);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@uname", uname);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@telid", telid);
                cmd.Parameters.AddWithValue("@deviceid", deviceid);
                cmd.Parameters.AddWithValue("@si", si);
                cmd.Parameters.AddWithValue("@pas", pas);

                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }

        public void Update_User(int iduser, string phone, string uname, string email, string telid, string pas)
        {

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";
            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();
                string query = $"Update Users set phone=@phone,uname=@uname,email=@email,telid=@telid,pas=@pas where iduser=@iduser";
                var cmd = new SqliteCommand(query, conn);

                cmd.Parameters.AddWithValue("@iduser", iduser);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@uname", uname);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@telid", telid);
                cmd.Parameters.AddWithValue("@pas", pas);

                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }

        public void EnableSelectedUser(int iduser)
        {


            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";
            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();
                string query = "UPDATE Users SET si = 'False'; UPDATE Users SET si = 'True' WHERE iduser = @iduser;";
                var cmd = new SqliteCommand(query, conn);

                cmd.Parameters.AddWithValue("@iduser", iduser);

                cmd.ExecuteNonQuery();
                conn.Close();

            }
        }
        public void Delete_User(int iduser)
        {

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";
            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();
                string query = "Delete From Users  where iduser=@iduser";
                var cmd = new SqliteCommand(query, conn);

                cmd.Parameters.AddWithValue("@iduser", iduser);
    

                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }


        public ObservableCollection<User> Select_All_User()
        {
            ObservableCollection<User> users = new ObservableCollection<User>();

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";

            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();

                string query = "SELECT * FROM Users";
                var cmd = new SqliteCommand(query, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new User
                    {
                        IdUser = reader.GetInt32(0),
                        Phone = reader.GetString(1),
                        UName = reader.GetString(2),
                        Email = reader.GetString(3),
                        TelId = reader.GetString(4),
                        DeviceId = reader.GetString(5),
                        Si = reader.GetString(6),
                        Pas = reader.GetString(7)
                    });
                }
            }

            return users;
        }

        public enum LoginResult
        {
            UserNotFound,
            UserInactive,
            WrongPassword,
            Success
        }

        public LoginResult Login_To_Application(string username, string password)
        {
            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = Path.Combine(exePath, "DataBases", "OCServer.db");
            string connectionString = $"Data Source={dbPath}";

            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();

           
                using (var cmd = new SqliteCommand(
                    "SELECT pas, si FROM Users WHERE uname = @username", conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    using (var reader = cmd.ExecuteReader())
                    {
                       
                        if (!reader.Read())
                        {
                            return LoginResult.UserNotFound;
                        }

                      
                        string dbPass = reader["pas"].ToString() ?? "";
                        bool isActive = Convert.ToBoolean(reader["si"]);

                        if (!isActive)
                            return LoginResult.UserInactive;

                        if (dbPass != password)
                            return LoginResult.WrongPassword;

                        return LoginResult.Success;
                    }
                }
            }
        }
        /// <summary>
        /// st = 1 is for user name
        /// st = 2 is for user id
        /// </summary>
        /// <param  name="st"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool Select_User_byID_or_Name(int st, string val)
        {
            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = Path.Combine(exePath, "DataBases", "OCServer.db");
            string connectionString = $"Data Source={dbPath}";

            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();

                string query = "";
                var cmd = new SqliteCommand();
                cmd.Connection = conn;

                if (st == 1)
                {
                    query = "SELECT * FROM Users WHERE uname = @uname";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@uname", val);
                }
                else if (st == 2)
                {
                    query = "SELECT * FROM Users WHERE iduser = @iduser";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@iduser", Convert.ToInt64(val));
                }

                using (var reader = cmd.ExecuteReader())
                {
                    DataTable mydataset = new DataTable();
                    mydataset.Load(reader);

                    if (mydataset.Rows.Count > 0 && mydataset.Rows[0][0] != DBNull.Value)
                    {
                        GlobalApp.iduser = Convert.ToInt32(mydataset.Rows[0][0]);
                        GlobalApp.phone = mydataset.Rows[0][1]?.ToString() ?? "";
                        GlobalApp.uname = mydataset.Rows[0][2]?.ToString() ?? "";
                        GlobalApp.email = mydataset.Rows[0][3]?.ToString() ?? "";
                        GlobalApp.telid = mydataset.Rows[0][4]?.ToString() ?? "";
                        GlobalApp.deviceid = mydataset.Rows[0][5]?.ToString() ?? "";
                        GlobalApp.si = mydataset.Rows[0][6]?.ToString() ?? "";
                        GlobalApp.pas = mydataset.Rows[0][7]?.ToString() ?? "";

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public bool Check_Email_UserAccount(string email, string username)
        {
            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = Path.Combine(exePath, "DataBases", "OCServer.db");
            string connectionString = $"Data Source={dbPath}";

            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT 1 FROM Users WHERE uname = @username AND email = @email LIMIT 1";
                using (var cmd = new SqliteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@email", email);

                    object? result = cmd.ExecuteScalar();
                    return result != null;
                }
            }
        }


        public string Return_UserName(int iduser)
        {
            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = Path.Combine(exePath, "DataBases", "OCServer.db");
            string connectionString = $"Data Source={dbPath}";

            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT username FROM Users WHERE iduser = @iduser";

                using (var cmd = new SqliteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@iduser", iduser);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.IsDBNull(0) ? "" : reader.GetString(0);
                        }
                    }
                }
            }

            return "";
        }


    }
}
