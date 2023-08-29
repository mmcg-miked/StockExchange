using Microsoft.EntityFrameworkCore;
using StockExchange.Infrastructure.Models;

namespace StockExchange.Infrastructure.Contexts;

public class StockExchangeDbContext : DbContext
{
    public StockExchangeDbContext(DbContextOptions options)
        : base(options)
    { 
    }

    public DbSet<TradeTransaction> TradeTransactions { get; set; } = null!;
}
