using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.IRepository;

namespace DataAccess.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMapper _mapper;

        public CustomerRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void CreateCustomer(CustomerDTO Customer)
        {
            var customers = _mapper.Map<Customer>(Customer);
            CustomerDAO.CreateCustomer(customers);
        }

        public void DeleteCustomer(CustomerDTO Customer)
        {
            var customers = _mapper.Map<Customer>(Customer);
            CustomerDAO.DeleteCustomer(customers);
        }

        public List<CustomerDTO> GetCustomer()
        {
            return _mapper.Map<List<CustomerDTO>>(CustomerDAO.GetCustomer());
        }

        public CustomerDTO GetCustomerById(string id)
        {
            return _mapper.Map<CustomerDTO>(CustomerDAO.GetCustomerById(id));
        }

        public void UpdateCustomer(CustomerDTO Customer)
        {
            var customers = _mapper.Map<Customer>(Customer);
            CustomerDAO.UpdateCustomer(customers);
        }
    }
}
