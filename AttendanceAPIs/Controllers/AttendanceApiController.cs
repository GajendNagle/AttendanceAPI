using Microsoft.AspNetCore.Mvc;
using PMPoshanWithAngular.Server.Data.CustomModels.Api;
using PMPoshanWithAngular.Server.Data.DAO;

namespace AttendanceAPIs.Controllers
{
    [ApiController]
    [Route("api/attendance")]
    public class AttendanceApiController : ControllerBase
    {
        private readonly IDocAttendanceService _attendanceService;
        private readonly ILogger<AttendanceApiController> _logger;

        public AttendanceApiController(
            IDocAttendanceService attendanceService,
            ILogger<AttendanceApiController> logger)
        {
            _attendanceService = attendanceService;
            _logger = logger;
        }

        [HttpGet]
        [Route("hello")]
        public IActionResult Hello()
        {
            return Ok("Hello World !!1");
        }

        [HttpPost("/attendance")]
        public async Task<IActionResult> SubmitAttendance([FromBody] ApiAttendanceDto request)
        {
            try
            {
                if (request != null)
                {
                    await _attendanceService.SubmitAttendanceAsync(request);
                    return Ok();
                }
                else
                {
                    return BadRequest(new ApiErrorResponse(
                        "Invalid Request Payload",
                        "Missing required attendance fields."));
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiErrorResponse("Invalid Request Payload", ex.Message));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiErrorResponse("Not Found", ex.Message));
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
