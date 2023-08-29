using Microsoft.AspNetCore.Mvc;

namespace StockExchange.Api.Interfaces;
public interface IStockApiService
{
    Task<ObjectResult> GetStocksAsync(List<string> tickers);
}
