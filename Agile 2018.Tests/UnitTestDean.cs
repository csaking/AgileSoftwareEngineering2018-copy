using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Agile_2018;

namespace Agile_2018.Tests
{
    [TestClass]
    public class UnitTestDean
    {
        [TestMethod]
        public void TestRejectDean()
        {
            Dean testDean = new Dean();
            int i = testDean.DeanReject(1);
            Assert.AreEqual(1, i);
        }

        //deanSigned must be 0 in table to pass
        [TestMethod]
        public void TestSignDean()
        {
            Dean testDean = new Dean();
            testDean.ClearValuesForTesting(1);
            int i = testDean.DeanSign(1, "11");
            Assert.AreEqual(1, i);
        }

        //assocDeanSigned must be 0 in table to pass
        [TestMethod]
        public void TestAssocSignDean()
        {
            Dean testDean = new Dean();
            testDean.ClearValuesForTesting(1);
            int i = testDean.AssocDeanSign(1, "12");
            Assert.AreEqual(1, i);
        }

        
    }
}
