using Microsoft.EntityFrameworkCore;
using StockExchange.Infrastructure.Interfaces;

namespace StockExchange.Infrastructure.Persistence;

public abstract class BaseStockExchangeRepository<T> : IBaseStockExchangeRepository<T> where T : class
{
    protected BaseStockExchangeRepository(
        DbContext dbContext)
    {
        DbContext = dbContext;
    }

    protected DbContext DbContext { get; }

    public DbSet<T> Table => DbContext.Set<T>();


    public virtual async Task AddAsync(T entity)
    {
        await DbContext.Set<T>().AddAsync(entity);
    }

    public virtual async Task<List<T>> GetAllAsync()
    {
        return await DbContext.Set<T>().ToListAsync();
    }

    public virtual async Task<int> SaveChangesAsync()
    {
        return await DbContext.SaveChangesAsync();
    }
}
