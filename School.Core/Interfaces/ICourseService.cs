using School.Core.Dtos;
using School.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace School.Core.Interfaces
{
    public interface ICourseService
    {
        Task<PagedList<Course>> FindAll(PagingParameters pagingParameters);
        Task<IEnumerable<CourseDto>> FindByCondition(Expression<Func<Course, bool>> expression);
        Task<CourseDto> FindByCode(int code);
        Task<bool> Create(CourseDto entity);
        Task<bool> Update(CourseDto entity);
        Task<bool> Delete(int code);
    }
}
