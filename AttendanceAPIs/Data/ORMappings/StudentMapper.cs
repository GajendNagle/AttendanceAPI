using FluentNHibernate.Mapping;
using PMPoshanWithAngular.Server.Data.Models;

namespace PMPoshanWithAngular.Server.Data.ORMappings
{
    public class StudentMapper : ClassMap<Student>
    {
        public StudentMapper()
        {
            Table("Student");

            Id(x => x.StudentId)
                .Column("student_id")
                .GeneratedBy.Identity();

            Map(x => x.StudentGuid)
                .Column("student_guid")
                .Not.Nullable()
                .Unique();

            Map(x => x.AdmissionNumber)
                .Column("admission_number")
                .Length(50)
                .Not.Nullable()
                .Unique();

            Map(x => x.Name)
                .Column("name")
                .Length(100)
                .Not.Nullable();

            Map(x => x.Age)
                .Column("age")
                .Not.Nullable();

            Map(x => x.Gender)
                .Column("gender")
                .Not.Nullable();

            Map(x => x.IsSynchronized)
                .Column("is_synchronized")
                .Not.Nullable();
        }
    }
}
