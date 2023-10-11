namespace VartanMVCv2.Models
{
    public class CustomError
    {
        public FileCheckResult? FileCheckResult { get; set; }

        public Dictionary<string, string[]>? ModelStateErrors { get; set; }
    }
}
