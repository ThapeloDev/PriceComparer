using Microsoft.VisualStudio.TestTools.UnitTesting;
using PriceComparer.Scrapers.Plus;

namespace PriceComparer.Scrapers.Tests
{
    [TestClass]
    public class PlusProductStoreTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var store = new PlusProductStore();

            store.Find("pijnboompitten");
        }
    }
}
