using FluentNHibernate.Mapping;
using PMPoshanWithAngular.Server.Data.Models;

namespace PMPoshanWithAngular.Server.Data.ORMappings
{
    public class PhotoTypeMapper : ClassMap<PhotoType>
    {
        public PhotoTypeMapper()
        {
            Table("PhotoType");

            Id(x => x.PhotoTypeId)
                .Column("photo_type_id")
                .GeneratedBy.Identity();

            Map(x => x.Name).Column("name").Length(20).Not.Nullable().Unique();
            Map(x => x.Shortcode).Column("shortcode").Length(1).Not.Nullable().Unique();
        }
    }
}
