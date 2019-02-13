using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Agile_2018;
using MySql.Data.MySqlClient;

namespace Agile_2018.Tests
{
    [TestClass]
    public class UnitTestDeleteProject
    {
        [TestMethod]
        public void DeleteProjectTest()
        {
            Project newProject = new Project();

            //Procedure works by passing through the projectID, this should be determinable from the button that is pressed.
            //Variable to mimic passed through project ID
            string projectID = ""; //Project ID NEEDS to be set each time this test is ran.
            //To do this we will create a new record, grab the project id and then delete that project ID.

            //Create the project
            //  Variable title to insert
            //  User ID will be the default 15 boi
            int userID = 15;
            string title = "Delete This!";
            newProject.CreateProject(title, userID);

            //delete that trash
            MySqlCommand cmd;
            ConnectionClass.OpenConnection();
            cmd = ConnectionClass.con.CreateCommand(); //New Connection object
            cmd.CommandText = "SELECT ProjectID From projects Where Title = @newtitle";
            cmd.Parameters.AddWithValue("@newtitle", title);
            
            //Read the return and grab the HIGHEST project ID. This allows multiple of the same named records
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                projectID = reader["ProjectID"].ToString();

            }
            reader.Close();
            ConnectionClass.CloseConnection();
            //System.Windows.Forms.MessageBox.Show("The following number has been added and will be deleted : " +projectID);

            //PROJECT ID SHOULD NOW CONTAIN THE RESULT OF THE SELECT. THE SELECT LOOPS TO THE BOTTOM AND MAKES THE VARIABLE THE LOWEST VALUE. THIS ENSURES ONE RETURN, THE LATEST ADDITION OF THAT NAME.
            //now we have the project id
            //use the ProjID to Delete

            //parse the return
            int projID = Int32.Parse(projectID);

            //Delete
            Assert.IsTrue(newProject.DeleteProject(projID));

           
        }
    }
}
