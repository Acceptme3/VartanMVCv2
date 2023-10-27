using System.ComponentModel.DataAnnotations;

namespace VartanMVCv2.Domain.Entities
{
    public class CompletedProjectPhoto:EntitiesBase
    {
        public virtual CompletedProject Project { get; set; } = new CompletedProject();
        public Guid CompletedProjectID { get; set; } 
        public override string? Description { get; set; }
        public string? ImagePath { get; set; }

        public CompletedProjectPhoto()
        {
            ID = Guid.NewGuid();
        }
    }
}
