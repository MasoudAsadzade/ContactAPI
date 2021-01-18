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
    public class SkillCacheRepository : ISkillCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ISkillRepository _skillRepository;

        public SkillCacheRepository(IDistributedCache distributedCache, ISkillRepository skillRepository)
        {
            _distributedCache = distributedCache;
            _skillRepository = skillRepository;
        }

        public async Task<Skill> GetByIdAsync(int skillId)
        {
            string cacheKey = SkillCacheKeys.GetKey(skillId);
            var skill = await _distributedCache.GetAsync<Skill>(cacheKey);
            if (skill == null)
            {
                skill = await _skillRepository.GetByIdAsync(skillId);
                Throw.Exception.IfNull(skill, "Skill", "No Skill Found");
                await _distributedCache.SetAsync(cacheKey, skill);
            }
            return skill;
        }

        public async Task<List<Skill>> GetCachedListAsync()
        {
            string cacheKey = SkillCacheKeys.ListKey;
            var skillList = await _distributedCache.GetAsync<List<Skill>>(cacheKey);
            if (skillList == null)
            {
                skillList = await _skillRepository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, skillList);
            }
            return skillList;
        }
    }
}