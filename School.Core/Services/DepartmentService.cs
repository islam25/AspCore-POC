using AutoMapper;
using School.Core.Dtos;
using School.Core.ILoggerService;
using School.Core.Interfaces;
using School.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace School.Core.Services
{
    public class DepartmentService: IDepartmentService
    {
        private readonly IRepository<Department> _repository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _loggerManager;
        public DepartmentService(IRepository<Department> repository, IMapper mapper, ILoggerManager loggerManager)
        {
            _repository = repository;
            _mapper = mapper;
            _loggerManager = loggerManager;
        }
        
        public async Task<bool> Create(DepartmentDto entity)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                var department = _mapper.Map<Department>(entity);
                await _repository.Create(department);
                var result = await _repository.SaveAsync();
                logger.LogInformation($"Department {department.Code} is added.");
                return result;
            }
        }

        public async Task<bool> Delete(int code)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                var department = await _repository.FindByCondition(d => d.Code == code);
                await _repository.Delete(department.First());
                var result = await _repository.SaveAsync();
                logger.LogInformation($"Department {department.First().Code} is deleted.");
                return result;
            }
        }

        public async Task<PagedList<Department>> FindAll(PagingParameters pagingParameters)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                var department = await _repository.FindAll(pagingParameters);
                logger.LogInformation($"departments are retreived.");
                return department;
            }
        }

        public async Task<DepartmentDto> FindByCode(int code)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                var departments = await _repository.FindByCondition(d => d.Code == code);
                var department = departments.First();
                logger.LogInformation($"Department {department.Code} is retreived.");
                return _mapper.Map<DepartmentDto>(department);
            }
        }

        public async Task<List<DepartmentDto>> FindByCondition(Expression<Func<Department, bool>> expression)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                var courses = await _repository.FindByCondition(expression);
                return _mapper.Map<List<DepartmentDto>>(courses.ToList());
            }
        }

        public async Task<bool> Update(DepartmentDto entity)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                var department = _mapper.Map<Department>(entity);
                await _repository.Update(department);
                var result = await _repository.SaveAsync();
                logger.LogInformation($"Department {department.Code} is updated.");
                return result;
            }
        }
    }
}
