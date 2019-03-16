using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PriceComparer.Plus.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var store = new PlusProductStore();

            store.Find("ei");
        }
    }
}
