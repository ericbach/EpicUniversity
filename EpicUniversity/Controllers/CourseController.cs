using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using EpicUniversity.Data;
using EpicUniversity.Models;
using EpicUniversity.Repository;
using Newtonsoft.Json;

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
            var course = CourseRepository.GetIncludingProfessorsStudents(id);

            if (course == null)
                return NotFound();
            
            return Ok(JsonConvert.SerializeObject(course, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));
        }

        // localhost/course/
        [HttpGet()]
        public ActionResult<List<Course>> GetAll()
        {
            return Ok(Context.Courses.ToList());
        }

        // localhost/course/generic
        [HttpGet("generic")]
        public ActionResult<List<Course>> GetAllGeneric()
        {
            return Ok(CourseRepository.GetAll());
        }

        [HttpPost]
        public IActionResult Create([FromBody]Course courseDetails)
        {
            CourseRepository.Add(courseDetails);
            Context.SaveChanges();

            return Ok();
        }
    }
}
