using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Office;
using Microsoft.Data.Sqlite;
using SharpVectors.Scripting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;

namespace TaskHive.Classes
{

    public class TableJobs
    {
        public int id { get; set; }
        public int idday { get; set; } 
        public string customername { get; set; } = string.Empty;
        public string jobcaption { get; set; } = string.Empty;
        public string jobcomment { get; set; } = string.Empty;
        public string jobsit { get; set; } = string.Empty;
        public string jobduty { get; set; } = string.Empty;
        public string jobtime { get; set; } = string.Empty;
        public string jobenable { get; set; } = string.Empty;
        public int idcustomer { get; set; }

        public string iduser { get; set; } = string.Empty;



    }

    internal class ClassJobs
    {

        public ObservableCollection<TableJobs> Select_All_Jobs(string jobsit,string jobenable)
        {

            ObservableCollection<TableJobs> MyTableJobs = new ObservableCollection<TableJobs>();

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";

            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();

                string query = $"SELECT * FROM TableJob where jobsit = @jobsit and jobenable = @jobenable";

                var cmd = new SqliteCommand(query, conn);
                cmd.Parameters.AddWithValue("@jobsit", jobsit);
                cmd.Parameters.AddWithValue("@jobenable", jobenable);
                var reader = cmd.ExecuteReader();

            
                
                while (reader.Read())
                {
                    
                    MyTableJobs.Add(new TableJobs
                    {
                        id = reader.GetInt32(0),
                        idday = reader.GetInt32(1),
                        customername = reader.GetString(2),
                        jobcaption = reader.GetString(3),
                        jobcomment = reader.GetString(4),
                        jobsit = reader.GetString(5),
                        jobduty = reader.GetString(6),
                        jobtime = reader.GetString(7),
                        jobenable = reader.GetString(8),
                        idcustomer = reader.GetInt32(9),
                        iduser = reader.GetString(10),


                    });

                   
                }
            }

            return MyTableJobs;
        }


        public ObservableCollection<TableJobs> SelectJobTask_by_IDday(string _query)
        {

            ObservableCollection<TableJobs> MyTableJobs = new ObservableCollection<TableJobs>();

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";

            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();

                var cmd = new SqliteCommand($"{_query}", conn);

                var reader = cmd.ExecuteReader();



                while (reader.Read())
                {

                    MyTableJobs.Add(new TableJobs
                    {
                        id = reader.GetInt32(0),
                        idday = reader.GetInt32(1),
                        customername = reader.GetString(2),
                        jobcaption = reader.GetString(3),
                        jobcomment = reader.GetString(4),
                        jobsit = reader.GetString(5),
                        jobduty = reader.GetString(6),
                        jobtime = reader.GetString(7),
                        jobenable = reader.GetString(8),
                        idcustomer = reader.GetInt32(9),
                        iduser = reader.GetString(10),


                    });


                }
            }

            return MyTableJobs;
        }


        public void Insert_Task(string idday, string customername, string jobcaption, string jobcomment, string jobsit, string jobduty, string jobtime, string jobenable, string idcustomer, string username)
        {

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";

            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();


                string query = @"INSERT INTO TableJob 
                            (idday, customername, jobcaption, jobcomment, jobsit, jobduty, jobtime, jobenable, idcustomer,iduser) 
                             VALUES 
                            (@idday, @customername, @jobcaption, @jobcomment, @jobsit, @jobduty, @jobtime, @jobenable, @idcustomer,@iduser)";

                using (var cmd = new SqliteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idday", idday);
                    cmd.Parameters.AddWithValue("@customername", customername);
                    cmd.Parameters.AddWithValue("@jobcaption", jobcaption);
                    cmd.Parameters.AddWithValue("@jobcomment", jobcomment);
                    cmd.Parameters.AddWithValue("@jobsit", jobsit);
                    cmd.Parameters.AddWithValue("@jobduty", jobduty);
                    cmd.Parameters.AddWithValue("@jobtime", jobtime);
                    cmd.Parameters.AddWithValue("@jobenable", jobenable);
                    cmd.Parameters.AddWithValue("@idcustomer", idcustomer);
                    cmd.Parameters.AddWithValue("@iduser", username);

                    cmd.ExecuteNonQuery();
                }


                conn.Close();
            }


        }
        public void Update_Task(int id, string jobcaption, string jobcomment, string jobsit, string jobduty, string jobtime, string jobenable)
        {

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";

            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();


                string query = @"Update TableJob set
                              jobcaption=@jobcaption, jobcomment=@jobcaption, jobsit=@jobsit, jobduty=@jobduty, jobtime=@jobtime, jobenable=@jobenable where id = @id";

                using (var cmd = new SqliteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@jobcaption", jobcaption);
                    cmd.Parameters.AddWithValue("@jobcomment", jobcomment);
                    cmd.Parameters.AddWithValue("@jobsit", jobsit);
                    cmd.Parameters.AddWithValue("@jobduty", jobduty);
                    cmd.Parameters.AddWithValue("@jobtime", jobtime);
                    cmd.Parameters.AddWithValue("@jobenable", jobenable);
               
          

                    cmd.ExecuteNonQuery();
                }


                conn.Close();
            }


        }

        public void Delete_Task(int id)
        {

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";

            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();


                string query = @"Delete from TableJob where
                               id = @id";

                using (var cmd = new SqliteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
               
                    cmd.ExecuteNonQuery();
                }


                conn.Close();
            }


        }

        public List<object> SelectDuties(int f)
        {

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";

            var conn = new SqliteConnection(connectionstring);

            conn.Open();

            var cmd = new SqliteCommand("SELECT DISTINCT jobduty from TableJob", conn);

            List<object> list = new List<object>();
            using (var reader = cmd.ExecuteReader())
            {
                list.Clear();
                if (f == 0)
                {

                    list.Add("No Filter");
                    Separator sep = new Separator();
                    list.Add(sep);

                }
                else if (f == 1)
                {

                    list.Add("Unassigned");
                    Separator sep = new Separator();
                    list.Add(sep);

                }

                while (reader.Read())
                {
                    list.Add(reader.GetString(0));

                }

                return list;
            }

        }

      
        public async Task<bool> GetNewRecordedTaskListAsync()
        {
            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";

            using (var conn = new SqliteConnection(connectionstring))
            {
                await conn.OpenAsync();

       
                string query = @"SELECT Id FROM TableJob WHERE IsNotify = 0 LIMIT 1";

                var cmd = new SqliteCommand(query, conn);

                var result = await cmd.ExecuteScalarAsync();

                if (result != null)
                {
                    int id = Convert.ToInt32(result);

                 
                    await MarkAsNotified(id, conn);

                    return true;
                }

                return false;
            }
        }
        private async Task MarkAsNotified(int id, SqliteConnection conn)
        {
            string query = @"UPDATE TableJob SET IsNotify = 1 WHERE Id = @id";
            var cmd = new SqliteCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            await cmd.ExecuteNonQueryAsync();
        }

        public int Get_Cout_Notification_Received()
        {
            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = System.IO.Path.Combine(exePath, "DataBases", "OCServer.db");
            string connectionString = $"Data Source={dbPath}";

            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SqliteCommand(
                    "SELECT COUNT(Id) FROM TableJob WHERE jobsit = @jobsit AND jobenable = @jobenable", conn))
                {
                    cmd.Parameters.AddWithValue("@jobsit", TaskHive.Strings.NotStarted);
                    cmd.Parameters.AddWithValue("@jobenable", TaskHive.Strings.Active);

                    var result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
        }
        public void Change_jobsit(int id, string jobsit)
        {

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";

            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();


                string query = "Update TableJob set jobsit = @jobsit where id=@id";

                using (var cmd = new SqliteCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@jobsit", jobsit);
       

                    cmd.ExecuteNonQuery();
                }


                conn.Close();
            }


        }

    }
}
