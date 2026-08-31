namespace PMPoshanWithAngular.Server.Data.CustomModels
{
    public class SchoolRequest
    {
        public string Name { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
    }

    public class SchoolResponse
    {
        public Guid SchoolGuid { get; set; }
        public string Name { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;

        public SchoolResponse() { }

        public SchoolResponse(Guid schoolGuid, string name, string district)
        {
            SchoolGuid = schoolGuid;
            Name = name;
            District = district;
        }
    }
}
