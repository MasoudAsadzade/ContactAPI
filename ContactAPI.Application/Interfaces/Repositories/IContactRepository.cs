using ContactAPI.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAPI.Application.Interfaces.Repositories
{
    public interface IContactRepository
    {
        IQueryable<Contact> Contacts { get; }

        Task<List<Contact>> GetListAsync();

        Task<Contact> GetByIdAsync(int contactId);

        Task<List<Contact>> GetByUserIdAsync(string UserIdentityId);

        Task<string> InsertAsync(Contact contact);

        Task UpdateAsync(Contact contact);

        Task DeleteAsync(Contact contact);
    }
}