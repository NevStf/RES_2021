using Contracts.Resources;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker;

namespace WorkerTest
{
    [TestFixture]
    public class WorkerImplementTest
    {
        [Test]
        public void InitListTest() 
        {
            WorkerImplement wi = new WorkerImplement();
            wi.InitList();

            Assert.IsNotNull(wi.LCD1);
            Assert.IsNotNull(wi.LCD2);
            Assert.IsNotNull(wi.LCD3);
            Assert.IsNotNull(wi.LCD4);

            Assert.AreEqual(4, wi.LCD1.Count);
            Assert.AreEqual(4, wi.LCD2.Count);
            Assert.AreEqual(4, wi.LCD3.Count);
            Assert.AreEqual(4, wi.LCD4.Count);
        }
        [Test]
        public void ValueHistoryTest() 
        {
            Mock<WorkerProperty> wpMock = new Mock<WorkerProperty>(1, Codes.CODE_ANALOG, 123.12, DateTime.Now);
            wpMock.Object.Code = Codes.CODE_ANALOG;
            wpMock.Object.WorkerValue = 123.12;
            wpMock.Object.WorkerID = 1;

            WorkerImplement wi = new WorkerImplement();
            wi.ValueHistory(wpMock.Object);

            Assert.AreEqual(wi.History.HistoricalCollection.Count, 1);
            Assert.Contains(wpMock.Object, wi.History.HistoricalCollection);
        }
        [Test]
        [TestCase(1, Codes.CODE_ANALOG)]
        public void ReadFromWorkerReturnsEmpty(int IDWorker, Codes code) 
        {
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;
            WorkerImplement wi = new WorkerImplement();
            List<WorkerProperty> list = wi.ReadFromWorker(IDWorker, code, start, end);

            Assert.IsNotNull(list);
            Assert.AreEqual(list.Count, 0);
        }

        [Test]
        [TestCase(1, Codes.CODE_ANALOG)]
        public void ReadFromWorkerReturnsSomething(int IDWorker, Codes code)
        {
            DateTime start = new DateTime(2021, 6, 2, 12, 0, 0);
            DateTime end = new DateTime(2022, 6, 2, 12, 0, 0);
            WorkerImplement wi = new WorkerImplement();
            wi.ValueHistory(new WorkerProperty(IDWorker, code, 123.12, DateTime.Now));
            List<WorkerProperty> list = wi.ReadFromWorker(IDWorker, code, start, end);

            Assert.IsNotNull(list);
            Assert.AreEqual(list.Count, 1);
        }
    }
}
