using Asp.Versioning;
using BFPR4B.EHiring.ApiService.Helpers.ExceptionHandle;
using BFPR4B.EHiring.ApiService.Helpers.Response;
using BFPR4B.EHiring.ApiService.Helpers.Settings;
using BFPR4B.EHiring.ApiService.Models.DTO;
using BFPR4B.EHiring.ApiService.Repository.IRepository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace BFPR4B.EHiring.ApiService.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = false)]
    [ApiVersion("2.0", Deprecated = false)]
    [Produces("application/json")]
    [EnableCors("AllowMultipleOrigins")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthRepository _authRepository;

        public AuthController(ILogger<AuthController> logger, IAuthRepository authRepository)
        {
            _logger = logger;
            _authRepository = authRepository;
        }

        // Action for LoginOTP (V1 only)
        [HttpPost("LoginOTP")] // Directly specify route template here
        [MapToApiVersion("1.0")] // Explicitly map to V1
        public async Task<ActionResult<ApiResponse>> LoginOTPAsync(LoginRequestDTO parameters)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse // Ensure you return ApiResponse structure
                    {
                        IsSuccess = false,
                        ErrorMessages = Settings.INVALID_MODEL_ERR_MSG
                    });
                }
                var _result = await _authRepository.LoginOTPAsync(parameters);
                return Ok(_result);
            }
            catch (SqlException ex) when (ex.Number == -1 || ex.InnerException?.Message.Contains("A network-related") == true)
            {
                return ExceptionResultHelper.HandleExceptionResult(Settings.DB_CONNECTION_ERR_MSG, ex);
            }
            catch (Exception ex)
            {
                return ExceptionResultHelper.HandleExceptionResult(Settings.UNKNOWN_ERR_MSG, ex);
            }
        }

        [HttpPost("Login")] // Directly specify route template here
        [MapToApiVersion("1.0")] // Explicitly map to V1
        public async Task<ActionResult<ApiResponse>> LoginV1Async(LoginRequestDTO parameters) // Renamed for clarity
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse
                    {
                        IsSuccess = false,
                        ErrorMessages = Settings.INVALID_MODEL_ERR_MSG
                    });
                }
                var _result = await _authRepository.LoginAsync(parameters); // Assuming LoginAsync is your V1 logic
                return Ok(_result);
            }
            catch (SqlException ex) when (ex.Number == -1 || ex.InnerException?.Message.Contains("A network-related") == true)
            {
                return ExceptionResultHelper.HandleExceptionResult(Settings.DB_CONNECTION_ERR_MSG, ex);
            }
            catch (Exception ex)
            {
                return ExceptionResultHelper.HandleExceptionResult(Settings.UNKNOWN_ERR_MSG, ex);
            }
        }

        [HttpPost("Login")] // Keep the same base route template "Login"
        [MapToApiVersion("2.0")] // Explicitly map to V2
        public async Task<ActionResult<ApiResponse>> LoginV2Async(LoginRequestDTO parameters)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse
                    {
                        IsSuccess = false,
                        ErrorMessages = Settings.INVALID_MODEL_ERR_MSG
                    });
                }
                // Call your V2 specific repository method
                var _result = await _authRepository.LoginV2Async(parameters);
                return Ok(_result);
            }
            catch (SqlException ex) when (ex.Number == -1 || ex.InnerException?.Message.Contains("A network-related") == true)
            {
                return ExceptionResultHelper.HandleExceptionResult(Settings.DB_CONNECTION_ERR_MSG, ex);
            }
            catch (Exception ex)
            {
                return ExceptionResultHelper.HandleExceptionResult(Settings.UNKNOWN_ERR_MSG, ex);
            }
        }

        [HttpPost("Register")] // Directly specify route template here
        [MapToApiVersion("1.0")] // Explicitly map to V1
        public async Task<ActionResult<ApiResponse>> RegisterAsync(RegistrationRequestDTO parameters)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse
                    {
                        IsSuccess = false,
                        ErrorMessages = Settings.INVALID_MODEL_ERR_MSG
                    });
                }
                var _result = await _authRepository.RegisterAsync(parameters);
                return Ok(_result);
            }
            catch (SqlException ex) when (ex.Number == -1 || ex.InnerException?.Message.Contains("A network-related") == true)
            {
                return ExceptionResultHelper.HandleExceptionResult(Settings.DB_CONNECTION_ERR_MSG, ex);
            }
            catch (Exception ex)
            {
                return ExceptionResultHelper.HandleExceptionResult(Settings.UNKNOWN_ERR_MSG, ex);
            }
        }
    }
}
