using System.ComponentModel.DataAnnotations;

namespace VartanMVCv2.Domain.Entities
{
    public class CompletedProjectPhoto:EntitiesBase
    {
        [Required]
        public CompletedProject Project { get; set; } = null!;
        [Required]
        public string ImagePath { get; set; } = null!;

        public CompletedProjectPhoto()
        {
            ID = Guid.NewGuid();
        }
    }
}
