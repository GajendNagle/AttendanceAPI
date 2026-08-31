using Microsoft.AspNetCore.Mvc;
using PMPoshanWithAngular.Server.Data.CustomModels.Api;
using PMPoshanWithAngular.Server.Data.DAO;

namespace AttendanceAPIs.Controllers
{
    [ApiController]
    [Route("api/Students")]
    public class StudentsController : ControllerBase
    {
        private readonly IDocStudentService _studentService;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(IDocStudentService studentService, ILogger<StudentsController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        [HttpGet]
        [Route("hello")]
        public IActionResult Hello()
        {
            return Ok("Hello World !!1");
        }


        [HttpPost("/students")]
        public async Task<IActionResult> SaveStudents([FromBody] List<ApiStudentDto> students)
        {
            try
            {
                if (students != null 
                    && students.Count > 0)
                {
                    var result = await _studentService.SaveStudentsAsync(students);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ApiErrorResponse(
                        "Invalid Request Payload",
                        "Student array is required."));
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiErrorResponse("Invalid Request Payload", ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, new ApiErrorResponse(
                    "Internal Server Error",
                    "Server failed to process the request."));
            }
        }
    }
}
