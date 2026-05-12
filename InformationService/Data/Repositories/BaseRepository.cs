using System.Linq.Expressions;
using InformationService.Data.Contexts;
using InformationService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationService.Data.Repositories;

public abstract class BaseRepository<TEntity>(InformationDbContext context)
    : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly InformationDbContext _context = context;
    protected readonly DbSet<TEntity> _table = context.Set<TEntity>();

    public async Task<RepositoryResult> AddAsync(TEntity entity)
    {
        try
        {
            _table.Add(entity);
            await _context.SaveChangesAsync();

            return new RepositoryResult { Success = true };
        }
        catch (Exception ex)
        {
            return new RepositoryResult { Success = false, Error = ex.Message };
        }
    }

    public async Task<RepositoryResult<IEnumerable<TEntity>>> GetAllAsync()
    {
        try
        {
            var result = await _table.ToListAsync();

            return new RepositoryResult<IEnumerable<TEntity>>
            {
                Success = true,
                Result = result
            };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<IEnumerable<TEntity>>
            {
                Success = false,
                Error = ex.Message
            };
        }
    }

    public async Task<RepositoryResult<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var result = await _table.FirstOrDefaultAsync(expression);

            return result == null
                ? new RepositoryResult<TEntity> { Success = false, Error = "Not found." }
                : new RepositoryResult<TEntity> { Success = true, Result = result };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<TEntity> { Success = false, Error = ex.Message };
        }
    }

    public async Task<RepositoryResult> UpdateAsync(TEntity entity)
    {
        try
        {
            _table.Update(entity);
            await _context.SaveChangesAsync();

            return new RepositoryResult { Success = true };
        }
        catch (Exception ex)
        {
            return new RepositoryResult { Success = false, Error = ex.Message };
        }
    }

    public async Task<RepositoryResult> DeleteAsync(TEntity entity)
    {
        try
        {
            _table.Remove(entity);
            await _context.SaveChangesAsync();

            return new RepositoryResult { Success = true };
        }
        catch (Exception ex)
        {
            return new RepositoryResult { Success = false, Error = ex.Message };
        }
    }
}