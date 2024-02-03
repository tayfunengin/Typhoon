using Microsoft.EntityFrameworkCore;

namespace Typhoon.Respository.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
