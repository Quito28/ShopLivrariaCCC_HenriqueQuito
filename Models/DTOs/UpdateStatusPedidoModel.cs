using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ShopLivrariaCCC_HenriqueQuito.Models.DTOs
{
    public class UpdateStatusPedidoModel
    {
        public int Id { get; set; }
        [Required]

        public int StatusPedidoId { get; set; }

        public IEnumerable<SelectListItem> StatusPedidoList { get; set; }
        public int PedidoId { get; internal set; }
    }
}
