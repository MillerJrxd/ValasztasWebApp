using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Valasztas.Models;

namespace Valasztas.Pages
{
    public class FajlFeltoltesModel : PageModel
    {
        private readonly IWebHostEnvironment _env;
        private readonly ValasztasDbContext _ctx;

        public FajlFeltoltesModel(IWebHostEnvironment env, ValasztasDbContext ctx)
        {
            _ctx = ctx;
            _env = env;
        }

        [BindProperty]
        public IFormFile Feltoltes { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            var uploadFilePath = Path.Combine(_env.ContentRootPath, 
                "uploads", Feltoltes.FileName);

            if (uploadFilePath != null)
            {
                using (var stream = new FileStream(uploadFilePath, FileMode.Create))
                {
                    await Feltoltes.CopyToAsync(stream);
                }
            }
            StreamReader sr = new StreamReader(uploadFilePath);

            sr.Close();

            return Page();
        }
    }
}
