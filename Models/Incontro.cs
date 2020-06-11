using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace TorneoJudo.Models
{
    public class Incontro
    {
        public Incontro(Atleta Atleta1, Atleta Atleta2)
        {
            atleta1 = Atleta1;
            atleta2 = Atleta2;
        }        

        public string id { get; set; }
        public Atleta atleta1 { get; set; }
        public Atleta atleta2 { get; set; }
        public Atleta vincitore { get; set; }
        public string tempo { get; set; }
        public int punteggioVincitore { get; set; }
        public int punteggioSconfitto { get; set; }
        public int shido1 { get; set; }
        public int shido2 { get; set; }
        string motivazione { get; set; }
    }
}
