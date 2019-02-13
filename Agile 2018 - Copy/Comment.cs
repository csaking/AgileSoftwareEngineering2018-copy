using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Data;

namespace Agile_2018
{
    public class Comment
    {
        public int postComment(string comment, int projectID)
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

        public string getComment(int projectID)
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
            string returnvalue="";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                returnvalue = reader.GetString("comments");
            }
            reader.Close();
            connection.Close();
            return returnvalue;
        }

         
    }
}