using VartanMVCv2.Domain.Entities;

namespace VartanMVCv2.ViewModels
{
    public class WorkServicesViewModel
    {
        public WorkServices? workServicesExample { get; set; } = new WorkServices();
        public WorksList? worksCategory { get; set; } = new WorksList();
        public WorksName? workName { get; set; } = new WorksName();

        public List<WorkServices>? workServices { get; set; } = new List<WorkServices>();
        public List<WorksList>? worksCategories { get; set; }= new List<WorksList>();
        public List<string>? worksNames { get; set; } = new List<string>();

        public List<IFormFile> files { get; set; } = new List<IFormFile>();
        

        public WorkServicesViewModel()
        {
           
        }
    }
}
