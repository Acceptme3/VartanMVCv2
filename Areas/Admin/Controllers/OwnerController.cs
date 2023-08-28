using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using VartanMVCv2.Domain;
using VartanMVCv2.Domain.Entities;
using VartanMVCv2.Models;
using VartanMVCv2.ViewModels;


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

        private ClientListDataModel _clientDataModel = null!;

        public OwnerController(AplicationDBContext dBContext, DataManager dataManager, ILogger<OwnerController> logger, Modelinitializer modelinitializer, SortingManager sortingManager)
        {
            _dbContext = dBContext;
            _dataManager = dataManager;
            _logger = logger;
            _modelInitializer = modelinitializer;
            _sortingManager = sortingManager;

            _clientDataModel = ClientListDataModel.GetInstance();
        }

        public IActionResult Index(ClientViewModel clientViewModel)
        {
            DataModelInit();
            SmallBoxValueInit();
            return View();
        }

        public IActionResult ClientView(ClientViewModel clientViewModel)
        {
            string? forb = Url.Action("Selector", "Owner", "Admin");
            _logger.LogInformation($"Пример ссылки {forb}");
            clientViewModel.clients = _clientDataModel.clientsList;
            return View(clientViewModel);
        }

        public IActionResult CompleteClientView(ClientViewModel clientViewModel)
        {
            DataModelInit();
            clientViewModel.clients = SortingManager.GetAllClientForACondition(_clientDataModel.clientsList, false);
            _logger.LogInformation($"Колличество завершенных клиентов  = {clientViewModel.clients.Count()}");
            return View(clientViewModel);
        }

        public IActionResult FeedbackView()
        {
            return View();
        }

        public IActionResult AddWorkServices()
        {
            return View();
        }

        public IActionResult WorkServicesView()
        {
            return View();
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

        [HttpPost]
        public IActionResult Selector (Guid id, string operation)
        {
            Guid _id = id;
            string _op = operation;

            _logger.LogInformation($"Идентификатор клиента  {id}");
            _logger.LogInformation($"Операция над клиентом  {operation}");


            if (_op == "del")
            {
                _dataManager.ClientRepository.DeleteEntity(id);
                DataModelInit();
                _logger.LogInformation("Так должно быть!");
                return RedirectToAction("ClientView");
            }

            if (_op == "to") 
            {
                _logger.LogInformation("Вызван Update");
                Client? client = _dataManager.ClientRepository.GetById(_id)!;
                if(client.IsComplete==false)
                {
                    client.IsComplete = true;
                    _dataManager.ClientRepository.Update(client);
                    DataModelInit();
                    return RedirectToAction("CompleteClientView");
                }

                if (client.IsComplete == true) 
                {
                     client.IsComplete = false;             
                    _dataManager.ClientRepository.Update(client);
                    DataModelInit();
                    return RedirectToAction("ClientView");
                }                             
                return RedirectToAction("ClientView");
            }
            _logger.LogInformation("Блядство!");
            return RedirectToAction("ClientView");
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Logout", "Account");
        }

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
    }
}
