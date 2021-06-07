using NUnit.Framework;
using System;
using Reader;
using Contracts.Resources;
using System.Collections.Generic;
using Contracts;
using System.ServiceModel;

namespace ReaderTest
{
    [TestFixture]
    public class ReaderTest
    {
        [Test]
        public void WriteElTestThrowsEx()
        {
            ReaderClass r = new ReaderClass();
            List<WorkerProperty> l = new List<WorkerProperty>();
            Assert.Throws<FaultException<CustomException>>( () =>r.WriteElements(l));
        }

        [Test]
        public void WriteElTestNoEx()
        {
            ReaderClass r = new ReaderClass();
            List<WorkerProperty> l = new List<WorkerProperty>();
            WorkerProperty wp = new WorkerProperty(Codes.CODE_ANALOG, 12.2);
            l.Add(wp);
            Assert.DoesNotThrow(() => r.WriteElements(l));
        }
    }
}
