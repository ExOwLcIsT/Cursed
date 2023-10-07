using Cursed.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cursed.Repository.Interfaces
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbSet<TEntity> _entities;
        protected DbSet<TEntity> Entities => this._entities ??= _context.Set<TEntity>();
        protected GameContext _context;

        public BaseRepository(GameContext context) => _context = context;


        public async Task AddAsync(TEntity entity) => await this.Entities.AddAsync(entity).ConfigureAwait(false);

        public virtual async Task<IReadOnlyCollection<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression) => await this.Entities.Where(expression).ToListAsync().ConfigureAwait(false);
        public virtual async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression) => await this.Entities.FirstAsync(expression).ConfigureAwait(false);
        public virtual async Task<IReadOnlyCollection<TEntity>> GetAllAsync() => await this.Entities.ToListAsync().ConfigureAwait(false);

        public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> expression) => this.Entities.Remove(await FirstAsync(expression));

        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> expression) => await this.Entities.AnyAsync(expression);

        public virtual async Task SaveAsync() => await _context.SaveChangesAsync().ConfigureAwait(false);

        public virtual async Task Update(TEntity entity) => this.Entities.Update(entity);
    }
}
