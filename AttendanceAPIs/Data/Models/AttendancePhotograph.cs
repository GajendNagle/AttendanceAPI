namespace PMPoshanWithAngular.Server.Data.Models
{
    public class AttendancePhotograph
    {
        public virtual Guid AttendancePhotoGuid { get; set; }
        public virtual Guid AttendanceGuid { get; set; }
        public virtual int DetectorId { get; set; }
        public virtual byte[] Photograph { get; set; } = Array.Empty<byte>();
        public virtual int NoFacesRecognized { get; set; }
        public virtual bool IsSynchronized { get; set; }
    }
}
