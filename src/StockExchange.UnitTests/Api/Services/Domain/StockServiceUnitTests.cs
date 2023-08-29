using Microsoft.Extensions.Logging;
using Moq;
using StockExchange.Api.Services.Domain;
using StockExchange.Infrastructure.Interfaces;
using StockExchange.Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StockExchange.UnitTests.Api.Services.Domain;

public class StockServiceUnitTests
{
    [Fact]
    public async void GetStocksAsync_Empty_Input_List_Should_Return_All_Symbols()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<StockService>>();
        var mockRepository = new Mock<ITradeTransactionRepository>();
        var stockService = new StockService(mockLogger.Object, mockRepository.Object);

        var transactions = new List<TradeTransaction>
        {
            new TradeTransaction { Symbol = "ABC", Price = 150.0m },
            new TradeTransaction { Symbol = "ABC", Price = 160.0m },
            new TradeTransaction { Symbol = "XYZ", Price = 2500.0m },
        };

        mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(transactions);

        // Act
        var stockValues = await stockService.GetStocksAsync(new List<string>());

        // Assert
        Assert.Equal(2, stockValues.Count);
        Assert.Equal("ABC", stockValues[0].Symbol);
        Assert.Equal(155.0m, stockValues[0].Value);
        Assert.Equal("XYZ", stockValues[1].Symbol);
        Assert.Equal(2500.0m, stockValues[1].Value);
    }

    [Fact]
    public async void GetStocksAsync_Non_Empty_Input_List_Should_Return_Filtered_Symbols()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<StockService>>();
        var mockRepository = new Mock<ITradeTransactionRepository>();
        var stockService = new StockService(mockLogger.Object, mockRepository.Object);

        var transactions = new List<TradeTransaction>
        {
            new TradeTransaction { Symbol = "ABC", Price = 150.0m },
            new TradeTransaction { Symbol = "ABC", Price = 160.0m }
        };

        var symbols = new List<string>() { "ABC" };

        mockRepository.Setup(repo => repo.GetBySymbolsAsync(symbols))
            .ReturnsAsync(transactions);

        // Act
        var stockValues = await stockService.GetStocksAsync(new List<string> { "ABC" });

        // Assert
        Assert.Single(stockValues);
        Assert.Equal("ABC", stockValues[0].Symbol);
        Assert.Equal(155.0m, stockValues[0].Value);
    }
}
