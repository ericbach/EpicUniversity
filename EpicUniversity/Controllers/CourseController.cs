using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using EpicUniversity.Data;
using EpicUniversity.Models;
using EpicUniversity.Repository;

namespace EpicUniversity.Controllers
{
    [ApiController]
    [Route("[controller]")] // [Route("course")]
    public class CourseController : Controller
    {
        public UniversityContext Context;
        public ICourseRepository CourseRepository;

        public CourseController(UniversityContext context, ICourseRepository courseRepository)
        {
            Context = context;
            CourseRepository = courseRepository;
        }

        // localhost/course/1
        [HttpGet("{id}")]
        public ActionResult<Course> Get([FromRoute]long id)
        {
            var course = CourseRepository.Get(id);

            if (course == null)
                return NotFound();
            
            return Ok(CourseRepository.Get(id));
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
