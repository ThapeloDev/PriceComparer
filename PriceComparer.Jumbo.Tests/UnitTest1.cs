﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PriceComparer.Jumbo.Tests
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
