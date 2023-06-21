using VartanMVCv2.Domain.Entities;

namespace VartanMVCv2.Domain
{
    public class DataModel
    {
        public List<WorkServices> workServicesList { get; set; } = new List<WorkServices>();
        public List<WorksList> worksList { get; set; } = new List<WorksList>();
        public List<WorksName> worksNameList { get; set; } = new List<WorksName>();
        public List<CompletedProject> completedProjectsList { get; set; } = new List<CompletedProject>();
        public List<CompletedProjectPhoto> completedProjectPhotosList { get; set; } = new List<CompletedProjectPhoto>();
        public List<Feedback> feedbackList { get; set; } = new List<Feedback>();


    }
}
