using System.ComponentModel.DataAnnotations;

namespace Valasztas.Models
{
    public class Part
    {
        [Key]
        public string RovidNev { get; set; }
        public string? HosszuNev { get; set; }

        public ICollection<Jelolt> Jeloltek { get; set; } 

    }
}
