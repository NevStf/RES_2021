using Contracts.Resources;
using NUnit.Framework;
using System;

namespace ContractsTest
{
    [TestFixture]
    public class DescriptionTest
    {
        [Test]
        [TestCase(3, 2)]
        public void DescreptionKonstruktorTest(int id, int ds)
        {
            Description desc = new Description(id, ds);

            Assert.AreEqual(desc.ID, id);
            Assert.AreEqual(desc.DataSet, ds);
           // Assert.IsNotNull(desc.Items); <- prolazi i ovo 
            Assert.IsEmpty(desc.Items);
        }
    }
}
