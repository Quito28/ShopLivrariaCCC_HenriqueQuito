using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopLivrariaCCC_HenriqueQuito.Constants;
using ShopLivrariaCCC_HenriqueQuito.Models;
using ShopLivrariaCCC_HenriqueQuito.Models.DTOs;
using ShopLivrariaCCC_HenriqueQuito.Repositories;

namespace ShopLivrariaCCC_HenriqueQuito.Controllers;

[Authorize(Roles = nameof(Roles.Admin))]

public class EstoqueController : Controller
{
    private readonly IEstoqueRepositorio _estoqueRepos;
    
public EstoqueController(IEstoqueRepositorio estoqueRepos)
    {
        _estoqueRepos = estoqueRepos;
    }
   
public async Task<IActionResult> Index(string sTerm = "")
    {
        var estoques = await _estoqueRepos.GetEstoques(sTerm);
        return View(estoques);
    }
   
public async Task<IActionResult> ManageEstoque(int livroId)
    {
        var existeEstoque = await _estoqueRepos.GetEstoqueByBookId(livroId);
        var estoque = new EstoqueDTO
        {

            LivroId = livroId,
            Quantidade = existeEstoque != null ? existeEstoque.Quantidade : 0
        };
return View(estoque);
    }
    [HttpPost]
    
public async Task<IActionResult> ManageEstoque(EstoqueDTO estoqueDTO)
    {
        if (!ModelState.IsValid)
        {
            return View(estoqueDTO);
        }
        try
        {
            await _estoqueRepos.ManageEstoque(estoqueDTO);
            TempData["sucesso Mensagem"] = "Estoque atualizado com sucesso";
        }
        catch (Exception ex)
        {

            TempData["erroMensagem"] = "Erro ao atualizar estoque: + ex.Message";
                }
return RedirectToAction("Index");



    }
}
