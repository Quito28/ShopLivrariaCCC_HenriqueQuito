using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopLivrariaCCC_HenriqueQuito.Models
{
    [Table("DetalhesCarrinho")]
    public class DetalhesCarrinho
    {
        public int Id { get; set; }
        [Required]

        public int CarrinhoId { get; set; }
        [Required]

        public int LivroId { get; set; }
        [Required]

        public int Quantidade {  get; set; }
        [Required]

        public double PrecoUnitario { get; set; }

        public Livro Livro { get; set; }

        public Carrinho Carrinho { get; set; }

    }
}
