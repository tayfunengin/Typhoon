using Microsoft.EntityFrameworkCore.Storage;
using Typhoon.Core.Repositories;
using Typhoon.Respository.Context;

namespace Typhoon.Respository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _contex;

        public UnitOfWork(ApplicationDbContext contex)
        {
            this._contex = contex;
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _contex.Database.BeginTransactionAsync();
        }

        public void Dispose()
        {
            _contex.Dispose();
        }

        public async Task<int> SaveChanges()
        {
            return await _contex.SaveChangesAsync();
        }
    }
}
