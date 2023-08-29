using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockExchange.Infrastructure.Models;

public class TradeTransaction
{   
    [Key]
    public int TransactionId { get; set; }

    [Required]
    public string? Symbol { get; set; }

    [Required]
    [Column(TypeName = "numeric(18, 2)")]
    public decimal Price { get; set; }

    [Required]
    [Column(TypeName = "numeric(18, 4)")]
    public decimal Shares { get; set; }

    [Required]
    public int BrokerId { get; set; }

    [Required]
    public DateTime TradeDateTime { get; set; }
}
