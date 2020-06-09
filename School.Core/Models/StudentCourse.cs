using System.ComponentModel.DataAnnotations.Schema;

namespace School.Core.Models
{
    public class StudentCourse
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Student))]
        public int StudId { get; set; }
        [ForeignKey(nameof(Course))]
        public int CrsId { get; set; }
        public decimal Degree { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
