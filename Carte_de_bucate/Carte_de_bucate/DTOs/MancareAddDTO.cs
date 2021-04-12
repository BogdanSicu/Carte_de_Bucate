using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Carte_de_bucate.DTOs
{
    public class MancareAddDTO
    {
        //nu avem nevoie de id pentru ca este creat automat de baza de date
        [Required]
        [MaxLength(250)]
        public string NameFood { get; set; }

        [Required]
        public int TaraID { get; set; }

        [Required]
        public string ModPreparare { get; set; }

        [Required]
        public int TimpPreparare { get; set; }
    }
}
