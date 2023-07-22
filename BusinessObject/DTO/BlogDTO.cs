using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class BlogDTO
    {
        public int BlogId { get; set; }
        public string? Content { get; set; }
        public string? Heading { get; set; }
        public string? PageTitle { get; set; }
        public string? ShortDescription { get; set; }
        public string? FeaturedImageUrl { get; set; }
        public DateTime? PublishedDate { get; set; }
    }
}
