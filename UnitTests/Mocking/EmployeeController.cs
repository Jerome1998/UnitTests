using Microsoft.EntityFrameworkCore;

namespace UnitTests.Mocking
{
    public class EmployeeController
    {
        private readonly IEmployeeStorage _storage;

        public EmployeeController(IEmployeeStorage storage)
        {
            _storage = storage;
        }

        public ActionResult GetEmployee(int id)
        {
            var employee = _storage.GetEmployee(id);

            return new OkResult(employee);
        }

        public ActionResult DeleteEmployee(int id)
        {
            _storage.DeleteEmployee(id);

            return RedirectToAction("Employees");
        }

        private ActionResult RedirectToAction(string employees)
        {
            return new RedirectResult();
        }
    }

    public class ActionResult { }

    public class RedirectResult : ActionResult { }

    public class OkResult : ActionResult 
    {
        public OkResult()
        { }

        public OkResult(object data)
        {
            this.Data = data;
        }

        public object Data { get; set; }
    }

    public class EmployeeContext
    {
        public DbSet<Employee> Employees { get; set; }

        public void SaveChanges()
        {
        }
    }

    public class Employee
    {
    }
}