using System.ComponentModel.DataAnnotations;
namespace ShopLivrariaCCC_HenriqueQuito.Models.DTOs
{
    public class ModeloCheckout
    {
        [Required, MaxLength(30)]

        public string? Nome { get; set; }
        [Required, MaxLength(30), EmailAddress]

        public string? Email { get; set; }
        [Required]

        public string? Celular { get; set; }
        [Required, MaxLength(200)]

        public string? Endereco { get; set; }
        [Required]

        public string? MetodoPagamento { get; set; }

    }
}
