using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Agile_2018
{
    public class Dean
    {
        /// <summary>
        /// Function which signs/reject project with Staff ID.
       ///  for procedure to work, user must have linked projects to their userID
      ///   i.e records must exist in userprojectpairing table
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="staffID"></param>
        /// <returns></returns>
        public int DeanReject(int projectID)
        {
            //assign stored procedure
            string storedProc = "`deanRejectProject`;";
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

        public int AssocDeanReject(int projectID)
        {
            //assign stored procedure
            string storedProc = "``assocDeanRejectProject``;";
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

        //clears the values of signings for testing purposes
        public int ClearValuesForTesting (int projectID)
        {
            //assign stored procedure
            string storedProc = "ClearValuesForTesting;";
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
    }
}