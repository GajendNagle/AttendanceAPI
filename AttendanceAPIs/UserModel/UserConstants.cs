namespace PMPoshanWithAngular.Server.JwtTokenModel
{
    public class UserConstants
    {
        public static List<UserModel> Users = new()
        {
            new UserModel(){ Username="laxmi", Password="laxmi", Role="Admin"}
        };
        public static string CLAIM_ID_FOR_NAME = "name";
        public static string CLAIM_ID_FOR_USERID = "userid";
        public static string CLAIM_ID_FOR_DISTRICTID = "districtid";
        public static string CLAIM_ID_FOR_BLOCKID = "blockid";
        public static string CLAIM_ID_FOR_ROLES = "roles";
        public static string CLAIM_ID_FOR_CRUDBy = "crudby";
        public static string CLAIM_ID_FOR_DISPLAY_NAME = "displayname";
    }
}
