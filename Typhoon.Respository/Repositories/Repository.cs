using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Typhoon.Core;
using Typhoon.Core.Extensions;
using Typhoon.Core.Filters;
using Typhoon.Core.Repositories;
using Typhoon.Respository.Context;

namespace Typhoon.Respository.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext Context;
        private readonly IMapper _mapper;

        public Repository(ApplicationDbContext context, IMapper mapper)
        {
            this.Context = context;
            this._mapper = mapper;
        }
        public async Task AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
        }
        public async Task<TEntity?> FindAsync(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>()
        {
            return await Context.Set<TEntity>()
                    .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                    .ToListAsync();
        }

        public async Task<TEntity?> GetAsync(int id)
        {
            return await Context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TEntity?> GetAsync(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Context.Set<TEntity>().AsQueryable();
            foreach (var include in includes)
                query = query.Include(include);

            return await query.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<TResult>> ListAsync<TResult>(BaseFilter<TEntity> filter)
        {
            return await Context.Set<TEntity>().AsNoTracking()
                .ApplyFilter(filter)
                .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(BaseFilter<TEntity> filter)
        {
            return await Context.Set<TEntity>().AsNoTracking()
                .ApplyFilter(filter)
                .CountAsync();
        }

        public void Remove(TEntity entity)
        {
            Context.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.RemoveRange(entities);
        }
    }
}
