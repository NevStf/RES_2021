using Contracts;
using Contracts.Resources;
using LoadBalancer;
using Moq;
using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;
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

        [Test]
        [TestCase(Codes.CODE_ANALOG, 456.7)]
        [TestCase(Codes.CODE_DIGITAL, 46.7)]
        public void PutItemToListDescriptionDataset1(Codes code, double value) 
        {
            LBImplement lb = new LBImplement();
            lb.InitList();
            Item i = new Item(code, value);
            ListDescription ld = lb.PutItemToListDescription(code, value);

            Assert.AreEqual(ld.ListOfDescription[0].Items.Count, 1);
        }

        [Test]
        [TestCase(Codes.CODE_CUSTOM, 456.7)]
        [TestCase(Codes.CODE_LIMITSET, 46.7)]
        public void PutItemToListDescriptionDataset2(Codes code, double value)
        {
            LBImplement lb = new LBImplement();
            lb.InitList();
            Item i = new Item(code, value);
            ListDescription ld = lb.PutItemToListDescription(code, value);

            Assert.AreEqual(ld.ListOfDescription[1].Items.Count, 1);
        }

        [Test]
        [TestCase(Codes.CODE_SINGLEONE, 456.7)]
        [TestCase(Codes.CODE_MULTIPLEONE, 46.7)]
        public void PutItemToListDescriptionDataset3(Codes code, double value)
        {
            LBImplement lb = new LBImplement();
            lb.InitList();
            Item i = new Item(code, value);
            ListDescription ld = lb.PutItemToListDescription(code, value);

            Assert.AreEqual(ld.ListOfDescription[2].Items.Count, 1);
        }
        [Test]
        [TestCase(Codes.CODE_CONSUMER, 456.7)]
        [TestCase(Codes.CODE_SOURCE, 46.7)]
        public void PutItemToListDescriptionDataset4(Codes code, double value)
        {
            LBImplement lb = new LBImplement();
            lb.InitList();
            Item i = new Item(code, value);
            ListDescription ld = lb.PutItemToListDescription(code, value);

            Assert.AreEqual(ld.ListOfDescription[3].Items.Count, 1);
        }


        [Ignore("Baza")]
        [ExcludeFromCodeCoverage]
        public void DistributeWorkTest()
        {
            ListDescription ld = new ListDescription();
            LBImplement.DistributeCount = 0;
            LBImplement.Brojac = 1;

            LBImplement lb = new LBImplement();

            using (ServiceHost host = new ServiceHost(typeof(WorkerImplement)))
            {
                string address = "net.tcp://localhost:5000/IWorker";
                NetTcpBinding binding = new NetTcpBinding();
                host.AddServiceEndpoint(typeof(IWorker), binding, address);
                host.Open();
                lb.DistributeWork(ld);
                Assert.AreEqual(1, LBImplement.DistributeCount);
                Assert.AreEqual(1, ld.WorkerID);
                host.Close();
            }
        }

        [Ignore("Lancani poziv do baze")]
        [ExcludeFromCodeCoverage]
        public void SendToWorkerTest()
        {
            ListDescription ld = new ListDescription();
            LBImplement lb = new LBImplement();

            using (ServiceHost host = new ServiceHost(typeof(WorkerImplement)))
            {
                string address = "net.tcp://localhost:5000/IWorker";
                NetTcpBinding binding = new NetTcpBinding();
                host.AddServiceEndpoint(typeof(IWorker), binding, address);
                host.Open();
                lb.SendToWorker(ld);
                
                host.Close();
            }
        }
    }
}
