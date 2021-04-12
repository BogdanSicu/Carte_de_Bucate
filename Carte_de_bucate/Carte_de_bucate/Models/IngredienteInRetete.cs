using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carte_de_bucate.Models
{
    public class IngredienteInRetete
    {
        public int MancareID { get; set; }
        public Mancare Mancare { get; set; }


        public int IngrediendID { get; set; }
        public Ingrediente Ingredient { get; set; }

    }
}
