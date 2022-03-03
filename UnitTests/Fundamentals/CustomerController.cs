namespace UnitTests.Fundamentals
{
    public class CustomerController
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public ActionResult GetCustomer(int id)
        {
            if (id <= 0)
                return new NotFound();

            var customer = _customerRepository.GetCustomer(id);

            if (customer is null)
                return new NotFound();

            return new Ok(customer);
        }
    }

    public interface ICustomerRepository
    {
        Customer GetCustomer(int id);
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
    public class ActionResult { }
    
    public class NotFound : ActionResult { }
 
    public class Ok : ActionResult 
    {
        public Ok()
        { }

        public Ok(object data)
        {
            this.Data = data;
        }

        public object Data { get; }
    }
}