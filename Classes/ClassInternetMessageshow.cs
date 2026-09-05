using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TaskHive.Classes
{

    public class InMessageUser
    {
        public int Id { get; set; }
        public int Iduser { get; set; } 
        public string dataRegister { get; set; } = string.Empty;
        public string describ { get; set; } = string.Empty;
        public string ConditionMessage { get; set; } = String.Empty;
        public string UserConfirm { get; set; } = string.Empty;


    }

    internal class ClassInternetMessageshow
    {

        public ObservableCollection<InMessageUser> Select_All_Messages(int IdUser)
        {

            ObservableCollection<InMessageUser> MyUserMessagesInternet = new ObservableCollection<InMessageUser>();

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";

            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();

                string query = $"SELECT * FROM UaserInform WHERE Iduser = @IdUser ORDER BY Id DESC";

                var cmd = new SqliteCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdUser", IdUser);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    MyUserMessagesInternet.Add(new InMessageUser
                    {
                        Id = reader.GetInt32(0),
                        Iduser = reader.GetInt32(1),
                        dataRegister = reader.GetString(2),
                        describ = reader.GetString(3),
                        ConditionMessage = reader.GetString(4),
                        UserConfirm = reader.GetString(5),

                    });
                }
            }

            return MyUserMessagesInternet;
        }


        public void Insert(int Iduser,string dataRegister,string describ,string ConditionMessage,string UserConfirm)
        {

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";
            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();
                string query = $"INSERT INTO UaserInform (Iduser,dataRegister, describ, ConditionMessage, UserConfirm) VALUES (@Iduser,@dataRegister, @describ, @ConditionMessage, @UserConfirm);";
                var cmd = new SqliteCommand(query, conn);

                cmd.Parameters.AddWithValue("@Iduser", Iduser);
                cmd.Parameters.AddWithValue("@dataRegister", dataRegister);
                cmd.Parameters.AddWithValue("@describ", describ);
                cmd.Parameters.AddWithValue("@ConditionMessage", ConditionMessage);
                cmd.Parameters.AddWithValue("@UserConfirm", UserConfirm);
    

                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }

        public void Update(int Id, string dataRegister,string describ)
        {

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";
            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();
                string query = $"Update UaserInform set describ=@describ,dataRegister=@dataRegister where Id=@Id";
                var cmd = new SqliteCommand(query, conn);

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@dataRegister", dataRegister);
                cmd.Parameters.AddWithValue("@describ", describ);
           
                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }

        public void Delete(int Id)
        {

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";
            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();
                string query = $"Delete from UaserInform where Id=@Id";
                var cmd = new SqliteCommand(query, conn);

                cmd.Parameters.AddWithValue("@Id", Id);


                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }



        public void EnableDesable(int Id,string sti)
        {

            string mysit = "";

            if (sti == "Enable") { mysit = "Desable"; }
            if (sti == "Desable") { mysit = "Enable"; }

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = System.IO.Path.Combine(exepath, "DataBases", "OCServer.db");
            string connectionstring = $"Data Source={dbpath}";
            using (var conn = new SqliteConnection(connectionstring))
            {
                conn.Open();
                string query = $"Update UaserInform set ConditionMessage=@ConditionMessage where Id=@Id";
                var cmd = new SqliteCommand(query, conn);

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@ConditionMessage", mysit);


                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }


    }
}
