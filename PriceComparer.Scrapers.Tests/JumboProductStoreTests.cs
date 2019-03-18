using Microsoft.VisualStudio.TestTools.UnitTesting;
using PriceComparer.Scrapers.Jumbo;

namespace PriceComparer.Scrapers.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var store = new JumboProductStore();

            store.Find("ei");
        }
    }
}
