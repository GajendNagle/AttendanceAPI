using FluentNHibernate.Mapping;
using PMPoshanWithAngular.Server.Data.Models;

namespace PMPoshanWithAngular.Server.Data.ORMappings
{
    public class StudentPhotoMapper : ClassMap<StudentPhoto>
    {
        public StudentPhotoMapper()
        {
            Table("StudentPhoto");

            Id(x => x.StudentPhotoGuid)
                .Column("student_photo_guid")
                .GeneratedBy.Assigned();

            Map(x => x.StudentGuid).Column("student_guid").Not.Nullable();
            Map(x => x.DetectorId).Column("detector_id").Not.Nullable();
            Map(x => x.PhotoTypeId).Column("photo_type_id").Not.Nullable();
            Map(x => x.Photo).Column("photo").Not.Nullable().CustomSqlType("MEDIUMBLOB");
            Map(x => x.IsSynchronized).Column("is_synchronized").Not.Nullable();
        }
    }
}
