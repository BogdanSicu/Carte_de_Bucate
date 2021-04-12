using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Carte_de_bucate.DTOs
{
    public class TaraPutPostDTO
    {
        [Required]
        public string Denumire_tara { get; set; }
    }
}
