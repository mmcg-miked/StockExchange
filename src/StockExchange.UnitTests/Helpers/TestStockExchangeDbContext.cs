using Microsoft.EntityFrameworkCore;

namespace StockExchange.UnitTests.Helpers;

public partial class TestStockExchangeDbContext : DbContext
{
    public TestStockExchangeDbContext(DbContextOptions<TestStockExchangeDbContext> options)
        : base(options)
    {
    }
}
