using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using SocialNetworkingKata.Infrastructure.Data.Interfaces;
using SocialNetworkingKata.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SocialNetworkingKata.Infrastructure.Data.Implementations.EntityFrameworkCore
{

    public class EfRepository<T> : IAsyncRepository<T> where T : EntityBase, IAggregateRoot
    {
        private readonly EfUnitOfWork _unitOfWork;
        private readonly DbSet<T> _dbSet;
        public IUnitOfWork UnitOfWork => _unitOfWork;

        public EfRepository(EfUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork == null ? throw new NotImplementedException() : unitOfWork;
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

        public async Task UpdateAsync(T entity)
        {
            var local = (_unitOfWork.GetContext() as DbContext).Set<T>().Local.FirstOrDefault(entry => entry.Id.Equals(entity.Id));

            // check if local is not null 
            if (local != null)
            {
                // detach
                (_unitOfWork.GetContext() as DbContext).Entry(local).State = EntityState.Detached;
                await _unitOfWork.SaveChangesAsync();
            }
            _dbSet.Update(entity);
        }

        public async Task<IReadOnlyList<T>> FindAsync(
                                          Expression<Func<T, bool>> predicate,
                                          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                          Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                          bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }
    }
}
