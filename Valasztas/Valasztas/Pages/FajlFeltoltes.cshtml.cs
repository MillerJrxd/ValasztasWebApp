using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
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
            //_ctx.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
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
            List<Part> partok = new List<Part>();
            while (!sr.EndOfStream) 
            {
                var part = sr.ReadLine().Split()[4];
                if (!partok.Select(x => x.RovidNev).Contains(part))
                {
                    partok.Add(new Part { RovidNev = part });
                }
            }
            sr.Close();
            foreach (var p in partok)
            {
                _ctx.Partok.Add(p);
            }
            sr = new StreamReader(uploadFilePath);
            while (!sr.EndOfStream)
            {
                var sor = sr.ReadLine();
                var elemek = sor.Split();
                Jelolt ujJelolt = new Jelolt();
                ujJelolt.Kerulet = int.Parse(elemek[0]);
                ujJelolt.SzavazatokSzama = int.Parse(elemek[1]);
                ujJelolt.Nev = elemek[2] + " " + elemek[3];
                ujJelolt.PartRovidNev = elemek[4];
                _ctx.Jeloltek.Add(ujJelolt);
            }
            sr.Close();
            _ctx.SaveChanges();

            return Page();
        }
    }
}
