using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace Agile_2018
{
    public class ProfileManager
    {
        public DataTable getUserInfo(int inputID)
        {
            //Connects to database
            ConnectionClass.OpenConnection();

            //Declare new mysql command using stored procedure.
            MySqlCommand command = new MySqlCommand("returnProfile", ConnectionClass.con);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new MySqlParameter("@id", inputID));

            //Create datatable for results to be read into
            DataTable dt = new DataTable();

            //Adaptor to read results into the datatable
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            //Fill the datatable with the results from the MYSQL command using data adapter
            adapter.Fill(dt);

            //Close Connection
            ConnectionClass.CloseConnection();

            /*
            foreach (DataRow dataRow in dt.Rows)
            {
                foreach (var item in dataRow.ItemArray)
                {
                    Console.WriteLine(item);
                }

            }
            */

            //If the datatable is empty, ie the project row does not exist in the database, then return null.
            if (dt == null)
            {
                return null;
            }
            //else if the project record does exist, return this datatable. 
            else
            {
                return dt;
            }


        }

        public void updateTop(int id, string f, string l, string e)
        {
            ConnectionClass.OpenConnection();

            MySqlCommand command = new MySqlCommand("updateProfileTop", ConnectionClass.con);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new MySqlParameter("@u", id));
            command.Parameters.Add(new MySqlParameter("@f", f));
            command.Parameters.Add(new MySqlParameter("@l", l));
            command.Parameters.Add(new MySqlParameter("@e", e));

            command.ExecuteNonQuery();
            ConnectionClass.CloseConnection();
        }

        public void updateBot(int id, string p)
        {
            ConnectionClass.OpenConnection();

            MySqlCommand command = new MySqlCommand("updateProfileBot", ConnectionClass.con);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new MySqlParameter("@u", id));
            command.Parameters.Add(new MySqlParameter("@p", p));


            command.ExecuteNonQuery();
            ConnectionClass.CloseConnection();
        }


    }
}