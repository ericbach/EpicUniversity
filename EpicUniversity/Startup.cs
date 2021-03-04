using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EpicUniversity.Data;
using EpicUniversity.Repository;
using EpicUniversity.Repository.Impl;
using Microsoft.EntityFrameworkCore;

namespace EpicUniversity
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
            services.AddDbContext<UniversityContext>(o =>
            {
                o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddControllers();

            services.AddSwaggerGen();

            // Add services (like Spring.NET - Services.xml, WebPages.xml, Dao.xml, etc)
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IProfessorRepository, ProfessorRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            //RegisterServices(services, typeof(Repository<>), typeof(IRepository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Seed database
                app.MigrateAndSeedData();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Epic University");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public static void RegisterServices(IServiceCollection services, Type baseClass, Type baseInterface)
        {
            services.Scan(scan => scan
                .FromAssemblies(baseClass.GetTypeInfo().Assembly)
                .AddClasses(classes => classes.Where(x =>
                {
                    var allInterfaces = x.GetInterfaces();
                    return allInterfaces.Any(y => y.GetTypeInfo().IsGenericType &&
                                                  y.GetTypeInfo().GetGenericTypeDefinition() == baseInterface);
                }))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );
        }
    }
}
