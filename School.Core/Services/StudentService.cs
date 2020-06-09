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
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> _repository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _loggerManager;
        public StudentService(IRepository<Student> repository, IMapper mapper, ILoggerManager loggerManager)
        {
            _repository = repository;
            _mapper = mapper;
            _loggerManager = loggerManager;
        }
        public async Task<bool> Create(StudentDto entity)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                var student = _mapper.Map<Student>(entity);
                await _repository.Create(student);
                var result = await _repository.SaveAsync();
                logger.LogInformation($"Student {student.Id} is added.");
                return result;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                var student = await _repository.FindByCondition(s => s.Id == id);
                await _repository.Delete(student.First());
                var result = await _repository.SaveAsync();
                logger.LogInformation($"Student {student.First().Id} is deleted.");
                return result;
            }
        }

        public async Task<PagedList<Student>> FindAll(PagingParameters pagingParameters)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                var students = await _repository.FindAll(pagingParameters);
                logger.LogInformation($"Students are retreived.");
                return students;
            }
        }

        public async Task<StudentDto> FindByCode(int id)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                var students = await _repository.FindByCondition(d => d.Id == id);
                logger.LogInformation($"Student {id} is retreived.");
                return _mapper.Map<StudentDto>(students.First());
            }
        }

        public async Task<IEnumerable<StudentDto>> FindByCondition(Expression<Func<Student, bool>> expression)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                var students = await _repository.FindByCondition(expression);
                return _mapper.Map<IEnumerable<StudentDto>>(students);
            }
        }

        public async Task<bool> Update(StudentDto entity)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                var student = _mapper.Map<Student>(entity);
                await _repository.Update(student);
                var result = await _repository.SaveAsync();
                logger.LogInformation($"Student {student.Id} is updated.");
                return result;
            }
        }

    }
}
