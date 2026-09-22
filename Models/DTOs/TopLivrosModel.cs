namespace ShopLivrariaCCC_HenriqueQuito.Models.DTOs
{
    public record TopLivroModel(string NomeLivro, string AutorNome, int TotalUniSoma);

    public record TopLivrosVm(DateTime DataInicio, DateTime DataFinal, IEnumerable<TopLivroModel> TopLivros);

    
    
}
