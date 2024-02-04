using System.Linq.Expressions;
using Typhoon.Core.Filters;

namespace Typhoon.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> AllAsNoTracking();
        IQueryable<TEntity> All();
        Task<TEntity?> FindAsync(int id);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> GetAsync(int id);
        Task<TEntity?> GetAsync(int id, params Expression<Func<TEntity, object>>[] includes);

        Task<IEnumerable<TResult>> GetAllAsync<TResult>();
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        Task<IEnumerable<TResult>> ListAsync<TResult>(BaseFilter<TEntity> filter);
    }
}
