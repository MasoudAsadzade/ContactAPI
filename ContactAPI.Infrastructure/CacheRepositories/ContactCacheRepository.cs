using AspNetCoreHero.Extensions.Caching;
using AspNetCoreHero.ThrowR;
using ContactAPI.Application.Interfaces.CacheRepositories;
using ContactAPI.Application.Interfaces.Repositories;
using ContactAPI.Domain.Entities;
using ContactAPI.Infrastructure.CacheKeys;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactAPI.Infrastructure.CacheRepositories
{
    public class ContactCacheRepository : IContactCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IContactRepository _contactRepository;

        public ContactCacheRepository(IDistributedCache distributedCache, IContactRepository contactRepository)
        {
            _distributedCache = distributedCache;
            _contactRepository = contactRepository;
        }

        public async Task<Contact> GetByIdAsync(int contactId)
        {
            string cacheKey = ContactCacheKeys.GetKey(contactId);
            var contact = await _distributedCache.GetAsync<Contact>(cacheKey);
            if (contact == null)
            {
                contact = await _contactRepository.GetByIdAsync(contactId);
                Throw.Exception.IfNull(contact, "Contact", "No Contact Found");
                await _distributedCache.SetAsync(cacheKey, contact);
            }
            return contact;
        }
        public async Task<List<Contact>> GetByUseIdAsync(string userId)
        {
            string cacheKey = ContactCacheKeys.GetKey(userId);
            var contact = await _distributedCache.GetAsync<List<Contact>>(cacheKey);
            if (contact == null)
            {
                contact = await _contactRepository.GetByUserIdAsync(userId);
                Throw.Exception.IfNull(contact, "Contact", "No Contact Found");
                await _distributedCache.SetAsync(cacheKey, contact);
            }
            return contact;
        }
        
        public async Task<List<Contact>> GetCachedListAsync()
        {
            string cacheKey = ContactCacheKeys.ListKey;
            var contactList = await _distributedCache.GetAsync<List<Contact>>(cacheKey);
            if (contactList == null)
            {
                contactList = await _contactRepository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, contactList);
            }
            return contactList;
        }
    }
}