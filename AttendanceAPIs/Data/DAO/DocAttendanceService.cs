using NHibernate.Linq;
using PMPoshanWithAngular.Server.Data.CustomModels.Api;
using PMPoshanWithAngular.Server.Data.Models;
using PMPoshanWithAngular.Server.Helper;
using ISessionFactory = NHibernate.ISessionFactory;

namespace PMPoshanWithAngular.Server.Data.DAO
{
    public interface IDocAttendanceService
    {
        Task SubmitAttendanceAsync(ApiAttendanceDto request);
    }

    public class DocAttendanceService : IDocAttendanceService
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<DocAttendanceService> _logger;

        public DocAttendanceService(
            ISessionFactory sessionFactory,
            IConfiguration configuration,
            ILogger<DocAttendanceService> logger)
        {
            _sessionFactory = sessionFactory;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SubmitAttendanceAsync(ApiAttendanceDto request)
        {
            if (request != null)
            {
                Guid schoolGuid = Guid.Empty;
                if (!string.IsNullOrWhiteSpace(request.scg))
                {
                    Guid.TryParse(request.scg, out schoolGuid);
                }

                if (schoolGuid == Guid.Empty)
                {
                    var defaultSchoolStr = _configuration["DefaultSchoolGuid"] ?? _configuration["Auth:SchoolGuid"];
                    Guid.TryParse(defaultSchoolStr, out schoolGuid);
                }

                if (schoolGuid != Guid.Empty
                    && !string.IsNullOrWhiteSpace(request.dt)
                    && request.ph != null
                    && request.ph.Count > 0)
                {
                    if (DocApiHelper.TryParseDocDate(request.dt, out var attendanceDate))
                    {
                        using var session = _sessionFactory.OpenSession();
                        using var transaction = session.BeginTransaction();

                        var schoolExists = await session.Query<School>().AnyAsync(s => s.SchoolGuid == schoolGuid);
                        if (!schoolExists)
                        {
                            await session.SaveAsync(new School
                            {
                                SchoolGuid = schoolGuid,
                                Name = _configuration["Auth:SchoolName"] ?? "Default School",
                                District = _configuration["Auth:SchoolDistrict"] ?? "Default District"
                            });
                            await session.FlushAsync();
                        }

                        if (request.tpc < 0)
                        {
                            throw new ArgumentException("Total present count (tpc) cannot be negative.");
                        }

                        var teacherGuid = await ResolveTeacherGuidAsync(session, schoolGuid);
                        var attendanceGuid = await UpsertAttendanceAsync(
                            session,
                            schoolGuid,
                            request,
                            attendanceDate.Date,
                            teacherGuid);

                        foreach (var photo in request.ph)
                        {
                            if (photo.pg != Guid.Empty
                                && photo.std != null
                                && photo.std.Count > 0)
                            {
                                const int detectorId = 1;
                                await DocApiHelper.EnsureDetectorExistsAsync(session, detectorId);

                                await UpsertAttendancePhotographAsync(
                                    session,
                                    photo.pg,
                                    photo.apc,
                                    attendanceGuid,
                                    detectorId);

                                foreach (var studentGuid in photo.std)
                                {
                                    if (studentGuid != Guid.Empty)
                                    {
                                        var studentExists = await session.Query<Student>().AnyAsync(s => s.StudentGuid == studentGuid);
                                        if (!studentExists)
                                        {
                                            var admissionNo = $"ADM-{studentGuid:N}";
                                            await session.SaveAsync(new Student
                                            {
                                                StudentGuid = studentGuid,
                                                AdmissionNumber = admissionNo.Substring(0, Math.Min(50, admissionNo.Length)),
                                                Name = "Unknown Student",
                                                Age = 10,
                                                Gender = "M",
                                                IsSynchronized = false
                                            });
                                            await session.FlushAsync();
                                        }

                                        await UpsertStudentAttendanceRecordAsync(
                                            session,
                                            attendanceGuid,
                                            studentGuid);

                                        await UpsertStudentAttendancePhotoAsync(
                                            session,
                                            photo.pg,
                                            studentGuid);
                                    }
                                    else
                                    {
                                        throw new ArgumentException("Invalid student GUID in attendance photo.");
                                    }
                                }
                            }
                            else
                            {
                                throw new ArgumentException("Invalid attendance photo payload.");
                            }
                        }

                        await transaction.CommitAsync();
                    }
                    else
                    {
                        throw new ArgumentException("Invalid date format. Use DD/MM/YYYY.");
                    }
                }
                else
                {
                    throw new ArgumentException("Missing required attendance fields.");
                }
            }
        }

        private async Task<Guid?> ResolveTeacherGuidAsync(NHibernate.ISession session, Guid schoolGuid)
        {
            var configuredTeacherGuid = _configuration.GetValue<Guid?>("DefaultTeacherGuid")
                ?? _configuration.GetValue<Guid?>("Auth:TeacherGuid");

            if (!configuredTeacherGuid.HasValue || configuredTeacherGuid.Value == Guid.Empty)
            {
                return null;
            }

            var teacherGuid = configuredTeacherGuid.Value;
            var exists = await session.Query<Teacher>()
                .AnyAsync(t => t.TeacherGuid == teacherGuid);

            if (!exists)
            {
                // Teacher table was empty after truncate; seed the configured default so FK insert succeeds.
                await session.SaveAsync(new Teacher
                {
                    TeacherGuid = teacherGuid,
                    Username = _configuration["Auth:Username"] ?? "teacher",
                    PasswordHash = _configuration["Auth:Password"] ?? "teacher",
                    Name = _configuration["Auth:Name"] ?? "Teacher",
                    SchoolGuid = schoolGuid,
                    IsSynchronized = false
                });
                await session.FlushAsync();
            }

            return teacherGuid;
        }

        private static async Task<Guid> UpsertAttendanceAsync(
            NHibernate.ISession session,
            Guid schoolGuid,
            ApiAttendanceDto request,
            DateTime attendanceDate,
            Guid? teacherGuid)
        {
            var existingAttendance = await session.Query<Attendance>()
                .FirstOrDefaultAsync(a =>
                    a.SchoolGuid == schoolGuid
                    && a.AttendanceDate == attendanceDate);

            if (existingAttendance == null)
            {
                var attendanceGuid = Guid.NewGuid();
                await session.SaveAsync(new Attendance
                {
                    AttendanceGuid = attendanceGuid,
                    AttendanceDate = attendanceDate,
                    SchoolGuid = schoolGuid,
                    TeacherGuid = teacherGuid,
                    StudentCount = Math.Max(1, request.tpc),
                    IsSynchronized = false
                });
                return attendanceGuid;
            }
            else
            {
                existingAttendance.StudentCount = Math.Max(1, request.tpc);
                existingAttendance.TeacherGuid = teacherGuid;
                existingAttendance.IsSynchronized = false;
                await session.UpdateAsync(existingAttendance);
                return existingAttendance.AttendanceGuid;
            }
        }

        private static async Task UpsertAttendancePhotographAsync(
            NHibernate.ISession session,
            Guid photoGuid,
            int noFacesRecognized,
            Guid attendanceGuid,
            int detectorId)
        {
            GuidUpsertHelper.ValidateGuidNotEmpty(photoGuid, "Photo GUID");

            var existingPhotograph = await session.GetAsync<AttendancePhotograph>(photoGuid);
            if (existingPhotograph == null)
            {
                await session.SaveAsync(new AttendancePhotograph
                {
                    AttendancePhotoGuid = photoGuid,
                    AttendanceGuid = attendanceGuid,
                    DetectorId = detectorId,
                    Photograph = Array.Empty<byte>(),
                    NoFacesRecognized = noFacesRecognized,
                    IsSynchronized = false
                });
            }
            else
            {
                existingPhotograph.AttendanceGuid = attendanceGuid;
                existingPhotograph.DetectorId = detectorId;
                existingPhotograph.NoFacesRecognized = noFacesRecognized;
                existingPhotograph.IsSynchronized = false;
                await session.UpdateAsync(existingPhotograph);
            }
        }

        private static async Task UpsertStudentAttendanceRecordAsync(
            NHibernate.ISession session,
            Guid attendanceGuid,
            Guid studentGuid)
        {
            var existingRecord = await session.Query<StudentAttendanceRecord>()
                .FirstOrDefaultAsync(r =>
                    r.AttendanceGuid == attendanceGuid
                    && r.StudentGuid == studentGuid);

            if (existingRecord == null)
            {
                await session.SaveAsync(new StudentAttendanceRecord
                {
                    RecordGuid = Guid.NewGuid(),
                    AttendanceGuid = attendanceGuid,
                    StudentGuid = studentGuid,
                    Status = "Present",
                    TeacherVerified = true,
                    IsSynchronized = false
                });
            }
            else
            {
                existingRecord.Status = "Present";
                existingRecord.TeacherVerified = true;
                existingRecord.IsSynchronized = false;
                await session.UpdateAsync(existingRecord);
            }
        }

        private static async Task UpsertStudentAttendancePhotoAsync(
            NHibernate.ISession session,
            Guid photoGuid,
            Guid studentGuid)
        {
            var existingPhoto = await session.Query<StudentAttendancePhoto>()
                .FirstOrDefaultAsync(p =>
                    p.AttendancePhotoGuid == photoGuid
                    && p.StudentGuid == studentGuid);

            if (existingPhoto == null)
            {
                await session.SaveAsync(new StudentAttendancePhoto
                {
                    AttendancePhotoGuid = photoGuid,
                    StudentGuid = studentGuid,
                    StudentPhoto = Array.Empty<byte>()
                });
            }
            else
            {
                existingPhoto.StudentPhoto = Array.Empty<byte>();
                await session.UpdateAsync(existingPhoto);
            }
        }
    }
}
