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

        public HomeController(AplicationDBContext appDbContext, IndexViewModel indexViewModel) 
        {
            _dbContext = appDbContext;
            _indexViewModel = indexViewModel;         
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _indexViewModel.ViewModelInitialAsync());
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
           // IndexViewModel indexViewModel = await ModelInitializeAsync();
            IndexViewModel.sortWorksList = IndexViewModel.worksList.ToList().FindAll(delegate (WorksList works)  { return works.Services.ID == selectedServicesID; });
            foreach (WorksList work in IndexViewModel.sortWorksList)
            {
                Debug.WriteLine(work.Services.ID);
            }
            //indexViewModel = new() { worksList = worksLists };
           // indexViewModel.worksNames = works;
            return View("ServicesByID", _indexViewModel);
        }

        public IActionResult Feedback()
        {
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

        /*private async Task<IndexViewModel> ModelInitializeAsync() 
        {
            services = await eFWorkServices.GetAllAsync();
            worksCategory = await eFWorksListRepositories.GetAllAsync();
            *//*foreach(WorksList work in worksCategory) 
            {
                Debug.WriteLine(work.Services.ID);
            }*//*
            works = await eFWorksNameRepository.GetAllAsync();
            *//*foreach (WorksName work in works)
            {
                Debug.WriteLine(work.WorksCategory.ID);
            }*//*
            //сборка viewModel 
            IndexViewModel indexModelWorkServices = new() { WorkServicesList = services };
            return indexModelWorkServices;
        }*/

    }
}
