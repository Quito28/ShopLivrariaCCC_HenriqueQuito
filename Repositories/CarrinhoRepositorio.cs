
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopLivrariaCCC_HenriqueQuito.Data;
using ShopLivrariaCCC_HenriqueQuito.Models;
using ShopLivrariaCCC_HenriqueQuito.Models.DTOs;

namespace ShopLivrariaCCC_HenriqueQuito.Repositories
{
    public class CarrinhoRepositorio : ICarrinhoRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CarrinhoRepositorio(ApplicationDbContext context, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> AddItem(int LivroId, int quant)
        {
            string userId = GetUserId();
            using var transacao = await _context.Database.BeginTransactionAsync();
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    throw new UnauthorizedAccessException("Usuário não logado");
                }

                var car = await GetCar(userId);
                if (car is null)
                {
                    car = new Carrinho
                    {
                        UserId = userId,
                    };
                    _context.Carrinhos.Add(car);
                }
                await _context.SaveChangesAsync();

                var carItem = await _context.DetalhesCarrinho.FirstOrDefaultAsync(a => a.CarrinhoId == car.Id && a.LivroId == LivroId);

                if (carItem is not null)
                {
                    carItem.Quantidade += quant;
                }
                else
                {
                    var livro = await _context.Livros.FindAsync(LivroId);
                    if (livro is null)
                        throw new InvalidOperationException("Livro não encontrado.");

                    carItem = new DetalhesCarrinho
                    {
                        LivroId = LivroId,
                        CarrinhoId = car.Id,
                        Quantidade = quant,
                        PrecoUnitario = livro.Preco
                    };
                    _context.DetalhesCarrinho.Add(carItem);
                }

                await _context.SaveChangesAsync();
                await transacao.CommitAsync();
            }
            catch
            {
                await transacao.RollbackAsync();
                throw;
            }

            var carItemQuant = await GetCarItemCont(userId);
            return carItemQuant;
        }

        public async Task<bool> DoCheckout(ModeloCheckout model)
        {
            using var transacao = await _context.Database.BeginTransactionAsync();
            try
            {
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                    throw new UnauthorizedAccessException("Usuário não logado");

                var car = await GetCar(userId);
                if (car is null)
                    throw new InvalidOperationException("Carrinho inválido");

                var detalhesCar = await _context.DetalhesCarrinho.Where(a => a.CarrinhoId == car.Id).ToListAsync();
                if (detalhesCar.Count == 0)
                    throw new InvalidOperationException("Carrinho Vazio");

                var pedidoPendente = await _context.StatusPedido.FirstOrDefaultAsync(s => s.NomeStatus == "Pendente");
                if (pedidoPendente is null)
                    throw new InvalidOperationException("O status do pedido não possui status Pendente");

                var pedido = new Pedido
                {
                    UserId = userId,
                    DataCriacao = DateTime.UtcNow,
                    Nome = model.Nome,
                    Email = model.Email,
                    Celular = model.Celular,
                    MetodoPagamento = model.MetodoPagamento,
                    Endereco = model.Endereco,
                    Pago = false,
                    StatusPedidoId = pedidoPendente.Id
                };

                _context.Pedidos.Add(pedido);
                await _context.SaveChangesAsync();

                foreach (var item in detalhesCar)
                {
                    var detalhesPedido = new DetalhesPedido
                    {
                        LivroId = item.LivroId,
                        PedidoId = pedido.Id,
                        Quantidade = item.Quantidade,
                        PrecoUnitario = item.PrecoUnitario // Corrigido de PecoUnitario para PrecoUnitario
                    };
                    _context.DetalhesPedido.Add(detalhesPedido);

                    var estoque = await _context.Estoques.FirstOrDefaultAsync(a => a.LivroId == item.LivroId);
                    if (estoque == null)
                        throw new InvalidOperationException("Estoque é nulo");

                    if (item.Quantidade > estoque.Quantidade)
                        throw new InvalidOperationException($"Existem apenas {estoque.Quantidade} itens no estoque.");

                    estoque.Quantidade -= item.Quantidade;
                }

                _context.DetalhesCarrinho.RemoveRange(detalhesCar);
                await _context.SaveChangesAsync();
                await transacao.CommitAsync();

                return true;
            }
            catch
            {
                await transacao.RollbackAsync();
                return false;
            }
        }

        public async Task<Carrinho> GetCar(string userId)
        {
            var car = await _context.Carrinhos.FirstOrDefaultAsync(a => a.UserId == userId);
            return car;
        }

        public async Task<int> GetCarItemCont(string userId = "")
        {
            if (string.IsNullOrEmpty(userId))
            {
                userId = GetUserId();
            }

            var dados = await (from car in _context.Carrinhos
                               join detalhes in _context.DetalhesCarrinho on car.Id equals detalhes.CarrinhoId
                               where car.UserId == userId
                               select new { detalhes.Id }).ToListAsync();

            return dados.Count;
        }

        public async Task<Carrinho> GetUserCar()
        {
            var userId = GetUserId();
            if (userId == null)
                throw new InvalidOperationException("Usuário inválido");

            var car = await _context.Carrinhos
                .Include(a => a.DetalhesCarrinho)
                .ThenInclude(a => a.Livro)
                .ThenInclude(a => a.Estoque)
                .Include(a => a.DetalhesCarrinho)
                .ThenInclude(a => a.Livro)
                .ThenInclude(a => a.Genero)
                .Where(a => a.UserId == userId).FirstOrDefaultAsync();

            return car;
        }

        public async Task<int> RemoveItem(int livroId)
        {
            string userId = GetUserId();
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    throw new UnauthorizedAccessException("Usuário não logado");
                }

                var car = await GetCar(userId);
                if (car is null)
                {
                    throw new InvalidOperationException("Carrinho inválido");
                }

                // Corrigido 'LivroId' para 'livroId'
                var carItem = await _context.DetalhesCarrinho.FirstOrDefaultAsync(a => a.CarrinhoId == car.Id && a.LivroId == livroId);
                if (carItem is null)
                {
                    throw new InvalidOperationException("Não existe item no carrinho");
                }
                else if (carItem.Quantidade == 1)
                {
                    _context.DetalhesCarrinho.Remove(carItem);
                }
                else
                {
                    carItem.Quantidade -= 1;
                }

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }

            var carItemQuant = await GetCarItemCont(userId);
            return carItemQuant;
        }

        private string GetUserId()
        {
            // Corrigido de HttpContextAccessor para _httpContextAccessor
            var principal = _httpContextAccessor.HttpContext?.User;
            if (principal is null)
                return string.Empty;

            string userId = _userManager.GetUserId(principal);
            return userId;
        }
    }
}
