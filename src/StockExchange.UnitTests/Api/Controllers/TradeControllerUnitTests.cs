using Microsoft.AspNetCore.Mvc;
using StockExchange.Infrastructure.Models;
using StockExchange.UnitTests.Api.Fixtures;
using Xunit;

namespace StockExchange.UnitTests.Api.Controllers;

public class TradeControllerUnitTests
{
    private readonly ControllerFixture _controllerFixture;

    public TradeControllerUnitTests()
    {
        _controllerFixture = new ControllerFixture();
    }

    [Fact]
    public async void Transaction_Notification_Should_Return_Success_200()
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

        _controllerFixture.SetupTradeApiService(trade);

        // Act
        var response = await _controllerFixture.TradeControllerHandler().Post(trade);

        // Assert
        Assert.NotNull(response);

        var result = Assert.IsType<OkObjectResult>(response.Result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async void Transaction_Notification_Should_Return_Valid_500_Error()
    {
        // Arrange
        var trade = new TradeTransaction()
        {
            TransactionId = 123,
            Symbol = "XYZ",
            Price = 10,
            Shares = 10,
            BrokerId = 1,
            TradeDateTime = System.DateTime.Now
        };

        _controllerFixture.SetupTradeApiServiceError(trade);

        // Act
        var response = await _controllerFixture.TradeControllerHandler().Post(trade);

        // Assert
        Assert.NotNull(response);

        var result = Assert.IsType<ObjectResult>(response.Result);
        Assert.Equal(500, result.StatusCode);
    }
}

