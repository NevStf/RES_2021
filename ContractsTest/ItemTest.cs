using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Resources;

namespace ContractsTest
{
    [TestFixture]
    public class ItemTest
    {
        [Test]
        [TestCase(Codes.CODE_ANALOG, 456.7)]
        [TestCase(Codes.CODE_DIGITAL, 46.7)]
        [TestCase(Codes.CODE_CUSTOM, 4.7)]
        [TestCase(Codes.CODE_LIMITSET, 245.7)]
        [TestCase(Codes.CODE_SINGLEONE, 369.7)]
        [TestCase(Codes.CODE_MULTIPLEONE, 584.0)]
        [TestCase(Codes.CODE_CONSUMER, 639.1)]
        [TestCase(Codes.CODE_SOURCE, 666.6)]

        public void ItemKonstruktorTest(Codes c, double value)
        {
            Item i = new Item(c, value);

            Assert.AreEqual(i.Code, c);
            Assert.AreEqual(i.Value, value);
        }

    }
}
