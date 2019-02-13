using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using Agile_2018;
using System.Data;

namespace Agile_2018.Tests
{
    [TestClass]
    public class UnitTestViewProjects
    {
        String userID;
        String risID;
        String AsDeanID = "457";
        String DeanID = "458";
        int projectID;
        Project newProject = new Project();

        [TestInitialize]
        public void TestInit()
        {
            MySqlCommand cmd;
            ConnectionClass.OpenConnection();
            cmd = ConnectionClass.con.CreateCommand(); //New Connection object
            cmd.CommandText = "INSERT INTO logindetails(StaffID,Forename,Surname,Pass,Position,Email)VALUES(1,1,1,1,0,1);SELECT LAST_INSERT_ID();";
            // Execute Query
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                userID = reader.GetString("LAST_INSERT_ID()");
            }
            reader.Close();

            cmd = ConnectionClass.con.CreateCommand(); //New Connection object
            cmd.CommandText = "INSERT INTO logindetails(StaffID,Forename,Surname,Pass,Position,Email)VALUES(1,1,1,1,1,1);SELECT LAST_INSERT_ID();";
            // Execute Query
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                risID = reader.GetString("LAST_INSERT_ID()");
            }
            reader.Close();
            ConnectionClass.CloseConnection();
            //Name of new project to be added
            string teststring = "UnitTestViewProject"; //random teststring
            projectID = Int32.Parse(newProject.CreateProject(teststring, Int32.Parse(userID)));
        }

        [TestCleanup]
        public void CleanUp()
        {
            MySqlCommand cmd;
            newProject.DeleteProject(projectID);
            ConnectionClass.OpenConnection();
            cmd = ConnectionClass.con.CreateCommand(); //New Connection object
            cmd.CommandText = "DELETE FROM logindetails WHERE UserID = " + userID;
            cmd.ExecuteNonQuery();
            cmd = ConnectionClass.con.CreateCommand(); //New Connection object
            cmd.CommandText = "DELETE FROM logindetails WHERE UserID = " + risID;
            cmd.ExecuteNonQuery();
            ConnectionClass.CloseConnection();
        }

        [TestMethod]
        public void ViewAllProjectsTest()
        {
            DataTable table = new DataTable();
            Project p = new Project();
            
            ViewProjects testView = new ViewProjects();
            table = testView.ViewAllProjects(userID);
            int i = table.Rows.Count;
            
            Assert.AreNotEqual(0,i);

            p.ResearcherSign(projectID, risID);
            table = testView.ViewAllProjects(risID);
            i = table.Rows.Count;

            Assert.AreNotEqual(0, i);
        }

        [TestMethod]
        public void ViewSignedProjectsTest()
        {
            DataTable table = new DataTable();
            Project p = new Project();

            ViewProjects testView = new ViewProjects();
            p.ResearcherSign(projectID, userID);
            p.RISSign(projectID, risID);
            p.AssocDeanSign(projectID, AsDeanID);
            p.DeanSign(projectID, DeanID);
            table = testView.ViewSignedProjects(DeanID);
            int i = table.Rows.Count;

            Assert.AreNotEqual(0, i);
        }
    }
}