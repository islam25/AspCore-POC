using School.Core.Dtos;
using School.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace School.Core.Interfaces
{
    public interface IStudentService
    {
        Task<PagedList<Student>> FindAll(PagingParameters pagingParameters);
        Task<IEnumerable<StudentDto>> FindByCondition(Expression<Func<Student, bool>> expression);
        Task<StudentDto> FindByCode(int id);
        Task<bool> Create(StudentDto entity);
        Task<bool> Update(StudentDto entity);
        Task<bool> Delete(int id);
    }
}
