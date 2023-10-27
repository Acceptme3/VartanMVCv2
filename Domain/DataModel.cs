using VartanMVCv2.Domain.Entities;

namespace VartanMVCv2.Domain
{
    public class DataModel
    {
        private static readonly Lazy<DataModel> _instance = new Lazy<DataModel>(() => new DataModel());
        public int Id { get; set; }

        public const int identificator = 0;

        public List<WorkServices> workServicesList { get; set; } = new List<WorkServices>();
        public List<CompletedProject> completedProjectsList { get; set; } = new List<CompletedProject>();
        public List<Feedback> feedbackList { get; set; } = new List<Feedback>();

        private DataModel() 
        {
            Id = identificator;
        }

        public static DataModel GetInstance() 
        {
            return _instance.Value;
        }


    }
}
