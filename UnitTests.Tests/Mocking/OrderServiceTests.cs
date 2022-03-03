using FakeItEasy;
using NUnit.Framework;
using UnitTests.Mocking;

namespace UnitTests.Tests.Mocking
{
    [TestFixture]
    public class OrderServiceTests
    {
        [Test]
        public void PlaceOrder_WhenCalled_StoreTheOrder()
        {
            var storage = new Fake<IStorage>();
            var service = new OrderService(storage.FakedObject);

            var order = new Order();
            service.PlaceOrder(order);

            storage.CallsTo(s => s.Store(order)).MustHaveHappened();
        }
    }
}