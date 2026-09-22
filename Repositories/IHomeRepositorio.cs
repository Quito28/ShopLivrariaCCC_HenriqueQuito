using ShopLivrariaCCC_HenriqueQuito.Models;

namespace ShopLivrariaCCC_HenriqueQuito.Repositories
{
    public interface IHomeRepositorio
    {
        Task<IEnumerable<Livro>> GetLivros(string sTerm = " ", int geneId = 0);

        Task<IEnumerable<Genero>> Generos();


    }
}
