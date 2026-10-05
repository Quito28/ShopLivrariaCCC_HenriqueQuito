using Microsoft.EntityFrameworkCore;
using ShopLivrariaCCC_HenriqueQuito.Data;
using ShopLivrariaCCC_HenriqueQuito.Models;

namespace ShopLivrariaCCC_HenriqueQuito.Repositories
{
    public class LivroRepositorio : ILivroRepositorio
    {
        private readonly ApplicationDbContext _context;

        public LivroRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task AddLivro(Livro livro)
        {
            throw new NotImplementedException();
        }

        public Task UpdateLivro(Livro livro)
        {
            throw new NotImplementedException();
        }

        public Task DeleteLivro(Livro livro)
        {
            throw new NotImplementedException();
        }

        public Task<Livro?> GetLivroId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Livro>> GetLivros()
        {
            throw new NotImplementedException();
        }
    }
}
