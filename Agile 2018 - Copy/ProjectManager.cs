using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Agile_2018
{
    public class ProjectManager
    {
        //DB INFO:
        //Hostname: silva.computing.dundee.ac.uk    
        //Port: 3306
        //Username: 17agileteam5
        //Password: 7485.at5.5847

        //DONE
        //Method which returns a datatable containing all the information returned for a project based on the projectID passed to it. 
        public DataTable viewProjectInfo(int input)
        {
            //Connects to database 
            ConnectionClass.OpenConnection();

            //Declare new mysql command using stored procedure.
            MySqlCommand command = new MySqlCommand("viewProjectInfo", ConnectionClass.con);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new MySqlParameter("@id", input));

            //Create datatable for results to be read into
            DataTable dt = new DataTable();

            //Adaptor to read results into the datatable
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            //Fill the datatable with the results from the MYSQL command using data adapter
            adapter.Fill(dt);

            //Close Connection
            ConnectionClass.CloseConnection();

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



        //Method which returns a datatable containing all the related files returned based on the projectID passed to it. 
        public DataTable viewProjectFiles(int input)
        {
            
            //Connects to database
            ConnectionClass.OpenConnection();

            //Declare new mysql command using stored procedure.
            MySqlCommand command = new MySqlCommand("viewProjectFiles", ConnectionClass.con);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new MySqlParameter("@id", input));

            //Create datatable for results to be read into
            DataTable dt = new DataTable();

            //Adaptor to read results into the datatable
            MySqlDataAdapter sda = new MySqlDataAdapter(command);
            //Fill the datatable with the results from the MYSQL command using data adapter
            sda.Fill(dt);

            //Close Connection
            ConnectionClass.CloseConnection();

            return dt;
        }



        //////////////////////////////////////////////////
        //  IGNORE BEYOND THIS POINT BUT DO NOT DELETE  //
        //////////////////////////////////////////////////


        /* //DO NOT DELETE
        //Method which returns all project records which have a status code of 0, ie need to be confirmed by a researcher. 
        public DataTable getResearcherUnconfirmedProjects()
        {
            //Connects to database
            ConnectionClass.OpenConnection();

            //Declare new mysql command using connection to returns all projects which need

            //MySqlCommand cmd = ConnectionClass.con.CreateCommand();
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "SELECT * FROM projects WHERE StatusCode = '0'";
            //cmd.ExecuteNonQuery();

            String query = "SELECT * FROM projects WHERE StatusCode = '0'";

            //Create datatable for results to be read into
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(query, ConnectionClass.con);
            //Fill the datatable with the results from the MYSQL command using data adapter
            da.Fill(dt);
            ConnectionClass.CloseConnection();
            //If the datatable is empty, ie there are no Projects which require a researcher to confirm
            if (dt == null)
            {
                return null;
            }
            //else if there are projects to be confirmed 
            else
            {
                return dt;
            }
        }
        */

        /*
         * 1. Using ProjectID researcher wants to sign, check the database to see if it has been confirmed or not already. 
         * 2. If it has not been signed yet, sign it
         * */


         //DO NOT DELETE
        //Function which takes in a ProjectID for the project to be confirmed, changing its status code to 1 and its Researcher signed value to userID
        public void researcherConfirmation1(int projectID, int userID)
        {

            //Connects to database
            ConnectionClass.OpenConnection();

            DataTable dt = viewProjectInfo(projectID);

            int researcherSigned = 0;
            int statusCode = 0;
            foreach (DataRow dr in dt.Rows)
            {
                researcherSigned = Convert.ToInt32(dr["ResearcherSigned"]);
                statusCode = Convert.ToInt32(dr["StatusCode"]);
                Console.WriteLine(researcherSigned + " " + statusCode);
            }

            if (researcherSigned == 0 && statusCode == 0)
            {
                //Declare new mysql command using connection which sets user specified project's ResearcherSigned and StatusCode values to userID


                ConnectionClass.OpenConnection();
                MySqlCommand cmd = ConnectionClass.con.CreateCommand();
                cmd.CommandText = "UPDATE projects SET ResearcherSigned = '" + userID + "', StatusCode = '1' WHERE ProjectID = '" + projectID + "'";
                cmd.ExecuteNonQuery();
                ConnectionClass.CloseConnection();

            }
       
            ConnectionClass.CloseConnection();

        }
        

    }
}