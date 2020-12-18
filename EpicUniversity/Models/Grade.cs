namespace EpicUniversity.Models
{
    public class Grade : Entity
    {
        public decimal Gpa { get; set; }
        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}