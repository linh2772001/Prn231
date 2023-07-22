using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.IRepository;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;

        public ProductRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void CreateProduct(ProductDTO product)
        {
            var products = _mapper.Map<Product>(product);
            ProductDAO.CreateProduct(products);
        }

        public void DeleteProduct(ProductDTO product)
        {
            var products = _mapper.Map<Product>(product);
            ProductDAO.DeleteProduct(products);
        }

        public List<ProductDTO> GetProducts()
        {
            return _mapper.Map<List<ProductDTO>>(ProductDAO.GetProducts());
        }

        public ProductDTO GetProductById(int id)
        {
            return _mapper.Map<ProductDTO>(ProductDAO.GetProductById(id));
        }

        public void UpdateProduct(ProductDTO product)
        {
            var products = _mapper.Map<Product>(product);
            ProductDAO.UpdateProduct(products);
        }
    }
}
