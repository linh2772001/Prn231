using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BlogRepository: IBlogRepository
    {
        private readonly IMapper _mapper;

        public BlogRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void CreateBlog(BlogDTO blog)
        {
            var blogs = _mapper.Map<BlogDetail>(blog);
            BlogDAO.CreateBlog(blogs);
        }

        public void DeleteBlog(BlogDTO blog)
        {
            var blogs = _mapper.Map<BlogDetail>(blog);
            BlogDAO.DeleteBlog(blogs);
        }

        public List<BlogDTO> GetBlogs()
        {
            return _mapper.Map<List<BlogDTO>>(BlogDAO.GetBlogs());
        }

        public BlogDTO GetBlogById(int id)
        {
            return _mapper.Map<BlogDTO>(BlogDAO.GetBlogById(id));
        }
        public List<BlogDTO> GetBlogByTitle(string name)
        {
            return _mapper.Map<List<BlogDTO>>(BlogDAO.GetBlogsByTitle(name));

        }

        public void UpdateBlog(BlogDTO blog)
        {
            var blogs = _mapper.Map<BlogDetail>(blog);
            BlogDAO.UpdateBlog(blogs);
        }
    }
}

