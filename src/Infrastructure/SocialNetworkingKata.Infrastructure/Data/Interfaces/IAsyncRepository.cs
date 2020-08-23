using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using SocialNetworkingKata.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SocialNetworkingKata.Infrastructure.Data.Interfaces
{
    public interface IAsyncRepository<T> where T : EntityBase, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IReadOnlyList<T>> FindAsync(
                                          Expression<Func<T, bool>> predicate,
                                          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                          Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                          bool disableTracking = true);
    }
}
