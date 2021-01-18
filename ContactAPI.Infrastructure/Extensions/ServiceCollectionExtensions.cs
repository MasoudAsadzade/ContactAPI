using ContactAPI.Application.Interfaces.CacheRepositories;
using ContactAPI.Application.Interfaces.Contexts;
using ContactAPI.Application.Interfaces.Repositories;
using ContactAPI.Infrastructure.CacheRepositories;
using ContactAPI.Infrastructure.DbContexts;
using ContactAPI.Infrastructure.Repositories;
using AutoMapper;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ContactAPI.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistenceContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<MapsterMapper.IMapper, MapsterMapper.Mapper>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            #region Repositories

            services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IContactCacheRepository, ContactCacheRepository>();
            services.AddTransient<IContactSkillRepository, ContactSkillRepository>();
            services.AddTransient<IContactSkillCacheRepository, ContactSkillCacheRepository>();
            services.AddTransient<ISkillRepository, SkillRepository>();
            services.AddTransient<ISkillCacheRepository, SkillCacheRepository>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            #endregion Repositories
        }
    }
}