using Microsoft.EntityFrameworkCore;
using SocialNetworkingKata.Infrastructure.Data.Interfaces;
using SocialNetworkingKata.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetworkingKata.Infrastructure.Data.Implementations.EntityFrameworkCore
{

    public class EfRepository<T> : IAsyncRepository<T> where T : BaseEntity, IAggregateRoot
    {
        private readonly EfUnitOfWork _unitOfWork;
        private readonly DbSet<T> _dbSet;
        public IUnitOfWork UnitOfWork => _unitOfWork;

        public EfRepository(EfUnitOfWork unitOfWork)
        {
            _unitOfWork = _unitOfWork == null ? throw new NotImplementedException() : unitOfWork;
            _dbSet = (_unitOfWork.GetContext() as DbContext).Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            var e = await _dbSet.AddAsync(entity);
            return e.Entity;
        }

        public Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);

            return Task.CompletedTask;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);

            return Task.CompletedTask;
        }
    }
}
