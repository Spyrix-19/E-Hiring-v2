using Asp.Versioning;
using BFPR4B.EHiring.ApiService.Repository.IRepository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        //[HttpPost]
        //[MapToApiVersion("1.0")]
        //[Route("Login")]
        //public async Task<ActionResult<ApiResponse>> LoginAsync(LoginRequestDTO parameters)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(new
        //            {
        //                IsSuccess = false,
        //                ErrorMessages = Settings.INVALID_MODEL_ERR_MSG
        //            });
        //        }

        //        var _result = await _authRepository.LoginAsync(parameters);

        //        return Ok(_result);
        //    }
        //    catch (SqlException ex) when (ex.Number == -1 || ex.InnerException?.Message.Contains("A network-related") == true)
        //    {
        //        return ExceptionResultHelper.HandleExceptionResult(Settings.DB_CONNECTION_ERR_MSG, ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ExceptionResultHelper.HandleExceptionResult(Settings.UNKNOWN_ERR_MSG, ex);
        //    }
        //}

        //[HttpPost]
        //[MapToApiVersion("1.0")]
        //[Route("Register")]
        //public async Task<ActionResult<ApiResponse>> RegisterAsync(RegistrationRequestDTO parameters)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(new
        //            {
        //                IsSuccess = false,
        //                ErrorMessages = Settings.INVALID_MODEL_ERR_MSG
        //            });
        //        }

        //        var _result = await _authRepository.RegisterAsync(parameters);

        //        return Ok(_result);
        //    }
        //    catch (SqlException ex) when (ex.Number == -1 || ex.InnerException?.Message.Contains("A network-related") == true)
        //    {
        //        return ExceptionResultHelper.HandleExceptionResult(Settings.DB_CONNECTION_ERR_MSG, ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ExceptionResultHelper.HandleExceptionResult(Settings.UNKNOWN_ERR_MSG, ex);
        //    }
        //}
    }
}
