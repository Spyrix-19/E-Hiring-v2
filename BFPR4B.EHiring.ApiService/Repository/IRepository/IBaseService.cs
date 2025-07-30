using BFPR4B.EHiring.ApiService.Helpers.Response;

namespace BFPR4B.EHiring.ApiService.Repository.IRepository
{
    public interface IBaseService
    {
        ApiResponse responseModel { get; set; }
    }
}
