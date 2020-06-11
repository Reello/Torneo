using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TorneoJudo.Data;
using TorneoJudo.Models;
using System.Security.Claims;

namespace TorneoJudo.Pages.CRUDatleti
{
    public class IndexModel : PageModel
    {
        private readonly TorneoJudo.Data.AtletaContext _context;

        public IndexModel(TorneoJudo.Data.AtletaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string userid { get; set; }

        public async Task OnGetAsync()
        {
            if (User.Identity.IsAuthenticated)
                userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Atleta = await _context.Atleta.ToListAsync();
        }

        public bool IsPowerOfTwo(ulong x)
        {
            return (x & (x - 1)) == 0;
        }

        public IList<Atleta> Atleta { get;set; }

    }
}
