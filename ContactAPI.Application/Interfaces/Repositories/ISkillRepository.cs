using ContactAPI.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAPI.Application.Interfaces.Repositories
{
    public interface ISkillRepository
    {
        IQueryable<Skill> Skills { get; }

        Task<List<Skill>> GetListAsync();

        Task<Skill> GetByIdAsync(int skillId);

        Task<int> InsertAsync(Skill skill);

        Task UpdateAsync(Skill skill);

        Task DeleteAsync(Skill skill);
    }
}