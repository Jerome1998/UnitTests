using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.AntiTests;

namespace UnitTests.Tests.AntiTests
{
    [TestFixture]
    public class RepositoryTest
    {
        private Repository db = new Repository();

        [Test]
        public void Add_ShouldAddADBObjectToDB()
        {
            var dbObject = new DBObject() { Id = "1",Text="Hello"};

            db.Add(dbObject);
            var result = db.Get(dbObject.Id);

            Assert.That(result, Is.EqualTo(dbObject));
        }

        [Test]
        public void Remove_ShouldRemoveADBObjectFromDB()
        {
            var result = db.Remove("1");

            Assert.That(result, Is.True);
        }
    }
}
