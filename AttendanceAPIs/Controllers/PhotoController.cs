using Microsoft.AspNetCore.Mvc;
using PMPoshanWithAngular.Server.Data.CustomModels.Api;
using PMPoshanWithAngular.Server.Data.DAO;
using PMPoshanWithAngular.Server.Helper;

namespace AttendanceAPIs.Controllers
{
    [ApiController]
    [Route("api/Photo")]
    public class PhotoController : ControllerBase
    {
        private readonly IDocPhotoService _photoService;
        private readonly ILogger<PhotoController> _logger;

        public PhotoController(IDocPhotoService photoService, ILogger<PhotoController> logger)
        {
            _photoService = photoService;
            _logger = logger;
        }

        [HttpPost("/ph/{pt}/{sg:guid}/{pg:guid}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadStudentPhoto(string pt, Guid sg, Guid pg)
        {
            try
            {
                GuidUpsertHelper.ValidateGuidNotEmpty(sg, "Student GUID");
                GuidUpsertHelper.ValidateGuidNotEmpty(pg, "Photo GUID");
                var bytes = await ReadUploadBytesAsync();

                if (bytes.Length > 0)
                {
                    await _photoService.UploadStudentPhotoAsync(pt, sg, pg, bytes);
                    return Ok();
                }
                else
                {
                    return BadRequest(new ApiErrorResponse(
                        "Invalid Request Payload",
                        "File is required."));
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
                    "Server failed to store the upload."));
            }
        }

        [HttpPost("/attendance/ph/{pg:guid}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadAttendancePhoto(Guid pg)
        {
            try
            {
                GuidUpsertHelper.ValidateGuidNotEmpty(pg, "Photo GUID");
                var bytes = await ReadUploadBytesAsync();

                if (bytes.Length > 0)
                {
                    await _photoService.UploadAttendancePhotoAsync(pg, bytes);
                    return Ok();
                }
                else
                {
                    return BadRequest(new ApiErrorResponse(
                        "Invalid Request Payload",
                        "File is required."));
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
                    "Server failed to store the upload."));
            }
        }

        private async Task<byte[]> ReadUploadBytesAsync()
        {
            if (Request.HasFormContentType)
            {
                var form = await Request.ReadFormAsync();
                if (form.Files.Count > 0)
                {
                    await using var stream = form.Files[0].OpenReadStream();
                    using var ms = new MemoryStream();
                    await stream.CopyToAsync(ms);
                    return ms.ToArray();
                }
                else
                {
                    return Array.Empty<byte>();
                }
            }
            else
            {
                using var ms = new MemoryStream();
                await Request.Body.CopyToAsync(ms);
                return ms.ToArray();
            }
        }
    }
}
