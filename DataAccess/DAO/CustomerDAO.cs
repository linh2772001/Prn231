using BusinessObject.Models;

namespace DataAccess.DAO
{
    public class CustomerDAO
    {
        public static List<Customer> GetCustomer()
        {
            var listCustomers = new List<Customer>();
            try
            {
                using (var context = new ProjectContext())
                {
                    listCustomers = context.Customers.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listCustomers;
        }

        public static Customer GetCustomerById(string id)
        {
            var Customer = new Customer();
            try
            {
                using (var context = new ProjectContext())
                {
                    Customer = context.Customers.SingleOrDefault(p => p.CustomerId == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Customer;
        }

        public static void CreateCustomer(Customer Customer)
        {
            try
            {
                using (var context = new ProjectContext())
                {
                    context.Customers.Add(Customer);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateCustomer(Customer Customer)
        {
            try
            {
                using (var context = new ProjectContext())
                {
                    context.Entry(Customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteCustomer(Customer Customer)
        {
            try
            {
                using (var context = new ProjectContext())
                {
                    var u = context.Customers.SingleOrDefault(c => c.CustomerId == Customer.CustomerId);
                    context.Customers.Remove(u);
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
