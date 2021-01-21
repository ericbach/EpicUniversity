namespace EpicUniversity.Models
{
    public class Grade : Entity
    {
        public decimal Gpa { get; set; }
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}