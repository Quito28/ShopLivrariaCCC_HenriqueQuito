using Microsoft.AspNetCore.Mvc;
using ShopLivrariaCCC_HenriqueQuito.Models;
using ShopLivrariaCCC_HenriqueQuito.Models.DTOs;
using ShopLivrariaCCC_HenriqueQuito.Repositories;
using System.Collections;
using System.Diagnostics;

namespace ShopLivrariaCCC_HenriqueQuito.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHomeRepositorio _homeRepos;

    public HomeController(ILogger<HomeController> logger, IHomeRepositorio homeRepos)
    {
        _logger = logger;
        _homeRepos = homeRepos;
    }

public async Task<IActionResult> Index(string sterm = "", int geneId = 0)
{
    IEnumerable <Livro> Livro = await _homeRepos.GetLivros(sterm, geneId);
    IEnumerable<Genero> generos = await _homeRepos.Generos();
        LivroDisplay_Model livroModel = new LivroDisplay_Model
{
Livro = Livro,
Generos = generos,
STerm = sterm,
GeneId = geneId
};
    return View(livroModel);
}
public ActionResult Privacy()
{
    return View();
}
[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

public IActionResult Error()
{

return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
   
}
}