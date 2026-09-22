namespace ShopLivrariaCCC_HenriqueQuito.Models.DTOs
{
    public class LivroDisplayModel
    {
        public IEnumerable<Livro> Livro {  get; set; }
        
        public IEnumerable<Genero> Generos { get; set; }

        public string STerm {  get; set; }

        public int GeneId { get; set; }
    }
}
