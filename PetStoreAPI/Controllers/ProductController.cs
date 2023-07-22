using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace PetStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> GetProducts() => repository.GetProducts();

        [HttpGet("id")]
        public ActionResult<ProductDTO> GetProductById(int id) => repository.GetProductById(id);

        [HttpPost]
        public IActionResult CreateAccount(ProductDTO product)
        {
            repository.CreateProduct(product);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteProduct(int id)
        {
            var Product = repository.GetProductById(id);
            if (Product == null) return NotFound();
            repository.DeleteProduct(Product);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateAccount(int id, ProductDTO product)
        {
            var Product = repository.GetProductById(id);
            if (Product == null) return NotFound();
            product.ProductId = id;
            repository.UpdateProduct(product);
            return NoContent();
        }
    }
}
