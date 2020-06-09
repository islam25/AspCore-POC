using System.ComponentModel.DataAnnotations;

namespace School.Core.Models
{
    public class Department
    {
        [Key()]
        public int Code { get; set; }
        public string Title { get; set; }
    }
}
