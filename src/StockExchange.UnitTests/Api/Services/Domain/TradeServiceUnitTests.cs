using Microsoft.Extensions.Logging;
using Moq;
using StockExchange.Api.Services.Domain;
using StockExchange.Infrastructure.Interfaces;
using StockExchange.Infrastructure.Models;
using Xunit;

namespace StockExchange.UnitTests.Api.Services.Domain;

public class TradeServiceUnitTests
{
    [Fact]
    public async void LoadTradeAsync_Should_Complete_All_Method_Calls_Successfully()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<TradeService>>();
        var mockRepository = new Mock<ITradeTransactionRepository>();
        var tradeService = new TradeService(mockLogger.Object, mockRepository.Object);

        var trade = new TradeTransaction()
        {
            TransactionId = 123,
            Symbol = "ABC",
            Price = 100.01m,
            Shares = 60,
            BrokerId = 1,
            TradeDateTime = System.DateTime.Now
        };

        // Act
        await tradeService.LoadTradeAsync(trade);

        // Assert
        mockRepository.Verify(repo => repo.AddAsync(trade), Times.Once);
        mockRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
    }
}
