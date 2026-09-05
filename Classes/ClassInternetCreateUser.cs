using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security;
using System.Text;

namespace TaskHive.Classes
{

    public class InterNetUsers
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string SecurityStamp { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTimeOffset? LockoutEnd { get; set; }



    }

    internal class ClassInternetCreateUser
    {

        public ObservableCollection<InterNetUsers> Select_All_InternetUsers()
        {

            ObservableCollection<InterNetUsers> MyInternettUsers = new ObservableCollection<InterNetUsers>();

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";

            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();

                string query = $"SELECT Id,UserName,PasswordHash,SecurityStamp,Email,LockoutEnd FROM AspNetUsers";

                var cmd = new SqliteCommand(query, conn);
          
                
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    MyInternettUsers.Add(new InterNetUsers
                    {

                        Id = reader["Id"]?.ToString() ?? string.Empty,
                        UserName = reader["UserName"]?.ToString() ?? string.Empty,
                        PasswordHash = reader["PasswordHash"]?.ToString() ?? string.Empty,
                        SecurityStamp = reader["SecurityStamp"]?.ToString() ?? string.Empty,
                        Email = reader["Email"]?.ToString() ?? string.Empty,
                        LockoutEnd = reader["LockoutEnd"] == DBNull.Value ? (DateTimeOffset?)null : (DateTimeOffset)reader["LockoutEnd"]

                    });
                }
            }

            return MyInternettUsers;
        }

        public void Insert(string userName, string email)
        {
            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";

            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();

                string query = @"INSERT INTO AspNetUsers 
        (Id, UserName, NormalizedUserName, Email, NormalizedEmail, SecurityStamp) 
        VALUES 
        (@Id, @UserName, @NormalizedUserName, @Email, @NormalizedEmail, @SecurityStamp);";

                var cmd = new SqliteCommand(query, conn);

                string id = Guid.NewGuid().ToString();
         

                cmd.Parameters.AddWithValue("@Id", Guid.NewGuid().ToString());
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@NormalizedUserName", userName.ToUpper());
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@NormalizedEmail", email.ToUpper());
                cmd.Parameters.AddWithValue("@SecurityStamp", Guid.NewGuid().ToString());

                cmd.ExecuteNonQuery();
            }
        }

        public void Update(string UserName, DateTime? LockoutEnd)
        {

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";
            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();
                string query = $"Update AspNetUsers set LockoutEnd=@LockoutEnd where UserName = @UserName";
                var cmd = new SqliteCommand(query, conn);


                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@LockoutEnd", LockoutEnd);


                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }
        public void Deletet(string id)
        {

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";
            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();
                string query = $"Delete from AspNetUsers where Id = @id;";
                var cmd = new SqliteCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }


        public bool Check_User_IsExisted(string UserName)
        {
            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = System.IO.Path.Combine(exePath, "DataBases", "OCServer.db");
            string connectionString = $"Data Source={dbPath}";

            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SqliteCommand(
                    "SELECT 1 FROM AspNetUsers WHERE UserName = @UserName LIMIT 1", conn))
                {
                    cmd.Parameters.AddWithValue("@UserName", UserName);

                    var result = cmd.ExecuteScalar();
                    return result != null;
                }
            }
        }

    }
}
