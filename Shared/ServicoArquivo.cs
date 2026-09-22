using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ShopLivrariaCCC_HenriqueQuito.Repositories;
using System;
using System.IO;
using System.Linq;

namespace ShopLivrariaCCC_HenriqueQuito.Shared;

public class ServicoArquivo : IServicoArquivo
{
    private readonly IWebHostEnvironment _environment;

    public ServicoArquivo(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> SavarArquivo(IFormFile arquivo, string[] extenssoesPermitida)
    {
        var wwwPath = _environment.WebRootPath;
        var path = Path.Combine(wwwPath, "imagens");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var extenssoes = Path.GetExtension(arquivo.FileName);

        if (!extenssoesPermitida.Contains(extenssoes))
        {
            throw new InvalidOperationException($"Apenas arquivos {string.Join(",", extenssoesPermitida)} são permitidos");
        }

        string arquivoNome = $"{Guid.NewGuid()}{extenssoes}";
        string arquivoNomeCaminho = Path.Combine(path, arquivoNome);

        using var fluxo = new FileStream(arquivoNomeCaminho, FileMode.Create);
        await arquivo.CopyToAsync(fluxo);

        return arquivoNome;
    }

    public void ApagarArquivo(string arquivoNome)
    {
        var wwwPath = _environment.WebRootPath;
        var arquivoNomeCaminho = Path.Combine(wwwPath, "imagens", arquivoNome);

        if (!File.Exists(arquivoNomeCaminho))
        {
            throw new FileNotFoundException("Arquivo não encontrado", arquivoNome);
        }

        File.Delete(arquivoNomeCaminho);
    }
}
