using ContactAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ContactAPI.Application.Interfaces.Contexts
{
    public interface IApplicationDbContext
    {
        IDbConnection Connection { get; }
        bool HasChanges { get; }
        EntityEntry Entry(object entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        public DbSet<ContactSkill> ContactSkills { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<Contact> Contacts { get; set; }

    }
}