using AutoFixture;
using FakeItEasy;
using NUnit.Framework;
using UnitTests.Fundamentals;

namespace UnitTests.Tests.Mocking
{
    [TestFixture]
    public class CustomerControllerTests
    {
        private CustomerController controller;
        private Fake<ICustomerRepository> customerRepositiory;

        [SetUp]
        public void Setup()
        {
            customerRepositiory = new Fake<ICustomerRepository>();
            controller = new CustomerController(customerRepositiory.FakedObject);
        }

        [Test]
        public void GetCstomer_CustomerExists_ReturnsOkWithCustomer()
        {
            var fixture = new Fixture();
            var customer = fixture.Create<Customer>();
            customerRepositiory.CallsTo(x => x.GetCustomer(A<int>.Ignored)).Returns(customer);

            var result = controller.GetCustomer(42);

            Assert.That(result, Is.TypeOf<Ok>());
            Assert.That((result as Ok).Data, Is.TypeOf<Customer>());
        }

        [Test]
        public void GetCustomer_CustomerNotExists_ReturnsNotFound()
        {
            customerRepositiory.CallsTo(x => x.GetCustomer(A<int>.Ignored)).Returns(null);

            var result = controller.GetCustomer(42);

            Assert.That(result, Is.TypeOf<NotFound>());
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-100)]
        public void GetCustomer_IdOutOfRange_ReturnsNotFound(int id)
        {
            var result = controller.GetCustomer(id);

            Assert.That(result, Is.TypeOf<NotFound>());
        }
    }
}
