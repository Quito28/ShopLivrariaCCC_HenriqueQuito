using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopLivrariaCCC_HenriqueQuito.Models;
using ShopLivrariaCCC_HenriqueQuito.Repositories;

namespace ShopLivrariaCCC_HenriqueQuito.Controllers;

[Authorize]

public class UserPedidoController : Controller
{
    private readonly IUserPedidoRepositorio _userPedidoRepos;

    public UserPedidoController(IUserPedidoRepositorio userPedidoRepos)
    {
        _userPedidoRepos = userPedidoRepos;
    }

    public async Task<IActionResult> UserPedidos()
    {

        var pedidos = await _userPedidoRepos.UserPedidos();
        return View(pedidos);
    }
}

