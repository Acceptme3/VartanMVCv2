using Microsoft.AspNetCore.Mvc;

namespace VartanMVCv2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OwnerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ClientView() 
        {
            return View();
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

        public IActionResult Logout()
        {
            return RedirectToAction("Logout", "Account");
        }
    }
}
