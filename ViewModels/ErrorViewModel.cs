using VartanMVCv2.Models;

namespace VartanMVCv2.ViewModels
{
    public class ErrorViewModel
    {
        public string? Errors { get; set; } = string.Empty;

        public CustomError? customError { get; set; }

        public ErrorViewModel()
        {
            
        }
    }
}
