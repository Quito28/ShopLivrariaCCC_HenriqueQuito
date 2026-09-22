using System.ComponentModel.DataAnnotations;
namespace ShopLivrariaCCC_HenriqueQuito.Models.DTOs
{
    public class GeneroDTO
    {
        public int Id { get; set; }
        [Required, MaxLength(40)]

        public string NomeGenero { get; set; }
    }
}
