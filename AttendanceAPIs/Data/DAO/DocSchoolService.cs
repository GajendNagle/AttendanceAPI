using NHibernate.Linq;
using PMPoshanWithAngular.Server.Data.CustomModels;
using PMPoshanWithAngular.Server.Data.Models;
using ISessionFactory = NHibernate.ISessionFactory;

namespace PMPoshanWithAngular.Server.Data.DAO
{
    public interface IDocSchoolService
    {
        Task<List<SchoolResponse>> GetSchoolsAsync();
    }

    public class DocSchoolService : IDocSchoolService
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly ILogger<DocSchoolService> _logger;

        public DocSchoolService(
            ISessionFactory sessionFactory,
            ILogger<DocSchoolService> logger)
        {
            _sessionFactory = sessionFactory;
            _logger = logger;
        }

        public async Task<List<SchoolResponse>> GetSchoolsAsync()
        {
            using var session = _sessionFactory.OpenSession();

            var schools = await session.Query<School>()
                .OrderBy(s => s.Name)
                .Select(s => new SchoolResponse(s.SchoolGuid, s.Name, s.District))
                .ToListAsync();

            return schools;
        }
    }
}
