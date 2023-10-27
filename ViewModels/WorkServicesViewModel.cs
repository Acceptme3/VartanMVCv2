using VartanMVCv2.Domain.Entities;

namespace VartanMVCv2.ViewModels
{
    public class WorkServicesViewModel
    {
        public WorkServices? workServicesExample { get; set; } //= new WorkServices();
        public List<WorkServices>? workServices { get; set; } //= new List<WorkServices>();
        public List<string>? worksNames { get; set; }//= new List<string>();

        public List<IFormFile>? files { get; set; } = new List<IFormFile>();
        
        public WorkServicesViewModel()
        {
           
        }
    }
}
