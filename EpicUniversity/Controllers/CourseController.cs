using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpicUniversity.Data;
using EpicUniversity.Models;
using EpicUniversity.Repository;

namespace EpicUniversity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : Controller
    {
        public UniversityContext Context;
        public IRepository<Course> CourseRepository;

        public CourseController(UniversityContext context, IRepository<Course> courseRepository)
        {
            Context = context;
            CourseRepository = courseRepository;
        }

        // localhost/course/
        [HttpGet()]
        public ActionResult<List<Course>> GetAll()
        {
            // Select * from Courses
            return Ok(Context.Courses.ToList());
        }

        // localhost/course/generic
        [HttpGet("generic")]
        public ActionResult<List<Course>> GetAllGeneric()
        {
            // Select * from Courses
            return Ok(CourseRepository.GetAll());
        }

        [HttpPost]
        public IActionResult Create([FromBody]Course courseDetails)
        {
            Context.Courses.Add(courseDetails); // insert into Courses(Id, Name, etc) values(courseDetails.Id, courseDetails.Name, etc)
            Context.SaveChanges();

            return Ok();
        }
    }
}
