using FluentNHibernate.Mapping;
using PMPoshanWithAngular.Server.Data.Models;

namespace PMPoshanWithAngular.Server.Data.ORMappings
{
    public class AdmissionDetailMapper : ClassMap<AdmissionDetail>
    {
        public AdmissionDetailMapper()
        {
            Table("AdmissionDetail");

            Id(x => x.AdmissionGuid)
                .Column("admission_guid")
                .GeneratedBy.Assigned();

            Map(x => x.StudentGuid)
                .Column("student_guid")
                .Not.Nullable();

            Map(x => x.SchoolGuid)
                .Column("school_guid")
                .Not.Nullable();

            Map(x => x.Class)
                .Column("class")
                .Length(20)
                .Not.Nullable();

            Map(x => x.Section)
                .Column("section")
                .Length(20)
                .Not.Nullable();

            Map(x => x.AcademicYear)
                .Column("academic_year")
                .Not.Nullable();
        }
    }
}
