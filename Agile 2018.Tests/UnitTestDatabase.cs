using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Agile_2018;

namespace Agile_2018.Tests
{
    [TestClass]
    public class UnitTestDatabaseConnection
    {
        //Test for connection to database
        [TestMethod]
        public void DatabaseConnectionTest()
        {
            //open connection            
            ConnectionClass.OpenConnection();

            //check connection bhas opened
            Assert.IsTrue(ConnectionClass.con.State == System.Data.ConnectionState.Open);

            //close connection
            ConnectionClass.CloseConnection();

            //check connection is closed
            Assert.IsTrue(ConnectionClass.con.State == System.Data.ConnectionState.Closed);
        }
    }
}
