using BusinessObject.DTO;

namespace DataAccess.IRepository
{
    public interface IEmployeeRepository
    {
        List<EmployeeDTO> GetEmployee();
        EmployeeDTO GetEmployeeById(string id);
        void CreateEmployee(EmployeeDTO Employee);
        void UpdateEmployee(EmployeeDTO Employee);
        void DeleteEmployee(EmployeeDTO Employee);
    }
}
