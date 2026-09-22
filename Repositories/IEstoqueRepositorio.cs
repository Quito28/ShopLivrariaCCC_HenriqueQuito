using ShopLivrariaCCC_HenriqueQuito.Models;
using ShopLivrariaCCC_HenriqueQuito.Models.DTOs;


namespace ShopLivrariaCCC_HenriqueQuito.Repositories
{
    public interface IEstoqueRepositorio
    {
        Task<IEnumerable<EstoqueDisplayModel>> GetEstoques(string sTerm = "");

        Task<Estoque?> GetEstoqueByBookId(int LivroId);

        Task ManageEstoque(EstoqueDTO estoqueManage);
    }
}
