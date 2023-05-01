using Microsoft.AspNetCore.Mvc;

namespace VartanMVCv2.Controllers
{
    public class ErrorAplicationController : Controller
    {
        public IActionResult ErrorPage()
        {
            return View();
        }
    }
}
