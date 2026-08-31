using FluentNHibernate.Mapping;
using PMPoshanWithAngular.Server.Data.Models;

namespace PMPoshanWithAngular.Server.Data.ORMappings
{
    public class StudentAttendanceRecordMapper : ClassMap<StudentAttendanceRecord>
    {
        public StudentAttendanceRecordMapper()
        {
            Table("StudentAttendanceRecord");

            Id(x => x.RecordGuid)
                .Column("record_guid")
                .GeneratedBy.Assigned();

            Map(x => x.AttendanceGuid).Column("attendance_guid").Not.Nullable();
            Map(x => x.StudentGuid).Column("student_guid").Not.Nullable();
            Map(x => x.Status).Column("status").Not.Nullable();
            Map(x => x.TeacherVerified).Column("teacher_verified").Not.Nullable();
            Map(x => x.IsSynchronized).Column("is_synchronized").Not.Nullable();
        }
    }
}
