using Microsoft.AspNetCore.Mvc;
using StockExchange.Infrastructure.Models;

namespace StockExchange.Api.Interfaces;
public interface ITradeApiService
{
    Task<ObjectResult> LoadTradeAsync(TradeTransaction trade);
}
