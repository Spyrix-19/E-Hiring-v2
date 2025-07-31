using BFPR4B.EHiring.ApiService.Helpers.Response;
using BFPR4B.EHiring.ApiService.Models.DTO;

namespace BFPR4B.EHiring.ApiService.Repository.IRepository
{
    public interface IAuthRepository
    {
        Task<ApiResponse> LoginOTPAsync(LoginRequestDTO parameters);
        Task<ApiResponse> LoginAsync(LoginRequestDTO parameters);
        Task<ApiResponse> LoginV2Async(LoginRequestDTO parameters);
        Task<ApiResponse> RegisterAsync(RegistrationRequestDTO parameters);
    }
}
