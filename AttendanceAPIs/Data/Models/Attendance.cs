namespace PMPoshanWithAngular.Server.Data.Models
{
    public class Attendance
    {
        public virtual Guid AttendanceGuid { get; set; }
        public virtual DateTime AttendanceDate { get; set; }
        public virtual Guid SchoolGuid { get; set; }
        public virtual Guid? TeacherGuid { get; set; }
        public virtual int StudentCount { get; set; }
        public virtual bool IsSynchronized { get; set; }
    }
}
