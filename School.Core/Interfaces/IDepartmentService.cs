using School.Core.Dtos;
using School.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace School.Core.Interfaces
{
    public interface  IDepartmentService
    {
        Task<PagedList<Department>> FindAll(PagingParameters pagingParameters);
        Task<List<DepartmentDto>> FindByCondition(Expression<Func<Department, bool>> expression);
        Task<DepartmentDto> FindByCode(int code);
        Task<bool> Create(DepartmentDto entity);
        Task<bool> Update(DepartmentDto entity);
        Task<bool> Delete(int code);
    }
}
