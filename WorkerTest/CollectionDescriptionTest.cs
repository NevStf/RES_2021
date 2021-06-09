using Contracts.Resources;
using NUnit.Framework;
using System;
using Worker;

namespace WorkerTest
{
    [TestFixture]
    public class CollectionDescriptionTest
    {
        [Test]
        [TestCase(1, 1)]
        public void TestKonstruktor(int id, int ds)
        {
            CollectionDescription cd = new CollectionDescription(id, ds);
            Assert.AreEqual(cd.ID, id);
            Assert.AreEqual(cd.DataSet, ds);
            Assert.IsEmpty(cd.HistoricalCollection);
        }

        [Test]
        [TestCase(1, Codes.CODE_LIMITSET, 321.4)]
        public void AddToHistoricalTest(int dataSet, Codes code, double value)
        {
            CollectionDescription cd = new CollectionDescription(1, dataSet);
            cd.AddToHistorical(dataSet, code, value);
            Assert.IsNotEmpty(cd.HistoricalCollection);
            cd.AddToHistorical(dataSet, code, value);
            Assert.AreEqual(cd.HistoricalCollection.Count, 1);
        }

        [Test]
        [TestCase(1, Codes.CODE_LIMITSET, 321.4)]
        public void AddToHistoricalTestTwoElements(int dataSet, Codes code, double value)
        {
            CollectionDescription cd = new CollectionDescription(1, dataSet);
            cd.AddToHistorical(dataSet, code, value);
            Assert.IsNotEmpty(cd.HistoricalCollection);
            cd.AddToHistorical(dataSet, Codes.CODE_CUSTOM, value);
            Assert.AreEqual(cd.HistoricalCollection.Count, 2);
            cd.AddToHistorical(dataSet, Codes.CODE_CUSTOM, value);
            Assert.AreEqual(cd.HistoricalCollection.Count, 2);
        }

    }
}
