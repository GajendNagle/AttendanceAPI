namespace PMPoshanWithAngular.Server.Data.Models
{
    public class School
    {
        public virtual Guid SchoolGuid { get; set; }
        public virtual string Name { get; set; } = string.Empty;
        public virtual string District { get; set; } = string.Empty;
    }
}
