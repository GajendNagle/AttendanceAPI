namespace PMPoshanWithAngular.Server.Data.Models
{
    public class PhotoType
    {
        public virtual int PhotoTypeId { get; set; }
        public virtual string Name { get; set; } = string.Empty;
        public virtual string Shortcode { get; set; } = string.Empty;
    }
}
