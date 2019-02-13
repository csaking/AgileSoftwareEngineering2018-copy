using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Agile_2018;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using System.Threading;

namespace Agile_2018.Tests
{
    [TestClass]
    public class UnitTestProjectManager
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

        //Method which tests if the search function works correctly by calling the searchProject() method and comparing
        //its returned results to what the results should be.
        [TestMethod]
        public void viewProjectInfo()
        {
            ConnectionClass.OpenConnection();

            //Add the expected record to the database, which will have a title of "viewProjectInfoTest" and a user ID of "1".
            ProjectManager pm = new ProjectManager();
            
            //Actual
            DataTable dt = pm.viewProjectInfo(projectID);

            //Making actual result comparable by converting into string format
            string rowRead = "";
            foreach (DataRow dr in dt.Rows)
            {
                rowRead = dr["ProjectID"].ToString();
                Console.WriteLine(rowRead);
            }
            
            //Testing if strings are equal
            Assert.AreEqual(projectID.ToString(), rowRead, false, "There was an error with the view for your project.");

            //REMEMBER TO DELETE THE RECORDS - get Pete's delete project method
        }


        //Method which tests if the correct number of storedfiles results are returned for a specific ProjectID by calling viewProjectInfo() and passing in 51 which 
        //should return 1 file record. 
        [TestMethod]
        public void viewProjectFiles()
        {
            ConnectionClass.OpenConnection();

            //Add the expected record to the database, which will have a title of "test" and a user ID of "1".
            ProjectManager pm = new ProjectManager();
            //Create and upload test file
            DatabaseFileHandler dfh = new DatabaseFileHandler();

            String fileName = "viewProjectFilesTest.txt";
            String path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            String fullPath = System.IO.Path.Combine(path, fileName);
            if (!File.Exists(fullPath))
            {
                using (StreamWriter sw = File.CreateText(fullPath))
                {
                    sw.WriteLine("TEST FILE :3");
                    sw.WriteLine("Maybe it needs lots of text????");
                }
                while (!File.Exists(fullPath))
                {
                    Thread.Sleep(1000);
                }
            }

            int expectedRowCount = dfh.UploadFile(projectID, File.Open(fullPath, FileMode.Open), fileName);

            //Actual
            DataTable dt = pm.viewProjectFiles(projectID);
            int actualRowCount = dt.Rows.Count;

            Assert.AreEqual(expectedRowCount, actualRowCount);

            File.Delete(fullPath);
            DatabaseFileHandler dbfh = new DatabaseFileHandler();
            ConnectionClass.OpenConnection();
            MySqlCommand comm = ConnectionClass.con.CreateCommand();
            comm.CommandText = "SELECT FileID FROM storedfiles sf WHERE sf.FileName = @fileName AND sf.ProjectID = @id";
            comm.Parameters.AddWithValue("@fileName", fileName);
            comm.Parameters.AddWithValue("@id", projectID);

            int fileID = 0;

            using (MySqlDataReader sqlQueryResult = comm.ExecuteReader())
                if (sqlQueryResult != null)
                {
                    sqlQueryResult.Read();
                    fileID = Int32.Parse(sqlQueryResult["FileID"].ToString());
                }
            ConnectionClass.CloseConnection();

            int i = dfh.DeleteFile(fileID);




            //REMEMBER TO DELETE THE PROJECT RECORDS with pete's method
            newProject.DeleteProject(projectID);

        }


         //////////////////////////////////////////////////
        //  IGNORE BEYOND THIS POINT BUT DO NOT DELETE  //
       //////////////////////////////////////////////////

        /*
       //Create a project which is unsigned, run the method, then check to see if it is signed by comparing it to what you think it should be.  then delete
       [TestMethod]
       public void researcherConfirmation()
       {

           ConnectionClass.OpenConnection();

           //Add the expected record to the database, which will have a title of "test" and a user ID of "1".
           ProjectManager pm = new ProjectManager();
           Project expectedProject = new Project();
           int expectedProjectID = Convert.ToInt32(expectedProject.CreateProject("researcherConfirmationTest", 1));

           pm.researcherConfirmation(int expectedProjectID, 1);

            //Actual
            DataTable dt = pm.viewProjectInfo(expectedProjectID);

            //Making actual result comparable by converting into int format
            int rSign = 0;
            int sCode = 0;
            foreach (DataRow dr in dt.Rows)
            {
                rSign = Int32.Parse(dr["ResearcherSigned"].ToString());
                sCode = Int32.Parse(dr["StatusCode"].ToString());
                Console.WriteLine(rSign);
                Console.WriteLine(sCode);

            }
            
        }*/

        /*
        //Method wich tests whether the correct number of records are returned which are unconfirmed by a researcher. There should be 31 records with a status code of 0
        //as of 16/02/2018 but this will change. 
        [TestMethod]
        public void researcherConfirmation()
        {
            //get a row with statuscode and researchersigned both at 0
            //get statuscode and ResearcherSigned values of a project before running this method in a string
            //compare this to what they should be before in another string
            //run method on this projectID record
            //get new values of these two fields
            //compare these to old values. 

            //REMEMBER TO DELETE THE RECORDS - get Pete's delete project method

           //////////

           ConnectionClass.OpenConnection();
           ProjectManager pm = new ProjectManager();

           //this is a datatable of all the rows which are unconfirmed
           DataTable dt = pm.getResearcherUnconfirmedProjects();
           //get projectID for the first row of this table
           String rowRead = "";

           foreach (DataRow dr in dt.Rows)
           {
               rowRead = dr["ProjectID"].ToString();
               String projectID = rowRead.
               Console.WriteLine(rowRead);
           }

           //Expected before method is run
           String oldExpected = "1 Dylan 0 11 0 0 0";

       }
       */

        //Method wich tests whether the correct number of records are returned which are unconfirmed by a researcher. There should be 31 records with a status code of 0
        //as of 16/02/2018 but this will change. 

        /* MAY NOT BE NEEDED
       [TestMethod]
       public void getResearcherUnconfirmedProjects()
       {
           //should return 31 values
           ConnectionClass.OpenConnection();
           ProjectManager pm = new ProjectManager();

           //Expected number of rows returned (THIS IS CONSTANTLY CHANGING SO EXPECT TEST TO FAIL)
           int expected = 67;

           //Actual number of rows returned
           DataTable dt = pm.getResearcherUnconfirmedProjects();
           int actual = dt.Rows.Count;
           Console.WriteLine(actual);

           //Testing if variables are equal
           Assert.AreEqual(expected, actual);
       }
       */


    }
}  
