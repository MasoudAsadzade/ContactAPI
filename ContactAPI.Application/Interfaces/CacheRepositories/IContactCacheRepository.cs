using ContactAPI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactAPI.Application.Interfaces.CacheRepositories
{
    public interface IContactCacheRepository
    {
        Task<List<Contact>> GetCachedListAsync();
        Task<List<Contact>> GetByUseIdAsync(string userId);
        Task<Contact> GetByIdAsync(int contactId);
    }
}