using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopLivrariaCCC_HenriqueQuito.Models
{
    [Table("StatusPedido")]
    public class StatusPedido
    {
        public int Id { get; set; }
        [Required]

        public int StatusId { get; set; }
        [Required, MaxLength(20)]

        public string? NomeStatus { get; set; } 
    }
}
