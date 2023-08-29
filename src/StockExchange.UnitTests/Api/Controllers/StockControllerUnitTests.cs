using Microsoft.AspNetCore.Mvc;
using StockExchange.Api.Models;
using StockExchange.Core.Models;
using StockExchange.UnitTests.Api.Fixtures;
using System.Collections.Generic;
using Xunit;

namespace StockExchange.UnitTests.Api.Controllers;

public class StockControllerUnitTests
{
    private readonly ControllerFixture _controllerFixture;

    public StockControllerUnitTests()
    {
        _controllerFixture = new ControllerFixture();
    }

    [Fact]
    public async void Get_Stock_Values_Should_Return_Success_200_When_Requesting_Single_Symbols()
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

        _controllerFixture.SetupStockApiService(symbols, expectedStockValues);

        // Act
        var response = await _controllerFixture.StockControllerHandler().Get(symbols);

        // Assert
        Assert.NotNull(response);

        var result = Assert.IsType<OkObjectResult>(response.Result);
        Assert.Equal(200, result.StatusCode);

        var value = (ApiResponse<List<StockValue>?>?)result.Value;

        Assert.Equal(expectedStockValues.Count, value?.Data?.Count);
        Assert.Equal("ABC", value?.Data?[0].Symbol);
        Assert.Equal(100, value?.Data?[0].Value);
    }

    [Fact]
    public async void Get_Stock_Values_Should_Return_Success_200_When_Requesting_Multiple_Symbols()
    {
        // Arrange
        var symbols = new List<string> { "ABC", "XYZ" };
        var expectedStockValues = new List<StockValue>
        {
            new StockValue()
            {
                Symbol = symbols[0],
                Value = 100
            },
            new StockValue()
            {
                Symbol = symbols[1],
                Value = 200
            }
        };

        _controllerFixture.SetupStockApiService(symbols, expectedStockValues);

        // Act
        var response = await _controllerFixture.StockControllerHandler().Get(symbols);

        // Assert
        Assert.NotNull(response);

        var result = Assert.IsType<OkObjectResult>(response.Result);
        Assert.Equal(200, result.StatusCode);

        var value = (ApiResponse<List<StockValue>?>?)result.Value;

        Assert.Equal(expectedStockValues.Count, value?.Data?.Count);
        Assert.Equal(expectedStockValues[0].Symbol, value?.Data?[0].Symbol);
        Assert.Equal(expectedStockValues[0].Value, value?.Data?[0].Value);
        Assert.Equal(expectedStockValues[1].Symbol, value?.Data?[1].Symbol);
        Assert.Equal(expectedStockValues[1].Value, value?.Data?[1].Value);
    }

    [Fact]
    public async void Get_Stock_Values_Should_Return_Valid_500_Error()
    {
        // Arrange
        var symbols = new List<string> { "XXX" };

        _controllerFixture.SetupStockApiServiceError(symbols);

        // Act
        var response = await _controllerFixture.StockControllerHandler().Get(symbols);

        // Assert
        Assert.NotNull(response);

        var result = Assert.IsType<ObjectResult>(response.Result);
        Assert.Equal(500, result.StatusCode);
    }
}
