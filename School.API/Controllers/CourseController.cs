using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using School.Core.Dtos;
using School.Core.ILoggerService;
using School.Core.Interfaces;
using School.Core.Models;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly ILoggerManager _loggerManager;
        public CourseController(ICourseService courseService, ILoggerManager loggerManager)
        {
            _courseService = courseService;
            _loggerManager = loggerManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PagingParameters pagingParameters)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                try
                {
                    var courses = await _courseService.FindAll(pagingParameters);
                    logger.LogInformation("Course are retreived");
                    var metadata = new
                    {
                        courses.TotalCount,
                        courses.PageSize,
                        courses.CurrentPage,
                        courses.TotalPages,
                        courses.HasNext,
                        courses.HasPrevious
                    };
                    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                    if (courses != null)
                        return Ok(courses);

                    return NoContent();
                }
                catch (Exception ex)
                {
                    logger.LogError("Something happend while getting courses", ex);
                    return StatusCode(500);
                }
            }
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> Get(int code)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                try
                {
                    var courses = await _courseService.FindByCondition(d => d.Code == code);
                    if (courses != null)
                    {
                        logger.LogInformation($"Course of {code} is retreived");
                        return Ok(courses);
                    }

                    return NoContent();
                }
                catch (Exception ex)
                {
                    logger.LogError("Something happend while getting courses", ex);
                    return StatusCode(500);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(CourseDto courseDto)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                try
                {
                    if (!ModelState.IsValid)
                        return BadRequest();

                    var courseCreated = await _courseService.Create(courseDto);
                    if (courseCreated)
                    {
                        logger.LogInformation($"Course {courseDto.Title} is added");
                        return Ok();
                    }
                    return StatusCode(500);
                }
                catch (Exception ex)
                {
                    logger.LogError("Something happend while adding courses", ex);
                    return StatusCode(500);
                }
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]CourseDto courseDto)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                try
                {
                    if (!ModelState.IsValid)
                        return BadRequest();

                    var courseCreated = await _courseService.Update(courseDto);
                    if (courseCreated)
                    {
                        logger.LogInformation($"Course {courseDto.Code} is updated");
                        return Ok();
                    }
                    return StatusCode(500);
                }
                catch (Exception ex)
                {
                    logger.LogError("Something happend while updating course", ex);
                    return StatusCode(500);
                }
            }
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> Delete(int code)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                try
                {
                    if (!ModelState.IsValid)
                        return BadRequest();

                    var courseCreated = await _courseService.Delete(code);
                    if (courseCreated)
                    {
                        logger.LogInformation($"Course {code} is deleted");
                        return Ok();
                    }
                    return StatusCode(500);
                }
                catch (Exception ex)
                {
                    logger.LogError($"Something happend while deleting course {code}", ex);
                    return StatusCode(500);
                }
            }
        }

    }
}