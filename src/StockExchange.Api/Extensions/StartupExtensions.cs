using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StockExchange.Api.Interfaces;
using StockExchange.Api.Interfaces.Domain;
using StockExchange.Api.Models;
using StockExchange.Api.Services;
using StockExchange.Api.Services.Domain;
using StockExchange.Core.Services;
using StockExchange.Infrastructure.Contexts;
using StockExchange.Infrastructure.Interfaces;
using StockExchange.Infrastructure.Persistence;

namespace StockExchange.Api.Extensions;
public static class StartupExtensions
{
    public static void RegisterServices(
        this IServiceCollection services)
    {
        services.AddTransient<IStockApiService, StockApiService>();
        services.AddTransient<ITradeApiService, TradeApiService>();

        services.AddTransient<IApiResponseBuilder<List<StockValue>>, ApiResponseBuilder<List<StockValue>>>();
        services.AddTransient<IApiResponseBuilder<string>, ApiResponseBuilder<string>>();

        services.AddTransient<IStockService, StockService>();
        services.AddTransient<ITradeService, TradeService>();

        services.AddTransient<ITradeTransactionRepository, TradeTransactionRepository>();
    }

    public static void RegisterLogging(
        this IServiceCollection services)
    {
        services.AddLogging();
    }

    public static void RegisterDbContexts(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<StockExchangeDbContext>(options => options
        .UseSqlServer(new SqlConnectionStringBuilder()
        {
            DataSource = config.GetValue<string>("SqlConfiguration:Server"),
            InitialCatalog = config.GetValue<string>("SqlConfiguration:InitialCatalog"),
            IntegratedSecurity = true
        }.ConnectionString));
    }
}
