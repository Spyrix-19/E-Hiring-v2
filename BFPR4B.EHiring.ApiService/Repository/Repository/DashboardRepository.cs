using BFPR4B.EHiring.ApiService.Models.Data;
using BFPR4B.EHiring.ApiService.Repository.IRepository;

namespace BFPR4B.EHiring.ApiService.Repository.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _db;

        public DashboardRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
        }







    }
}
