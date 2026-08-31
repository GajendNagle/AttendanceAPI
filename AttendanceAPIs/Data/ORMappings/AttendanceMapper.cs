using FluentNHibernate.Mapping;
using PMPoshanWithAngular.Server.Data.Models;

namespace PMPoshanWithAngular.Server.Data.ORMappings
{
    public class AttendanceMapper : ClassMap<Attendance>
    {
        public AttendanceMapper()
        {
            Table("Attendance");

            Id(x => x.AttendanceGuid)
                .Column("attendance_guid")
                .GeneratedBy.Assigned();

            Map(x => x.AttendanceDate).Column("attendance_date").Not.Nullable();
            Map(x => x.SchoolGuid).Column("school_guid").Not.Nullable();
            Map(x => x.TeacherGuid).Column("teacher_guid").Nullable();
            Map(x => x.StudentCount).Column("student_count").Not.Nullable();
            Map(x => x.IsSynchronized).Column("is_synchronized").Not.Nullable();
        }
    }
}
