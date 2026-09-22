using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopLivrariaCCC_HenriqueQuito.Models
{
    [Table("Carrinho")]
    public class Carrinho
    {
        public int Id { get; set; }
        [Required]

        public string UserId { get; set; }

        public bool Deletado { get; set; } = false;

        public ICollection<DetalhesCarrinho> DetalhesCarrinho { get; set; }
        
    }
}
