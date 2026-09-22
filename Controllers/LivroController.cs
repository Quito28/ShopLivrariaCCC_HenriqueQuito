using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopLivrariaCCC_HenriqueQuito.Constants;
using ShopLivrariaCCC_HenriqueQuito.Models;
using ShopLivrariaCCC_HenriqueQuito.Models.DTOs;
using ShopLivrariaCCC_HenriqueQuito.Repositories;
using ShopLivrariaCCC_HenriqueQuito.Shared;
using static System.Net.Mime.MediaTypeNames;

namespace ShopLivrariaCCC_HenriqueQuito.Controllers;

[Authorize(Roles = nameof(Roles.Admin))]

public class LivroController : Controller
{
    private readonly ILivroRepositorio _livroRepos;
    private readonly IGeneroRepositorio _generoRepos;
    private readonly IServicoArquivo _servicoArquivo;

    public LivroController(ILivroRepositorio livroRepos, IGeneroRepositorio generoRepos, IServicoArquivo servicoArquivo)
    {
        _livroRepos = livroRepos;
        _generoRepos = generoRepos;
        _servicoArquivo = servicoArquivo;
    }
    public async Task<IActionResult> Index()
    {

        var livros = await _livroRepos.GetLivros();
        return View(livros);
    }
    public async Task<IActionResult> AddLivro()
    {
        var generoSelectList = (await _generoRepos.GetGeneros()).Select(genero => new SelectListItem
        {
            Text = genero.NomeGenero,
            Value = genero.Id.ToString(),
        });
        LivroDTO livroAdd = new() { ListaGenero = generoSelectList };
        return View(livroAdd);
    }
    [HttpPost]
    public async Task<IActionResult> AddLivro(LivroDTO livroAdd)
    {
        var generoSelectList = (await _generoRepos.GetGeneros()).Select(genero => new

            SelectListItem
        {
            Text = genero.NomeGenero,
            Value = genero.Id.ToString(),
        });
        livroAdd.ListaGenero = generoSelectList;
        if (!ModelState.IsValid)
        {
            return View(livroAdd);
        }
        try
        {
            if (livroAdd.ImagemArquivo != null)
            {
                if (livroAdd.ImagemArquivo.Length > 1 * 1024 * 1024)
                {
                    throw new InvalidOperationException("Tamanho máximo de 1MB excedida.");
                }
                string[] extensoesPermitidas = [".jpeg", ".jpg", ".png"];
                string imagemNome = await _servicoArquivo.SavarArquivo(livroAdd.ImagemArquivo, extensoesPermitidas);
                livroAdd.Imagem = imagemNome;
            }
            Livro livro = new()
            {
                Id = livroAdd.Id,
                NomeLivro = livroAdd.NomeLivro,
                AutorNome = livroAdd.AutorNome,
                Imagem = livroAdd.Imagem,
                GeneroID = livroAdd.GeneroID,
                Preco = livroAdd.Preco
            }; TempData["SucessoMensagem"] = "Livro adicionado com sucesso.";
            await _livroRepos.AddLivro(livro);
            return RedirectToAction(nameof(AddLivro));

        }
        catch (InvalidOperationException ex) {

            TempData["ErroMensagem"] = "Erro ao adicionar Livro: " + ex.Message;
            return View(livroAdd);
        }
        catch (FileNotFoundException ex) {
            TempData["ErroMensagem"] = "Erro ao adicionar Livro:" + ex.Message;
            return View(livroAdd);
        }
        catch (Exception ex)
        {
            TempData["ErroMensagem"] = "Erro ao adicionar Livro: " + ex.Message;
            return View(livroAdd);
        }
    }
    public async Task<IActionResult> UpdateLivro(int id)
    {
        var livro = await _livroRepos.GetLivroId(id);
        if (livro == null)
        {

            TempData["ErroMensagem"] = $"Livro com id {id} não encontrado";
            return RedirectToAction(nameof(Index));
        }
        var generolist = (await _generoRepos.GetGeneros()).Select(genero => new
        SelectListItem
        {
            Text = genero.NomeGenero,
            Value = genero.Id.ToString(),
            Selected = genero.Id == livro.GeneroID
        });
        LivroDTO LivroUpdate = new()
        {
            ListaGenero = generolist,
            NomeLivro = livro.NomeLivro,
            AutorNome = livro.AutorNome,
            GeneroID = livro.GeneroID,
            Preco = livro.Preco,
            Imagem = livro.Imagem
        };
        return View(LivroUpdate);
    }
    [HttpPost]
   
public async Task<IActionResult> UpdateLivro(LivroDTO livroUpdate)
    {
        var generoList = (await _generoRepos.GetGeneros()).Select(genero => new
        SelectListItem
        {
            Text = genero.NomeGenero,
            Value = genero.Id.ToString(),
            Selected = genero.Id == livroUpdate.GeneroID
        });
        livroUpdate.ListaGenero = generoList;

        if (!ModelState.IsValid)
        {
            return View(livroUpdate);
        }
        try
        {
            string oldImagem = "";
            if (livroUpdate.ImagemArquivo != null)
            {
                if (livroUpdate.ImagemArquivo.Length > 1 * 1024 * 1024)
                {

                    throw new InvalidOperationException("Tamanho máximo de 1MB excedida.");
                }
                string[] extensoesPermitidas = [".jpeg", ".jpg", ".png"];
                string nomeImagem = await _servicoArquivo.SavarArquivo(livroUpdate.ImagemArquivo, extensoesPermitidas);
                oldImagem = livroUpdate.Imagem;
                livroUpdate.Imagem = nomeImagem;
            }

            Livro livro = new()
            {
                Id = livroUpdate.Id,
                NomeLivro = livroUpdate.NomeLivro,
                AutorNome = livroUpdate.AutorNome,
                GeneroID = livroUpdate.GeneroID,
                Preco = livroUpdate.Preco,
                Imagem = livroUpdate.Imagem
            };
            await _livroRepos.UpdateLivro(livro);
            if (!string.IsNullOrWhiteSpace(oldImagem))
            {
                _servicoArquivo.ApagarArquivo(oldImagem);
            }

            TempData["Sucesso Mensagem"] = "Livro atualizado com sucesso.";
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            TempData["ErroMensagem"] = "Erro ao atualizar Livro: + ex. Message";
        return View(livroUpdate);
        } 
        catch (FileNotFoundException ex)
        {
            TempData["ErroMensagem"] = "Erro ao atualizar Livro: + ex. Message";
            return View(livroUpdate);
        }
        catch (Exception ex)
        {
            TempData["ErroMensagem"] = "Erro ao atualizar Livro: + ex. Message";
        return View(livroUpdate);
        }
    }
    
public async Task<IActionResult> DeleteLivro(int id)
    {
        try
        {
            var livro = await _livroRepos.GetLivroId(id);
            if (livro == null)
            {
                TempData["ErroMensagem"] = $"Livro de id {id} não encontrado.";
            }
            else
            {
                await _livroRepos.DeleteLivro(livro);
                if (!string.IsNullOrWhiteSpace(livro.Imagem))
                {
                    _servicoArquivo.ApagarArquivo(livro.Imagem);
                }
            }
        }
        catch (InvalidOperationException ex)
        {
            TempData["ErroMensagem"] = "Erro ao deletar Livro: + ex. Message";
        }
        catch (FileNotFoundException ex)
        {
            TempData["ErroMensagem"] = "Erro ao deletar Livro: + ex. Message";
        }
        catch (Exception ex)
        {
            TempData["ErroMensagem"] = "Erro ao deletar Livro: + ex.Message";
        }
        return RedirectToAction(nameof(Index));
    }
}




