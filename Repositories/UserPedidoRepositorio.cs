using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopLivrariaCCC_HenriqueQuito.Data;
using ShopLivrariaCCC_HenriqueQuito.Data;
using ShopLivrariaCCC_HenriqueQuito.Models;
using ShopLivrariaCCC_HenriqueQuito.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopLivrariaCCC_HenriqueQuito.Repositories
{
    // Certifique-se de que a classe UserPedidoRepositorio implementa a interface IUserPedidoRepositorio
    public class UserPedidoRepositorio : IUserPedidoRepositorio
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;

        public UserPedidoRepositorio(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task AlterarStatusPagamento(int pedidoId)
        {
            // CORRIGIDO: Faltava o sinal de '=' antes do await
            var pedido = await _db.Pedidos.FindAsync(pedidoId);
            if (pedido == null)
            {
                throw new InvalidOperationException($"pedido com id: {pedidoId} não encontrado");
            }
            pedido.Pago = !pedido.Pago;
            await _db.SaveChangesAsync();
        }

        // CORRIGIDO: Removido espaço do nome do método "AlterarStatus Pedido"
        public async Task AlterarStatusPedido(UpdateStatusPedidoModel dado)
        {
            // CORRIGIDO: O código abaixo estava fora das chaves do método e faltava '=' em 'await_db'
            var pedido = await _db.Pedidos.FindAsync(dado.PedidoId);

            if (pedido == null)
            {
                throw new InvalidOperationException($"pedido com id:{dado.PedidoId} não encontrado");
            }
            pedido.StatusPedidoId = dado.StatusPedidoId;
            await _db.SaveChangesAsync();
        }

        public async Task<Pedido?> GetPedidoById(int id)
        {
            return await _db.Pedidos.FindAsync(id);
        }

        // CORRIGIDO: Removido espaço de "Status Pedido" no tipo de retorno e na tabela
        public async Task<IEnumerable<StatusPedido>> GetPedidosStatus()
        {
            return await _db.StatusPedido.ToListAsync();
        }

        public async Task<IEnumerable<Pedido>> UserPedidos(bool getAll = false)
        {
            // CORRIGIDO: Removido espaço de "Detalhes Pedido"
            var pedidos = _db.Pedidos
                .Include(x => x.StatusPedido)
                .Include(x => x.DetalhesPedido)
                .ThenInclude(a => a.Livro)
                .ThenInclude(x => x.Genero).AsQueryable();

            if (!getAll)
            {
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("Usuário Não logado");
                
                pedidos = pedidos.Where(a => a.UserId == userId);
            }
            
            return await pedidos.ToListAsync();
        }

        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext?.User;
            if (principal == null) return string.Empty;

            string? userId = _userManager.GetUserId(principal);
            return userId ?? string.Empty;
        }
    }
}
