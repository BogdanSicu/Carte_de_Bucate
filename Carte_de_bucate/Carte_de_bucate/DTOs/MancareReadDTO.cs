using Carte_de_bucate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carte_de_bucate.DTOs
{
    public class MancareReadDTO
    {
        public int Id { get; set; }
        public string NameFood { get; set; }
        public string ModPreparare { get; set; }
        public int TimpPreparare { get; set; }
        
        public List<IngredienteMancareDTO> ListaIngrediente { get; set; }

        public TaraMancareDTO Tara { get; set; }
    }
}
