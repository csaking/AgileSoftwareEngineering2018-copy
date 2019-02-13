using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading;

namespace Agile_2018.Tests
{
    [TestClass]
    public class DatabaseFileHandlerTests
    {
        String userID;
        int projectID;
        Project newProject = new Project();

        [TestInitialize]
        public void TestInit()
        {
            MySqlCommand cmd;
            ConnectionClass.OpenConnection();
            cmd = ConnectionClass.con.CreateCommand(); //New Connection object
            cmd.CommandText = "INSERT INTO logindetails(StaffID,Forename,Surname,Pass,Position,Email)VALUES(1,1,1,1,1,1);SELECT LAST_INSERT_ID();";
            // Execute Query
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                userID = reader.GetString("LAST_INSERT_ID()");
            }
            reader.Close();
            ConnectionClass.CloseConnection();

            //Name of new project to be added
            string teststring = "AnotherTest"; //random teststring
            projectID = Int32.Parse(newProject.CreateProject(teststring, Int32.Parse(userID)));
        }

        [TestCleanup]
        public void CleanUp()
        {
            MySqlCommand cmd;
            ConnectionClass.OpenConnection();
            cmd = ConnectionClass.con.CreateCommand(); //New Connection object
            cmd.CommandText = "DELETE FROM storedfiles WHERE ProjectID = " + projectID;
            cmd.ExecuteReader();
            ConnectionClass.CloseConnection();
            newProject.DeleteProject(projectID);
            ConnectionClass.OpenConnection();
            cmd = ConnectionClass.con.CreateCommand(); //New Connection object
            cmd.CommandText = "DELETE FROM logindetails WHERE UserID = " + userID;
            cmd.ExecuteReader();
            ConnectionClass.CloseConnection();
        }

        [TestMethod]
        public void UploadFile()
        {
            DatabaseFileHandler dfh = new DatabaseFileHandler();

            String fileName = "test.txt";
            String path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            String fullPath = System.IO.Path.Combine(path, fileName);
            if (!File.Exists(fullPath))
            {
                using (StreamWriter sw = File.CreateText(fullPath))
                {
                    sw.WriteLine("TEST FILE :)");
                    sw.WriteLine("Maybe it needs lots of text????");
                }
                while (!File.Exists(fullPath))
                {
                    Thread.Sleep(1000);
                }
            }

            int i = dfh.UploadFile(projectID, File.Open(fullPath, FileMode.Open), fileName);

            Assert.AreEqual(1, i);
            File.Delete(fullPath);
        }

        [TestMethod]
        public void DownloadAllFiles()
        {
            DatabaseFileHandler dfh = new DatabaseFileHandler();

            String fileName = "test.txt";
            String path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            String fullPath = System.IO.Path.Combine(path, fileName);
            if (!File.Exists(fullPath))
            {
                using (StreamWriter sw = File.CreateText(fullPath))
                {
                    sw.WriteLine("TEST FILE :)");
                    sw.WriteLine("Maybe it needs lots of text????");
                }
                while (!File.Exists(fullPath))
                {
                    Thread.Sleep(1000);
                }
            }

            int i = dfh.UploadFile(projectID, File.Open(fullPath, FileMode.Open), fileName);

            path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            List<String> fileList = dfh.DownloadAllFiles(projectID, path);

            foreach (String f in fileList)
            {
                if (File.Exists(f))
                {
                    Assert.IsTrue(true);
                    File.Delete(f);
                }
            }

            int fileID = 0;

            ConnectionClass.OpenConnection();
            MySqlCommand comm = ConnectionClass.con.CreateCommand();
            comm.CommandText = "SELECT FileID FROM storedfiles sf WHERE sf.FileName = @fileName AND sf.ProjectID = @id";
            comm.Parameters.AddWithValue("@fileName", fileName);
            comm.Parameters.AddWithValue("@id", projectID);
            using (MySqlDataReader sqlQueryResult = comm.ExecuteReader())
                if (sqlQueryResult != null)
                {
                    sqlQueryResult.Read();
                    fileID = Int32.Parse(sqlQueryResult["FileID"].ToString());
                }
            ConnectionClass.CloseConnection();

            dfh.DeleteFile(fileID);
        }

        [TestMethod]
        public void DownloadFile()
        {
            DatabaseFileHandler dfh = new DatabaseFileHandler();

            String path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            List<String> fileList = dfh.DownloadFile(projectID, path);

            foreach (String f in fileList)
            {
                if (File.Exists(f))
                {
                    Assert.IsTrue(true);
                    File.Delete(f);
                }
            }
        }

        [TestMethod]
        public void DeleteFile()
        {
            DatabaseFileHandler dfh = new DatabaseFileHandler();

            String fileName = "test.txt";
            String path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            String fullPath = System.IO.Path.Combine(path, fileName);
            if (!File.Exists(fullPath))
            {
                FileStream f = File.Create(fullPath);
                f.Close();
            }

            dfh.UploadFile(projectID, File.Open(fullPath, FileMode.Open), fileName);

            int fileID = 0;

            ConnectionClass.OpenConnection();
            MySqlCommand comm = ConnectionClass.con.CreateCommand();
            comm.CommandText = "SELECT FileID FROM storedfiles sf WHERE sf.FileName = @fileName AND sf.ProjectID = @id";
            comm.Parameters.AddWithValue("@fileName", fileName);
            comm.Parameters.AddWithValue("@id", projectID);
            using(MySqlDataReader sqlQueryResult = comm.ExecuteReader())
                if (sqlQueryResult != null)
            {
                sqlQueryResult.Read();
                    fileID = Int32.Parse(sqlQueryResult["FileID"].ToString());
            }
            ConnectionClass.CloseConnection();

            int i = dfh.DeleteFile(fileID);
            Assert.AreEqual(1, i);
        }
    }
}