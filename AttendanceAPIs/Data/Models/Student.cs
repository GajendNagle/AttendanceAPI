namespace PMPoshanWithAngular.Server.Data.Models
{
    public class Student
    {
        public virtual int StudentId { get; set; }
        public virtual Guid StudentGuid { get; set; }
        public virtual string AdmissionNumber { get; set; } = string.Empty;
        public virtual string Name { get; set; } = string.Empty;
        public virtual int Age { get; set; }
        public virtual string Gender { get; set; } = string.Empty;
        public virtual bool IsSynchronized { get; set; }
    }
}
