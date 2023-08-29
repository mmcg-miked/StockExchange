using Microsoft.Extensions.Logging;
using Moq;
using StockExchange.Api.Interfaces.Domain;
using StockExchange.Api.Models;
using StockExchange.Api.Services;
using StockExchange.Core.Services;
using StockExchange.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace StockExchange.UnitTests.Api.Fixtures;

public class ServicesFixture
{
    // stock api service
    private readonly Mock<ILogger<StockApiService>> _mockStockApiServiceLogger = new();
    private readonly Mock<IStockService> _mockStockService = new();

    // trade api service
    private readonly Mock<ILogger<TradeApiService>> _mockTradeApiServiceLogger = new();
    private readonly Mock<ITradeService> _mockTradeService = new();
    private readonly Mock<IApiResponseBuilder<string>> _mockApiResponseBuilderTrade = new();

    private readonly ApiResponseBuilder<List<StockValue>> _apiResponseBuilderStock = new ApiResponseBuilder<List<StockValue>>();
    private readonly ApiResponseBuilder<string> _apiResponseBuilderTrade = new ApiResponseBuilder<string>();

    internal StockApiService StockApiServiceHandler()
    {
        return new(
            _mockStockApiServiceLogger.Object,
            _mockStockService.Object,
            _apiResponseBuilderStock);
    }

    internal TradeApiService TradeApiServiceHandler()
    {
        return new(
            _mockTradeApiServiceLogger.Object,
            _mockTradeService.Object,
            _apiResponseBuilderTrade);
    }

    internal void SetupStockService(
        List<string> symbols,
        List<StockValue> expected)
    {
        _mockStockService.Setup(x => x.GetStocksAsync(symbols))
            .ReturnsAsync(expected);
    }

    internal void SetupStockServiceError(
        List<string> symbols)
    {
        _mockStockService.Setup(x => x.GetStocksAsync(symbols))
            .ThrowsAsync(new Exception());
    }

    internal void SetupTradeService(
        TradeTransaction trade)
    {
        _mockTradeService.Setup(x => x.LoadTradeAsync(trade))
            .Verifiable();
    }

    internal void SetupTradeServiceError(
        TradeTransaction trade)
    {
        _mockTradeService.Setup(x => x.LoadTradeAsync(trade))
            .ThrowsAsync(new Exception());
    }
}
