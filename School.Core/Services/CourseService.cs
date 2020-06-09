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
    public class CourseService: ICourseService
    {
        private readonly IRepository<Course> _repository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _loggerManager;
        public CourseService(IRepository<Course> repository, IMapper mapper, ILoggerManager loggerManager)
        {
            _loggerManager = loggerManager;
            _repository = repository;
            _mapper = mapper;
        }
       
        public async Task<bool> Create(CourseDto entity)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                var course = _mapper.Map<Course>(entity);
                await _repository.Create(course);
                var result =  await _repository.SaveAsync();

                logger.LogInformation($"Course {course.Code} is added.");
                return result;
            }
        }

        public async Task<bool> Delete(int code)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                var course = await _repository.FindByCondition(r => r.Code == code);
                await _repository.Delete(course.First());
                var result = await _repository.SaveAsync();

                logger.LogInformation($"Course {course.First().Code} is deleted.");
                return result;
            }
        }

        public async Task<PagedList<Course>> FindAll(PagingParameters pagingParameters)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                var courses = await _repository.FindAll(pagingParameters);
                logger.LogInformation($"Courses are retreived.");
                return courses;
            }
        }

        public async Task<CourseDto> FindByCode(int code)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                var courses = await _repository.FindByCondition(d => d.Code == code);
                var course = courses.ToList()[0];
                logger.LogInformation($"Course {course.Code} is retreived.");
                return _mapper.Map<CourseDto>(course);
            }
        }

        public async Task<IEnumerable<CourseDto>> FindByCondition(Expression<Func<Course, bool>> expression)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                var courses = await _repository.FindByCondition(expression);
                logger.LogInformation($"Courses are retreived.");
                return _mapper.Map<IEnumerable<CourseDto>>(courses);
            }
        }

        public async Task<bool> Update(CourseDto entity)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                var course = _mapper.Map<Course>(entity);
                await _repository.Update(course);
                var result = await _repository.SaveAsync();
                logger.LogInformation($"Course {course.Code} is updated.");
                return result;
            }
        }
    }
}
