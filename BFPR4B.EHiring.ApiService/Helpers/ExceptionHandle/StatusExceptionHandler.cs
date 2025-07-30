using Microsoft.Data.SqlClient;
using System.Net;

namespace BFPR4B.EHiring.ApiService.Helpers.ExceptionHandle
{
    public class StatusExceptionHandler
    {
        public static HttpStatusCode GetStatusCode(Exception ex)
        {
            return ex switch
            {
                ArgumentNullException => HttpStatusCode.BadRequest, // 400
                ArgumentException => HttpStatusCode.BadRequest, // 400
                FormatException => HttpStatusCode.BadRequest, // 400
                InvalidOperationException => HttpStatusCode.Conflict, // 409
                UnauthorizedAccessException => HttpStatusCode.Unauthorized, // 401
                AccessViolationException => HttpStatusCode.Forbidden, // 403
                KeyNotFoundException => HttpStatusCode.NotFound, // 404
                NullReferenceException => HttpStatusCode.InternalServerError, // 500
                InvalidCastException => HttpStatusCode.InternalServerError, // 500
                TimeoutException => HttpStatusCode.GatewayTimeout, // 504
                SqlException => HttpStatusCode.InternalServerError, // 500
                _ => HttpStatusCode.InternalServerError // Default case
            };
        }

        public static bool IsClientError(Exception ex)
        {
            var code = GetStatusCode(ex);
            return ((int)code >= 400 && (int)code < 500);
        }

        public static bool IsClientError(HttpStatusCode code)
        {
            return ((int)code >= 400 && (int)code < 500);
        }
    }
}
