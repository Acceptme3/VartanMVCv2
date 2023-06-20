using VartanMVCv2.Domain.Repositories.Abstract;
using VartanMVCv2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace VartanMVCv2.Domain
{
    public class Modelinitializer : IDisposable
    {
        private static Modelinitializer instance;
        private static readonly Lazy<Modelinitializer> instanceLazy = new Lazy<Modelinitializer>(instance);
        private bool _disposed = false;

        

        private readonly IEntityRepository<WorkServices> _workServicesRepository;
        private readonly IEntityRepository<WorksList> _worksListRepository;
        private readonly IEntityRepository<WorksName> _workNameRepository;
        private readonly IEntityRepository<CompletedProject> _completedProjectRepository;
        private readonly IEntityRepository<CompletedProjectPhoto> _completedProjectPhotoRepository;
        private readonly IEntityRepository<Feedback> _feedbackRepository;

        private readonly ILogger<Modelinitializer> _logger;

        private readonly DbContext _dbContext;

        public List<WorkServices> workServicesList { get; set; } = new List<WorkServices>();
        public List<WorksList> worksList { get; set; } = new List<WorksList>();
        public List<WorksName> worksNameList { get; set; } = new List<WorksName>();
        public List<CompletedProject> completedProjectsList { get; set; } = new List<CompletedProject>();
        public List<CompletedProjectPhoto> completedProjectPhotosList { get; set; } = new List<CompletedProjectPhoto>();
        public List<Feedback> feedbackList { get; set; } = new List<Feedback>();

        public bool InitialFlag { get; protected set; } = false;

        /*private Modelinitializer()
        {
            
        }*/


        public Modelinitializer(IEntityRepository<WorkServices> workServicesRepository, IEntityRepository<WorksList> worksListRepository, IEntityRepository<WorksName> workNameRepository, IEntityRepository<CompletedProject> completedProject, IEntityRepository<CompletedProjectPhoto> completedProjectPhoto, IEntityRepository<Feedback> feedback, AplicationDBContext appDBContext, ILogger<Modelinitializer> logger)
        {
            _workServicesRepository = workServicesRepository;
            _worksListRepository = worksListRepository;
            _workNameRepository = workNameRepository;
            _completedProjectRepository = completedProject;
            _completedProjectPhotoRepository = completedProjectPhoto;
            _feedbackRepository = feedback;

            _dbContext = appDBContext;

            _logger = logger;

            instance = this;
        }

        public static Modelinitializer CreateInstance ()
        {
            //return instanceLazy.Value;
            return instance;
        }

        public async Task ModelInitialAsync()
        {
            if (_workServicesRepository == null) 
            {
                Debug.WriteLine("DI conteiner is empty");
            }
            workServicesList = await _workServicesRepository.GetAllAsync();
            worksList = await _worksListRepository.GetAllAsync();
            worksNameList = await _workNameRepository.GetAllAsync();
            completedProjectsList = await _completedProjectRepository.GetAllAsync();
            completedProjectPhotosList = await _completedProjectPhotoRepository.GetAllAsync();
            feedbackList = await _feedbackRepository.GetAllAsync();



            _logger.LogInformation($"Инициализация списков, в рамках вызова метода ModelInitialAsync () завершена. Колличество элементов WorkServices: {workServicesList.Count} \n" +
                $"WorksList: {worksList.Count} \n" +
                $"WorksName: {worksNameList.Count} \n" +
                $"CompletedProjectList: {completedProjectsList.Count} \n" +
                $"CompletedProjectPhoto: {completedProjectPhotosList.Count} \n" +
                $"Feedback: {feedbackList.Count}");  
            InitialFlag = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) 
        {
            if (_disposed) return;
            if (disposing) 
            {
                // позже нужно реализовать удаление управляемых ресурсов 
            }
            // тут очистка неуправляемых ресурсов
            _disposed = true;
        }
        ~Modelinitializer() 
        {
            Dispose(false);
        }
    }
}
