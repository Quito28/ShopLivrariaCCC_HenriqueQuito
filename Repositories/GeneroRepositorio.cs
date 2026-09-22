using Microsoft.EntityFrameworkCore;
using ShopLivrariaCCC_HenriqueQuito.Data;
using ShopLivrariaCCC_HenriqueQuito.Data;
using ShopLivrariaCCC_HenriqueQuito.Models;

namespace ShopLivrariaCCC_HenriqueQuito.Repositories
{
    public class GeneroRepositorio : IGeneroRepositorio
    {
        private readonly ApplicationDbContext _context;

        public GeneroRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddGenero(Genero genero)
        {
            _context.Generos.Add(genero);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGenero(Genero genero)
        {
            _context.Generos.Remove(genero);
            await _context.SaveChangesAsync();
        }

        public async Task<Genero?> GetGeneroById(int id)
        {
            return await _context.Generos.FindAsync(id);
        }

        public async Task<IEnumerable<Genero>> GetGeneros()
        {
            return await _context.Generos.ToListAsync();
        }

        public async Task UpdateGenero(Genero genero)
        {
            _context.Generos.Update(genero);
            await _context.SaveChangesAsync();
        }
    }
}