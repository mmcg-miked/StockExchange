using Microsoft.AspNetCore.Mvc;
using StockExchange.Api.Models;
using StockExchange.Core.Models;
using StockExchange.UnitTests.Api.Fixtures;
using System.Collections.Generic;
using Xunit;

namespace StockExchange.UnitTests.Api.Services;

public class StockApiServiceUnitTests
{
    private readonly ServicesFixture _servicesFixture;

    public StockApiServiceUnitTests()
    {
        _servicesFixture = new ServicesFixture();
    }

    [Fact]
    public async void GetStocksAsync_Should_Return_Success_200_With_Valid_Response()
    {
        // Arrange
        var symbols = new List<string> { "ABC" };
        var expectedStockValues = new List<StockValue>
        {
            new StockValue()
            {
                Symbol = symbols[0],
                Value = 100
            }
        };

        _servicesFixture.SetupStockService(symbols, expectedStockValues);

        // Act
        var response = await _servicesFixture.StockApiServiceHandler().GetStocksAsync(symbols);

        // Assert
        Assert.NotNull(response);

        var result = Assert.IsType<OkObjectResult>(response);
        Assert.Equal(200, result.StatusCode);

        var value = (ApiResponse<List<StockValue>?>?)result.Value;

        Assert.Equal(expectedStockValues.Count, value?.Data?.Count);
        Assert.Equal("ABC", value?.Data?[0].Symbol);
        Assert.Equal(100, value?.Data?[0].Value);
    }

    [Fact]
    public async void GetStocksAsync_Should_Return_Error_500_With_Valid_Response()
    {
        // Arrange
        var symbols = new List<string> { "XXX" };

        _servicesFixture.SetupStockServiceError(symbols);

        // Act
        var response = await _servicesFixture.StockApiServiceHandler().GetStocksAsync(symbols);

        // Assert
        Assert.NotNull(response);

        var result = Assert.IsType<ObjectResult>(response);
        Assert.Equal(500, result.StatusCode);
    }
}
