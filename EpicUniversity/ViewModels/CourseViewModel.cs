namespace EpicUniversity.ViewModels
{
    public class CourseViewModel
    {
        public string Name { get; set; }
        public int Credits { get; set; }

        public int NumberOfStudents { get; set; }
    }

    public class CourseUpdateViewModel
    {
        public long Id { get; set; }
        public CourseViewModel Course { get; set; }
    }
}
