using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Agile_2018;
using MySql.Data.MySqlClient;

namespace Agile_2018.Tests
{
    [TestClass]
    public class UnitTestLoginDatabase
    {
        String userID;

        [TestInitialize]
        public void TestInit()
        {
            MySqlCommand cmd;
            ConnectionClass.OpenConnection();
            cmd = ConnectionClass.con.CreateCommand(); //New Connection object
            cmd.CommandText = "INSERT INTO logindetails(StaffID,Forename,Surname,Pass,Position,Email)VALUES('testlogin',1,1,'testlogin',1,1);SELECT LAST_INSERT_ID();";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                userID = reader.GetString("LAST_INSERT_ID()");
            }
            reader.Close();
            ConnectionClass.CloseConnection();
        }
        
        [TestMethod]
        public void Login()
        {            
            LoginClass test = new LoginClass();
            string result = test.ValidateLoginDetails("testlogin", "testlogin");
            Assert.AreEqual(userID,result);
            ConnectionClass.CloseConnection();
        }
        [TestCleanup]
        public void CleanUp()
        {
            MySqlCommand cmd;
            ConnectionClass.OpenConnection();
            cmd = ConnectionClass.con.CreateCommand(); //New Connection object
            cmd.CommandText = "DELETE FROM `17agileteam5db`.`logindetails` WHERE `UserID`= "+userID;
            cmd.ExecuteNonQuery();
            ConnectionClass.CloseConnection();
        }
    }
}
