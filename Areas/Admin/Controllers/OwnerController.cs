using Microsoft.AspNetCore.Mvc;
using VartanMVCv2.Domain;
using VartanMVCv2.Domain.Entities;
using VartanMVCv2.Models;
using VartanMVCv2.ViewModels;
using VartanMVCv2.Services;
using VartanMVCv2.Domain.Repositories.Abstract;
using System.Security.Cryptography;
using System.Drawing;

namespace VartanMVCv2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OwnerController : Controller
    {
        private readonly AplicationDBContext _dbContext;
        private readonly DataManager _dataManager;
        private readonly ILogger<OwnerController> _logger;
        private readonly Modelinitializer _modelInitializer;
        private readonly SortingManager _sortingManager;
        private readonly CurrentViewContext _currentViewContext;

        private DataModel? _dataModel = DataModel.GetInstance();

        private ClientListDataModel _clientDataModel = null!;
        private List<WorksList> worksLists = new List<WorksList>();

        public OwnerController(AplicationDBContext dBContext, DataManager dataManager, ILogger<OwnerController> logger, Modelinitializer modelinitializer, SortingManager sortingManager, CurrentViewContext currentViewContext)
        {
            _dbContext = dBContext;
            _dataManager = dataManager;
            _logger = logger;
            _modelInitializer = modelinitializer;
            _sortingManager = sortingManager;
            _currentViewContext = currentViewContext;

            _clientDataModel = ClientListDataModel.GetInstance();
        }

        public IActionResult Index(ClientViewModel clientViewModel)
        {
            DataModelInit();
            SmallBoxValueInit();
            clientViewModel.clients = _sortingManager.GetAllClientForADate(_clientDataModel.clientsList, DateTime.Now, SortingTime.Day);
            return View(clientViewModel);
        }

        public IActionResult ClientView(ClientViewModel clientViewModel)
        {
            clientViewModel.clients = _clientDataModel.clientsList;
            return View(clientViewModel);
        }

        public IActionResult CompleteClientView(ClientViewModel clientViewModel)
        {
            DataModelInit();
            clientViewModel.clients = SortingManager.GetAllClientForACondition(_clientDataModel.clientsList, false);        
            return View(clientViewModel);
        }

        public async Task<IActionResult> FeedbackView(FeedbackViewModel feedbackViewModel)
        {
            if (_dataModel!.feedbackList != null)
            {
                feedbackViewModel.feedbacks = _dataModel.feedbackList;
                return View(feedbackViewModel);
            }
            else 
            {
               await DataInit();
               await FeedbackView(feedbackViewModel);
            }
                return View(feedbackViewModel);
        }

        public IActionResult CompletedProjectView (CompletedProjectViewModel completedProjectViewModel)
        {
            completedProjectViewModel.completedProjects = _dataModel!.completedProjectsList;
            completedProjectViewModel.completedProjectPhotos = _dataModel!.completedProjectPhotosList;
            return View(completedProjectViewModel);
        }

        [HttpGet]
        public IActionResult AddCompletedProject ()
        {
           return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCompletedProject(CompletedProjectViewModel completedProjectViewModel)
        {
            var CPExample = completedProjectViewModel.completedProjectExample;

            long maxFileSize = 5 * 1024 * 1024; // Максимальный размер файла (5 МБ)
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png" }; // Разрешенные расширения файлов
            var files = completedProjectViewModel.files;

            if (files == null)
            {
                _logger.LogInformation("Список с фото пуст");
                ModelState.AddModelError("files", "Вы не выбрали ни одного файла");
                return View(completedProjectViewModel);
            }

            foreach(var file in files)
            {
                if (file.Length > maxFileSize)
                {
                    _logger.LogInformation("Фото слишком большое");
                    ModelState.AddModelError("files", $"Файл '{file.FileName}' превышает максимальный размер {maxFileSize} байт");
                    return View(completedProjectViewModel);
                }
                if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
                {
                    ModelState.AddModelError("files", $"Недопустимый тип файла '{file.FileName}'");
                    return View(completedProjectViewModel);
                }
            }
            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "projectPhotos",completedProjectViewModel.completedProjectExample!.Title!);
            _logger.LogInformation($"{uploadPath}");

            _dataManager.CompletedProject.Added(CPExample!); // здесь добавляем в базу данных экземпляр CompletedProject

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var filePath = Path.Combine(uploadPath, file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    CompletedProjectPhoto exampleToDb = new CompletedProjectPhoto { Project = CPExample!, ImagePath = RemoveSubstring(filePath,Path.Combine(Directory.GetCurrentDirectory(),"wwwroot"))};
                    _logger.LogInformation($"Поля экземпляра {exampleToDb.ID} ,{exampleToDb.ImagePath},");
                    _dataManager.CompletedProjectPhoto.Added(exampleToDb); //здесь добавляем в базу данных экземпляр CompletedProjectPhoto
                }          
            }
            return RedirectToAction("CompletedProjectView");
        }

        public string UploadFiles(IFormFile[] files, string finalCatalogName) 
        {
            
        }


        public bool CheckUploadFiles(IFormFile[] files, long maxFileSize, string[] extensions)
        {

            bool result;
            return result;
        }

        public IActionResult Metrick()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddWorkServices()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddWorkServices(WorkServicesViewModel workServicesViewModel)
        {
            var WSExample = workServicesViewModel.workServicesExample;
            List<string> workNameStrings = new List<string>();
            if (WSExample == null) 
            {
                return RedirectToAction("Index"); 
            }
            
            for (int i = 0; i < workServicesViewModel.worksCategories!.Count(); i++) 
            {
                workNameStrings = workServicesViewModel.worksNames![i].SplitString(workServicesViewModel.worksNames[i], ",");
                workServicesViewModel.worksCategories![i].worksNames = workNameStrings.Select(str => new WorksName { Title = str, WorksCategory = workServicesViewModel.worksCategories![i] }).ToList();
            }
            WSExample.worksLists = workServicesViewModel.worksCategories!;
            
            return View();
        }

        public IActionResult WorkServicesView(WorkServicesViewModel workServicesViewModel)
        {
            workServicesViewModel.workServices = _dataModel!.workServicesList;
            return View(workServicesViewModel);
        }

        public IActionResult UserSettings()
        {
            return View();
        }

        public IActionResult Commits()
        {
            return View();
        }

        public IActionResult LogsExplorer()
        {
            return View();
        }

        public async Task<IActionResult> DataInit ()
        {
            _dataModel = await _modelInitializer.GetDataModelAsync(DataModel.identificator, true);
            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Logout", "Account");
        }

        /* **************Selectors Action***************/
        public async Task<IActionResult> EntitySelector(Guid id, string operation, string viewName)
        {
            if (viewName == "ClientView" || viewName == "CompletedClientView")
            {
                IClientRepository repository = _dataManager.ClientRepository;

                if (operation == "del")
                {
                    repository.DeleteEntity(id);
                    DataModelInit();
                    return RedirectToAction(viewName);
                }

                if (operation == "to")
                {
                    Client? client = repository.GetById(id)!;
                    if (client.IsComplete == false)
                    {
                        client.IsComplete = true;
                        repository.Update(client);
                        DataModelInit();
                        return RedirectToAction(viewName);
                    }

                    if (client.IsComplete == true)
                    {
                        client.IsComplete = false;
                        _dataManager.ClientRepository.Update(client);
                        DataModelInit();
                        return RedirectToAction(viewName);
                    }                  
                }

                return RedirectToAction(viewName);
            }
            if (viewName == "CompletedProjectView") 
            {
                IEntityRepository<CompletedProject> repository = _dataManager.CompletedProject;
                CompletedProject completedProject = repository.GetById(id);

                if (operation == "del")
                {
                    repository.DeleteEntity(id);
                    FileRemove(completedProject.Title!);
                    await DataInit();
                    return RedirectToAction(viewName);
                }
                return RedirectToAction(viewName);

            }
            if (viewName == "FeedbackView") 
            {
                IEntityRepository<Feedback> repository = _dataManager.Feedback;

                if (operation == "del")
                {
                    repository.DeleteEntity(id);
                    await DataInit();
                    return RedirectToAction(viewName);
                }

                if (operation == "upd")
                {
                    Feedback feedback = repository.GetById(id);
                    if (feedback != null)
                    {
                        if (feedback.FeedbackEnabled == false)
                        {
                            feedback.FeedbackEnabled = true;
                            repository.SaveEntities(feedback);
                            await DataInit();
                            return RedirectToAction(viewName);
                        }

                        if (feedback.FeedbackEnabled == true)
                        {
                            feedback.FeedbackEnabled = false;
                            repository.SaveEntities(feedback);
                            await DataInit();
                            return RedirectToAction(viewName);
                        }
                    }
                    return RedirectToAction(viewName);
                }
                _logger.LogInformation("Entyty Selector, EMPTY  Operation NONE");
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        /* **************NonActions***************/
        [NonAction]
        void SmallBoxValueInit() 
        {
            int clientCount = _clientDataModel.clientsList.Count();
            ViewBag.ClientCount = clientCount;

            int todayClientCount = _sortingManager.GetAllClientForADate(_clientDataModel.clientsList, DateTime.Now, SortingTime.Day).Count();
            ViewBag.TodayClientCount = todayClientCount;

            int monthClientCount = _sortingManager.GetAllClientForADate(_clientDataModel.clientsList, DateTime.Now, SortingTime.Month).Count();
            ViewBag.MonthClientCount = monthClientCount;

            int yearClientCount = _sortingManager.GetAllClientForADate(_clientDataModel.clientsList, DateTime.Now, SortingTime.Year).Count();
            ViewBag.YearClientCount = yearClientCount;
        }
        [NonAction]
        void DataModelInit () 
        {
            _clientDataModel.InstanceInit(_dataManager.ClientRepository.GetAll);
            _logger.LogInformation($"Элементы в коллекции + {_clientDataModel.clientsQuery.Count()}");
        }// функция ручной иницализации экземпляра с данными
        [NonAction]
        static string RemoveSubstring(string inputString, string substring)
        {
            // Проверка на пустую строку или пустой фрагмент
            if (string.IsNullOrEmpty(inputString) || string.IsNullOrEmpty(substring))
            {
                return inputString;
            }

            // Удаление фрагмента из строки
            string result = inputString.Replace(substring, string.Empty);

            return result;
        }
        [NonAction]
        string FileRemove(string catalogName) 
        {
            string dirPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "projectPhotos");
            string path = Path.Combine(dirPath, catalogName);
            string resultMessage = "";

            try
            {
                // Проверяем, существует ли указанный каталог
                if (Directory.Exists(path))
                {
                    // Удаляем каталог и все его содержимое
                    Directory.Delete(path, true);
                    resultMessage = "Каталог успешно удален.";
                    return resultMessage;
                }
                else
                {
                    resultMessage = "Каталог не существует.";
                    return resultMessage;
                }
            }
            catch (Exception ex)
            {
                resultMessage = $"Ошибка при удалении каталога: {ex.Message}";
                return resultMessage;
            }
        }

    }

}
