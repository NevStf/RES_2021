using Contracts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractsTest
{
    [TestFixture]
    public class CustomExceptionTest
    {
        [Test]
        [TestCase("TextException")]

        public void ExceptionKonstruktor (string s)
        {
            CustomException ce = new CustomException(s);
            Assert.AreEqual(ce.CMessage, s);
        }
    }
}
