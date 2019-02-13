using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Agile_2018;

namespace Agile_2018.Tests
{
    [TestClass]
    public class UnitTestLoginProc
    {
        [TestMethod]
        public void testCallproc()
        {
            //create instance of procedure class
            ProcedureClass proc = new ProcedureClass();
            //run procedure through class
            //using test values
            proc.loginCreate("testID", "testforename", "testsurname", "testpassword", 1, "testemail@test.test");
        }
    }
}
