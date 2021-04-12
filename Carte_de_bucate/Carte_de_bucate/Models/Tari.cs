using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carte_de_bucate.Models
{
    public class Tari
    {
        public int Id { get; set; }
        public string DenumireTara { get; set; }

        // Relations
        public virtual ICollection<Mancare> ListaMancaruri { get; set; }
    }
}
