using System.Net;

namespace BFPR4B.EHiring.ApiService.Helpers.Response
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessages { get; set; } = "";
        public int Draw { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public object? Data { get; set; }
    }

    public class LoginResponseModel
    {
        public DateTime Expiration { get; set; } = Convert.ToDateTime("1/1/1900");
        public object? Data { get; set; }
        public string AccessToken { get; set; } = "";
    }
}
