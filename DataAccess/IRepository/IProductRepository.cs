using BusinessObject.DTO;

namespace DataAccess.IRepository
{
    public interface IProductRepository
    {
        List<ProductDTO> GetProducts();
        ProductDTO GetProductById(int id);
        void CreateProduct(ProductDTO product);
        void UpdateProduct(ProductDTO product);
        void DeleteProduct(ProductDTO product);
    }
}
