using SocialNetworkingKata.Infrastructure.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetworkingKata.Infrastructure.Data.Interfaces
{
    public interface IAsyncRepository<T> where T : BaseEntity, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
