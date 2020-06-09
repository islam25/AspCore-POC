using AutoMapper;
using School.Core.Dtos;
using School.Core.Models;

namespace School.Core.Mapping
{
    public class SchoolProfile: Profile
    {
        public SchoolProfile()
        {
            CreateMap<StudentDto, Student>();
            CreateMap<Student, StudentDto>();
            
            CreateMap<CourseDto, Course>();
            CreateMap<Course, CourseDto>();
            
            CreateMap<StudentCourseDto, StudentCourse>();
            CreateMap<StudentCourse, StudentCourseDto>();

            CreateMap<DepartmentDto, Department>();
            CreateMap<Department, DepartmentDto>();
        }
    }
}
