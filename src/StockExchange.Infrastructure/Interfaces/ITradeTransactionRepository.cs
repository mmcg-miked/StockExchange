using StockExchange.Infrastructure.Models;

namespace StockExchange.Infrastructure.Interfaces;

public interface ITradeTransactionRepository
{
    Task AddAsync(TradeTransaction trade);
    Task<List<TradeTransaction>> GetAllAsync();
    Task<List<TradeTransaction>> GetBySymbolsAsync(List<string> symbols);
    Task<int> SaveChangesAsync();
}
