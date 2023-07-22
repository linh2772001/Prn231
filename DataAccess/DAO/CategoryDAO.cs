using BusinessObject.Models;

namespace DataAccess.DAO
{
    public class CategoryDAO
    {
        public static List<Category> GetCategory()
        {
            var listCategorys = new List<Category>();
            try
            {
                using (var context = new ProjectContext())
                {
                    listCategorys = context.Categories.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listCategorys;
        }

        public static Category GetCategoryById(int id)
        {
            var Category = new Category();
            try
            {
                using (var context = new ProjectContext())
                {
                    Category = context.Categories.SingleOrDefault(p => p.CategoryId == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Category;
        }

        public static void CreateCategory(Category Category)
        {
            try
            {
                using (var context = new ProjectContext())
                {
                    context.Categories.Add(Category);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateCategory(Category Category)
        {
            try
            {
                using (var context = new ProjectContext())
                {
                    context.Entry(Category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteCategory(Category Category)
        {
            try
            {
                using (var context = new ProjectContext())
                {
                    var u = context.Categories.SingleOrDefault(c => c.CategoryId == Category.CategoryId);
                    context.Categories.Remove(u);
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
