using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Data;
using VartanMVCv2.Domain;
using VartanMVCv2.ViewModels;
using Recaptcha.Web.Mvc;
using Recaptcha.Web;
using VartanMVCv2.Services;

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
        private readonly IConfiguration _config ;

        public HomeController(IConfiguration configuration, AplicationDBContext appDbContext, IndexViewModel indexViewModel, ILogger<HomeController> logger, Modelinitializer modelinitializer, DataManager dataManager) 
        {
            _config = configuration;
            _dbContext = appDbContext;
            _indexViewModel = indexViewModel;
            _logger = logger;
            _modelinitializer = modelinitializer;
            _dataManager = dataManager;

            _dataModel = _modelinitializer.GetModelObject();
        }

        [HttpGet]
        public IActionResult Index()
        {
            SetMetaTags(_config["Index:Title"]!, String.Format(_config["Index:Description"]!, Config.CompanyPhone, Config.CompanySecondPhone, Config.CompanyName), String.Format(_config["Index:Keywords"]!,Config.CompanyName));
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
            SetMetaTags(_config["Services:Title"]!, String.Format(_config["Services:Description"]!, Config.CompanyPhone, Config.CompanySecondPhone, Config.CompanyName), _config["Services:Keywords"]!);
            return View( _indexViewModel);
        }

        public IActionResult ServicesByID(Guid id) 
        {
            ViewBag.SelectedServicesID = id;
            _indexViewModel.WorkServicesExample = _dataModel!.workServicesList.FirstOrDefault(ws=>ws.ID == id);
            foreach (var category in _indexViewModel.WorkServicesExample!.WorksCategories)
            {
                foreach (var item in category.Works)
                {
                    _logger.LogInformation($"{item.ID}");
                }
            }
            _logger.LogInformation($"{_indexViewModel.WorkServicesExample?.ID}");
            ViewBag.SelectedServicesImagePath = _indexViewModel.WorkServicesExample?.TitleImagePath;
            SetMetaTags(_indexViewModel.WorkServicesExample?.MetaTitle!, _indexViewModel.WorkServicesExample?.MetaDescription!, _indexViewModel.WorkServicesExample?.MetaKeywords!);
            return View("ServicesByID", _indexViewModel);
        }

        public IActionResult Feedback(IndexViewModel feedback)
        {
            SetMetaTags(String.Format(_config["Feedback:Title"]!, Config.CompanyName), String.Format(_config["Feedback:Description"]!, Config.CompanyPhone, Config.CompanySecondPhone, Config.CompanyName), _config["Feedback:Keywords"]!);
            FeedbackInit();
            return View(_indexViewModel);
        }

        public IActionResult FeedbackPhoto(Guid id) 
        {
            ViewBag.SelectedCompletedProjectID = id;
            _indexViewModel.CompletedProjectExample = _dataModel!.completedProjectsList.FirstOrDefault(ws=>ws.ID == id); 
            SetMetaTags(_indexViewModel.CompletedProjectExample!.MetaTitle!, _indexViewModel.CompletedProjectExample?.MetaDescription!, _indexViewModel.CompletedProjectExample!.MetaKeywords!);
            return View(_indexViewModel);
        }

        public IActionResult About()
        {
            SetMetaTags(String.Format(_config["AboutUs:Title"]!,Config.CompanyName), String.Format(_config["AboutUs:Description"]!, Config.CompanyPhone, Config.CompanySecondPhone, Config.CompanyName), _config["AboutUs:Keywords"]!);
            return View();
        }
    
        public IActionResult Contact()
        {
            SetMetaTags(_config["Contact:Title"]!, String.Format(_config["Contact:Description"]!, Config.CompanyPhone, Config.CompanySecondPhone, Config.CompanyName), _config["Contact:Keywords"]!);
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

        public IActionResult DownloadDocument(string documentName, string contentType)
        {
            
            string filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","doc", documentName);

            if(!System.IO.File.Exists(filePath)) 
            {
                return NotFound();
            }

            // Заголовок Content-Disposition, для того чтобы сказать браузеру что нужно скачать этот файл
            Response.Headers.Add("Content-Disposition", "attachment; filename=" + documentName);
            Response.ContentType = contentType;

            return PhysicalFile(filePath, contentType, documentName);
        }

        [NonAction]
        private void FeedbackInit() 
        {
            _logger.LogInformation("Начинает выполнение метод инициализации списка отображаемых отзывов [тип запроса: NonAction]");
            _indexViewModel.sortFeedbackList = from f in _indexViewModel.dataModelExample!.feedbackList where f.FeedbackEnabled == true select f;
        }
        [NonAction]
        private void SetMetaTags(string title, string description, string keywords) 
        {
            ViewBag.Title = title;
            ViewBag.Description = description;
            ViewBag.Keywords = keywords;
        }
    }
}
