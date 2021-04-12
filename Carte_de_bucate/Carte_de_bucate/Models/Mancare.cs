
using System.Collections.Generic;

namespace Carte_de_bucate.Models
{
    public class Mancare
    {
        public int Id { get; set; }
        public string NameFood { get; set; }
        public string ModPreparare { get; set; }
        public int TimpPreparare { get;set; }

        // Relations
        public int TaraID { get; set; }
        public virtual Tari Tara { get; set; }
        public virtual ICollection<IngredienteInRetete> ListaIngrediente { get; set; }
    }
}
