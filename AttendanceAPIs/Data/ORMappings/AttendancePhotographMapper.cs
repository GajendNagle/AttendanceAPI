using FluentNHibernate.Mapping;
using PMPoshanWithAngular.Server.Data.Models;

namespace PMPoshanWithAngular.Server.Data.ORMappings
{
    public class AttendancePhotographMapper : ClassMap<AttendancePhotograph>
    {
        public AttendancePhotographMapper()
        {
            Table("AttendancePhotograph");

            Id(x => x.AttendancePhotoGuid)
                .Column("attendance_photo_guid")
                .GeneratedBy.Assigned();

            Map(x => x.AttendanceGuid).Column("attendance_guid").Not.Nullable();
            Map(x => x.DetectorId).Column("detector_id").Not.Nullable();
            Map(x => x.Photograph).Column("photograph").Not.Nullable().CustomSqlType("LONGBLOB");
            Map(x => x.NoFacesRecognized).Column("no_faces_recognized").Not.Nullable();
            Map(x => x.IsSynchronized).Column("is_synchronized").Not.Nullable();
        }
    }
}
