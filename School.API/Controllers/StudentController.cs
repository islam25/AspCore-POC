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
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILoggerManager _loggerManager;
        public StudentController(IStudentService studentService, ILoggerManager loggerManager)
        {
            _studentService = studentService;
            _loggerManager = loggerManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PagingParameters pagingParameters)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                try
                {
                    var students = await _studentService.FindAll(pagingParameters);
                    logger.LogInformation("Students are retreived");
                    var metadata = new
                    {
                        students.TotalCount,
                        students.PageSize,
                        students.CurrentPage,
                        students.TotalPages,
                        students.HasNext,
                        students.HasPrevious
                    };
                    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                    if (students != null)
                        return Ok(students);

                    return NoContent();
                }
                catch (Exception ex)
                {
                    logger.LogError("Something happend while getting students", ex);
                    return StatusCode(500);
                }
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                try
                {
                    var student = await _studentService.FindByCondition(d => d.Id == id);
                    if (student != null)
                    {
                        logger.LogInformation($"Course of {id} is retreived");
                        return Ok(student);
                    }

                    return NoContent();
                }
                catch (Exception ex)
                {
                    logger.LogError("Something happend while getting students", ex);
                    return StatusCode(500);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(StudentDto studentDto)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                try
                {
                    if (!ModelState.IsValid)
                        return BadRequest();

                    var studentCreated = await _studentService.Create(studentDto);
                    if (studentCreated)
                    {
                        logger.LogInformation($"Student {studentDto.Name} is added");
                        return Ok();
                    }
                    return StatusCode(500);
                }
                catch (Exception ex)
                {
                    logger.LogError("Something happend while adding student", ex);
                    return StatusCode(500);
                }
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]StudentDto studentDto)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                try
                {
                    if (!ModelState.IsValid)
                        return BadRequest();

                    var studentCreated = await _studentService.Update(studentDto);
                    if (studentCreated)
                    {
                        logger.LogInformation($"Student {studentDto.Id} is updated");
                        return Ok();
                    }
                    return StatusCode(500);
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

                    var studentCreated = await _studentService.Delete(id);
                    if (studentCreated)
                    {
                        logger.LogInformation($"Student {id} is deleted");
                        return Ok();
                    }
                    return StatusCode(500);
                }
                catch (Exception ex)
                {
                    logger.LogError($"Something happend while deleting student {id}", ex);
                    return StatusCode(500);
                }
            }
        }

    }
}