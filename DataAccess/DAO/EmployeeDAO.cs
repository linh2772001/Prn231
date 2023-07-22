using BusinessObject.Models;

namespace DataAccess.DAO
{
    public class EmployeeDAO
    {
        public static List<Employee> GetEmployee()
        {
            var listEmployees = new List<Employee>();
            try
            {
                using (var context = new ProjectContext())
                {
                    listEmployees = context.Employees.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listEmployees;
        }

        public static Employee GetEmployeeById(string id)
        {
            var Employee = new Employee();
            try
            {
                using (var context = new ProjectContext())
                {
                    Employee = context.Employees.SingleOrDefault(p => p.EmployeeId == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Employee;
        }

        public static void CreateEmployee(Employee Employee)
        {
            try
            {
                using (var context = new ProjectContext())
                {
                    context.Employees.Add(Employee);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateEmployee(Employee Employee)
        {
            try
            {
                using (var context = new ProjectContext())
                {
                    context.Entry(Employee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteEmployee(Employee Employee)
        {
            try
            {
                using (var context = new ProjectContext())
                {
                    var u = context.Employees.SingleOrDefault(c => c.EmployeeId == Employee.EmployeeId);
                    context.Employees.Remove(u);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
