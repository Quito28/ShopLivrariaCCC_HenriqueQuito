using System.ComponentModel.DataAnnotations;


namespace ShopLivrariaCCC_HenriqueQuito.Models.DTOs
{
    public class EstoqueDTO
    {
        public int LivroId { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Quantidade não por ser um número negativo")]

        public int Quantidade { get; set; }
    }
}
