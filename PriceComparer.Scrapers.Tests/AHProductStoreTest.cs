using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PriceComparer.Scrapers.AH;

namespace PriceComparer.Scrapers.Tests
{
    [TestClass]
    public class AHProductStoreTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var store = new AHProductStore();
            store.Find("gehakt");
        }
    }
}
