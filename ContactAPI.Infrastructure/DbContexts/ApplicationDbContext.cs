using AspNetCoreHero.Abstractions.Domain;
using AspNetCoreHero.EntityFrameworkCore.AuditTrail;
using ContactAPI.Application.Interfaces.Contexts;
using ContactAPI.Application.Interfaces.Shared;
using ContactAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContactAPI.Infrastructure.DbContexts
{
    public class ApplicationDbContext : AuditableContext, IApplicationDbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime, IAuthenticatedUserService authenticatedUser) : base(options)
        {
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
        }

        public DbSet<ContactSkill> ContactSkills { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public IDbConnection Connection => Database.GetDbConnection();
        public bool HasChanges => ChangeTracker.HasChanges();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = _dateTime.NowUtc;
                        entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = _dateTime.NowUtc;
                        entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        break;
                }
            }
            if (_authenticatedUser.UserId == null)
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            else
            {
                return await base.SaveChangesAsync(_authenticatedUser.UserId);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }
            //builder.Entity<AspNetCoreIdentity>()
            //.HasMany(h => (ICollection<Contact>)h.User)
            //.WithOne()
            //.HasForeignKey(p => p.UserIdentity);
            builder.Entity<ContactSkill>()
                .HasKey(cs => new { cs.UserIdentityId, cs.SkillId });
            builder.Entity<ContactSkill>()
                .HasOne(cs => cs.Contact)
                .WithMany(c => c.ContactSkills)
                .HasForeignKey(cs => cs.UserIdentityId);
            builder.Entity<ContactSkill>()
                .HasOne(cs => cs.Skill)
                .WithMany(s => s.ContactSkills)
                .HasForeignKey(bc => bc.SkillId);
            base.OnModelCreating(builder);
        }
    }
}