using Microsoft.Extensions.DependencyInjection;
using StockExchange.Core.Filters;

namespace StockExchange.Core.Extensions;
public static class CoreExtensions
{
    public static void RegisterFilters(
        this IServiceCollection services)
    {
        services.AddScoped<ValidateModelAttribute>();
    }
}
