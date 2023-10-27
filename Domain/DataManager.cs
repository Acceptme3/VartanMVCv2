using VartanMVCv2.Domain.Entities;
using VartanMVCv2.Domain.Repositories.Abstract;

namespace VartanMVCv2.Domain
{
    public class DataManager
    {
        public IEntityRepository<WorkServices> WorkServicesRepository { get; set; }
        public IEntityRepository<WorksCategory> WorksCategoryRepository{ get; set; }
        public IEntityRepository<Work> WorksRepository { get; set; }
        public IEntityRepository<Feedback> FeedbackRepository  { get; set; }
        public IEntityRepository<CompletedProject> CompletedProjectRepository { get; set; }
        public IEntityRepository<CompletedProjectPhoto> CompletedProjectPhoto { get; set; }

        public IClientRepository ClientRepository { get; set; }

        public DataManager(IEntityRepository<WorkServices> workServicesRepository, IEntityRepository<WorksCategory> worksCategoryRepository, IEntityRepository<Work> works, IEntityRepository<Feedback> feedback, IEntityRepository<CompletedProject> completedProject, IEntityRepository<CompletedProjectPhoto> completedProjectPhoto, IClientRepository clientRepository)
        {
            WorkServicesRepository = workServicesRepository;
            WorksCategoryRepository = worksCategoryRepository;
            WorksRepository = works;
            FeedbackRepository = feedback;
            CompletedProjectRepository = completedProject;
            CompletedProjectPhoto = completedProjectPhoto;
            ClientRepository = clientRepository;
        }
    }
}
