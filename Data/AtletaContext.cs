using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TorneoJudo.Models;

namespace TorneoJudo.Data
{
    public class AtletaContext : DbContext
    {
        public AtletaContext (DbContextOptions<AtletaContext> options)
            : base(options)
        {
        }

        public DbSet<TorneoJudo.Models.Atleta> Atleta { get; set; }
    }
}
