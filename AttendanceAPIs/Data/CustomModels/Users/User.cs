using System.ComponentModel.DataAnnotations;

namespace PMPoshanWithAngular.Server.Data.CustomeModels.Users
{
    public class User
    {

        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string mobile { get; set; }
        public string servicecode { get; set; }
    }
}
