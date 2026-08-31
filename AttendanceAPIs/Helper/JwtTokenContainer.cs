using PMPoshanWithAngular.Server.Exceptions;
using PMPoshanWithAngular.Server.JwtTokenModel;
using System.Security.Claims;

namespace PMPoshanWithAngular.Server.Helper
{
    public class JwtTokenContainer
    {
        private readonly ILogger<JwtTokenContainer> _logger;
        public JwtTokenContainer(ILogger<JwtTokenContainer> logger)
        {
            _logger = logger;
        }
        private ClaimsPrincipal MyUser
        { get; set; }

        public JwtTokenContainer(ClaimsPrincipal User)
        {
            MyUser = User;
        }
       
        public JwtTokenContainer(string User)
        {
        }

       


        public int GetUserCrudBy()
        {
            int i = 0;
            if (
                MyUser != null
                && MyUser.HasClaim(c => c.Type == UserConstants.CLAIM_ID_FOR_CRUDBy)
                && !string.IsNullOrEmpty(MyUser.Claims.FirstOrDefault(c => c.Type == UserConstants.CLAIM_ID_FOR_CRUDBy).Value)
                )
            {
                i = Convert.ToInt32(MyUser.Claims.FirstOrDefault(c => c.Type == UserConstants.CLAIM_ID_FOR_CRUDBy).Value);
            }
            return i;
        }

        public string GetDisplayName()
        {
            string i = "";
            if (
                MyUser != null
                && MyUser.HasClaim(c => c.Type == UserConstants.CLAIM_ID_FOR_DISPLAY_NAME)
                && !string.IsNullOrEmpty(MyUser.Claims.FirstOrDefault(c => c.Type == UserConstants.CLAIM_ID_FOR_DISPLAY_NAME).Value)
                )
            {
                i = MyUser.Claims.FirstOrDefault(c => c.Type == UserConstants.CLAIM_ID_FOR_DISPLAY_NAME).Value;
            }
            return i;
        }
        public string GetUserName()
        {
            string i = "";
            if (
                MyUser != null
                && MyUser.HasClaim(c => c.Type == UserConstants.CLAIM_ID_FOR_NAME)
                && !string.IsNullOrEmpty(MyUser.Claims.FirstOrDefault(c => c.Type == UserConstants.CLAIM_ID_FOR_NAME).Value)
                )
            {
                i = MyUser.Claims.FirstOrDefault(c => c.Type == UserConstants.CLAIM_ID_FOR_NAME).Value;
            }
            return i;
        }

        public string GetUserGUID()
        {
            string i = "";
            if (
                MyUser != null
                && MyUser.HasClaim(c => c.Type == UserConstants.CLAIM_ID_FOR_USERID)
                && !string.IsNullOrEmpty(MyUser.Claims.FirstOrDefault(c => c.Type == UserConstants.CLAIM_ID_FOR_USERID).Value)
                )
            {
                i = MyUser.Claims.FirstOrDefault(c => c.Type == UserConstants.CLAIM_ID_FOR_USERID).Value;
            }
            return i;
        }

        private string GetValue(string Key)
        {
            string i = "";
            if (
                MyUser != null
                && MyUser.HasClaim(c => c.Type == Key)
                && !string.IsNullOrEmpty(MyUser.Claims.FirstOrDefault(c => c.Type == Key).Value)
                )
            {
                i = MyUser.Claims.FirstOrDefault(c => c.Type == Key).Value;
            }
            return i;
        }

        public string GetSessionGUID()
        {
            string i = "";
            if (
                MyUser != null
                && MyUser.HasClaim(c => c.Type == UserConstants.CLAIM_ID_FOR_DISPLAY_NAME)
                && !string.IsNullOrEmpty(MyUser.Claims.FirstOrDefault(c => c.Type == UserConstants.CLAIM_ID_FOR_DISPLAY_NAME).Value)
                )
            {
                i = MyUser.Claims.FirstOrDefault(c => c.Type == UserConstants.CLAIM_ID_FOR_DISPLAY_NAME).Value;
            }
            return i;
        }

        public int GetDistrictId()
        {

            int i = 0;
            if (
                MyUser != null
                && MyUser.HasClaim(c => c.Type == UserConstants.CLAIM_ID_FOR_DISTRICTID)
                && !string.IsNullOrEmpty(MyUser.Claims.FirstOrDefault(c => c.Type == UserConstants.CLAIM_ID_FOR_DISTRICTID).Value)
                )
            {
                i = Convert.ToInt32(MyUser.Claims.FirstOrDefault(c => c.Type == UserConstants.CLAIM_ID_FOR_DISTRICTID).Value);
            }
            return i;
        }
        public int GetBlockId()
        {

            int i = 0;
            if (
                MyUser != null
                && MyUser.HasClaim(c => c.Type == UserConstants.CLAIM_ID_FOR_BLOCKID)
                && !string.IsNullOrEmpty(MyUser.Claims.FirstOrDefault(c => c.Type == UserConstants.CLAIM_ID_FOR_BLOCKID).Value)
                )
            {
                i = Convert.ToInt32(MyUser.Claims.FirstOrDefault(c => c.Type == UserConstants.CLAIM_ID_FOR_BLOCKID).Value);
            }
            return i;
        }
        public string[] GetUserRole()
        {
            string[] roles = Array.Empty<string>();

            if (MyUser != null &&
                MyUser.HasClaim(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                && !string.IsNullOrEmpty(MyUser.Claims.FirstOrDefault(c =>
                   c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value))
            {
                roles = MyUser.Claims
                    .Where(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                    .Select(c => c.Value)
                    .ToArray();
            }

            return roles;
        }


        public bool IsDistrictAdmin(byte expectedDistrictId)
        {
            bool result = false;
            try
            {
                string[] roles = GetUserRole();
                byte districtId = (byte)GetDistrictId();
                if (roles != null
                    && roles.Length > 0
                    && roles.Contains("DistrictAdmin")
                    && districtId == expectedDistrictId)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new AttendanceException(ex.Message, ex);
            }
            return result;
        }

        public bool IsStateAdmin()
        {
            bool result = false;
            try
            {
                string[] roles = GetUserRole();
                if (roles != null
                    && roles.Length > 0
                    && roles.Contains("StateAdmin"))
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new AttendanceException(ex.Message, ex);
            }
            return result;
        }

        public bool hasRole()
        {
            bool result = false;
            try
            {
                string[] roles = GetUserRole();
                if (roles != null
                    && roles.Length > 0
                    && roles.Contains("DistrictAdmin"))
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new AttendanceException(ex.Message, ex);
            }
            return result;
        }

        public bool obscure(byte expectedDistrictId)
        {
            bool result = true;
            try
            {
                string[] roles = GetUserRole();
                byte districtId = (byte)GetDistrictId();
                if (
                    roles != null
                    && roles.Length > 0
                    && (roles.Contains("StateAdmin") || roles.Contains("StateOfficer"))
                    && hasRole()
                    || districtId == expectedDistrictId
                )
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new AttendanceException(ex.Message, ex);
            }
            return result;
        }


    }
}
