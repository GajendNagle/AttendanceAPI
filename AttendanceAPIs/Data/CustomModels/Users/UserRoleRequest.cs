using System.ComponentModel.DataAnnotations;

namespace PMPoshanWithAngular.Server.Data.CustomeModels.Users
{
    public class UserRoleRequest
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string role { get; set; }
        public string claim { get; set; }
        public string servicecode { get; set; }
    }

}
