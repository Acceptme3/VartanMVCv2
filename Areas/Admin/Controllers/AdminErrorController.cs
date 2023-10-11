using Microsoft.AspNetCore.Mvc;
using VartanMVCv2.ViewModels;

namespace VartanMVCv2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminErrorController : Controller
    {
        public IActionResult DefaultErrorPage(ErrorViewModel errorViewModel)
        {
            return View(errorViewModel);
        }
    }
}
