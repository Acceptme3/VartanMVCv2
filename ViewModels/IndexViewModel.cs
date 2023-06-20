using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using VartanMVCv2.Domain;
using VartanMVCv2.Domain.Entities;
using VartanMVCv2.Domain.Repositories.Abstract;
using VartanMVCv2.Views;

namespace VartanMVCv2.ViewModels
{
    public class IndexViewModel
    {
        public _ViewStartModel _viewStartModel = new _ViewStartModel();

        public Client ClientExample { get; set; } = new Client();

        public IEnumerable<WorksList> sortWorksList { get; set; } = new List<WorksList>();
  
        private readonly DbContext _dbContext;
        private readonly ILogger<IndexViewModel> _logger;

        public Modelinitializer modelinitializer = Modelinitializer.CreateInstance();

        public IndexViewModel( AplicationDBContext appDBContext, ILogger<IndexViewModel> logger) 
        {        
            _dbContext = appDBContext;
            _logger = logger;          
        }

        public void GetInstance(bool initialStatus)
        {
            if (initialStatus) 
            {
                modelinitializer = Modelinitializer.CreateInstance();
            }           
        }
       
    }
}
