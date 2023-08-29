namespace StockExchange.Infrastructure.Interfaces;

public interface IBaseStockExchangeRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task<List<T>> GetAllAsync();
    Task<int> SaveChangesAsync();
}
