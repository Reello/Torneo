using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TorneoJudo.Data;
using TorneoJudo.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace TorneoJudo.Pages
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly TorneoJudo.Data.AtletaContext _context;


        public CreateModel(TorneoJudo.Data.AtletaContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            try
            {
                if (Atleta.foreignKeySquadra != User.FindFirst(ClaimTypes.NameIdentifier).Value)
                    return RedirectToPage("./Index");
            }
            catch { return Page(); }
            return Page();
        }

        [BindProperty]
        public Atleta Atleta { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Atleta.foreignKeySquadra = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            _context.Atleta.Add(Atleta);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}