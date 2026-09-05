using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace TaskHive.Classes
{
    class ClassSetting
    {



     

        public void Select_Setting_Application() 
        {


            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";


            var conn = new SqliteConnection(connectionstring);

            conn.Open();
            var cmd = new SqliteCommand("SELECT * from setting", conn);
            var reader = cmd.ExecuteReader();
            DataTable mydataset = new DataTable();
            mydataset.Load(reader);

           GlobalApp.CompanyName = mydataset.Rows[0][2].ToString() ?? "";
           GlobalApp.CompanyPasswordEmailAcount = mydataset.Rows[1][2].ToString() ?? "";
           GlobalApp.CompanyEmail = mydataset.Rows[2][2].ToString() ?? "";
           GlobalApp.CompanyAddress = mydataset.Rows[3][2].ToString() ?? "";
           GlobalApp.CompanyPhone = mydataset.Rows[4][2].ToString() ?? "";
            GlobalApp.CompanyWebsite = mydataset.Rows[5][2].ToString() ?? "";
            GlobalApp.WeatherAddress = mydataset.Rows[6][2].ToString() ?? "";

            conn.Close();
           

        }

    

        public void updatesetting(int id, string txt)
        {

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";
            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();
                string query = "Update setting set tagvalue=@tagvalue where id=@id";
                var cmd = new SqliteCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@tagvalue", txt);

                cmd.ExecuteNonQuery();
                conn.Close();


            }

        }





    }
}
