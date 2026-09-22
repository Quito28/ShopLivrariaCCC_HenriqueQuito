using System.ComponentModel.DataAnnotations.Schema;

namespace ShopLivrariaCCC_HenriqueQuito.Models
{
    [Table("Estoque")]
    public class Estoque
    {
        public int Id { get; set; }

        public int LivroId { get; set; }

        public int Quantidade { get; set; }

        public Livro? livro { get; set; }


    }
}
