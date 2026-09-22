using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopLivrariaCCC_HenriqueQuito.Constants;
using ShopLivrariaCCC_HenriqueQuito.Models;
using ShopLivrariaCCC_HenriqueQuito.Models.DTOs;
using ShopLivrariaCCC_HenriqueQuito.Repositories;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace ShopLivrariaCCC_HenriqueQuito.Controllers;

[Authorize(Roles = nameof(Roles.Admin))]

public class AdminOperacoesController : Controller
{
    private readonly IUserPedidoRepositorio  _userPedidoRepos;

    public AdminOperacoesController(IUserPedidoRepositorio userPedidoRepos)
    {
        _userPedidoRepos = userPedidoRepos;
    }

public async Task<IActionResult> AllPedidos()
    {
        var pedidos = await _userPedidoRepos.UserPedidos(true);
        return View(pedidos);
    }
    
public async Task<IActionResult> AlterarStatusPagamento(int pedidoId)
    {
        try
        {
            await _userPedidoRepos.AlterarStatusPagamento(pedidoId);
        }
        catch (Exception ex)
        {

        }
        return RedirectToAction(nameof(AllPedidos));
    }

public async Task<IActionResult> AlterarStatusPedido(int pedidoId)
    {
        var pedido = await _userPedidoRepos.GetPedidoById(pedidoId);
        if (pedido == null)
        {
            throw new InvalidOperationException($"Pedido com id: {pedidoId} não encontrado");
        }
        var pedidoStatusList = (await _userPedidoRepos.GetPedidosStatus()).Select
        (pedidoStatus =>
        {
        return new SelectListItem
        {
            Value = pedidoStatus.Id.ToString(),
            Text = pedidoStatus.NomeStatus,
            Selected = pedido.StatusPedidoId == pedidoStatus.Id
        };
    });
        var dado = new UpdateStatusPedidoModel
        {
            PedidoId = pedidoId,
            StatusPedidoId = pedido.StatusPedidoId,
            StatusPedidoList = pedidoStatusList
        };
return View(dado);
}
    [HttpPost]

    public async Task<IActionResult> AlterarStatusPedido(UpdateStatusPedidoModel dado)
    {
        try
        {

            if (!ModelState.IsValid)
            {
                dado.StatusPedidoList = (await _userPedidoRepos.GetPedidosStatus()).Select(pedidoStatus =>
                {
                    return new SelectListItem { Value = pedidoStatus.Id.ToString(), Text = pedidoStatus.NomeStatus, Selected = dado.StatusPedidoId == pedidoStatus.Id };
                });
                return View(dado);

            }
            await _userPedidoRepos.AlterarStatusPedido(dado);
            TempData["msg"] = "Atualizado com Sucesso";
        }
        catch (Exception ex)
        {
            TempData["msg"] = "Algo deu errado ao atualizar status do Pedido: " + ex.Message;
        }
        return RedirectToAction(nameof(AlterarStatusPedido), new { pedidoId = dado.PedidoId });
    }

    public IActionResult Dasboard()
    {
        return View();
    }
}
