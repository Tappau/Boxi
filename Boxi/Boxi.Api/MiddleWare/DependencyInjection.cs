using Boxi.Dal;
using Boxi.Dal.Interfaces;
using Boxi.Dal.Models;
using Boxi.Dal.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Boxi.Api.MiddleWare
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IBoxStoreRepository, BoxStoreRepository>();
            services.AddTransient<IPublisherRepository, PublisherRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            //Register any explicit repository implementations here.
            return services;
        }
    }
}