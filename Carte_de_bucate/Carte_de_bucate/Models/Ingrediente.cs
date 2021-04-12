using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carte_de_bucate.Models
{
    public class Ingrediente
    {
        public int Id { get; set; }
        public string Denumire { get; set; }
        public virtual ICollection<IngredienteInRetete> ListaRetete { get; set; }
    }
}
