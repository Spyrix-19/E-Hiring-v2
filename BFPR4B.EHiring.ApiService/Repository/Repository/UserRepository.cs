using BFPR4B.EHiring.ApiService.Models.Data;
using BFPR4B.EHiring.ApiService.Repository.IRepository;

namespace BFPR4B.EHiring.ApiService.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;

        public UserRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
        }






    }
}
