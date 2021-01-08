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

            #region Courses Data

            // Seed course test data in database


            var courses = new Course[6];
            courses[0] = new Course
            {
                CreatedDate = DateTime.Today,
                Name = "C# programming",
                Credits = 3

            };
            courses[1] = new Course
            {
                CreatedDate = DateTime.Today,
                Name = "Database programming",
                Credits = 3
            };
            courses[2] = new Course
            {
                CreatedDate = DateTime.Today,
                Name = "Networking",
                Credits = 3
            };
            courses[3] = new Course
            {
                CreatedDate = DateTime.Today,
                Name = "Java programming",
                Credits = 3
            };
            courses[4] = new Course
            {
                CreatedDate = DateTime.Today,
                Name = "Communication and organization",
                Credits = 3
            };
            courses[5] = new Course
            {
                CreatedDate = DateTime.Today,
                Name = "Distributed programming",
                Credits = 3
            };


            // EF

            foreach (Course c in courses)
            {
                if (!context.Courses.Any(x => x.Name == c.Name))
                {
                    context.Courses.Add(c);
                }

            }
            context.SaveChanges();

            #endregion Courses Data

            #region student data
            var students = new Student[8];
            students[0] = new Student
            {
                CreatedDate = DateTime.Today,
                FirstName = "Sara",
                LastName = "Charlie",
                Birthdate = Convert.ToDateTime("12/31/2006"),
                Gpa = 3.5M

            };
            students[1] = new Student
            {
                CreatedDate = DateTime.Today,
                FirstName = "Ali",
                LastName = "Charlie",
                Birthdate = Convert.ToDateTime("6/30/2006"),
                Gpa = 4.0M

            };
            students[2] = new Student
            {
                CreatedDate = DateTime.Today,
                FirstName = "John",
                LastName = "Elton",
                Birthdate = Convert.ToDateTime("05/02/2006"),
                Gpa = 3.5M

            };
            students[3] = new Student
            {
                CreatedDate = DateTime.Today,
                FirstName = "Nicola",
                LastName = "Coleman",
                Birthdate = Convert.ToDateTime("03/15/2006"),
                Gpa = 3.5M

            };
            students[4] = new Student
            {
                CreatedDate = DateTime.Today,
                FirstName = "Cameron",
                LastName = "Buckland",
                Birthdate = Convert.ToDateTime("07/31/2006"),
                Gpa = 3.5M

            };
            students[5] = new Student
            {
                CreatedDate = DateTime.Today,
                FirstName = "Sarah",
                LastName = "Flynn",
                Birthdate = Convert.ToDateTime("12/11/2006"),
                Gpa = 3.5M

            };
            students[6] = new Student
            {
                CreatedDate = DateTime.Today,
                FirstName = "Paul",
                LastName = "Hughes",
                Birthdate = Convert.ToDateTime("12/31/2006"),
                Gpa = 4.3M

            };
            students[7] = new Student
            {
                CreatedDate = DateTime.Today,
                FirstName = "Tara",
                LastName = "Wilton",
                Birthdate = Convert.ToDateTime("1/1/2005"),
                Gpa = 5.0M

            };
            foreach (Student s in students)
            {
                if (!context.Students.Any(x => x.FirstName == s.FirstName))
                {
                    context.Students.Add(s);
                }
            }
            context.SaveChanges();

            #endregion student data

            #region Professor data
            var professors = new Professor[4];
            professors[0] = new Professor
            {
                FirstName = "Bill",
                LastName = "Gates",
                ParkingSpot = 1,
                Tenure = 15

            };
            professors[1] = new Professor
            {
                FirstName = "Tim",
                LastName = "Berners-Lee",
                ParkingSpot = 2,
                Tenure = 10

            };
            professors[2] = new Professor
            {
                //As the creator of the Linux operating system
                FirstName = "Linus",
                LastName = "Torvalds",
                ParkingSpot = 3,
                Tenure = 25

            };
            professors[3] = new Professor
            {
                //Ted Codd created 12 rules on which every relational database is built -
                //an essential ingredient for building business computer systems.
                FirstName = "Ted",
                LastName = "Codd",
                ParkingSpot = 4,
                Tenure = 25

            };
            foreach (Professor p in professors)
            {
                if (!context.Professors.Any(x => x.FirstName == p.FirstName))
                {
                    context.Professors.Add(p);
                }
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
            context.SaveChanges();

            #endregion professors data

        }

        // Unit of Work - single transaction that can involve many operations
        // Repository Pattern - dealing database actions - insert/update/delete

    }
}
