using StockExchange.Api.Interfaces.Domain;
using StockExchange.Api.Models;
using StockExchange.Infrastructure.Interfaces;
using StockExchange.Infrastructure.Models;

namespace StockExchange.Api.Services.Domain;

public class StockService : IStockService
{
    private readonly ILogger<StockService> _logger;
    private readonly ITradeTransactionRepository _tradeTransactionRepository;

    public StockService(
        ILogger<StockService> logger,
        ITradeTransactionRepository tradeTransactionRepository)
    {
        _logger = logger;
        _tradeTransactionRepository = tradeTransactionRepository;
    }

    public async Task<List<StockValue>> GetStocksAsync(List<string> symbols)
    {
        List<TradeTransaction> tradeTransactions = null!;

        if(symbols?.Count == 0)
        {
            tradeTransactions = await _tradeTransactionRepository.GetAllAsync();
        }
        else
        {
            tradeTransactions = await _tradeTransactionRepository.GetBySymbolsAsync(symbols!);
        }

        var stockValues = tradeTransactions
            .GroupBy(g => g.Symbol)
            .Select(s => new StockValue
            {
                Symbol = s.Key,
                Value = s.Average(a => a.Price)
            }).ToList();

        return stockValues;
    }
}
