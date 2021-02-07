using Boxi.Dal;
using Boxi.Dal.Interfaces;
using Boxi.Dal.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Boxi.Api.MiddleWare
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IBoxRepository, BoxRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            //Register any explicit repository implementations here.
            return services;
        }
    }
}