namespace PMPoshanWithAngular.Server.Data.CustomeModels.Reponse
{
    public class ServiceResponseConstants
    {
        //-- Generic Response -- 
        public static readonly string GENERIC_INVALID_SERVICE_CODE = "INVALID_SERVICE_CODE";
        public static readonly string GENERIC_MISSING_PARAMETER = "MISSING_PARAMETER";
        public static readonly string GENERIC_EXCEPTION = "UNABLE_TO_PROCESS_YOUR_REQUEST";
        public static readonly string GENERIC_PARSE_ERROR = "UNABLE_TO_PARSE_REQUEST";
        public static readonly string GENERIC_INCOMPLETE_REQUEST = "INCOMPLETE_REQUEST";
        public static readonly string GENERIC_SUCCESS = "SUCCESS";
        public static readonly string GENERIC_FAIL = "FAIL";
        public static readonly string GENERIC_OOPS = "OOPS";
        public static readonly string GENERIC_DUPLICATE = "DUPLICATE";
        public static readonly string GENERIC_ALREADY = "ALREADY";
        public static readonly string GENERIC_NORECORD = "NO_RECORD";
        public static readonly string GENERIC_INVALID_DISECODE = "INVALID_DISECODE";
        public static readonly string GENERIC_UNAUTHORIZED = "UNAUTHORIZED";
        public static readonly string GENERIC_INVALID_YEAR = "INVALID_YEAR_ID";
        public static readonly string GENERIC_INVALID_GUID = "INVALID_GUID";
        public static readonly string GENERIC_INVALID_DATE = "INVALID_DATE";
        public static readonly string BANK_ACCOUNT_NUMBER_ALREADY_EXIST = "BANK_ALREADY";
        public static readonly string GENERIC_INVALID_IFSC = "INVALID_IFSC_CODE";
        public static readonly string UNAUTHORIZED_DISTRICT_ACCESS = "UNAUTHORIZED_DISTRICT_ACCESS";
        public static readonly string UNAUTHORIZED_BLOCK_ACCESS = "UNAUTHORIZED_BLOCK_ACCESS";
        public static readonly string COOK_NOT_FROM_YOUR_DISTRICT = "COOK_NOT_FROM_YOUR_DISTRICT";
        public static readonly string INVALID_DISTRICT_FOR_DISTRICT_ADMIN = "INVALID_DISTRICT_FOR_DISTRICT_ADMIN";
        public static readonly string INVALID_BLOCK = "INVALID_BLOCK";
        public static readonly string INVALID_REASON = "INVALID_REASON";
        public static readonly string NO_MAPPING_FOUND = "NO_MAPPING_FOUND";
        public static readonly string UNSUPPORTED_PAYMENT_TYPE = "UNSUPPORTED_PAYMENT_TYPE";
        public static readonly string GENERIC_FILE_NOT_GENERATED = "GENERIC_FILE_NOT_GENERATED";
        public static readonly string GENERIC_NOT_FOUND = "GENERIC_NOT_FOUND";



        //------------------------------------User Response-------------------------------------
        public static readonly string USER_REGISTRATION_MOBILE_ALREADY_REGISTERED = "USER_MOBILE_ALREADY_REGISTERED";
        public static readonly string USER_REGISTRATION_USERNAME_ALREADY_REGISTERED = "USERNAME_ALREADY_REGISTERED";
        public static readonly string USER_REGISTRATION_NOT_REGISTERED_USER = "USER_NOT_REGISTERED";
        public static readonly string USER_REGISTRATION_USER_REGISTERED = "USERNAME_REGISTERED";
        public static readonly string USER_REGISTRATION_USER_NOT_FOUND = "USER_NOT_FOUND";
        public static readonly string USER_REGISTRATION_INVALID_USERNAME_OR_PASSWORD = "INVALID_USERNAME_OR_PASSWORD";
        public static readonly string USER_REGISTRATION_ROLE_ALREADY_ASSIGN = "ROLE_ALREADY_ASSIGNED_TO_USER";
        public static readonly string USER_REGISTRATION_ROLE_ASSIGNED = "ROLE_ASSIGNED_TO_USER";
        public static readonly string USER_REGISTRATION_ROLE_FAIL_TO_ASSIGN = "FAIL_TO_ASSIGN_ROLE";
        public static readonly string USER_REGISTRATION_INVALID_PASSWORD_LENGTH = "PASSWORD_LENGTH_SHOULD_BE_GREATER_THAN_8_CHARATER";
        public static readonly string USER_REGISTRATION_PASSWORD_CONTAINE = "PASSWORD_MUST_CONTAINE_SPECIAL_SYMBOL_ATLEAST_ONE_NUMERIC_VALUE_AND_ALPHABET";





        //-- Allotment --
        public static readonly string ALLOTMENT_DONE = "ALLOTMENT_DONE";
        public static readonly string NO_ACTIVE_ALLOTMENT_MONTH = "NO_ACTIVE_ALLOTMENT_MONTH";
        public static readonly string NO_ACTIVE_PAYMENT_MONTH = "NO_ACTIVE_PAYMENT_MONTH";
        public static readonly string NO_PRIMARY_DATA_FOUND = "NO_PRIMARY_DATA_FOUND";
        public static readonly string COMMODITY_ALREADY_EXISTS = "COMMODITY_ALREADY_EXISTS";
        public static readonly string ENR_NOT_SET = "ENR_NOT_SET";

        //-- Register Bank Details --
        public static readonly string BANK_ACCOUNT_NUMBER_FAILED_TO_REGISTERED = "Unable to registered account details";
       

        //------------------------------------Cooking Agency Response-------------------------------------
        public static readonly string COOKING_AGENCY_REGISTERED = "Successfully Registered";
        public static readonly string COOKING_AGENCY_FAILED_TO_REGISTERED = "Failed to register";
        public static readonly string COOKING_AGENCY_DATA_ALREADY_EXIST = "Data already exists in the database";
        public static readonly string COOKING_AGENCY_MAPPING_ALREADY_EXIST = "School already mapped";
        public static readonly string COOKING_AGENCY_INVALID_GUID = "Invalid cooking agecny guid";
        public static readonly string COOKING_AGENCY_MAPPING_COUNT_NOT_MATCH = "Cooking agency mapping count not matched";
       

        //----------------------------------Deactive Agency-----------------------------
        public static readonly string DEACTIVE_COOKING_AGENCY_SUCCESS = "Cooking agency successfully deactivated";
        public static readonly string DEACTIVE_COOKING_AGENCY_FAIL = "Fail to deactivated cooking agency";
        public static readonly string DEACTIVE_COOKING_AGENCY_HISTORY_AND_AGENCY_COUNT_NOT_MATCHED = "Deactive cooking agency history and agency count not match";
        public static readonly string DEACTIVE_NO_AGENCY_FOUND = "AGENCY_DETAILS_NOT_FOUND";
        
        //--------------------------------BlackList Agency ------------------
        public static readonly string BLACKLIST_COOKING_AGENCY_SUCCESS = "COOKING_AGENCY_SUCCESSFULLY_BLACKLIST";
        public static readonly string BLACKLIST_COOKING_AGENCY_FAIL = "FAIL_TO_BLACKLIST_COOKING_AGENCY";
        public static readonly string BLACKLIST_COOKING_AGENCY_HISTORY_AND_AGENCY_COUNT_NOT_MATCHED = "BLACKLIST_COOKING_AGENCY_HISTORY_AND_AGENCY_COUNT_NOT_MATCH";
        public static readonly string BLACKLIST_NO_AGENCY_FOUND = "AGENCY_DETAILS_NOT_FOUND";
        public static readonly string BLACKLIST_MEMBER_AND_MEMBER_HISTORY_NOT_MATCH = "MEMBER_AND_MEMBER_HISTORY_NOT_MATCH";
        public static readonly string BLACKLIST_MEMBER_REMOVED_AND_MEMBER_HISTORY_INSERT_COUNT_NOT_MATCH = "MEMBER_REMOVED_AND_MEMBER_HISTORY_INSERT_COUNT_NOT_MATCH";
        public static readonly string BLACKLIST_MAPPING_HISTORY_AND_MEMBER_COUNT_NOT_MATCH = "BLACKLIST_MAPPING_HISTORY_AND_MEMBER_COUNT_NOT_MATCH";

        //--------------------------------Agency Member -------------------------------------
        public static readonly string AGENCY_MEMBER_INVALID_AADHAR = "Invalid Aadhar Number";
        public static readonly string AGENCY_MEMBER_AADHAR_ALREADY_REGISTERED = "AADHAR Number Already Registered";
        public static readonly string AGENCY_MEMBER_AADHAR_FAIL_TO_REGISTER_AADHAR = "Fail To Register Aadhar";
        public static readonly string AGENCY_MEMBER_MOBILE_ALREADY_EXIST = "Agency Member Already Exist";
        public static readonly string AGENCY_MEMBER_SUCCESSFULLY_REGISTERED = "Agency Member Registered Successfully";
        public static readonly string AGENCY_MEMBER_FAIL_TO_REGISTER = "Agency Member Fail To Register";
        public static readonly string AGENCY_MEMBER_AGENCY_NOT_FOUND = "Agency Member Not Found";

        //--------------------------------Agency bank update -------------------------------------
        public static readonly string AGENCY_NOT_FOUND = "Agency not found";
        public static readonly string ACCOUNT_AND_IFSC_ARE_SAME_AS_OLD_ACCOUNT_AND_IFSC = "Account and ifsc are same as old account and ifsc";
        public static readonly string FAIL_TO_REGISTERED = "Bank details could not be updated";
        public static readonly string UNABLE_TO_INSERT_IN_HISTORY = "Unable to insert in history";
        public static readonly string BANK_SUCCESSFULLY_UPDATED = "Bank details have been updated successfully";
        public static readonly string FAIL_TO_UPDATE_BANK_DETAIL_IN_COOKING_AGENCY = "Fail to update bank detail in cooking agency";


       

        //--------------------------------------Cook -----------------------------------------------------

        public static readonly string COOK_DATA_ALREADY_EXIST = "Data already exists in the database";
        public static readonly string COOK_REGISTERED = "Successfully registered";
        public static readonly string COOK_FAILED_TO_REGISTERED = "Failed to register";
        public static readonly string SCHOOL_INVALID_GUID = "School invalid guid";
        public static readonly string INVALID_BANK_DETAILS = "INVALID_BANK_DETAILS ";
    
        //--------------------------------------Cook Mapping--------------------------------------------------
        public static readonly string COOK_SUCCESSFULLY_MAPPED = "Cook successfully mapped";
        public static readonly string FAIL_TO_MAP_COOK = "Fail to map cook";
        public static readonly string COOK_ALREADY_MAPPED_WITH_SCHOOL = "Cook already mapped with school";
        public static readonly string COOK_INVALID_GUID = "Cook invalid guid";
        //--------------------------------BlackList Cook ------------------
        public static readonly string BLACKLIST_COOK_SUCCESS = "COOK_SUCCESSFULLY_BLACKLISTED";
        public static readonly string BLACKLIST_COOK_FAIL = "FAIL_TO_BLACKLIST_COOK";
        public static readonly string UNABLE_ADD_MAPPING_HISTORY = "UNABLE_ADD_MAPPING_HISTORY";
        public static readonly string UNABLE_TO_REMOVE_MAPPING = "UNABLE_TO_REMOVE_MAPPING";
        public static readonly string COOK_DETAIL_NOT_FOUND = "COOK_DETAILS_NOT_FOUND";
        public static readonly string ONLY_BLACKLIST_REASON_ALLOWED = "Invalid reason. Only blacklist reasons are allowed.";
        public static readonly string ALREADY_INACTIVE_COOK = "Cook is already inactive.";
        public static readonly string DEACTIVATE_COOK_SUCCESS = "COOK_SUCCESSFULLY_DEACTIVATED";
        public static readonly string DEACTIVATE_COOK_FAIL = "FAIL_TO_DEACTIVATE_COOK";
  
        public static readonly string COOK_MAPPING_NOT_FOUND = "COOK_MAPPING_NOT_FOUND";
        public static readonly string SCHOOL_NOT_MAPPED_WITH_ANGECY = "SCHOOL_NOT_MAPPED_WITH_ANGECY";
        //--------------------------------PFMS Payment ------------------
        public static readonly string INVALID_PAYMENT_TYPE = "INVALID_PAYMENT_TYPE";
        public static readonly string HEADER_NOT_FOUND = "HEADER_NOT_FOUND";
        public static readonly string EPO_NOT_FOUND = "EPO_NOT_FOUND";


        //////////////////////////////////       FPS         //////////////////////////////////////////////
        public static readonly string NEW_FPS_ADDED = "NEW_FPS_SUCCESSFULLY_ADDED_TO_OUR_PORTAL";
        public static readonly string FPS_UPDATED_SUCCESS = "FPS_DETAILS_UPDATED_SUCCESSFULLY";
        public static readonly string FPS_FAIL = "UNABLE_TO_UPDATE/ADD_THE_FPS_TO_OUR_PORTAL";
        public static readonly string FPS_NO_DETAILS_FOUND = "FPS_NO_DETAILS_FOUND_FOR_THE_FPS_CODE";
        //////////////////////////////////// FPS Mapping ///////////////////////////////////////
        public static readonly string FPS_MAPPING_ALREADY_EXIST = "SCHOOL_ALREADY_MAPPED_WITH_FPS";
        public static readonly string FPS_INVALID_GUID = "INVALID_FPS_GUID";
        public static readonly string FPS_SUCCESSFULLY_MAPPED = "FPS_SUCCESSFULLY_MAPPED";
        public static readonly string FPS_MAPPING_COUNT_NOT_MATCH = "FPS_MAPPING_COUNT_NOT_MATCHED";
        public static readonly string FPS_FAILED_TO_MAP = "FPS_FAILED_TO_MAP";
        //////////////////////////////////// FPS Discontinue ///////////////////////////////////////
        public static readonly string DISCONTINUE_NO_FPS_FOUND = "FPS_DETAILS_NOT_FOUND";
        public static readonly string DISCONTINUE_FPS_SUCCESS = "FPS_SUCCESSFULLY_DISCONTINUED";
        public static readonly string DISCONTINUE_FPS_FAIL = "FAIL_TO_DISCONTINUE_FPS";
        ////////////////////////////////// school Module /////////////////////////////////////////
        public static readonly string ADD_UDATE_SCHOOL_FROM_SHIKSHA_SUCCESS = "School Successfully Added/Updated";
        public static readonly string ADD_UDATE_SCHOOL_FROM_SHIKSHA_FAIL = "Fail Added/Updated School";
        // Add New Month 
        public static readonly string ADD_MONTH_SUCCESS = "Month added successfully";
        public static readonly string ADD_MONTH_FAIL = "Fail Added the Month";
        public static readonly string ADD_MONTH_DUPLICATE = "Month Already Exists";

        // Set Enrollment
        public static readonly string SET_ENROL_SUCCESS = "Month added successfully";
        public static readonly string SET_ENROL_FAIL = "Fail Added the Month";
        public static readonly string SET_ENROL_DUPLICATE = "Month Already Exists";

        public static string INVALID_COOK_GUID = "INVALID COOK GUID";
        public static string COOK_NOT_FOUND = "COOK NOT FOUND";
        public static string FAIL_TO_UNMAPED_COOK = "Fail To Unmaped Cook";
        public static string SCHOOL_NOT_FOUND = "School not found ";
        public static string INVALID_FPS_CODE = "Invalid FPS Code";
        public static string INVALID_DISE_CODE = " Invalid DISE Code";
        public static string GENERIC_INVALID_REASON = "Invalid Reason";
        public static string GENERIC_INVALID_MONTH = "Invalid Month";
        public static string GENERIC_NO_DRAFT_EXISTS = "No Draft Exists";
        public static string GENERIC_REQUIRED_FIELDS = "Required Fields";
        public static string FAILED_TO_CREATE_EPO_ORDER = "Failed To Create Epo Order";
        public static string GENERIC_INVALID_BLOCK = "Invalid Block";
        public static string GENERIC_BAD_STATE = " Bad State";

        public static string REPROCESS_OLD_EPO_NOT_FOUND = "OLD_EPO_NOT_FOUND";
        public static string REPROCESS_ALL_ALREADY_REPROCESSED = "ALL_TRANSACTIONS_ALREADY_REPROCESSED";
        public static string REPROCESS_NO_FAILED_TRANSACTIONS = "NO_FAILED_TRANSACTIONS_FOUND";
        public static string REPROCESS_NEW_EPO_NOT_PERSISTED = "NEW_EPO_NOT_PERSISTED";
    }
}
