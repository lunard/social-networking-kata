using System.Threading.Tasks;

namespace SocialNetworkingKata.Infrastructure.Data.Interfaces
{
    public interface IUnitOfWork
    {
        object GetContext();
        Task<bool> SaveChangesAsync();
    }
}
