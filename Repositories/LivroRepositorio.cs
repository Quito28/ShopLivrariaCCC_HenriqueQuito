using Microsoft.EntityFrameworkCore;
using ShopLivrariaCCC.Data;
using ShopLivrariaCCC_HenriqueQuito.Data;
using ShopLivrariaCCC_HenriqueQuito.Models;

namespace ShopLivrariaCCC_HenriqueQuito.Repositories
{
    // Certifique-se de que a classe LivroRepositorio implementa a interface ILivroRepositorio
    public class LivroRepositorio : ILivroRepositorio
    {
        private readonly ApplicationDbContext _context;

        public LivroRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task AddLivro(Livro livro)
        {
            // Implementação
            throw new NotImplementedException();
        }

        public Task UpdateLivro(Livro livro)
        {
            // Implementação
            throw new NotImplementedException();
        }

        public Task DeleteLivro(Livro livro)
        {
            // Implementação
            throw new NotImplementedException();
        }

        public Task<Livro?> GetLivroId(int id)
        {
            // Implementação
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Livro>> GetLivros()
        {
            // Implementação
            throw new NotImplementedException();
        }
    }
}
