using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Data;
using System.Diagnostics;
using VartanMVCv2.Domain;
using VartanMVCv2.Domain.Entities;
using VartanMVCv2.ViewModels;

namespace VartanMVCv2.Controllers
{
    public class HomeController : Controller
    {
        private readonly AplicationDBContext _dbContext;
        //глобальная переменная для передачи данных в представление 
        private readonly IndexViewModel _indexViewModel;

        private readonly ILogger<HomeController> _logger;
        //переменная для работы с моделью
        public DataModel? _dataModel = DataModel.GetInstance();
        // экземпляр класса инициализатора модели 
        private readonly Modelinitializer _modelinitializer;

        private readonly DataManager _dataManager;

        public HomeController(AplicationDBContext appDbContext, IndexViewModel indexViewModel, ILogger<HomeController> logger, Modelinitializer modelinitializer, DataManager dataManager) 
        {
            _dbContext = appDbContext;
            _indexViewModel = indexViewModel;
            _logger = logger;
            _modelinitializer = modelinitializer;
            _dataManager = dataManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Начинает выполнение Home/Index [тип запроса: GET]");
            _dataModel = await _modelinitializer.GetDataModelAsync(DataModel.identificator);
            if (_dataModel == null) 
            {
               Exception ex = new Exception("Объект _dataModel (Home/Index) не прошел инициализацию.");
                _logger.LogError(ex.Message);
            }
            else { _logger.LogInformation("Объект _dataModel (Home/Index) инициализирован."); }
            return View(_indexViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Index(IndexViewModel client)
        {
            //client.ClientExample = new Client();

            //_logger.LogInformation($"NameClient - {client.ClientExample.Name}");

            _logger.LogInformation("Начинает выполнение Home/Index, [тип запроса: POST]");

            if (ModelState.IsValid) 
            {
                _logger.LogInformation("Екземпляр клиента успешно прошел валидацию на стороне сервера [VALIDATE]");
                await _dataManager.ClientRepository.AddedAsync(client.ClientExample);
                return View("Confirm");
            }
            _logger.LogInformation("Екземпляр клиента НЕ прошел валидацию на стороне сервера [NOT VALID]");
           //_indexViewModel.ClientExample = client.ClientExample;
            return RedirectToAction("IndexToContact",client);
        }

        [HttpPost]
        public async Task<IActionResult> AddFeedbackAsync(IndexViewModel feedback)
        {
            //feedback.FeedbackExample = new Feedback();
            // _indexViewModel.FeedbackExample = feedback.FeedbackExample;
            _logger.LogInformation("Начинает выполнение Home/AddFeedbackAsync, [тип запроса: POST]");

/*            _logger.LogInformation($"Все поля модели: Имя {feedback.FeedbackExample.FeedbackClientName} + \n" +
                $"Телефон {feedback.FeedbackExample.FeedbackPhone}+\n" +
                $"Текст отзыва {feedback.FeedbackExample.FeedbackText}");*/

            foreach (var item in ModelState)
            {
                _logger.LogInformation($"Пара ключ значение {item}");
            }

            if (ModelState.IsValid)
            {
                _logger.LogInformation("Екземпляр отзыва клиента успешно прошел валидацию на стороне сервера [VALIDATE]");
                await _dataManager.Feedback.AddedAsync(feedback.FeedbackExample);
                return View("Confirm");
            }
            _logger.LogInformation("Екземпляр клиента НЕ прошел валидацию на стороне сервера [NOT VALID]");
            string validErrors = "";
            foreach (var item in ModelState)
            {
                // если для определенного элемента имеются ошибки
                if (item.Value.ValidationState == ModelValidationState.Invalid)
                {
                    validErrors = $"{validErrors}\n Ошибки для свойства {item.Key}:\n";
                    _logger.LogInformation(validErrors);
                    // пробегаемся по всем ошибкам
                    foreach (var error in item.Value.Errors)
                    {
                        validErrors = $"{validErrors}{error.ErrorMessage}\n";
                        _logger.LogInformation(validErrors);
                    }
                }
            }
            //_indexViewModel.FeedbackExample = feedback.FeedbackExample;
            return RedirectToAction("Index", feedback);
        }

        public IActionResult Services()
        {
            _logger.LogInformation("Начинает выполнение Home/Services [тип запроса: GET]");
            return View( _indexViewModel);
        }


        public IActionResult ServicesByID(int id) 
        {
            _logger.LogInformation("Начинает выполнение Home/ServicesByID [тип запроса: GET]");
            int selectedServicesID = id;
            ViewBag.SelectedServicesID = selectedServicesID;

            _indexViewModel.sortWorksList = _indexViewModel.dataModelExample!.worksList.ToList().FindAll(delegate (WorksList works)  { return works.Services.ID == selectedServicesID; });

            foreach (WorksList work in _indexViewModel.sortWorksList)
            {
                _logger.LogInformation($"Имена работ выбранной услуги: {work.Title}");
            }

            return View("ServicesByID", _indexViewModel);
        }

        public IActionResult Feedback(IndexViewModel feedback)
        {
            return View(_indexViewModel);
        }

        public IActionResult FeedbackPhoto(int id) 
        {
            int selectedCompletedProjectID = id;
            ViewBag.SelectedCompletedProjectID = selectedCompletedProjectID;
            return View(_indexViewModel);
        }

        public IActionResult About()
        {
            return View();
        }

        
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult IndexToContact(IndexViewModel client)
        {
            return View("Contact");
        }

        public IActionResult ResourseUsed()
        {
            return View();
        }
    }
}
