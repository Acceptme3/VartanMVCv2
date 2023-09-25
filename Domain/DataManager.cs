using VartanMVCv2.Domain.Entities;
using VartanMVCv2.Domain.Repositories.Abstract;

namespace VartanMVCv2.Domain
{
    public class DataManager
    {
        public IEntityRepository<WorkServices> WorkServices {  get; set; }
        public IEntityRepository<WorksList> WorksList { get; set; }
        public IEntityRepository<WorksName> WorksName { get; set; }
        public IEntityRepository<Feedback> Feedback  { get; set; }
        public IEntityRepository<CompletedProject> CompletedProject { get; set; }
        public IEntityRepository<CompletedProjectPhoto> CompletedProjectPhoto { get; set; }

        public IClientRepository ClientRepository { get; set; }

        public DataManager(IEntityRepository<WorkServices> workServicesRepository, IEntityRepository<WorksList> worksListRepository, IEntityRepository<WorksName> worksName, IEntityRepository<Feedback> feedback, IEntityRepository<CompletedProject> completedProject, IEntityRepository<CompletedProjectPhoto> completedProjectPhoto, IClientRepository clientRepository)
        {
            WorkServices = workServicesRepository;
            WorksList = worksListRepository;
            WorksName = worksName;
            Feedback = feedback;
            CompletedProject = completedProject;
            CompletedProjectPhoto = completedProjectPhoto;
            ClientRepository = clientRepository;
        }
    }
}
