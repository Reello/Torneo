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

namespace TorneoJudo.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly TorneoJudo.Data.AtletaContext _context;
        private readonly ApplicationDbContext _contextUtente;

        public DetailsModel(TorneoJudo.Data.AtletaContext context, ApplicationDbContext contextUtente)
        {
            _context = context;
            _contextUtente = contextUtente;
        }

        public Atleta Atleta { get; set; }

        [BindProperty]
        public string societaAppartenenza { get; set; }

        [BindProperty]
        public string userid { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if(User.Identity.IsAuthenticated)
            userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (id == null)
            {
                return NotFound();
            }

            Atleta = await _context.Atleta.FirstOrDefaultAsync(m => m.id == id);
            societaAppartenenza = _contextUtente.Users.Where(a => a.Id == Atleta.foreignKeySquadra).FirstOrDefault().Email;

            if (Atleta == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
