using System.ComponentModel.DataAnnotations;

namespace AmpliFundCodeSample.Models
{
    public class Pokemon
    {
        public int PokemonId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int PokedexNum { get; set; }
        [Required]
        public int Type1Id { get; set; }
        public PokemonType Type1 { get; set; }
        public int? Type2Id { get; set; }
        public PokemonType Type2 { get; set; }
    }
}