using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Agile_2018;
using MySql.Data.MySqlClient;

namespace Agile_2018.Tests
{
    [TestClass]
    public class AutomaticEmailTests
    {
        String userID;

        [TestInitialize]
        public void TestInit()
        {
            MySqlCommand cmd;
            ConnectionClass.OpenConnection();
            cmd = ConnectionClass.con.CreateCommand(); //New Connection object
            cmd.CommandText = "INSERT INTO `17agileteam5db`.`logindetails` (`StaffID`, `Forename`, `Surname`, `Pass`, `Position`, `Email`) VALUES ('1', '1', '1', '1', '1', 'testemail@acapper.tk');SELECT LAST_INSERT_ID();";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                userID = reader.GetString("LAST_INSERT_ID()");
            }
            reader.Close();
            ConnectionClass.CloseConnection();
        }

        [TestMethod]
        public void SendConfirmationEmail()
        {
            AutomaticEmail ae = new AutomaticEmail();
            bool r = ae.SendEmail("testemail@acapper.tk", "Test", "This is an automated test");
            Assert.IsTrue(r);
        }

        [TestMethod]
        public void GetEmail()
        {
            AutomaticEmail ae = new AutomaticEmail();
            String r = ae.getUserEmail(Int32.Parse(userID));
            Assert.AreEqual("testemail@acapper.tk", r);
        }

        [TestCleanup]
        public void CLeanUp()
        {
            MySqlCommand cmd;
            ConnectionClass.OpenConnection();
            cmd = ConnectionClass.con.CreateCommand(); //New Connection object
            cmd.CommandText = "DELETE FROM `17agileteam5db`.`logindetails` WHERE `UserID`= " + userID;
            cmd.ExecuteNonQuery();
            ConnectionClass.CloseConnection();
        }
    }
}
