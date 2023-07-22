using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.IRepository;

namespace DataAccess.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IMapper _mapper;

        public EmployeeRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void CreateEmployee(EmployeeDTO Employee)
        {
            var employees = _mapper.Map<Employee>(Employee);
            EmployeeDAO.CreateEmployee(employees);
        }

        public void DeleteEmployee(EmployeeDTO Employee)
        {
            var employees = _mapper.Map<Employee>(Employee);
            EmployeeDAO.DeleteEmployee(employees);
        }

        public List<EmployeeDTO> GetEmployee()
        {
            return _mapper.Map<List<EmployeeDTO>>(EmployeeDAO.GetEmployee());
        }

        public EmployeeDTO GetEmployeeById(string id)
        {
            return _mapper.Map<EmployeeDTO>(EmployeeDAO.GetEmployeeById(id));
        }

        public void UpdateEmployee(EmployeeDTO Employee)
        {
            var employees = _mapper.Map<Employee>(Employee);
            EmployeeDAO.UpdateEmployee(employees);
        }
    }
}
