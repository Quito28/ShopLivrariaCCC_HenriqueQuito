using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopLivrariaCCC_HenriqueQuito.Data;
using ShopLivrariaCCC_HenriqueQuito.Repositories;
using ShopLivrariaCCC_HenriqueQuito.Shared;

var builder = WebApplication.CreateBuilder(args);

// String de conexão corrigida (sem quebras de linha)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Adicionado o '<' antes de IdentityUser e corrigido TokenProviders
builder.Services
    .AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

// Adicionados os sinais '<' que faltavam nos repositórios
builder.Services.AddTransient<IHomeRepositorio, HomeRepositorio>();
builder.Services.AddTransient<ICarrinhoRepositorio, CarrinhoRepositorio>();
builder.Services.AddTransient<IUserPedidoRepositorio, UserPedidoRepositorio>();
builder.Services.AddTransient<IEstoqueRepositorio, EstoqueRepositorio>();
builder.Services.AddTransient<IGeneroRepositorio, GeneroRepositorio>();
builder.Services.AddTransient<IServicoArquivo, ServicoArquivo>();
builder.Services.AddTransient<ILivroRepositorio, LivroRepositorio>();
builder.Services.AddTransient<IRelatorioRepositorio, RelatorioRepositorio>();

var app = builder.Build();

using (var escopo = app.Services.CreateScope())
{
    await DbSemeador.SemeadorDados(escopo.ServiceProvider);
}

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
