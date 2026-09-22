using ShopLivrariaCCC_HenriqueQuito.Models;
using ShopLivrariaCCC_HenriqueQuito.Models.DTOs;

namespace ShopLivrariaCCC_HenriqueQuito.Repositories
{
    public interface IRelatorioRepositorio
    {
        Task<IEnumerable<TopLivroModel>> GetTopLivroByDate(DateTime dtInicio, DateTime dtFinal);
    }
}
