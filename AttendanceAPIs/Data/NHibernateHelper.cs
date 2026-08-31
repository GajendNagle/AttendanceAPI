using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using PMPoshanWithAngular.Server.Data.ORMappings;
using ISession = NHibernate.ISession;
using ISessionFactory = NHibernate.ISessionFactory;

namespace PMPoshanWithAngular.Server.Data
{
    public class NHibernateHelper
    {
        public static ISessionFactory AttendanceSessionFactory { get; private set; } = null!;

        public static void Initialize(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AttendanceDB");

            AttendanceSessionFactory = Fluently.Configure()
                .Database(
                    MySQLConfiguration.Standard
                        .ConnectionString(connectionString)
                        .Dialect<NHibernate.Dialect.MySQL8Dialect>()
                        .ShowSql()
                )
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<SchoolMapper>())
                .BuildSessionFactory();
        }

        public static ISession AttendanceSession() => AttendanceSessionFactory.OpenSession();
    }
}
