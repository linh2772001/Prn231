using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class BlogDAO
    {
        public static List<BlogDetail> GetBlogs()
        {
            var listBlogs = new List<BlogDetail>();
            try
            {
                using (var context = new ProjectContext())
                {
                    listBlogs = context.BlogDetails.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listBlogs;
        }

        public static BlogDetail GetBlogById(int id)
        {
            var Blog = new BlogDetail();
            try
            {
                using (var context = new ProjectContext())
                {
                    Blog = context.BlogDetails.SingleOrDefault(p => p.BlogId == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Blog;
        }
        public static List<BlogDetail> GetBlogsByTitle(string Title)
        {
            List<BlogDetail> blogs = new List<BlogDetail>();
            try
            {
                using (var context = new ProjectContext())
                {
                    // Perform a case-insensitive and partial search
                    blogs = context.BlogDetails.Where(p => p.PageTitle.ToLower().Contains(Title.ToLower())).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return blogs;
        }



        public static void CreateBlog(BlogDetail blogDetail)
        {
            try
            {
                using (var context = new ProjectContext())
                {
                    context.BlogDetails.Add(blogDetail);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateBlog(BlogDetail blogDetail)
        {
            try
            {
                using (var context = new ProjectContext())
                {
                    context.Entry(blogDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteBlog(BlogDetail blogDetail)
        {
            try
            {
                using (var context = new ProjectContext())
                {
                    var u = context.BlogDetails.SingleOrDefault(c => c.BlogId == blogDetail.BlogId);
                    context.BlogDetails.Remove(u);
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

