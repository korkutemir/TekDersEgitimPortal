using AutoMapper;
using BuildBackEnd.Core.Models;
using BuildBackEnd.Core.Repositories;
using BuildBackEnd.Core.Services;
using BuildBackEnd.Core.UnitOfWorks;

namespace BuildBackEnd.Service.Services
{
    public class CourseCategoryBridgeService : Service<CourseCategoryBridge>, ICourseCategoryBridgeService
    {
        private readonly ICourseCategoryBridgeRepository _main_repository;
        private readonly IMapper _mapper;

        public CourseCategoryBridgeService(IGenericRepository<CourseCategoryBridge> repository, IUnitOfWork unitOfWork, IMapper mapper, ICourseCategoryBridgeRepository main_repository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _main_repository = main_repository;
        }

    }
}
