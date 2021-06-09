using Contracts.Resources;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractsTest
{
    [TestFixture]
    public class WorkerPropertyTest
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

        public void WorkerProperty2Test(Codes c, double v)
        {
            WorkerProperty wp2 = new WorkerProperty(c, v);
            Assert.AreEqual(wp2.Code, c);
            Assert.AreEqual(wp2.WorkerValue, v);
            Assert.IsNotNull(wp2.TimeStamp);
            Assert.IsNotNull(wp2.WorkerID);
        }

        [Test]
        [TestCase(1, Codes.CODE_ANALOG, 456.7, null)] // <- if it looks stupid and it works... 
        //[TestCase(Codes.CODE_DIGITAL, 46.7)]
        //[TestCase(Codes.CODE_CUSTOM, 4.7)]
        //[TestCase(Codes.CODE_LIMITSET, 245.7)]
        //[TestCase(Codes.CODE_SINGLEONE, 369.7)]
        //[TestCase(Codes.CODE_MULTIPLEONE, 584.0)]
        //[TestCase(Codes.CODE_CONSUMER, 639.1)]
        //[TestCase(Codes.CODE_SOURCE, 666.6)]

        public void WorkerProperty4Test(int ID, Codes c, double v, DateTime dt)
        {
            DateTime Datet = new DateTime(2021, 6, 7, 23, 20, 10);
            WorkerProperty wp4 = new WorkerProperty(ID, c, v, Datet);


            Assert.AreEqual(wp4.WorkerID, ID);
            Assert.AreEqual(wp4.Code, c);
            Assert.AreEqual(wp4.WorkerValue, v);
            Assert.AreEqual(wp4.TimeStamp, Datet);
        }
    }
}
