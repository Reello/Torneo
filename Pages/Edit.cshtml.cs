using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TorneoJudo.Data;
using TorneoJudo.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TorneoJudo.Pages
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly TorneoJudo.Data.AtletaContext _context;

        public EditModel(TorneoJudo.Data.AtletaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Atleta Atleta { get; set; }

        [BindProperty]
        public string userid { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            Atleta = await _context.Atleta.FirstOrDefaultAsync(m => m.id == id);

            if (Atleta.foreignKeySquadra != User.FindFirst(ClaimTypes.NameIdentifier).Value && userid != "admin")
                return RedirectToPage("./Index");

            if (Atleta == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            Atleta.foreignKeySquadra = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            _context.Attach(Atleta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AtletaExists(Atleta.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AtletaExists(string id)
        {
            return _context.Atleta.Any(e => e.id == id);
        }
    }
}
