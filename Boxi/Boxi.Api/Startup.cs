using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Boxi.Api.MiddleWare;
using Boxi.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace Boxi.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Boxi.Api", Version = "v1" });
            });
            
            services.AddDbContext<BoxiDataContext>(options =>
            {
                var sqlDbName = Configuration.GetConnectionString("SqliteDatabase").Split("=")[0];
                var test = $"Data Source={new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName}\\{sqlDbName}";
                options.UseSqlite(Configuration.GetConnectionString("SqliteDatabase"), builder =>
                {
                    builder.CommandTimeout(30);
                });
            });

            //Custom extension to add repositories
            services.AddDataAccessLayer();
            //This is here to set up a IDbConnection for Dapper usage to be injected
            //services.AddTransient<IDbConnection>((sp) =>
            //    new SqlConnection(Configuration.GetConnectionString("InventoryDatabase")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Boxi.Api v1"));
            }

            using (var services = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = services.ServiceProvider.GetRequiredService<BoxiDataContext>();
                if (context != null)
                {
                    context.Database.EnsureCreated();
                }
            }

            //app.UseConsistantApiResponses();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
