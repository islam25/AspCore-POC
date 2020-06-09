using School.Core.Dtos;
using School.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace School.Core.Interfaces
{
    public interface IStudentCourseService
    {
        Task<PagedList<StudentCourse>> FindAll(PagingParameters pagingParameters);
        Task<IEnumerable<StudentCourseDto>> FindByCondition(Expression<Func<StudentCourse, bool>> expression);
        Task<StudentCourseDto> FindByCode(int studentId);
        Task<bool> Create(StudentCourseDto entity);
        Task<bool> Update(StudentCourseDto entity);
        Task<bool> Delete(int id);
    }
}
