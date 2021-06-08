using Contracts;
using Contracts.Resources;
using LoadBalancer;
using Moq;
using NUnit.Framework;
using System;
using System.ServiceModel;
using Worker;

namespace LoadBalancerTest
{
    [TestFixture]
    public class LBImplementTest
    {
        [Test]
        public void InitListTest()
        {
            LBImplement lb = new LBImplement();

            lb.InitList();
            Assert.IsNotEmpty(lb.list);
            Assert.AreEqual(lb.list.Count, 4);
        }

        [Test]
        public void InitListDescTest()
        {
            LBImplement lb = new LBImplement();
            ListDescription ld = lb.InitListDesc();
            Assert.AreEqual(ld.ListOfDescription.Count, 4);
        }


        [Test]
        public void TurnOnTestEx()
        {
            LBImplement.Brojac = 4;
            LBImplement lb = new LBImplement();

            Assert.Throws<FaultException<CustomException>>(() => lb.TurnOnWorker());
            Assert.AreEqual(LBImplement.Brojac, 4);
        }
        [Test]
        public void TurnOnTestNoEx() 
        {
            LBImplement.Brojac = 1;
            LBImplement lb = new LBImplement();
            using (ServiceHost host = new ServiceHost(typeof(WorkerImplement)))
            {
                string address = "net.tcp://localhost:5000/IWorker";
                NetTcpBinding binding = new NetTcpBinding();
                host.AddServiceEndpoint(typeof(IWorker), binding, address);
                host.Open();
                Assert.DoesNotThrow(() => lb.TurnOnWorker());
                Assert.DoesNotThrow(() => lb.TurnOnWorker());
                Assert.DoesNotThrow(() => lb.TurnOnWorker());
                Assert.AreEqual(4, LBImplement.Brojac);
                host.Close();
            }
        }

        [Test]
        public void TurnOffTestEx()
        {
            LBImplement.Brojac = 1;
            LBImplement lb = new LBImplement();

            Assert.Throws<FaultException<CustomException>>(() => lb.TurnOffWorker());
            Assert.AreEqual(LBImplement.Brojac, 1);
        }
        [Test]
        public void TurnOffTestNoEx()
        {
            LBImplement.Brojac = 4;
            LBImplement lb = new LBImplement();
            using (ServiceHost host = new ServiceHost(typeof(WorkerImplement)))
            {
                string address = "net.tcp://localhost:5000/IWorker";
                NetTcpBinding binding = new NetTcpBinding();
                host.AddServiceEndpoint(typeof(IWorker), binding, address);
                host.Open();
                Assert.DoesNotThrow(() => lb.TurnOffWorker());
                Assert.DoesNotThrow(() => lb.TurnOffWorker());
                Assert.DoesNotThrow(() => lb.TurnOffWorker());
                Assert.AreEqual(1, LBImplement.Brojac);
                host.Close();
            }
        }
    }
}
