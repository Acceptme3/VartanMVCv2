using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Data;
using VartanMVCv2.Domain;
using VartanMVCv2.ViewModels;
using Recaptcha.Web.Mvc;
using Recaptcha.Web;

namespace VartanMVCv2.Controllers
{
    public class HomeController : Controller
    {
        private readonly AplicationDBContext _dbContext;
        //глобальная переменная для передачи данных в представление 
        private readonly IndexViewModel _indexViewModel;

        private readonly ILogger<HomeController> _logger;
        //переменная для работы с моделью
        public DataModel? _dataModel;
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

            _dataModel = _modelinitializer.GetModelObject();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            _dataModel = await _modelinitializer.GetDataModelAsync(DataModel.identificator);
            if (_dataModel == null) 
            {
               Exception ex = new Exception("Сервис временно не доступен. Обновите страницу или попробуйте выполнить запрос позднее");
                _logger.LogError(ex.Message);
                return RedirectToAction("DataModelError", "ErrorAplication",new ErrorViewModel { Exception = ex});
            }
            else { _logger.LogInformation("Объект _dataModel (Home/Index) инициализирован."); }
            FeedbackInit();
            return View(_indexViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Index(IndexViewModel client)
        {
            _logger.LogInformation("Начинает выполнение Home/Index, [тип запроса: POST]");

            RecaptchaVerificationHelper recaptchaHelper = this.GetRecaptchaVerificationHelper();
            if (String.IsNullOrEmpty(recaptchaHelper.Response))
            {
                ModelState.AddModelError("", "Капча не может быть пустой.");
                return View(client);
            }
            RecaptchaVerificationResult recaptchaResult = recaptchaHelper.VerifyRecaptchaResponse();
            if (!recaptchaResult.Success)
            {
                ModelState.AddModelError("", "Неккоректное значение капчи.");
            }

            if (ModelState.IsValid) 
            {
                _logger.LogInformation("Екземпляр клиента успешно прошел валидацию на стороне сервера [VALIDATE]");
                await _dataManager.ClientRepository.AddedAsync(client.ClientExample!);
                return View("Confirm");
            }
            _logger.LogInformation("Екземпляр клиента НЕ прошел валидацию на стороне сервера [NOT VALID]");
            return RedirectToAction("IndexToContact",client);
        }

        [HttpPost]
        public async Task<IActionResult> AddFeedbackAsync(IndexViewModel feedback)
        {
            _logger.LogInformation("Начинает выполнение Home/AddFeedbackAsync, [тип запроса: POST]");

            RecaptchaVerificationHelper recaptchaHelper = this.GetRecaptchaVerificationHelper();
            if (String.IsNullOrEmpty(recaptchaHelper.Response))
            {
                ModelState.AddModelError("", "Капча не может быть пустой.");
                return View(feedback);
            }
            RecaptchaVerificationResult recaptchaResult = recaptchaHelper.VerifyRecaptchaResponse();
            if (!recaptchaResult.Success)
            {
                ModelState.AddModelError("", "Неккоректное значение капчи.");
            }

            if (ModelState.IsValid)
            {
                _logger.LogInformation("Екземпляр отзыва клиента успешно прошел валидацию на стороне сервера [VALIDATE]");
                await _dataManager.FeedbackRepository.AddedAsync(feedback.FeedbackExample!);
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

        public IActionResult ServicesByID(Guid id) 
        {
            ViewBag.SelectedServicesID = id;
            _indexViewModel.WorkServicesExample = _dataModel!.workServicesList.FirstOrDefault(ws=>ws.ID == id);
            return View("ServicesByID", _indexViewModel);
        }

        public IActionResult Feedback(IndexViewModel feedback)
        {
            FeedbackInit();
            return View(_indexViewModel);
        }

        public IActionResult FeedbackPhoto(Guid id) 
        {
            ViewBag.SelectedCompletedProjectID = id;
            _indexViewModel.CompletedProjectExample = _dataModel!.completedProjectsList.FirstOrDefault(ws=>ws.ID == id); 
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

        [NonAction]
        private void FeedbackInit() 
        {
            _logger.LogInformation("Начинает выполнение метод инициализации списка отображаемых отзывов [тип запроса: NonAction]");
            _indexViewModel.sortFeedbackList = from f in _indexViewModel.dataModelExample!.feedbackList where f.FeedbackEnabled == true select f;
        }
    }
}
