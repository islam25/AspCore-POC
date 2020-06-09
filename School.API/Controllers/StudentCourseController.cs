using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using School.API.ViewModels;
using School.Core.Dtos;
using School.Core.ILoggerService;
using School.Core.Interfaces;
using School.Core.Models;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentCourseController : ControllerBase
    {
        private readonly IStudentCourseService _studentCourseService;
        private readonly ILoggerManager _loggerManager;
        public StudentCourseController(IStudentCourseService studentCourseService, ILoggerManager loggerManager)
        {
            _studentCourseService = studentCourseService;
            _loggerManager = loggerManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PagingParameters pagingParameters)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                try
                {
                    var studentCrss = await _studentCourseService.FindAll(pagingParameters);
                    logger.LogInformation("studentCrss are retreived");
                    var metadata = new
                    {
                        studentCrss.TotalCount,
                        studentCrss.PageSize,
                        studentCrss.CurrentPage,
                        studentCrss.TotalPages,
                        studentCrss.HasNext,
                        studentCrss.HasPrevious
                    };
                    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                    if (studentCrss != null)
                        return Ok(studentCrss);

                    return NoContent();
                }
                catch (Exception ex)
                {
                    logger.LogError("Something happend while getting studentCrss", ex);
                    return StatusCode(500);
                }
            }
        }

        [HttpGet("student")]
        public async Task<IActionResult> Get(int studentId)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                try
                {
                    var studentCrs = await _studentCourseService.FindByCondition(d => d.StudId == studentId);

                    var studentCrsModel = new StudentCoursesViewModel();
                    studentCrsModel.StudentId = studentId;
                    foreach (var item in studentCrs)
                        studentCrsModel.CoursesDegrees.Add(new CoursesDegree { Id = item.Id, CourseId = item.CrsId , Degree = item.Degree });

                    if (studentCrsModel.CoursesDegrees.Any())
                    {
                        logger.LogInformation($"studentCrs of {studentId} is retreived");
                        return Ok(studentCrsModel);
                    }

                    return NoContent();
                }
                catch (Exception ex)
                {
                    logger.LogError("Something happend while getting studentCrs", ex);
                    return StatusCode(500);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(StudentCoursesViewModel studentCoursesViewModel)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                try
                {
                    if (!ModelState.IsValid)
                        return BadRequest();

                    foreach (var courseDegree in studentCoursesViewModel.CoursesDegrees)
                    {
                        await _studentCourseService.Create(new StudentCourseDto 
                        {
                            StudId = studentCoursesViewModel.StudentId ,
                            CrsId = courseDegree.CourseId,
                            Degree = courseDegree.Degree
                        });
                    }
                    logger.LogInformation($"All degrees for student {studentCoursesViewModel.StudentId} is added.");
                    return Ok();
                }
                catch (Exception ex)
                {
                    logger.LogError("Something happend while adding studentCrs", ex);
                    return StatusCode(500);
                }
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]StudentCoursesViewModel studentCoursesViewModel)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                try
                {
                    if (!ModelState.IsValid)
                        return BadRequest();

                    foreach (var courseDegree in studentCoursesViewModel.CoursesDegrees)
                    {
                        await _studentCourseService.Update(new StudentCourseDto
                        {
                            Id = courseDegree.Id,
                            StudId = studentCoursesViewModel.StudentId,
                            CrsId = courseDegree.CourseId,
                            Degree = courseDegree.Degree
                        });
                    }

                    logger.LogInformation($"StudentCrs {studentCoursesViewModel.StudentId} is updated");
                    return Ok();
                }
                catch (Exception ex)
                {
                    logger.LogError("Something happend while updating student", ex);
                    return StatusCode(500);
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                try
                {
                    if (!ModelState.IsValid)
                        return BadRequest();

                    var studentCrsDeleted = await _studentCourseService.Delete(id);
                    if (studentCrsDeleted)
                    {
                        logger.LogInformation($"Student {id} is deleted");
                        return Ok();
                    }
                    return StatusCode(500);
                }
                catch (Exception ex)
                {
                    logger.LogError($"Something happend while deleting studentCrs {id}", ex);
                    return StatusCode(500);
                }
            }
        }

    }
}