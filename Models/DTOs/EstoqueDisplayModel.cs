namespace ShopLivrariaCCC_HenriqueQuito.Models.DTOs
{
    public class EstoqueDisplayModel
    {
        public int Id { get; set; }

        public int LivroId { get; set; }

        public int Quantidade { get; set; }

        public string? NomeLivro { get; set; }

    }
}
