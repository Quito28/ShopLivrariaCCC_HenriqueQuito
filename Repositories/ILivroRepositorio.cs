using ShopLivrariaCCC_HenriqueQuito.Models;

namespace ShopLivrariaCCC_HenriqueQuito.Repositories
{
    public interface ILivroRepositorio
    {

        Task AddLivro(Livro livro);

        Task UpdateLivro(Livro livro);

        Task DeleteLivro(Livro livro);

        Task<Livro?> GetLivroId(int id);

        Task<IEnumerable<Livro>> GetLivros();
    }
}
