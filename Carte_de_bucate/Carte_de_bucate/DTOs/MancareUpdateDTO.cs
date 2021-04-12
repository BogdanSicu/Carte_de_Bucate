using System.ComponentModel.DataAnnotations;

namespace Carte_de_bucate.DTOs
{
    public class MancareUpdateDTO
    {
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
