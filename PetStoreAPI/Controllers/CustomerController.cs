using BusinessObject.DTO;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PetStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerRepository repository;
        public CustomerController(ICustomerRepository repo)
        {
            repository = repo;
        }
        [HttpGet]
        public ActionResult<IEnumerable<CustomerDTO>> GetCustomers() => repository.GetCustomer();

        [HttpGet("id")]
        public ActionResult<CustomerDTO> GetCustomerById(string id) => repository.GetCustomerById(id);

        [HttpPost]
        public IActionResult CreateAccount(CustomerDTO Customer)
        {
            repository.CreateCustomer(Customer);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteCustomer(string id)
        {
            var Customer = repository.GetCustomerById(id);
            if (Customer == null) return NotFound();
            repository.DeleteCustomer(Customer);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateAccount(string id, CustomerDTO customer)
        {
            var Customer = repository.GetCustomerById(id);
            if (Customer == null) return NotFound();
            customer.CustomerId = id;
            repository.UpdateCustomer(customer);
            return NoContent();
        }
    }
}
