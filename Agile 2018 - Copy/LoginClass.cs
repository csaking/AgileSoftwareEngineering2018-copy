using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;

namespace Agile_2018
{
    public class LoginClass
    {
        public string ValidateLoginDetails(string StaffID, string pwd)
        {
            //assign stored procedure
            string storedProc = "checkLogin;";
            DataTable dt = new DataTable(); //this is creating a virtual table
            ConnectionClass.OpenConnection();

            //open connection
            MySqlConnection connection = new MySqlConnection(ConnectionClass.ConnectionString);
            MySqlDataAdapter sda = new MySqlDataAdapter(storedProc,connection);
            //assign parameters
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.AddWithValue("?sID", StaffID);
            sda.SelectCommand.Parameters.AddWithValue("?pwd", pwd);

            // in above line the program is selecting the whole data from table and the matching it with the user name and password provided by user. 

            sda.Fill(dt);
            try      //when data table has something in it
            {
                Console.WriteLine("found!");
                ConnectionClass.CloseConnection();
                string uid = dt.Rows[0][0].ToString();      //store the user id as a string
                return uid;                //return string
            }
            catch(Exception)                   //when data table is empty
            {
                return null;
            }
        }
    }
}