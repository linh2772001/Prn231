using BusinessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IBlogRepository
    {
        List<BlogDTO> GetBlogs();
        List<BlogDTO> GetBlogByTitle(string name);
        BlogDTO GetBlogById(int id);
        void CreateBlog(BlogDTO blog);
        void UpdateBlog(BlogDTO blog);
        void DeleteBlog(BlogDTO blog);
    }
}
