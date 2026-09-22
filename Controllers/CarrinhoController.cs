using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopLivrariaCCC_HenriqueQuito.Models;
using ShopLivrariaCCC_HenriqueQuito.Models.DTOs;
using ShopLivrariaCCC_HenriqueQuito.Repositories;

namespace ShopLivrariaCCC_HenriqueQuito.Controllers;

[Authorize]

public class CarrinhoController : Controller
{
    private readonly ICarrinhoRepositorio _carrinho;
    
public CarrinhoController(ICarrinhoRepositorio carrinho)
    {
        _carrinho = carrinho;
    }
    
public async Task<IActionResult> AddItem(int livroId, int quant = 1, int
redirecionar = 0)
    {
        var carrinhoContar = await _carrinho.AddItem(livroId, quant);
        if (redirecionar == 0)
        {
            return Ok(carrinhoContar);
        }
        return RedirectToAction("GetUserCar");
    }
    public async Task<IActionResult> RemoveItem(int livroId)
    {
        var carQuant = await _carrinho.RemoveItem(livroId);
        return RedirectToAction("GetUserCar");
    }
   
public async Task<IActionResult> GetUserCar()
    {
        var car = await _carrinho.GetUserCar();
        return View(car);
    }
    
public async Task<IActionResult> GetTotalItemCar()
    {
        int carTotal = await _carrinho.GetCarItemCont();
        return Ok(carTotal);
    }
    
public IActionResult Checkout()
    {
        return View();
    }
    [HttpPost]
    
public async Task<IActionResult> Checkout(ModeloCheckout model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        bool isCheckedout = await _carrinho.DoCheckout(model);
        if (!isCheckedout)
        {
            return RedirectToAction(nameof(PedidoFalha));
        }
        return RedirectToAction(nameof(PedidoSucesso));
    }

public ActionResult PedidoSucesso()
    {
        return View();
    }
    public IActionResult PedidoFalha()
    {
        return View();
    }
}
