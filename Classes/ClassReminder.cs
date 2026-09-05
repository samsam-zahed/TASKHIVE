using DocumentFormat.OpenXml.Office.Word;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TaskHive.Classes
{

    public class Reminder
    {
        public int ID { get; set; }
        public DateTime? ReminderDateTime { get; set; }
        public string? Description { get; set; }
        public string? IsActive { get; set; }
   
    }
    internal class ClassReminder
    {

        public ObservableCollection<Reminder> Select_All_Reminder()
        {
            ObservableCollection<Reminder> RM = new ObservableCollection<Reminder>();

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";

            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();

                string query = "SELECT * FROM Reminder ORDER BY ID DESC";
                var cmd = new SqliteCommand(query, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    RM.Add(new Reminder
                    {

                        ID = reader.GetInt32(0),
                        ReminderDateTime = reader.GetDateTime(1),
                        Description = reader.GetString(2),
                        IsActive = reader.GetString(3),
             
                    });
                }
            }

            return RM;
        }

        public void Insert_Reminder(DateTime ReminderDateTime,string Description,string IsActive)
        {

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";
            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();
                string query = $"INSERT INTO Reminder (ReminderDateTime, Description, IsActive) VALUES (@ReminderDateTime, @Description, @IsActive);";
                var cmd = new SqliteCommand(query, conn);

                cmd.Parameters.AddWithValue("@ReminderDateTime", ReminderDateTime);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@IsActive", IsActive);


                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }

        public void Update_Reminder(int ID,DateTime ReminderDateTime, string Description, string IsActive)
        {

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";
            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();
                string query = $"Update Reminder set ReminderDateTime=@ReminderDateTime ,Description=@Description,IsActive=@IsActive where ID=@ID";
                var cmd = new SqliteCommand(query, conn);

                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@ReminderDateTime", ReminderDateTime);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@IsActive", IsActive);


                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }

        public void AvtiveAndInactive_Reminder(int ID,string IsActive)
        {


            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";
            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();
                string query = "UPDATE Reminder SET IsActive = @IsActive WHERE ID = @ID;";
                var cmd = new SqliteCommand(query, conn);

                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@IsActive", IsActive);

                cmd.ExecuteNonQuery();
                conn.Close();

            }
        }
        public void Delete_Reminder(int ID)
        {

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";
            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();
                string query = "Delete From Reminder where ID=@ID";
                var cmd = new SqliteCommand(query, conn);

                cmd.Parameters.AddWithValue("@ID", ID);

                cmd.ExecuteNonQuery();
                conn.Close();

            }

        }

        public Reminder? GetReminder()
        {
            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = System.IO.Path.Combine(exePath, "DataBases", "OCServer.db");
            string connectionString = $"Data Source={dbPath}";

            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT ID, ReminderDateTime, Description FROM Reminder WHERE ReminderDateTime <= @now AND IsActive = 'True' ORDER BY ReminderDateTime ASC LIMIT 1";

                using (var cmd = new SqliteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@now", now);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Reminder
                            {
                                ID = reader.GetInt32(0),
                                ReminderDateTime = reader.GetDateTime(1),
                                Description = reader.GetString(2)
                            };
                        }
                    }
                }
            }

            return null;
        }

    }
}
