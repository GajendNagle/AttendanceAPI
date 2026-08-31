using FluentNHibernate.Mapping;
using PMPoshanWithAngular.Server.Data.Models;

namespace PMPoshanWithAngular.Server.Data.ORMappings
{
    public class TeacherMapper : ClassMap<Teacher>
    {
        public TeacherMapper()
        {
            Table("Teacher");

            Id(x => x.TeacherId)
                .Column("teacher_id")
                .GeneratedBy.Identity();

            Map(x => x.TeacherGuid).Column("teacher_guid").Not.Nullable().Unique();
            Map(x => x.Username).Column("username").Length(100).Not.Nullable().Unique();
            Map(x => x.PasswordHash).Column("password_hash").Length(255).Not.Nullable();
            Map(x => x.Name).Column("name").Length(100).Not.Nullable();
            Map(x => x.SchoolGuid).Column("school_guid").Not.Nullable();
            Map(x => x.IsSynchronized).Column("is_synchronized").Not.Nullable();
        }
    }
}
