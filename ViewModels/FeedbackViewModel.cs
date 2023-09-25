using VartanMVCv2.Domain.Entities;

namespace VartanMVCv2.ViewModels
{
    public class FeedbackViewModel
    {
        public Feedback FeedbackExample { get; set; } = new Feedback();

        public IEnumerable<Feedback> feedbacks { get; set; } = null!;

        public List<IFormFile> files { get; set; }

        public FeedbackViewModel()
        {
            
        }
    }
}
