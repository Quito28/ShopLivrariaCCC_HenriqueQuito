using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ShopLivrariaCCC_HenriqueQuito.Models
{
    [Table("Livro")]
    public class Livro
    {
        public int LivroId { get; set; }
        [Required]
        [StringLength(40)]
        public string NomeLivro { get; set; }
        [Required]
        [StringLength(40)]

        public string AutorNome { get; set; }
        [Required]

        public double Preco { get; set; }

        public string Imagem { get; set; }
        [Required]

        public int GeneroID { get; set; }

        public Genero Genero { get; set; }

        public List<DetalhesPedido> PedidoDetalhes { get; set; }

        public List<DetalhesCarrinho> detalhesCarrinhos { get; set; }

        public Estoque Estoque { get; set; }

        [NotMapped]
        public String NomeGenero { get; set; }

        [NotMapped]
        public int Quantidade { get; set; }
        
    }
}
