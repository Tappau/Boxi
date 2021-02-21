using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using Boxi.Api.MiddleWare;
using Boxi.Common.Handlers.CommandHandlers;
using Boxi.Core.Commands;
using Boxi.Core.Domain;
using Boxi.Core.Queries;
using Boxi.Dal.Models;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Boxi.Api", Version = "v1"}); });

            services.AddDbContext<BoxiDataContext>(options =>
            {
                var sqlDbName = Configuration.GetConnectionString("SqliteDatabase").Split("=")[1];
                var connectionString =
                    $"Data Source={new FileInfo(Assembly.GetExecutingAssembly().Location).Directory?.FullName}\\{sqlDbName}";
                options.UseSqlite(connectionString, builder => { builder.CommandTimeout(30); });
            });

            //Custom extension to add repositories
            services.AddDataAccessLayer();
            //This is here to set up a IDbConnection for Dapper usage to be injected
            //services.AddTransient<IDbConnection>((sp) =>
            //    new SqlConnection(Configuration.GetConnectionString("InventoryDatabase")));

            services.AddMediatR(typeof(CreateBoxCommand), typeof(CreateBoxCommandHandler));
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
                context?.Database.EnsureDeleted();
                context?.Database.EnsureCreated();

                var boxes = new List<Box>
                {
                    new ("DC Comic Box 1", "Contains DC Comics")
                    {
                        Items = new List<Item>
                        {
                            new("Detective Comics #1"), 
                            new ("Wonder Woman #286")
                        }
                    }, 
                    new ("Marvel Comic Box 1", "Contains Marvel Comics", "QR DATA")
                    {
                        Items = new List<Item>
                        {
                            new ("X-Men #188")
                        }
                    }
                };

                context?.Box.AddRangeAsync(boxes);
                context?.SaveChangesAsync();
            }

            app.UseConsistantApiResponses();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}