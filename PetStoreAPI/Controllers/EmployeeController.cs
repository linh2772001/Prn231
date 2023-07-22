using BusinessObject.DTO;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace PetStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private IEmployeeRepository repository;
        public EmployeeController(IEmployeeRepository repo)
        {
            repository = repo;
        }
        [HttpGet]
        public ActionResult<IEnumerable<EmployeeDTO>> GetEmployees() => repository.GetEmployee();

        [HttpGet("id")]
        public ActionResult<EmployeeDTO> GetEmployeeById(string id) => repository.GetEmployeeById(id);

        [HttpPost]
        public IActionResult CreateAccount(EmployeeDTO Employee)
        {
            repository.CreateEmployee(Employee);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteEmployee(string id)
        {
            var Employee = repository.GetEmployeeById(id);
            if (Employee == null) return NotFound();
            repository.DeleteEmployee(Employee);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateAccount(string id, EmployeeDTO employee)
        {
            var Employee = repository.GetEmployeeById(id);
            if (Employee == null) return NotFound();
            employee.EmployeeId = id;
            repository.UpdateEmployee(employee);
            return NoContent();
        }
    }
}
