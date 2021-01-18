using ContactAPI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactAPI.Application.Interfaces.CacheRepositories
{
    public interface ISkillCacheRepository
    {
        Task<List<Skill>> GetCachedListAsync();

        Task<Skill> GetByIdAsync(int skillId);
    }
}