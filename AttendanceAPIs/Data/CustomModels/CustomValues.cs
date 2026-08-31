namespace PMPoshanWithAngular.Server.Data.CustomeModels
{
    public class CustomValues
    {

        public static int LENGTH_OF_PASSWORD = 11;
        public static string PASSWORD_PATTERN = @"^(?=.*[!@#$%^&*()_+{}\[\]:;<>,.?~\\/-])(?=.*\d)(?=.*[a-zA-Z]).*$";
        public static string COOKING_AGENCY_SERVICECODE = "E0F24D22-71E0-45E8-B5E6-659A05403C17";
        public static int SCHOOL_TYPE_PRIMARY_WITH_MIDDLE_SCHOOL_CLASS1TO8 = 2;
        public static int SCHOOL_TYPE_Primary_to_Higher_Secondary_School_Class1to12 = 3;
        public static int SCHOOL_TYPE_Primary_to_High_School_Class1to_10 = 6;
        public static int SCHOOL_TYPE_Goverment_Schools = 1;
        public static int SCHOOL_ID_PREFIX_FOR_EPES_PRIMARY = 14000000;
        public static int SCHOOL_ID_PREFIX_FOR_EPES_UPPER_PRIMARY = 16000000;
    }
}
