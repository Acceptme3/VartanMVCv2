using VartanMVCv2.Domain.Entities;

namespace VartanMVCv2.ViewModels
{
    public class WorkServicesViewModel
    {
        public WorkServices? workServicesExample { get; set; }
        public WorksList? worksCategory { get; set; }
        public WorksName? workName { get; set; }

        public List<WorkServices>? workServices { get; set; } = new List<WorkServices>();
        public List<WorksList>? worksCategories { get; set; }= new List<WorksList>();
        public List<string>? worksNames { get; set; }

        public List<IFormFile> files { get; set; } = new List<IFormFile>();
        

        public WorkServicesViewModel()
        {

        }
    }
}
