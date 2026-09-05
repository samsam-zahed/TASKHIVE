using ControlzEx.Standard;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;

namespace TaskHive.Classes
{


    public class Companies
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = String.Empty;
        public string Website { get; set; } = string.Empty;
        public string ServiceType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
  


    }


    internal class ClassCompany
    {


        public void Insert_Company(string CompanyName, string Address, string Phone, string Email, string Website, string ServiceType, string Notes)
        {

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";
            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();
                string query = $"INSERT INTO Companies (CompanyName,Address, Phone, Email, Website, ServiceType, Status, Notes) VALUES (@CompanyName,@Address, @Phone, @Email, @Website, @ServiceType, @Status, @Notes);";
                var cmd = new SqliteCommand(query, conn);

                cmd.Parameters.AddWithValue("@CompanyName", CompanyName);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@Phone", Phone);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Website", Website);
                cmd.Parameters.AddWithValue("@ServiceType", ServiceType);
                cmd.Parameters.AddWithValue("@Status", "Active");
                cmd.Parameters.AddWithValue("@Notes", Notes);

                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }

        public void Update_Company(int Id,string CompanyName, string Address, string Phone, string Email, string Website, string ServiceType, string Notes,string Status)
        {

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";
            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();
                string query = $"Update Companies set CompanyName=@CompanyName,Address=@Address,Phone=@Phone,Email=@Email,Website=@Website,ServiceType=@ServiceType,Status=@Status,Notes=@Notes where Id=@Id";
                var cmd = new SqliteCommand(query, conn);

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@CompanyName", CompanyName);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@Phone", Phone);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Website", Website);
                cmd.Parameters.AddWithValue("@ServiceType", ServiceType);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@Notes", Notes);

                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }

        public void Delete_Company(int Id)
        {

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";
            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();
                string query = $"Delete from Companies where Id=@Id";
                var cmd = new SqliteCommand(query, conn);

                cmd.Parameters.AddWithValue("@Id", Id);
             

                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }

        public ObservableCollection<Companies> Select_All_Companies(string status)
        {

            ObservableCollection<Companies> Mycomanies = new ObservableCollection<Companies>();

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";

            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();

                string query = $"SELECT * FROM Companies where Status = @sit";

                var cmd = new SqliteCommand(query, conn);
                cmd.Parameters.AddWithValue("@sit", status);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Mycomanies.Add(new Companies
                    {
                        Id = reader.GetInt32(0),
                        CompanyName = reader.GetString(1),
                        Address = reader.GetString(2),
                        Phone = reader.GetString(3),
                        Email = reader.GetString(4),
                        Website = reader.GetString(5),
                        ServiceType = reader.GetString(6),
                        Status = reader.GetString(7),
                        Notes = reader.GetString(8),
                        CreatedAt = reader.GetString(9),
        

                    });
                }
            }

            return Mycomanies;
        }

        public ObservableCollection<Companies> Search_By_Columns(string colname,string value)
        {

            ObservableCollection<Companies> Mycomanies = new ObservableCollection<Companies>();

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";

            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();

                string query = $"SELECT * FROM Companies where {colname} like @value and Status='Active'";

                var cmd = new SqliteCommand(query, conn);
                cmd.Parameters.AddWithValue("@value", value + "%");
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Mycomanies.Add(new Companies
                    {
                        Id = reader.GetInt32(0),
                        CompanyName = reader.GetString(1),
                        Address = reader.GetString(2),
                        Phone = reader.GetString(3),
                        Email = reader.GetString(4),
                        Website = reader.GetString(5),
                        ServiceType = reader.GetString(6),
                        Status = reader.GetString(7),
                        CreatedAt = reader.GetString(8),


                    });
                }
            }

            return Mycomanies;
        }

    }
}
