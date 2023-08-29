using StockExchange.Api.Interfaces.Domain;
using StockExchange.Infrastructure.Interfaces;
using StockExchange.Infrastructure.Models;

namespace StockExchange.Api.Services.Domain;

public class TradeService : ITradeService
{
    private readonly ILogger<TradeService> _logger;
    private readonly ITradeTransactionRepository _tradeTransactionRepository;

    public TradeService(
        ILogger<TradeService> logger,
        ITradeTransactionRepository tradeTransactionRepository)
    {
        _logger = logger;
        _tradeTransactionRepository = tradeTransactionRepository;
    }

    public async Task LoadTradeAsync(TradeTransaction trade)
    {
        await _tradeTransactionRepository.AddAsync(trade);

        await _tradeTransactionRepository.SaveChangesAsync();
    }
}
