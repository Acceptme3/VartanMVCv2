using System.Text.Json;

namespace VartanMVCv2.Services.Dto
{
    public class ErrorDto
    {
        public string? Message { get; set; }
        public int StatusCode { get; set; } = 0;

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
