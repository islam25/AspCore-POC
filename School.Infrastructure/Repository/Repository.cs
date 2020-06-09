using Microsoft.EntityFrameworkCore;
using School.Core.Interfaces;
using School.Core.Models;
using School.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace School.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        protected SchoolContext context { get; set; }

        public Repository(SchoolContext schoolContext)
        {
            context = schoolContext;
        }
        public Task Create(T entity)
        {
            return Task.FromResult(context.Set<T>().Add(entity));
        }

        public Task Delete(T entity)
        {
            return Task.FromResult(context.Set<T>().Remove(entity));
        }

        public async Task<PagedList<T>> FindAll(PagingParameters pagingParameters)
        {
            var result = context.Set<T>().AsNoTracking();
            return PagedList<T>.ToPagedList(result, pagingParameters.PageNumber, pagingParameters.PageSize);
        }

        public async Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return await context.Set<T>().AsNoTracking().Where(expression).ToListAsync();
        }

        public Task Update(T entity)
        {
            return Task.FromResult(context.Set<T>().Update(entity));
        }

        public async Task<bool> SaveAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
