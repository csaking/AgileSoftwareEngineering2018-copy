using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace Agile_2018
{
    public class Project
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public String CreateProject(string title, int userID)
        {
            MySqlCommand cmd;
            ConnectionClass.OpenConnection();
            cmd = ConnectionClass.con.CreateCommand(); //New Connection object

            try
            {
                //FIRST INSERT THE NEW PROJECT INTO THE PROJECTS TABLE
                //SQL Query
                cmd.CommandText = "INSERT INTO projects(Title)VALUES(@title);SELECT LAST_INSERT_ID();";

                // Populate SQl query values
                cmd.Parameters.AddWithValue("@title", title);

                // Execute Query
                MySqlDataReader reader = cmd.ExecuteReader();
                String pID = "";
                while (reader.Read())
                {
                    pID = reader.GetString("LAST_INSERT_ID()");
                }
                reader.Close();

                //FOLLOW BY INSERTING THE PROJECT AND ID INTO LINK
                //SQL Query
                cmd.CommandText = "INSERT INTO userprojectpairing(LoginDetails_UserID,Projects_ProjectID)VALUES(@userID,@projID)";

                // Populate SQl query values
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Parameters.AddWithValue("@projID", pID);


                // Execute Query
                cmd.ExecuteNonQuery();

                // Close Connection
                ConnectionClass.CloseConnection();
                return pID;
            }
            catch (Exception)
            {
                ConnectionClass.CloseConnection();
                return null;
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public bool UpdateTitle(int projectID, string title)
        {
            //assign stored procedure
            string storedProc = "updateTitle;";
            //open connection
            MySqlConnection connection = new MySqlConnection(ConnectionClass.ConnectionString);
            connection.Open();
            //define stored procedure
            MySqlCommand cmd = new MySqlCommand(storedProc, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //assign parameters
            cmd.Parameters.Add(new MySqlParameter("?pID", projectID));
            cmd.Parameters.Add(new MySqlParameter("?title", title));
            //execute procedure
            cmd.ExecuteNonQuery();
            //close connection and return number of rows affected (should be 1)
            connection.Close();
            return true;
        }

        internal string getProjectName(int projectID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets project record researcherSigned field = staffID. 
        /// Increments project record statusCode 1.
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        /// 
        public int ResearcherSign(int projectID, string staffID)
        {
            //assign stored procedure
            string storedProc = "researcherSignProject";
            //open connection
            MySqlConnection connection = new MySqlConnection(ConnectionClass.ConnectionString);
            connection.Open();
            //define stored procedure
            MySqlCommand cmd = new MySqlCommand(storedProc, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //assign parameters
            cmd.Parameters.Add(new MySqlParameter("?pID", projectID));
            cmd.Parameters.Add(new MySqlParameter("?sID", staffID));
            //execute procedure
            int i = cmd.ExecuteNonQuery();
            //close connection and return number of rows affected (should be 1)
            connection.Close();
            return i;
        }

        /// <summary>
        /// Sets project record RISSigned field = staffID. 
        /// Increments project record statusCode 1.
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="staffID"></param>
        /// <returns></returns>
        public int RISSign(int projectID, string staffID)
        {
            //assign stored procedure
            string storedProc = "RISSign;";
            //open connection
            MySqlConnection connection = new MySqlConnection(ConnectionClass.ConnectionString);
            connection.Open();
            //define stored procedure
            MySqlCommand cmd = new MySqlCommand(storedProc, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //assign parameters
            cmd.Parameters.Add(new MySqlParameter("?sID", staffID));
            cmd.Parameters.Add(new MySqlParameter("?pID", projectID));
            //execute procedure
            int i = cmd.ExecuteNonQuery();
            //close connection and return number of rows affected (should be 1)
            connection.Close();
            return i;
        }

        /// <summary>
        /// Sets project record AssocDeanSigned field = staffID. 
        /// Increments project record statusCode 1.
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="staffID"></param>
        /// <returns></returns>
        public int AssocDeanSign(int projectID, string staffID)
        {
            //assign stored procedure
            string storedProc = "assocDeanSign;";
            //open connection
            MySqlConnection connection = new MySqlConnection(ConnectionClass.ConnectionString);
            connection.Open();
            //define stored procedure
            MySqlCommand cmd = new MySqlCommand(storedProc, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //assign parameters
            cmd.Parameters.Add(new MySqlParameter("?pID", projectID));
            cmd.Parameters.Add(new MySqlParameter("?sID", staffID));
            //execute procedure
            int i = cmd.ExecuteNonQuery();
            //close connection and return number of rows affected (should be 1)
            connection.Close();
            return i;
        }

        /// <summary>
        /// Sets project record DeanSigned field = staffID. 
        /// Increments project record statusCode 1.
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="staffID"></param>
        /// <returns></returns>
        public int DeanSign(int projectID, string staffID)
        {
            //assign stored procedure
            string storedProc = "deanSign;";
            //open connection
            MySqlConnection connection = new MySqlConnection(ConnectionClass.ConnectionString);
            connection.Open();
            //define stored procedure
            MySqlCommand cmd = new MySqlCommand(storedProc, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //assign parameters
            cmd.Parameters.Add(new MySqlParameter("?pID", projectID));
            cmd.Parameters.Add(new MySqlParameter("?sID", staffID));
            //execute procedure
            int i = cmd.ExecuteNonQuery();
            //close connection and return number of rows affected (should be 1)
            connection.Close();
            return i;
        }

        /// <summary>
        /// Update project record statusCode to 5(rejected).
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public int ResearcherReject(int projectID)
        {
            //assign stored procedure
            string storedProc = "ResearcherRejectProject";
            //open connection
            MySqlConnection connection = new MySqlConnection(ConnectionClass.ConnectionString);
            connection.Open();
            //define stored procedure
            MySqlCommand cmd = new MySqlCommand(storedProc, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //assign parameters
            cmd.Parameters.Add(new MySqlParameter("?pID", projectID));
            //execute procedure
            int i = cmd.ExecuteNonQuery();
            //close connection and return number of rows affected (should be 1)
            connection.Close();
            return i;
        }

        /// <summary>
        /// Update project record statusCode to 5(rejected).
        /// Sets researcherSigned field to 0. 
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public int RISReject(int projectID)
        {
            //assign stored procedure
            string storedProc = "RISRejectProject";
            //open connection
            MySqlConnection connection = new MySqlConnection(ConnectionClass.ConnectionString);
            connection.Open();
            //define stored procedure
            MySqlCommand cmd = new MySqlCommand(storedProc, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //assign parameters
            cmd.Parameters.Add(new MySqlParameter("?pID", projectID));
            //execute procedure
            int i = cmd.ExecuteNonQuery();
            //close connection and return number of rows affected (should be 1)
            connection.Close();
            return i;
        }

        /// <summary>
        /// Update project record statusCode to 5(rejected).
        /// Sets researcherSigned & RISSigned fields to 0. 
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public int AssocDeanReject(int projectID)
        {
            //assign stored procedure
            string storedProc = "assocDeanRejectProject";
            //open connection
            MySqlConnection connection = new MySqlConnection(ConnectionClass.ConnectionString);
            connection.Open();
            //define stored procedure
            MySqlCommand cmd = new MySqlCommand(storedProc, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //assign parameters
            cmd.Parameters.Add(new MySqlParameter("?pID", projectID));
            //execute procedure
            int i = cmd.ExecuteNonQuery();
            //close connection and return number of rows affected (should be 1)
            connection.Close();
            return i;
        }

        /// <summary>
        /// Update project record statusCode to 5(rejected).
        /// Sets AssocDeanSigned, researcherSigned & RISSigned fields to 0. 
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public int DeanReject(int projectID)
        {
            //assign stored procedure
            string storedProc = "deanRejectProject";
            //open connection
            MySqlConnection connection = new MySqlConnection(ConnectionClass.ConnectionString);
            connection.Open();
            //define stored procedure
            MySqlCommand cmd = new MySqlCommand(storedProc, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //assign parameters
            cmd.Parameters.Add(new MySqlParameter("?pID", projectID));
            //execute procedure
            int i = cmd.ExecuteNonQuery();
            //close connection and return number of rows affected (should be 1)
            connection.Close();
            return i;
        }

        /*      
         * Below is potential sign/reject methods refactored. Reject doesn't require switch statement as it
         * doesn't matter who rejects it, all signed fields are reset to 0/default.
         * All reject stored procedures should simply use 'deanRejectProject'.
         *
        
        /// <summary>
        /// Sign project switch statement to run correct signing stored procedure.
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="staffID"></param>
        /// <param name="position"></param>
        public void SignChoice(int projectID, string staffID, int position)
        {
            switch (position)
            {
                case 0:
                    Sign(projectID, staffID, "researcherSignProject;");
                    break;
                case 1:
                    Sign(projectID, staffID, "RISSign;");
                    break;
                case 2:
                    Sign(projectID, staffID, "AssocDeanSign;");
                    break;
                case 3:
                    Sign(projectID, staffID, "DeanSign;");
                    break;
            }
        }

        /// <summary>
        /// Sign selected project with staffID based on user's job position.
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="staffID"></param>
        /// <param name="proc"></param>
        /// <returns>Number of records affected</returns>
        private int Sign(int projectID, string staffID, string proc)
        {
            //assign stored procedure
            string storedProc = proc;
            //open connection
            MySqlConnection connection = new MySqlConnection(ConnectionClass.ConnectionString);
            connection.Open();
            //define stored procedure
            MySqlCommand cmd = new MySqlCommand(storedProc, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //assign parameters
            cmd.Parameters.Add(new MySqlParameter("?pID", projectID));
            cmd.Parameters.Add(new MySqlParameter("?sID", staffID));
            //execute procedure
            int i = cmd.ExecuteNonQuery();
            //close connection and return number of rows affected (should be 1)
            connection.Close();
            return i;
        }

        /// <summary>
        /// Rejects selected project.
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="proc"></param>
        /// <returns>Number of records affected</returns>
        private int Reject(int projectID)
        {
            //assign stored procedure
            string storedProc = "rejectProject;";
            //open connection
            MySqlConnection connection = new MySqlConnection(ConnectionClass.ConnectionString);
            connection.Open();
            //define stored procedure
            MySqlCommand cmd = new MySqlCommand(storedProc, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //assign parameters
            cmd.Parameters.Add(new MySqlParameter("?pID", projectID));
            //execute procedure
            int i = cmd.ExecuteNonQuery();
            //close connection and return number of rows affected (should be 1)
            connection.Close();
            return i;
        }*/
        public bool DeleteProject(int projectID)
        {
            try
            {
                //assign stored procedure
                string storedProc = "DeletePairingAndProject;";

                //open connection
                MySqlConnection connection = new MySqlConnection(ConnectionClass.ConnectionString);
                connection.Open();

                //define stored procedure
                MySqlCommand cmd = new MySqlCommand(storedProc, connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //assign parameters
                cmd.Parameters.Add(new MySqlParameter("?pID", projectID));

                //execute procedure
                cmd.ExecuteNonQuery();
                ConnectionClass.CloseConnection();

                return true;//file deleted
            }
            catch (Exception)
            {
                ConnectionClass.CloseConnection();
                return false;
                throw;
            }
        }

        public int PostComment(string comment, int projectID)
        {
            string storedProc = "postComment";
            //open connection
            MySqlConnection connection = new MySqlConnection(ConnectionClass.ConnectionString);
            connection.Open();
            //define stored procedure
            MySqlCommand cmd = new MySqlCommand(storedProc, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("?pID", projectID));
            cmd.Parameters.Add(new MySqlParameter("?c", comment));
            //execute procedure
            int i = cmd.ExecuteNonQuery();
            //close connection and return number of rows affected (should be 1)
            connection.Close();
            return i;
        }

        public string GetComment(int projectID)
        {
            string storedProc = "getComment";
            MySqlConnection connection = new MySqlConnection(ConnectionClass.ConnectionString);
            connection.Open();
            //define stored procedure
            MySqlCommand cmd = new MySqlCommand(storedProc, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("?pID", projectID));
            //cmd.ExecuteNonQuery();
            //string returnvalue = (string)cmd.Parameters["comments"].Value;
            string returnvalue = "";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                returnvalue = reader.GetString("comments");
            }
            reader.Close();
            connection.Close();
            return returnvalue;
        }

        public int GetProjectOwner(int pID)
        {

            int owner = 0;
            ConnectionClass.OpenConnection();
            MySqlCommand comm = new MySqlCommand("GetProjectOwner", ConnectionClass.con);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@id", pID);
            using (MySqlDataReader sqlQueryResult = comm.ExecuteReader())
            {
                if (sqlQueryResult.HasRows)
                {
                    while (sqlQueryResult != null && sqlQueryResult.Read())
                    {
                        owner = (int)sqlQueryResult["LoginDetails_UserID"];
                    }
                }
            }
            ConnectionClass.CloseConnection();
            return owner;
        }

        public string GetProjectName(int projectID)
        {
            string projectName = "";
            ConnectionClass.OpenConnection();
            MySqlCommand comm = new MySqlCommand("GetProjectName", ConnectionClass.con);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@pID", projectID);
            using (MySqlDataReader sqlQueryResult = comm.ExecuteReader())
            {
                if (sqlQueryResult.HasRows)
                {
                    while (sqlQueryResult != null && sqlQueryResult.Read())
                    {
                        projectName = (string)sqlQueryResult["Title"];
                    }
                }
            }
            ConnectionClass.CloseConnection();
            return projectName;
        }

        public int GetRISSignID(int projectID)
        {
            int RISID = 0;
            ConnectionClass.OpenConnection();
            MySqlCommand comm = new MySqlCommand("GetRISSignID", ConnectionClass.con);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@pID", projectID);
            using (MySqlDataReader sqlQueryResult = comm.ExecuteReader())
            {
                if (sqlQueryResult.HasRows)
                {
                    while (sqlQueryResult != null && sqlQueryResult.Read())
                    {
                        RISID = (int)sqlQueryResult["Title"];
                    }
                }
            }
            ConnectionClass.CloseConnection();
            return RISID;
        }
    }
}