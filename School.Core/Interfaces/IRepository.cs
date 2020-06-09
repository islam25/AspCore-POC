using School.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace School.Core.Interfaces
{
    public interface IRepository<T>
        where T: class
    {
        Task<PagedList<T>> FindAll(PagingParameters pagingParameters);
        Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<bool> SaveAsync();
    }
}
