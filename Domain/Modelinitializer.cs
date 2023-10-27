using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Serilog;

namespace VartanMVCv2.Domain
{
    public class Modelinitializer
    {
        private readonly DataManager _dataManager;
        private readonly ILogger<Modelinitializer> _logger;
        private IMemoryCache cache;
        //свойство которое хранит инициализированный объект класса DataModel
        private DataModel dataModel { get; set; } = DataModel.GetInstance();

        public Modelinitializer(DataManager dataManager, ILogger<Modelinitializer> logger, IMemoryCache cache)
        {
            _dataManager = dataManager;
            _logger = logger;
            this.cache = cache;
        }

        public DataModel GetModelObject()
        {
            return dataModel;
        }

        private async Task ModelInitialAsync(DataModel dataModelInstance)
        {
            dataModelInstance.workServicesList = await _dataManager.WorkServicesRepository.GetAllAsync();
            foreach (var service in dataModelInstance.workServicesList)
            {
                foreach (var category in service.WorksCategories)
                {
                    foreach(var works in category.Works) 
                    {
                        _logger.LogInformation($"{works.Title}");
                    }
                }

            }
            dataModelInstance.completedProjectsList = await _dataManager.CompletedProjectRepository.GetAllAsync();
            foreach (var CProject in dataModelInstance.completedProjectsList)
            { 
                foreach(var CPPhotos in CProject.Photos) 
                {
                    _logger.LogInformation($"{CPPhotos.Title}");
                }
            }
            dataModelInstance.feedbackList = await _dataManager.FeedbackRepository.GetAllAsync();
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
