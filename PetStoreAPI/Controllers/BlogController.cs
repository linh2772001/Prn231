using BusinessObject.DTO;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PetStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private IBlogRepository repository;
        public BlogController(IBlogRepository repo)
        {
            repository = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BlogDTO>> GetBlog() => repository.GetBlogs();

        [HttpGet("id")]
        public ActionResult<BlogDTO> GetBlogById(int id) => repository.GetBlogById(id);
        [HttpGet("name")]
        public ActionResult<IEnumerable<BlogDTO>> GetBlogByTitle(string name) => repository.GetBlogByTitle(name);

        [HttpPost]
        public IActionResult CreateBlog(BlogDTO blog)
        {
            repository.CreateBlog(blog);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteBlog(int id)
        {
            var Blog = repository.GetBlogById(id);
            if (Blog == null) return NotFound();
            repository.DeleteBlog(Blog);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateBlog(int id, BlogDTO blog)
        {
            var Blog = repository.GetBlogById(id);
            if (Blog == null) return NotFound();
            blog.BlogId = id;
            repository.UpdateBlog(blog);
            return NoContent();
        }
    }
}
