using System.Linq.Expressions;
using InformationService.Data.Models;

namespace InformationService.Data.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<RepositoryResult> AddAsync(TEntity entity);
    Task<RepositoryResult<IEnumerable<TEntity>>> GetAllAsync();
    Task<RepositoryResult<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression);
    Task<RepositoryResult> UpdateAsync(TEntity entity);
    Task<RepositoryResult> DeleteAsync(TEntity entity);
}