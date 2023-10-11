using System.ComponentModel.DataAnnotations;

namespace VartanMVCv2.Domain.Entities
{
    public class CompletedProjectPhoto:EntitiesBase
    {
        public CompletedProject? Project { get; set; }
        public string? ImagePath { get; set; }

        public CompletedProjectPhoto()
        {
            ID = Guid.NewGuid();
        }
    }
}
