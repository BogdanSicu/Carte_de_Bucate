using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carte_de_bucate.DTOs
{
    public class TaraDTO
    {
        public int Id { get; set; }
        public string DenumireTara { get; set; }

        public List<MancareTaraDTO> Mancaruri { get; set; }
    }
}
