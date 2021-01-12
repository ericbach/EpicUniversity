using System;
using System.Collections.Generic;
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

            if (context == null) return;

            // Ensure any pending migrations are applied
            if (context.Database.GetPendingMigrations().Any())
                context.Database.Migrate();

            #region student data

            var students = new List<Student>
            {
                new Student
                {
                    FirstName = "Sara",
                    LastName = "Charlie",
                    Birthdate = new DateTime(2006, 12, 31)
                },
                new Student
                {
                    FirstName = "Ali",
                    LastName = "Charlie",
                    Birthdate = new DateTime(2006, 6, 30)
                },
                new Student
                {
                    FirstName = "John",
                    LastName = "Elton",
                    Birthdate = new DateTime(2006, 5, 2)
                },
                new Student
                {
                    FirstName = "Nicola",
                    LastName = "Coleman",
                    Birthdate = new DateTime(2006, 3, 15)
                },
                new Student
                {
                    FirstName = "Cameron",
                    LastName = "Buckland",
                    Birthdate = new DateTime(2006, 7, 31)
                },
                new Student
                {
                    FirstName = "Sarah",
                    LastName = "Flynn",
                    Birthdate = new DateTime(2006, 12, 11)
                },
                new Student
                {
                    FirstName = "Paul",
                    LastName = "Hughes",
                    Birthdate = new DateTime(2006, 12, 31)
                },
                new Student
                {
                    FirstName = "Tara",
                    LastName = "Wilton",
                    Birthdate = new DateTime(2005, 1, 1),
                },
                new Student
                {
                    FirstName = "Sor",
                    LastName = "TestS",
                    Birthdate = new DateTime(2001, 05, 18)
                },
                new Student
                {
                    FirstName = "Grace",
                    LastName = "Robinson",
                    Birthdate = new DateTime(2002, 6, 17)
                }
            };
            
            foreach (var s in students.Where(s => !context.Students.Any(x => x.FirstName == s.FirstName)))
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            #endregion student data

            #region Professor data

            var professors = new List<Professor>
            {
                new Professor
                {
                    FirstName = "Bill",
                    LastName = "Gates",
                    ParkingSpot = 1,
                    Tenure = 15
                },
                new Professor
                {
                    FirstName = "Tim",
                    LastName = "Berners-Lee",
                    ParkingSpot = 2,
                    Tenure = 10
                },
                new Professor
                {
                    //As the creator of the Linux operating system
                    FirstName = "Linus",
                    LastName = "Torvalds",
                    ParkingSpot = 3,
                    Tenure = 25
                },
                new Professor
                {
                    //Ted Codd created 12 rules on which every relational database is built -
                    //an essential ingredient for building business computer systems.
                    FirstName = "Ted",
                    LastName = "Codd",
                    ParkingSpot = 4,
                    Tenure = 25
                },
                new Professor
                {
                    FirstName = "Amy",
                    LastName = "TestP",
                }
            };
            
            foreach (var p in professors.Where(p => !context.Professors.Any(x => x.FirstName == p.FirstName)))
            {
                context.Professors.Add(p);
            }
            context.SaveChanges();

            #endregion professors data

            #region Courses Data

            // Seed course test data in database

            var courses = new List<Course>
            {
                new Course
                {
                    CreatedDate = DateTime.Today,
                    Name = "Epic programming",
                    Credits = 5,
                    Professor = professors[0],
                    Students = new List<Student>
                    {
                        students[0],
                        students[1],
                        students[2],
                        students[3],
                        students[4]
                    }
                },
                new Course
                {
                    CreatedDate = DateTime.Today,
                    Name = "C# programming",
                    Credits = 3,

                },
                new Course
                {
                    CreatedDate = DateTime.Today,
                    Name = "Database programming",
                    Credits = 3
                },
                new Course
                {
                    CreatedDate = DateTime.Today,
                    Name = "Networking",
                    Credits = 3
                },
                new Course
                {
                    CreatedDate = DateTime.Today,
                    Name = "Java programming",
                    Credits = 3
                },
                new Course
                {
                    CreatedDate = DateTime.Today,
                    Name = "Communication and organization",
                    Credits = 3
                },
                new Course
                {
                    CreatedDate = DateTime.Today,
                    Name = "Distributed programming",
                    Credits = 3
                }
            };

            foreach (var c in courses.Where(c => !context.Courses.Any(x => x.Name == c.Name)))
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            #endregion Courses Data

            #region Course Labs Data

            if (!context.CourseLabs.Any(c => c.Name == "Epic programming lab"))
            {
                var courseLab = new CourseLab
                {
                    CreatedDate = DateTime.Today,
                    Name = "Epic programming lab",
                    Course = courses[0]
                };

                context.CourseLabs.Add(courseLab);
                context.SaveChanges();
            }

            #endregion Course Labs Data
        }

        // Unit of Work - single transaction that can involve many operations
        // Repository Pattern - dealing database actions - insert/update/delete
    }
}
