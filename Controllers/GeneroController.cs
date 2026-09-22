using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopLivrariaCCC_HenriqueQuito.Constants;
using ShopLivrariaCCC_HenriqueQuito.Models;
using ShopLivrariaCCC_HenriqueQuito.Models.DTOs;
using ShopLivrariaCCC_HenriqueQuito.Repositories;


namespace ShopLivrariaCCC_HenriqueQuito.Controllers;

[Authorize(Roles = nameof(Roles.Admin))]
public class GeneroController : Controller
{
    private readonly IGeneroRepositorio _generoRepos;

    public GeneroController(IGeneroRepositorio generoRepos)
    {
        _generoRepos = generoRepos;
    }

    public async Task<IActionResult> Index()
    {
        var generos = await _generoRepos.GetGeneros();
        return View(generos);
    }
    public ActionResult AddGenero()
    {
        return View();
    }
    [HttpPost]

    public async Task<IActionResult> AddGenero(GeneroDTO genero)
    {
        if (ModelState.IsValid)
        {
            return View(genero);
        }
        try
        {
            var generoAdd = new Genero { NomeGenero = genero.NomeGenero, Id =
genero.Id, };
            await _generoRepos.AddGenero(generoAdd);
            TempData["sucesso Mensagem"] = "Genero adicionado com sucesso.";
            return RedirectToAction(nameof(AddGenero));
        }
        catch (Exception ex)
        {
            TempData["erroMensagem"] = "Erro ao adicionar genero: " + ex.Message;
        return View(genero);
        }
    }

    public async Task<IActionResult> UpdateGenero(int id)
    {
        var genero = await _generoRepos.GetGeneroById(id);
        if (genero == null)
        {
            throw new InvalidOperationException($"Genero com id {id} não encontrado.");
        }
        GeneroDTO generoUpdate = new GeneroDTO
        {
            Id = genero.Id,
            NomeGenero = genero.NomeGenero
        };
        return View(generoUpdate);
    }
    [HttpPost]

    public async Task<IActionResult> UpdateGenero(GeneroDTO generoUpdate)
    {
        if (!ModelState.IsValid)
        {
            return View(generoUpdate);
        }
        try
        {
            var genero = new Genero
            {
                Id = generoUpdate.Id,
                NomeGenero = generoUpdate.NomeGenero
            };
        
await _generoRepos.UpdateGenero(genero);
        TempData["sucesso Mensagem"] = "Genero atualizado com sucesso";
        return RedirectToAction(nameof(Index));
    }
catch (Exception ex)
{
            TempData["erroMensagem"] = "Erro ao atualizar Genero: " + ex.Message;
        return View(generoUpdate);
}
    }

    public async Task<IActionResult> DeleteGenero(int id)
    {
        var genero = await _generoRepos.GetGeneroById(id);
        if (genero == null)
        {
            throw new InvalidOperationException($"Genero de id {id} não encontrado.");
        }
        await _generoRepos.DeleteGenero(genero);

        return RedirectToAction(nameof(Index));
    }
}
