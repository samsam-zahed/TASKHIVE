using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace TaskHive.Classes
{
    class ClassBackupAndRestore
    {

        public async Task BackUp(string path, string backupname)
        {
            if (!backupname.EndsWith(".db")) backupname += ".db";

            string exepath = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = Path.Combine(exepath, "DataBases", "OCServer.db");

          
            string connectionstring = $"Data Source={dbpath}";

          
            string backupDir = Path.Combine(path, "DataBases");
            if (!Directory.Exists(backupDir))
                Directory.CreateDirectory(backupDir);

            string backupPath = Path.Combine(backupDir, backupname);

         
            string backupConnectionString = $"Data Source={backupPath}";

            await Task.Run(() =>
            {
                using (var source = new SqliteConnection(connectionstring))
                using (var destination = new SqliteConnection(backupConnectionString))  
                {
                    source.Open();
                    destination.Open();
                    source.BackupDatabase(destination);
                }
            });

            MessageBox.Show(TaskHive.Strings.backucreatedsuccess);
        }

    

public async Task RestoreAsync(string backupFilePath)
    {
        string exepath = AppDomain.CurrentDomain.BaseDirectory;
        string dbpath = Path.Combine(exepath, "DataBases", "OCServer.db");

     
        string? dbDir = Path.GetDirectoryName(dbpath);
            if (!Directory.Exists(dbDir)) {

                Directory.CreateDirectory(dbDir);

            }
     

        string connectionstring = $"Data Source={dbpath}";
        string backupConnectionString = $"Data Source={backupFilePath}";

        await Task.Run(() =>
        {
            using (var backup = new SqliteConnection(backupConnectionString))
            using (var mainDb = new SqliteConnection(connectionstring))
            {
                backup.Open();
                mainDb.Open();
                backup.BackupDatabase(mainDb); 
            }
        });

        MessageBox.Show(TaskHive.Strings.Restoredsuccessfully);
    }

}
}
