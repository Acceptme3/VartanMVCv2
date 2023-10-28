using Microsoft.AspNetCore.Mvc;
using VartanMVCv2.Domain;
using VartanMVCv2.Domain.Entities;
using VartanMVCv2.Models;
using VartanMVCv2.ViewModels;
using VartanMVCv2.Services;
using VartanMVCv2.Domain.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.SignalR;

namespace VartanMVCv2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OwnerController : Controller
    {
        private readonly AplicationDBContext _dbContext;
        private readonly ILogger<OwnerController> _logger;

        private readonly DataManager _dataManager;
        private readonly Modelinitializer _modelInitializer;
        private DataModel? _dataModel;

        private readonly SortingManager _sortingManager;
        private readonly CurrentViewContext _currentViewContext;
        private readonly IHubContext<ProgressHub> _hubContext;
        private ClientListDataModel _clientDataModel = null!;

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

            _dataModel = _modelInitializer.GetModelObject();

            _clientDataModel = ClientListDataModel.GetInstance();
        }

        public IActionResult Index(ClientViewModel clientViewModel)
        {
            DataModelInit(); // инициализщация таблиц с клиентами
            SmallBoxValueInit();
            clientViewModel.clients = _sortingManager.GetAllClientForADate(_clientDataModel.clientsList, DateTime.Now, SortingTime.Day); // заполнение данными списка клиентов на странице Index
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

        public IActionResult FeedbackView(FeedbackViewModel feedbackViewModel)
        {
            feedbackViewModel.feedbacks = _dataModel!.feedbackList;
            return View(feedbackViewModel);
        }

        public IActionResult CompletedProjectView(CompletedProjectViewModel completedProjectViewModel)
        {        
            completedProjectViewModel.completedProjects = _dataModel!.completedProjectsList;
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

                _dataManager.CompletedProjectRepository.Added(CPExample!); // здесь добавляем в базу данных экземпляр CompletedProject

                fileCheck = await FileCheckResult.FileUpload(completedProjectViewModel.files, uploadPath, AddExampleAction);
                ViewBag.Message = fileCheck.Message;
                await DataInitNonRedirect();
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
        public IActionResult GetAddWorkServices(Guid id)
        {
            var WS = _dataManager.WorkServicesRepository.GetById(id);
            List<string> workStrings = new List<string>();

            if (WS == null)
            {
                WS = new WorkServices { ID = Guid.NewGuid() };
                return View(new WorkServicesViewModel { workServicesExample = WS });
            }

            foreach (var category in WS.WorksCategories) 
            {
                workStrings.Add(SortingManager.ConvertPropertyToString(category.Works, "Title"));
            }

            foreach (var item in workStrings)
            {
                _logger.LogInformation($"{item}");
            }

            return View(new WorkServicesViewModel {workServicesExample = WS});
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

            _logger.LogInformation($"{WSExample.WorksCategories.Count}");



            for (int i = 0; i < WSExample!.WorksCategories!.Count(); i++)
            {
                List<string> workNameStrings = workServicesViewModel.worksNames![i].SplitString(workServicesViewModel.worksNames[i], ",");
                WSExample.WorksCategories![i].Works = workNameStrings.Select(str => new Work { Title = str, WorksCategory = WSExample.WorksCategories![i] }).ToList();
            }
            
            FileCheckResult fileCheck = FileCheckResult.CheckUploadFiles(workServicesViewModel.files!, FileCheckResult.defaultExtensions);
            if (fileCheck.Success == false)
            {
                ViewBag.Message = fileCheck.Message;
                return View(workServicesViewModel);
            }

            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "servicesImage", workServicesViewModel.workServicesExample!.Title!);

            fileCheck = await FileCheckResult.FileUpload(workServicesViewModel.files!, uploadPath, AddExampleAction);
            if (fileCheck.Success == false)
            {
                ViewBag.Message = fileCheck.Message;
                return View(workServicesViewModel);
            }

            if (workServicesViewModel.workServicesExample!.ID != Guid.Empty)
            {
                _dataManager.WorkServicesRepository.SaveEntities(WSExample);
                return RedirectToAction("WorkServicesView");
            }
            else
            { 
                WSExample.ID = Guid.NewGuid();
                await _dataManager.WorkServicesRepository.AddedAsync(WSExample);
            }
            
            await DataInitNonRedirect();
            return RedirectToAction("WorkServicesView");

            async Task AddExampleAction(string filePath, int progress)
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

        public IActionResult RedirectToErrorPage(IActionResult actionResult)
        {
            return actionResult;
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
                IEntityRepository<CompletedProject> repository = _dataManager.CompletedProjectRepository;
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
                IEntityRepository<Feedback> repository = _dataManager.FeedbackRepository;

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
                var workServices = _dataManager.WorkServicesRepository.GetById(id);
                if (workServices == null)
                {
                    string error = "Объект отсутствует в базе данных.";
                    RedirectToAction("DefaultErrorPage", "AdminError", new ErrorViewModel { Errors = error });
                }
                if (operation == "del")
                {
                    _dataManager.WorkServicesRepository.DeleteEntity(id);
                    string dir = "servicesImage";
                    FileCheckResult fileCheck = FileCheckResult.FileRemove(dir, workServices!.Title!);
                    ViewBag.Message = fileCheck.Message;
                   return await DataInit("WorkServicesView");
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
        } // инициализация блоков на странице Index
        [NonAction]
        void DataModelInit()
        {
            _clientDataModel.InstanceInit(_dataManager.ClientRepository.GetAll);
        }// инициализация списков с клиентами
        [NonAction]
        public async Task DataInitNonRedirect()
        {
            _dataModel = await _modelInitializer.GetDataModelAsync(DataModel.identificator, true);
        }// принудительная инициализация сервисов без редиректа

    }

}
