using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
/*using PetShop_Project_SWP391.Models;*/

namespace PetShop_Project_SWP391.Pages.GalleryPic
{
    public class GalleryModel : PageModel
    {
        private readonly ProjectContext _context;

        public GalleryModel(ProjectContext context)
        {
            _context = context;
        }

        public IList<PictureGallery> Pictures { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Pictures = await _context.PictureGalleries.ToListAsync();
            return Page();
        }
    }
}
