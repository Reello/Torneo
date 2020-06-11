using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TorneoJudo.Models
{
    public class Atleta
    {
        public string id { get; set; }
        [Required]
        public string nome { get; set; }
        public string cognome { get; set; }
        public string foreignKeySquadra { get; set; }
        public char sesso { get; set; }
        public int peso { get; set; }
        public int cintura { get; set; }
        public int vittorie { get; set; }
    }
}
