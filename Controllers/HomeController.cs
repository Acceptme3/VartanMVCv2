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

        private Modelinitializer modelinitializer;   // экземпляр класса инициализатора модели один на все время жизни приложения 

        public HomeController(AplicationDBContext appDbContext, IndexViewModel indexViewModel, ILogger<HomeController> logger, Modelinitializer modelinitial) 
        {
            _dbContext = appDbContext;
            _indexViewModel = indexViewModel;
            _logger = logger;
            modelinitializer = modelinitial;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //await _indexViewModel.ViewModelInitialAsync();
            if (!modelinitializer.InitialFlag) // проверяем проинициализированы ли списки с данными модели если нет то получаем данные из бд
            {
               await modelinitializer.ModelInitialAsync();
            }

            if (_indexViewModel.modelinitializer == null) // экземпляр IndexViewModel пойдет в представления поэтому нужно предоставить ему доступ к инициализатору данных. проверяем есть ли ссылка на экземпляр инициализатора
            {
                _indexViewModel.GetInstance(modelinitializer.InitialFlag);// если ссылки нет то задаем только в случае успешной инициализации списков
            }

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
