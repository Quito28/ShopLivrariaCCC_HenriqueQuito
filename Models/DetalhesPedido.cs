using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ShopLivrariaCCC_HenriqueQuito.Models
{
    [Table("DetalhesPedido")]
    public class DetalhesPedido
    {
        public int Id { get; set; }
        [Required]

        public int PedidoId { get; set; }
        [Required]

        public int LivroId { get; set; }
        [Required]

        public int Quantidade { get; set; }

        public double PecoUnitario { get; set; }

        public Pedido Pedido { get; set; }

        public Livro Livro { get; set; }
        public double PrecoUnitario { get; internal set; }
    }
}
