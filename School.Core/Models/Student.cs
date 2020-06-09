using System.ComponentModel.DataAnnotations.Schema;

namespace School.Core.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Address { get; set; }

        [ForeignKey(nameof(Department))]
        public int DeptId { get; set; }
        public Department Department { get; set; }
    }
}
