using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopLivrariaCCC_HenriqueQuito.Models
{
    [Table("Pedido")]

    public class Pedido
    {
        public int Id { get; set; }
        [Required]

        public string UserId { get; set; }
        [Required]

        public DateTime DataCriacao { get; set; } = DateTime.Now;
        [Required]

        public int StatusPedidoId { get; set; }

        public bool Deletado { get; set; } = false;
        [Required]
        [MaxLength(30)]

        public string? Nome { get; set; }
        [Required]
        [MaxLength(30)]
        [EmailAddress]

        public string? Email { get; set; }
        [Required]

        public string? Celular { get; set;  }
        [Required]
        [MaxLength(200)]

        public string? Endereco { get; set;}
        [Required]
        [MaxLength(30)]

        public string? MetodoPagamento { get; set; }

        public bool Pago { get; set; }

        public StatusPedido StatusPedido { get; set; }
       
        public List<DetalhesPedido> DetalhesPedido { get; set;}
        
    }
}
