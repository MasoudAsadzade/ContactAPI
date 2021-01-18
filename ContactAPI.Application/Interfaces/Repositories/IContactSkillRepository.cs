using ContactAPI.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAPI.Application.Interfaces.Repositories
{
    public interface IContactSkillRepository
    {
        IQueryable<ContactSkill> ContactSkills { get; }

        Task<List<ContactSkill>> GetListAsync();

        Task<List<ContactSkill>> GetByIdAsync(string UserIdentityId);

        Task<ContactSkill> GetByIdsAsync(string UserIdentityId, int SkillId);

        Task<string> InsertAsync(ContactSkill contactContactSkill);

        Task UpdateAsync(ContactSkill contactContactSkill);

        Task DeleteAsync(ContactSkill contactContactSkill);
    }
}