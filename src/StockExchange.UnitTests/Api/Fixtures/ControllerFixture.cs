using Microsoft.AspNetCore.Http;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StockExchange.Api.Controllers;
using StockExchange.Api.Interfaces;
using StockExchange.Api.Models;
using StockExchange.Core.Models;
using StockExchange.Infrastructure.Models;
using System.Collections.Generic;

namespace StockExchange.UnitTests.Api.Fixtures;

public class ControllerFixture
{
    // stock controller
    private readonly Mock<ILogger<StockController>> _mockStockControllerLogger = new();
    private readonly Mock<IStockApiService> _mockStockApiService = new();

    // trade controller
    private readonly Mock<ILogger<TradeController>> _mockTradeControllerLogger = new();
    private readonly Mock<ITradeApiService> _mockTradeApiService = new();

    private readonly Mock<IHttpContextAccessor> _mockContextAccessor = new();

    internal StockController StockControllerHandler()
    {
        return new(
            _mockStockControllerLogger.Object,
            _mockContextAccessor.Object,
            _mockStockApiService.Object);
    }

    internal TradeController TradeControllerHandler()
    {
        return new(
            _mockTradeControllerLogger.Object,
            _mockContextAccessor.Object,
            _mockTradeApiService.Object);
    }

    internal void SetupStockApiService(
        List<string> symbols,
        List<StockValue> expected)
    {
        _mockStockApiService.Setup(x => x.GetStocksAsync(symbols))
            .ReturnsAsync(new OkObjectResult(new ApiResponse<List<StockValue>>(expected, new Links(""))));
    }

    internal void SetupStockApiServiceError(
        List<string> symbols)
    {
        _mockStockApiService.Setup(x => x.GetStocksAsync(symbols))
            .ReturnsAsync(new ObjectResult(new ApiErrorResponse("500", "", ""))
            {
                StatusCode = Status500InternalServerError
            });
    }

    internal void SetupTradeApiService(
        TradeTransaction trade)
    {
        _mockTradeApiService.Setup(x => x.LoadTradeAsync(trade))
            .ReturnsAsync(new OkObjectResult(new ApiResponse<string>("Successfully loaded trade notification", new Links(""))));
    }

    internal void SetupTradeApiServiceError(
        TradeTransaction trade)
    {
        _mockTradeApiService.Setup(x => x.LoadTradeAsync(trade))
            .ReturnsAsync(new ObjectResult(new ApiErrorResponse("500", "", ""))
            {
                StatusCode = Status500InternalServerError
            });
    }
}
