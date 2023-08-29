using Microsoft.EntityFrameworkCore;
using Moq;
using StockExchange.Infrastructure.Contexts;
using StockExchange.Infrastructure.Models;
using StockExchange.Infrastructure.Persistence;
using StockExchange.UnitTests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StockExchange.UnitTests.Infrastructure.Persistence;

public class TradeTransactionRepositoryUnitTests
{
    private readonly List<TradeTransaction> _testData;

    public TradeTransactionRepositoryUnitTests()
    {
        _testData = new List<TradeTransaction>
        {
            new TradeTransaction
            {
                TransactionId = 1,
                Symbol = "ABC",
                Price = 100
            },
            new TradeTransaction
            {
                TransactionId = 2,
                Symbol = "ABC",
                Price = 120
            },
            new TradeTransaction
            {
                TransactionId = 3,
                Symbol = "DEF",
                Price = 120
            },
            new TradeTransaction
            {
                TransactionId = 4,
                Symbol = "XYZ",
                Price = 120
            }
        };
    }

    [Fact]
    public async void GetBySymbolsAsync_Should_Return_Valid_List()
    {
        // Arrange
        var symbols = new List<string> { "ABC", "DEF" };

        var databaseOptions = TestHelper
            .GenerateInMemoryDatabaseOptions<StockExchangeDbContext>(Guid.NewGuid().ToString());

        await using (var dbContext = new StockExchangeDbContext(databaseOptions))
        {
            dbContext.TradeTransactions.AddRange(_testData);
            await dbContext.SaveChangesAsync();
        }

        // Act
        List<TradeTransaction> result;

        await using (var dbContext = new StockExchangeDbContext(databaseOptions))
        {
            var repository = new TradeTransactionRepository(dbContext);
            result = await repository.GetBySymbolsAsync(symbols);
        }

        Assert.NotNull(result);
        Assert.Equal(3, result.Count);

        Assert.Equal(2, result.Where(x => x.Symbol == "ABC").ToList().Count);
        Assert.Single(result.Where(x => x.Symbol == "DEF").ToList());
    }
}
