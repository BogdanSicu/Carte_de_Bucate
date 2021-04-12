using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Carte_de_bucate.DTOs
{
    public class MancarePatchModPreparare
    {
        [Required(ErrorMessage = "ModPreparare is required")]
        public string ModPreparare { get; set; }
    }
}
