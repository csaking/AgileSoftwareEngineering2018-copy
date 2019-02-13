using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Agile_2018;
using MySql.Data.MySqlClient;

namespace Agile_2018.Tests
{
    [TestClass]
    public class Issue3Test
    {
        [TestMethod]
        public void CreateProjectTest()
        {
            MySqlCommand cmd;
            ConnectionClass.OpenConnection();
            cmd = ConnectionClass.con.CreateCommand(); //New Connection object
            cmd.CommandText = "INSERT INTO logindetails(StaffID,Forename,Surname,Pass,Position,Email)VALUES(1,1,1,1,1,1);SELECT LAST_INSERT_ID();";
            // Execute Query
            MySqlDataReader reader = cmd.ExecuteReader();
            String uID = "";
            while (reader.Read())
            {
                uID = reader.GetString("LAST_INSERT_ID()");
            }
            reader.Close();
            ConnectionClass.CloseConnection();


            Project newProject = new Project();

            //Name of new project to be added
            string teststring = "AnotherTest"; //random teststring

            String project = newProject.CreateProject(teststring, Int32.Parse(uID));
            Assert.IsNotNull(project);

            newProject.DeleteProject(Int32.Parse(project));
            ConnectionClass.OpenConnection();
            cmd = ConnectionClass.con.CreateCommand(); //New Connection object
            cmd.CommandText = "DELETE FROM logindetails WHERE UserID = " + uID;
            cmd.ExecuteReader();
            ConnectionClass.CloseConnection();
        }
    }
}
