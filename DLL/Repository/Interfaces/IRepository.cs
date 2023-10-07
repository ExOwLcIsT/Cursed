using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;

namespace Cursed.Repository.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task<IReadOnlyCollection<TEntity>> GetAllAsync();
        Task<IReadOnlyCollection<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression);
        Task<bool> Exists(Expression<Func<TEntity, bool>> expression);
        Task AddAsync(TEntity entity);
        Task Update(TEntity entity);
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression);
        Task DeleteAsync(Expression<Func<TEntity, bool>> expression);
        Task SaveAsync();
    }
}
