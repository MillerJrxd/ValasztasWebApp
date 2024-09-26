using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Valasztas.Models;

namespace Valasztas.Pages
{
    public class JeloltekLekerdezeseModel : PageModel
    {
        private readonly Valasztas.Models.ValasztasDbContext _context;

        public JeloltekLekerdezeseModel(Valasztas.Models.ValasztasDbContext context)
        {
            _context = context;
        }

        public IList<Jelolt> Jelolt { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Jelolt = await _context.Jeloltek.ToListAsync();
        }
    }
}
