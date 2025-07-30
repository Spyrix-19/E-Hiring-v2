namespace BFPR4B.EHiring.ApiService.Helpers.Settings
{
    public class Settings
    { 
        #region " *** Local *** "

		public static String DATA_SOURCE_A = "SPYRIX19\\SQLSERVER";
        public static String USER_ID_A = "sa";
        public static String PASSWORD_A = "@M!m@r0p@2034";
        public static String CATALOG_A = "bfpr4b_dev";

        public static int CommandTimeout = 40000;

        #endregion

        public static String Client_ID = "07FD63B9-E0CF-48C6-8E64-946602519A34-BCDDCF91-2188-44B1-A88B-558B33FC7D0C-F19A6793-E7DA-4289-9FA8-44C33CDEE1D2-6CEF608C-9D97-4005-9111-4A753169F124-09DB4463-9747-461D-8D7D-98440132B732-A3F3B94A-9412-47B0-9613-2EC1149A1917";
        public static String Client_Secret = "69A902B6-C7BB-42D7-AB1F-7C5A91976AFC-C6CC02DE-0825-4A2C-AC4D-088E2B86E9AC-5B903F0D-075B-437C-AF05-B8541B19EE91-20E853E5-9AF6-4189-B991-8A13BE599890-61ED87F2-47D8-420A-8959-CD07C1322D1A-0942554A-35DF-4552-A795-5DE145D25A80";
        public static String Grant_Type = "Password";
        public static String Login_Type = "BFP MIMAOPA EHIRING";

        public static int N_SESSIONWARN = 10; //Session WARN in minutes
        public static int N_SESSIONEXPIRE = 120; //Session EXPIRE in minutes

        public static string INVALID_SESSION_ERR_MSG = "Your session has expired for your security.";

        /// <summary>
        /// This is the default error message if model validation is invalid.
        /// </summary> 
        public static string INVALID_MODEL_ERR_MSG = "Request validation failed. Please check invalid parameters.";

        /// <summary>
        /// This is the default error message if API error cannot be determined by the system.
        /// </summary>
        public static string API_ERR_MSG = "API error. Please try again.";

        /// <summary>
        /// This is the default error message if error cannot be determined by the system.
        /// </summary>
        public static string UNKNOWN_ERR_MSG = "Something went wrong. Please try again.";

        /// <summary>
        /// This is the default error message if error cannot be determined by the system.
        /// </summary>
        public const string API_JSON_ERR_MSG = "Something went wrong while processing the server response. Please try again.";

        /// <summary>
        /// This is the default error message if error cannot be determined by the system.
        /// </summary>
        public static string DB_CONNECTION_ERR_MSG = "Unable to connect to the database. Please try again later.";


        /// <summary>
        /// This is the default error message if user has no user access to certain features.
        /// </summary>
        public static string USER_ACCESS_ERR_MSG = "Your user accessibility does not allow you to access this feature. Please consult MIS/IT for assistance.";

    }
}
