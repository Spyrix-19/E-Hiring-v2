using BFPR4B.EHiring.ApiService.Models.Data;
using BFPR4B.EHiring.ApiService.Repository.IRepository;

namespace BFPR4B.EHiring.ApiService.Repository.Repository
{
    public class FrameworkRepository : IFrameworkRepository
    {
        private readonly ApplicationDbContext _db;

        public FrameworkRepository(ApplicationDbContext db)
        {
            _db = db;
        }



    }
}
