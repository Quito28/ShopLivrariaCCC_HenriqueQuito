using Microsoft.EntityFrameworkCore;
using ShopLivrariaCCC_HenriqueQuito.Data;
using ShopLivrariaCCC_HenriqueQuito.Data;
using ShopLivrariaCCC_HenriqueQuito.Models;
using ShopLivrariaCCC_HenriqueQuito.Models.DTOs;

namespace ShopLivrariaCCC_HenriqueQuito.Repositories
{
    public class EstoqueRepositorio : IEstoqueRepositorio
    {
        private readonly ApplicationDbContext _db;

        public EstoqueRepositorio(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Estoque?> GetEstoqueByBookId(int LivroId) =>
            await _db.Estoques.FirstOrDefaultAsync(s => s.LivroId == LivroId);

        public async Task<IEnumerable<EstoqueDisplayModel>> GetEstoques(string sTerm = "")
        {
            var estoques = await (from Livro in _db.Livros
                                  join estoque in _db.Estoques on Livro.LivroId equals estoque.LivroId into Livro_estoque
                                  from livroEstoque in Livro_estoque.DefaultIfEmpty()
                                  where string.IsNullOrWhiteSpace(sTerm) || Livro.NomeLivro.ToLower().Contains(sTerm.ToLower())
                                  select new EstoqueDisplayModel
                                  {
                                      LivroId = Livro.LivroId,
                                      NomeLivro = Livro.NomeLivro,
                                      Quantidade = livroEstoque == null ? 0 : livroEstoque.Quantidade
                                  }).ToListAsync();
            return estoques;
        }

        public async Task ManageEstoque(EstoqueDTO estoqueManage)
        {
            var temEstoque = await GetEstoqueByBookId(estoqueManage.LivroId);
            if (temEstoque is null)
            {
                var estoque = new Estoque
                {
                    LivroId = estoqueManage.LivroId,
                    Quantidade = estoqueManage.Quantidade
                };
                _db.Estoques.Add(estoque);
            }
            else
            {
                temEstoque.Quantidade = estoqueManage.Quantidade;
            }
            await _db.SaveChangesAsync();
        }
    }
}