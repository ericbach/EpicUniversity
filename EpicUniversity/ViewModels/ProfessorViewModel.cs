using System;

namespace EpicUniversity.ViewModels
{
    public class ProfessorViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Tenure { get; set; }
        public int ParkingSpot { get; set; }
        public virtual int NumberOfCoursesOfferedByProfessor { get; set; }
        public DateTime Birthdate { get; set; }
    }
}