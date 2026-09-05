using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Documents;

namespace TaskHive.Classes
{

    public class Mylist { 
    
        public int id { get; set; }
        public DateTime dateday { get; set; }
    
    }
    public static class ClassYear
    {
        public static string DateDay { get; set; } = string.Empty;
        public static int DateId { get; set; }

        public static bool AddDayToDatabase(string mydate)
        {

            try
            {

                string exepath = AppDomain.CurrentDomain.BaseDirectory;
                string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
                string connectionstring = $"Data Source={dbpath}";

                var conn = new SqliteConnection(connectionstring);
                conn.Open();
                var cmd = new SqliteCommand("INSERT INTO TableDays (dayname) VALUES (@dayname)", conn);

                cmd.Parameters.AddWithValue("@dayname", mydate);

                cmd.ExecuteNonQuery();
                conn.Close();
                return false;

            }
            catch
            {
                return true;
            }


        }


        public static bool Check_Date_Registered(string mydate)
        {
            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = System.IO.Path.Combine(exePath, "DataBases", "OCServer.db");
            string connectionString = $"Data Source={dbPath}";

            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SqliteCommand(
                    "SELECT 1 FROM TableDays WHERE dayname = @mydate LIMIT 1", conn))
                {
                    cmd.Parameters.AddWithValue("@mydate", mydate);

                    var result = cmd.ExecuteScalar();
                    return result != null;
                }
            }
        }


        public static bool Load_Date(string dateday)
        {
            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = System.IO.Path.Combine(exePath, "DataBases", "OCServer.db");
            string connectionString = $"Data Source={dbPath}";

            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SqliteCommand(
                    "SELECT id, dayname FROM TableDays WHERE dayname = @dateday", conn))
                {
                    cmd.Parameters.AddWithValue("@dateday", dateday);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DateId = reader.GetInt32(0);
                            DateDay = reader.GetString(1);
                            return true;   
                        }
                    }
                }
            }

            return false;
        }


        public static string Return_DateName(int id)
        {
            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = System.IO.Path.Combine(exePath, "DataBases", "OCServer.db");
            string connectionString = $"Data Source={dbPath}";

            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SqliteCommand(
                    "SELECT dayname FROM TableDays WHERE id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {


                        if (reader.Read())  
                        {
                            return reader.GetString(0);
                        }


                    }
                }
            }

            return null ?? "No";

        }

        public static int Return_DateId(string dayname)
        {
            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = System.IO.Path.Combine(exePath, "DataBases", "OCServer.db");
            string connectionString = $"Data Source={dbPath}";

            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SqliteCommand(
                    "SELECT id FROM TableDays WHERE dayname = @dayname", conn))
                {
                    cmd.Parameters.AddWithValue("@dayname", dayname);

                    using (var reader = cmd.ExecuteReader())
                    {


                        if (reader.Read())
                        {
                            return reader.GetInt32(0);
                        }


                    }
                }
            }

            return 0;

        }

    }
}
