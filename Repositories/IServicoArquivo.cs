namespace ShopLivrariaCCC_HenriqueQuito.Repositories
{
    public interface IServicoArquivo
    {
        Task<string> SavarArquivo(IFormFile arquivo, string[] extenssoesPermitida);
        void ApagarArquivo(string arquivoNome);
    }
}
