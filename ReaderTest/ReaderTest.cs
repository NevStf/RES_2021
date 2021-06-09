using NUnit.Framework;
using System;
using Reader;
using Contracts.Resources;
using System.Collections.Generic;
using Contracts;
using System.ServiceModel;
using System.IO;

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
            Assert.Throws<FaultException<CustomException>>(() => r.WriteElements(l));
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

        //reference za bacanje exceptiona: https://gist.github.com/asierba/ad9978c8b548f3fcef40 i https://gist.github.com/asierba/3f51a7b82011bd171299fae307580cd8?fbclid=IwAR0AvW9gQhCsyIaH3cr0guNLg-KMyMdoILa6hY8CKGfqjdAIAphk7Q8g85w
        [Test]

        public void InputTestThrowsExID()
        {
            ReaderClass r = new ReaderClass();

            var output = new StringWriter();
            Console.SetOut(output);

            var id = new StringReader("-1");
            Console.SetIn(id);

            Assert.Throws<FaultException<CustomException>>(() => r.Input());
        }

        [Test]
        public void InputTestThrowsExCode()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            ReaderClass r = new ReaderClass();
            var input = new StringReader(@"1
                                           9");


            Console.SetIn(input);
           
            Assert.Throws<FaultException<CustomException>>(() => r.Input());
        }

        [Test]
        public void InputTestThrowsExStartDT()
        {
            ReaderClass r = new ReaderClass();
            var input = new StringReader(@"1
                                           6
                                           32112");


            Console.SetIn(input);

            Assert.Throws<FaultException<CustomException>>(() => r.Input());
        }

        [Test]
        public void InputTestThrowsExEndDT()
        {
            ReaderClass r = new ReaderClass();
            var input = new StringReader(@"1
                                           6
                                           08-Jun-21 2:31:00 PM
                                           52454111s");


            Console.SetIn(input);

            Assert.Throws<FaultException<CustomException>>(() => r.Input());
        }

        [Test]
        public void InputTestDoesNotThrowEx()
        {
            ReaderClass r = new ReaderClass();

            var output = new StringWriter();
            Console.SetOut(output);

            var input = new StringReader(@"1
                                           3
                                           08-Jun-21 2:31:00 PM
                                           08-Jun-21 2:41:00 PM");

            Console.SetIn(input);

            Assert.DoesNotThrow(() => r.Input());
            Assert.AreEqual(r.ID.ToString(), "1");
            Assert.AreEqual(r.Code.ToString(), "3");
            Assert.AreEqual(r.StartDT.ToString(), "08-Jun-21 2:31:00 PM");
            Assert.AreEqual(r.EndDT.ToString(), "08-Jun-21 2:41:00 PM");

            //var c = new StringReader("1");
            //Console.SetIn(c);

            //Assert.DoesNotThrow(() => r.Input());

            ////var output1 = new StringWriter();
            ////Console.SetOut(output1);

            //var output2 = new StringWriter();
            //Console.SetOut(output2);
            //var startdt = new StringReader("08-Jun-21 2:31:00 PM");
            //Console.SetIn(startdt);

            //var output3 = new StringWriter();
            //Console.SetOut(output3);
            //var enddt = new StringReader("08-Jun-21 2:41:00 PM");
            //Console.SetIn(enddt);
        }



        [Test]
        [TestCase(2, Codes.CODE_DIGITAL, null, null)]
        public void ReaderKonstruktorTest(int id, int c, DateTime dt1, DateTime dt2)
        {
            dt1 = new DateTime(2021, 06, 08, 15, 01, 00);
            dt2 = new DateTime(2021, 06, 08, 16, 01, 00);
            ReaderClass r = new ReaderClass(id, c, dt1, dt2);
            //r.ID = 1;
            //r.Code = (int)Codes.CODE_LIMITSET;


            Assert.AreEqual(r.ID, id);
            Assert.AreEqual(r.Code, c);
            Assert.AreEqual(r.StartDT, dt1);
            Assert.AreEqual(r.EndDT, dt2);
        }


    }
}
