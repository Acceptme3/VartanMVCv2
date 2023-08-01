using Microsoft.EntityFrameworkCore;
using VartanMVCv2.Domain;
using VartanMVCv2.Domain.Entities;
using VartanMVCv2.Domain.Repositories.Abstract;
using VartanMVCv2.Views;

namespace VartanMVCv2.ViewModels
{
    public class IndexViewModel
    {
        public _ViewStartModel _viewStartModel = new _ViewStartModel();

        public Client? ClientExample { get; set; } = null;
        public Feedback? FeedbackExample { get; set; } = null;

        public IEnumerable<WorksList> sortWorksList { get; set; } = new List<WorksList>();
  
        private readonly DbContext? _dbContext;
        private readonly ILogger<IndexViewModel>? _logger;
        private readonly Modelinitializer? _initializer;

        public  DataModel? dataModelExample;

        public IndexViewModel() {}

        public  IndexViewModel( AplicationDBContext appDBContext, ILogger<IndexViewModel> logger, Modelinitializer modelinitializer) 
        {        
            _dbContext = appDBContext;
            _logger = logger; 
            _initializer = modelinitializer;
            dataModelExample = _initializer.dataModel;
        }

        public void SetInstance(DataModel model)
        {
            dataModelExample = model;                   
        }
       
    }
}
