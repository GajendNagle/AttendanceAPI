namespace PMPoshanWithAngular.Server.Data.CustomModels
{
    public class StudentRequest
    {
        public string AdmissionNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public Guid SchoolGuid { get; set; }
        public string Class { get; set; } = string.Empty;
        public string Section { get; set; } = string.Empty;
        public int AcademicYear { get; set; }
    }

    public class StudentResponse
    {
        public int StudentId { get; set; }
        public Guid StudentGuid { get; set; }
        public string AdmissionNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public Guid? SchoolGuid { get; set; }
        public string Class { get; set; } = string.Empty;
        public string Section { get; set; } = string.Empty;
        public int? AcademicYear { get; set; }
    }
}
