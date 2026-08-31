using FluentNHibernate.Mapping;
using PMPoshanWithAngular.Server.Data.Models;

namespace PMPoshanWithAngular.Server.Data.ORMappings
{
    public class StudentAttendancePhotoMapper : ClassMap<StudentAttendancePhoto>
    {
        public StudentAttendancePhotoMapper()
        {
            Table("StudentAttendancePhoto");

            CompositeId()
                .KeyProperty(x => x.AttendancePhotoGuid, "attendance_photo_guid")
                .KeyProperty(x => x.StudentGuid, "student_guid");

            Map(x => x.StudentPhoto).Column("student_photo").Not.Nullable().CustomSqlType("MEDIUMBLOB");
        }
    }
}
