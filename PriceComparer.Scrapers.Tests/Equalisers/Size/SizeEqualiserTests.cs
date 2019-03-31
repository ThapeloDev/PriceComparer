using Microsoft.VisualStudio.TestTools.UnitTesting;
using PriceComparer.Domain.Products;
using PriceComparer.Scrapers.Equalisers.Size;

namespace PriceComparer.Scrapers.Tests.Equalisers.Size
{
    [TestClass]
    public class SizeEqualiserTests
    {
        private SizeEqualiser _equaliser;

        public SizeEqualiserTests()
        {
            _equaliser = new SizeEqualiser();
        }

        [TestMethod]
        public void TestMethod1()
        {
            var product1L = "1 l";
            var product2Liter = "2 liter";
            var productHalfLiter = "0,5 liter";
            var productOneAndHalfLiter = "1,5 liter";
            var productOneAndHalfLiterNewline = "pak 1,5 liter \n";

            AssertValues(product1L, 1);
            AssertValues(product2Liter, 2);
            AssertValues(productHalfLiter, 0.5M);
            AssertValues(productOneAndHalfLiter, 1.5M);
            AssertValues(productOneAndHalfLiterNewline, 1.5M);
        }

        private void AssertValues(string unitText, decimal expectedSize)
        {
            var unit = _equaliser.GetUnitSize(unitText);

            Assert.AreEqual(expectedSize, unit.Size);
            Assert.AreEqual("liter", unit.Unit);
        }
    }
}
