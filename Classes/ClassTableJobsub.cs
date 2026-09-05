using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TaskHive.Classes
{
    public class TableJobssub
    {
        public int id { get; set; }
        public string image { get; set; } = string.Empty;
        public string age { get; set; } = string.Empty;
        public string nationality { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
        public string clientmail { get; set; } = string.Empty;
        public string clientaddress { get; set; } = string.Empty;
        public string companyaddress { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string companyemail { get; set; } = string.Empty;
        public string companyphone { get; set; } = string.Empty;
        public string companywebsite { get; set; } = string.Empty;
        public string ServiceType { get; set; } = string.Empty;
        public string dayname { get; set; } = string.Empty;
        public string customername { get; set; } = string.Empty;
        public string jobcaption { get; set; } = string.Empty;
        public string jobcomment { get; set; } = string.Empty;
        public string Notes1 { get; set; } = string.Empty;
        public string Notes2 { get; set; } = string.Empty;
        public int idsub { get; set; }
        public int idclient { get; set; }
        public int idjob { get; set; }








    }
    internal class ClassTableJobsub
    {

        public void Insert(int id,int idcustomer,int idjob,int idcompany,string notes, string sit)
        {

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";
            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();
                string query = $"INSERT INTO TableJobsub (id,idcustomer,idjob,idcompany,notes,sit) VALUES (@id,@idcustomer,@idjob,@idcompany,@notes,@sit);";
                var cmd = new SqliteCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@idcustomer", idcustomer);
                cmd.Parameters.AddWithValue("@idjob", idjob);
                cmd.Parameters.AddWithValue("@idcompany", idcompany);
                cmd.Parameters.AddWithValue("@notes", notes);
                cmd.Parameters.AddWithValue("@sit", sit);
        
                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }

        public ObservableCollection<TableJobssub> Select_All_Ongoing(string sit)
        {

            ObservableCollection<TableJobssub> MyTableJobssub = new ObservableCollection<TableJobssub>();

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";

            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();

                string query = $"SELECT clients.id as idclient,clients.image as img,clients.age as age,clients.nationality as nationality," +
                    $"clients.phone as phone,clients.myemail as clientmail,clients.address as clientaddress," +
                    $"Companies.CompanyName,Companies.Address as companyaddress,Companies.Email as companyemail,Companies.Phone as " +
                    $"companyphone,Companies.Website as companywebsite,Companies.ServiceType,Companies.Id as id,TableDays.dayname as " +
                    $"dayname,TableJob.customername as customername,TableJob.jobcaption as jobcaption,TableJob.jobcomment as " +
                    $"jobcomment,Companies.Notes Notes1,TableJobsub.notes AS Notes2,TableJob.id as idjob," +
                    $"TableJobsub.id as idsub FROM TableDays INNER JOIN TableJob ON TableDays.id = TableJob.idday INNER JOIN clients ON TableJob.idcustomer = clients.id " +
                    $"INNER JOIN TableJobsub ON TableJob.id = TableJobsub.idjob INNER JOIN Companies ON TableJobsub.idcompany = Companies.Id WHERE TableJobsub.sit = @sit ORDER by id DESC";
                var cmd = new SqliteCommand(query, conn);
                cmd.Parameters.AddWithValue("@sit", sit);
                var reader = cmd.ExecuteReader();



                while (reader.Read())
                {

                    MyTableJobssub.Add(new TableJobssub
                    {


                        idclient = reader.GetInt32(0),
                        image = reader.GetString(1),
                        age = reader.GetString(2),
                        nationality = reader.GetString(3),
                        phone = reader.GetString(4),
                        clientmail = reader.GetString(5),
                        clientaddress = reader.GetString(6),
                        CompanyName = reader.GetString(7),
                        companyaddress = reader.GetString(8),
                        companyemail = reader.GetString(9),
                        companyphone = reader.GetString(10),
                        companywebsite = reader.GetString(11),
                        ServiceType = reader.GetString(12),
                        id = reader.GetInt32(13),
                        dayname = reader.GetString(14),
                        customername = reader.GetString(15),
                        jobcaption = reader.GetString(16),
                        jobcomment = reader.GetString(17),               
                        Notes1 = reader.GetString(18),
                        Notes2 = reader.GetString(19),
                        idjob = reader.GetInt32(20),
                        idsub = reader.GetInt32(21),




                    });


                }
            }

            return MyTableJobssub;
        }

        public void Update(int id,  string notes,string sit)
        {

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";
            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();
                string query = $"Update TableJobsub set notes=@notes,sit=@sit where id=@id";
                var cmd = new SqliteCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@notes", notes);
                cmd.Parameters.AddWithValue("@sit", sit);

                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }


        public ObservableCollection<TableJobssub> Select_All_Status(string st)
        {

            ObservableCollection<TableJobssub> MyTableJobssub = new ObservableCollection<TableJobssub>();

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";

            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();

                string query = $"SELECT clients.id as idclient,clients.image as img,clients.age as age,clients.nationality as nationality," +
                    $"clients.phone as phone,clients.myemail as clientmail,clients.address as clientaddress," +
                    $"Companies.CompanyName,Companies.Address as companyaddress,Companies.Email as companyemail,Companies.Phone as " +
                    $"companyphone,Companies.Website as companywebsite,Companies.ServiceType,Companies.Id as id,TableDays.dayname as " +
                    $"dayname,TableJob.customername as customername,TableJob.jobcaption as jobcaption,TableJob.jobcomment as " +
                    $"jobcomment,Companies.Notes Notes1,TableJobsub.notes AS Notes2,TableJob.id as idjob," +
                    $"TableJobsub.id as idsub FROM TableDays INNER JOIN TableJob ON TableDays.id = TableJob.idday INNER JOIN clients ON TableJob.idcustomer = clients.id " +
                    $"INNER JOIN TableJobsub ON TableJob.id = TableJobsub.idjob INNER JOIN Companies ON TableJobsub.idcompany = Companies.Id WHERE TableJob.jobsit = @sit ORDER by id DESC";
                var cmd = new SqliteCommand(query, conn);
                cmd.Parameters.AddWithValue("@sit", st);
                var reader = cmd.ExecuteReader();



                while (reader.Read())
                {

                    MyTableJobssub.Add(new TableJobssub
                    {

                        idclient = reader.GetInt32(0),
                        image = reader.GetString(1),
                        age = reader.GetString(2),
                        nationality = reader.GetString(3),
                        phone = reader.GetString(4),
                        clientmail = reader.GetString(5),
                        clientaddress = reader.GetString(6),
                        CompanyName = reader.GetString(7),
                        companyaddress = reader.GetString(8),
                        companyemail = reader.GetString(9),
                        companyphone = reader.GetString(10),
                        companywebsite = reader.GetString(11),
                        ServiceType = reader.GetString(12),
                        id = reader.GetInt32(13),
                        dayname = reader.GetString(14),
                        customername = reader.GetString(15),
                        jobcaption = reader.GetString(16),
                        jobcomment = reader.GetString(17),
                        Notes1 = reader.GetString(18),
                        Notes2 = reader.GetString(19),
                        idjob = reader.GetInt32(20),
                        idsub = reader.GetInt32(21),

                    });


                }
            }

            return MyTableJobssub;
        }

     
            


        public ObservableCollection<TableJobssub> Search_by(int index,string st)
        {

            ObservableCollection<TableJobssub> MyTableJobssub = new ObservableCollection<TableJobssub>();

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";


                string Col_1 = "TableJob.jobcaption";
                string Col_2 = "Companies.CompanyName";
                string Col_3 = "TableJob.idcustomer";
                string Col_4 = "TableJob.customername";
                string Col = "";

            if (index == 0) { Col = Col_1; }
            if (index == 1) { Col = Col_2; }
            if (index == 2) { Col = Col_3; }
            if (index == 3) { Col = Col_4; }

            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();

                string query = $"SELECT clients.id as idclient,clients.image as img,clients.age as age,clients.nationality as nationality," +
                    $"clients.phone as phone,clients.myemail as clientmail,clients.address as clientaddress," +
                    $"Companies.CompanyName,Companies.Address as companyaddress,Companies.Email as companyemail,Companies.Phone as " +
                    $"companyphone,Companies.Website as companywebsite,Companies.ServiceType,Companies.Id as id,TableDays.dayname as " +
                    $"dayname,TableJob.customername as customername,TableJob.jobcaption as jobcaption,TableJob.jobcomment as " +
                    $"jobcomment,Companies.Notes Notes1,TableJobsub.notes AS Notes2,TableJob.id as idjob," +
                    $"TableJobsub.id as idsub FROM TableDays INNER JOIN TableJob ON TableDays.id = TableJob.idday INNER JOIN clients ON TableJob.idcustomer = clients.id " +
                    $"INNER JOIN TableJobsub ON TableJob.id = TableJobsub.idjob INNER JOIN Companies ON TableJobsub.idcompany = Companies.Id WHERE {Col} like @jobsit ORDER by id DESC";
               
                var cmd = new SqliteCommand(query, conn);
                cmd.Parameters.AddWithValue("@jobsit", st + "%");
                var reader = cmd.ExecuteReader();



                while (reader.Read())
                {

                    MyTableJobssub.Add(new TableJobssub
                    {

                        idclient = reader.GetInt32(0),
                        image = reader.GetString(1),
                        age = reader.GetString(2),
                        nationality = reader.GetString(3),
                        phone = reader.GetString(4),
                        clientmail = reader.GetString(5),
                        clientaddress = reader.GetString(6),
                        CompanyName = reader.GetString(7),
                        companyaddress = reader.GetString(8),
                        companyemail = reader.GetString(9),
                        companyphone = reader.GetString(10),
                        companywebsite = reader.GetString(11),
                        ServiceType = reader.GetString(12),
                        id = reader.GetInt32(13),
                        dayname = reader.GetString(14),
                        customername = reader.GetString(15),
                        jobcaption = reader.GetString(16),
                        jobcomment = reader.GetString(17),
                        Notes1 = reader.GetString(18),
                        Notes2 = reader.GetString(19),
                        idjob = reader.GetInt32(20),
                        idsub = reader.GetInt32(21),

                    });


                }
            }

            return MyTableJobssub;
        }

    }
}
