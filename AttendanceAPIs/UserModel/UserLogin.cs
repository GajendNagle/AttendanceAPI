using System.ComponentModel.DataAnnotations;

namespace PMPoshanWithAngular.Server.JwtTokenModel
{
    public class UserLogin
    {
        [Required]
        public string username { get; set; }
        public string password { get; set; }
       
    }
}
