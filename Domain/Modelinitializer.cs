using VartanMVCv2.Domain.Repositories.Abstract;
using VartanMVCv2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace VartanMVCv2.Domain
{
    public class Modelinitializer 
    {
        private readonly IEntityRepository<WorkServices> _workServicesRepository;
        private readonly IEntityRepository<WorksList> _worksListRepository;
        private readonly IEntityRepository<WorksName> _workNameRepository;
        private readonly IEntityRepository<CompletedProject> _completedProjectRepository;
        private readonly IEntityRepository<CompletedProjectPhoto> _completedProjectPhotoRepository;
        private readonly IEntityRepository<Feedback> _feedbackRepository;

        private readonly ILogger<Modelinitializer> _logger;

        private readonly DbContext _dbContext;

        private IMemoryCache cache;
        //свойство которое хранит инициализированный объект класса DataModel
        public DataModel dataModel { get; set; } = DataModel.GetInstance();  

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

        public DataModel GetModelObject() 
        {
            return dataModel;
        }

        public async Task<DataModel> ModelInitialAsync(DataModel dataModelInstance)
        {
            dataModelInstance.workServicesList = await _workServicesRepository.GetAllAsync();
            dataModelInstance.worksList = await _worksListRepository.GetAllAsync();
            dataModelInstance.worksNameList = await _workNameRepository.GetAllAsync();
            dataModelInstance.completedProjectsList = await _completedProjectRepository.GetAllAsync();
            dataModelInstance.completedProjectPhotosList = await _completedProjectPhotoRepository.GetAllAsync();
            dataModelInstance.feedbackList = await _feedbackRepository.GetAllAsync();

            _logger.LogInformation($"Инициализация списков, в рамках вызова метода ModelInitialAsync () завершена. Колличество элементов WorkServices: {dataModel.workServicesList.Count} \n" +
                $"WorksList: {dataModel.worksList.Count} \n" +
                $"WorksName: {dataModel.worksNameList.Count} \n" +
                $"CompletedProjectList: {dataModel.completedProjectsList.Count} \n" +
                $"CompletedProjectPhoto: {dataModel.completedProjectPhotosList.Count} \n" +
                $"Feedback: {dataModel.feedbackList.Count}"); 

            return dataModelInstance;
        }

        public async Task<DataModel?> GetDataModelAsync(int id, bool initFlag = false) 
        {
            DataModel? _dataModel = null;

            if (!initFlag) 
            {
                cache.TryGetValue(id, out _dataModel);
            }

            if (_dataModel == null)
            {
                await ModelInitialAsync(dataModel);
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

       

        
    }
}
