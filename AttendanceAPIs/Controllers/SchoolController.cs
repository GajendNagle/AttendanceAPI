using Microsoft.AspNetCore.Mvc;
using PMPoshanWithAngular.Server.Data.CustomModels.Api;
using PMPoshanWithAngular.Server.Data.CustomeModels.Reponse;
using PMPoshanWithAngular.Server.Data.DAO;

namespace AttendanceAPIs.Controllers
{
    [Route("api/School")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly IDocSchoolService _schoolService;
        private readonly ILogger<SchoolController> _logger;

        public SchoolController(
            IDocSchoolService schoolService,
            ILogger<SchoolController> logger)
        {
            _schoolService = schoolService;
            _logger = logger;
        }

        [HttpGet]
        [Route("hello")]
        public IActionResult Hello()
        {
            return Ok("Hello World !!1");
        }

        [HttpPost("SchoolList")]
        public async Task<IActionResult> GetSchoolList()
        {
            try
            {
                var schools = await _schoolService.GetSchoolsAsync();
                if (schools != null && schools.Count > 0)
                {
                    return Ok(schools);
                }
                else
                {
                    return NotFound(new ApiErrorResponse(
                        "Not Found",
                        "No schools found."));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, new ApiErrorResponse(
                    "Internal Server Error",
                    ServiceResponseConstants.GENERIC_OOPS));
            }
        }
    }
}
