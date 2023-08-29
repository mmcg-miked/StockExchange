using StockExchange.Api.Models;

namespace StockExchange.Api.Interfaces.Domain;

public interface IStockService
{
    Task<List<StockValue>> GetStocksAsync(List<string> tickers);
}
