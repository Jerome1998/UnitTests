using FakeItEasy;
using NUnit.Framework;
using UnitTests.Mocking;

namespace UnitTests.Tests.Mocking
{
    [TestFixture]
    public class ProductTests
    {
        [Test]
        public void GetPrice_GoldCustomer_Apply30PercentDiscount()
        {
            var product = new Product { ListPrice = 100 };
            var result = product.GetPrice(new Customer { IsGold = true });

            Assert.That(result, Is.EqualTo(70));
        }

        [Test]
        public void GetPrice_GoldCustomer_Apply30PercentDiscount2()
        {
            var customer = new Fake<ICustomer>();
            customer.CallsTo(c => c.IsGold).Returns(true);

            var product = new Product { ListPrice = 100 };
            var result = product.GetPrice(customer.FakedObject);

            Assert.That(result, Is.EqualTo(70));
        }
    }
}