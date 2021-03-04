using System.Collections.Generic;

namespace EpicUniversity.ViewModels
{
    public class StudentViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<CourseViewModel> Courses { get; set; } = new List<CourseViewModel>();
    }
}
