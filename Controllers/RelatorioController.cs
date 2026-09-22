using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopLivrariaCCC_HenriqueQuito.Constants;
using ShopLivrariaCCC_HenriqueQuito.Models.DTOs;
using ShopLivrariaCCC_HenriqueQuito.Repositories;

namespace ShopLivrariaCCC_HenriqueQuito.Controllers;

[Authorize(Roles = nameof(Roles.Admin))]

public class RelatorioController : Controller
{
    private readonly IRelatorioRepositorio _relatorioRepos;
    
public RelatorioController(IRelatorioRepositorio relatorioRepos)
    {
        _relatorioRepos = relatorioRepos;
    }
public async Task<ActionResult> TopLivrosVendidos(DateTime? iData = null, DateTime? fData = null)
    {
        try
        {

            DateTime inicioData = iData ?? DateTime.UtcNow.AddDays(-7);
            DateTime finalData = fData ?? DateTime.UtcNow;
            var toplivros = await _relatorioRepos.GetTopLivroByDate(inicioData, finalData);
            var vm = new TopLivrosVm(inicioData, finalData, toplivros);
            return View(vm);
        }
        catch (Exception ex)
        {
            TempData["ErroMensagem"] = "Algo deu errado: + ex.Message";
        return RedirectToAction("Dasboard", "AdminOperacoes");
        }
    }
    
public ActionResult Index()
    {
        return View();
    }
}
