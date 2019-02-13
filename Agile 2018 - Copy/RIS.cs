using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Agile_2018
{
    public class RIS
    {
        /// <summary>
        /// Function which signs/reject project with Staff ID.
        ///  for procedure to work, user must have linked projects to their userID
        ///   i.e records must exist in userprojectpairing table
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="staffID"></param>
        /// <returns></returns>
        public int RISReject(int projectID)
        {
            //assign stored procedure
            string storedProc = "`RISRejectProject`;";
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