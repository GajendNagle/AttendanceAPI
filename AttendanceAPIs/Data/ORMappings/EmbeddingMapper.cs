using FluentNHibernate.Mapping;
using PMPoshanWithAngular.Server.Data.Models;

namespace PMPoshanWithAngular.Server.Data.ORMappings
{
    public class EmbeddingMapper : ClassMap<Embedding>
    {
        public EmbeddingMapper()
        {
            Table("Embedding");

            Id(x => x.EmbeddingGuid)
                .Column("embedding_guid")
                .GeneratedBy.Assigned();

            Map(x => x.StudentPhotoGuid).Column("student_photo_guid").Not.Nullable();
            Map(x => x.DetectorId).Column("detector_id").Not.Nullable();
            Map(x => x.EmbeddingTypeId).Column("embedding_type_id").Not.Nullable();
            Map(x => x.EmbeddingData).Column("embedding").Not.Nullable().CustomSqlType("LONGBLOB");
            Map(x => x.IsSynchronized).Column("is_synchronized").Not.Nullable();
        }
    }
}
