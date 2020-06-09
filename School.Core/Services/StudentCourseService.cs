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
    public class StudentCourseService : IStudentCourseService
    {
        private readonly IRepository<StudentCourse> _repository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _loggerManager;
        public StudentCourseService(IRepository<StudentCourse> repository, IMapper mapper , ILoggerManager loggerManager)
        {
            _repository = repository;
            _mapper = mapper;
            _loggerManager = loggerManager;
        }
        
        public async Task<bool> Create(StudentCourseDto entity)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                var studCrs = _mapper.Map<StudentCourse>(entity);
                await _repository.Create(studCrs);
                var result = await _repository.SaveAsync();
                logger.LogInformation($"Degree {entity.Degree} of Student {studCrs.Id} is created.");
                return result;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                var studentCrs = await _repository.FindByCondition(i => i.Id == id);
                await _repository.Delete(studentCrs.First());
                var result = await _repository.SaveAsync();
                logger.LogInformation($"student degree {id} is deleted.");
                return result;
            }
        }

        public async Task<PagedList<StudentCourse>> FindAll(PagingParameters pagingParameters)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                return await _repository.FindAll(pagingParameters);
            }
        }

        public async Task<StudentCourseDto> FindByCode(int studentId)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                var studentCrss = await _repository.FindByCondition(d => d.StudId == studentId);
                var studentCrs = studentCrss.First();
                logger.LogInformation($"Degrees of student {studentId} is retreived");
                return _mapper.Map<StudentCourseDto>(studentCrs);
            }
        }

        public async Task<IEnumerable<StudentCourseDto>> FindByCondition(Expression<Func<StudentCourse, bool>> expression)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                var studCrs = await _repository.FindByCondition(expression);
                return _mapper.Map<IEnumerable<StudentCourseDto>>(studCrs);
            }
        }

        public async Task<bool> Update(StudentCourseDto entity)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                var studCrs = _mapper.Map<StudentCourse>(entity);
                await _repository.Update(studCrs);
                var result = await _repository.SaveAsync();
                logger.LogInformation($"Degree {entity.Degree} of Student {studCrs.Id} is updated.");
                return result;
            }
        }
    }
}
