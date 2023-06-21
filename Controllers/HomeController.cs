using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VartanMVCv2.Domain;
using VartanMVCv2.Domain.Entities;
using VartanMVCv2.ViewModels;

namespace VartanMVCv2.Controllers
{
    public class HomeController : Controller
    {
        //пееменная для экземпляра контекста db
        private readonly AplicationDBContext _dbContext;
        //глобальная переменная для хранения списка объектов WorkServices
        private readonly IndexViewModel _indexViewModel;

        private readonly ILogger<HomeController> _logger;

        DataModel? _dataModel;
        private readonly Modelinitializer _modelinitializer;   // экземпляр класса инициализатора модели один на все время жизни приложения 

        public HomeController(AplicationDBContext appDbContext, IndexViewModel indexViewModel, ILogger<HomeController> logger, Modelinitializer modelinitializer) 
        {
            _dbContext = appDbContext;
            _indexViewModel = indexViewModel;
            _logger = logger;
            _modelinitializer = modelinitializer;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            _dataModel = await _modelinitializer.GetDataModelAsync(0);
            _indexViewModel.GetInstance(_dataModel);
            // c этого места продолжим
            

            foreach (Feedback feedback in _indexViewModel!.modelinitializer!.feedbackList)
            {
                _logger.LogInformation($"feedbackList содержит следующие имена в данном порядке:  {feedback.FeedbackClientName}");
            }
            _logger.LogInformation($"Indexing feedback list");
            _indexViewModel.modelinitializer.feedbackList.ToList().Reverse();
            foreach (Feedback feedback in modelinitializer.feedbackList)
            {
                _logger.LogInformation($"feedbackList содержит следующие имена в порядке, определенном сортировкой:  {feedback.FeedbackClientName}");
            }
            
            return View(_indexViewModel);
        }


        [HttpPost]
        public IActionResult Index(IndexViewModel client)
        { 
            if (ModelState.IsValid) 
            {
                Client.clients.Add(client.ClientExample);
                Client.PrintClient();
                return View("Confirm");
            }
            _indexViewModel.ClientExample = client.ClientExample;
            return View("Index", _indexViewModel);
        }

        public IActionResult Services()
        {         
            return View( _indexViewModel);
        }


        public IActionResult ServicesByID(int id) 
        {
            int selectedServicesID = id;
            ViewBag.SelectedServicesID = selectedServicesID;
            _indexViewModel.sortWorksList = _indexViewModel.modelinitializer.worksList.ToList().FindAll(delegate (WorksList works)  { return works.Services.ID == selectedServicesID; });

            foreach (WorksList work in _indexViewModel.sortWorksList)
            {
                _logger.LogInformation($"Имена работ выбранной услуги: {work.Title}");
            }

            return View("ServicesByID", _indexViewModel);
        }

        public IActionResult Feedback()
        {
            return View();
        }

        public IActionResult FeedbackPhoto(int id) 
        {
            int selectedCompletedProjectID = id;
            ViewBag.SelectedCompletedProjectID = selectedCompletedProjectID;
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult ResourseUsed()
        {
            return View();
        }
    }
}
