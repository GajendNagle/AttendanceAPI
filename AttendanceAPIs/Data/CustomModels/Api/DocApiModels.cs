namespace PMPoshanWithAngular.Server.Data.CustomModels.Api
{
    public class ApiStudentDto
    {
        public Guid sg { get; set; }
        public string n { get; set; } = string.Empty;
        public string c { get; set; } = string.Empty;
        public int a { get; set; }
        public string g { get; set; } = string.Empty;
    }

    public class SaveStudentsResponse
    {
        public List<string> saved { get; set; } = new();
    }

    public class ApiAttendancePhotoDto
    {
        public Guid pg { get; set; }
        public int apc { get; set; }
        public List<Guid> std { get; set; } = new();
    }

    public class ApiAttendanceDto
    {
        public string scg { get; set; } = string.Empty;
        public string dt { get; set; } = string.Empty;
        public int tpc { get; set; }
        public int apc { get; set; }
        public List<ApiAttendancePhotoDto> ph { get; set; } = new();
    }

    public class ApiErrorResponse
    {
        public string error { get; set; } = string.Empty;
        public string message { get; set; } = string.Empty;

        public ApiErrorResponse(string error, string message)
        {
            this.error = error;
            this.message = message;
        }
    }
}
