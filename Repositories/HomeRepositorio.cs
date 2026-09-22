using Microsoft.EntityFrameworkCore;
using ShopLivrariaCCC_HenriqueQuito.Data;
using ShopLivrariaCCC_HenriqueQuito.Data;
using ShopLivrariaCCC_HenriqueQuito.Models;

namespace ShopLivrariaCCC_HenriqueQuito.Repositories
{
    public class HomeRepositorio : IHomeRepositorio
    {
        private readonly ApplicationDbContext _db;

        public HomeRepositorio(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Genero>> Generos()
        {
            return await _db.Generos.ToListAsync();
        }

        public async Task<IEnumerable<Livro>> GetLivros(string sTerm = "", int geneId = 0)
        {
            sTerm = sTerm.ToLower();

            var livroQuery = _db.Livros
                .AsNoTracking()
                .Include(x => x.Genero)
                .Include(x => x.Estoque)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(sTerm))
            {
                livroQuery = livroQuery.Where(l => l.NomeLivro.ToLower().StartsWith(sTerm));
            }

            if (geneId > 0)
            {
                livroQuery = livroQuery.Where(l => l.GeneroID == geneId);
            }

            var livros = await livroQuery
                .Select(livro => new Livro
                {
                    LivroId = livro.LivroId,
                    Imagem = livro.Imagem,
                    AutorNome = livro.AutorNome,
                    NomeLivro = livro.NomeLivro,
                    GeneroID = livro.GeneroID,
                    Preco = livro.Preco,
                    Quantidade = livro.Estoque == null ? 0 : livro.Estoque.Quantidade
                }).ToListAsync();

            return livros;
        }
    }
}