using System;

namespace PMPoshanWithAngular.Server.Helper
{
    public class AuthUserResponse
    {
        public string Username { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public Guid TeacherGuid { get; set; }
        public Guid SchoolGuid { get; set; }
        public string SchoolName { get; set; } = string.Empty;
    }

    public class AuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthUserResponse? ValidateUser(string username, string password)
        {
            var expectedUsername = _configuration["Auth:Username"] ?? "laxmi";
            var expectedPassword = _configuration["Auth:Password"] ?? "laxmi";

            if (username.Equals(expectedUsername, StringComparison.OrdinalIgnoreCase)
                && password == expectedPassword)
            {
                var teacherGuidStr = _configuration["Auth:TeacherGuid"] ?? "590127e4-a169-4d49-ab38-45550cb5f0fb";
                var schoolGuidStr = _configuration["Auth:SchoolGuid"] ?? "02c1d0f5-bd4a-4fbd-bb4c-0d297ac30655";
                Guid.TryParse(teacherGuidStr, out var teacherGuid);
                Guid.TryParse(schoolGuidStr, out var schoolGuid);

                return new AuthUserResponse
                {
                    Username = expectedUsername,
                    Name = _configuration["Auth:Name"] ?? "Laxmi",
                    TeacherGuid = teacherGuid == Guid.Empty ? Guid.NewGuid() : teacherGuid,
                    SchoolGuid = schoolGuid == Guid.Empty ? Guid.Parse("02c1d0f5-bd4a-4fbd-bb4c-0d297ac30655") : schoolGuid,
                    SchoolName = _configuration["Auth:SchoolName"] ?? "PS Baandi Khedi"
                };
            }
            else
            {
                return null;
            }
        }
    }
}
