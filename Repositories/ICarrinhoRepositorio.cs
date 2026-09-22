using ShopLivrariaCCC_HenriqueQuito.Models;
using ShopLivrariaCCC_HenriqueQuito.Models.DTOs;
    

namespace ShopLivrariaCCC_HenriqueQuito.Repositories
{
    public interface ICarrinhoRepositorio
    {
        Task<int> AddItem(int LivroId, int quant);
        Task<int> RemoveItem(int LivroId);
        Task<Carrinho> GetUserCar();
        Task<int> GetCarItemCont(string userId = " ");
        Task<Carrinho> GetCar(string userId);
        Task<bool> DoCheckout(ModeloCheckout model);
    }
}
