using BFPR4B.EHiring.ApiService.Helpers.Response;

namespace BFPR4B.EHiring.ApiService.Helpers.ExceptionHandle
{
    public class ExceptionResultHelper
    {
        public static ApiResponse HandleExceptionResult(string message, Exception ex)
        {
            var statusCode = StatusExceptionHandler.GetStatusCode(ex);

            return new ApiResponse
            {
                StatusCode = statusCode,
                IsSuccess = false,
                ErrorMessages = message,
                Data = null
            };
        }
    }
}
