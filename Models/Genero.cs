

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopLivrariaCCC_HenriqueQuito.Models
{
    [Table("Genero")]
    public class Genero
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]

        public string NomeGenero { get; set; }

        public List<Livro> Livros { get; set; }
    }
}
