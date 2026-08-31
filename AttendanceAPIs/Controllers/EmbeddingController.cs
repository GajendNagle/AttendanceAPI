using Microsoft.AspNetCore.Mvc;
using PMPoshanWithAngular.Server.Data.CustomModels.Api;
using PMPoshanWithAngular.Server.Data.DAO;
using PMPoshanWithAngular.Server.Helper;

namespace AttendanceAPIs.Controllers
{
    [ApiController]
    [Route("api/Embedding")]
    public class EmbeddingController : ControllerBase
    {
        private readonly IDocPhotoService _photoService;
        private readonly ILogger<EmbeddingController> _logger;

        public EmbeddingController(IDocPhotoService photoService, ILogger<EmbeddingController> logger)
        {
            _photoService = photoService;
            _logger = logger;
        }

        [HttpPost("/emb/{pt}/{sg:guid}/{pg:guid}/{t:int}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadStudentEmbedding(
            string pt,
            Guid sg,
            Guid pg,
            int t)
        {
            try
            {
                GuidUpsertHelper.ValidateGuidNotEmpty(sg, "Student GUID");
                GuidUpsertHelper.ValidateGuidNotEmpty(pg, "Photo GUID");
                var bytes = await ReadUploadBytesAsync();

                if (bytes.Length > 0)
                {
                    await _photoService.UploadStudentEmbeddingAsync(pt, sg, pg, t, bytes);
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

        [HttpPost("/attendance/emb/{pg:guid}/{t:int}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadAttendanceEmbedding(Guid pg, int t)
        {
            try
            {
                GuidUpsertHelper.ValidateGuidNotEmpty(pg, "Photo GUID");
                var bytes = await ReadUploadBytesAsync();

                if (bytes.Length > 0)
                {
                    await _photoService.UploadAttendanceEmbeddingAsync(pg, t, bytes);
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
