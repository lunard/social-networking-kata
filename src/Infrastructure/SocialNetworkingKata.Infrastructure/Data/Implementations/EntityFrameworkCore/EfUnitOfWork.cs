using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocialNetworkingKata.Infrastructure.Data.Interfaces;
using System;
using System.Threading.Tasks;

namespace SocialNetworkingKata.Infrastructure.Data.Implementations.EntityFrameworkCore
{
    public abstract class EfUnitOfWork : IUnitOfWork
    {
        private DbContext _context;

        protected readonly IConfiguration _configuration;
        public EfUnitOfWork(
            IConfiguration configuration
            )
        {
            _configuration = configuration == null ? throw new NotImplementedException() : configuration;
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
