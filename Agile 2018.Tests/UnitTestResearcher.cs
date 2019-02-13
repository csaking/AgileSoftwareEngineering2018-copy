using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Agile_2018;

namespace Agile_2018.Tests
{
    [TestClass]
    public class UnitTestResearcher
    {
        [TestMethod]
        public void TestSign()
        {
            Researcher testResearcher = new Researcher();
            int i = testResearcher.Sign(50, "11");
            Assert.AreEqual(1, i);
        }
    }
}
