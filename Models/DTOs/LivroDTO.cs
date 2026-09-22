using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ShopLivrariaCCC_HenriqueQuito.Models.DTOs
{
    public class LivroDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]

        public string? NomeLivro { get; set; }
        [Required]
        [MaxLength(40)]

        public string? AutorNome { get; set; }
        [Required]

        public double Preco {  get; set; }

        public string?  Imagem { get; set; }
        [Required]

        public int GeneroID { get; set; }

        public IFormFile? ImagemArquivo { get; set; }

        public IEnumerable<SelectListItem>?  ListaGenero { get; set; }



    }
}
