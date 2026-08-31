namespace PMPoshanWithAngular.Server.Data.Models
{
    public class StudentAttendanceRecord
    {
        public virtual Guid RecordGuid { get; set; }
        public virtual Guid AttendanceGuid { get; set; }
        public virtual Guid StudentGuid { get; set; }
        public virtual string Status { get; set; } = string.Empty;
        public virtual bool TeacherVerified { get; set; }
        public virtual bool IsSynchronized { get; set; }
    }
}
