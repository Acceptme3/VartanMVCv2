using VartanMVCv2.Domain.Entities;

namespace VartanMVCv2.ViewModels
{
    public class CompletedProjectViewModel
    {
        public CompletedProject? completedProjectExample { get; set; }
        public List<CompletedProject>? completedProjects { get; set; }

        public CompletedProjectPhoto completedProjectPhotoExample { get; set; } 
        public List<CompletedProjectPhoto> completedProjectPhotos { get; set; }

        public List<IFormFile> files { get; set; } = new List<IFormFile>();

        public CompletedProjectViewModel()
        {
            
        }
    }
}
