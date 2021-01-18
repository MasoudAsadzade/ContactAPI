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
    public class ContactSkillCacheRepository : IContactSkillCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IContactSkillRepository _contactSkillRepository;

        public ContactSkillCacheRepository(IDistributedCache distributedCache, IContactSkillRepository contactSkillRepository)
        {
            _distributedCache = distributedCache;
            _contactSkillRepository = contactSkillRepository;
        }

        public async Task<List<ContactSkill>> GetByIdAsync(string UserIdentityId)
        {
            string cacheKey = ContactSkillCacheKeys.GetKey(UserIdentityId);
            var contactSkill = await _distributedCache.GetAsync<List<ContactSkill>>(cacheKey);
            if (contactSkill == null)
            {
                contactSkill = await _contactSkillRepository.GetByIdAsync(UserIdentityId);
                Throw.Exception.IfNull(contactSkill, "ContactSkill", "No ContactSkill Found");
                await _distributedCache.SetAsync(cacheKey, contactSkill);
            }
            return contactSkill;
        }
        public async Task<ContactSkill> GetByIdsAsync(string UserIdentityId, int SkillId)
        {
            string cacheKey = ContactSkillCacheKeys.GetKey(UserIdentityId, SkillId);
            var contactSkill = await _distributedCache.GetAsync<ContactSkill>(cacheKey);
            if (contactSkill == null)
            {
                contactSkill = await _contactSkillRepository.GetByIdsAsync(UserIdentityId, SkillId);
                Throw.Exception.IfNull(contactSkill, "ContactSkill", "No ContactSkill Found");
                await _distributedCache.SetAsync(cacheKey, contactSkill);
            }
            return contactSkill;
        }
        public async Task<List<ContactSkill>> GetCachedListAsync()
        {
            string cacheKey = ContactSkillCacheKeys.ListKey;
            var contactSkillList = await _distributedCache.GetAsync<List<ContactSkill>>(cacheKey);
            if (contactSkillList == null)
            {
                contactSkillList = await _contactSkillRepository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, contactSkillList);
            }
            return contactSkillList;
        }
    }
}