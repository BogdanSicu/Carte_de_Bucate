using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carte_de_bucate.DTOs
{
    public class IngredientReadDTO
    {
        public int Id { get; set; }
        public string Denumire { get; set; }
        public List<MancareAddDTO> ListaMancaruri { get; set; }
    }
}
