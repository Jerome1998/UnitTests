using FakeItEasy;
using NUnit.Framework;
using UnitTests.Mocking;

namespace UnitTests.Tests.Mocking
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        [Test]
        public void GetEmployee_WhenCalled_ReturnsEmployeeFromDb1()
        {
            var storage = A.Fake<IEmployeeStorage>();
            A.CallTo(() => storage.GetEmployee(A<int>.Ignored)).Returns(new Employee());
            var controller = new EmployeeController(storage);

            var result = controller.GetEmployee(42);

            Assert.That(result, Is.TypeOf<OkResult>());
            Assert.That((result as OkResult).Data, Is.TypeOf<Employee>());
        }

        [Test]
        public void GetEmployee_WhenCalled_ReturnsEmployeeFromDb2()
        {
            var storage = new Fake<IEmployeeStorage>();
            storage.CallsTo(storage => storage.GetEmployee(A<int>.Ignored)).Returns(new Employee());
            var controller = new EmployeeController(storage.FakedObject);

            var result = controller.GetEmployee(42);

            Assert.That(result, Is.TypeOf<OkResult>());
            Assert.That((result as OkResult).Data, Is.TypeOf<Employee>());
        }

        [Test]
        public void DeleteEmployee_WhenCalled_DeleteTheEmployeeFromDb1()
        {
            var storage = A.Fake<IEmployeeStorage>();
            var controller = new EmployeeController(storage);

            controller.DeleteEmployee(1);

            A.CallTo(() => storage.DeleteEmployee(1)).MustHaveHappened();
        }

        [Test]
        public void DeleteEmployee_WhenCalled_DeleteTheEmployeeFromDb2()
        {
            var storage = new Fake<IEmployeeStorage>();
            var controller = new EmployeeController(storage.FakedObject);

            controller.DeleteEmployee(1);

            storage.CallsTo(s => s.DeleteEmployee(1)).MustHaveHappened();
        }
    }
}