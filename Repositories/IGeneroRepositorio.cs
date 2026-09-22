using ShopLivrariaCCC_HenriqueQuito.Models;
namespace ShopLivrariaCCC_HenriqueQuito.Repositories
{
    public interface IGeneroRepositorio
    {
        Task AddGenero(Genero genero);

        Task UpdateGenero(Genero genero);

        Task<Genero?> GetGeneroById (int id);

        Task DeleteGenero(Genero genero);

        Task<IEnumerable<Genero>> GetGeneros();
    }
}
