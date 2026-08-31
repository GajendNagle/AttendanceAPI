namespace PMPoshanWithAngular.Server.Data.CustomModels
{
    public class AttendanceRecordRequest
    {
        public Guid StudentGuid { get; set; }
        public string Status { get; set; } = string.Empty;
        public float? Confidence { get; set; }
        public bool TeacherVerified { get; set; }
    }

    public class AttendanceRequest
    {
        public DateTime AttendanceDate { get; set; }
        public Guid SchoolGuid { get; set; }
        public List<AttendanceRecordRequest> Records { get; set; } = new();
    }

    public class AttendanceResponse
    {
        public Guid AttendanceGuid { get; set; }
        public DateTime AttendanceDate { get; set; }
        public Guid SchoolGuid { get; set; }
        public int StudentCount { get; set; }
        public List<StudentAttendanceRecordResponse> Records { get; set; } = new();
    }

    public class StudentAttendanceRecordResponse
    {
        public Guid RecordGuid { get; set; }
        public Guid StudentGuid { get; set; }
        public string Status { get; set; } = string.Empty;
        public float? Confidence { get; set; }
        public bool TeacherVerified { get; set; }
    }
}
