namespace PMPoshanWithAngular.Server.Data.Models
{
    public class AdmissionDetail
    {
        public virtual Guid AdmissionGuid { get; set; }
        public virtual Guid StudentGuid { get; set; }
        public virtual Guid SchoolGuid { get; set; }
        public virtual string Class { get; set; } = string.Empty;
        public virtual string Section { get; set; } = string.Empty;
        public virtual int AcademicYear { get; set; }
    }
}
