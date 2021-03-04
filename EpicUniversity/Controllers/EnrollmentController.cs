using Microsoft.AspNetCore.Mvc;
using System.Linq;
using EpicUniversity.Services;
using EpicUniversity.ViewModels;

namespace EpicUniversity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnrollmentController : Controller
    {
        public IEnrollmentService EnrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            EnrollmentService = enrollmentService;
        }

        //// localhost/enrollment/courseId=1
        //[HttpGet("courseId={courseId}")]
        //public ActionResult<CourseViewModel> Get([FromRoute] long courseId)
        //{
        //    var course = CourseRepository.GetIncludingProfessorsStudents(id);

        //    if (course == null)
        //        return NotFound();

        //    var courseVm = Mapper.Map<Course, CourseViewModel>(course);
        //    return Ok(courseVm);
        //}
        
        //// localhost/course/
        //[HttpGet()]
        //public ActionResult<List<CourseViewModel>> GetAll()
        //{
        //    var courses = CourseRepository.GetAll().ToList();

        //    //var courseViewModels = Mapper.Map<List<Course>, List<CourseViewModel>>(courses);
        //    var courseViewModels = courses.Select(course => Mapper.Map<Course, CourseViewModel>(course)).ToList();

        //    return Ok(courseViewModels);
        //}

        [HttpPost]
        public ActionResult<EnrollmentResponse> Enroll([FromBody] EnrollmentViewModel enrollment)
        {
            var enrollmentResponse = new EnrollmentResponse();

            if (enrollment.StudentId <= 0)
                return BadRequest("StudentId is required");

            if (enrollment.CourseId <= 0)
                return BadRequest("CourseId is required");

            var result = EnrollmentService.Enroll(enrollment.StudentId, enrollment.CourseId);

            enrollmentResponse.Errors = result.ToList();

            return Ok(enrollmentResponse);
        }
    }
}
