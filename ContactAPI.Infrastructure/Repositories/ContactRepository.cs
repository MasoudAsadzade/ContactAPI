using ContactAPI.Application.Interfaces.Repositories;
using ContactAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAPI.Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly IRepositoryAsync<Contact> _repository;
        private readonly IDistributedCache _distributedCache;

        public ContactRepository(IDistributedCache distributedCache, IRepositoryAsync<Contact> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Contact> Contacts => _repository.Entities;

        public async Task DeleteAsync(Contact contact)
        {
            await _repository.DeleteAsync(contact);
            await _distributedCache.RemoveAsync(CacheKeys.ContactCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.ContactCacheKeys.GetKey(contact.UserIdentityId));
        }

        public async Task<Contact> GetByIdAsync(int contactId)
        {
            return await _repository.Entities.Where(p => p.Id == contactId).FirstOrDefaultAsync();
        }
        public async Task<List<Contact>> GetByUserIdAsync(string UserIdentityId)
        {
            return await _repository.Entities.Where(p => p.UserIdentityId == UserIdentityId).ToListAsync();
        }
        
        public async Task<List<Contact>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<string> InsertAsync(Contact contact)
        {
            await _repository.AddAsync(contact);
            await _distributedCache.RemoveAsync(CacheKeys.ContactCacheKeys.ListKey);
            return contact.Id.ToString();
        }

        public async Task UpdateAsync(Contact contact)
        {
            await _repository.UpdateAsync(contact);
            await _distributedCache.RemoveAsync(CacheKeys.ContactCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.ContactCacheKeys.GetKey(contact.UserIdentityId));
        }
    }
}