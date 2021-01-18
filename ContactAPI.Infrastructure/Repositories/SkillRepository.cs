using ContactAPI.Application.Interfaces.Repositories;
using ContactAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAPI.Infrastructure.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly IRepositoryAsync<Skill> _repository;
        private readonly IDistributedCache _distributedCache;

        public SkillRepository(IDistributedCache distributedCache, IRepositoryAsync<Skill> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Skill> Skills => _repository.Entities;

        public async Task DeleteAsync(Skill skill)
        {
            await _repository.DeleteAsync(skill);
            await _distributedCache.RemoveAsync(CacheKeys.SkillCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.SkillCacheKeys.GetKey(skill.SkillId));
        }

        public async Task<Skill> GetByIdAsync(int skillId)
        {
            return await _repository.Entities.Where(p => p.SkillId == skillId).FirstOrDefaultAsync();
        }

        public async Task<List<Skill>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Skill skill)
        {
            await _repository.AddAsync(skill);
            await _distributedCache.RemoveAsync(CacheKeys.SkillCacheKeys.ListKey);
            return skill.SkillId;
        }

        public async Task UpdateAsync(Skill skill)
        {
            await _repository.UpdateAsync(skill);
            await _distributedCache.RemoveAsync(CacheKeys.SkillCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.SkillCacheKeys.GetKey(skill.SkillId));
        }
    }
}