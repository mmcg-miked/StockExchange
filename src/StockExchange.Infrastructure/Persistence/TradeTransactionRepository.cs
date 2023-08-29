using StockExchange.Infrastructure.Contexts;
using StockExchange.Infrastructure.Interfaces;
using StockExchange.Infrastructure.Models;

namespace StockExchange.Infrastructure.Persistence;

public class TradeTransactionRepository : BaseStockExchangeRepository<TradeTransaction>, ITradeTransactionRepository
{
    public TradeTransactionRepository(StockExchangeDbContext dbContext) : base(dbContext)
    {

    }
    public async Task<List<TradeTransaction>> GetBySymbolsAsync(List<string> symbols)
    {
        var trades = await GetAllAsync();

        return trades.Where(s => symbols.Contains(s.Symbol!)).ToList();
    }
}
