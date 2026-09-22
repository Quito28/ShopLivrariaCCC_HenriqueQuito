using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ShopLivrariaCCC.Data;
using ShopLivrariaCCC_HenriqueQuito.Data;
using ShopLivrariaCCC_HenriqueQuito.Models.DTOs;
using System.Data;

namespace ShopLivrariaCCC_HenriqueQuito.Repositories
{
    public class RelatorioRepositorio : IRelatorioRepositorio
    {
        private readonly ApplicationDbContext _db;

        public RelatorioRepositorio(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<TopLivroModel>> GetTopLivroByDate(DateTime dtInicio, DateTime dtFinal)
        {
            var incioDataParam = new SqlParameter("@dtInicio", SqlDbType.DateTime) { Value = dtInicio };
            var finalDataParam = new SqlParameter("@dtFinal", SqlDbType.DateTime) { Value = dtFinal };

            var topVendaLivros = await _db.Database
                .SqlQueryRaw<TopLivroModel>("exec Usp_GetTopNSellingBooksByDate @dtInicio, @dtFinal", incioDataParam, finalDataParam)
                .ToListAsync();

            return topVendaLivros;
        }
    }
}