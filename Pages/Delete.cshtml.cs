using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TorneoJudo.Data;
using TorneoJudo.Models;
using System.Security.Claims;

namespace TorneoJudo.Pages
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly TorneoJudo.Data.AtletaContext _context;

        public DeleteModel(TorneoJudo.Data.AtletaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Atleta Atleta { get; set; }

        public string userid { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (id == null)
            {
                return NotFound();
            }

            Atleta = await _context.Atleta.FirstOrDefaultAsync(m => m.id == id);

            if (Atleta == null)
            {
                return NotFound();
            }

            if (userid != Atleta.foreignKeySquadra && userid != "admin") { return RedirectToPage("./Index"); }
            return Page();
            
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Atleta = await _context.Atleta.FindAsync(id);

            if (Atleta != null)
            {
                _context.Atleta.Remove(Atleta);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
