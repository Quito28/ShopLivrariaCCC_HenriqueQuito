using Microsoft.AspNetCore.Components;
using System.Data;

namespace ShopLivrariaCCC_HenriqueQuito.Models.DTOs
{
    public class DetalhePedidoModalDTO
    {
        public string DivId { get; set; }

        public IEnumerable<DetalhesPedido> DetalhesPedidos { get; set; }


    }
}
    