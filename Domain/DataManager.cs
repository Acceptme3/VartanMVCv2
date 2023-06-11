using VartanMVCv2.Domain.Entities;
using VartanMVCv2.Domain.Repositories.Abstract;

namespace VartanMVCv2.Domain
{
    public class DataManager
    {
        public IEntityRepository<WorkServices> WorkServices {  get; set; }
        public IEntityRepository<WorksList> WorksList { get; set; }

        public DataManager(IEntityRepository<WorkServices> workServicesRepository, IEntityRepository<WorksList> worksListRepository)
        {
            WorkServices = workServicesRepository;
            WorksList = worksListRepository;
        }
    }
}
