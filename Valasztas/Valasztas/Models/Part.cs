using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Valasztas.Models
{
    public class Part
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public string RovidNev { get; set; }
        public string? HosszuNev { get; set; }

        public ICollection<Jelolt> Jeloltek { get; set; } 

    }
}
