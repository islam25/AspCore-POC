using System.Collections.Generic;

namespace School.API.ViewModels
{
    public class StudentCoursesViewModel
    {
        public int StudentId { get; set; }
        public List<CoursesDegree> CoursesDegrees { get; set; }

        public StudentCoursesViewModel()
        {
            CoursesDegrees = new List<CoursesDegree>();
        }
    }

    public class CoursesDegree
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public decimal Degree { get; set; }
    }
}
