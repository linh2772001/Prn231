using BusinessObject.Models;

namespace DataAccess.DAO
{
    public class ProductDAO
    {
        public static List<Product> GetProducts()
        {
            var listProducts = new List<Product>();
            try
            {
                using (var context = new ProjectContext())
                {
                    listProducts = context.Products.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listProducts;
        }

        public static Product GetProductById(int id)
        {
            var Product = new Product();
            try
            {
                using (var context = new ProjectContext())
                {
                    Product = context.Products.SingleOrDefault(p => p.ProductId == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Product;
        }

        public static void CreateProduct(Product Product)
        {
            try
            {
                using (var context = new ProjectContext())
                {
                    context.Products.Add(Product);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateProduct(Product Product)
        {
            try
            {
                using (var context = new ProjectContext())
                {
                    context.Entry(Product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteProduct(Product Product)
        {
            try
            {
                using (var context = new ProjectContext())
                {
                    var u = context.Products.SingleOrDefault(c => c.ProductId == Product.ProductId);
                    context.Products.Remove(u);
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
