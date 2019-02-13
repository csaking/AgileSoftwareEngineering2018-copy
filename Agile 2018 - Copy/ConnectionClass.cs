using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Agile_2018
{
    //Class to control connecting and disconnecting to and from database
    //Static class so methods can be called from anywhere
    public static class ConnectionClass
    {
        //variable holds database information
        static public string ConnectionString = "Server=silva.computing.dundee.ac.uk;Uid=17agileteam5;Pwd=7485.at5.5847;Database=17agileteam5db;";
        //variable holds the SQL connection
        static public MySqlConnection con;

        //function that connects to the database
        public static void OpenConnection()
        {
            con = new MySqlConnection(ConnectionString);
            con.Open();
        }

        //function that disconnects to the database
        public static void CloseConnection()
        {
            con.Close();
        }
    }
}