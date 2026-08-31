using NHibernate.Linq;
using PMPoshanWithAngular.Server.Data.Models;
using PMPoshanWithAngular.Server.Helper;
using ISessionFactory = NHibernate.ISessionFactory;

namespace PMPoshanWithAngular.Server.Data.DAO
{
    public interface IDocPhotoService
    {
        Task UploadStudentPhotoAsync(string photoTypeShortcode, Guid studentGuid, Guid photoGuid, byte[] fileBytes);
        Task UploadStudentEmbeddingAsync(string photoTypeShortcode, Guid studentGuid, Guid photoGuid, int embeddingType, byte[] fileBytes);
        Task UploadAttendancePhotoAsync(Guid photoGuid, byte[] fileBytes);
        Task UploadAttendanceEmbeddingAsync(Guid photoGuid, int embeddingType, byte[] fileBytes);
    }

    public class DocPhotoService : IDocPhotoService
    {
        private const int DefaultDetectorId = 1;

        private readonly ISessionFactory _sessionFactory;
        private readonly ILogger<DocPhotoService> _logger;

        public DocPhotoService(
            ISessionFactory sessionFactory,
            ILogger<DocPhotoService> logger)
        {
            _sessionFactory = sessionFactory;
            _logger = logger;
        }

        public async Task UploadStudentPhotoAsync(
     string photoTypeShortcode,
     Guid studentGuid,
     Guid photoGuid,
     byte[] fileBytes)
        {
            GuidUpsertHelper.ValidateGuidNotEmpty(studentGuid, "Student GUID");
            GuidUpsertHelper.ValidateGuidNotEmpty(photoGuid, "Photo GUID");

            using var session = _sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();

            await DocApiHelper.EnsureDetectorExistsAsync(session, DefaultDetectorId);

            bool studentExists = await session.Query<Student>()
                .AnyAsync(s => s.StudentGuid == studentGuid);

            int photoTypeId = 0;
            if (studentExists)
            {
                photoTypeId = await DocApiHelper.ResolvePhotoTypeIdAsync(session, photoTypeShortcode);
            }

            var existingPhoto = await session.GetAsync<StudentPhoto>(photoGuid);

            if (fileBytes != null &&
                fileBytes.Length > 0 &&
                studentExists)
            {
                // All validations passed

                if (existingPhoto == null)
                {
                    await session.SaveAsync(new StudentPhoto
                    {
                        StudentPhotoGuid = photoGuid,
                        StudentGuid = studentGuid,
                        DetectorId = DefaultDetectorId,
                        PhotoTypeId = photoTypeId,
                        Photo = fileBytes,
                        IsSynchronized = false
                    });
                }
                else
                {
                    existingPhoto.StudentGuid = studentGuid;
                    existingPhoto.DetectorId = DefaultDetectorId;
                    existingPhoto.PhotoTypeId = photoTypeId;
                    existingPhoto.Photo = fileBytes;
                    existingPhoto.IsSynchronized = false;

                    await session.UpdateAsync(existingPhoto);
                }

                await transaction.CommitAsync();
            }
            else if (fileBytes == null || fileBytes.Length == 0)
            {
                throw new ArgumentException("File is required.");
            }
            else if (!studentExists)
            {
                throw new KeyNotFoundException("Student not found.");
            }
            else
            {
                throw new Exception("Unable to upload student photo.");
            }
        }

        public async Task UploadStudentEmbeddingAsync(
        string photoTypeShortcode,
        Guid studentGuid,
        Guid photoGuid,
        int embeddingType,
        byte[] fileBytes)
        {
            DocApiHelper.ValidateEmbeddingType(embeddingType);
            GuidUpsertHelper.ValidateGuidNotEmpty(studentGuid, "Student GUID");
            GuidUpsertHelper.ValidateGuidNotEmpty(photoGuid, "Photo GUID");

            using var session = _sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();

            await DocApiHelper.EnsureDetectorExistsAsync(session, DefaultDetectorId);
            await DocApiHelper.EnsureEmbeddingTypeExistsAsync(session, embeddingType);

            bool studentExists = await session.Query<Student>()
                .AnyAsync(s => s.StudentGuid == studentGuid);

            var existingStudentPhoto = await session.GetAsync<StudentPhoto>(photoGuid);

            int photoTypeId = 0;
            if (existingStudentPhoto != null)
            {
                photoTypeId = await DocApiHelper.ResolvePhotoTypeIdAsync(session, photoTypeShortcode);
            }

            Embedding existingEmbedding = null;
            Guid embeddingGuid = Guid.Empty;

            if (existingStudentPhoto != null)
            {
                embeddingGuid = DocApiHelper.CreateEmbeddingGuid(
                    photoGuid,
                    existingStudentPhoto.DetectorId,
                    embeddingType);

                existingEmbedding = await session.GetAsync<Embedding>(embeddingGuid);
            }

            if (fileBytes != null &&
                fileBytes.Length > 0 &&
                studentExists &&
                existingStudentPhoto != null &&
                existingStudentPhoto.StudentGuid == studentGuid &&
                existingStudentPhoto.PhotoTypeId == photoTypeId)
            {

                if (existingEmbedding == null)
                {
                    await session.SaveAsync(new Embedding
                    {
                        EmbeddingGuid = embeddingGuid,
                        StudentPhotoGuid = photoGuid,
                        DetectorId = existingStudentPhoto.DetectorId,
                        EmbeddingTypeId = embeddingType,
                        EmbeddingData = fileBytes,
                        IsSynchronized = false
                    });
                }
                else
                {
                    existingEmbedding.EmbeddingData = fileBytes;
                    existingEmbedding.IsSynchronized = false;

                    await session.UpdateAsync(existingEmbedding);
                }

                await transaction.CommitAsync();
            }
            else if (fileBytes == null || fileBytes.Length == 0)
            {
                throw new ArgumentException("File is required.");
            }
            else if (!studentExists)
            {
                throw new KeyNotFoundException("Student not found.");
            }
            else if (existingStudentPhoto == null)
            {
                throw new KeyNotFoundException("Student photo not found.");
            }
            else if (existingStudentPhoto.StudentGuid != studentGuid)
            {
                throw new ArgumentException("Photo belongs to another student.");
            }
            else if (existingStudentPhoto.PhotoTypeId != photoTypeId)
            {
                throw new ArgumentException("Photo type mismatch.");
            }
            else
            {
                throw new Exception("Unable to upload embedding.");
            }
        }

        public async Task UploadAttendancePhotoAsync(Guid photoGuid, byte[] fileBytes)
        {
            GuidUpsertHelper.ValidateGuidNotEmpty(photoGuid, "Photo GUID");

            using var session = _sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();

            var existingPhotograph = await session.GetAsync<AttendancePhotograph>(photoGuid);

            if (fileBytes != null &&
                fileBytes.Length > 0 &&
                existingPhotograph != null)
            {
                existingPhotograph.Photograph = fileBytes;
                existingPhotograph.IsSynchronized = false;

                await session.UpdateAsync(existingPhotograph);
                await transaction.CommitAsync();
            }
            else if (fileBytes == null || fileBytes.Length == 0)
            {
                throw new ArgumentException("File is required.");
            }
            else if (existingPhotograph == null)
            {
                throw new KeyNotFoundException("Attendance photo not found.");
            }
            else
            {
                throw new Exception("Unable to upload attendance photo.");
            }
        }
        public async Task UploadAttendanceEmbeddingAsync(Guid photoGuid, int embeddingType, byte[] fileBytes)
        {
            DocApiHelper.ValidateEmbeddingType(embeddingType);
            GuidUpsertHelper.ValidateGuidNotEmpty(photoGuid, "Photo GUID");

            using var session = _sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();

            await DocApiHelper.EnsureEmbeddingTypeExistsAsync(session, embeddingType);

            var existingPhotograph = await session.GetAsync<AttendancePhotograph>(photoGuid);

            Guid studentGuid = Guid.Empty;

            if (existingPhotograph != null)
            {
                studentGuid = await session.Query<StudentAttendancePhoto>()
                    .Where(p => p.AttendancePhotoGuid == photoGuid)
                    .Select(p => p.StudentGuid)
                    .FirstOrDefaultAsync();

                if (studentGuid == Guid.Empty)
                {
                    studentGuid = await session.Query<StudentAttendanceRecord>()
                        .Where(r => r.AttendanceGuid == existingPhotograph.AttendanceGuid)
                        .Select(r => r.StudentGuid)
                        .FirstOrDefaultAsync();
                }
            }

            Embedding? existingEmbedding = null;
            Guid embeddingGuid = Guid.Empty;

            if (existingPhotograph != null)
            {
                embeddingGuid = DocApiHelper.CreateEmbeddingGuid(
                    photoGuid,
                    existingPhotograph.DetectorId,
                    embeddingType);

                existingEmbedding = await session.GetAsync<Embedding>(embeddingGuid);
            }

            if (fileBytes != null &&
                fileBytes.Length > 0 &&
                existingPhotograph != null &&
                studentGuid != Guid.Empty)
            {
                if (existingEmbedding == null)
                {
                    await session.SaveAsync(new Embedding
                    {
                        EmbeddingGuid = embeddingGuid,
                        StudentPhotoGuid = photoGuid,
                        DetectorId = existingPhotograph.DetectorId,
                        EmbeddingTypeId = embeddingType,
                        EmbeddingData = fileBytes,
                        IsSynchronized = false
                    });
                }
                else
                {
                    existingEmbedding.EmbeddingData = fileBytes;
                    existingEmbedding.IsSynchronized = false;

                    await session.UpdateAsync(existingEmbedding);
                }

                await transaction.CommitAsync();
            }
            else if (fileBytes == null || fileBytes.Length == 0)
            {
                throw new ArgumentException("File is required.");
            }
            else if (existingPhotograph == null)
            {
                throw new KeyNotFoundException("Attendance photo not found.");
            }
            else if (studentGuid == Guid.Empty)
            {
                throw new KeyNotFoundException("No student linked to attendance photo.");
            }
            else
            {
                throw new Exception("Unable to upload attendance embedding.");
            }
        }
    }
}
