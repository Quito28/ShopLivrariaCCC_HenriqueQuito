
using ShopLivrariaCCC_HenriqueQuito.Models;
using ShopLivrariaCCC_HenriqueQuito.Models.DTOs;
using System.Data;

namespace ShopLivrariaCCC_HenriqueQuito.Repositories
{
    public interface IUserPedidoRepositorio
    {

        Task<IEnumerable<Pedido>> UserPedidos(bool getAll = false);

        Task AlterarStatusPedido(UpdateStatusPedidoModel dado);

        Task AlterarStatusPagamento(int pedidoId);

        Task<Pedido?> GetPedidoById(int id);

        Task<IEnumerable<StatusPedido>> GetPedidosStatus();

    }
}
