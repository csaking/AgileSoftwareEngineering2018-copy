using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Agile_2018;
using MySql.Data.MySqlClient;

namespace Agile_2018.Tests
{
    [TestClass]
    public class UnitTestSign
    {
        string researcherID;
        string risID;
        string asDeanID;
        string deanID;
        int projectID;
        Project newProject = new Project();
        string testString = "SigningTest";

        [TestInitialize]
        public void TestInit()
        {
            MySqlCommand cmd, cmd1, cmd2, cmd3;
            ConnectionClass.OpenConnection();
            cmd = ConnectionClass.con.CreateCommand(); //New Connection object
            cmd1 = ConnectionClass.con.CreateCommand();
            cmd2 = ConnectionClass.con.CreateCommand();
            cmd3 = ConnectionClass.con.CreateCommand();

            cmd.CommandText = "INSERT INTO Logindetails(staffID,Forename,Surname,Pass,Position,Email)VALUES('researcher',1,1,1,0,1);SELECT LAST_INSERT_ID();";
            MySqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                researcherID = r.GetString("LAST_INSERT_ID()");
            }
            r.Close();
            cmd1.CommandText = "INSERT INTO Logindetails(staffID,Forename,Surname,Pass,Position,Email)VALUES('ris',1,1,1,1,1);";
            r = cmd.ExecuteReader();
            while (r.Read())
            {
                risID = r.GetString("LAST_INSERT_ID()");
            }
            r.Close();
            cmd2.CommandText = "INSERT INTO Logindetails(staffID,Forename,Surname,Pass,Position,Email)VALUES('assdean',1,1,1,2,1);";
            r = cmd.ExecuteReader();
            while (r.Read())
            {
                asDeanID = r.GetString("LAST_INSERT_ID()");
            }
            r.Close();
            cmd3.CommandText = "INSERT INTO Logindetails(staffID,Forename,Surname,Pass,Position,Email)VALUES('dean',1,1,1,3,1);";
            r = cmd.ExecuteReader();
            while (r.Read())
            {
                deanID = r.GetString("LAST_INSERT_ID()");
            }
            r.Close();

            ConnectionClass.CloseConnection();
        }

        [TestMethod]
        public void TestSignResearcher()
        {
            projectID = Int32.Parse(newProject.CreateProject(testString, Int32.Parse(researcherID)));
            Project testResearcher = new Project();
            int i = testResearcher.ResearcherSign(projectID, researcherID);
            Assert.AreEqual(1, i);
        }

        [TestMethod]
        public void TestSignRIS()
        {
            projectID = Int32.Parse(newProject.CreateProject(testString, Int32.Parse(researcherID)));
            newProject.ResearcherSign(projectID, researcherID);
            int i = newProject.RISSign(projectID, risID);
            Assert.AreEqual(1, i);
        }

        [TestMethod]
        public void TestSignAssocDean()
        {
            projectID = Int32.Parse(newProject.CreateProject(testString, Int32.Parse(researcherID)));
            newProject.ResearcherSign(projectID, researcherID);
            newProject.RISSign(projectID, risID);
            int i = newProject.AssocDeanSign(projectID, asDeanID);
            Assert.AreEqual(1, i);
        }

        [TestMethod]
        public void TestSignDean()
        {
            projectID = Int32.Parse(newProject.CreateProject(testString, Int32.Parse(researcherID)));
            newProject.ResearcherSign(projectID, researcherID);
            newProject.RISSign(projectID, risID);
            newProject.AssocDeanSign(projectID, asDeanID);
            int i = newProject.DeanSign(projectID, deanID);
            Assert.AreEqual(1, i);
        }

        [TestCleanup]
        public void CleanUp()
        {
            MySqlCommand cmd;
            newProject.DeleteProject(projectID);
            ConnectionClass.OpenConnection();
            cmd = ConnectionClass.con.CreateCommand();
            cmd.CommandText = "DELETE FROM logindetails WHERE UserID=" + researcherID;
            cmd.ExecuteNonQuery();
            cmd = ConnectionClass.con.CreateCommand();
            cmd.CommandText = "DELETE FROM logindetails WHERE UserID=" + risID;
            cmd.ExecuteNonQuery();
            cmd = ConnectionClass.con.CreateCommand();
            cmd.CommandText = "DELETE FROM logindetails WHERE UserID=" + asDeanID;
            cmd.ExecuteNonQuery();
            cmd = ConnectionClass.con.CreateCommand();
            cmd.CommandText = "DELETE FROM logindetails WHERE UserID=" + deanID;
            cmd.ExecuteNonQuery();
            ConnectionClass.CloseConnection();
        }
    }
}