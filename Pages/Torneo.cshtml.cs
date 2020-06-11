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
using Microsoft.AspNetCore.Razor.Language;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization;

namespace TorneoJudo.Pages
{
    public class TorneoModel : PageModel
    {
        private readonly TorneoJudo.Data.AtletaContext _context;

        public TorneoModel(TorneoJudo.Data.AtletaContext context)
        {
            _context = context;
        }

        public int AvvioTorneo(int n)
        {
            for (int j = 0; j < n / 2; j++)
            {
                Random r = new Random();
                double handicapSconfitto;

                if (incontri[j].atleta1.cintura < incontri[j].atleta2.cintura * 0.75)
                {
                    handicapSconfitto = 0.75;
                    if (r.Next(1, 100) > 25)
                        incontri[j].vincitore = incontri[j].atleta2;
                    else
                        incontri[j].vincitore = incontri[j].atleta2;
                }

                else if (incontri[j].atleta1.cintura < incontri[j].atleta2.cintura * 0.50)
                {
                    handicapSconfitto = 0.50;
                    if (r.Next(1, 100) > 75)
                        incontri[j].vincitore = incontri[j].atleta2;
                    else
                        incontri[j].vincitore = incontri[j].atleta1;
                }

                else if (incontri[j].atleta1.cintura < incontri[j].atleta2.cintura * 0.25)
                {
                    handicapSconfitto = 0.25;
                    incontri[j].vincitore = incontri[j].atleta2;
                }

                else
                {
                    handicapSconfitto = 0.50;
                    if (r.Next(1, 100) > 50)
                        incontri[j].vincitore = incontri[j].atleta2;
                    else
                        incontri[j].vincitore = incontri[j].atleta1;
                }

                int min = r.Next(0, 10);
                int sec = min == 0 ? r.Next(7, 60) : r.Next(0, 60);
                if (sec >= 10) incontri[j].tempo = min + ":" + sec;
                else { incontri[j].tempo = min + ":" + sec.ToString().PadLeft(2, '0'); }

                if (r.Next(0,100) < 30) { incontri[j].shido1 = 1; }
                else if(r.Next(0,100)< 15) { incontri[j].shido1 = 2; }
                else if(r.Next(0,100)< 5) { incontri[j].shido1 = 3; incontri[j].vincitore = incontri[j].atleta2; }

                if (r.Next(0, 100) < 30) { incontri[j].shido2 = 1; }
                else if (r.Next(0, 100) < 15) { incontri[j].shido2 = 2; }
                else if (r.Next(0, 100) < 5) { incontri[j].shido2 = 3; incontri[j].vincitore = incontri[j].atleta1; }   

                incontri[j].punteggioVincitore = r.Next(0, 100);
                incontri[j].punteggioSconfitto = (int)(r.Next(0, incontri[j].punteggioVincitore-1));

                storicoIncontri.Add(incontri[j]);
            }

            if (n == 2)
                return 0;
            else
            {
                IList<Incontro> vincitori = new List<Incontro>();

                for (int h = 0; h < incontri.Count(); h += 2)
                {
                    vincitori.Add(new Incontro(incontri[h].vincitore, incontri[h + 1].vincitore));
                }

                incontri.Clear();
                incontri = new List<Incontro>(vincitori);
                return AvvioTorneo(n / 2);
            }

        }

        [BindProperty]
        public string userid { get; set; }

        public IList<Incontro> incontri { get; set; }

        [BindProperty]
        public IList<Incontro> storicoIncontri { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (User.Identity.IsAuthenticated && User.FindFirst(ClaimTypes.NameIdentifier).Value == "admin")
            {
                Atleta = await _context.Atleta.ToListAsync();

                if (IsPowerOfTwo((ulong)Atleta.Count()) == false)
                {
                    return RedirectToPage("./ErrorePotenza2");
                }

                IListExtensions.Shuffle(Atleta);

                incontri = new List<Incontro>();

                for (int i = 0; i < Atleta.Count(); i += 2)
                    incontri.Add(new Incontro(Atleta[i], Atleta[i + 1]));

                storicoIncontri = new List<Incontro>();

                AvvioTorneo(Atleta.Count());

                _context.Atleta.Where(a => a.id == storicoIncontri[storicoIncontri.Count() - 1].vincitore.id).FirstOrDefault().vittorie++;

                await _context.SaveChangesAsync();
                return Page();
            }
            else { return RedirectToPage("Index"); }
        }

        public IList<Atleta> Atleta { get; set; } 

        public bool IsPowerOfTwo(ulong x)
        {
            return (x & (x - 1)) == 0;
        }
    }

    public static class IListExtensions
    {
        public static void Shuffle<T>(this IList<T> ts)
        {
            var count = ts.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i)
            {
                Random rnd = new Random();
                var r = rnd.Next(i, count);
                var tmp = ts[i];
                ts[i] = ts[r];
                ts[r] = tmp;
            }
        }
    }
}