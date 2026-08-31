using Microsoft.AspNetCore.Mvc;
using PMPoshanWithAngular.Server.Data.CustomModels.Api;
using PMPoshanWithAngular.Server.Data.CustomeModels.Reponse;
using PMPoshanWithAngular.Server.Helper;
using PMPoshanWithAngular.Server.JwtTokenModel;
using Microsoft.Extensions.Options;

namespace AttendanceAPIs.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;
        public AuthController(AuthService authService,
            ILogger<AuthController> logger,
             IConfiguration configuration)
        {
            _authService = authService;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("hello")]
        public IActionResult Hello()
        {
            string abcURL = Environment.GetEnvironmentVariable( "AbcBaseURL",EnvironmentVariableTarget.Machine);
            string value = _configuration["AePDS:AePDSBaseURL"];
            return Ok(value);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin model)
        {
            try
            {
                if (model != null
                    && !string.IsNullOrWhiteSpace(model.username)
                    && !string.IsNullOrWhiteSpace(model.password))
                {
                    var user = _authService.ValidateUser(model.username, model.password);
                    if (user != null)
                    {
                        return Ok(new
                        {
                            success = true,
                            username = user.Username,
                            name = user.Name,
                            teacher_guid = user.TeacherGuid,
                            school_guid = user.SchoolGuid,
                            school_name = user.SchoolName
                        });
                    }
                    else
                    {
                        return NotFound(new ApiErrorResponse(
                            "Not Found",
                            ServiceResponseConstants.USER_REGISTRATION_INVALID_USERNAME_OR_PASSWORD));
                    }
                }
                else
                {
                    return BadRequest(new ApiErrorResponse(
                        "Invalid Request Payload",
                        "Username and password are required."));
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
