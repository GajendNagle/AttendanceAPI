using FluentNHibernate.Mapping;
using PMPoshanWithAngular.Server.Data.Models;

namespace PMPoshanWithAngular.Server.Data.ORMappings
{
    public class DetectorMapper : ClassMap<Detector>
    {
        public DetectorMapper()
        {
            Table("Detector");

            Id(x => x.DetectorId)
                .Column("detector_id")
                .GeneratedBy.Assigned();

            Map(x => x.DetectorName).Column("detector_name").Length(50).Not.Nullable();
        }
    }
}
