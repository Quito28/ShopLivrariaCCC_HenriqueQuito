using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopLivrariaCCC_HenriqueQuito.Models;

namespace ShopLivrariaCCC_HenriqueQuito.Data
{

    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Genero> Generos { get; set; }

        public DbSet<Livro> Livros { get; set; }

        public DbSet<Carrinho> Carrinhos { get; set; }

        public DbSet<DetalhesCarrinho> DetalhesCarrinho { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }

        public DbSet<DetalhesPedido> DetalhesPedido { get; set; }

        public DbSet<StatusPedido> StatusPedido { get; set; }

        public DbSet<Estoque> Estoques { get; set; }

    }
}