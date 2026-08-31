namespace PMPoshanWithAngular.Server.Data.Models
{
    public class StudentAttendancePhoto
    {
        public virtual Guid AttendancePhotoGuid { get; set; }
        public virtual Guid StudentGuid { get; set; }
        public virtual byte[] StudentPhoto { get; set; } = Array.Empty<byte>();

        public override bool Equals(object? obj)
        {
            if (obj is not StudentAttendancePhoto other)
                return false;

            return AttendancePhotoGuid == other.AttendancePhotoGuid
                   && StudentGuid == other.StudentGuid;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AttendancePhotoGuid, StudentGuid);
        }
    }
}
