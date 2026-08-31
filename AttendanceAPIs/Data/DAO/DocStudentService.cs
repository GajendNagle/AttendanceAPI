using NHibernate.Linq;
using PMPoshanWithAngular.Server.Data.CustomModels.Api;
using PMPoshanWithAngular.Server.Data.Models;
using PMPoshanWithAngular.Server.Helper;
using ISessionFactory = NHibernate.ISessionFactory;

namespace PMPoshanWithAngular.Server.Data.DAO
{
    public interface IDocStudentService
    {
        Task<SaveStudentsResponse> SaveStudentsAsync(List<ApiStudentDto> students);
    }

    public class DocStudentService : IDocStudentService
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<DocStudentService> _logger;

        public DocStudentService(
            ISessionFactory sessionFactory,
            IConfiguration configuration,
            ILogger<DocStudentService> logger)
        {
            _sessionFactory = sessionFactory;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<SaveStudentsResponse> SaveStudentsAsync(List<ApiStudentDto> students)
        {
            if (students != null && students.Count > 0)
            {
                var response = new SaveStudentsResponse();
                var defaultSchoolGuid = _configuration.GetValue<Guid?>("DefaultSchoolGuid");

                using var session = _sessionFactory.OpenSession();
                using var transaction = session.BeginTransaction();

                foreach (var item in students)
                {
                    GuidUpsertHelper.ValidateGuidNotEmpty(item.sg, "Student GUID");

                    var existingStudent = await session.Query<Student>()
                        .FirstOrDefaultAsync(s => s.StudentGuid == item.sg);
                    if (existingStudent == null)
                    {
                        ValidateStudentForInsert(item);
                        await session.SaveAsync(new Student
                        {
                            StudentGuid = item.sg,
                            AdmissionNumber = BuildAdmissionNumber(item.sg),
                            Name = item.n,
                            Age = item.a,
                            Gender = DocApiHelper.MapGender(item.g),
                            IsSynchronized = false
                        });
                    }
                    else
                    {
                        existingStudent.Name = item.n;
                        existingStudent.Age = item.a;
                        existingStudent.Gender = DocApiHelper.MapGender(item.g);
                        existingStudent.IsSynchronized = false;
                        await session.UpdateAsync(existingStudent);
                    }

                    if (defaultSchoolGuid.HasValue && defaultSchoolGuid.Value != Guid.Empty)
                    {
                        var schoolExists = await session.Query<School>()
                            .AnyAsync(s => s.SchoolGuid == defaultSchoolGuid.Value);
                        if (schoolExists)
                        {
                            var year = DateTime.UtcNow.Year;
                            var existingAdmission = await session.Query<AdmissionDetail>()
                                .FirstOrDefaultAsync(a =>
                                    a.StudentGuid == item.sg
                                    && a.SchoolGuid == defaultSchoolGuid.Value
                                    && a.AcademicYear == year);

                            if (existingAdmission == null)
                            {
                                ValidateStudentForInsert(item);
                                await session.SaveAsync(new AdmissionDetail
                                {
                                    AdmissionGuid = Guid.NewGuid(),
                                    StudentGuid = item.sg,
                                    SchoolGuid = defaultSchoolGuid.Value,
                                    Class = item.c,
                                    Section = string.Empty,
                                    AcademicYear = year
                                });
                            }
                            else
                            {
                                existingAdmission.Class = item.c;
                                await session.UpdateAsync(existingAdmission);
                            }
                        }
                    }

                    response.saved.Add(item.sg.ToString());
                }

                await transaction.CommitAsync();
                return response;
            }
            else
            {
                throw new ArgumentException("Student array is required.");
            }
        }

        private static void ValidateStudentForInsert(ApiStudentDto item)
        {
            if (item.sg != Guid.Empty
                && !string.IsNullOrWhiteSpace(item.n)
                && !string.IsNullOrWhiteSpace(item.c)
                && !string.IsNullOrWhiteSpace(item.g)
                && item.a >= 3 && item.a <= 30)
            {
                if (item.g is not ("M" or "F" or "O"))
                {
                    throw new ArgumentException("Invalid gender value.");
                }
            }
            else
            {
                throw new ArgumentException("Missing or invalid student field.");
            }
        }

        private static string BuildAdmissionNumber(Guid studentGuid)
        {
            return $"ADM-{studentGuid:N}"[..Math.Min(50, $"ADM-{studentGuid:N}".Length)];
        }
    }
}
