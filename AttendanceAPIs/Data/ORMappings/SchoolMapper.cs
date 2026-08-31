using FluentNHibernate.Mapping;
using PMPoshanWithAngular.Server.Data.Models;

namespace PMPoshanWithAngular.Server.Data.ORMappings
{
    public class SchoolMapper : ClassMap<School>
    {
        public SchoolMapper()
        {
            Table("School");

            Id(x => x.SchoolGuid)
                .Column("school_guid")
                .GeneratedBy.Assigned();

            Map(x => x.Name)
                .Column("name")
                .Length(150)
                .Not.Nullable();

            Map(x => x.District)
                .Column("district")
                .Length(100)
                .Not.Nullable();
        }
    }
}
