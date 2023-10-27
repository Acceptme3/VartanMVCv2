using Microsoft.AspNetCore.Mvc;
using VartanMVCv2.ViewModels;

namespace VartanMVCv2.Controllers
{
    public class ErrorAplicationController : Controller
    {
        public IActionResult ErrorPage()
        {
            return View();
        }

        public IActionResult DataModelError (ErrorViewModel errorViewModel) 
        {
            ViewBag.ErrorTitle = "Сервис недоступен";
            if (errorViewModel.Exception == null)
                return View("ErrorPage");

            return View(errorViewModel);
        }
    }
}
