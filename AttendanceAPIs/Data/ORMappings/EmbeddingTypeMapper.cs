using FluentNHibernate.Mapping;
using PMPoshanWithAngular.Server.Data.Models;

namespace PMPoshanWithAngular.Server.Data.ORMappings
{
    public class EmbeddingTypeMapper : ClassMap<EmbeddingType>
    {
        public EmbeddingTypeMapper()
        {
            Table("EmbeddingType");

            Id(x => x.EmbeddingTypeId)
                .Column("embedding_type_id")
                .GeneratedBy.Assigned();

            Map(x => x.EmbeddingName).Column("embedding_name").Length(50).Not.Nullable();
        }
    }
}
