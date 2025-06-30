using BuildBackEnd.Core.Models;
using BuildBackEnd.Core.Repositories;
using BuildBackEnd.Data;

namespace BuildBackEnd.Repository.Repositories
{
    public class CourseCategoryBridgeRepository : GenericRepository<CourseCategoryBridge>, ICourseCategoryBridgeRepository
    {
        public CourseCategoryBridgeRepository(AppDbContext context) : base(context)
        {
        }
    }
}
