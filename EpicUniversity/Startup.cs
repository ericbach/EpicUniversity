using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EpicUniversity.Data;
using EpicUniversity.Models;
using EpicUniversity.Repository;
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
                o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();

            services.AddSwaggerGen();

            // Spring.NET - Services.xml, WebPages.xml, Dao.xml, etc.
            services.AddScoped<ICourseRepository, CourseRepository>();
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Epic Univesity");
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

    public static class UniversityContextMigrateAndSeed
    {
        public static void MigrateAndSeedData(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<UniversityContext>();

            // Ensure database is created
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            // Ensure any pending migrations are applied
            if (context.Database.GetPendingMigrations().Any())
                context.Database.Migrate();

            if (!context.Courses.Any(c => c.Name == "Epic programming"))
            {
                // Seed test data in database
                var course = new Course
                {
                    CreatedDate = DateTime.Today,
                    Name = "Epic programming",
                    Credits = 2
                };

                // EF
                context.Courses.Add(course);
                context.SaveChanges();
            }

            if (!context.CourseLabs.Any(c => c.Name == "Epic programming lab"))
            {
                var courseLab = new CourseLab
                {
                    CreatedDate = DateTime.Today,
                    Name = "Epic programming lab",
                    CourseId = 1
                };

                context.CourseLabs.Add(courseLab);
                context.SaveChanges();
            }

            if (!context.Personnel.Any(p => p.Id == 1))
            {
                // Seed test data in database
                var professor = new Professor
                {
                    CreatedDate = DateTime.Today,
                    FirstName = "Amy",
                    LastName = "TestP",
                    Birthdate = DateTime.Today
                };

                // EF
                context.Personnel.Add(professor);
                context.SaveChanges();
            }

            if (!context.Personnel.Any(p => p.Id == 2))
            {
                // Seed test data in database
                var student = new Student
                {
                    CreatedDate = DateTime.Today,
                    FirstName = "Sor",
                    LastName = "TestS",
                    Birthdate = DateTime.Today
                };

                // EF
                context.Personnel.Add(student);
                context.SaveChanges();
            }
        }

        // Unit of Work - single transaction that can involve many operations
        // Repository Pattern - dealing database actions - insert/update/delete

    }
}
