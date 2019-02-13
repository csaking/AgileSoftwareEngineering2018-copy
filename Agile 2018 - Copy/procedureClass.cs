using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Agile_2018
{
    //class that defines how to execute a stored procedure
    //stored procedures and their parameter names are within the MySQL db
    public class ProcedureClass
    {
        //function that calls the stored procedure createLogin
        //takes 6 parameters
        //only for stored procedure example reference, should not be used in system
        public void loginCreate(string staffID, string forename, string surname, string pwd, int position, string email)
        {
            //create reader - reader will be what executes the procedure
            MySqlDataReader rdr = null;
            //define stored procedure to run
            string storedProc = "loginCreate;";

            //open connection
            MySqlConnection connection = new MySqlConnection(ConnectionClass.ConnectionString);
            connection.Open();

            //create command and assign stored procedure type
            MySqlCommand cmd = new MySqlCommand(storedProc, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //assign parameters
            //parameters are assigned with ? followed by their name within the stored procedure
            //i.e a procedure in MySQL with the parameter sID (short for staffID), is denoted as ?sID in C#
            cmd.Parameters.Add(new MySqlParameter("?sID", staffID));
            cmd.Parameters.Add(new MySqlParameter("?fName", forename));
            cmd.Parameters.Add(new MySqlParameter("?sName", surname));
            cmd.Parameters.Add(new MySqlParameter("?pwd", pwd));
            cmd.Parameters.Add(new MySqlParameter("?pos", position));
            cmd.Parameters.Add(new MySqlParameter("?eMail", email));

            //execute with reader
            rdr = cmd.ExecuteReader();

            //close connection
            connection.Close();
            //close reader
            rdr.Close();
        }

    }
}