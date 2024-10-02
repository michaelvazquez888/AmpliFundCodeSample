using System.ComponentModel.DataAnnotations;

namespace AmpliFundCodeSample.Models
{
    public class PokemonType
    {
        public int PokemonTypeId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}