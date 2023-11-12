using VartanMVCv2.Domain.Entities;

namespace VartanMVCv2.ViewModels
{
    public class WorkServicesViewModel
    {
        public WorkServices? workServicesExample { get; set; }
        public List<WorkServices>? workServices { get; set; }

        public List<IFormFile>? files { get; set; } = new List<IFormFile>();
        
        public WorkServicesViewModel()
        {
           
        }

        
    }
}
