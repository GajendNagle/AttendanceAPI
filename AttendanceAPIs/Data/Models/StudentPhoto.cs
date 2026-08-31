namespace PMPoshanWithAngular.Server.Data.Models
{
    public class StudentPhoto
    {
        public virtual Guid StudentPhotoGuid { get; set; }
        public virtual Guid StudentGuid { get; set; }
        public virtual int DetectorId { get; set; }
        public virtual int PhotoTypeId { get; set; }
        public virtual byte[] Photo { get; set; } = Array.Empty<byte>();
        public virtual bool IsSynchronized { get; set; }
    }
}
