using BFPR4B.EHiring.ApiService.Models.Data;
using BFPR4B.EHiring.ApiService.Repository.IRepository;

namespace BFPR4B.EHiring.ApiService.Repository.Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationRepository(ApplicationDbContext db)
        {
            _db = db;
        }





    }
}
