using ContactAPI.Application.Interfaces.Repositories;
using ContactAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAPI.Infrastructure.Repositories
{
    public class ContactSkillRepository : IContactSkillRepository
    {
        private readonly IRepositoryAsync<ContactSkill> _csRepository;
        private readonly IRepositoryAsync<Contact> _cRepository;
        private readonly IRepositoryAsync<Skill> _sRepository;
        private readonly IDistributedCache _distributedCache;

        public ContactSkillRepository(IDistributedCache distributedCache, IRepositoryAsync<ContactSkill> csRepository,
            IRepositoryAsync<Contact> cRepository, IRepositoryAsync<Skill> sRepository)
        {
            _distributedCache = distributedCache;
            _csRepository = csRepository;
            _cRepository = cRepository;
            _sRepository = sRepository;
        }

        public IQueryable<ContactSkill> ContactSkills => _csRepository.Entities.Include(c=>c.Contact).Include(c=>c.Skill);

        public async Task DeleteAsync(ContactSkill contactSkill)
        {
            await _csRepository.DeleteAsync(contactSkill);
            await _distributedCache.RemoveAsync(CacheKeys.ContactSkillCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.ContactSkillCacheKeys.GetKey(contactSkill.UserIdentityId, contactSkill.SkillId));
        }
        
        public async Task<ContactSkill> GetByIdsAsync(string UserIdentityId, int SkillId)
        {
            return await _csRepository.Entities.Where(p => p.UserIdentityId == UserIdentityId && p.SkillId == SkillId)
                .Include(c => c.Contact).Include(c => c.Skill).FirstOrDefaultAsync();
        }
        public async Task<List<ContactSkill>> GetByIdAsync(string UserIdentityId)
        {
            return await _csRepository.Entities.Where(p => p.UserIdentityId == UserIdentityId)
                .Include(c => c.Contact).Include(c => c.Skill).ToListAsync();
        }

        public async Task<List<ContactSkill>> GetListAsync()
        {
            return await _csRepository.Entities.ToListAsync();
        }

        public async Task<string> InsertAsync(ContactSkill contactSkill)
        {
            await _csRepository.AddAsync(contactSkill);
            await _distributedCache.RemoveAsync(CacheKeys.ContactSkillCacheKeys.ListKey);
            return contactSkill.UserIdentityId;
        }

        public async Task UpdateAsync(ContactSkill contactSkill)
        {
            await _csRepository.UpdateAsync(contactSkill);
            await _distributedCache.RemoveAsync(CacheKeys.ContactSkillCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.ContactSkillCacheKeys.GetKey(contactSkill.UserIdentityId, contactSkill.SkillId));
        }
    }
}