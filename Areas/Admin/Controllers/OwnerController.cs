using Microsoft.AspNetCore.Mvc;
using VartanMVCv2.Domain;
using VartanMVCv2.Domain.Entities;
using VartanMVCv2.Models;
using VartanMVCv2.ViewModels;
using VartanMVCv2.Services;
using VartanMVCv2.Domain.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.SignalR;
using VartanMVCv2.Domain.Repositories.EntityFramework;

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
        private readonly IHubContext<ProgressHub> _hubContext;

        private DataModel? _dataModel = DataModel.GetInstance();

        private ClientListDataModel _clientDataModel = null!;
        private List<WorksList> worksLists = new List<WorksList>();

        private WorkServicesViewModel _workServicesViewModel = new WorkServicesViewModel();

        public OwnerController(AplicationDBContext dBContext, DataManager dataManager, ILogger<OwnerController> logger, Modelinitializer modelinitializer, SortingManager sortingManager, CurrentViewContext currentViewContext, IHubContext<ProgressHub> hubContext)
        {
            _dbContext = dBContext;
            _dataManager = dataManager;
            _logger = logger;
            _modelInitializer = modelinitializer;
            _sortingManager = sortingManager;
            _currentViewContext = currentViewContext;
            _hubContext = hubContext;

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

        public IActionResult CompletedProjectView(CompletedProjectViewModel completedProjectViewModel)
        {
            completedProjectViewModel.completedProjects = _dataModel!.completedProjectsList;
            completedProjectViewModel.completedProjectPhotos = _dataModel!.completedProjectPhotosList;
            return View(completedProjectViewModel);
        }

        [HttpGet]
        public IActionResult AddCompletedProject()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCompletedProject(CompletedProjectViewModel completedProjectViewModel)
        {
            var CPExample = completedProjectViewModel.completedProjectExample;

            if (ModelState.IsValid)
            {
                FileCheckResult fileCheck = FileCheckResult.CheckUploadFiles(completedProjectViewModel.files, FileCheckResult.defaultExtensions);

                if (fileCheck.Success == false)
                {
                    ViewBag.Message = fileCheck.Message;
                    return View(completedProjectViewModel);
                }

                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "projectPhotos", completedProjectViewModel.completedProjectExample!.Title!);

                _dataManager.CompletedProject.Added(CPExample!); // здесь добавляем в базу данных экземпляр CompletedProject

                fileCheck = await FileCheckResult.FileUpload(completedProjectViewModel.files, uploadPath, AddExampleAction);
                ViewBag.Message = fileCheck.Message;

                return RedirectToAction("CompletedProjectView");
            }

            string? errorMessages = "";
            foreach (var item in ModelState)
            {
                if (item.Value.ValidationState == ModelValidationState.Invalid)
                {
                    errorMessages = $"{errorMessages} \n Ошибки для свойства {item.Key}:\n";
                    foreach (var error in item.Value.Errors)
                    {
                        errorMessages = $"{errorMessages} {error.ErrorMessage} \n";
                    }
                }
            }
            return RedirectToAction("DefaultErrorPage", "AdminError", new ErrorViewModel { Errors = errorMessages });


           async Task AddExampleAction(string filePath, int progress)
            {
                await _hubContext.Clients.All.SendAsync("UpdateProgress", progress);
                CompletedProjectPhoto exampleToDb = new CompletedProjectPhoto { Project = CPExample!, ImagePath = new string(String.Empty).RemoveSubstring(filePath, Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")) };
                _dataManager.CompletedProjectPhoto.Added(exampleToDb); //здесь добавляем в базу данных экземпляр CompletedProjectPhoto
            }
        }

        public IActionResult Metrick()
        {
            string metrickUrl = "https://metrika.yandex.ru/";
            return Redirect(metrickUrl);
        }

        [HttpGet]
        public IActionResult AddWorkServices()
        {
            return View(_workServicesViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddWorkServices(WorkServicesViewModel workServicesViewModel)
        {
            if (!ModelState.IsValid)
            {
                string? errorMessages = "";

                foreach (var item in ModelState)
                {
                    if (item.Value.ValidationState == ModelValidationState.Invalid)
                    {
                        errorMessages = $"{errorMessages}\n Ошибки для свойства {item.Key}:\n";
                        foreach (var error in item.Value.Errors)
                        {
                            errorMessages = $"{errorMessages}{error.ErrorMessage}\n";
                        }
                    }
                }
                return RedirectToAction("DefaultErrorPage", "AdminError", new ErrorViewModel { Errors = errorMessages });
            }

            var WSExample = workServicesViewModel.workServicesExample;
            _workServicesViewModel.worksCategories = workServicesViewModel.worksCategories;

            if (WSExample == null)
            {
                _logger.LogInformation("Объект NULL");
                return RedirectToAction("Index");
            }

            for (int i = 0; i < workServicesViewModel.worksCategories!.Count(); i++)
            {
                List<string> workNameStrings = workServicesViewModel.worksNames![i].SplitString(workServicesViewModel.worksNames[i], ",");
                workServicesViewModel.worksCategories![i].worksNames = workNameStrings.Select(str => new WorksName { Title = str, WorksCategory = workServicesViewModel.worksCategories![i] }).ToList();
            }
            WSExample.worksLists = workServicesViewModel.worksCategories!;

            FileCheckResult fileCheck = FileCheckResult.CheckUploadFiles(workServicesViewModel.files, FileCheckResult.defaultExtensions);
            if (fileCheck.Success == false)
            {
                ViewBag.Message = fileCheck.Message;
                return View(workServicesViewModel);
            }

            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "servicesImage", workServicesViewModel.workServicesExample!.Title!);

            fileCheck = await FileCheckResult.FileUpload(workServicesViewModel.files, uploadPath, AddExampleAction);
            if (fileCheck.Success == false)
            {
                ViewBag.Message = fileCheck.Message;
                return View(workServicesViewModel);
            }

            await _dataManager.Works.AddedAsync(WSExample);

            return RedirectToAction("WorkServicesView");

            async Task AddExampleAction(string filePath,int progress)
            {
                await _hubContext.Clients.All.SendAsync("UpdateProgress", progress);
                WSExample!.TitleImagePath = new string(String.Empty).RemoveSubstring(filePath, Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"));
            }

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

        public async Task<IActionResult> DataInit(string returnAction = "Index")
        {
            _dataModel = await _modelInitializer.GetDataModelAsync(DataModel.identificator, true);
            return RedirectToAction(returnAction);
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Logout", "Account");
        }

        /* **************Selectors Action***************/
        public async Task<IActionResult> EntitySelector(Guid id, string operation, string viewName)
        {
            if (viewName == nameof(ClientView) || viewName == nameof(CompleteClientView))
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
            if (viewName == nameof(CompletedProjectView))
            {
                IEntityRepository<CompletedProject> repository = _dataManager.CompletedProject;
                CompletedProject completedProject = repository.GetById(id);

                if (operation == "del")
                {
                    string directory = "projectPhotos";
                    repository.DeleteEntity(id);
                    FileCheckResult fileCheck = FileCheckResult.FileRemove(directory, completedProject.Title!);
                    ViewBag.Message = fileCheck.Message;
                    await DataInit();
                    return RedirectToAction(viewName);
                }
                return RedirectToAction(viewName);

            }
            if (viewName == nameof(FeedbackView))
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
            if (viewName == nameof(WorkServicesView))
            {
                var workServices = _dataManager.WorkServices.GetById(id);
                if (workServices == null) 
                {
                    string error = "Объект отсутствует в базе данных.";
                    RedirectToAction("DefaultErrorPage", "AdminError", new ErrorViewModel { Errors=error});
                }
                if (operation == "del")
                {
                    List<EntitiesBase> allWorks =_dataManager.WorkServices.GetAllWorksAsync().ToList();// получили все объекты works
                    List<WorksList> worksListsFromDel = allWorks.Where(w => w is WorksList).Cast<WorksList>().Where(w=>w.Services==workServices).ToList();//получили все категории для удаления 
                    List<WorksName> worksNamesFromDel = allWorks.Where(w => w is WorksName).Cast<WorksName>().Where(wn=>worksListsFromDel.Any(wl=>wl.Equals(wn.WorksCategory))).ToList();// получили все работы для удаления
                    
                    foreach (var worksN in worksNamesFromDel)
                    {                
                      _dataManager.WorksName.DeleteEntity(worksN.ID); 
                    }

                    foreach (var worksCategory in worksListsFromDel)
                    {
                        _dataManager.WorksList.DeleteEntity(worksCategory.ID);
                    }
                    _dataManager.WorkServices.DeleteEntity(workServices!.ID);
                    string dir = "servicesImage";
                    FileCheckResult fileCheck = FileCheckResult.FileRemove(dir, workServices.Title!);
                    ViewBag.Message = fileCheck.Message;
                    await DataInit();
                }
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
        void DataModelInit()
        {
            _clientDataModel.InstanceInit(_dataManager.ClientRepository.GetAll);
            _logger.LogInformation($"Элементы в коллекции + {_clientDataModel.clientsQuery.Count()}");
        }// функция ручной иницализации экземпляра с данными

    }

}
