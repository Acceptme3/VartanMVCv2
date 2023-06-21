using VartanMVCv2.Domain.Repositories.Abstract;
using VartanMVCv2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;

namespace VartanMVCv2.Domain
{
    public class Modelinitializer : IDisposable
    {
        private bool _disposed = false;

        private readonly IEntityRepository<WorkServices> _workServicesRepository;
        private readonly IEntityRepository<WorksList> _worksListRepository;
        private readonly IEntityRepository<WorksName> _workNameRepository;
        private readonly IEntityRepository<CompletedProject> _completedProjectRepository;
        private readonly IEntityRepository<CompletedProjectPhoto> _completedProjectPhotoRepository;
        private readonly IEntityRepository<Feedback> _feedbackRepository;

        private readonly ILogger<Modelinitializer> _logger;

        private readonly DbContext _dbContext;

        private IMemoryCache cache;

        public DataModel dataModel { get; set; }  
        /*public List<WorkServices> workServicesList { get; set; } = new List<WorkServices>();
        public List<WorksList> worksList { get; set; } = new List<WorksList>();
        public List<WorksName> worksNameList { get; set; } = new List<WorksName>();
        public List<CompletedProject> completedProjectsList { get; set; } = new List<CompletedProject>();
        public List<CompletedProjectPhoto> completedProjectPhotosList { get; set; } = new List<CompletedProjectPhoto>();
        public List<Feedback> feedbackList { get; set; } = new List<Feedback>();*/


        public Modelinitializer(IEntityRepository<WorkServices> workServicesRepository, IEntityRepository<WorksList> worksListRepository, IEntityRepository<WorksName> workNameRepository, IEntityRepository<CompletedProject> completedProject, IEntityRepository<CompletedProjectPhoto> completedProjectPhoto, IEntityRepository<Feedback> feedback, AplicationDBContext appDBContext, ILogger<Modelinitializer> logger, IMemoryCache cache)
        {
            _workServicesRepository = workServicesRepository;
            _worksListRepository = worksListRepository;
            _workNameRepository = workNameRepository;
            _completedProjectRepository = completedProject;
            _completedProjectPhotoRepository = completedProjectPhoto;
            _feedbackRepository = feedback;

            _dbContext = appDBContext;
            _logger = logger;
            this.cache = cache;
        }

        public async Task ModelInitialAsync()
        {
            dataModel.workServicesList = await _workServicesRepository.GetAllAsync();
            dataModel.worksList = await _worksListRepository.GetAllAsync();
            dataModel.worksNameList = await _workNameRepository.GetAllAsync();
            dataModel.completedProjectsList = await _completedProjectRepository.GetAllAsync();
            dataModel.completedProjectPhotosList = await _completedProjectPhotoRepository.GetAllAsync();
            dataModel.feedbackList = await _feedbackRepository.GetAllAsync();

            _logger.LogInformation($"Инициализация списков, в рамках вызова метода ModelInitialAsync () завершена. Колличество элементов WorkServices: {dataModel.workServicesList.Count} \n" +
                $"WorksList: {dataModel.worksList.Count} \n" +
                $"WorksName: {dataModel.worksNameList.Count} \n" +
                $"CompletedProjectList: {dataModel.completedProjectsList.Count} \n" +
                $"CompletedProjectPhoto: {dataModel.completedProjectPhotosList.Count} \n" +
                $"Feedback: {dataModel.feedbackList.Count}");  
        }

        public async Task<DataModel?> GetDataModelAsync(int id) 
        {
            cache.TryGetValue(id, out DataModel? _dataModel);
            if (_dataModel == null)
            {
                await ModelInitialAsync();
                _dataModel = dataModel;
                _logger.LogInformation($"Model created and cashing {id}");
                cache.Set(id, _dataModel);
            }
            else 
            {
                _logger.LogInformation($"DataModel извлечен из кэша.");
            }
            return _dataModel;
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
