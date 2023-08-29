using Microsoft.EntityFrameworkCore;

namespace StockExchange.UnitTests.Helpers;

public static class TestHelper
{
    public static DbContextOptions<T> GenerateInMemoryDatabaseOptions<T>(string databaseName) where T : DbContext
    {
        return new DbContextOptionsBuilder<T>()
            .UseInMemoryDatabase(databaseName)
            .Options;
    }
}
