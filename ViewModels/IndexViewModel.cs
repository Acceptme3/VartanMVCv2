using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VartanMVCv2.Domain;
using VartanMVCv2.Domain.Entities;
using VartanMVCv2.Domain.Repositories.Abstract;
using VartanMVCv2.Views;

namespace VartanMVCv2.ViewModels
{
    public class IndexViewModel
    {
        public _ViewStartModel _viewStartModel = new _ViewStartModel();

        public static IEnumerable<WorkServices> workServicesList { get; set; } = new List<WorkServices>();
        public static IEnumerable<WorksList> worksList { get; set; } = new List<WorksList>();
        public static IEnumerable<WorksName> worksNames { get; set; } = new List<WorksName>();

        public Client ClientExample { get; set; } = new Client();

        public static IEnumerable<CompletedProject> completedProjectsList { get; set; } = new List<CompletedProject>();
        public static IEnumerable<CompletedProjectPhoto> completedProjectsPhotoList { get; set; } = new List<CompletedProjectPhoto>();

        public static int projectCounter = 0;// переменная для подсчета итераций при формировании карусели.

        public static IEnumerable<WorksList> sortWorksList { get; set; } = new List<WorksList>();
        public static IEnumerable<WorksName> sortWorksNames { get; set; } = new List<WorksName>();

        private readonly IEntityRepository<WorkServices> _workServicesRepository;
        private readonly IEntityRepository<WorksList> _worksListRepository;
        private readonly IEntityRepository<WorksName> _workNameRepository;
        private readonly IEntityRepository<CompletedProject> _completedProjectRepository;
        private readonly IEntityRepository<CompletedProjectPhoto> _completedProjectPhotoRepository;
        
        private readonly DbContext _dbContext;

        public IndexViewModel(IEntityRepository<WorkServices> workServicesRepository, IEntityRepository<WorksList> worksListRepository, IEntityRepository<WorksName> workNameRepository,IEntityRepository<CompletedProject> completedProject, IEntityRepository<CompletedProjectPhoto> completedProjectPhoto, AplicationDBContext appDBContext) 
        {
            _workServicesRepository = workServicesRepository;
            _worksListRepository = worksListRepository;
            _workNameRepository = workNameRepository;
            _completedProjectRepository = completedProject;
            _completedProjectPhotoRepository = completedProjectPhoto;
            
            _dbContext = appDBContext;
            
        }

        public IndexViewModel()
        {

        }

        public async Task<IndexViewModel> ViewModelInitialAsync() 
        {
            workServicesList = await _workServicesRepository.GetAllAsync();
            worksList = await _worksListRepository.GetAllAsync();
            worksNames = await _workNameRepository.GetAllAsync();

            completedProjectsList = await _completedProjectRepository.GetAllAsync();
            projectCounter = completedProjectsList.Count();
            completedProjectsPhotoList = await _completedProjectPhotoRepository.GetAllAsync();
            return this;
        }
       
    }
}
