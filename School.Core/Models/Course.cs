using System.ComponentModel.DataAnnotations;

namespace School.Core.Models
{
    public class Course
    {
        [Key()]
        public int Code { get; set; }
        public string Title { get; set; }
    }
}
