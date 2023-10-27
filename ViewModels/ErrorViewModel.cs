using VartanMVCv2.Models;

namespace VartanMVCv2.ViewModels
{
    public class ErrorViewModel
    {
        public string? Errors { get; set; } = string.Empty;

        public CustomError? customError { get; set; } = new CustomError();

        public Exception? Exception { get; set; } = new Exception();

        public ErrorViewModel()
        {
            
        }
    }
}
