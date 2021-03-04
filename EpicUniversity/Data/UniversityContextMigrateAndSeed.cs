using System;
using System.Collections.Generic;
using System.Linq;
using EpicUniversity.Data;
using EpicUniversity.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EpicUniversity.Data
{
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
                new Student("Sara", "Charlie", new DateTime(2006, 12, 31)),
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

            foreach (var s in students.Where(s => !context.Students.Any(x => x.FirstName == s.FirstName && x.LastName == s.LastName && x.Birthdate == s.Birthdate)))
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

            professors = context.Professors.ToList();
            students = context.Students.ToList();   // select * from Students
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
                        students[1]
                    }
                },
                new Course
                {
                    CreatedDate = DateTime.Today,
                    Name = "C# programming",
                    Credits = 3,
                    Professor = professors[1],
                    Students = new List<Student>
                    {
                        students[0],
                        students[1],
                        students[4]
                    }
                },
                new Course
                {
                    CreatedDate = DateTime.Today,
                    Name = "Database programming",
                    Credits = 3,
                    Professor = professors[3],
                    Students = new List<Student>
                    {
                        students[0],
                        students[6],
                        students[8]
                    }
                },
                new Course
                {
                    CreatedDate = DateTime.Today,
                    Name = "Networking",
                    Credits = 3,
                    Professor = professors[4],
                    Students = new List<Student>
                    {
                        students[7],
                        students[9]
                    }
                },
                new Course
                {
                    CreatedDate = DateTime.Today,
                    Name = "Java programming",
                    Credits = 3,
                    Professor = professors[3],
                    Students = new List<Student>
                    {
                        students[0]
                    }
                },
                new Course
                {
                    CreatedDate = DateTime.Today,
                    Name = "Communication and organization",
                    Credits = 3,
                    Professor = professors[2],
                    Students = new List<Student>
                    {
                        students[0]
                    }
                },
                new Course
                {
                    CreatedDate = DateTime.Today,
                    Name = "Distributed programming",
                    Credits = 3,
                    Professor = professors[1]
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