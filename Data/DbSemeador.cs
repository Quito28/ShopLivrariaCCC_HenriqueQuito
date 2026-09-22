using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopLivrariaCCC_HenriqueQuito.Constants;
using ShopLivrariaCCC_HenriqueQuito.Models;

namespace ShopLivrariaCCC_HenriqueQuito.Data
{
    public class DbSemeador
    {
        public static async Task SemeadorDados(IServiceProvider servico)
        {
            try
            {
                var context = servico.GetService<ApplicationDbContext>();

                if (context != null && (await context.Database.GetPendingMigrationsAsync()).Any())
                {
                    await context.Database.MigrateAsync();
                }

                var userMgr = servico.GetService<UserManager<IdentityUser>>();
                var roleMgr = servico.GetService<RoleManager<IdentityRole>>();

                if (roleMgr != null)
                {
                    var adminRoleExists = await roleMgr.RoleExistsAsync(Roles.Admin.ToString());
                    if (!adminRoleExists)
                    {
                        await roleMgr.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
                    }

                    var userRoleExists = await roleMgr.RoleExistsAsync(Roles.User.ToString());
                    if (!userRoleExists)
                    {
                        await roleMgr.CreateAsync(new IdentityRole(Roles.User.ToString()));
                    }
                }

                if (userMgr != null)
                {
                    var admin = new IdentityUser
                    {
                        UserName = "admin@gmail.com",
                        Email = "admin@gmail.com",
                        EmailConfirmed = true
                    };

                    var userInDb = await userMgr.FindByEmailAsync(admin.Email);
                    if (userInDb is null)
                    {
                        await userMgr.CreateAsync(admin, "Admin@123");
                        await userMgr.AddToRoleAsync(admin, Roles.Admin.ToString());
                    }
                }

                if (context != null)
                {
                    if (!context.Generos.Any())
                    {
                        await SeedGeneroAsync(context);
                    }

                    if (!context.StatusPedido.Any())
                    {
                        await SeedPedidoStatusAsync(context);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"***Erro no DbSemeador: {ex.Message}");
            }
        }

        #region private methods

        private static async Task SeedGeneroAsync(ApplicationDbContext context)
        {
            var generos = new[]
            {
                new Genero { NomeGenero = "Romance" },
                new Genero { NomeGenero = "Action" },
                new Genero { NomeGenero = "Filme de Ação" },
                new Genero { NomeGenero = "Crime" },
                new Genero { NomeGenero = "Autoajuda" },
                new Genero { NomeGenero = "Programação" }
            };

            await context.Generos.AddRangeAsync(generos);
            await context.SaveChangesAsync();
        }

        private static async Task SeedPedidoStatusAsync(ApplicationDbContext context)
        {
            var pedidoStatus = new[]
            {
                new StatusPedido { StatusId = 1, NomeStatus = "Pendente" },
                new StatusPedido { StatusId = 2, NomeStatus = "Enviado" },
                new StatusPedido { StatusId = 3, NomeStatus = "Entregue" },
                new StatusPedido { StatusId = 4, NomeStatus = "Cancelado" },
                new StatusPedido { StatusId = 5, NomeStatus = "Devolvido" },
                new StatusPedido { StatusId = 6, NomeStatus = "Reembolso" }
            };

            await context.StatusPedido.AddRangeAsync(pedidoStatus);
            await context.SaveChangesAsync();
        }

        #endregion
    }
}
