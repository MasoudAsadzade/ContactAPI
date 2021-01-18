using ContactAPI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactAPI.Application.Interfaces.CacheRepositories
{
    public interface IContactSkillCacheRepository
    {
        Task<List<ContactSkill>> GetCachedListAsync();
        Task<List<ContactSkill>> GetByIdAsync(string UserIdentityId);
        Task<ContactSkill> GetByIdsAsync(string UserIdentityId, int SkillId);
    }
}