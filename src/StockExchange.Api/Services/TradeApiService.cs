using Microsoft.AspNetCore.Mvc;
using StockExchange.Api.Interfaces;
using StockExchange.Api.Interfaces.Domain;
using StockExchange.Infrastructure.Models;
using StockExchange.Core.Services;

namespace StockExchange.Api.Services;

public class TradeApiService : ITradeApiService
{
    private readonly ILogger<TradeApiService> _logger;
    private readonly ITradeService _tradeService;
    private readonly IApiResponseBuilder<string> _apiResponseBuilder;

    public TradeApiService(
        ILogger<TradeApiService> logger,
        ITradeService tradeService,
        IApiResponseBuilder<string> apiResponseBuilder)
    {
        _logger = logger;
        _tradeService = tradeService;
        _apiResponseBuilder = apiResponseBuilder;
    }

    public async Task<ObjectResult> LoadTradeAsync(TradeTransaction trade)
    {
        try
        {
            _logger.LogInformation($"Loading trade notification: {trade.Symbol} {trade.Shares}@{trade.Price}. BrokerId: {trade.BrokerId}");

            await _tradeService.LoadTradeAsync(trade);

            return _apiResponseBuilder.GetOkObjectResult("Successfully loaded trade notification");
        }
        catch (Exception ex)
        {
            _logger.LogError($"There was an error loading trade notification: {trade.Symbol} {trade.Shares}@{trade.Price}. BrokerId: {trade.BrokerId}");
            _logger.LogError(ex.Message);

            return _apiResponseBuilder.GetErrorObjectResult(ex.Message, "500");
        }
    }
}
