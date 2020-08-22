using Microsoft.EntityFrameworkCore;
using SocialNetworkingKata.Infrastructure.Data.Interfaces;
using System.Threading.Tasks;

namespace SocialNetworkingKata.Infrastructure.Data.Implementations.EntityFrameworkCore
{
    public abstract class EfUnitOfWork : IUnitOfWork
    {
        private DbContext _context;
        public EfUnitOfWork()
        {
            _context = CreateDbContext();
        }

        public abstract DbContext CreateDbContext();

        public object GetContext() => _context;

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
