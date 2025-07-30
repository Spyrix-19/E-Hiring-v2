using Asp.Versioning;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BFPR4B.EHiring.ApiService.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = false)]
    [Produces("application/json")]
    [EnableCors("AllowMultipleOrigins")]
    public class MedicalController : ControllerBase
    {
    }
}
