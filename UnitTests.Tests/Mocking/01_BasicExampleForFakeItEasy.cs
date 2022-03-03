using FakeItEasy;
using NUnit.Framework;
using UnitTests.Mocking;

namespace UnitTests.Tests.Mocking
{
    [TestFixture]
    public class EmployeeControllerTests
    {
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