using Microsoft.EntityFrameworkCore.Storage;

namespace Typhoon.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task<IDbContextTransaction> BeginTransactionAsync();

        Task<int> SaveChanges();
    }
}
