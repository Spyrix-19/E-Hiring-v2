using BFPR4B.EHiring.ApiService.Models.Data;
using BFPR4B.EHiring.ApiService.Repository.IRepository;

namespace BFPR4B.EHiring.ApiService.Repository.Repository
{
    public class RecruitmentRepository : IRecruitmentRepository
    {
        private readonly ApplicationDbContext _db;

        public RecruitmentRepository(ApplicationDbContext db)
        {
            _db = db;
        }





    }
}
