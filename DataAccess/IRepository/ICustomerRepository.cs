using BusinessObject.DTO;

namespace DataAccess.IRepository
{
    public interface ICustomerRepository
    {
        List<CustomerDTO> GetCustomer();
        CustomerDTO GetCustomerById(string id);
        void CreateCustomer(CustomerDTO Customer);
        void UpdateCustomer(CustomerDTO Customer);
        void DeleteCustomer(CustomerDTO Customer);
    }
}
