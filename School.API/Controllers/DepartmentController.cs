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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILoggerManager _loggerManager;
        public DepartmentController(IDepartmentService departmentService, ILoggerManager loggerManager)
        {
            _departmentService = departmentService;
            _loggerManager = loggerManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PagingParameters pagingParameters) 
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                try
                {
                    var departments = await _departmentService.FindAll(pagingParameters);
                    logger.LogInformation("Departments are retreived");
                    var metadata = new
                    {
                        departments.TotalCount,
                        departments.PageSize,
                        departments.CurrentPage,
                        departments.TotalPages,
                        departments.HasNext,
                        departments.HasPrevious
                    };
                    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                    if (departments != null)
                        return Ok(departments);

                    return NoContent();
                }
                catch (Exception ex)
                {
                    logger.LogError("Something happend while getting departments", ex);
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
                    var departments = await _departmentService.FindByCondition(d => d.Code == code);
                    if (departments != null)
                    {
                        logger.LogInformation($"Department of {code} is retreived");
                        return Ok(departments);
                    }

                    return NoContent();
                }
                catch (Exception ex)
                {
                    logger.LogError("Something happend while getting departments", ex);
                    return StatusCode(500);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(DepartmentDto departmentDto) 
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                try
                {
                    if (!ModelState.IsValid)
                        return BadRequest();

                    var departmentCreated = await _departmentService.Create(departmentDto);
                    if (departmentCreated)
                    {
                        logger.LogInformation($"Department {departmentDto.Title} is added");
                        return Ok();
                    }
                    return StatusCode(500);
                }
                catch (Exception ex)
                {
                    logger.LogError("Something happend while adding departments", ex);
                    return StatusCode(500);
                }
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]DepartmentDto departmentDto)
        {
            using (var logger = _loggerManager.CreateLogger())
            {
                try
                {
                    if (!ModelState.IsValid)
                        return BadRequest();

                    var departmentCreated = await _departmentService.Update(departmentDto);
                    if (departmentCreated)
                    {
                        logger.LogInformation($"Department {departmentDto.Code} is updated");
                        return Ok();
                    }
                    return StatusCode(500);
                }
                catch (Exception ex)
                {
                    logger.LogError("Something happend while updating departments", ex);
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

                    var departmentCreated = await _departmentService.Delete(code);
                    if (departmentCreated)
                    {
                        logger.LogInformation($"Department {code} is deleted");
                        return Ok();
                    }
                    return StatusCode(500);
                }
                catch (Exception ex)
                {
                    logger.LogError($"Something happend while deleting department {code}", ex);
                    return StatusCode(500);
                }
            }
        }

    }
}