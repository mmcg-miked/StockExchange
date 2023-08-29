using Microsoft.AspNetCore.Mvc;
using StockExchange.Core.Models;
using StockExchange.Infrastructure.Models;
using StockExchange.UnitTests.Api.Fixtures;
using Xunit;

namespace StockExchange.UnitTests.Api.Services;

public class TradeApiServiceUnitTests
{
    private readonly ServicesFixture _servicesFixture;

    public TradeApiServiceUnitTests()
    {
        _servicesFixture = new ServicesFixture();
    }

    [Fact]
    public async void LoadTradeAsync_Should_Return_Success_200_With_Valid_Response_Type()
    {
        // Arrange
        var trade = new TradeTransaction()
        {
            TransactionId = 123,
            Symbol = "ABC",
            Price = 100.01m,
            Shares = 60,
            BrokerId = 1,
            TradeDateTime = System.DateTime.Now
        };

        _servicesFixture.SetupTradeService(trade);

        // Act
        var response = await _servicesFixture.TradeApiServiceHandler().LoadTradeAsync(trade);

        // Assert
        Assert.NotNull(response);

        var result = Assert.IsType<OkObjectResult>(response);
        Assert.Equal(200, result.StatusCode);

        var value = (ApiResponse<string>?)result.Value;
        Assert.Equal("Successfully loaded trade notification", value?.Data);
    }

    [Fact]
    public async void LoadTradeAsync_Should_Return_Error_500_With_Valid_Response_Type()
    {
        // Arrange
        var trade = new TradeTransaction()
        {
            TransactionId = 123,
            Symbol = "XYZ",
            Price = 20,
            Shares = 60,
            BrokerId = 101,
            TradeDateTime = System.DateTime.Now
        };

        _servicesFixture.SetupTradeServiceError(trade);

        // Act
        var response = await _servicesFixture.TradeApiServiceHandler().LoadTradeAsync(trade);

        // Assert
        Assert.NotNull(response);

        var result = Assert.IsType<ObjectResult>(response);
        Assert.Equal(500, result.StatusCode);
    }
}
