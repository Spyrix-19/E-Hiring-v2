using BFPR4B.EHiring.ApiService.Models.Data;
using BFPR4B.EHiring.ApiService.Repository.IRepository;

namespace BFPR4B.EHiring.ApiService.Repository.Repository
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly ApplicationDbContext _db;

        public AttachmentRepository(ApplicationDbContext db)
        {
            _db = db;
        }






    }
}
