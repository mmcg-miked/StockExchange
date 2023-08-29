using StockExchange.Infrastructure.Models;

namespace StockExchange.Api.Interfaces.Domain;

public interface ITradeService
{
    Task LoadTradeAsync(TradeTransaction trade);
}
