namespace PMPoshanWithAngular.Server.Data.Models
{
    public class Teacher
    {
        public virtual int TeacherId { get; set; }
        public virtual Guid TeacherGuid { get; set; }
        public virtual string Username { get; set; } = string.Empty;
        public virtual string PasswordHash { get; set; } = string.Empty;
        public virtual string Name { get; set; } = string.Empty;
        public virtual Guid SchoolGuid { get; set; }
        public virtual bool IsSynchronized { get; set; }
    }
}
