using Microsoft.AspNetCore.Mvc;
using StockExchange.Api.Interfaces;
using StockExchange.Api.Interfaces.Domain;
using StockExchange.Api.Models;
using StockExchange.Core.Services;

namespace StockExchange.Api.Services;

public class StockApiService : IStockApiService
{
    private readonly ILogger<StockApiService> _logger;
    private readonly IStockService _stockService;
    private readonly IApiResponseBuilder<List<StockValue>> _apiResponseBuilder;

    public StockApiService(
        ILogger<StockApiService> logger,
        IStockService stockService,
        IApiResponseBuilder<List<StockValue>> apiResponseBuilder)
    {
        _logger = logger;
        _stockService = stockService;
        _apiResponseBuilder = apiResponseBuilder;
    }

    public async Task<ObjectResult> GetStocksAsync(List<string> symbols)
    {
        try
        {
            _logger.LogInformation($"Getting stock values {string.Join(",", symbols)}");

            var stockValues = await _stockService.GetStocksAsync(symbols);

            return _apiResponseBuilder.GetOkObjectResult(stockValues);
        }
        catch (Exception ex)
        {
            _logger.LogError($"There was an error getting stock values for: {string.Join(",", symbols)}");
            _logger.LogError(ex.Message);

            return _apiResponseBuilder.GetErrorObjectResult(ex.Message, "500");
        }
    }
}

